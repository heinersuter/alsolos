namespace Alsolos.AttendanceRecorder.Client.Views
{
    using System;

    public class DatePeriod
    {
        private readonly string _name;
        private readonly DateTime _start;
        private readonly DateTime _end;

        public DatePeriod(string name, DateTime start, DateTime end)
        {
            _name = name;
            _start = start.Date;
            _end = end.Date;
        }

        public string Name
        {
            get { return _name; }
        }

        public DateTime Start
        {
            get { return _start; }
        }

        public DateTime End
        {
            get { return _end; }
        }
    }
}
