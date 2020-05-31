namespace Singapore.UVIndex
{
    // Raw UV index from Data.gov.sg
    public class UVIndex
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

        public string timestamp { get; set; }

        // Reverse-chronologically ordered indexes
        public Index[] index { get; set; }
    }
    public class Index
    {
        // Timestamp indicating the start of the hour for which the index is for
        public string timestamp { get; set; }

        // UV index for the hour
        public float value { get; set; }
    }
}