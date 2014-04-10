using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Alsolos.AttendanceRecorder.WebApi.Models;

namespace Alsolos.AttendanceRecorder.WebApi.Controllers {
    public class LifeSignController : ApiController {
        private readonly IList<Interval> _intervals = new List<Interval> { 
            new Interval { TimeAccountName = "A", Start = new DateTime(2014, 04, 04, 8, 0, 0), End = new DateTime(2014, 04, 04, 12, 0, 0) }, 
            new Interval { TimeAccountName = "A", Start = new DateTime(2014, 04, 04, 13, 0, 0), End = new DateTime(2014, 04, 04, 17, 0, 0) }, 
        };

        public IEnumerable<LifeSign> Get() {
            return new[] { new LifeSign { IntervalDuration = TimeSpan.FromMinutes(1), TimeAccountName = "A" } };
        }

        public void Post(LifeSign lifeSign) {
            var currentTime = DateTime.Now;

            var currentInterval = _intervals.SingleOrDefault(interval => interval.TimeAccountName == lifeSign.TimeAccountName
                && interval.End + lifeSign.IntervalDuration + lifeSign.IntervalDuration > currentTime);
            if (currentInterval != null) {
                currentInterval.End = currentTime;
            } else {
                _intervals.Add(new Interval { TimeAccountName = lifeSign.TimeAccountName, Start = currentTime, End = currentTime });
            }
        }
    }
}
