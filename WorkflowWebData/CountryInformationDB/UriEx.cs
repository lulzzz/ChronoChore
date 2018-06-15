using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountryInformationDB
{
    public static class UriEx
    {
        public static string Full(string root, string url)
        {
            return new Uri(new Uri(root), url).AbsoluteUri;
        }
    }
}
