using System;
using System.Net.Http;
using System.Threading.Tasks;
using Avalonia.Controls;
using Newtonsoft.Json.Linq;

namespace WeatherApp
{
    public partial class MainWindow : Window
    {
        private const string ApiKey = "60cdf380eb70636eb041a26c8930e84c";
        private const string City = "Tomsk";
        private const string ApiUrl = "https://api.openweathermap.org/data/2.5/weather";

        public MainWindow()
        {
            InitializeComponent();
            LoadWeatherAsync();
        }

        private async void LoadWeatherAsync()
        {
            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"{ApiUrl}?q={City}&appid={ApiKey}&units=metric&lang=ru");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var weatherData = JObject.Parse(json);
                var temperature = weatherData["main"]["temp"].ToString();
                var description = weatherData["weather"][0]["description"].ToString();
                WeatherText.Text = $"Температура: {temperature}°C\nОписание: {description}\nВыполнили: Поздняков А.С., Белодед Е.О.";
            }
            else
            {
                WeatherText.Text = "Failed to load weather data.";
            }
        }
    }
}
