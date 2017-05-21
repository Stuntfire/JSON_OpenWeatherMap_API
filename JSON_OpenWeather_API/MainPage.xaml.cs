using JSON_OpenWeatherMap_API.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace JSON_OpenWeatherMap_API
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            GetWeatherData();
        }

        async void GetWeatherData()
        {
            //URL til Lejre med City id=2617832
            string url = "http://api.openweathermap.org/data/2.5/weather?id=2617832&units=metric&appid=849b2c0ed687ebcd5de14111bcfada83";

            HttpClient client = new HttpClient();

            string response = await client.GetStringAsync(url);

            var data = JsonConvert.DeserializeObject<Rootobject>(response);

            //Temperatur Binding til TextBlock på MainPage.xaml
            result_weather_lable_01.Text = "Temperaturen i " + data.name.ToString() + " er " + data.main.temp.ToString() + " ºC";

            //Luftfugtighed Binding til TextBlock på MainPage.xaml
            result_weather_lable_02.Text = "og luftfugtigheder er " + data.main.humidity.ToString() + "%";

            //Link til OpenWeatherMap vejr-ikoner
            string icon = String.Format("http://openweathermap.org/img/w/{0}.png", data.weather[0].icon);

            //Binding til Image på MainPage.xaml
            ResultImage.Source = new BitmapImage(new Uri(icon, UriKind.Absolute));
        }
    }
}
