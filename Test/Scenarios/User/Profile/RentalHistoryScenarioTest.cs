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

            using (var db = new RentItContext())
            {
                User user = db.Users.First(u => u.Username == "testUserRent2");

                var result = User.GetRentalHistory(user.Token);

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

                var result = User.GetRentalHistory(user.Token);

                Assert.AreEqual(user.Rentals, null);
            }
        }

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

    }
}
