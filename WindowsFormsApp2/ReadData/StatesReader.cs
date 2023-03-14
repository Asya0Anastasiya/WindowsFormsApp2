using WindowsFormsApp2.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.AxHost;
using System.IO;

namespace WindowsFormsApp2.ReadData
{
    public class StatesReader
    {
        public static Dictionary<string, Newtonsoft.Json.Linq.JArray> ReadStates(string fileName)
        {
            string lines = File.ReadAllText(fileName);
            Dictionary<string, Newtonsoft.Json.Linq.JArray> states = JsonConvert.DeserializeObject<Dictionary<string, Newtonsoft.Json.Linq.JArray>>(lines);
            return states;
        }
        public List<State> states;
        public StatesReader()
        {
            states = new List<State>();
        }
    }
}
