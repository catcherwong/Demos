using Catcher.Lib.Model;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Catcher.Lib.Test
{
    public class UserDALTest
    {
        private List<UserInfo> _userList;
        private UserInfo _user;

        public UserDALTest()
        {
            //fake data
            _userList = new List<UserInfo>();
            _userList.Add(new UserInfo { UserID = 1, UserName="catcher", UserIsActive=true });
            _userList.Add(new UserInfo { UserID = 2, UserName = "hawk", UserIsActive = false });

            _user = new UserInfo() { UserID=3, UserIsActive=true, UserName = "tom" }; 
        }

        [Fact]
        public void get_all_users_should_success()
        {
            //arrange
            var fakeObject = new Mock<IUserDAL>();

            fakeObject.Setup(x=>x.GetAllUsers()).Returns(_userList);

            //act
            var res = fakeObject.Object.GetAllUsers();

            //assert
            Assert.Equal(2,res.Count);
        }

        [Fact]
        public void add_a_user_should_success()
        {
            var fakeObject = new Mock<IUserDAL>();
      
            fakeObject.Setup(x => x.AddUser(It.IsAny<UserInfo>())).Returns(true);

            var res =  fakeObject.Object.AddUser(_user);

            Assert.Equal<bool>(true,res);
        }

        [Fact]
        public void add_a_user_should_fail()
        {
            var fakeObject = new Mock<IUserDAL>();

            fakeObject.Setup(x => x.AddUser(It.IsAny<UserInfo>())).Returns(false);

            var res = fakeObject.Object.AddUser(_user);

            Assert.Equal<bool>(false, res);
        }

        [Fact]
        public void get_a_user_by_valid_id_should_success()
        {
            var fakeObject = new Mock<IUserDAL>();

            fakeObject.Setup(x=>x.GetUser(It.IsAny<int>())).Returns(_user);

            var res = fakeObject.Object.GetUser(3);

            Assert.Equal(3,res.UserID);
            Assert.Equal("tom",res.UserName);
            Assert.Equal(true,res.UserIsActive);
        }

        [Fact]
        public void get_a_user_by_invalid_id_should_throw_exception()
        {
            var fakeObject = new Mock<IUserDAL>();

            fakeObject.Setup(x => x.GetUser(It.IsAny<int>())).Throws(new NullReferenceException());           

            Assert.Throws<NullReferenceException>(()=> fakeObject.Object.GetUser(4));               
        }

    }
}
