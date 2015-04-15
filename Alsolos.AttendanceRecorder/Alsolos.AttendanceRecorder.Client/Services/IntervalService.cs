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
        private readonly Logger _logger;

        public IntervalService()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        public async Task<IEnumerable<Interval>> GetIntervals()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:30515/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync("api/intervals");
                if (response.IsSuccessStatusCode)
                {
                    var intervals = await response.Content.ReadAsAsync<IEnumerable<Interval>>();
                    return intervals;
                }
                _logger.Error("No data from server. {0} - {1}", response.StatusCode, response.ReasonPhrase);
            }
            return Enumerable.Empty<Interval>();
        }
    }
}
