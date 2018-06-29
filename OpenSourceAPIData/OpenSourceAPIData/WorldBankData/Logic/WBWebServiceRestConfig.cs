using OpenSourceAPIData.Persistence.Logic;
using OpenSourceAPIData.WorldBankData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSourceAPIData.WorldBankData.Logic
{
    /// <summary>
    /// World bank rest api service configuration
    /// </summary>
    public class WBWebServiceRestConfig
    {
        /// <summary>
        /// The base uri of the web request url
        /// </summary>
        public string BaseApi { get; set; }

        /// <summary>
        /// The per page count
        /// </summary>
        public int PerPageCount { get; set; }

        /// <summary>
        /// The relative Uri path which represent the complete web request url
        /// </summary>
        public string RelativeUriPath { get; set; }

        /// <summary>
        /// The root node
        /// </summary>
        public string RootXPath { get; set; }

        /// <summary>
        /// The xpath for the data nodes in the response
        /// </summary>
        public string XPathDataNodes { get; set; }

        /// <summary>
        /// The persistence manager
        /// </summary>
        public PersistenceManager PersistenceManager { get; set; }

        /// <summary>
        /// The database model
        /// </summary>
        public WorldBankOrgOSDatabase Database { get; set; }

        public string UniqueName { get; set; }

        /// <summary>
        /// COnstructor
        /// </summary>
        public WBWebServiceRestConfig()
        {
            BaseApi = "https://api.worldbank.org/v2";
            PerPageCount = 50;
        }
    }
}
