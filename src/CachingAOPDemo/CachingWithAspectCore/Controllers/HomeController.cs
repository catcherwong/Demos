namespace CachingWithAspectCore.Controllers
{
    using AspectCore.Injector;
    using CachingWithAspectCore.BLL;
    using CachingWithAspectCore.Services;
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
        private IServiceResolver _scope;
        private DateTimeBLL _dateTimeBLL;

        public BllController(IServiceResolver scope)
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
