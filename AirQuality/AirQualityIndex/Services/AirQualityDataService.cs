using AirQualityIndex.Interfaces;
using AirQualityIndex.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AirQualityIndex.Services
{
    public class AirQualityDataService : IAirQualityDataProvider
    {
        private readonly string _apiUrl;
        private readonly string _apiKey;
        private readonly AppSettings _appSettings;

        public AirQualityDataService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _apiUrl = _appSettings.ApiUrl;
            _apiKey = _appSettings.ApiKey;
        }
        /// <summary>
        /// Fetches the data from the government provided api based on the parameters provided
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        private (bool, List<Record>) RealFetch(int offset, int limit, string filters)
        {
            using (var httpClient = new HttpClient())
            {
                string url = $"{_apiUrl}?api-key={_apiKey}&format=json&offset={offset}&limit={limit}{FormFilterText(filters)}"; //&filters[state]=Bihar&filters[city]=Arrah
                var response = httpClient.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    bool toContinue = false;
                    var modelResponse = JsonConvert.DeserializeObject<AirQualityRoot>(response.Content.ReadAsStringAsync().Result);
                    if (modelResponse.total > offset + limit)
                    {
                        toContinue = true;
                    }
                    return (toContinue, modelResponse.records);
                }
            }
            return (false, new List<Record>());
        }

        /// <summary>
        /// Fetch ALL the data for the selected filters 
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        public async Task<List<Record>> Fetch(int offset, int limit, string filters)
        {
            try
            {
                var toContinue = true;
                var lst = new List<Record>();
                int tempOffset = offset;
                while (toContinue)
                {
                    (bool, List<Record>) tupleFetched = RealFetch(tempOffset, limit, filters);
                    toContinue = tupleFetched.Item1;
                    lst.AddRange(tupleFetched.Item2);
                    tempOffset += limit;
                }
                return lst;
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }

        /// <summary>
        /// Converts an enumerator string with each element coming in format <key;value> to &filters[a]=b&filters[c]=d
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        private string FormFilterText(IEnumerable<string> filters)
        {
            StringBuilder sb = new StringBuilder();
            IEnumerator<string> filtersEnumerator = filters.GetEnumerator();
            while (filtersEnumerator.MoveNext())
            {
                var currentItem = filtersEnumerator.Current.Split(';');
                sb.Append($"filters[{currentItem[0]}]={currentItem[1]}");
            }
            return sb.ToString();
        }
        /// <summary>
        /// Converts an input in format  a,b;c,d to &filters[a]=b&filters[c]=d which is understood by the government provided API
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        private string FormFilterText(string filters)
        {
            return string.IsNullOrWhiteSpace(filters) ? "" : $"&filters[{filters.Replace(",", "]=").Replace(";", "&filters[")}";
        }
    }
}
