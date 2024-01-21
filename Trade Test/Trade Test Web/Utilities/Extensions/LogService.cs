using System.Data;
using System.Data.SqlClient;


namespace Trade_Test.Utilities.Extensions
{

    public class LogService : ILogService
    {
        private const string INSERTEXCEPTIONQUERY = "INSERT INTO ExceptionLogs (CreatedDate, Exception) VALUES (@currentDate, @exception) ";
        private readonly string ConnectionString;

        public LogService(
            IEncryption encryption, 
            string connectionString
            )
        {
            ConnectionString = encryption.Decrypt(connectionString);
        }


        public void LogException(Exception ex)
        {
            if (ex != null)
            {
                var exceptionString = $"Exception Message {ex.Message} \n\n Inner Exception: {ex.InnerException} \n\n Stack Trace: {ex.StackTrace}";

                if (!string.IsNullOrEmpty(exceptionString))
                {
                    AddDataInLog(exceptionString);
                }
            }
        }


        private void AddDataInLog(string exceptionString)
        {
            string query = INSERTEXCEPTIONQUERY;

            // create connection and command
            using var cn = new SqlConnection(ConnectionString);
            using var cmd = new SqlCommand(query, cn);
            // define parameters and their values
            cmd.Parameters.Add("@currentDate", SqlDbType.DateTime).Value = DateTime.UtcNow;
            cmd.Parameters.Add("@exception", SqlDbType.NVarChar).Value = exceptionString;

            // open connection, execute INSERT, close connection
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
        }
    }
}
