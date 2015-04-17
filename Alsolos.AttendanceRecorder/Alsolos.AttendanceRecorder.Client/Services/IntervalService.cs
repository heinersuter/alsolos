namespace Alsolos.AttendanceRecorder.Client.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using Alsolos.AttendanceRecorder.Client.Models;
    using NLog;

    public class IntervalService
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly Uri _baseAddress = new Uri("http://localhost:30515/");

        public async Task<IEnumerable<Interval>> GetIntervals()
        {
            using (var client = InitClient())
            {
                var response = await client.GetAsync("api/intervals");
                if (response.IsSuccessStatusCode)
                {
                    var intervals = await response.Content.ReadAsAsync<IEnumerable<Interval>>();
                    return intervals;
                }
                _logger.Error("Getting intervals failed. {0} - {1}", response.StatusCode, response.ReasonPhrase);
            }
            return Enumerable.Empty<Interval>();
        }

        public async Task RemoveInterval(Interval interval)
        {
            using (var client = InitClient())
            {
                var response = await client.PostAsJsonAsync("api/intervals/remove", interval);
                if (response.IsSuccessStatusCode)
                {
                    var isSuccessful = await response.Content.ReadAsAsync<bool>();
                    if (isSuccessful)
                    {
                        _logger.Error("Removing interval not possible.");
                    }
                }
                _logger.Error("Removing interval failed. {0} - {1}", response.StatusCode, response.ReasonPhrase);
            }
        }

        private HttpClient InitClient()
        {
            var client = new HttpClient { BaseAddress = _baseAddress };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }
    }
}
