using WindowsFormsApp2.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WindowsFormsApp2.ReadData
{
    public class TweetsReader
    {
        public Dictionary<string, double> sentiments = SentimentsReader.ReadSentiments("sentiments.txt");
        public Dictionary<string, Newtonsoft.Json.Linq.JArray> points = StatesReader.ReadStates("states.json");
        public List<Tweet> HandlingTweets(string[] lines)
        {
            List<Tweet> Tweets = new List<Tweet>();
            foreach (string line in lines)
            {
                string[] data = line.Split('\t');
                string latitude = data[0].Split(',')[0].Remove(0, 1);
                string longitude = data[0].Split(',')[1];
                longitude = longitude.Remove(longitude.Length - 1, 1);
                Location location = new Location(float.Parse(latitude, CultureInfo.InvariantCulture.NumberFormat), float.Parse(longitude, CultureInfo.InvariantCulture.NumberFormat));
                Tweet tweet = new Tweet(location, DateTime.Parse(data[2]), data[3]);
                tweet.EvaluateTweet(sentiments);
                //tweet.DefineState(points);
                Tweets.Add(tweet);
            }

            return Tweets;
        }

        public string[] ReadTweetsFromFile(string fileName) => File.ReadAllLines(fileName);
    }
}
