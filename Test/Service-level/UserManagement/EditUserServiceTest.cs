﻿//-------------------------------------------------------------------------------------------------
// <copyright file="EditUserServiceTest.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentIt.Tests.Service_level.ContentBrowsing
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RentItService;
    using RentItService.Entities;
    using Utils;
    
    /// <summary>
    /// Tests for UserManagement.EditUser.
    /// </summary>
    [TestClass]
    public class EditUserServiceTest : ServiceTest
    {
        /// <summary>
        /// Purpose: Verfiy that you can edit a user.
        /// 
        /// Steps:
        ///     1. Login to the system.
        ///     2. Edit a user.
        ///     3. Verify that the user was edited.
        /// </summary>
        [TestMethod]
        public void EditUserValidServiceTest()
        {
            User user;
            UserManagement.Login(out user, TestUser.User.Username, TestUser.User.Password);

            const string Name = "Stein Bagger";
            var targetUser = TestUser.User;
            var id = targetUser.ID;
            targetUser.FullName = Name;
            var result = UserManagement.EditUser(user.Token, ref targetUser);

            RentItContext.ReloadDb();

            targetUser = User.All.Single(u => u.ID.Equals(id));

            Assert.IsTrue(result, "EditUser failed");
            Assert.AreEqual(Name, targetUser.FullName);
        }

        /// <summary>
        /// Purpose: Verify that EditUser fails on invalid input.
        /// 
        /// Steps:
        ///     1. Edit a null user.
        ///     2. Verify that the method failed.
        /// </summary>
        [TestMethod]
        public void EditUserNullServiceTest()
        {
            User user;
            UserManagement.Login(out user, TestUser.SystemAdmin.Username, TestUser.SystemAdmin.Password);

            User targetUser = null;
            var result = UserManagement.EditUser(user.Token, ref targetUser);

            Assert.IsFalse(result, "EditUser didn't fail");
        }
    }
}
