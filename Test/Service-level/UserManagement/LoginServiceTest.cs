namespace RentIt.Tests.Service_level.ContentBrowsing
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RentItService.Services;
    using Utils;
    using System.Collections.Generic;
    using RentItService.Entities;
    using System.Linq;
    using RentItService.Enums;
    
    [TestClass]
    public class LoginServiceTest : ServiceTest
    {
        /// <summary>
        /// Purpose: Verify that you can login in
        /// 
        /// Steps:
        ///     1. Login to the system
        ///     2. Verify that the user was logged in
        /// </summary>
        [TestMethod]
        public void LoginTest()
        {
            var targetUser = User.All.First();

            User user;
            var result = UserManagement.Login(out user, targetUser.Username, targetUser.Password); // ?

            Assert.IsTrue(result, "Login failed");
            Assert.IsNotNull(user.Token, "User doesn't have a token");
        }
    }
}
