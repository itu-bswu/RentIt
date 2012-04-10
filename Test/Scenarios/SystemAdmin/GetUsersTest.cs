// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetUsersTest.cs" company="RentIt">
//   Copyright (c) RentIt. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace RentIt.Tests.Scenarios.SystemAdmin
{
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RentIt.Tests.Utils;

    using RentItService;
    using RentItService.Entities;
    using RentItService.Enums;
    using RentItService.Exceptions;
    using RentItService.Services;

    /// <summary>
    /// Class for testing the GetUsers method
    /// </summary>
    [TestClass]
    public class GetUsersTest : DataTest
    {
        /// <summary>
        /// Purpose: Verify that a list of content publishers is returned
        /// and that the list contains only content publishers
        /// <para></para>
        /// Pre-condtions:
        ///     1. At least one user must exist in the database
        ///     2. An admin must exist in the database.
        /// <para></para>
        /// Steps:
        ///     1. Assert that pre-conditions hold.
        ///     2. Call GetUsers with the token from the admin.
        ///     3. Assert that the list holds the correct amount of user
        ///        and that the test content publisher is among them.
        /// </summary>
        [TestMethod]
        public void GetUsersValidTest()
        {
            var service = new Service();

            using (var db = new RentItContext())
            {
                var testAdmin = TestUser.SystemAdmin;
                var testUser = TestUser.User;

                var loggedinUser = User.Login(testAdmin.Username, testAdmin.Password);

                var amountOfUsers = Enumerable.Count(db.Users, user => user.Type == UserType.User);
                var userList = service.GetUsers(loggedinUser.Token);

                Assert.IsNotNull(userList);
                Assert.AreEqual(amountOfUsers, userList.Count(), "A 'wrong' number of users is returned");

                userList = service.GetUsers(loggedinUser.Token);

                Assert.IsNotNull(userList);
                Assert.AreEqual(testUser.ID, userList.First(u => u.FullName == "James Smith").ID, "The IDs doesn't match");
            }
        }

        /// <summary>
        /// Purpose: Verify that the method throws the correct exception
        /// when it's not an admin calling it.
        /// <para></para>
        /// Pre-condtions:
        ///     1. At least one normal user must exist in the database
        /// <para></para>
        /// Steps:
        ///     1. Assert that pre-conditions hold.
        ///     2. Login as the user.
        ///     3. Call GetUsers with the token from the user.
        ///     4. Assert that an InsufficientAccessLevelException is thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InsufficientRightsException))]
        public void GetUsersInvalidUserTypeTest()
        {
            var service = new Service();
            var testUser = TestUser.User;

            var loggedinUser = User.Login(testUser.Username, testUser.Password);

            service.GetUsers(loggedinUser.Token);
        }

        /// <summary>
        /// Purpose: Verify that the method throws the correct exception
        /// when it recieves an invalid token.
        /// <para></para>
        /// Pre-condtions:
        ///     1. The token used must not corrospond to a user.
        /// <para></para>
        /// Steps:
        ///     1. Assert that pre-conditions hold.
        ///     2. Call GetUsers with the token.
        ///     3. Assert that a UserNotFoundException is thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(UserNotFoundException))]
        public void GetUsersInvalidTokenTest()
        {
            const string Token = "vneriupahv894p3n8uv92iun";

            var service = new Service();

            service.GetUsers(Token);
        }
    }
}