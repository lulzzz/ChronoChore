using System.Data.SQLite;

namespace OpenSourceAPIData.Persistence.Logic
{
    public class SqlitePersistContext : DBContextBase
    {
        public SqlitePersistContext(string topic) : base(topic) { }
        
        public override void Open()
        {
            connection = new SQLiteConnection(connectionString);
            connection.Open();
        }

        /// <summary>
        /// Close() method throws exception if already disposed
        /// </summary>
        public override void Close()
        {
            try
            {
                if (connection != null)
                {
                    connection.Close();
                    connection = null;
                }
            }
            finally { }
        }

        public override void CreateDatabase(string databaseName)
        {
            string fileName = $".\\{Topic}\\{databaseName}.sqlite";
            connectionString = $"Data Source={fileName};Version=3;ConnectTimeout=60;";
            SQLiteConnection.CreateFile(fileName);
        }
        
        public void ExecuteNonQueryPrivate(string query)
        {
            var command = connection.CreateCommand();
            command.CommandText = query;
            command.ExecuteNonQuery();
        }

        public override void ExecuteNonQuery(string query)
        {
            using (connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                ExecuteNonQueryPrivate(query);
                connection.Close();
            }
        }

        public override void Insert(string query)
        {
            using (connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                ExecuteNonQueryPrivate(query);
                connection.Close();
            }
        }
    }
}
