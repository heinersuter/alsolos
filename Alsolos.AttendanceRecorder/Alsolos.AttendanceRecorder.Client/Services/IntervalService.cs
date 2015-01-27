namespace Alsolos.AttendanceRecorder.Client.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using Alsolos.AttendanceRecorder.Client.Models;

    public class IntervalService
    {
        public async Task<IEnumerable<Interval>> GetIntervals()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:30515/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // New code:
                var response = await client.GetAsync("api/intervals");
                if (response.IsSuccessStatusCode)
                {
                    var intervals = await response.Content.ReadAsAsync<IEnumerable<Interval>>();
                    return intervals;
                }
            }
            return Enumerable.Empty<Interval>();
        }
    }
}
