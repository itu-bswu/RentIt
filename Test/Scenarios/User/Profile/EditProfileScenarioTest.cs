// -----------------------------------------------------------------------
// <copyright file="EditProfileScenarioTest.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace RentIt.Tests.Scenarios.User.Profile
{
    using System;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RentIt.Tests.Utils;

    using RentItService;
    using RentItService.Entities;
    using RentItService.Exceptions;

    /// <summary>
    /// Scenario tests for the Edit Feature.
    /// </summary>
    [TestClass]
    public class EditProfileScenarioTest : DataTest
    {
        /// <summary>
        /// Purpose: Verify that it is possible to edit a user profile.
        /// 
        /// Steps:
        ///     1. Copy password, full name and email into local variables.
        ///     2. Create new user with changed informaton.
        ///     3. Call editprofile with the new user.
        ///     4. Assert that the user information has changed.
        /// </summary>
        [TestMethod]
        public void EditProfileTest()
        {
            // Arrange
            var user = User.Login(TestUser.User.Username, TestUser.User.Password);

            var oldPassword = TestUser.User.Password;
            var oldName = user.FullName;
            var oldEmail = user.Email;
            var oldID = user.ID;

            var user2 = new User
            {
                Email = user.Email.ToUpper(),
                FullName = user.FullName.ToUpper(),
                ID = user.ID,
                Password = oldPassword.ToUpper(),
                Token = user.Token,
                Type = user.Type,
                TypeValue = user.TypeValue,
                Username = user.Username
            };

            // Call edit profile
            user.Edit(user2);
            RentItContext.ReloadDb();

            // Assert and clean
            var user3 = User.All.First(u => u.Username == TestUser.User.Username);

            Assert.AreEqual(oldID, User.Login(user3.Username, oldPassword.ToUpper()).ID, "Password change did not succeed.");
            Assert.AreEqual(oldName.ToUpper(), user3.FullName, "The name was not changed as expected.");
            Assert.AreEqual(oldEmail.ToUpper(), user3.Email, "The email was not changed as expected.");
        }

        /// <summary>
        /// Purpose: Verify that it is possible to edit only part of a user's profile.
        /// 
        /// Steps:
        ///     1. Log in as a user.
        ///     2. Keep a copy of all old values of the user.
        ///     3. Edit profile with only a changed email address.
        ///     4. Refresh user info.
        ///     5. Verify that the email has changed.
        ///     6. Verify that the rest has not been updated.
        /// </summary>
        [TestMethod]
        public void EditPartOfProfileInfoTest()
        {
            var newEmail = "new@email.dk";

            // Step 1
            var user = User.Login(TestUser.User);

            // Step 2
            var oldName = user.FullName;
            var oldEmail = user.Email;
            var oldPassword = user.Password;

            // Step 3
            user.Edit(new User
            {
                ID = user.ID,
                Email = newEmail,
            });

            // Step 4
            user = User.Login(TestUser.User);

            // Step 5
            Assert.AreEqual(newEmail, user.Email, "Email has not changed!");
            Assert.AreNotEqual(oldEmail, user.Email, "Email has not changed!");

            // Step 6
            Assert.AreEqual(oldName, user.FullName, "Full name has changed!");
            Assert.AreEqual(oldPassword, user.Password, "Password has changed!");
        }

        /// <summary>
        /// Purpose: Verify that it is possible to edit a user's password, and nothing else.
        /// 
        /// Steps:
        ///     1. Log in as a user.
        ///     2. Keep a copy of all old values of the user.
        ///     3. Edit profile with only a changed password.
        ///     4. Refresh user info.
        ///     5. Verify that the password has changed (and has been re-hashed).
        ///     6. Verify that the rest has not been updated.
        ///     7. Verify that it is possible to login with the new password.
        ///     8. Verify that it is not possible to login with the old password.
        /// </summary>
        [TestMethod]
        public void EditPartOfProfileOnlyPasswordTest()
        {
            var newPassword = User.GenerateToken();

            // Step 1
            var user = User.Login(TestUser.User);

            // Step 2
            var oldName = user.FullName;
            var oldEmail = user.Email;
            var oldPassword = user.Password;

            // Step 3
            user.Edit(new User
            {
                ID = user.ID,
                Password = newPassword,
            });

            // Step 4
            user = User.Login(TestUser.User.Username, newPassword);

            // Step 5
            Assert.AreNotEqual(oldPassword, user.Password, "Password has not changed!");
            Assert.AreNotEqual(newPassword, user.Password, "Password has not been hashed!");

            // Step 6
            Assert.AreEqual(oldName, user.FullName, "Full name has changed!");
            Assert.AreEqual(oldEmail, user.Email, "Email has changed!");

            // Step 7
            Assert.IsNotNull(User.Login(TestUser.User.Username, newPassword), "Not able to login with new password!");

            // Step 8
            Assert.IsNull(User.Login(TestUser.User), "Able to login with old password!");
        }
    }
}
