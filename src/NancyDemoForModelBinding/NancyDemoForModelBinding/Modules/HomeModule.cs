using Nancy;

namespace NancyDemoForModelBinding.Modules
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = _ =>
            {
                return View["index"];
            };
        }
    }
}