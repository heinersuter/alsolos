namespace Alsolos.AttendanceRecorder.WebApi.Controllers
{
    using System.Collections.Generic;

    public interface IIntervalCollection
    {
        IEnumerable<IInterval> Intervals { get; }
    }
}
