using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.WebUtilities;
using Application.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Domain.Models;
using System;

namespace Infrastructure.Services.UberAPI
{
    public class UberAPI : IUberAPI
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _serverKey;
        private readonly string _host;

        public UberAPI(
            IHttpClientFactory httpClientFactory, 
            IConfiguration configuration)
        {
            _serverKey = configuration["UberAPI:ServerToken"];
            _host = configuration["UberAPI:Host"];

            _httpClientFactory = httpClientFactory;
        }

        private Dictionary<string, string> BuildParams(Location start, Location end)
        {
            return new Dictionary<string, string>
            {
                { "start_longitude", start.Longitude.ToString() },
                { "start_latitude", start.Latitude.ToString() },
                { "end_longitude", end.Longitude.ToString() },
                { "end_latitude", end.Latitude.ToString() }
            };
        }

        private HttpRequestMessage BuildRequest(string endpoint, Dictionary<string, string> values)
        {
            var uri = QueryHelpers.AddQueryString(_host + endpoint, values);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);

            request.Headers.Add("Authorization", "Token " + _serverKey);
            request.Headers.Add("Accept-Language", "en_US");

            return request;
        }

        private async Task<IEnumerable<PriceEstimate>> ReadResponse(HttpResponseMessage response)
        {
            var estimateResponse = await response.Content.ReadAsAsync<EstimateResponse>();
            var estimates = estimateResponse.Prices;

            var result = new List<PriceEstimate>();
            foreach (Estimate e in estimates)
            {
                result.Add(new PriceEstimate
                {
                    ProductId = e.product_id,
                    HighEstimate = e.high_estimate,
                    LowEstimate = e.low_estimate,
                    Date = DateTime.Now
                });
            }

            return result;
        }

        public async Task<IEnumerable<PriceEstimate>> Estimate(Location start, Location end)
        {
            string endpoint = "/estimates/price";

            var values = BuildParams(start, end);
            var request = BuildRequest(endpoint, values);

            HttpClient client = _httpClientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return await ReadResponse(response);
        }
    }
}
