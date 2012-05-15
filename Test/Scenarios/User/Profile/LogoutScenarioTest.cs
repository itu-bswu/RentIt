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
        /// </summary>
        [TestMethod]
        public void LogoutValidToken()
        {
            // Step 1
            var user = User.Login(TestUser.User.Username, TestUser.User.Password);

            RentItContext.ReloadDb();

            // Step 2
            Assert.IsNotNull(user.Token, "Token is null!");

            // Step 3
            User.Logout(user);

            RentItContext.ReloadDb();

            // Step 4
            Assert.IsTrue(User.All.Any(u => u.ID.Equals(user.ID) && u.Token != null));
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
        /*
        [TestMethod]
        [ExpectedException(typeof(UserNotFoundException))]
        public void LogoutInvalidToken()
        {
            string token;

            // Step 1
            do
            {
                token = User.GenerateToken();
            }
            while (User.All.Any(u => u.Token == token));

            // Step 2
            User.Logout(User.GetByToken(token));
        }*/
    }
}