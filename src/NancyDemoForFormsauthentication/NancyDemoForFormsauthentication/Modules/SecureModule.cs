using Nancy;
using Nancy.Security;

namespace NancyDemoForFormsauthentication.Modules
{
    public class SecureModule : NancyModule
    {
        public SecureModule()
        {
            this.RequiresAuthentication();
            Get["/secure"] = _ =>
            {
                return "Hello ," + this.Context.CurrentUser.UserName;
            };
        }
    }
}