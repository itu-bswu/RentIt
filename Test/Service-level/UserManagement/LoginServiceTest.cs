//-------------------------------------------------------------------------------------------------
// <copyright file="LoginServiceTest.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentIt.Tests.Service_level.ContentBrowsing
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RentItService.Entities;
    using Utils;

    /// <summary>
    /// Tests for UserManagement.Login.
    /// </summary>
    [TestClass]
    public class LoginServiceTest : ServiceTest
    {
        /// <summary>
        /// Purpose: Verify that you can log in.
        /// 
        /// Steps:
        ///     1. Log in to the system.
        ///     2. Verify that the user was logged in.
        /// </summary>
        [TestMethod]
        public void LoginValidServiceTest()
        {
            var targetUser = TestUser.User;

            User user;
            var result = UserManagement.Login(out user, targetUser.Username, targetUser.Password);

            Assert.IsTrue(result, "Login failed");
            Assert.IsNotNull(user.Token, "User doesn't have a token");
        }

        /// <summary>
        /// Purpose: Verify that the user won't get logged in, 
        ///          if using a wrong password.
        /// 
        /// Steps:
        ///     1. Try to log in to the service, with a correct 
        ///        username, but wrong password.
        ///     2. Verify that the returned value is false.
        ///     3. Verify that the returned user is null.
        /// </summary>
        [TestMethod]
        public void LoginWrongPasswordServiceTest()
        {
            var targetUser = TestUser.User;

            User user;
            var result = UserManagement.Login(out user, targetUser.Username, "not the real password");

            Assert.IsFalse(result, "Login didn't fail.");
            Assert.IsNull(user, "User information has been returned, when shouldn't.");
        }
    }
}
