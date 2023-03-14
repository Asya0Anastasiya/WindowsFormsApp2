using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2.ReadData
{
    public static class SentimentsReader
    {
        public static Dictionary<string, double> ReadSentiments(string fileName)
        {
            Dictionary<string, double> sentiments = new Dictionary<string, double>();
            string[] lines = File.ReadAllLines(fileName);
            foreach (string line in lines)
            {
                string[] data = line.Split(',');
                sentiments.Add(data[0], double.Parse(data[1], CultureInfo.InvariantCulture.NumberFormat));
            }
            return sentiments;
        }
    }
}
