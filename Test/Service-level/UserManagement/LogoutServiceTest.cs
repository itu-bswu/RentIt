﻿namespace RentIt.Tests.Service_level.ContentBrowsing
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RentItService.Services;
    using Utils;
    using System.Collections.Generic;
    using RentItService.Entities;
    using System.Linq;
    using RentItService.Enums;
    
    [TestClass]
    public class LogoutServiceTest : ServiceTest
    {
        /// <summary>
        /// Purpose: Verify that you can log out
        /// 
        /// Steps:
        ///     1. Login to the system
        ///     2. Logout of the system
        ///     3. Verify that the user was logged out
        /// </summary>
        [TestMethod]
        public void LogoutTest()
        {
            User user;
            UserManagement.Login(out user, TestUser.User.Username, TestUser.User.Password);

            Assert.IsNotNull(user.Token, "User was not logged in");

            var result = UserManagement.Logout(user.Token);

            Assert.IsTrue(result, "Logout failed");
            Assert.IsNull(user.Token, "Token was not reset");
        }
    }
}