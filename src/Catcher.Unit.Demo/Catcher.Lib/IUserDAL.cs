using Catcher.Lib.Model;
using System.Collections.Generic;

namespace Catcher.Lib
{
    public interface IUserDAL
    {
        IList<UserInfo> GetAllUsers();

        bool AddUser(UserInfo user);

        UserInfo GetUser(int id);       
    }
}
