//-------------------------------------------------------------------------------------------------
// <copyright file="LoginScenarioTest.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentIt.Tests.Scenarios.User.Profile
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RentItService;
    using RentItService.Entities;
    using RentItService.Exceptions;
    using RentIt.Tests.Utils;

    /// <summary>
    /// Scenario tests of the Login feature.
    /// </summary>
    [TestClass]
    public class LoginScenarioTest : DataTest
    {
        /// <summary>
        /// Purpose: Verify that login is possible when using the 
        /// right username and password.
        /// 
        /// Pre-conditions:
        ///     1. A normal user exists in the database.
        /// 
        /// Steps:
        ///     1. Log in with the credentials for user from pre-condition 1.
        ///     2. Verify it is possible.
        /// </summary>
        [TestMethod]
        public void LoginWithExistingUser()
        {
            var username = User.GenerateToken(); // Should be unique enough.
            const string Password = "VeryUniquePassword";

            // Pre-condition
            var user = new User
            {
                Username = username,
                Password = Password,
                Email = "Very@unique.com"
            };

            User.SignUp(user);
            RentItContext.ReloadDb();

            var userId = User.All.First(u => u.Username == username).ID;

            // Step 1
            var loggedIn = User.Login(username, Password);

            // Step 2
            Assert.AreEqual(loggedIn.ID, userId, "Incorrect user logged in!");
        }

        /// <summary>
        /// Purpose: Verify it is not possible to login, when no 
        /// users with the given username and password exists.
        /// 
        /// Pre-conditions:
        ///     1. No user with a specific pre-defined set of credentials exists.
        /// 
        /// Steps:
        ///     1. Try to login with the credentials from pre-condition 1.
        ///     2. Verify it is not possible.
        /// </summary>
        [TestMethod]
        public void LoginWithNonExistingUser()
        {
            string username;
            string password;

            // Pre-condition
            do
            {
                username = User.GenerateToken();
                password = User.GenerateToken();
            }
            while (User.All.Any(u => u.Username == username || u.Password == password));

            // Step 1
            var result = User.Login(username, password);

            // Step 2
            Assert.IsNull(result, "User logged despite of wrong username and password.");
        }

        /// <summary>
        /// Purpose: Verify that even though a user with a given username 
        /// exists, login will fail if the password is wrong.
        /// 
        /// Pre-conditions:
        ///     1. A user with a given username exists.
        /// 
        /// Steps:
        ///     1. Try to login with the username from pre-condition 1, 
        ///        but use another (not used) password.
        ///     2. Verify it is not possible.
        /// </summary>
        [TestMethod]
        public void LoginWithWrongPassword()
        {
            var username = TestUser.User.Username;
            var password = User.GenerateToken();

            // Step 1
            var result = User.Login(username, password);

            // Step 2
            Assert.IsNull(result, "User logged in, despite of wrong password.");
        }

        /// <summary>
        /// Purpose: Verify that even though a user with a given password 
        /// exists, login will fail if the username is wrong.
        /// 
        /// Pre-conditions:
        ///     1. A user with a given password exists.
        /// 
        /// Steps:
        ///     1. Try to login with the password from pre-condition 1,
        ///        but use another (non-existing) username.
        ///     2. Verify it is not possible.
        /// </summary>
        [TestMethod]
        public void LoginWithWrongUsername()
        {
            string username;
            string password = TestUser.User.Password;

            // Pre-condition
            do
            {
                username = User.GenerateToken();
            }
            while (User.All.Any(u => u.Username == username));

            // Step 1
            var result = User.Login(username, password);

            // Step 2
            Assert.IsNull(result, "User logged in, despite of wrong username.");
        }

        /// <summary>
        /// Purpose: Verify that even though a user with a given username 
        /// exists, and a user with a given password exists, login will 
        /// fail if those two users are not the same.
        /// 
        /// Pre-conditions:
        ///     1. A user with a given username exists (User 1).
        ///     2. Another user with a given password exists (User 2).
        /// 
        /// Steps:
        ///     1. Try to login with username from User 1 and 
        ///        password from User 2.
        ///     2. Verify it is not possible.
        /// </summary>
        [TestMethod]
        public void LoginWithDifferentUsernameAndPassword()
        {
            string username;
            string password;

            do
            {
                username = User.GenerateToken();
                password = User.GenerateToken();
            }
            while (User.All.Any(u => u.Username == username || u.Password == password));

            // Pre-condition 1
            var user1 = new User
            {
                Username = username,
                Password = "very" + password + "unique",
                Email = "Very@unique.com"
            };
            User.SignUp(user1);

            // Pre-condition 2
            var user2 = new User
            {
                Username = "very" + username + "unique",
                Password = password,
                Email = "Very@unique.com"
            };
            User.SignUp(user2);

            RentItContext.ReloadDb();

            // Step 1
            var result = User.Login(username, password);

            // Step 2
            Assert.IsNull(result, "User logged in, despite of using username and password from two different users.");
        }
    }
}
