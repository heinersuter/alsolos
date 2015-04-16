namespace Alsolos.AttendanceRecorder.Client.Views.Model
{
    using System;

    public class DatePeriod
    {
        public DatePeriod(DatePeriodType type, string name, DateTime start, DateTime end)
        {
            Type = type;
            Name = name;
            Start = start.Date;
            End = end.Date;
        }

        public DatePeriodType Type { get; private set; }

        public string Name { get; private set; }

        public DateTime Start { get; private set; }

        public DateTime End { get; private set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
