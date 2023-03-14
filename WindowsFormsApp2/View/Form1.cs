using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp2.Model;
using WindowsFormsApp2.ReadData;
using static GMap.NET.MapProviders.StrucRoads.SnappedPoint;
using static System.Windows.Forms.AxHost;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void gMapControl1_Load(object sender, EventArgs e)
        {
            GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;
            gMapControl1.SetPositionByKeywords("Usa");
            gMapControl1.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
            gMapControl1.MaxZoom = 16;
            gMapControl1.Zoom = 4;
            
            //gMapControl1.Position = new GMap.NET.PointLatLng(77, 38);
            gMapControl1.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter; 
            gMapControl1.CanDragMap = true; 
            gMapControl1.DragButton = MouseButtons.Left; 
            gMapControl1.ShowCenter = false; 
            gMapControl1.ShowTileGridLines = false;

         
            TweetsReader tweetsReader = new TweetsReader();
            List<Tweet> tweets = tweetsReader.HandlingTweets(tweetsReader.ReadTweetsFromFile("weekend_tweets2014.txt"));
            Dictionary<string, Newtonsoft.Json.Linq.JArray> states = StatesReader.ReadStates("states.json");

            GMapOverlay polygons = new GMapOverlay("polygons");
            List<PointLatLng> points = new List<PointLatLng>();

            for (int i = 0; i < states.Values.Count; i++)
            {
                for (int j = 0; j < states.Values.ElementAt(i).Count; j++)
                {
                    for (int q = 0; q < states.Values.ElementAt(i).ElementAt(j).Count(); q++)
                    {
                        for (int w = 0; w < states.Values.ElementAt(i).ElementAt(j).ElementAt(q).Count(); w++)
                        {
                            try
                            {
                                points.Add(new PointLatLng((float)states.Values.ElementAt(i).ElementAt(j).ElementAt(q).ElementAt(w).Last, (float)states.Values.ElementAt(i).ElementAt(j).ElementAt(q).ElementAt(w).First));
                            }
                            catch (ArgumentException)
                            {
                                for (int a = 0; a < states.Values.ElementAt(i).ElementAt(j).ElementAt(q).ElementAt(w).Count(); a++)
                                {
                                    points.Add(new PointLatLng((float)states.Values.ElementAt(i).ElementAt(j).ElementAt(q).ElementAt(w).ElementAt(a).Last, (float)states.Values.ElementAt(i).ElementAt(j).ElementAt(q).ElementAt(w).ElementAt(a).First));

                                }
                                
                            }
                           
                            if (w == 60)
                            {
                                int y = 0;
                            }
                            //GMapPolygon polygon = new GMapPolygon(points, states.ElementAt(i).Key);
                            //polygons.Polygons.Add(polygon);
                            //gMapControl1.Overlays.Add(polygons);
                        }
                        GMapPolygon polygon = new GMapPolygon(points, states.ElementAt(i).Key);
                        polygons.Polygons.Add(polygon);
                        gMapControl1.Overlays.Add(polygons);
                        points.Clear();
                        //break;
                    }
                    //break;
                }
                break;
            }
            //GMapOverlay markers = new GMapOverlay("markers");

            //for (int i = 0; i < tweets.Count; i++)
            //{
            //    PointLatLng point = new PointLatLng(tweets[i].location.latitude, tweets[i].location.longitude);
            //    GMapMarker marker = new GMarkerGoogle(point, GMarkerGoogleType.green);
            //    markers.Markers.Add(marker);
            //}

            //gMapControl1.Overlays.Add(markers);


            #region Draw_Polygon
            //GMapOverlay polygons = new GMapOverlay("polygons");
            //List<PointLatLng> points = new List<PointLatLng>();
            //points.Add(new PointLatLng(48.866383, 2.323575));
            //points.Add(new PointLatLng(48.863868, 2.321554));
            //points.Add(new PointLatLng(48.861017, 2.330030));
            //points.Add(new PointLatLng(48.863727, 2.331918));
            //GMapPolygon polygon = new GMapPolygon(points, "Jardin des Tuileries");
            //polygons.Polygons.Add(polygon);
            //gMapControl1.Overlays.Add(polygons);
            #endregion
        }

        #region Draw_Marker
        //private void button1_Click(object sender, EventArgs e)
        //{
        //    gMapControl1.ShowCenter = false;

        //    gMapControl1.DragButton = MouseButtons.Left;
        //    gMapControl1.MapProvider = GMapProviders.GoogleMap;

        //    double lat = Convert.ToDouble(textBox1.Text);
        //    double lon = Convert.ToDouble(textBox2.Text);
        //    gMapControl1.Position = new PointLatLng(lat, lon);

        //    gMapControl1.MinZoom = 5;
        //    gMapControl1.MaxZoom = 100;
        //    gMapControl1.Zoom = 5;

        //    PointLatLng point = new PointLatLng(lat, lon);
        //    GMapMarker marker = new GMarkerGoogle(point, GMarkerGoogleType.green);

        //    GMapOverlay markers = new GMapOverlay("markers");
        //    markers.Markers.Add(marker);
        //    gMapControl1.Overlays.Add(markers);

        //}
        #endregion
    }
}
