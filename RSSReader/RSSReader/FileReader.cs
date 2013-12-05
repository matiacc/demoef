using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace RSSReader
{
    public static class FileReader
    {
        public static List<string> ObtenerRSS()
        {
            FileStream file = new FileStream("../../RSS.txt", FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader reader = new StreamReader(file);
            List<string> listaRSS = new List<string>();

            while (!reader.EndOfStream)
            {
                var rssFeed = reader.ReadLine();
                listaRSS.Add(rssFeed);
            }

            return listaRSS;
        }
    }
}
