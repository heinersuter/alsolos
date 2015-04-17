namespace Alsolos.AttendanceRecorder.WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using Alsolos.AttendanceRecorder.WebApi.Model;

    [RoutePrefix("api/intervals")]
    public class IntervalsController : ApiController
    {
        private readonly IIntervalCollection _intervalCollection;

        public IntervalsController()
        {
            _intervalCollection = WebApiStarter.IntervalCollection;
        }

        [Route("")]
        [HttpGet]
        public IEnumerable<IInterval> Get()
        {
            return _intervalCollection.Intervals;
        }

        [Route("remove")]
        [HttpPost]
        public bool Remove([FromBody]ReceivedInterval interval)
        {
            if (interval == null)
            {
                return false;
            }
            return _intervalCollection.Remove(interval);
        }

        [Route("merge")]
        [HttpPost]
        public bool Merge(IIntervalPair intervalPair)
        {
            return _intervalCollection.Merge(intervalPair);
        }
    }
}
