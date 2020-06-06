using System;
using System.Data;
using CommonClasses;
using System.Threading.Tasks;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace Singapore.FourDayWeatherForecast
{
    public class SingaporeFourDayWeatherForecastService
    {
        private Uri URL = new Uri(@"https://api.data.gov.sg/v1/environment/4-day-weather-forecast");
        private string StoredProcedure { get{ return @"[dbo].[UpdateFourDayWeatherForecast]";}}

        // fillup the data table for bulk update
        public async Task<DataTable> GetDataAsync()
        {
            GeometryFactory geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

            CommonClass cls = new CommonClass();
            FourDayWeatherForecast obj = await cls.GetDataAsync<FourDayWeatherForecast>(URL);

            DataTable dataTable = new DataTable("FourDayWeatherForecast");
            dataTable.Columns.Add("date", typeof(String));
            dataTable.Columns.Add("forecast", typeof(String));
            dataTable.Columns.Add("temperature_low", typeof(int));
            dataTable.Columns.Add("temperature_high", typeof(int));
            dataTable.Columns.Add("relative_humidity_low", typeof(int));
            dataTable.Columns.Add("relative_humidity_high", typeof(int));
            dataTable.Columns.Add("wind_speed_low", typeof(int));
            dataTable.Columns.Add("wind_speed_high", typeof(int));
            dataTable.Columns.Add("wind_speed_direction", typeof(String));
            dataTable.Columns.Add("update_timestamp", typeof(DateTime));
            if (obj.api_info.status.ToLower() == "healthy") {
                foreach (Item i in obj.items) {
                    foreach (Forecast f in i.forecasts) {
                        dataTable.Rows.Add(new object[] {
                            f.date,
                            f.forecast,
                            f.temperature.low,
                            f.temperature.high,
                            f.relative_humidity.low,
                            f.relative_humidity.high,
                            f.wind.speed.low,
                            f.wind.speed.high,
                            f.wind.direction,
                            Convert.ToDateTime(i.update_timestamp)
                        });
                    }
                }
//                if (dataTable.Rows.Count > 0) {
//                    if (!cls.UpdateDatabase(dataTable, StoredProcedure)) {
//                        return new DataTable("FourDayWeatherForecast");
//                    }
//                }
            }
            return dataTable;
        }
    }
}