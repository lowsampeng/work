using System;

namespace Singapore.FourDayWeatherForecast
{
    // Raw 4-day Weather Forecast from Data.gov.sg

    public class FourDayWeatherForecast
    {
        public Item[] items { get; set; }
        public Api_Info api_info { get; set; }
    }

    public class Api_Info
    {
        public string status { get; set; }
    }

    public class Item
    {
        // Time of acquisition of data from NEA
        public string update_timestamp { get; set; }

        // Time forecast was issued by NEA
        public string timestamp { get; set; }

        // Chronologically ordered forecasts for the next 4 days
        public Forecast[] forecasts { get; set; }
    }

    public class Forecast
    {
        // Unit of measure - Degrees Celsius
        public Temperature temperature { get; set; }

        // Forecast Date
        public string date { get; set; }

        // Forecast summary for the day
        public string forecast { get; set; }

        // Unit of measure - Percentage
        public Relative_Humidity relative_humidity { get; set; }
        public Wind wind { get; set; }

        // Timestamp which indicates the start of the day
        public string timestamp { get; set; }
    }

    public class Temperature
    {
        public int low { get; set; }
        public int high { get; set; }
    }

    public class Relative_Humidity
    {
        public int low { get; set; }
        public int high { get; set; }
    }

    public class Wind
    {
        // Unit of measure - Kilometeres per hour
        public Speed speed { get; set; }
        public string direction { get; set; }
    }

    public class Speed
    {
        public int low { get; set; }
        public int high { get; set; }
    }
}
