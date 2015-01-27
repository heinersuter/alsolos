namespace Alsolos.AttendanceRecorder.WebApi
{
    using System;
    using Microsoft.Owin.Hosting;

    public class WebApiStarter : IDisposable
    {
        private IDisposable _webApiService;

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
