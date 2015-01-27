namespace Alsolos.AttendanceRecorder.WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;

    public class IntervalsController : ApiController
    {
        // GET api/intervals 
        public IEnumerable<IInterval> Get()
        {
            var intervals = WebApiStarter.Intervals;
            return intervals.Intervals;
        }

        // GET api/values/5 
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values 
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5 
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5 
        public void Delete(int id)
        {
        }
    }
}
