using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using Alsolos.AttendanceRecorder.WebApi.Models;
using Alsolos.Commons.Mvvm;

namespace Alsolos.AttendanceRecorder.TestClient {
    public class MainViewModel : ViewModel {
        private const string _timeAccountName = "A";
        private static readonly TimeSpan _intervalDuration = TimeSpan.FromMinutes(1.0);

        private readonly UrlBuilder _urlBuilder = new UrlBuilder();

        public IEnumerable<Interval> Intervals {
            get { return BackingFields.GetValue(() => Intervals); }
            set { BackingFields.SetValue(() => Intervals, value); }
        }

        public DelegateCommand GetIntervalsCommand {
            get { return BackingFields.GetCommand(() => GetIntervalsCommand, GetIntervals); }
        }

        public DelegateCommand SendLifeSignalCommand {
            get { return BackingFields.GetCommand(() => SendLifeSignalCommand, SendLifeSignal); }
        }

        private async void GetIntervals() {
            var client = new HttpClient();
            var response = await client.GetAsync(_urlBuilder.Get(UrlBuilder.Controller.Interval, "timeAccountName", _timeAccountName));
            response.EnsureSuccessStatusCode();

            var formatters = new List<MediaTypeFormatter> { new JsonMediaTypeFormatter() };
            Intervals = await response.Content.ReadAsAsync<IEnumerable<Interval>>(formatters);
        }

        private async void SendLifeSignal() {
            var client = new HttpClient();
            var response = await client.PostAsJsonAsync(
                _urlBuilder.Get(UrlBuilder.Controller.LifeSign),
                new LifeSign {
                    TimeAccountName = _timeAccountName,
                    IntervalDuration = _intervalDuration
                });
            response.EnsureSuccessStatusCode();
        }
    }
}
