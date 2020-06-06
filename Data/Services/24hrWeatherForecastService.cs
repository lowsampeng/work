using System;
using System.Data;
using CommonClasses;
using System.Threading.Tasks;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace Singapore.TwentyFourHourWeatherForecast
{
    public class SingaporeTwentyFourHourWeatherForecastService
    {
        private Uri URL = new Uri(@"https://api.data.gov.sg/v1/environment/24-hour-weather-forecast");
        private string StoredProcedure { get{ return @"[dbo].[UpdateTwentyFourHourWeatherForecast]";}}

        // fillup the data table for bulk update
        public async Task<DataTable> GetDataAsync()
        {
            GeometryFactory geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

            CommonClass cls = new CommonClass();
            TwentyFourHourWeatherForecast obj = await cls.GetDataAsync<TwentyFourHourWeatherForecast>(URL);

            DataTable dataTable = new DataTable("TwentyFourHourWeatherForecast");
            dataTable.Columns.Add("validperiod_start", typeof(DateTime));
            dataTable.Columns.Add("validperiod_end", typeof(DateTime));
            dataTable.Columns.Add("general_forecast", typeof(String));
            dataTable.Columns.Add("general_relative_humidity_low", typeof(int));
            dataTable.Columns.Add("general_relative_humidity_high", typeof(int));
            dataTable.Columns.Add("general_temperature_low", typeof(int));
            dataTable.Columns.Add("general_temperature_high", typeof(int));
            dataTable.Columns.Add("general_wind_speed_low", typeof(int));
            dataTable.Columns.Add("general_wind_speed_high", typeof(int));
            dataTable.Columns.Add("general_wind_direction", typeof(String));
            dataTable.Columns.Add("update_timestamp", typeof(DateTime));
            if (obj.api_info.status.ToLower() == "healthy") {
                foreach (Item i in obj.items) {
                    dataTable.Rows.Add(new object[] {
                        i.valid_period.start,
                        i.valid_period.end,
                        i.general.forecast,
                        i.general.relative_humidity.low,
                        i.general.relative_humidity.high,
                        i.general.temperature.low,
                        i.general.temperature.high,
                        i.general.wind.speed.low,
                        i.general.wind.speed.high,
                        i.general.wind.direction,
                        Convert.ToDateTime(i.update_timestamp)
                    });
                }
//                if (dataTable.Rows.Count > 0) {
//                    if (!cls.UpdateDatabase(dataTable, StoredProcedure)) {
//                        return new DataTable("TwentyFourHourWeatherForecast");
//                    }
//                }
            }
            return dataTable;
        }
    }
}