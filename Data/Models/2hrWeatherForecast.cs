using System;

namespace Singapore.TwoHourWeatherForecast
{
    // Raw 2-hr Weather Forecast from Data.gov.sg

    public class TwoHourWeatherForecast
    {
        public Area_Metadata[] area_metadata { get; set; }
        public Item[] items { get; set; }
        public Api_Info api_info { get; set; }
    }

    public class Api_Info
    {
        public string status { get; set; }
    }

    public class Area_Metadata
    {
        // Name of the area
        public string name { get; set; }

        // Provides longitude and latitude for placing readings on a map
        public Label_Location label_location { get; set; }
    }

    public class Label_Location
    {
        public float latitude { get; set; }
        public float longitude { get; set; }
    }

    public class Item
    {
        // Time of acquisition of data from NEA
        public string update_timestamp { get; set; }

        // Time forecast was issued by NEA
        public string timestamp { get; set; }

        // Period of time the forecast is valid for
        public Valid_Period valid_period { get; set; }

        // Forecasts for various areas in Singapore
        public Forecast[] forecasts { get; set; }
    }

    public class Valid_Period
    {
        public string start { get; set; }
        public string end { get; set; }
    }

    public class Forecast
    {
        public string area { get; set; }
        public string forecast { get; set; }
    }
}
