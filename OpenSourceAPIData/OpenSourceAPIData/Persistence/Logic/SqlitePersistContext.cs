using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using OpenSourceAPIData.Extensions;
using OpenSourceAPIData.Persistence.Models;
using System.IO;

namespace OpenSourceAPIData.Persistence.Logic
{
    class SqlitePersistContext : IDisposable
    {
        private const string DatabaseSuffix = "database";
        private const string TableSuffix = "table";

        private string Topic;
        private string connectionString;
        private SQLiteConnection connection;

        public SqlitePersistContext(string topic)
        {
            Topic = topic;
            Directory.CreateDirectory(Topic);
        }

        public void Create<T>()
        {
            string databaseName = GetDatabase<T>();

            Create(databaseName);

            Open();
            CreateTables<T>();
            Close();
        }

        public void Open()
        {
            connection = new SQLiteConnection(connectionString);
            connection.Open();
        }

        public void Close()
        {
            connection.Close();
            connection = null;
        }

        public void Create(string databaseName)
        {
            string fileName = $".\\{Topic}\\{databaseName}.sqlite";
            connectionString = $"Data Source={fileName};Version=3;";
            SQLiteConnection.CreateFile(fileName);
        }

        private string GetDatabase<T>()
        {
            Type dbtype = typeof(T);
            var dbDatabaseAttrib = dbtype.GetCustomAttribute<DBDatabaseAttribute>();

            string databaseName = "";

            if (dbDatabaseAttrib != null && !string.IsNullOrWhiteSpace(dbDatabaseAttrib.Name))
                databaseName = dbDatabaseAttrib.Name;
            else
            {
                string className = dbtype.Name;
                if (className.ToLower().EndsWith(DatabaseSuffix))
                    databaseName = className.Substring(0, className.Length - DatabaseSuffix.Length);
            }

            return databaseName; 
        }

        private void CreateTables<T>()
        {
            Type dbtype = typeof(T);
            PropertyInfo[] publicProperties = dbtype.GetProperties(BindingFlags.Instance |
                 BindingFlags.Public);

            if (publicProperties == null || publicProperties.Length <= 0) return;

            foreach (var item in publicProperties)
            {
                Type tabletype = item.PropertyType;
                var tableAttribute = tabletype.GetCustomAttribute<DBTableAttribute>();
                CreateTable(tabletype, tableAttribute);
            }
        }

        private void CreateTable(Type tableType, DBTableAttribute tableAttribute)
        {
            string tableName = "";

            if (tableAttribute != null && !string.IsNullOrWhiteSpace(tableAttribute.Name))
                tableName = tableAttribute.Name;
            else
            {
                string className = tableType.Name;
                if (className.ToLower().EndsWith(TableSuffix))
                    tableName = className.Substring(0, className.Length - TableSuffix.Length);
            }

            var colInfos = new List<ColumnInformation>();
            var tablePublicProperties = tableType.GetProperties(BindingFlags.Instance |
                 BindingFlags.Public);

            foreach (var item in tablePublicProperties)
            {
                var dbIgnoreAttr = item.GetCustomAttribute<DBIgnoreAttribute>();
                if (dbIgnoreAttr != null) continue;

                var dbColAttr = item.GetCustomAttribute<DBColumnAttribute>();

                var colInfo = CreateColumnInfo(item, dbColAttr);
                colInfos.Add(colInfo);
            }

            var query = CreateTableQueryBuilder(tableName, colInfos);
            

            ExecuteNonQuery(query);
        }

        private string CreateTableQueryBuilder(string tableName, List<ColumnInformation> colInfos)
        {
            var dbcolQueries = new List<string>();
            foreach (var item in colInfos)
            {
                dbcolQueries.Add(ColumnString(item));
            }

            string query = $"CREATE TABLE IF NOT EXISTS {tableName}";

            var colListQuery = string.Join(",", dbcolQueries);
            query += $" ({colListQuery})";

            return query;
        }

        private void ExecuteNonQuery(string query)
        {
            var command = connection.CreateCommand();
            command.CommandText = query;
            command.ExecuteNonQuery();
        }

        private ColumnInformation CreateColumnInfo(PropertyInfo propInfo, DBColumnAttribute dbColAttr)
        {
            var colInfo = new ColumnInformation();

            Type colType = propInfo.PropertyType;

            if (dbColAttr != null && !string.IsNullOrWhiteSpace(dbColAttr.Name))
                colInfo.Name = dbColAttr.Name;
            else
                colInfo.Name = propInfo.Name;

            colInfo.IsNullable = (dbColAttr != null)? dbColAttr.Nullable : DBColumnAttribute.DefaultNullable;
            colInfo.IsPrimary = (dbColAttr != null) ? dbColAttr.Primary : DBColumnAttribute.DefaultPrimary;
            colInfo.IsAutoIncrement = (dbColAttr != null) ? dbColAttr.AutoIncrement : DBColumnAttribute.DefaultAutoIncrement;
            colInfo.IsUnique = (dbColAttr != null) ? dbColAttr.Unique : DBColumnAttribute.DefaultUnique;
            colInfo.DbType = colType;

            return colInfo;
        }

        private string ColumnString(ColumnInformation colInfo)
        {
            StringBuilder colQueryString = new StringBuilder($"{colInfo.Name} {GetType(colInfo.DbType)}");

            if (!colInfo.IsNullable)
                colQueryString.Append(" NOT NULL");

            if(colInfo.IsPrimary) colQueryString.Append(" PRIMARY KEY");
            if (colInfo.IsAutoIncrement) colQueryString.Append(" AUTOINCREMENT");
            if (colInfo.IsUnique) colQueryString.Append(" UNIQUE");

            return colQueryString.ToString();
        }

        private string GetType(Type colType)
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

        public void Dispose()
        {
            if (connection != null) Close();
        }
    }
}
