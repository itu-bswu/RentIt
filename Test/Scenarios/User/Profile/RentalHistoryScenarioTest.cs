// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RentalHistoryScenarioTest.cs" company="Hewlett-Packard">
//   Copyright (c) RentIt. All rights reserved.
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RentIt.Tests.Scenarios.User.Profile
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RentItService;
    using RentItService.Entities;

    /// <summary>
    /// Scenario tests of the rentalhistory feature.
    /// </summary>
    [TestClass]
    public class RentalHistoryScenarioTest
    {
        /// <summary>
        /// Purpose: Verify that it is possible to retreive list of the user rental history.
        /// 
        /// Steps:
        ///     1: Create an instance of user and fill it with valid information.
        ///     2: Create an instance of movie and fill it with valid information.
        ///     3: Create an instance of rental and fill it with valid information.
        ///     4: Verify that the element in the users rental history is correct.
        /// </summary>
        [TestMethod]
        public void RentalHistoryTest()
        {
            TestHelper.SetUpRentalTestUsers();
            TestHelper.SetUpMoviesForRentalTest();
            TestHelper.TestRentalsMostDownloaded();
            TestHelper.SetUpTestUsers();

            using (var db = new RentItContext())
            {
                User user = db.Users.First(u => u.Username == "testUserRent2");

                var result = User.GetRentalHistory(user.Token);

                Assert.AreEqual(user.Rentals, "batman");
            }
        }

        /// <summary>
        /// Purpose: Verify that you will get a empty list using user with no rental history.
        /// 
        /// Steps:
        ///     1: Create an instance of user and fill it with valid information.
        ///     2: Create an instance of movie and fill it with valid information.
        ///     3: Create an instance of rental and fill it with valid information.
        ///     4: Verify that list is null.
        /// </summary>
        [TestMethod]
        public void RentalHistoryNoRentals()
        {
            using (var db = new RentItContext())
            {
                User user = db.Users.First(u => u.Username == "testUserRent1");

                Assert.AreEqual(user.Rentals, null);
            }
        }

        /// <summary>
        /// Purpose: Verify that a user wtih serveral movies in rental history and
        ///          with multiple instance of the same movie will return the correct list.
        /// 
        /// Steps:
        ///     1: Create an instance of user and fill it with valid information.
        ///     2: Create an instance of movie and fill it with valid information.
        ///     3: Create an instance of rental and fill it with valid information.
        ///     4: Verify that the list contains the same elements.
        /// </summary>
        [TestMethod]
        public void MultipleRentalHistory()
        {
            using (var db = new RentItContext())
            {
                User user = db.Users.First(u => u.Username == "testUserRent3");

                var result = User.GetRentalHistory(user.Token);

                Assert.AreEqual(user.Rentals, result);
            }
        }

        /// <summary>
        /// Purpose: Verify that a contentProvider has no rental history.
        /// 
        /// Steps:
        ///     1: Create an instance of user and fill it with valid information.
        ///     2: Create an instance of movie and fill it with valid information.
        ///     3: Create an instance of rental and fill it with valid information.
        ///     4: Verify that the rental history is null.
        /// </summary>
        [TestMethod]
        public void ContentproviderRentalHistory()
        {
            using (var db = new RentItContext())
            {
                User user = db.Users.First(u => u.Username == "testContentProvider");

                Assert.AreEqual(user.Rentals, null);
            }
        }

        /// <summary>
        /// Purpose: Verify that a  admin has no rental history.
        /// 
        /// Steps:
        ///     1: Create an instance of user and fill it with valid information.
        ///     2: Create an instance of movie and fill it with valid information.
        ///     3: Create an instance of rental and fill it with valid information.
        ///     4: Verify that the rental history is null.
        /// </summary>
        [TestMethod]
        public void AdminRentalHistory()
        {
            using (var db = new RentItContext())
            {
                User user = db.Users.First(u => u.Username == "testAdmin");

                Assert.AreEqual(user.Rentals, null);
            }
        }

    }
}
