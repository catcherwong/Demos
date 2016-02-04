using Nancy;

namespace MovieDemoWithOwin.Modules
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = _ => ShowHomePage();
            Get["/home/about"] = _ => ShowAboutPage();
            Get["/home/contact"] = _ => ShowContactPage();
        }

        private dynamic ShowContactPage()
        {
            return View["Contact"];
        }

        private dynamic ShowAboutPage()
        {
            return View["About"];
        }

        private dynamic ShowHomePage()
        {
            return View["Index"];
        }
    }
}