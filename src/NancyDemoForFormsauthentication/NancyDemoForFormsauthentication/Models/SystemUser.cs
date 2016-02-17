using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NancyDemoForFormsauthentication.Models
{
    public class SystemUser
    {
        public Guid SystemUserId { get; set; }
        public string SystemUserName { get; set; }
        public string SystemUserPassword { get; set; }
    }
}