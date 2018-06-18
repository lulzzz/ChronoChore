using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwaitDemo
{
    public class IndexFile
    {
        protected static ILog logger = LogManager.GetLogger(typeof(IndexFile));

        string indexFile = "index.txt";
        Dictionary<string, string> cachedFiles = new Dictionary<string, string>();

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
                    cachedFiles.Add(splits[0], splits[1]);
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
                    cachedFiles.Add(key, fileName);
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
    }
}
