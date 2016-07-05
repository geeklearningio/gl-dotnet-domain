namespace GeekLearning.Domain.WebApiSamples.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using GeekLearning.Domain.AspnetCore;
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
