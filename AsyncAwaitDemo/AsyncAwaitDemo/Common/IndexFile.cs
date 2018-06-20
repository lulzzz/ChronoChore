using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace AsyncAwaitDemo
{
    public class IndexFile
    {
        protected static ILog logger = LogManager.GetLogger(typeof(IndexFile));

        string indexFile = "index.txt";
        ConcurrentDictionary<string, string> cachedFiles = new ConcurrentDictionary<string, string>();
        ConcurrentDictionary<string, bool> downloadCompletedFiles = new ConcurrentDictionary<string, bool>();

        public string LocalFolder { get; protected set; }

        public IndexFile(string local)
        {
            LocalFolder = local;

            if (!Directory.Exists(LocalFolder))
                Directory.CreateDirectory(LocalFolder);

            indexFile = Path.Combine(LocalFolder, indexFile);

            if (!File.Exists(indexFile)) File.Create(indexFile);
            else
            {
                ParseIndexFile();
            }
        }

        private void ParseIndexFile()
        {
            using (var reader = (TextReader)new StreamReader(indexFile))
            {
                string lineText = "";
                while ((lineText = reader.ReadLine()) != null)
                {
                    var splits = lineText.Split(',');
                    cachedFiles.TryAdd(splits[0], splits[1]);
                }
            }
        }

        public void Cache(string key, string extension)
        {
            if (!cachedFiles.ContainsKey(key))
            {
                string fileName = null;
                if (cachedFiles.ContainsKey(key))
                {
                    fileName = cachedFiles[key];
                    cachedFiles[key] = fileName;
                }
                else
                {
                    fileName = GenerateFileName(key);
                    cachedFiles.TryAdd(key, fileName);
                    downloadCompletedFiles.TryAdd(key, false);
                }
            }
        }

        private string GenerateFileName(string extension)
        {
            return Guid.NewGuid().ToString().Replace("-", "") + extension;
        }

        public bool Contains(string key)
        {
            return cachedFiles.ContainsKey(key);
        }

        public string Value(string key)
        {
            return cachedFiles[key];
        }

        public bool IsDownloaded(string key)
        {
            return downloadCompletedFiles.ContainsKey(key) && downloadCompletedFiles[key];
        }

        public void CompleteDownload(string key)
        {
            downloadCompletedFiles[key] = true;
        }
    }
}
