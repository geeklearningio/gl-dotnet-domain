namespace GeekLearning.Domain.WebSamples.Controllers.Api
{
    using AspnetCore;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private SampleDomain domain;

        public ValuesController(SampleDomain domain)
        {
            this.domain = domain;
        }

        [HttpGet]
        public IActionResult Get(int id, bool throwNow)
        {
            return this.Maybe(this.domain.GetSomeData(id, throwNow));
        }
    }
}
