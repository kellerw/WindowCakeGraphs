﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GoogleMapsApi;
using System.Net;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using System.Reflection;

namespace GoogleMapsApiTester
{
    public partial class Form1 : Form, IGoogleMapHost
    {
        private IGoogleMapWrapper _gMapsWrapper;

        public Form1()
        {
            InitializeComponent();
            pgPolygon.SelectedObject = new PolygonOptions();
            pgMarker.SelectedObject = new MarkerOptions();
            pgMapOptions.SelectedObject = new MapOptions();
            pgStreetViewOptions.SelectedObject = new StreetViewOptions();

            InitializeMap();
        }

        public void SetHostDocumentText(string text)
        {
            browser.DocumentText = text;
        }

        public void RegisterScriptingObject(IGoogleMapRequired wrapper)
        {
            browser.ObjectForScripting = wrapper;
        }

        public object InvokeScript(string methodName, params object[] parameters)
        {
            return browser.Document.InvokeScript(methodName, parameters);
        }

        public bool HandleException(string message, string url, string line)
        {
            return true;
        }

        private void PolygonTest_Click(object sender, EventArgs e)
        {
            List<GeographicLocation> locations = new List<GeographicLocation>();
            locations.Add(new GeographicLocation(-35.0418205762627, 16.3491015625));
            locations.Add(new GeographicLocation(-21.9971918060098, 16.3491015625));
            locations.Add(new GeographicLocation(-21.9971918060098, 33.0483203125));
            locations.Add(new GeographicLocation(-35.0418205762627, 33.0483203125));

            var polygon = _gMapsWrapper.DrawPolygon(locations, (PolygonOptions)pgPolygon.SelectedObject, true);
            polygon.Click += new Action<IPolygon, GeographicLocation>(polygon_Click);
            polygon.DoubleClick += new Action<IPolygon, GeographicLocation>(polygon_DoubleClick);
            polygon.RightClick += new Action<IPolygon, GeographicLocation>(polygon_RightClick);
        }

        private void GeocodingTest1_Click(object sender, EventArgs e)
        {
            var response = _gMapsWrapper.RunGeocoder("Newlands, Cape Town");
            if (response.Status == ServiceResponseStatus.Ok)
            {
                if (response.Results.Count > 0)
                {
                    var bounds = response.Results[0].Geometry.ViewPort;
                    _gMapsWrapper.FitToBounds(bounds);
                }
            }
        }

        private void GeocodingTest2_Click(object sender, EventArgs e)
        {
            var response = _gMapsWrapper.RunGeocoder("Dean Street, Newlands, Cape Town");
            if (response.Status == ServiceResponseStatus.Ok)
            {
                if (response.Results.Count > 0)
                {
                    var bounds = response.Results[0].Geometry.ViewPort;
                    _gMapsWrapper.FitToBounds(bounds);
                }
            }
        }

        private void GeocodingTest3_Click(object sender, EventArgs e)
        {
            var response = _gMapsWrapper.RunGeocoder(txtGeocoding.Text);
            if (response.Status == ServiceResponseStatus.Ok)
            {
                if (response.Results.Count > 0)
                {
                    var bounds = response.Results[0].Geometry.ViewPort;
                    _gMapsWrapper.FitToBounds(bounds);
                }
            }
        }

        private void GeocodingTest4_Click(object sender, EventArgs e)
        {
            GeographicBounds limit = new GeographicBounds()
            {
                SouthWest = new GeographicLocation(-35.0418205762627, 16.3491015625),
                NorthEast = new GeographicLocation(-21.9971918060098, 33.0483203125)
            };
            var response = _gMapsWrapper.RunGeocoder(txtGeocoding.Text, "za", limit);
            if (response.Status == ServiceResponseStatus.Ok)
            {
                if (response.Results.Count > 0)
                {
                    var bounds = response.Results[0].Geometry.ViewPort;
                    _gMapsWrapper.FitToBounds(bounds);
                }
            }
        }

        private void MarkerTest1_Click(object sender, EventArgs e)
        {
            var marker = _gMapsWrapper.AddMarker(_gMapsWrapper.Center, (MarkerOptions)pgMarker.SelectedObject, false);
            marker.Click += new Action<IMarker, GeographicLocation>(marker_Click);
            marker.DoubleClick += new Action<IMarker, GeographicLocation>(marker_DoubleClick);
            marker.DragEnd += new Action<IMarker, GeographicLocation>(marker_DragEnd);
        }

        private void MarkerTest2_Click(object sender, EventArgs e)
        {
            MarkerOptions option = new MarkerOptions()
            {
                Icon = Application.StartupPath + "\\about.png",
                DraggingEnabled = true,
            };
            var marker = _gMapsWrapper.AddMarker(_gMapsWrapper.Center, option, false);
            _gMapsWrapper.ShowInfoWindow("Marker Info", marker, new InfoWindowOptions() { MaxWidth = 100 }, false);
        }

        private void MapTest_Click(object sender, EventArgs e)
        {
            InitializeMap();
        }

        private void btnStreetView_Click(object sender, EventArgs e)
        {
            _gMapsWrapper.StreetView.Position = new GeographicLocation(-33.9713736, 18.4648659);
            _gMapsWrapper.StreetView.ShowInfoWindow("Hello world", new GeographicLocation(-33.9712969, 18.4649886), new InfoWindowOptions() { MaxWidth = 100 }, true);
            _gMapsWrapper.StreetView.Visible = true;
        }

        private void InitializeMap()
        {
            if (_gMapsWrapper != null)
            {
                _gMapsWrapper.MapClick -= _gMapsWrapper_MapClick;
                _gMapsWrapper.ZoomChanged -= _gMapsWrapper_ZoomChanged;
                _gMapsWrapper.CenterChanged -= _gMapsWrapper_CenterChanged;
                _gMapsWrapper.MapDoubleClick -= _gMapsWrapper_MapDoubleClick;
                _gMapsWrapper.MapRightClick -= _gMapsWrapper_MapRightClick;
                _gMapsWrapper.BoundsChanged -= _gMapsWrapper_BoundsChanged;
            }

            _gMapsWrapper = GoogleMapWrapper.Create(this, (MapOptions)pgMapOptions.SelectedObject, (StreetViewOptions)pgStreetViewOptions.SelectedObject);
            _gMapsWrapper.MapClick += _gMapsWrapper_MapClick;
            _gMapsWrapper.ZoomChanged += _gMapsWrapper_ZoomChanged;
            _gMapsWrapper.CenterChanged += _gMapsWrapper_CenterChanged;
            _gMapsWrapper.MapDoubleClick += _gMapsWrapper_MapDoubleClick;
            _gMapsWrapper.MapRightClick += _gMapsWrapper_MapRightClick;
            _gMapsWrapper.BoundsChanged += _gMapsWrapper_BoundsChanged;

            lblZoom.Text = _gMapsWrapper.Zoom.ToString();
            lblCenter.Text = _gMapsWrapper.Center.ToString();
        }

        #region Events

        private void _gMapsWrapper_MapClick(GeographicLocation obj)
        {
            if (cbShowInfoWindow.Checked)
            {
                var contentString = "<div id=\"content\">" +
                           "<h1 id=\"firstHeading\" class=\"firstHeading\">Sample Info</h1>" +
                           "<div id=\"bodyContent\">" +
                           "<p>This is some <b>sample</b> text</p>" +

                           "<p><img src=\"http://www.quackit.com/pix/milford_sound/milford_sound.JPG\" width=\"100\" height=\"50\" border=\"2\" style=\"border:2px solid black;\" alt=\"Photo of Milford Sound in New Zealand!\" /></p>" +

                           "<p><a href=\"http://www.google.co.za\">" +
                           "Google it.</p>" +
                           "</div>" +
                           "</div>";

                var infoWindow = _gMapsWrapper.ShowInfoWindow(contentString, obj, new InfoWindowOptions() { MaxWidth = 200 }, true);
                infoWindow.CloseClick += new Action<IInfoWindow>(infoWindow_CloseClick);
            }
            lblLastEvent.Text = "Map_Click";
        }

        private void _gMapsWrapper_CenterChanged(GeographicLocation obj)
        {
            GeographicBounds limit = new GeographicBounds()
            {
                SouthWest = new GeographicLocation(-35.0418205762627, 16.3491015625),
                NorthEast = new GeographicLocation(-21.9971918060098, 33.0483203125)
            };

            if (cbLimitMap.Checked)
            {
                bool changed = false;
                if (obj.Latitude < limit.SouthWest.Latitude - 0.1)
                {
                    changed = true;
                    obj.Latitude = limit.SouthWest.Latitude;
                }
                else if (obj.Latitude > limit.NorthEast.Latitude + 0.1)
                {
                    changed = true;
                    obj.Latitude = limit.NorthEast.Latitude;
                }

                if (obj.Longitude < limit.SouthWest.Longitude - 0.1)
                {
                    changed = true;
                    obj.Longitude = limit.SouthWest.Longitude;
                }
                else if (obj.Longitude > limit.NorthEast.Longitude + 0.1)
                {
                    changed = true;
                    obj.Longitude = limit.NorthEast.Longitude;
                }

                if (changed)
                {
                    _gMapsWrapper.Center = obj;
                }
            }

            lblCenter.Text = obj.ToString();
            lblLastEvent.Text = "Center_Changed";
        }

        private void _gMapsWrapper_ZoomChanged(int obj)
        {
            lblZoom.Text = obj.ToString();
            lblLastEvent.Text = "Zoom_Changed";
        }

        private void _gMapsWrapper_MapDoubleClick(GeographicLocation obj)
        {
            lblLastEvent.Text = "Map_DoubleClick";
        }

        private void _gMapsWrapper_MapRightClick(GeographicLocation obj)
        {
            lblLastEvent.Text = "Map_RightClick";
        }

        private void marker_DragEnd(IMarker arg1, GeographicLocation arg2)
        {
            lblLastEvent.Text = "Marker_DragEnd";
        }

        private void marker_DoubleClick(IMarker arg1, GeographicLocation arg2)
        {
            lblLastEvent.Text = "Marker_DoubleClick";
        }

        private void marker_Click(IMarker arg1, GeographicLocation arg2)
        {
            lblLastEvent.Text = "Marker_Click";
            MessageBox.Show($"Clicked water report #{markers[arg1.MarkerId]}");
        }

        private void polygon_RightClick(IPolygon arg1, GeographicLocation arg2)
        {
            lblLastEvent.Text = "Polygon_RightClick";
        }

        private void polygon_DoubleClick(IPolygon arg1, GeographicLocation arg2)
        {
            lblLastEvent.Text = "Polygon_DoubleClick";
        }

        private void polygon_Click(IPolygon arg1, GeographicLocation arg2)
        {
            lblLastEvent.Text = "Polygon_Click";
        }

        private void infoWindow_CloseClick(IInfoWindow obj)
        {
            lblLastEvent.Text = "InfoWindow_CloseClick";
        }

        private void _gMapsWrapper_BoundsChanged(GeographicBounds obj)
        {
            lblLastEvent.Text = "Bounds_Changed";
        }

        #endregion Events

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }


        private static int reportNumner = 1;
        Dictionary<int, string> markers = new Dictionary<int, string>();
        private void button1_Click(object sender, EventArgs e)
        {
            double lat = Convert.ToDouble(waterReportLat.Text);
            double lng = Convert.ToDouble(waterReportLng.Text);
            var location = new GeographicLocation(lat, lng);

            var messageToShow = $"Water Report #{reportNumner++}";

            MarkerOptions option = new MarkerOptions()
            {
                Animation = MarkerAnimation.Bounce,
                Clickable = true,
                Icon = Application.StartupPath + "\\about.png",
                DraggingEnabled = false,
            };

            var marker = _gMapsWrapper.AddMarker(location, option, false);
            marker.Click += new Action<IMarker, GeographicLocation>(marker_Click);
            marker.DoubleClick += new Action<IMarker, GeographicLocation>(marker_DoubleClick);
            marker.DragEnd += new Action<IMarker, GeographicLocation>(marker_DragEnd);
            markers.Add(marker.MarkerId, $"{reportNumner - 1}");
            var infoWindow = _gMapsWrapper.ShowInfoWindow(messageToShow, marker, new InfoWindowOptions() { MaxWidth = 100 }, true);
            infoWindow.CloseClick += new Action<IInfoWindow>(infoWindow_CloseClick);

            //CircleOptions radiusOptions = new CircleOptions() {
            //    FillColor = System.Drawing.Color.Aqua,
            //    FillOpacity = 0.35,
            //    StrokeWeight = 2,
            //    StrokeColor=System.Drawing.Color.Aqua,
            //    StrokeOpacity=0.8,
            //    Clickable=false,
            //    Editable=false
            //};

            //_gMapsWrapper.DrawCircle(location, 16000, radiusOptions, false);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            getAllWaterSourceReports();
        }

        private void getAllWaterSourceReports()
        {
            string jsonResponse = GET(@"http://localhost:8080/water-reports");
            var allSourceReports = JArray.Parse(jsonResponse).ToObject<List<WaterSourceReport>>();
            foreach (var report in allSourceReports)
            {
                var location = new GeographicLocation(report.latitude, report.longitude);
                var messageToShow = $@"Water Report #{report.id} <br /> {GetDescription(report.waterCondition)} {GetDescription(report.waterType)}";

                MarkerOptions option = new MarkerOptions()
                {
                    Animation = MarkerAnimation.Drop,
                    Clickable = true,
                    //Icon = Application.StartupPath + "\\about.png",
                    DraggingEnabled = false,
                };

                var marker = _gMapsWrapper.AddMarker(location, option, false);
                marker.Click += new Action<IMarker, GeographicLocation>(my_marker_Click);
                marker.DoubleClick += new Action<IMarker, GeographicLocation>(marker_DoubleClick);
                marker.DragEnd += new Action<IMarker, GeographicLocation>(marker_DragEnd);

                markers.Add(marker.MarkerId, messageToShow);
            }
        }

        private void my_marker_Click(IMarker arg1, GeographicLocation arg2)
        {
            lblLastEvent.Text = "Marker_Click";
            var infoWindow = _gMapsWrapper.ShowInfoWindow(markers[arg1.MarkerId], arg1, 
                new InfoWindowOptions() { MaxWidth = 150, DisableAutoPan = false }, true);
            infoWindow.CloseClick += new Action<IInfoWindow>(infoWindow_CloseClick);

        }

        string GET(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    return reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                WebResponse errorResponse = ex.Response;
                using (Stream responseStream = errorResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                    String errorText = reader.ReadToEnd();
                    // log errorText
                }
                throw;
            }
        }

        static string GetDescription( Enum en)
        {
            Type type = en.GetType();
            MemberInfo[] memInfo = type.GetMember(en.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(Description), false);
                if (attrs != null && attrs.Length > 0)
                    return ((Description)attrs[0]).Text;
            }
            return en.ToString();
        }

    }

    public class WaterSourceReport
    {
        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("postDate")]
        public DateTime postDate { get; set; }

        [JsonProperty("latitude")]
        public double latitude { get; set; }

        [JsonProperty("longitude")]
        public double longitude { get; set; }

        [JsonProperty("waterType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public WaterType waterType { get; set; }

        [JsonProperty("waterCondition")]
        [JsonConverter(typeof(StringEnumConverter))]
        public WaterCondition waterCondition { get; set; }

        [JsonProperty("authorUsername")]
        public string authorUsername { get; set; }
    }

    class Description : Attribute
    {
        public string Text;
        public Description(string text)
        {
            Text = text;
        }
    }

    public enum WaterType
    {
        [Description("Bottled")]
        BOTTLED,
        [Description("Well")]
        WELL,
        [Description("Stream")]
        STREAM,
        [Description("Lake")]
        LAKE,
        [Description("Spring")]
        SPRING,
        [Description("Other")]
        OTHER
    };

    public enum WaterCondition
    {
        [Description("Potable")]
        POTABLE,
        [Description("Treatable Clear")]
        TREATABLE_CLEAR,
        [Description("Treatable Muddy")]
        TREATABLE_MUDDY,
        [Description("Waste")]
        WASTE
    };


   
}