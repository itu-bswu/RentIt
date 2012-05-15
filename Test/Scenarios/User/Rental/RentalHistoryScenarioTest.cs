// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RentalHistoryScenarioTest.cs" company="Hewlett-Packard">
//   Copyright (c) RentIt. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace RentIt.Tests.Scenarios.User.Rental
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RentIt.Tests.Utils;
    using RentItService.Entities;

    /// <summary>
    /// Scenario tests of the rentalhistory feature.
    /// </summary>
    [TestClass]
    public class RentalHistoryScenarioTest : DataTest
    {
        /// <summary>
        /// Purpose: Verify that it is possible to retreive list of the user rental history.
        /// 
        /// Steps:
        ///     1: Rent a movie.
        ///     2: Verify that the number of elements in the users rental history is correct.
        /// </summary>
        [TestMethod]
        public void RentalHistoryTest()
        {
            var user = User.Login(TestUser.User);
            var movieEdition = Movie.All.First().Editions.First();

            // Step 1
            user.RentMovie(movieEdition);

            // Step 2
            var result = user.GetRentalHistory();
            var rentals = user.Rentals.ToList();
            Assert.AreEqual(rentals.Count, result.Count(), "The list is not filled with the same amount of elements");
        }

        /// <summary>
        /// Purpose: Verify that you will get a empty list from a user with no rental history.
        /// 
        /// Steps:
        ///     1: Get the test user instance.
        ///     2: Vertify that it has no rentals.
        /// </summary>
        [TestMethod]
        public void RentalHistoryNoRentals()
        {
            var user = User.Login(TestUser.User);

            Assert.AreEqual(0, user.GetRentalHistory().Count(), "The list is not empty");
        }

        /// <summary>
        /// Purpose: Verify that a user wtih serveral movies in rental history and
        ///          with multiple instance of the same movie will return the correct list.
        /// 
        /// Steps:
        ///     1. Login as a user.
        ///     2. Pick two movies for the test.
        ///     3. Make the user rent the two movies from step 2.
        ///     4. Verify that GetRentalHistory() contains the correct amount of elements.
        /// </summary>
        [TestMethod]
        public void MultipleRentalHistory()
        {
            // Step 1
            var user = User.Login(TestUser.User);

            // Step 2
            var movies = Movie.All.Take(2);

            // Step 3
            foreach (var movie in movies)
            {
                user.RentMovie(movie.Editions.First());
            }

            // Step 4
            var result = user.GetRentalHistory();
            var rentals = user.Rentals.ToList();
            Assert.AreEqual(rentals.Count, result.Count(), "The rental list dosn't contain the right amount of items.");
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
            var user = User.Login(TestUser.ContentProvider);

            Assert.AreEqual(0, user.GetRentalHistory().Count(), "Content Provider shouldn't have a rental history");
        }

        /// <summary>
        /// Purpose: Verify that a admin has no rental history.
        /// 
        /// Steps:
        ///     1: Login as a system admin.
        ///     3: Verify that the rental history is empty.
        /// </summary>
        [TestMethod]
        public void AdminRentalHistory()
        {
            var user = User.Login(TestUser.SystemAdmin);

            Assert.AreEqual(0, user.GetRentalHistory().Count(), "Admin shouldn't have a rental history");
        }
    }
}