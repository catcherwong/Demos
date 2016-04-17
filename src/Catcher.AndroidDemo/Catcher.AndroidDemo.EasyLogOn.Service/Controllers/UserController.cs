using System.Linq;
using System.Web.Mvc;

namespace Catcher.AndroidDemo.EasyLogOn.Service.Controllers
{
    public class UserController : Controller
    {        
        public ActionResult LogOn(string userName, string userPwd)
        {
            bool result = IsAuth(userName,userPwd);
            ReturnModel m = new ReturnModel();
            if (result)
            {
                m.Code = "00000";
                m.Msg = "Success";
            }
            else
            {
                m.Code = "00001";
                m.Msg = "Failure";
            }
            return Json(m, JsonRequestBehavior.AllowGet);          
        }

        public bool IsAuth(string name, string pwd)
        {
            using (Models.DBDemo db = new Models.DBDemo())
            {
                int count = db.UserInfo.Count(u=>u.UserName==name&&u.UPassword==pwd);
                return count == 1 ? true : false;
            }            
        }
    }

    public class ReturnModel
    {
        public string Code { get; set; }
        public string Msg { get; set; }
    }
}