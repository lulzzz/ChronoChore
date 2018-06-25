using AsyncAwaitDemo.Persistence;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace AsyncAwaitDemo
{
    class SqlitePersistence : IDisposable
    {
        private string topic;
        private string ConnectionString;
        private SQLiteConnection connection;

        public void Dispose()
        {
            if (connection != null)
                connection.Close();
            connection = null;
        }

        public void Create(string topic)
        {
            this.topic = topic;
            ConnectionString = string.Format("Data Source={0}.sqlite;Version=3;", topic);

            if (connection != null && connection.State != System.Data.ConnectionState.Closed)
                throw new Exception("Already open");
            connection = new SQLiteConnection(ConnectionString);
        }

        public void CreateTable(string tableName, IList<DBColumnModel> columns)
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = CreateTableQueryBuilder(tableName, columns);
            command.ExecuteNonQuery();
        }

        public void ExecuteNoQuery<T>(string table, IList<string> colNames, IList<T> list)
        {

        }

        private string CreateTableQueryBuilder(string tableName, IList<DBColumnModel> columns)
        {
            string query = $"CREATE TABLE IF NOT EXISTS {tableName}";

            List<string> colList = new List<string>();
            foreach (var item in columns)
            {
                string colQuery = $"{item.Name} {GetTypeString(item.ColType)}";
                if (item.IsNotNull) colQuery += " NOT NULL";
                if (item.IsPrimary) colQuery += " PRIMARY KEY";
                if (item.IsAutoIncrement) colQuery += " AUTOINCREMENT";
            }

            var colListQuery = string.Join(",", colList);
            query += $" ({colListQuery})";

            return query;
        }

        private string GetTypeString(Type colType)
        {
            if (colType == typeof(int) ||
                colType == typeof(short) ||
                colType == typeof(ushort) ||
                colType == typeof(uint) ||
                colType == typeof(long) ||
                colType == typeof(ulong))
            {
                return "INT";
            }
            else if (colType == typeof(char) ||
                colType == typeof(string))
            {
                return "TEXT";
            }
            else if (colType == typeof(float) ||
                colType == typeof(double) ||
                colType == typeof(decimal))
            {
                return "REAL";
            }
            else if (colType == typeof(bool) ||
                colType == typeof(DateTime))
            {
                return "NUMERIC";
            }
            else
                return "BLOB";
        }
    }
}
