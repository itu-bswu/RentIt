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
    public class SignupServiceTest : ServiceTest
    {
        /// <summary>
        /// Purpose: Verify that you can create a new user
        /// 
        /// Steps:
        ///     1. Create a new user
        ///     2. Verify that the user was created
        /// </summary>
        [TestMethod]
        public void SignupTest()
        {
            User user = new User() { Email = "test@example.com", Username = "SteinBagger32", Password = "GOD" };
            var result = UserManagement.SignUp(ref user);

            Assert.IsTrue(result, "Signup failed");
            Assert.IsNotNull(user.ID, "User has not been assigned an id");
        }

        /// <summary>
        /// Purpose: Verify that you cannot create a user without basic information
        /// 
        /// Steps:
        ///     1. Create a new user without the required info
        ///     2. Verify that the user wasn't created
        /// </summary>
        [TestMethod]
        public void SignupMissingInfoTest()
        {
            User user = new User();
            var result = UserManagement.SignUp(ref user);

            Assert.IsFalse(result, "Signup didn't fail");
            Assert.IsNull(user.ID, "User has been assigned an id");
        }
    }
}