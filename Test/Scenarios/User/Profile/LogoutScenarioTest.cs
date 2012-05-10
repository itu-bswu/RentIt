//-------------------------------------------------------------------------------------------------
// <copyright file="LogoutScenarioTest.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentIt.Tests.Scenarios.User.Profile
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RentIt.Tests.Utils;

    using RentItService;
    using RentItService.Entities;
    using RentItService.Exceptions;

    /// <summary>
    /// Scenario tests for the logout feature.
    /// </summary>
    [TestClass]
    public class LogoutScenarioTest : DataTest
    {
        /// <summary>
        /// Purpose: Verify that it is possible to logout, 
        /// when specifying a valid token.
        /// 
        /// Steps:
        ///     1. Log in a user.
        ///     2. Verify token is set.
        ///     3. Log out the user from step 1.
        ///     4. Verify that the token has been cleared.
        ///     5. Verify that the token cannot be used anymore.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(UserNotFoundException))]
        public void LogoutValidToken()
        {
            // Step 1
            var user = User.Login(TestUser.User.Username, TestUser.User.Password);

            // Step 2
            string token = user.Token;
            Assert.IsNotNull(token, "Token is null!");

            // Step 3
            User.Logout(token);

            // Step 4
            using (var db = new RentItContext())
            {
                Assert.IsNull(db.Users.Find(user.ID).Token);
            }

            // Step 5
            User.Edit(token, user);
        }

        /// <summary>
        /// Purpose: Verify it is not possible to logout 
        /// using a token that isn't in use.
        /// 
        /// Steps:
        ///     1. Generate a token that is not in use.
        ///     2. Logout using that token.
        ///     3. Verify it is not possible.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(UserNotFoundException))]
        public void LogoutInvalidToken()
        {
            string token;

            // Step 1
            using (var db = new RentItContext())
            {
                do
                {
                    token = User.GenerateToken();
                }
                while (db.Users.Any(u => u.Token == token));
            }

            // Step 2
            User.Logout(token);
        }
    }
}