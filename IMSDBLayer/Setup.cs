using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbUp;

namespace IMSDBLayer
{
    public class Setup
    {
        public Setup(string connstring)
        {
            var connectionString = connstring;
            //EnsureDatabase.For.SqlDatabase(connectionString);
            SetupDatabase(connstring);
            var upgrader =
                            DeployChanges.To
                            .SqlDatabase(connectionString)
                            .WithScriptsEmbeddedInAssembly(System.Reflection.Assembly.GetExecutingAssembly())
                            .LogToConsole()
                            .Build();

            var result = upgrader.PerformUpgrade();


            if (!result.Successful)
            {
                throw new Exception("Database is not updated");
            }
        }
        /// <summary>
        /// Create the database file if it doesn't exist
        /// </summary>
        /// <param name="connstring">Connection string to the database</param>
        private void SetupDatabase(string connstring)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connstring);

            string database = Path.Combine(AppDomain.CurrentDomain.GetData("DataDirectory").ToString(), builder.InitialCatalog + ".mdf");

            if (!File.Exists(database))
            {
                string dbName = builder.InitialCatalog;
                string log = Path.Combine(AppDomain.CurrentDomain.GetData("DataDirectory").ToString(), dbName + ".ldf");
                SqlConnectionStringBuilder newBuilder = new SqlConnectionStringBuilder();
                newBuilder["Data Source"] = builder.DataSource;
                newBuilder["Integrated Security"] = builder.IntegratedSecurity;

                SqlConnection conn = new SqlConnection(newBuilder.ConnectionString);
                string[] files = { database, log };
                var query = "CREATE DATABASE [" + dbName +
                    " ] ON PRIMARY" +
                    " (Name = [" + dbName + "_data]," +
                    " FILENAME = '" + files[0] + "'," +
                    " SIZE = 3MB," +
                    " MAXSIZE = 20MB," +
                    " FILEGROWTH = 10%)" +

                    " LOG ON" +
                    " (NAME = [" + dbName + "_log]," +
                    " FILENAME = '" + files[1] + "'," +
                    " SIZE = 1MB," +
                    " MAXSIZE = 5MB," +
                    " FILEGROWTH = 10%)" + ";";

                SqlCommand cmd = new SqlCommand(query, conn);
                using (conn)
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }


            }
        }
    }
}
