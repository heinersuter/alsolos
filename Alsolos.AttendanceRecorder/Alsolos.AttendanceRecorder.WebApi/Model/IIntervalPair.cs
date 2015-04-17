namespace Alsolos.AttendanceRecorder.WebApi.Model
{
    public interface IIntervalPair
    {
        IInterval Interval1 { get; }

        IInterval Interval2 { get; }
    }
}
