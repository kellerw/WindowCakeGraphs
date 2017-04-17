using System.Windows;
using GoogleMapsApi;
using Newtonsoft.Json;
using System;
using Newtonsoft.Json.Converters;
using System.Reflection;
using System.Net;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GoogleMapsApiWpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IGoogleMapHost
    {
        private IGoogleMapWrapper _gMapsWrapper;
        private Dictionary<int, string> markers;

        public MainWindow()
        {
            InitializeComponent();
            markers = new Dictionary<int, string>();
            _gMapsWrapper = GoogleMapWrapper.Create(this, new MapOptions() { DraggingEnabled = true, MapType = MapTypeId.Satellite, Center = new GeographicLocation(0, 0), Zoom = 3 }, new StreetViewOptions() {  });
        }

        


        private void loadSourceReports()
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
                //marker.DoubleClick += new Action<IMarker, GeographicLocation>(marker_DoubleClick);
                //marker.DragEnd += new Action<IMarker, GeographicLocation>(marker_DragEnd);

                markers.Add(marker.MarkerId, messageToShow);
            }

        }


        private void my_marker_Click(IMarker arg1, GeographicLocation arg2)
        {
            var infoWindow = _gMapsWrapper.ShowInfoWindow(markers[arg1.MarkerId], arg1,
                new InfoWindowOptions() { MaxWidth = 150, DisableAutoPan = false }, true);
            //infoWindow.CloseClick += new Action<IInfoWindow>(infoWindow_CloseClick);

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

        public void RegisterScriptingObject(IGoogleMapRequired wrapper)
        {
            Browser.ObjectForScripting = wrapper;
        }

        public void SetHostDocumentText(string text)
        {
             Browser.NavigateToString(text);
        }

        public object InvokeScript(string methodName, params object[] parameters)
        {
            return Browser.InvokeScript(methodName, parameters);
        }

        public bool HandleException(string message, string url, string line)
        {
            return true;
        }

        static string GetDescription(Enum en)
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


        private void Browser_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            StartAsyncTimedWork();
        }

        private async Task delayedWork()
        {
            await Task.Delay(1500);
            this.loadSourceReports();
        }

        //This could be a button click event handler or the like */
        private void StartAsyncTimedWork()
        {
            Task ignoredAwaitableResult = this.delayedWork();
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