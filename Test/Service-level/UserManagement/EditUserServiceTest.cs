namespace RentIt.Tests.Service_level.ContentBrowsing
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RentItService.Services;
    using Utils;
    using System.Collections.Generic;
    using RentItService.Entities;
    using System.Linq;
    using RentItService.Enums;
    using RentItService;
    
    [TestClass]
    public class EditUserServiceTest : ServiceTest
    {
        /// <summary>
        /// Purpose: Verfiy that you can edit a user
        /// 
        /// Steps:
        ///     1. Login to the system
        ///     2. Edit a user
        ///     3. Verify that the user was edited
        /// </summary>
        [TestMethod]
        public void EditUserTest()
        {
            User user;
            UserManagement.Login(out user, TestUser.SystemAdmin.Username, TestUser.SystemAdmin.Password);

            const string name = "Stein Bagger";
            var targetUser = User.All.First();
            var id = targetUser.ID;
            targetUser.FullName = name;
            var result = UserManagement.EditUser(user.Token, ref targetUser);

            RentItContext.ReloadDb();

            targetUser = User.All.Single(u => u.ID.Equals(id));

            Assert.IsTrue(result, "EditUser failed");
            Assert.AreEqual(name, targetUser.FullName);
        }

        /// <summary>
        /// Purpose: Verify that EditUser fails on invalid input
        /// 
        /// Steps:
        ///     1. Edit a null user
        ///     2. Verify that the method failed
        /// </summary>
        [TestMethod]
        public void EditUserNullTest()
        {
            User user;
            UserManagement.Login(out user, TestUser.SystemAdmin.Username, TestUser.SystemAdmin.Password);

            User targetUser = null;
            var result = UserManagement.EditUser(user.Token, ref targetUser);

            Assert.IsFalse(result, "EditUser didn't fail");
        }
    }
}
