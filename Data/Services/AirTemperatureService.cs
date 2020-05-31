using System;
using System.Data;
using System.Linq;
using CommonClasses;
using System.Threading.Tasks;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using System.Data.SqlTypes;

namespace Singapore.AirTemperature
{
    public class SingaporeAirTemperatureService
    {
        private Uri URL = new Uri(@"https://api.data.gov.sg/v1/environment/air-temperature");
        private string StoredProcedure { get{ return @"[dbo].[UpdateAirTemperature]";}}

        // fillup the data table for bulk update
        public async Task<DataTable> GetAirTemperatureAsync()
        {
            GeometryFactory geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

            CommonClass cls = new CommonClass();
            AirTemperature obj = await cls.GetDataAsync<AirTemperature>(URL);

            DataTable dataTable = new DataTable("AirTemperature");
            dataTable.Columns.Add("id", typeof(String));
            dataTable.Columns.Add("name", typeof(String));
            dataTable.Columns.Add("geom", typeof(System.Data.SqlTypes.SqlBytes));
            dataTable.Columns.Add("value", typeof(float));
            dataTable.Columns.Add("timestamp", typeof(DateTime));
            if (obj.api_info.status.ToLower() == "healthy") {
                foreach (Station s in obj.metadata.stations) {
                    dataTable.Rows.Add(new object[] {
                        s.id,
                        s.name,
//                        SqlGeometry.Point(s.location.longitude, s.location.latitude, 4326).STAsBinary(),
                        new SqlBytes(geometryFactory.CreatePoint(new Coordinate(s.location.longitude, s.location.latitude)).AsBinary()),
                        obj.items.Select(i => i.readings.Where(r => r.station_id == s.id).Select(r => r.value)).ToList()[0].FirstOrDefault(),
                        Convert.ToDateTime(obj.items[0].timestamp)
                    });
                }
                if (dataTable.Rows.Count > 0) {
                    if (!cls.UpdateDatabase(dataTable, StoredProcedure)) {
                        return new DataTable("AirTemperature");
                    }
                }
            }
            return dataTable;
        }
    }
}