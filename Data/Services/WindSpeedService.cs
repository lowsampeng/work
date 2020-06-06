using System;
using System.Data;
using System.Linq;
using CommonClasses;
using System.Threading.Tasks;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using System.Data.SqlTypes;

namespace Singapore.WindSpeed
{
    public class SingaporeWindSpeedService
    {
        private Uri URL = new Uri(@"https://api.data.gov.sg/v1/environment/wind-speed");
        private string StoredProcedure { get{ return @"[dbo].[UpdateWindSpeed]";}}

        // fillup the data table for bulk update
        public async Task<DataTable> GetDataAsync()
        {
            GeometryFactory geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

            CommonClass cls = new CommonClass();
            WindSpeed obj = await cls.GetDataAsync<WindSpeed>(URL);

            DataTable dataTable = new DataTable("WindSpeed");
            dataTable.Columns.Add("id", typeof(String));
            dataTable.Columns.Add("name", typeof(String));
            //dataTable.Columns.Add("geom", typeof(SqlGeometry));
            dataTable.Columns.Add("geom", typeof(System.Data.SqlTypes.SqlBytes));
            dataTable.Columns.Add("value", typeof(float));
            dataTable.Columns.Add("timestamp", typeof(DateTime));
            if (obj.api_info.status.ToLower() == "healthy") {
                float tokmh = 1.0F;
                if (obj.metadata.reading_unit == "knots") {
                    tokmh = 1.852F;
                }
                foreach (Station s in obj.metadata.stations) {
                    dataTable.Rows.Add(new object[] {
                        s.id,
                        s.name,
//                        SqlGeometry.Point(s.location.longitude, s.location.latitude, 4326).STAsBinary(),
//                        geometryFactory.CreatePoint(new NetTopologySuite.Geometries.Coordinate(s.location.longitude, s.location.latitude)).AsBinary(),
                        new SqlBytes(geometryFactory.CreatePoint(new Coordinate(s.location.longitude, s.location.latitude)).AsBinary()),
                        obj.items.Select(i => i.readings.Where(r => r.station_id == s.id).Select(r => r.value * tokmh)).ToList()[0].FirstOrDefault(),
                        Convert.ToDateTime(obj.items[0].timestamp)
                    });
                }
                if (dataTable.Rows.Count > 0) {
                    if (!cls.UpdateDatabase(dataTable, StoredProcedure)) {
                        return new DataTable("WindSpeed");
                    }
                }
            }
            return dataTable;
        }
    }
}