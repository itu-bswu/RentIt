// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RentalHistoryScenarioTest.cs" company="Hewlett-Packard">
//   Copyright (c) RentIt. All rights reserved.
// </copyright>
// <summary>
//   
// </summary>
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

                Assert.AreEqual(result, user.Rentals, "The list no filled with the same elements");
            }
        }

        /// <summary>
        /// Purpose: Verify that you will get a empty list using user with no rental history.
        /// 
        /// Steps:
        ///     1: Create an instance of user and fill it with valid information.
        ///     2: Create an instance of movie and fill it with valid information.
        ///     3: Create an empty rental list to compare with.
        ///     4: Verify that list is null.
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
        ///     4: Verify that the list contains the same elements.
        /// </summary>
        [TestMethod]
        public void MultipleRentalHistory()
        {
            TestHelper.SetUpRentalTestUsers();

            using (var db = new RentItContext())
            {
                User user = db.Users.First(u => u.Username == "testUserRent1");

                var result = User.GetRentalHistory(user.Token);

                Assert.AreEqual(result, user.Rentals, "The rental list is wrong");
            }
        }

        /// <summary>
        /// Purpose: Verify that a contentProvider has no rental history.
        /// 
        /// Steps:
        ///     1: Create an instance of user and fill it with valid information.
        ///     2: Create an instance of movie and fill it with valid information.
        ///     3: Verify that the rental history is null.
        /// </summary>
        [TestMethod]
        public void ContentproviderRentalHistory()
        {
            using (var db = new RentItContext())
            {
                Assert.IsFalse(db.Users.Any(u => u.Username == "testContentProvider"), "contentprovider shouldn't have any rentals!");
            }
        }

        /// <summary>
        /// Purpose: Verify that a  admin has no rental history.
        /// 
        /// Steps:
        ///     1: Create an instance of user and fill it with valid information.
        ///     2: Create an instance of movie and fill it with valid information.
        ///     3: Verify that the rental history is null.
        /// </summary>
        [TestMethod]
        public void AdminRentalHistory()
        {
            using (var db = new RentItContext())
            {
                User user = db.Users.First(u => u.Username == "testAdmin");

                Assert.AreEqual(0, user.Rentals.Count, "Admin shouldn't have a rental history");
            }
        }
    }
}
