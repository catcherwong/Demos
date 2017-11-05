namespace CachingWithCastle.Controllers
{
    using Autofac;
    using CachingWithCastle.BLL;
    using CachingWithCastle.Services;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : Controller
    {
        private IDateTimeService _dateTimeService;

        public HomeController(IDateTimeService dateTimeService)
        {
            _dateTimeService = dateTimeService;
        }

        public IActionResult Index()
        {
            return Content(_dateTimeService.GetCurrentUtcTime());
        }
    }

    public class BllController : Controller
    {
        private ILifetimeScope _scope;
        private DateTimeBLL _dateTimeBLL;

        public BllController(ILifetimeScope scope)
        {
            this._scope = scope;
            _dateTimeBLL = _scope.Resolve<DateTimeBLL>();
        }

        public IActionResult Index()
        {
            return Content(_dateTimeBLL.GetCurrentUtcTime());
        }
    }
}
