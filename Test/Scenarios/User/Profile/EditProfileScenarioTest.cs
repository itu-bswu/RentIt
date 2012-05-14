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
        /// <para>
        /// Pre-condtions:
        ///     1. A user with user name "Smith" must exist in the database.
        ///     2. "Smith" must have the FullName 'James Smith'.
        ///     3. "Smith" must have the email 'smith@matrix.org'.
        /// </para>
        /// <para>
        /// Steps:
        ///     1. Assert that pre-conditions hold.
        ///     2. Copy password, full name and email into local variables.
        ///     3. Create new user with changed informaton.
        ///     4. Call editprofile with the new user.
        ///     5. Assert that the user information has changed.
        /// </para>
        /// </summary>
        [TestMethod]
        public void EditProfileTest()
        {
            // Arrange
            var user = User.Login(TestUser.User.Username, TestUser.User.Password);

            Assert.AreEqual("James Smith", user.FullName, "The full name did not match pre-condition 2.");
            Assert.AreEqual("smith@matrix.org", user.Email, "The email did not match pre-condition 3.");

            var oldPassword = "userPassword";
            var oldName = user.FullName;
            var oldEmail = user.Email;
            var oldID = user.ID;

            var user2 = new User
            {
                Email = user.Email.ToUpper(),
                FullName = user.FullName.ToUpper(),
                ID = user.ID,
                Password = oldPassword.ToUpper(),
                Rentals = user.Rentals,
                Token = user.Token,
                Type = user.Type,
                TypeValue = user.TypeValue,
                Username = user.Username
            };

            // Call edit profile
            User.Edit(user2, user2);
            RentItContext.ReloadDb();

            // Assert and clean
            var user3 = User.All().First(u => u.Username == "Smith");

            Assert.AreEqual(oldID, User.Login(user3.Username, oldPassword.ToUpper()).ID, "Password change did not succeed.");
            Assert.AreEqual(oldName.ToUpper(), user3.FullName, "The name was not changed as expected.");
            Assert.AreEqual(oldEmail.ToUpper(), user3.Email, "The email was not changed as expected.");
        }

        /// <summary>
        /// Purpose: Verify that a different user cannot edit a users information.
        /// <para>
        /// Pre-condtions:
        ///     1. A user with user name "Smith" must exist in the database.
        ///     2. A user with user name "Universal" must exist in the database.
        /// </para>
        /// <para>
        /// Steps:
        ///     1. Make sure pre-condtions hold.
        ///     2. Create a new user object as a copy of the "Smith" user.
        ///     3. Call edit profile with "Universal" token and the "Smith" copy object.
        ///     4. Verify that InsufficientRightsException is thrown.
        /// </para>
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InsufficientRightsException))]
        public void InsufficientRightsEditProfileTest()
        {
            var user = User.Login(TestUser.User.Username, TestUser.User.Password);
            var user1 = User.Login(TestUser.ContentProvider.Username, TestUser.ContentProvider.Password);

            var user2 = new User
            {
                Email = user.Email.ToUpper(),
                FullName = user.FullName.ToUpper(),
                ID = user.ID,
                Password = user.Password.ToUpper(),
                Rentals = user.Rentals,
                Token = user.Token,
                Type = user.Type,
                TypeValue = user.TypeValue,
                Username = user.Username
            };

            User.Edit(user1, user2);
        }

        /// <summary>
        /// Purpose: Verify that values in the userObject parameter cannot be null values.
        /// <para>
        /// Pre-condtions:
        ///     1. A user called "Smith" must exist in the database.
        /// </para>
        /// <para>
        /// Steps:
        ///     1. Make sure pre-conditions hold.
        ///     2. Create a new user object with invalid information.
        ///     3. Call edit profile with the new user object.
        ///     4. Verify that "ArgumentNullException" is thrown.
        /// </para>
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InvalidInputEditProfileTest()
        {
            var user = User.Login(TestUser.User.Username, TestUser.User.Password);

            var user2 = new User
            {
                Email = user.Email.ToUpper(),
                FullName = null,
                ID = user.ID,
                Password = null,
                Rentals = user.Rentals,
                Token = user.Token,
                Type = user.Type,
                TypeValue = user.TypeValue,
                Username = user.Username
            };

            User.Edit(user, user2);
        }
    }
}
