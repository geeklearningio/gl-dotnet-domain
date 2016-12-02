namespace GeekLearning.Domain.WebSamples.Controllers
{
    using AspnetCore;
    using Microsoft.AspNetCore.Mvc;
    using System;

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
