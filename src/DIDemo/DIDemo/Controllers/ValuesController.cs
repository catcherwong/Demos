namespace DIDemo.Controllers
{
    using DIDemo.Services;
    using Microsoft.AspNetCore.Mvc;
    using System;
    
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {        
        private IDemoService _serviceA;

        private IDemoService _serviceB;

        private readonly Func<string, IDemoService> _serviceAccessor;

        public ValuesController(Func<string, IDemoService> serviceAccessor)
        {
            this._serviceAccessor = serviceAccessor;

            _serviceA = _serviceAccessor("ServiceA");
            _serviceB = _serviceAccessor("ServiceB");
        }

        //public ValuesController(IDemoService serviceA, IDemoService serviceB)
        //{
        //    _serviceA = serviceA;
        //    _serviceB = serviceB;
        //}

        // GET api/values
        [HttpGet]
        public string Get()
        {
            return $"{_serviceA.Get()}-{_serviceB.Get()}";
        }
    }
}
