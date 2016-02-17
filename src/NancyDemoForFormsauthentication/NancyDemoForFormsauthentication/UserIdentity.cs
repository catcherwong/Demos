using Nancy.Security;
using System.Collections.Generic;

namespace NancyDemoForFormsauthentication
{
    public class UserIdentity : IUserIdentity
    {
        public string UserName { get; set; }
        public IEnumerable<string> Claims { get; set; }
    }
}