using CORE.Models;
using DAL.Contexts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class TransportationService
    {
        private readonly SmartRecyclingDbContext dbContext;

        public TransportationService(SmartRecyclingDbContext smartRecyclingDbContext)
        {
            dbContext = smartRecyclingDbContext;
        }

        public async void CreateTransportOperation(string location, string trashType)
        {
            var distanceToPoints =
                dbContext.RecyclingPoint
                .Where(p => p.RecyclingType == trashType)
                .OrderBy(p => p.Workload)
                .ToDictionary(p => p,  async p => await GetDistance(location.Split(' '), p.Location.Split(' ')));
        }

        public async Task<double> GetDistance(string[] coordinate, string[] coordinate2)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string apiUrl = $"https://api.mapbox.com/directions/v5/mapbox/driving/{coordinate}%3B{coordinate2}?alternatives=false&annotations=distance%2Cduration&geometries=geojson&overview=full&steps=false&notifications=none&access_token=pk.eyJ1Ijoib2xla3NpeXkiLCJhIjoiY2x2ejQ3am9wMWJ0dTJrb2dqMmtoMzVzNSJ9.poCEnt5lJRlocbukSzazZQ";

                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();

                        var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseBody);
                        return apiResponse.Routes[0].Distance;
                    }
                    else
                    {
                        Console.WriteLine($"Failed to retrieve data. Status code: {response.StatusCode}");
                    }
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"An error occurred: {e.Message}");
                }
            }

            return 0.0;
        }

        public class ApiResponse
        {
            public Route[] Routes { get; set; }
        }

        public class Route
        {
            public double Distance { get; set; }
            // Add more properties as needed
        }
    }
}
