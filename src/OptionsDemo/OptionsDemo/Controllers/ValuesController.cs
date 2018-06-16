namespace OptionsDemo.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly DemoOptions _normal;
        private readonly DemoOptions _snapshot;
        private readonly DemoOptions _monitor;
        //private readonly DemoOptions _normalWithName;
        private readonly DemoOptions _snapshotWithName;
        private readonly DemoOptions _monitorWithName;

        public ValuesController(IOptions<DemoOptions> normalAcc,
                IOptionsSnapshot<DemoOptions> snapshotAcc,
                IOptionsMonitor<DemoOptions> monitorAcc,
                //IOptions<DemoOptions> normalWithNameAcc,
                IOptionsSnapshot<DemoOptions> snapshotWithNameAcc,
                IOptionsMonitor<DemoOptions> monitorWithNameAcc)
        {
            this._normal = normalAcc.Value;
            this._snapshot = snapshotAcc.Value;
            this._monitor = monitorAcc.CurrentValue;
            //this._normalWithName = normalWithNameAcc.get
            this._snapshotWithName = snapshotWithNameAcc.Get("Sec");
            this._monitorWithName = monitorWithNameAcc.Get("Sec");
        }


        // GET api/values
        [HttpGet]
        public string Get()
        {
            var age = $"normal-[{_normal.Age}];snapshot-[{_snapshot.Age}];monitor-[{_monitor.Age}];snapshotWithName-[{_snapshotWithName.Age}];monitorWithName-[{_monitorWithName.Age}]";
            var name = $"normal-[{_normal.Name}];snapshot-[{_snapshot.Name}];monitor-[{_monitor.Name}];snapshotWithName-[{_snapshotWithName.Name}];monitorWithName-[{_monitorWithName.Name}]";

            return $"age:{age} \nname:{name}";
        }
    }
}
