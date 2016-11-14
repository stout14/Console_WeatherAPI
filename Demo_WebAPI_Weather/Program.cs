using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;

namespace Demo_WebAPI_Weather
{
    class Program
    {
        static void Main(string[] args)
        {
            DisplayOpeningScreen();
            DisplayMenu();
            DisplayClosingScreen();
        }


        static void DisplayMenu()
        {
            bool quit = false;
            LocationCoordinates coordinates = new LocationCoordinates(0, 0);

            while (!quit)
            {
                DisplayHeader("Main Menu");

                Console.WriteLine("Enter the number of your menu choice.");
                Console.WriteLine();
                Console.WriteLine("1) Set the Location");
                Console.WriteLine("2) Display the Current Weather");
                Console.WriteLine("3) Exit");
                Console.WriteLine();
                Console.Write("Enter Choice:");
                string userMenuChoice = Console.ReadLine();

                switch (userMenuChoice)
                {
                    case "1":
                        coordinates = DisplayGetLocation();
                        break;

                    case "2":
                        DisplayCurrentWeather(coordinates);
                        break;

                    case "3":
                        quit = true;
                        break;

                    default:
                        Console.WriteLine("You must enter a number from the menu.");
                        break;
                }
            }
        }


        static void DisplayOpeningScreen()
        {
            //
            // display an opening screen
            //
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Weather Reporter");
            Console.WriteLine();
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        static void DisplayClosingScreen()
        {
            //
            // display an closing screen
            //
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Thank you for using my application!");
            Console.WriteLine();
            Console.WriteLine();

            //
            // display continue prompt
            //
            Console.WriteLine();
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
            Console.WriteLine();
        }

        static void DisplayContinuePrompt()
        {
            //
            // display continue prompt
            //
            Console.WriteLine();
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
            Console.WriteLine();
        }

        static void DisplayHeader(string headerText)
        {
            //
            // display header
            //
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine(headerText);
            Console.WriteLine();
        }

        static LocationCoordinates DisplayGetLocation()
        {
            DisplayHeader("Set Location by Coordinates");

            LocationCoordinates coordinates = new LocationCoordinates(45, 85);

            DisplayContinuePrompt();

            return coordinates;
        }


        static async Task<WeatherData> GetCurrentWeatherData(LocationCoordinates coordinates)
        {
            string url;
            WeatherData currentWeather = new WeatherData();

            url = "http://api.openweathermap.org/data/2.5/weather?lat=45&lon=85&appid=864d252afc928abff4010abe732617a1";

            Task<WeatherData> getCurrentWeather = HttpGetCurrentWeatherByLocation(url);
 
            currentWeather = await getCurrentWeather;

            return currentWeather;
        }

        static async Task<WeatherData> HttpGetCurrentWeatherByLocation(string url)
        {
            string result = null;

            using (HttpClient syncClient = new HttpClient())
            {
                var response = await syncClient.GetAsync(url);
                result = await response.Content.ReadAsStringAsync();
            }

            Console.WriteLine(result);

            WeatherData currentWeather = JsonConvert.DeserializeObject<WeatherData>(result);

            return currentWeather;
        }

        static async void DisplayCurrentWeather(LocationCoordinates coordinates)
        {
            DisplayHeader("Current Weather");

            WeatherData currentWeatherData = await GetCurrentWeatherData(coordinates);
            
            Console.WriteLine(currentWeatherData.main.temp);

            DisplayContinuePrompt();
        }
    }
}
