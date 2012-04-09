// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetContentPublishersTest.cs" company="RentIt">
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
    /// Class for testing the GetContentProviders method
    /// </summary>
    [TestClass]
    public class GetContentPublishersTest : DataTest
    {
        /// <summary>
        /// Purpose: Verify that a list of content publishers is returned
        /// and that the list contains only content publishers
        /// <para></para>
        /// Pre-condtions:
        ///     1. At least one content publisher must exist in the database
        ///     2. An admin must exist in the database.
        /// <para></para>
        /// Steps:
        ///     1. Assert that pre-conditions hold.
        ///     2. Login as the admin.
        ///     2. Call GetContentPublishers with the token from the admin.
        ///     3. Assert that the list holds the correct amount of content
        ///        publishers and that the test content publisher is among
        ///        them.
        /// </summary>
        [TestMethod]
        public void GetContentProvidersValidTest()
        {
            var service = new Service();

            using (var db = new RentItContext())
            {
                var testAdmin = TestUser.SystemAdmin;
                var testProvider = TestUser.ContentProvider;

                User.Login(testAdmin.Username, testAdmin.Password);

                Assert.IsNotNull(testProvider);

                var amountOfPublishers = Enumerable.Count(db.Users, user => user.Type == UserType.ContentProvider);
                var publisherList = service.GetContentPublishers(testAdmin.Token);

                Assert.IsNotNull(publisherList);
                Assert.AreEqual(amountOfPublishers, publisherList.Count(), "A 'wrong' number of Content Publishers is returned");

                publisherList = service.GetContentPublishers(testAdmin.Token);

                Assert.IsNotNull(publisherList);
                Assert.AreEqual(testProvider.ID, publisherList.First(u => u.Username == "Universal Pictures").ID, "The IDs doesn't match");
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
        ///     2. Call GetContentPublishers with the token from the user.
        ///     3. Assert that an InsufficientRightsException is thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InsufficientRightsException))]
        public void GetContentPublishersInvalidUserTypeTest()
        {
            var service = new Service();
            var testUser = TestUser.User;

            User.Login(testUser.Username, testUser.Password);

            service.GetContentPublishers(testUser.Token);
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
        ///     2. Call GetContentPublishers with the token.
        ///     3. Assert that a UserNotFoundException is thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(UserNotFoundException))]
        public void GetContentPublishersInvalidTokenTest()
        {
            const string Token = "bneiuwnvu9p28h3ny84o28uyh43892";

            var service = new Service();
            
            service.GetContentPublishers(Token);
        }
    }
}