// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RentalHistoryScenarioTest.cs" company="Hewlett-Packard">
//   Copyright (c) RentIt. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace RentIt.Tests.Scenarios.User.Rental
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
        ///     4: Verify that the number of elements in the users rental history is correct.
        /// </summary>
        [TestMethod]
        public void RentalHistoryTest()
        {
            TestHelper.SetUpRentalTestUsers();
            TestHelper.SetUpMoviesForRentalTest();
            TestHelper.TestRentalsMostDownloaded();

            using (var db = new RentItContext())
            {
                User user = db.Users.First(u => u.Username == "testUserRent2");

                var result = User.GetRentalHistory(user.Token).ToList();

                var rentals = user.Rentals.ToList();

                Assert.AreEqual(result.Count, rentals.Count, "The list is not filled with the same amount of elements");
            }
        }

        /// <summary>
        /// Purpose: Verify that you will get a empty list from a user with no rental history.
        /// 
        /// Steps:
        ///     1: Create an instance of user and fill it with valid information.
        ///     2: Create an instance of movie and fill it with valid information.
        ///     4: Verify that the list is empty.
        /// </summary>
        [TestMethod]
        public void RentalHistoryNoRentals()
        {
            TestHelper.SetUpRentalTestUsers();

            using (var db = new RentItContext())
            {
                User user = db.Users.First(u => u.Username == "testUserRent1");

                Assert.AreEqual(0, user.Rentals.Count, "The list is not empty");
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
        ///     4: Verify that the list contains the same amount of elements.
        /// </summary>
        [TestMethod]
        public void MultipleRentalHistory()
        {
            TestHelper.SetUpRentalTestUsers();

            using (var db = new RentItContext())
            {
                User user = db.Users.First(u => u.Username == "testUserRent3");

                var result = User.GetRentalHistory(user.Token).ToList();

                var rentals = user.Rentals.ToList();

                Assert.AreEqual(result.Count, rentals.Count, "The rental list dosn't contain the right amount of items.");
            }
        }

        /// <summary>
        /// Purpose: Verify that a contentProvider has no rental history.
        /// 
        /// Steps:
        ///     1: Create an instance of user and fill it with valid information.
        ///     2: Create an instance of movie and fill it with valid information.
        ///     3: Verify that the rental history is empty.
        /// </summary>
        [TestMethod]
        public void ContentproviderRentalHistory()
        {
            using (var db = new RentItContext())
            {
                User user = db.Users.First(u => u.Username == "Universal");

                Assert.AreEqual(0, user.Rentals.Count, "Content Provider shouldn't have a rental history");
            }
        }

        /// <summary>
        /// Purpose: Verify that a  admin has no rental history.
        /// 
        /// Steps:
        ///     1: Create an instance of user and fill it with valid information.
        ///     2: Create an instance of movie and fill it with valid information.
        ///     3: Verify that the rental history is empty.
        /// </summary>
        [TestMethod]
        public void AdminRentalHistory()
        {
            using (var db = new RentItContext())
            {
                User user = db.Users.First(u => u.Username == "Anderson");

                Assert.AreEqual(0, user.Rentals.Count, "Admin shouldn't have a rental history");
            }
        }
    }
}
