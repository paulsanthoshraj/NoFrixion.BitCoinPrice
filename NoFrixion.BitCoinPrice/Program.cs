using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace NoFrixion.BitCoinPrice
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();
       
        static async Task Main(string[] args)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://api.coindesk.com/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //GET Method
                HttpResponseMessage response = await client.GetAsync("v1/bpi/currentprice.json");
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    Root root = JsonConvert.DeserializeObject<Root>(result);

                    Console.WriteLine("BTC Price EUR " + root.bpi.EUR.rate);

                }
                else
                {
                    Console.WriteLine("Internal server Error");
                }
            }
        }       
    }

    public class Time
    {
        public string updated { get; set; }
        public DateTime updatedISO { get; set; }
        public string updateduk { get; set; }
    }

    public class CurrencyCode
    {
        public string code { get; set; }
        public string symbol { get; set; }
        public string rate { get; set; }
        public string description { get; set; }
        public double rate_float { get; set; }
    }   

    public class Bpi
    {
        public CurrencyCode USD { get; set; }
        public CurrencyCode GBP { get; set; }
        public CurrencyCode EUR { get; set; }
    }

    public class Root
    {
        public Time time { get; set; }
        public string disclaimer { get; set; }
        public string chartName { get; set; }
        public Bpi bpi { get; set; }
    }
}
