﻿using log4net;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersistenceManagement
{
    public class SqlitePersistContext : DBContextBase
    {
        protected static ILog logger = LogManager.GetLogger(typeof(SqlitePersistContext));

        /// <summary>
        /// Create the local database file
        /// </summary>
        /// <param name="databaseName"></param>
        public override void CreateDatabase(string databaseName, string path)
        {
            string fileName = $".\\{path}\\{databaseName}.sqlite";
            connectionString = $"Data Source={fileName};Version=3;ConnectTimeout=60;";
            SQLiteConnection.CreateFile(fileName);
        }

        /// <summary>
        /// Execute a non query
        /// </summary>
        /// <param name="query"></param>
        public override void ExecuteNonQuery(string query)
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    var command = connection.CreateCommand();
                    command.CommandText = query;
                    command.ExecuteNonQuery();

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                logger.Error($"Error in sqlite for query {query} \nExecute: {ex.Message} \nStack: {ex.StackTrace}");
            }
        }
    }
}