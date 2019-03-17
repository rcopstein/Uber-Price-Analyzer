using Domain.Models;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.WebUtilities;

namespace Infrastructure.Services
{
    public class UberAPI : IUberAPI
    {
        private static readonly string address = "https://sandbox-api.uber.com/v1.2";
        private static readonly string serverKey = "xmM25b_vp8DKvv5Yik7M_W2ql3s6JjnSjbjJ2pNw";

        private readonly IHttpClientFactory _httpClientFactory;

        public UberAPI(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> EstimatePrice(Location start, Location end)
        {
            string endpoint = "/estimates/price";

            var values = new Dictionary<string, string>
            {
                { "start_longitude", start.Longitude.ToString() },
                { "start_latitude", start.Latitude.ToString() },
                { "end_longitude", end.Longitude.ToString() },
                { "end_latitude", end.Latitude.ToString() }
            };

            var uri = QueryHelpers.AddQueryString(address + endpoint, values);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);

            request.Headers.Add("Authorization", "Token " + serverKey);
            request.Headers.Add("Accept-Language", "en_US");

            HttpClient client = _httpClientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
