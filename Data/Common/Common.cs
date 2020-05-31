using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace CommonClasses
{
    public class CommonClass
    {
        public CommonClass(){}
        // connect to Data.gov.sg to get the data
        public async Task<T> GetDataAsync<T>(Uri uri) where T : class, new()
        {
            try {
                using (HttpClient client = new HttpClient())
                using (Stream data = await client.GetStreamAsync(uri)) {
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                    return (T)serializer.ReadObject(data) as T;
                }
            } catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return new T();
            }
        }

        // insert/update the database table using stored procedure
        public bool UpdateDatabase(DataTable dataTable, string updateSP)
        {
            // connect to database
            using (SqlConnection cnn = new SqlConnection(@"Data Source=HOME-PC;Initial Catalog=GEODWH;Integrated Security=True")) {
                cnn.Open();
                using (var transaction = cnn.BeginTransaction()) {
                    // Configure the SqlCommand and SqlParameter.  
                    SqlCommand insertCommand = new SqlCommand(updateSP, cnn, transaction) {
                        CommandType = CommandType.StoredProcedure
                    };
                    SqlParameter tvpParam = insertCommand.Parameters.AddWithValue("@TableVar", dataTable);
                    tvpParam.SqlDbType = SqlDbType.Structured;

                    // Execute the command.
                    insertCommand.ExecuteNonQuery();
                    transaction.Commit();
                    return true;
                }
            }
        }
    }
}