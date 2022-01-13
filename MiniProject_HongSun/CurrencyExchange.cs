
using System;
using System.Net;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MiniProject_HongSun
{
    internal  class CurrencyExchange
    {
        public const string API_PATH = "https://freecurrencyapi.net/api/v2/latest";
        public const string API_KEY = "b573c960-7387-11ec-a4ac-c7e6d1b7a0af";
        public const int TIMEOUT = 5000;
        public const string FILE_PATH = "../../../rates.json";

        private Dictionary<string, double> _rates =null;

        public  CurrencyExchange()
        {
            Utilities.PrintLineColor("Initializing exchange rates...", ConsoleColor.Yellow);
            string jsonStr;

            // First try to get exchange rates online 
            jsonStr = this.GetOnlineRates().Result;
            if (jsonStr != null)
            {
                this.MapJsonToDicts(jsonStr);
                Utilities.PrintLineColor("Fetch online data success, update local file rates.json.", ConsoleColor.Green);
            }

            // if can NOT fetch exchange rates online, fetch from local file.
            else
            {
                Utilities.PrintLineColor("Fetch online data failed!", ConsoleColor.Red);
                jsonStr = this.GetLocalRates().Result;
                this.MapJsonToDicts(jsonStr);
            }
            if (this._rates != null && this._rates.Count > 0)
            {
                Utilities.PrintLineColor("Initialing exchange rates succsefully", ConsoleColor.Green);
            }
            // Console.WriteLine(this._rates.Count);
        }

       
        /* get latest USD based exchange rate
         * if fetch data succesfully, write data to JSON file
         * return JSON string if success, return null if failed.
         */
        private async Task<string> GetOnlineRates()
        {
            Utilities.PrintLineColor("Fetching latest exchange rates from online resource...", ConsoleColor.Yellow);
            string api_url = API_PATH + "?apikey=" + API_KEY;
            Console.WriteLine("API_URL: " + api_url);
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(api_url);
            req.Method = "GET";
            req.Timeout = TIMEOUT;
            req.ContentType = "text/html;charset=UFT-8";

            try
            {
                HttpWebResponse res = (HttpWebResponse)await req.GetResponseAsync();
                if (res != null && res.StatusCode == HttpStatusCode.OK)
                {
                    Stream resStream = res.GetResponseStream();
                    StreamReader reader = new StreamReader(resStream, Encoding.GetEncoding("utf-8"));
                    string data = reader.ReadToEnd();
                    resStream.Close();
                    reader.Close();
                    // Console.WriteLine(data);
                    await File.WriteAllTextAsync(FILE_PATH, data);
                    return data;
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                return null;    
            }
        }

        /* recover previous exchange rate from file, In case of online souse not available.
         */
        private async Task<string> GetLocalRates()
        {
            Utilities.PrintLineColor("Recovering previous exchange rates from file ...", ConsoleColor.Yellow);
            try
            {
                return await File.ReadAllTextAsync(FILE_PATH);
            }
            catch (Exception e){
                Console.Error.WriteLine(e.Message);
                return null; }
        }

        // map the muti-layer json to dictionaries
        private void MapJsonToDicts(string jsonStr)
        {
            Dictionary<string, JsonElement> firstLayer = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(jsonStr);
            // Console.WriteLine(firstLayer["query"].ToString());
            // this._query = JsonSerializer.Deserialize <Dictionary<string, string>>(firstLayer["query"].ToString());
            this._rates = JsonSerializer.Deserialize<Dictionary<string, double>>(firstLayer["data"].ToString());
        }

        public string Amount(Location location, double priceInUSD)
        {
            string currencyName;
            switch (location)
            {
                case Location.Sweden:
                    currencyName = "SEK";
                    break;
                case Location.Norway:
                    currencyName = "NOK";
                    break;
                case Location.Denmark:
                    currencyName = "DKK";
                    break;
                case Location.Finland:
                    currencyName = "EUR";
                    break;
                default:
                    currencyName = "USD";
                    break; ;
            }
            if (currencyName == "USD") // rates is USD based, therefore "USD" key is not in the dictionary
                return priceInUSD + " USD";
            else
            {
                double rate = this._rates[currencyName];
                return Math.Round(rate * priceInUSD, 2) + " " + currencyName;
            }
        }
    }
}
