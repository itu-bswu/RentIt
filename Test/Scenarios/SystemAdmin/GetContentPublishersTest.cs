// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetContentPublishersTest.cs" company="RentIt">
//   Copyright (c) RentIt. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace RentIt.Tests.Scenarios.UserInformationService
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

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
        ///     2. Call GetContentPublishers with the token from the admin.
        ///     3. Assert that the list holds the correct amount of content
        ///        publishers and that the test content publisher is among
        ///        them.
        /// </summary>
        [TestMethod]
        public void GetContentProvidersTest1()
        {
            Service service = new Service();

            using (var db = new RentItContext())
            {
                TestHelper.SetUpTestUsers();

                User testAdmin = db.Users.First(u => u.Username == "testAdmin");
                User testProvider = db.Users.First(u => u.Username == "testContentProvider");

                Assert.IsNotNull(testProvider);

                var amountOfPublishers = Enumerable.Count(db.Users, user => user.Type == UserType.ContentProvider);
                var publisherList = service.GetContentPublishers(testAdmin.Token);

                Assert.IsNotNull(publisherList);
                Assert.AreEqual(amountOfPublishers, publisherList.Count());

                publisherList = service.GetContentPublishers(testAdmin.Token);

                Assert.IsNotNull(publisherList);
                Assert.AreEqual(testProvider.ID, publisherList.First(u => u.Username == "testContentProvider").ID);
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
        ///     3. Assert that an InsufficientAccessLevelException is thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InsufficientAccessLevelException))]
        public void InvalidUserTypeTest()
        {
            Service service = new Service();

            using (var db = new RentItContext())
            {
                TestHelper.SetUpTestUsers();

                User testUser = db.Users.First(u => u.Username == "testUser");

                service.GetContentPublishers(testUser.Token);
            }
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
        public void InvalidTokenTest()
        {
            Service service = new Service();

            string token = "bneiuwnvu9p28h3ny84o28uyh43892";

// ReSharper disable UnusedVariable
            // Disabled because we've been told to use this
            // piece of code for the time being
            using (var db = new RentItContext())
// ReSharper restore UnusedVariable
            {
                TestHelper.SetUpTestUsers();

                service.GetContentPublishers(token);
            }
        }
    }
}
