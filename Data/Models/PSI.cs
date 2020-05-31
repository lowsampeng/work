namespace Singapore.PSI
{
    public class PSI
    {
        public Api_Info api_info { get; set; }
        public Region_Metadata[] region_metadata { get; set; }
        public Item[] items { get; set; }

        public class Api_Info
        {
            public string status { get; set; }
        }

        public class Region_Metadata
        {
            public string name { get; set; }
            public Label_Location label_location { get; set; }
        }

        public class Label_Location
        {
            public float longitude { get; set; }
            public float latitude { get; set; }
        }

        public class Item
        {
            // Time of acquisition of data from NEA
            public string update_timestamp { get; set; }

            public string timestamp { get; set; }

            // Overall and regional PSI data including pollutant concentrations and sub-indices
            public Readings readings { get; set; }
        }

        public class Readings
        {
            public Region psi_twenty_four_hourly { get; set; }
            public Region psi_three_hourly { get; set; }
            public Region pm10_sub_index { get; set; }
            public Region pm25_sub_index { get; set; }
            public Region so2_sub_index { get; set; }
            public Region o3_sub_index { get; set; }
            public Region co_sub_index { get; set; }

            // Concentration is measured in micrograms per cubic metre
            public Region pm10_twenty_four_hourly { get; set; }

            // Concentration is measured in micrograms per cubic metre
            public Region pm25_twenty_four_hourly { get; set; }

            // Concentration is measured in micrograms per cubic metre
            public Region no2_one_hour_max { get; set; }

            // Concentration is measured in micrograms per cubic metre
            public Region so2_twenty_four_hourly { get; set; }

            // Concentration is measured in micrograms per cubic metre
            public Region co_eight_hour_max { get; set; }

            // Concentration is measured in micrograms per cubic metre
            public Region o3_eight_hour_max { get; set; }
        }

        public class Region
        {
            public float national { get; set; }
            public float north { get; set; }
            public float south { get; set; }
            public float east { get; set; }
            public float west { get; set; }
            public float central { get; set; }
        }
    }
}