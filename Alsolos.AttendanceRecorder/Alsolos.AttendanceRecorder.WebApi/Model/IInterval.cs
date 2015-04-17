namespace Alsolos.AttendanceRecorder.WebApi.Model
{
    using System;

    public interface IInterval
    {
        int Id { get; set; }

        IntervalState State { get; set; }

        DateTime Date { get; set; }

        TimeSpan Start { get; set; }

        TimeSpan End { get; set; }

        DateTime LastModified { get; set; }

        string TimeAccountName { get; set; }
    }
}
