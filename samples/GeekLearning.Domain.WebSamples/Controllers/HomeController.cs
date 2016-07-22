using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Diagnostics;
using GeekLearning.Domain.AspnetCore;

namespace GeekLearning.Domain.WebSamples.Controllers
{
    public class HomeController : Controller
    {
        private readonly SampleDomain domain;

        public HomeController(SampleDomain domain)
        {
            this.domain = domain;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ThrowDomainException()
        {
            this.domain.GetSomeData(1, true);
            return View();
        }

        public IActionResult ThrowStandardException()
        {
            throw new NotSupportedException();
        }

        public IActionResult Error()
        {
            return this.ErrorView("DomainError", "Error");
        }
    }
}
