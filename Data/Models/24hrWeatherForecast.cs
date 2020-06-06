using System;

namespace Singapore.TwentyFourHourWeatherForecast
{
    // Raw 24-hr Weather Forecast from Data.gov.sg
    public class TwentyFourHourWeatherForecast
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

        // Period of time the forecast is valid for
        public Valid_Period valid_period { get; set; }

        // A general weather forecast for the 24 hour period
        public General general { get; set; }
        public Period[] periods { get; set; }
    }

    public class Valid_Period
    {
        public string start { get; set; }
        public string end { get; set; }
    }

    public class General
    {
        public string forecast { get; set; }

        // Unit of measure - Percentage
        public Relative_Humidity relative_humidity { get; set; }

        // Unit of measure - Degrees Celsius
        public Temperature temperature { get; set; }
        public Wind wind { get; set; }
    }

    public class Relative_Humidity
    {
        public int low { get; set; }
        public int high { get; set; }
    }

    public class Temperature
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

    public class Period
    {
        public Time time { get; set; }
        public Regions regions { get; set; }
    }

    public class Time
    {
        public string start { get; set; }
        public string end { get; set; }
    }

    public class Regions
    {
        public string west { get; set; }
        public string east { get; set; }
        public string central { get; set; }
        public string south { get; set; }
        public string north { get; set; }
    }
}
/* json example
{"items":[
    {"update_timestamp":"2020-06-05T10:06:18+08:00",
     "timestamp":"2020-06-05T09:46:00+08:00",
     "valid_period":{
        "start":"2020-06-05T06:00:00+08:00",
        "end":"2020-06-06T06:00:00+08:00"
     },
     "general":{
        "forecast":"Partly Cloudy (Day)",
        "relative_humidity":{"low":70,"high":100},
        "temperature":{"low":24,"high":33},
        "wind":{
            "speed":{"low":10,"high":20},
            "direction":"SSW"}
     },
     "periods":[
        {"time":{
            "start":"2020-06-05T06:00:00+08:00",
            "end":"2020-06-05T12:00:00+08:00"},
         "regions":{
            "west":"Partly Cloudy (Day)",
            "east":"Partly Cloudy (Day)",
            "central":"Partly Cloudy (Day)",
            "south":"Partly Cloudy (Day)",
            "north":"Partly Cloudy (Day)"}
        },
        {"time":{
            "start":"2020-06-05T12:00:00+08:00",
            "end":"2020-06-05T18:00:00+08:00"},
         "regions":{
            "west":"Partly Cloudy (Day)",
            "east":"Partly Cloudy (Day)",
            "central":"Partly Cloudy (Day)",
            "south":"Partly Cloudy (Day)",
            "north":"Partly Cloudy (Day)"}
        },
        {"time":{
            "start":"2020-06-05T18:00:00+08:00",
            "end":"2020-06-06T06:00:00+08:00"},
         "regions":{
            "west":"Partly Cloudy (Night)",
            "east":"Partly Cloudy (Night)",
            "central":"Partly Cloudy (Night)",
            "south":"Partly Cloudy (Night)",
            "north":"Partly Cloudy (Night)"}
        }]
    }
 ],
 "api_info":{"status":"healthy"}
}
*/ 