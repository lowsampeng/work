using System;
using System.Data;
using CommonClasses;
using System.Threading.Tasks;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using System.Data.SqlTypes;

namespace Singapore.PSI
{
    public class SingaporePSIService
    {
        private Uri URL = new Uri(@"https://api.data.gov.sg/v1/environment/psi");
        private string StoredProcedure { get{ return @"[dbo].[UpdatePSI]";}}

        // fillup the data table for bulk update
        public async Task<DataTable> GetPSIAsync()
        {
            GeometryFactory geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

            CommonClass cls = new CommonClass();
            PSI obj = await cls.GetDataAsync<PSI>(URL);

            DataTable dataTable = new DataTable("PSI");
            dataTable.Columns.Add("id", typeof(String));
            //dataTable.Columns.Add("geom", typeof(SqlGeometry));
            dataTable.Columns.Add("geom", typeof(System.Data.SqlTypes.SqlBytes));
            dataTable.Columns.Add("timestamp", typeof(DateTime));
            dataTable.Columns.Add("update_timestamp", typeof(DateTime));
            string[] types = {"psi_24hr", "psi_3hr", "pm10_sub_index", "pm25_sub_index",
                              "so2_sub_index", "o3_sub_index", "co_sub_index", "pm10_24hr",
                              "pm25_24hr", "no2_1hr_max", "so2_24hr", "co_8hr_max", "o3_8hr_max" };
            foreach (string type in types) {
                dataTable.Columns.Add(type, typeof(float)).AllowDBNull = true;
            }
            if (obj.api_info.status.ToLower() == "healthy") {
                foreach (PSI.Region_Metadata md in obj.region_metadata)
                {
                    if (md.name == "north")
                    {
                        dataTable.Rows.Add(new object[] {
                            md.name,
//                            SqlGeometry.Point(md.label_location.longitude, md.label_location.latitude, 4326).STAsBinary(),
//                            geometryFactory.CreatePoint(new NetTopologySuite.Geometries.Coordinate(md.label_location.longitude, md.label_location.latitude)).AsBinary(),
                            new SqlBytes(geometryFactory.CreatePoint(new Coordinate(md.label_location.longitude, md.label_location.latitude)).AsBinary()),
                            Convert.ToDateTime(obj.items[0].timestamp),
                            Convert.ToDateTime(obj.items[0].update_timestamp),
                            ((obj.items[0].readings.psi_twenty_four_hourly != null)? obj.items[0].readings.psi_twenty_four_hourly.north: Convert.DBNull),
                            ((obj.items[0].readings.psi_three_hourly != null)? obj.items[0].readings.psi_three_hourly.north: Convert.DBNull),
                            ((obj.items[0].readings.pm10_sub_index != null)? obj.items[0].readings.pm10_sub_index.north: Convert.DBNull),
                            ((obj.items[0].readings.pm25_sub_index != null)? obj.items[0].readings.pm25_sub_index.north: Convert.DBNull),
                            ((obj.items[0].readings.so2_sub_index != null)? obj.items[0].readings.so2_sub_index.north: Convert.DBNull),
                            ((obj.items[0].readings.o3_sub_index != null)? obj.items[0].readings.o3_sub_index.north: Convert.DBNull),
                            ((obj.items[0].readings.co_sub_index != null)? obj.items[0].readings.co_sub_index.north: Convert.DBNull),
                            ((obj.items[0].readings.pm10_twenty_four_hourly != null)? obj.items[0].readings.pm10_twenty_four_hourly.north: Convert.DBNull),
                            ((obj.items[0].readings.pm25_twenty_four_hourly != null)? obj.items[0].readings.pm25_twenty_four_hourly.north: Convert.DBNull),
                            ((obj.items[0].readings.no2_one_hour_max != null)? obj.items[0].readings.no2_one_hour_max.north: Convert.DBNull),
                            ((obj.items[0].readings.so2_twenty_four_hourly != null)? obj.items[0].readings.so2_twenty_four_hourly.north: Convert.DBNull),
                            ((obj.items[0].readings.co_eight_hour_max != null)? obj.items[0].readings.co_eight_hour_max.north: Convert.DBNull),
                            ((obj.items[0].readings.o3_eight_hour_max != null)? obj.items[0].readings.o3_eight_hour_max.north: Convert.DBNull),
                        });
                    }
                    else if (md.name == "south")
                    {
                        dataTable.Rows.Add(new object[] {
                            md.name,
//                            SqlGeometry.Point(md.label_location.longitude, md.label_location.latitude, 4326).STAsBinary(),
//                            geometryFactory.CreatePoint(new NetTopologySuite.Geometries.Coordinate(md.label_location.longitude, md.label_location.latitude)).AsBinary(),
                            new SqlBytes(geometryFactory.CreatePoint(new Coordinate(md.label_location.longitude, md.label_location.latitude)).AsBinary()),
                            Convert.ToDateTime(obj.items[0].timestamp),
                            Convert.ToDateTime(obj.items[0].update_timestamp),
                            ((obj.items[0].readings.psi_twenty_four_hourly != null)? obj.items[0].readings.psi_twenty_four_hourly.south: Convert.DBNull),
                            ((obj.items[0].readings.psi_three_hourly != null)? obj.items[0].readings.psi_three_hourly.south: Convert.DBNull),
                            ((obj.items[0].readings.pm10_sub_index != null)? obj.items[0].readings.pm10_sub_index.south: Convert.DBNull),
                            ((obj.items[0].readings.pm25_sub_index != null)? obj.items[0].readings.pm25_sub_index.south: Convert.DBNull),
                            ((obj.items[0].readings.so2_sub_index != null)? obj.items[0].readings.so2_sub_index.south: Convert.DBNull),
                            ((obj.items[0].readings.o3_sub_index != null)? obj.items[0].readings.o3_sub_index.south: Convert.DBNull),
                            ((obj.items[0].readings.co_sub_index != null)? obj.items[0].readings.co_sub_index.south: Convert.DBNull),
                            ((obj.items[0].readings.pm10_twenty_four_hourly != null)? obj.items[0].readings.pm10_twenty_four_hourly.south: Convert.DBNull),
                            ((obj.items[0].readings.pm25_twenty_four_hourly != null)? obj.items[0].readings.pm25_twenty_four_hourly.south: Convert.DBNull),
                            ((obj.items[0].readings.no2_one_hour_max != null)? obj.items[0].readings.no2_one_hour_max.south: Convert.DBNull),
                            ((obj.items[0].readings.so2_twenty_four_hourly != null)? obj.items[0].readings.so2_twenty_four_hourly.south: Convert.DBNull),
                            ((obj.items[0].readings.co_eight_hour_max != null)? obj.items[0].readings.co_eight_hour_max.south: Convert.DBNull),
                            ((obj.items[0].readings.o3_eight_hour_max != null)? obj.items[0].readings.o3_eight_hour_max.south: Convert.DBNull),
                        });
                    }
                    else if (md.name == "east")
                    {
                        dataTable.Rows.Add(new object[] {
                            md.name,
//                            SqlGeometry.Point(md.label_location.longitude, md.label_location.latitude, 4326).STAsBinary(),
//                            geometryFactory.CreatePoint(new NetTopologySuite.Geometries.Coordinate(md.label_location.longitude, md.label_location.latitude)).AsBinary(),
                            new SqlBytes(geometryFactory.CreatePoint(new Coordinate(md.label_location.longitude, md.label_location.latitude)).AsBinary()),
                            Convert.ToDateTime(obj.items[0].timestamp),
                            Convert.ToDateTime(obj.items[0].update_timestamp),
                            ((obj.items[0].readings.psi_twenty_four_hourly != null)? obj.items[0].readings.psi_twenty_four_hourly.east: Convert.DBNull),
                            ((obj.items[0].readings.psi_three_hourly != null)? obj.items[0].readings.psi_three_hourly.east: Convert.DBNull),
                            ((obj.items[0].readings.pm10_sub_index != null)? obj.items[0].readings.pm10_sub_index.east: Convert.DBNull),
                            ((obj.items[0].readings.pm25_sub_index != null)? obj.items[0].readings.pm25_sub_index.east: Convert.DBNull),
                            ((obj.items[0].readings.so2_sub_index != null)? obj.items[0].readings.so2_sub_index.east: Convert.DBNull),
                            ((obj.items[0].readings.o3_sub_index != null)? obj.items[0].readings.o3_sub_index.east: Convert.DBNull),
                            ((obj.items[0].readings.co_sub_index != null)? obj.items[0].readings.co_sub_index.east: Convert.DBNull),
                            ((obj.items[0].readings.pm10_twenty_four_hourly != null)? obj.items[0].readings.pm10_twenty_four_hourly.east: Convert.DBNull),
                            ((obj.items[0].readings.pm25_twenty_four_hourly != null)? obj.items[0].readings.pm25_twenty_four_hourly.east: Convert.DBNull),
                            ((obj.items[0].readings.no2_one_hour_max != null)? obj.items[0].readings.no2_one_hour_max.east: Convert.DBNull),
                            ((obj.items[0].readings.so2_twenty_four_hourly != null)? obj.items[0].readings.so2_twenty_four_hourly.east: Convert.DBNull),
                            ((obj.items[0].readings.co_eight_hour_max != null)? obj.items[0].readings.co_eight_hour_max.east: Convert.DBNull),
                            ((obj.items[0].readings.o3_eight_hour_max != null)? obj.items[0].readings.o3_eight_hour_max.east: Convert.DBNull),
                        });
                    }
                    else if (md.name == "west")
                    {
                        dataTable.Rows.Add(new object[] {
                            md.name,
//                            SqlGeometry.Point(md.label_location.longitude, md.label_location.latitude, 4326).STAsBinary(),
//                            geometryFactory.CreatePoint(new NetTopologySuite.Geometries.Coordinate(md.label_location.longitude, md.label_location.latitude)).AsBinary(),
                            new SqlBytes(geometryFactory.CreatePoint(new Coordinate(md.label_location.longitude, md.label_location.latitude)).AsBinary()),
                            Convert.ToDateTime(obj.items[0].timestamp),
                            Convert.ToDateTime(obj.items[0].update_timestamp),
                            ((obj.items[0].readings.psi_twenty_four_hourly != null)? obj.items[0].readings.psi_twenty_four_hourly.west: Convert.DBNull),
                            ((obj.items[0].readings.psi_three_hourly != null)? obj.items[0].readings.psi_three_hourly.west: Convert.DBNull),
                            ((obj.items[0].readings.pm10_sub_index != null)? obj.items[0].readings.pm10_sub_index.west: Convert.DBNull),
                            ((obj.items[0].readings.pm25_sub_index != null)? obj.items[0].readings.pm25_sub_index.west: Convert.DBNull),
                            ((obj.items[0].readings.so2_sub_index != null)? obj.items[0].readings.so2_sub_index.west: Convert.DBNull),
                            ((obj.items[0].readings.o3_sub_index != null)? obj.items[0].readings.o3_sub_index.west: Convert.DBNull),
                            ((obj.items[0].readings.co_sub_index != null)? obj.items[0].readings.co_sub_index.west: Convert.DBNull),
                            ((obj.items[0].readings.pm10_twenty_four_hourly != null)? obj.items[0].readings.pm10_twenty_four_hourly.west: Convert.DBNull),
                            ((obj.items[0].readings.pm25_twenty_four_hourly != null)? obj.items[0].readings.pm25_twenty_four_hourly.west: Convert.DBNull),
                            ((obj.items[0].readings.no2_one_hour_max != null)? obj.items[0].readings.no2_one_hour_max.west: Convert.DBNull),
                            ((obj.items[0].readings.so2_twenty_four_hourly != null)? obj.items[0].readings.so2_twenty_four_hourly.west: Convert.DBNull),
                            ((obj.items[0].readings.co_eight_hour_max != null)? obj.items[0].readings.co_eight_hour_max.west: Convert.DBNull),
                            ((obj.items[0].readings.o3_eight_hour_max != null)? obj.items[0].readings.o3_eight_hour_max.west: Convert.DBNull),
                        });
                    }
                    else if (md.name == "central")
                    {
                        dataTable.Rows.Add(new object[] {
                            md.name,
//                            SqlGeometry.Point(md.label_location.longitude, md.label_location.latitude, 4326).STAsBinary(),
//                            geometryFactory.CreatePoint(new NetTopologySuite.Geometries.Coordinate(md.label_location.longitude, md.label_location.latitude)).AsBinary(),
                            new SqlBytes(geometryFactory.CreatePoint(new Coordinate(md.label_location.longitude, md.label_location.latitude)).AsBinary()),
                            Convert.ToDateTime(obj.items[0].timestamp),
                            Convert.ToDateTime(obj.items[0].update_timestamp),
                            ((obj.items[0].readings.psi_twenty_four_hourly != null)? obj.items[0].readings.psi_twenty_four_hourly.central: Convert.DBNull),
                            ((obj.items[0].readings.psi_three_hourly != null)? obj.items[0].readings.psi_three_hourly.central: Convert.DBNull),
                            ((obj.items[0].readings.pm10_sub_index != null)? obj.items[0].readings.pm10_sub_index.central: Convert.DBNull),
                            ((obj.items[0].readings.pm25_sub_index != null)? obj.items[0].readings.pm25_sub_index.central: Convert.DBNull),
                            ((obj.items[0].readings.so2_sub_index != null)? obj.items[0].readings.so2_sub_index.central: Convert.DBNull),
                            ((obj.items[0].readings.o3_sub_index != null)? obj.items[0].readings.o3_sub_index.central: Convert.DBNull),
                            ((obj.items[0].readings.co_sub_index != null)? obj.items[0].readings.co_sub_index.central: Convert.DBNull),
                            ((obj.items[0].readings.pm10_twenty_four_hourly != null)? obj.items[0].readings.pm10_twenty_four_hourly.central: Convert.DBNull),
                            ((obj.items[0].readings.pm25_twenty_four_hourly != null)? obj.items[0].readings.pm25_twenty_four_hourly.central: Convert.DBNull),
                            ((obj.items[0].readings.no2_one_hour_max != null)? obj.items[0].readings.no2_one_hour_max.central: Convert.DBNull),
                            ((obj.items[0].readings.so2_twenty_four_hourly != null)? obj.items[0].readings.so2_twenty_four_hourly.central: Convert.DBNull),
                            ((obj.items[0].readings.co_eight_hour_max != null)? obj.items[0].readings.co_eight_hour_max.central: Convert.DBNull),
                            ((obj.items[0].readings.o3_eight_hour_max != null)? obj.items[0].readings.o3_eight_hour_max.central: Convert.DBNull),
                        });
                    }
                    else if (md.name == "national")
                    {
                        dataTable.Rows.Add(new object[] {
                            md.name,
//                            SqlGeometry.Point(md.label_location.longitude, md.label_location.latitude, 4326).STAsBinary(),
//                            geometryFactory.CreatePoint(new NetTopologySuite.Geometries.Coordinate(md.label_location.longitude, md.label_location.latitude)).AsBinary(),
                            new SqlBytes(geometryFactory.CreatePoint(new Coordinate(103.6955, 1.4562)).AsBinary()),
                            Convert.ToDateTime(obj.items[0].timestamp),
                            Convert.ToDateTime(obj.items[0].update_timestamp),
                            ((obj.items[0].readings.psi_twenty_four_hourly != null)? obj.items[0].readings.psi_twenty_four_hourly.central: Convert.DBNull),
                            ((obj.items[0].readings.psi_three_hourly != null)? obj.items[0].readings.psi_three_hourly.central: Convert.DBNull),
                            ((obj.items[0].readings.pm10_sub_index != null)? obj.items[0].readings.pm10_sub_index.central: Convert.DBNull),
                            ((obj.items[0].readings.pm25_sub_index != null)? obj.items[0].readings.pm25_sub_index.central: Convert.DBNull),
                            ((obj.items[0].readings.so2_sub_index != null)? obj.items[0].readings.so2_sub_index.central: Convert.DBNull),
                            ((obj.items[0].readings.o3_sub_index != null)? obj.items[0].readings.o3_sub_index.central: Convert.DBNull),
                            ((obj.items[0].readings.co_sub_index != null)? obj.items[0].readings.co_sub_index.central: Convert.DBNull),
                            ((obj.items[0].readings.pm10_twenty_four_hourly != null)? obj.items[0].readings.pm10_twenty_four_hourly.central: Convert.DBNull),
                            ((obj.items[0].readings.pm25_twenty_four_hourly != null)? obj.items[0].readings.pm25_twenty_four_hourly.central: Convert.DBNull),
                            ((obj.items[0].readings.no2_one_hour_max != null)? obj.items[0].readings.no2_one_hour_max.central: Convert.DBNull),
                            ((obj.items[0].readings.so2_twenty_four_hourly != null)? obj.items[0].readings.so2_twenty_four_hourly.central: Convert.DBNull),
                            ((obj.items[0].readings.co_eight_hour_max != null)? obj.items[0].readings.co_eight_hour_max.central: Convert.DBNull),
                            ((obj.items[0].readings.o3_eight_hour_max != null)? obj.items[0].readings.o3_eight_hour_max.central: Convert.DBNull),
                        });
                    }
                }
                if (dataTable.Rows.Count > 0) {
                    if (!cls.UpdateDatabase(dataTable, StoredProcedure)) {
                        return new DataTable("PSI");
                    }
                }
            }
            return dataTable;
        }
    }
}