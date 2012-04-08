// -----------------------------------------------------------------------
// <copyright file="UserRentalScenarioTest.cs" company="Hewlett-Packard">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace RentIt.Tests.Scenarios.User.Profile
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RentItService;
    using RentItService.Entities;

    /// <summary>
    /// Scenario tests of the rentalhistory feature
    /// </summary>
    [TestClass]
    public class RentalHistoryScenarioTest
    {
        /// <summary>
        /// Tests a successful retreive of the user rental history
        /// </summary>
        [TestMethod]
        public void RentalHistoryTest()
        {
            TestHelper.SetUpRentalTestUsers();
            TestHelper.SetUpMoviesForRentalTest();
            TestHelper.SetUpTestRentals();
            TestHelper.SetUpTestUsers();

            using (var db = new RentItContext())
            {
                User user = db.Users.First(u => u.Username == "testUserRent2");

                Assert.AreEqual(user.Rentals, "batman");
            }
        }

        /// <summary>
        /// Tests a user with no rental history
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
        /// Tests a user wtih serveral movies in rental history,
        /// with multiple instance of the same movie
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
        /// Tests that a contentProvider has no rental history
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
        /// Tests that a admin has no rental history
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
