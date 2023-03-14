using WindowsFormsApp2.ReadData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2.Model
{
    public class Tweet
    {
        public List<string> words;
        public DateTime dateTime;
        public Location location;
        public double? feelings;
        public string state;
        public Tweet(Location location, DateTime dateTime, string text)
        {
            this.location = location;
            this.dateTime = dateTime;
            words = CleanTweet(text.Split(' ').ToList());
        }

        public List<string> CleanTweet(List<string> text)
        {
            for (int i = 0; i < text.Count; i++)
            {
                if (text[i].StartsWith("@") || text[i].StartsWith("http") || text[i].StartsWith("#"))
                {
                    text.Remove(text[i]);
                }
                else RemovePunc(text[i]);
            }

            return text;
        }

        //public void EvaluateTweet()
        //{
        //    SentimentsReader sr = new();
        //    Dictionary<string, double> sentiments = sr.ReadSentiments(@"D:\\Projects\\C#\\MapLab\\MapLab\\bin\\Debug\\net6.0\\sentiments.txt");
        //    foreach (var sentiment in sentiments)
        //    {
        //        if (text.Contains(sentiment.Key))
        //        {
        //            feelings += sentiment.Value;
        //        }
        //    }
        //}

        //public void EvaluateTweet_2()
        //{
        //    SentimentsReader sr = new();
        //    Dictionary<string, double> sentiments = sr.ReadSentiments(@"D:\\Projects\\C#\\MapLab\\MapLab\\bin\\Debug\\net6.0\\sentiments.txt");
        //    foreach (var sentiment in sentiments)
        //    {
        //        string[] data = sentiment.Key.Split(' ');
        //        if (data.Count() > 1)
        //        {
        //            if (text.Contains(sentiment.Key)) 
        //            { 
        //                feelings += sentiment.Value; 
        //            }
        //        }
        //        else
        //        {
        //            data = text.Split(" ");
        //            foreach (string word in data)
        //            {
        //                if (word == sentiment.Key) 
        //                { 
        //                    feelings += sentiment.Value; 
        //                }
        //            }
        //        }
        //    }
        //}
        public void EvaluateTweet(Dictionary<string, double> sentiments)
        {
            int count = 0;
            double result = 0.0;
            for (int i = 0; i < words.Count; i++)
            {
                if (sentiments.ContainsKey(words[i].ToLower()))
                {
                    double value;
                    sentiments.TryGetValue(words[i], out value);
                    result += value;
                    //result += sentiments.GetValueOrDefault(words[i]);
                    count++;
                }
            }
            if (count > 0) feelings = result / count;
            else feelings = null;
        }

        public void RemovePunc(string word)
        {
            if (word.EndsWith(",") || word.EndsWith(".") || word.EndsWith("!"))
            {
                word.Remove(word.Length - 1);
            }
        }
        public void DefineState(Dictionary<string, Newtonsoft.Json.Linq.JArray> points)
        {
            for (int i = 0; i < points.Values.Count; i++)
            {
                for (int j = 0; j < points.Values.ElementAt(i).Count; j++)
                {
                    for (int q = 0; q < points.Values.ElementAt(i).ElementAt(j).Count(); q++)
                    {
                        for (int w = 0; w < points.Values.ElementAt(i).ElementAt(j).ElementAt(q).Count(); w++)
                        {
                            try
                            {
                                float number = (float)points.Values.ElementAt(i).ElementAt(j).ElementAt(q).ElementAt(w);
                            }
                            catch (ArgumentException)
                            {
                                for (int e = 0; e < points.Values.ElementAt(i).ElementAt(j).ElementAt(q).ElementAt(w).Count(); e++)
                                {
                                    if ((float)points.Values.ElementAt(i).ElementAt(j).ElementAt(q).ElementAt(w).First == location.latitude && (float)points.Values.ElementAt(i).ElementAt(j).ElementAt(q).ElementAt(w).Last == location.longitude)
                                    {
                                        state = points.ElementAt(i).Key;
                                        Console.WriteLine("Hello");
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public override string ToString()
        {
            return $"{feelings}";
        }
    }
}