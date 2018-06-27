using OpenSourceAPIData.Persistence.Logic;
using OpenSourceAPIData.WorldBankData.Logic;
using OpenSourceAPIData.WorldBankData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Parser.Xml;
using WebScrap.LibExtension.XPath;

namespace OpenSourceAPIData
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var sqliteContext = new SqlitePersistContext("WorldBank"))
            {
                // Create the database
                var databaseModel = new WorldBankOrgOSDatabase();
                databaseModel.Create(sqliteContext);

                var wbTopics = new WBTopicsWebServiceRest();
                wbTopics.Read();
            }
        }
    }
}
