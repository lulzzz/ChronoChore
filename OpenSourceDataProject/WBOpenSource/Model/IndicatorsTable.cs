using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBOpenSource.Model
{
    public class IndicatorsTable
    {
        public string Id { get; set; }
        public int TopicId { get; set; }
        public string Name { get; set; }
        public int SourceId { get; set; }
        public string Source { get; set; }
        public string SourceNote { get; set; }
        public string SourceOrgaisation { get; set; }

        public static string Create()
        {
            return $"CREATE TABLE IF NOT EXISTS {nameof(IndicatorsTable).Substring(0, nameof(IndicatorsTable).Length - 5)} (" +
                $"{nameof(Id)} TEXT, " +
                $"{nameof(TopicId)} INT, " +
                $"{nameof(Name)} TEXT NOT NULL, " +
                $"{nameof(SourceId)} INT," +
                $"{nameof(Source)} TEXT," +
                $"{nameof(SourceNote)} TEXT, " +
                $"{nameof(SourceOrgaisation)} TEXT," +
                $"PRIMARY KEY ({nameof(Id)}, {nameof(TopicId)}))";
        }

        public static string InsertQuery(IndicatorsTable[] topics)
        {
            var colList = topics.Select(result => $"('{result.Id}', {result.TopicId}, '{Normalize(result.Name)}', {result.SourceId}, '{Normalize(result.Source)}', '{Normalize(result.SourceNote)}', '{Normalize(result.SourceOrgaisation)}')")
                                .ToList();

            return $"INSERT INTO {nameof(IndicatorsTable)} VALUES {(string.Join(",", colList))}";
        }

        public static string Normalize(string queryValue)
        {
            return queryValue?.Replace("'", "''");
        }
    }
}
