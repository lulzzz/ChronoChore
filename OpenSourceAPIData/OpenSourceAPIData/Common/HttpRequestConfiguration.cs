﻿using System.Net;
using System.Net.Security;
using System.Security.Principal;

namespace CountryInformationDB
{
    public class HttpRequestConfiguration
    {
        #region Properties

        /// <summary>
        /// Gets or sets the Request method
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// Get or set the credential for accessing the website
        /// </summary>
        public ICredentials Credrntials { get; set; }

        /// <summary>
        /// Get or set the authentication level
        /// </summary>
        public AuthenticationLevel AuthLevel { get; set; }

        /// <summary>
        /// Get or set the impersonation level
        /// </summary>
        public TokenImpersonationLevel ImpersonationLevel { get; set; }

        /// <summary>
        /// Get or set a value that indicates whether to make a persistent 
        /// connection to the Internet resource.
        /// </summary>
        public bool KeepAlive { get; set; }

        /// <summary>
        /// Get or set the value for user agent http headers
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        /// Accept headers
        /// </summary>
        public string Accept { get; set; }

        /// <summary>
        /// Timeout in milliseconds
        /// </summary>
        public int Timeout { get; set; }

        /// <summary>
        /// Get or set the security protocol used by serivce point object
        /// </summary>
        public SecurityProtocolType SecurityProtocol { get; set; }

        /// <summary>
        /// retry delay in seconds
        /// </summary>
        public int RetryDelay { get; set; }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public HttpRequestConfiguration()
        {
            Method = "GET";
            Credrntials = CredentialCache.DefaultNetworkCredentials;
            AuthLevel = AuthenticationLevel.None;
            ImpersonationLevel = TokenImpersonationLevel.None;
            KeepAlive = true;
            UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/64.0.3282.119 Safari/5";
            SecurityProtocol = SecurityProtocolType.Ssl3 |
                SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            Accept = "*/*";
            Timeout = 1000000;
        }

        #endregion Constructor
    }
}
