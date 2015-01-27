namespace Alsolos.AttendanceRecorder.WebApi
{
    using System;
    using Alsolos.AttendanceRecorder.WebApi.Controllers;
    using Microsoft.Owin.Hosting;

    public class WebApiStarter : IDisposable
    {
        private IDisposable _webApiService;

        public WebApiStarter(IIntervalCollection intervals)
        {
            Intervals = intervals;
        }

        public static IIntervalCollection Intervals { get; private set; }

        public void Start()
        {
            _webApiService = WebApp.Start<Startup>("http://localhost:30515/");
        }

        public void Dispose()
        {
            if (_webApiService != null)
            {
                _webApiService.Dispose();
            }
        }
    }
}
