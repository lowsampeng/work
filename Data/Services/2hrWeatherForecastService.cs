using System;
using System.Data;
using System.Linq;
using CommonClasses;
using System.Threading.Tasks;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using System.Data.SqlTypes;

namespace Singapore.TwoHourWeatherForecast
{
    public class SingaporeTwoHourWeatherForecastService
    {
        private Uri URL = new Uri(@"https://api.data.gov.sg/v1/environment/2-hour-weather-forecast");
        private string StoredProcedure { get{ return @"[dbo].[UpdateTwoHourWeatherForecast]";}}

        // fillup the data table for bulk update
        public async Task<DataTable> GetDataAsync()
        {
            GeometryFactory geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

            CommonClass cls = new CommonClass();
            TwoHourWeatherForecast obj = await cls.GetDataAsync<TwoHourWeatherForecast>(URL);

            DataTable dataTable = new DataTable("TwoHourWeatherForecast");
            dataTable.Columns.Add("name", typeof(String));
            dataTable.Columns.Add("geom", typeof(System.Data.SqlTypes.SqlBytes));
            dataTable.Columns.Add("forecast", typeof(String));
            dataTable.Columns.Add("validperiod_start", typeof(DateTime));
            dataTable.Columns.Add("validperiod_end", typeof(DateTime));
            dataTable.Columns.Add("update_timestamp", typeof(DateTime));
            if (obj.api_info.status.ToLower() == "healthy") {
                foreach (Area_Metadata a in obj.area_metadata) {
                    dataTable.Rows.Add(new object[] {
                        a.name,
                        new SqlBytes(geometryFactory.CreatePoint(new Coordinate(a.label_location.longitude, a.label_location.latitude)).AsBinary()),
                        obj.items.Select(i => i.forecasts.Where(r => r.area == a.name).Select(r => r.forecast)).ToList()[0].FirstOrDefault(),
                        Convert.ToDateTime(obj.items[0].valid_period.start),
                        Convert.ToDateTime(obj.items[0].valid_period.end),
                        Convert.ToDateTime(obj.items[0].update_timestamp)
                    });
                }
//                if (dataTable.Rows.Count > 0) {
//                    if (!cls.UpdateDatabase(dataTable, StoredProcedure)) {
//                        return new DataTable("TwoHourWeatherForecast");
//                    }
//                }
            }
            return dataTable;
        }
    }
}