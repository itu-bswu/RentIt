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

    using RentItService;
    using RentItService.Entities;
    using RentItService.Exceptions;

    /// <summary>
    /// Scenario tests for the EditProfile Feature.
    /// </summary>
    [TestClass]
    public class EditProfileScenarioTest : DataTest
    {
        /// <summary>
        /// Purpose: Verify that it is possible to edit a user profile.
        /// <para></para>
        /// Pre-condtions:
        ///     1. A user with user name "testUser" must exist in the database.
        ///     2. "testUser" must have the password 'test.dk'.
        ///     3. "testUser" must have the FullName 'Test User'.
        ///     4. "testUser" must have the email 'testUser@testing.dk'.
        /// <para></para>
        /// Steps:
        ///     1. Assert that pre-conditions hold.
        ///     2. Copy password, full name and email into local variables.
        ///     3. Create new user with changed informaton.
        ///     4. Call editprofile with the new user.
        ///     5. Assert that the user information has changed.
        /// </summary>
        [TestMethod]
        public void EditProfileTest()
        {
            // Arrange
            TestHelper.SetUpTestUsers();

            string oldPassword;
            string oldName;
            string oldEmail;

            using (var db = new RentItContext())
            {
                var user = db.Users.First(u => u.Username == "testUser");

                Assert.AreEqual("test.dk", user.Password, "The password did not match pre-condition 2.");
                Assert.AreEqual("Test User", user.FullName, "The full name did not match pre-condition 3.");
                Assert.AreEqual("testUser@testing.dk", user.Email, "The email did not match pre-condition 4.");

                oldPassword = user.Password;
                oldName = user.FullName;
                oldEmail = user.Email;

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

                // Call edit profile
                User.EditProfile(user2.Token, user2);
            }

            // Assert and clean
            using (var db = new RentItContext())
            {
                User user = db.Users.First(u => u.Username == "testUser");

                Assert.AreEqual(oldPassword.ToUpper(), user.Password);
                Assert.AreEqual(oldName.ToUpper(), user.FullName);
                Assert.AreEqual(oldEmail.ToUpper(), user.Email);

                user.Password = "test.dk";
                user.FullName = "Test User";
                user.Email = "testUser@testing.dk";
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Purpose: Verify that a different user cannot edit a users information.
        /// <para></para>
        /// Pre-condtions:
        ///     1. A user with user name "testUser" must exist in the database.
        ///     2. A user with user name "testContentProvider" must exist in the database.
        /// <para></para>
        /// Steps:
        ///     1. Make sure pre-condtions hold.
        ///     2. Create a new user object as a copy of the "testUser" user.
        ///     3. Call edit profile with "testContentProvider" token and the "testUser" copy object.
        ///     4. Verify that InsufficientAccessLevelException is thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InsufficientAccessLevelException))]
        public void InsufficientAccessEditProfileTest()
        {
            TestHelper.SetUpTestUsers();

            using (var db = new RentItContext())
            {
                var user = db.Users.First(u => u.Username == "testUser");
                var user1 = db.Users.First(u => u.Username == "testContentProvider");

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

                User.EditProfile(user1.Token, user2);
            }
        }

        /// <summary>
        /// Purpose: Verify that values in the userObject parameter cannot be null values.
        /// <para></para>
        /// Pre-condtions:
        ///     1. A user called "testUser" must exist in the database.
        /// <para></para>
        /// Steps:
        ///     1. Make sure pre-conditions hold.
        ///     2. Create a new user object with invalid information.
        ///     3. Call edit profile with the new user object.
        ///     4. Verify that "ArgumentNullException" is thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InvalidInputEditProfileTest()
        {
            TestHelper.SetUpTestUsers();

            using (var db = new RentItContext())
            {
                var user = db.Users.First(u => u.Username == "testUser");

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

                User.EditProfile(user.Token, user2);
            }
        }
    }
}
