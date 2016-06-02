using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeekLearning.Domain.WebApiSamples.Domain;
using GeekLearning.Domain.AspnetCore;
using Microsoft.AspNetCore.Mvc;

namespace GeekLearning.Domain.WebApiSamples.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private SampleDomain domain;

        public ValuesController(Domain.SampleDomain domain)
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
