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

        public void CreateTransportOperation(string location, string trashType)
        {
            List<RecyclingPoint> availablePoints = dbContext.RecyclingPoint
                .Where(p => p.RecyclingType == trashType)
                .OrderBy(p => p.Workload)
                .ToList();


            for(int i = 0; i < availablePoints.Count; i++)
            {

            }
        }

        public static async Task GetDistance()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string apiUrl = "https://api.mapbox.com/directions/v5/mapbox/driving/36.230521%2C49.984167%3B36.243065%2C49.992936?alternatives=false&annotations=distance%2Cduration&geometries=geojson&overview=full&steps=false&notifications=none&access_token=pk.eyJ1Ijoib2xla3NpeXkiLCJhIjoiY2x2ejQ3am9wMWJ0dTJrb2dqMmtoMzVzNSJ9.poCEnt5lJRlocbukSzazZQ";

                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();

                        var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseBody);
                        double distance = apiResponse.Routes[0].Distance;

                        Console.WriteLine(responseBody);
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
