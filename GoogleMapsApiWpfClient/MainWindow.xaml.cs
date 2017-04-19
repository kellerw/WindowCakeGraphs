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
using System.Threading.Tasks;
using System.Windows.Controls;

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
            _gMapsWrapper = GoogleMapWrapper.Create(this, new MapOptions() { DraggingEnabled = true, MapType = MapTypeId.Satellite, Center = new GeographicLocation(0, 0), Zoom = 3 }, new StreetViewOptions() { });
        }

        private void loadSourceReports()
        {
            string jsonResponse = GET(@"http://localhost:8080/water-reports");
            var allSourceReports = JArray.Parse(jsonResponse).ToObject<List<WaterSourceReport>>();
            sourceReportsListView.ItemsSource = allSourceReports;
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

        private void getAllSourceReports()
        {
            string jsonResponse = GET(@"http://localhost:8080/water-reports");
            var allSourceReports = JArray.Parse(jsonResponse).ToObject<List<WaterSourceReport>>();
            sourceReportsListView.ItemsSource = allSourceReports;
        }

        private void getAllPurityReports()
        {
            string jsonResponse = GET(@"http://localhost:8080/purity-reports");
            var allPurityReports = JArray.Parse(jsonResponse).ToObject<List<WaterPurityReport>>();
            purityReportsListView.ItemsSource = allPurityReports;
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
                }
                throw;
            }
        }

        int attemptLogin(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "application/json; charset=utf-8";
            request.Method = "POST";

            try
            {
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(JsonConvert.SerializeObject(new Credentials()
                    {
                        username = usernameTextbox.Text,
                        password = passwordTextbox.Password
                    }));
                    streamWriter.Flush();
                }

                var response = request.GetResponse() as HttpWebResponse;
                return (int)response.StatusCode;
            }
            catch (WebException ex)
            {
                return (int)((HttpWebResponse)ex.Response).StatusCode;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return 400;
            }
        }

        int attemptRegister(string url) //201
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "application/json; charset=utf-8";
            request.Method = "POST";

            try
            {
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(JsonConvert.SerializeObject(new NewUser()
                    {
                        username = newUsernameTextbox.Text,
                        password = newPasswordTextbox.Password,
                        city = newCityTextbox.Text,
                        email = newEmailTextbox.Text,
                        title = newTitleTextbox.Text,
                        userType = newUserTypeCombobox.Text.ToUpper()

                    }));
                    streamWriter.Flush();
                }

                var response = request.GetResponse() as HttpWebResponse;
                return (int)response.StatusCode;
            }
            catch (WebException ex)
            {
                return (int)((HttpWebResponse)ex.Response).StatusCode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return 400;
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
            try
            {
                await Task.Delay(1500);
                this.loadSourceReports();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection timed out.", "Network Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void StartAsyncTimedWork()
        {
            Task ignoredAwaitableResult = this.delayedWork();
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            var statusCode = attemptLogin(@"http://localhost:8080/login");
            if (statusCode == 204)
            {
                mapView.Visibility = Visibility.Visible;
                tabMenu.SelectedItem = mapView;
                loginView.Visibility = Visibility.Collapsed;
                registerView.Visibility = Visibility.Collapsed;
                logoutView.Visibility = Visibility.Visible;

                purityReportsView.Visibility = Visibility.Visible;
                sourceReportsView.Visibility = Visibility.Visible;

                usernameTextbox.Clear();
                passwordTextbox.Clear();

            }
            else if (statusCode == 401)
            {
                MessageBox.Show("Wrong password!", "Authentication Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (statusCode == 404)
            {
                MessageBox.Show("Username not found!", "Authentication Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("Unknown error!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            if (newPasswordTextbox.Password != newPasswordRepeatTextbox.Password)
            {
                MessageBox.Show("Passwords do not match!", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var statusCode = attemptRegister(@"http://localhost:8080/accounts/");
            if (statusCode == 201)
            {
                mapView.Visibility = Visibility.Visible;
                tabMenu.SelectedItem = mapView;
                loginView.Visibility = Visibility.Collapsed;
                registerView.Visibility = Visibility.Collapsed;
                logoutView.Visibility = Visibility.Visible;

                purityReportsView.Visibility = Visibility.Visible;
                sourceReportsView.Visibility = Visibility.Visible;

                newUsernameTextbox.Clear();
                newPasswordTextbox.Clear();
                newPasswordRepeatTextbox.Clear();
                newEmailTextbox.Clear();
                newCityTextbox.Clear();
                newTitleTextbox.Clear();
                newUserTypeCombobox.SelectedIndex = -1;
                
            }
            else if (statusCode == 400)
            {
                MessageBox.Show("Bad request!", "Registration Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("Unknown error!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Label_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            loginView.Visibility = Visibility.Visible;
            tabMenu.SelectedItem = loginView;
            mapView.Visibility = Visibility.Collapsed;
            registerView.Visibility = Visibility.Visible;
            logoutView.Visibility = Visibility.Collapsed;
            purityReportsView.Visibility = Visibility.Collapsed;
            sourceReportsView.Visibility = Visibility.Collapsed;
        }

        private void Label_MouseLeftButtonDown_1(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            getAllPurityReports();
        }

        private void Label_MouseLeftButtonDown_2(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            getAllSourceReports();
        }
    }

    public class Credentials
    {
        [JsonProperty("username")]
        public string username { get; set; }
        [JsonProperty("password")]
        public string password { get; set; }
    }

    public class NewUser : Credentials
    {
        [JsonProperty("userType")]
        public string userType { get; set; }

        [JsonProperty("email")]
        public string email { get; set; }

        [JsonProperty("title")]
        public string title { get; set; }

        [JsonProperty("city")]
        public string city { get; set; }
    }
   
    public class Report
    {
        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("postDate")]
        public DateTime postDate { get; set; }

        [JsonProperty("latitude")]
        public double latitude { get; set; }

        [JsonProperty("longitude")]
        public double longitude { get; set; }

        [JsonProperty("authorUsername")]
        public string authorUsername { get; set; }
    }

    public class WaterSourceReport :Report
    {
        [JsonProperty("waterType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public WaterType waterType { get; set; }

        [JsonProperty("waterCondition")]
        [JsonConverter(typeof(StringEnumConverter))]
        public WaterCondition waterCondition { get; set; }
    }

    public class WaterPurityReport : Report
    {
        [JsonProperty("waterPurityCondition")]
        [JsonConverter(typeof(StringEnumConverter))]
        public WaterPurityCondition waterPurityCondition { get; set; }

        [JsonProperty("virusPpm")]
        public double virusPpm { get; set; }

        [JsonProperty("contaminantPpm")]
        public double contaminantPpm { get; set; }
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

    public enum WaterPurityCondition
    {
        [Description("Safe")]
        SAFE,
        [Description("Treatable")]
        TREATABLE,
        [Description("Unsafe")]
        UNSAFE
    };
    
}