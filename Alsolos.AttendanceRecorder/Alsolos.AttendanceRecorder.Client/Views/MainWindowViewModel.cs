namespace Alsolos.AttendanceRecorder.Client.Views
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Alsolos.AttendanceRecorder.Client.Models;
    using Alsolos.AttendanceRecorder.Client.Services;
    using Alsolos.Commons.Mvvm;
    using NLog;

    public class MainWindowViewModel : ViewModel
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public MainWindowViewModel()
        {
            LoadIntervals();
        }

        public IntervalsSelectorViewModel IntervalsSelectorViewModel
        {
            get { return BackingFields.GetValue(() => new IntervalsSelectorViewModel()); }
        }

        private async void LoadIntervals()
        {
            var intervalService = new IntervalService();
            IEnumerable<Interval> modelIntervals = null;
            try
            {
                modelIntervals = await intervalService.GetIntervals();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            if (modelIntervals != null)
            {
                IntervalsSelectorViewModel.SetIntervals(modelIntervals.ToList());
            }
        }
    }
}
