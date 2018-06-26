#region copyright
// Copyright (c) Microsoft.
// All Rights Reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.
#endregion
using CountryInformationDB;
using log4net;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace WebScrap.Common
{
    /// <summary>
    /// The class which helps in Website queries / commands like loading, querying xpath
    /// When using .NET 4.0 version of <see cref="System.Net"/> library, there was an issue of 
    /// downloading text file from an Url directly. Hence the Project was changed to use .NET 4.5.
    /// </summary>
    public class HttpRequestAndLoad
    {
        #region Fields

        /// <summary>
        /// Logger
        /// </summary>
        private ILog logger = LogManager.GetLogger(typeof(HttpRequestAndLoad));

        /// <summary>
        /// The configuration for the class
        /// </summary>
        private HttpRequestConfiguration configuration;

        /// <summary>
        /// The uri path sent for loading
        /// </summary>
        private Uri uriPath;

        #endregion Fields

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public HttpRequestAndLoad() : this(new HttpRequestConfiguration()) { }

        /// <summary>
        /// Constructor with configuration
        /// </summary>
        /// <param name="config"></param>
        public HttpRequestAndLoad(HttpRequestConfiguration config) 
            => configuration = config;

        #endregion Constructor

        #region Check

        /// <summary>
        /// Check if the url is online web page
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private bool IsOnlineWebPage(string url) => url.Contains("http:/") || url.Contains("https:/");

        #endregion Check

        #region Load

        /// <summary>
        /// Set the <see cref="WebRequest"/> settings for Http pages
        /// Set the user agent to mimic a web browser
        /// </summary>
        /// <param name="httpWebRequestObj"></param>
        private void HttpWebRequestSettings(WebRequest webRequestObj)
        {
            var httpWebRequestObj = (HttpWebRequest)webRequestObj;
            httpWebRequestObj.Method = configuration.Method;
            httpWebRequestObj.Proxy.Credentials = configuration.Credrntials;
            httpWebRequestObj.AuthenticationLevel = configuration.AuthLevel;
            httpWebRequestObj.ImpersonationLevel = configuration.ImpersonationLevel;
            httpWebRequestObj.KeepAlive = configuration.KeepAlive;
            httpWebRequestObj.UserAgent = configuration.UserAgent;
        }

        /// <summary>
        /// Common Load method
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="fManipulateWebStream"></param>
        /// <param name="xPath"></param>
        /// <returns></returns>
        private T Load<T>(string url, Func<WebResponse, T> fManipulateWebStream)
            where T : class
        {
            logger.DebugFormat("Try to Load The Url: \"{0}\"", url);
            uriPath = new Uri(url);

            ServicePointManager.SecurityProtocol = configuration.SecurityProtocol;
            ServicePointManager.ServerCertificateValidationCallback +=
                (sender, cert, chain, error) =>
                {
                    return true;
                };
            var webRequestObj = WebRequest.Create(url);

            if (IsOnlineWebPage(url)) HttpWebRequestSettings(webRequestObj);

            var webResponseObj = webRequestObj.GetResponse();

            if (webResponseObj == null)
            {
                logger.Error("No web response found for " + url);
                return null;
            }
            else if (IsOnlineWebPage(url))
            {
                logger.DebugFormat("This is a HTTP(S) url");

                HttpWebResponse httpResponse = (HttpWebResponse)webResponseObj;
                if (httpResponse.StatusCode != HttpStatusCode.OK)
                {
                    logger.ErrorFormat("Http web response for url {0} status code {1}",
                        url, httpResponse.StatusCode);
                    return null;
                }
            }

            return fManipulateWebStream(webResponseObj);
        }

        /// <summary>
        /// Load a html page from online or offline
        /// </summary>
        /// <param name="url"></param>
        /// <param name="xPath"></param>
        /// <returns></returns>
        private string ManipulateWebStreamForPage(WebResponse webResponseObj)
        {
            var stream = webResponseObj.GetResponseStream();
            var encoding = Encoding.UTF8;

            logger.DebugFormat("Current Encoding of the document is {0}", encoding);

            using (var reader = new StreamReader(stream, encoding))
                return reader.ReadToEnd();
        }

        /// <summary>
        /// Load a html page from online or offline
        /// </summary>
        /// <param name="url"></param>
        /// <param name="xPath"></param>
        /// <returns></returns>
        private StreamReader ManipulateWebStreamForPageAsStream(WebResponse webResponseObj)
        {
            var stream = webResponseObj.GetResponseStream();
            var encoding = Encoding.UTF8;

            logger.DebugFormat("Current Encoding of the document is {0}", encoding);

            return new StreamReader(stream, encoding);
        }

        /// <summary>
        /// Load a file from the online or offline
        /// </summary>
        /// <param name="url"></param>
        /// <param name="xPath"></param>
        /// <returns></returns>
        public string ManipulateWebStreamForFile(WebResponse webResponseObj)
        {
            var stream = webResponseObj.GetResponseStream();
            using (var reader = new StreamReader(stream))
                return reader.ReadToEnd();
        }

        /// <summary>
        /// Load a html page from online or offline
        /// </summary>
        /// <param name="url"></param>
        /// <param name="xPath"></param>
        /// <returns></returns>
        public string Load(string url) => Load(url, ManipulateWebStreamForPage);

        /// <summary>
        /// Load a file from the online or offline
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string LoadFile(string url) => Load(url, ManipulateWebStreamForPage);

        /// <summary>
        /// Load a file from the online or offline
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public StreamReader LoadAsStream(string url) => Load(url, ManipulateWebStreamForPageAsStream);

        #endregion Load
    }
}
