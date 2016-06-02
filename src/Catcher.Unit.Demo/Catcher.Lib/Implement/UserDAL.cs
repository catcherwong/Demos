using Catcher.Lib.Model;
using System.Collections.Generic;
using System.Linq;

namespace Catcher.Lib.Implement
{
    public class UserDAL : IUserDAL
    {
        public bool AddUser(UserInfo user)
        {
            using (DB db = new DB())
            {
                db.UserInfoes.Add(user);
                return db.SaveChanges() > 0;
            }
        }

        public IList<UserInfo> GetAllUsers()
        {
            using (DB db = new DB())
            {
                return db.UserInfoes.ToList();
            }
        }

        public UserInfo GetUser(int id)
        {
            using (DB db = new DB())
            {
                return db.UserInfoes.Find(id);
            }
        }
    }
}