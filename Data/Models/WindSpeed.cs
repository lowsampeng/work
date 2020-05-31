namespace Singapore.WindSpeed
{
    // Raw Wind Speed from Data.gov.sg
    public class WindSpeed
    {
        public Metadata metadata { get; set; }
        public Item[] items { get; set; }
        public Api_Info api_info { get; set; }
    }

    public class Metadata
    {
        public Station[] stations { get; set; }

        // Information about the reading
        public string reading_type { get; set; }

        // Measurement unit for reading
        public string reading_unit { get; set; }
    }

    public class Station
    {
        // Stations's ID
        public string id { get; set; }

        // Reading Device's ID (usually same as Station's ID)
        public string device_id { get; set; }

        // Stations's name
        public string name { get; set; }

        // Location information for the station
        public Location location { get; set; }
    }

    public class Location
    {
        public float latitude { get; set; }
        public float longitude { get; set; }
    }

    public class Api_Info
    {
        public string status { get; set; }
    }

    public class Item
    {
        // Timestamp of reading
        public string timestamp { get; set; }
        public Reading[] readings { get; set; }
    }

    public class Reading
    {
        public string station_id { get; set; }
        public float value { get; set; }
    }
}