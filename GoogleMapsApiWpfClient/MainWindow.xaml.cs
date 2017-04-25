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
using System.Windows.Media;

namespace GoogleMapsApiWpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IGoogleMapHost
    {
        private IGoogleMapWrapper _gMapsWrapper;
        private Dictionary<int, string> markers;
        private User currentUser;
        public MainWindow()
        {
            InitializeComponent();
            currentUser = null;
            markers = new Dictionary<int, string>();
            _gMapsWrapper = GoogleMapWrapper.Create(this, new MapOptions() { DraggingEnabled = true, MapType = MapTypeId.Satellite, Center = new GeographicLocation(0, 0), Zoom = 3 }, new StreetViewOptions() { });
        }

        private void loadSourceReports()
        {
            _gMapsWrapper.Clean();
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

        private List<WaterPurityReport> getPurityReportsList()
        {
            string jsonResponse = GET(@"http://localhost:8080/purity-reports");
            return JArray.Parse(jsonResponse).ToObject<List<WaterPurityReport>>();
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
                    currentUser = new User()
                    {
                        username = usernameTextbox.Text,
                        password = passwordTextbox.Password
                    };

                    streamWriter.Write(JsonConvert.SerializeObject(currentUser));
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



        int attemptRegister(string url) //201
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "application/json; charset=utf-8";
            request.Method = "POST";

            try
            {
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    currentUser = new User()
                    {
                        username = newUsernameTextbox.Text,
                        password = newPasswordTextbox.Password,
                        city = newCityTextbox.Text,
                        email = newEmailTextbox.Text,
                        title = newTitleTextbox.Text,
                        userType = newUserTypeCombobox.Text.ToUpper()

                    };

                    streamWriter.Write(JsonConvert.SerializeObject(currentUser));
                    streamWriter.Flush();
                }

                var response = request.GetResponse() as HttpWebResponse;
                return (int)response.StatusCode;
            }
            catch (WebException ex)
            {
                //currentUser = null;
                return (int)((HttpWebResponse)ex.Response).StatusCode;
            }
            catch (Exception ex)
            {
                //currentUser = null;
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return 400;
            }
        }

        int attemptCreatePurityReport()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($@"http://localhost:8080/{currentUser.username}/purity-reports");
            request.ContentType = "application/json; charset=utf-8";
            request.Method = "POST";

            try
            {

                var newPurityReport = new WaterPurityReport()
                {
                    contaminantPpm = Convert.ToDouble(createPurityContaminantPPMTextbox.Text),
                    latitude = Convert.ToDouble(createPurityLatitutdeTextbox.Text),
                    longitude = Convert.ToDouble(createPurityLongitudeTextbox.Text),
                    virusPpm = Convert.ToDouble(createPurityVirusPPMTextbox.Text),
                    waterPurityCondition = (WaterPurityCondition)Enum.Parse(typeof(WaterPurityCondition), createPurityConditionCombobox.Text.ToUpper()),
                    authorUsername = currentUser.username,
                    postDate = DateTime.Now
                };

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(JsonConvert.SerializeObject(newPurityReport));
                    streamWriter.Flush();
                }

                var response = request.GetResponse() as HttpWebResponse;
                return (int)response.StatusCode;
            }
            catch (WebException ex)
            {
                //currentUser = null;
                return (int)((HttpWebResponse)ex.Response).StatusCode;
            }
            catch (Exception ex)
            {
                //currentUser = null;
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return 400;
            }

        }

        int attemptCreateSourceReport()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($@"http://localhost:8080/{currentUser.username}/water-reports");
            request.ContentType = "application/json; charset=utf-8";
            request.Method = "POST";

            try
            {
                var newSourceReport = new WaterSourceReport()
                {
                    authorUsername = currentUser.username,
                    latitude = Convert.ToDouble(createSourceLatitutdeTextbox.Text),
                    longitude = Convert.ToDouble(createSourceLongitudeTextbox.Text),
                    postDate = DateTime.Now,
                    waterCondition = (WaterCondition)Enum.Parse(typeof(WaterCondition), createSourceWaterConditionCombobox.Text.ToUpper().Replace(' ', '_')),
                    waterType = (WaterType)Enum.Parse(typeof(WaterType), createSourceWaterTypeCombobox.Text.ToUpper())
                };

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(JsonConvert.SerializeObject(newSourceReport));
                    streamWriter.Flush();
                }

                var response = request.GetResponse() as HttpWebResponse;
                return (int)response.StatusCode;
            }
            catch (WebException ex)
            {
                //currentUser = null;
                return (int)((HttpWebResponse)ex.Response).StatusCode;
            }
            catch (Exception ex)
            {
                //currentUser = null;
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

                String type = currentUser.username.ToLower();
                if (type.Equals("manager")|| type.Equals("worker") || type.Equals("admin"))
                {
                    purityReportsView.Visibility = Visibility.Visible;
                    createPurityReportView.Visibility = Visibility.Visible;
                    if (!type.Equals("worker"))
                    {
                        viewHistoricalReport.Visibility = Visibility.Visible;
                    }
                }

                createSourceReportView.Visibility = Visibility.Visible;
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
                String type = currentUser.username.ToLower();
                if (type.Equals("manager") || type.Equals("worker") || type.Equals("admin"))
                {
                    purityReportsView.Visibility = Visibility.Visible;
                    createPurityReportView.Visibility = Visibility.Visible;
                    if (!type.Equals("worker"))
                    {
                        viewHistoricalReport.Visibility = Visibility.Visible;
                    }
                }
                mapView.Visibility = Visibility.Visible;
                tabMenu.SelectedItem = mapView;
                loginView.Visibility = Visibility.Collapsed;
                registerView.Visibility = Visibility.Collapsed;
                logoutView.Visibility = Visibility.Visible;
                
                sourceReportsView.Visibility = Visibility.Visible;
                createSourceReportView.Visibility = Visibility.Visible;

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

        private void viewHistoricalReportButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double lat = Double.Parse(viewHistoricalLatitudeTextbox.Text);
                double lon = Double.Parse(viewHistoricalLongitudeTextbox.Text);
                int year = Int32.Parse(viewHistoricalYearTextbox.Text);
                drawGraph(lat, lon, year);
                tabMenu.SelectedItem = viewHistoricalReportGraph;
                mapView.Visibility = Visibility.Collapsed;
                logoutView.Visibility = Visibility.Collapsed;
                purityReportsView.Visibility = Visibility.Collapsed;
                sourceReportsView.Visibility = Visibility.Collapsed;
                createSourceReportView.Visibility = Visibility.Collapsed;
                createPurityReportView.Visibility = Visibility.Collapsed;
                createPurityReportView.Visibility = Visibility.Collapsed;
                viewHistoricalReport.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex) { MessageBox.Show("Invalid fields!", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
        private void backFromGraphButton_Click(object sender, RoutedEventArgs e)
        {
            tabMenu.SelectedItem = viewHistoricalReport;
            mapView.Visibility = Visibility.Visible;
            logoutView.Visibility = Visibility.Visible;
            purityReportsView.Visibility = Visibility.Visible;
            sourceReportsView.Visibility = Visibility.Visible;
            createSourceReportView.Visibility = Visibility.Visible;
            createPurityReportView.Visibility = Visibility.Visible;
            createPurityReportView.Visibility = Visibility.Visible;
            viewHistoricalReport.Visibility = Visibility.Visible;
        }

        // logout
        private void Label_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            loginView.Visibility = Visibility.Visible;
            tabMenu.SelectedItem = loginView;
            mapView.Visibility = Visibility.Collapsed;
            registerView.Visibility = Visibility.Visible;
            logoutView.Visibility = Visibility.Collapsed;
            purityReportsView.Visibility = Visibility.Collapsed;
            sourceReportsView.Visibility = Visibility.Collapsed;
            createSourceReportView.Visibility = Visibility.Collapsed;
            createPurityReportView.Visibility = Visibility.Collapsed;
            viewHistoricalReport.Visibility = Visibility.Collapsed;
        }

        private void Label_MouseLeftButtonDown_1(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            getAllPurityReports();
        }

        private void Label_MouseLeftButtonDown_2(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            getAllSourceReports();
        }

        private void tabMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void createPurityReportButton_Click(object sender, RoutedEventArgs e)
        {
            int statusCode = attemptCreatePurityReport();
            MessageBox.Show($"status code for purity report create:{statusCode}");
        }

        private void createSourceReportButton_Click(object sender, RoutedEventArgs e)
        {
            int statusCode = attemptCreateSourceReport();
            MessageBox.Show($"status code for purity report create:{statusCode}");
        }

        private void Label_MouseLeftButtonDown_3(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            loadSourceReports();
        }
        private void drawGraph(double lat, double lon, int year)
        {
            List<WaterPurityReport> reports = getPurityReportsList();
            List<double>[] contam = new List<double>[13];
            List<double>[] virus = new List<double>[13];
            for(int i = 0; i < contam.Length; i++)
            {
                contam[i] = new List<double>();
                virus[i] = new List<double>();
            }
            
            foreach(WaterPurityReport pr in reports)
            {
                if(pr.postDate.Year == year && Math.Abs(pr.latitude-lat) + Math.Abs(pr.longitude - lon) < 0.00001)
                {
                    contam[pr.postDate.Month].Add(pr.contaminantPpm);
                    virus[pr.postDate.Month].Add(pr.virusPpm);
                }
            }

            double[,] aver = new double[13,2];
            double min = Double.MaxValue/2;
            double max = 0;
            for (int i = 0; i < contam.Length; i++)
            {
                if (contam[i].Count > 0)
                {
                    foreach (double d in contam[i])
                    {
                        aver[i,0] += d;
                    }
                    foreach (double d in virus[i])
                    {
                        aver[i,1] += d;
                    }
                    aver[i,0] /= contam[i].Count;
                    aver[i,0] /= contam[i].Count;
                    if (aver[i,0] < min)
                        min = aver[i,0];
                    if (aver[i,1] < min)
                        min = aver[i,1];
                    if (aver[i,0] > max)
                        max = aver[i,0];
                    if (aver[i,1] > max)
                        max = aver[i,1];
                }
                else
                {
                    aver[i,0] = -1;
                    aver[i,1] = -1;
                }
            }
            //hard coded default values
            /*aver = new double[13,2] { {1,5 } , {2,7 } , {3,6 } , {2,5 } , {1,5 } , {3,5 } , 
                {1,4 } , { 2,6} , {3,6 } , {4,5 } , {2,5 } , {1,5 } , {6,8 }};
            max = 8;
            min = 1;*/
            min--;
            max++;
            System.Windows.Shapes.Rectangle rect;
            rect = new System.Windows.Shapes.Rectangle();
            rect.Stroke = new SolidColorBrush(Colors.Black);
            rect.Fill = new SolidColorBrush(Colors.LightGray);
            System.Windows.Shapes.Rectangle contPoint;
            System.Windows.Shapes.Rectangle virPoint;
            rect.Width = 1066;
            rect.Height = 700;
            Canvas.Children.Add(rect);
            Canvas.SetLeft(rect, 57);
            Canvas.SetTop(rect, 0);
            int last = -1;
            int lastyc = 0;
            int lastyv = 0;
            for (int i = 0; i < contam.Length; i++)
            {
                if (contam[i].Count > 0)// || true)
                {
                    contPoint = new System.Windows.Shapes.Rectangle();
                    virPoint = new System.Windows.Shapes.Rectangle();
                    contPoint.Width = 5;
                    contPoint.Height = 5;
                    virPoint.Width = 5;
                    virPoint.Height = 5;
                    contPoint.Stroke = new SolidColorBrush(Colors.Blue);
                    contPoint.Fill = new SolidColorBrush(Colors.Blue);
                    virPoint.Stroke = new SolidColorBrush(Colors.Red);
                    virPoint.Fill = new SolidColorBrush(Colors.Red);
                    Canvas.Children.Add(contPoint);
                    Canvas.SetLeft(contPoint, 82 * (1 + i));
                    Canvas.SetTop(contPoint, (int)(700 - 700*aver[i,0] / (max-min)));
                    Canvas.Children.Add(virPoint);
                    Canvas.SetLeft(virPoint, 82 * (1+i));
                    Canvas.SetTop(virPoint, (int)(700 - 700*aver[i,1] / (max - min)));
                    if(last > -1)
                    {
                        System.Windows.Shapes.Line myLine = new System.Windows.Shapes.Line();

                        myLine.Stroke = System.Windows.Media.Brushes.Black;

                        myLine.X1 = last;
                        myLine.X2 = 82 * (1 + i);
                        myLine.Y1 = lastyc;
                        myLine.Y2 = (int)(700-700 * aver[i,0] / (max - min));

                        myLine.StrokeThickness = 1;

                        Canvas.Children.Add(myLine);
                        myLine = new System.Windows.Shapes.Line();
                        myLine.Stroke = System.Windows.Media.Brushes.Black;
                        myLine.X1 = last;
                        myLine.X2 = 82 * (1 + i);
                        myLine.Y1 = lastyv;
                        myLine.Y2 = (int)(700 - 700 * aver[i,1] / (max - min));

                        myLine.StrokeThickness = 1;

                        Canvas.Children.Add(myLine);
                    }
                    last = 82 * (1 + i);
                    lastyc = (int)(700 - 700*aver[i,0] / (max - min));
                    lastyv = (int)(700 - 700 * aver[i,1] / (max - min));
                }
            }
        }
    }

    public class Credentials
    {
        [JsonProperty("username")]
        public string username { get; set; }
        [JsonProperty("password")]
        public string password { get; set; }
    }

    public class User : Credentials
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

    public class WaterSourceReport : Report
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