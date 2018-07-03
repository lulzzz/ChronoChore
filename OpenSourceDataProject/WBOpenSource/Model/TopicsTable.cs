using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBOpenSource.Model
{
    public class TopicsTable
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public string SourceNote { get; set; }

        public static string CreateQuery()
        {
            return $"CREATE TABLE IF NOT EXISTS {nameof(TopicsTable)} (" +
                $"{nameof(Id)} INT PRIMARY KEY, " +
                $"{nameof(Value)} TEXT NOT NULL, " +
                $"{nameof(SourceNote)} TEXT)";
        }

        public static string InsertQuery(TopicsTable[] topics)
        {
            var colList = topics.Select(item => $"({item.Id},'{Normalize(item.Value)}','{Normalize(item.SourceNote)}')")
                                .ToList();

            return $"INSERT INTO {nameof(TopicsTable)} VALUES {(string.Join(",", colList))}";
        }

        public static string Normalize(string queryValue)
        {
            return queryValue?.Replace("'", "''");
        }

        internal static string SelectTopicIds()
        {
            return $"SELECT {nameof(Id)} FROM {nameof(TopicsTable)}";
        }
    }
}
