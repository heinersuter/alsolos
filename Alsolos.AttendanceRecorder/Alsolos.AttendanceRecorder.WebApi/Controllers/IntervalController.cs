using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Alsolos.AttendanceRecorder.WebApi.Models;

namespace Alsolos.AttendanceRecorder.WebApi.Controllers {
    public class IntervalController : ApiController {
        private readonly IList<Interval> _intervals = new List<Interval> { 
            new Interval { TimeAccountName = "A", Start = new DateTime(2014, 04, 04, 8, 0, 0), End = new DateTime(2014, 04, 04, 12, 0, 0) }, 
            new Interval { TimeAccountName = "A", Start = new DateTime(2014, 04, 04, 13, 0, 0), End = new DateTime(2014, 04, 04, 17, 0, 0) }, 
        };

        public IEnumerable<Interval> Get(string timeAccountName) {
            return _intervals.Where(interval => interval.TimeAccountName == timeAccountName);
        }
    }
}
