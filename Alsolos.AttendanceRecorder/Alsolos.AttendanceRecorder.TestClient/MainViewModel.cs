namespace Alsolos.AttendanceRecorder.TestClient
{
    using System;
    using Alsolos.Commons.Mvvm;

    public class MainViewModel : ViewModel
    {
        private const string _timeAccountName = "A";
        private static readonly TimeSpan _intervalDuration = TimeSpan.FromMinutes(1.0);

        private readonly UrlBuilder _urlBuilder = new UrlBuilder();

        //public DelegateCommand GetIntervalsCommand
        //{
        //    get { return BackingFields.GetCommand(() => GetIntervalsCommand, GetIntervals); }
        //}

        //private async void GetIntervals()
        //{
        //    //var client = new HttpClient();
        //    //var response = await client.GetAsync(_urlBuilder.Get(UrlBuilder.Controller.Interval, "timeAccountName", _timeAccountName));
        //    //response.EnsureSuccessStatusCode();

        //    //var formatters = new List<MediaTypeFormatter> { new JsonMediaTypeFormatter() };
        //    //Intervals = await response.Content.ReadAsAsync<IEnumerable<Interval>>(formatters);
        //}
    }
}
