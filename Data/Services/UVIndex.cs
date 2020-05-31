using System;
using System.Data;
using System.Linq;
using CommonClasses;
using System.Threading.Tasks;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using System.Data.SqlTypes;

namespace Singapore.UVIndex
{
    public class SingaporeUVIndexService
    {
        private Uri URL = new Uri(@"https://api.data.gov.sg/v1/environment/uv-index");
        private string StoredProcedure { get{ return @"[dbo].[UpdateUVIndex]";}}

        // fillup the data table for bulk update
        public async Task<DataTable> GetUVIndexAsync()
        {
            GeometryFactory geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

            CommonClass cls = new CommonClass();
            UVIndex obj = await cls.GetDataAsync<UVIndex>(URL);

            DataTable dataTable = new DataTable("UVIndex");
            dataTable.Columns.Add("id", typeof(String));
            dataTable.Columns.Add("name", typeof(String));
            dataTable.Columns.Add("geom", typeof(System.Data.SqlTypes.SqlBytes));
            dataTable.Columns.Add("value", typeof(float));
            dataTable.Columns.Add("timestamp", typeof(DateTime));
            if (obj.api_info.status.ToLower() == "healthy") {
/*                foreach (Station s in obj.metadata.stations) {
                    dataTable.Rows.Add(new object[] {
                        s.id,
                        s.name,
                        new SqlBytes(geometryFactory.CreatePoint(new Coordinate(s.location.longitude, s.location.latitude)).AsBinary()),
                        obj.items.Select(i => i.readings.Where(r => r.station_id == s.id).Select(r => r.value)).ToList()[0].FirstOrDefault(),
                        Convert.ToDateTime(obj.items[0].timestamp)
                    });
                }*/
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