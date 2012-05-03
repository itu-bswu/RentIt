//-------------------------------------------------------------------------------------------------
// <copyright file="RentMovieScenarioTest.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentIt.Tests.Scenarios.User.Rental
{
    using System;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RentIt.Tests.Utils;

    using RentItService;
    using RentItService.Entities;
    using RentItService.Exceptions;

    /// <summary>
    /// Scenario test for the Rent Movie functionality.
    /// </summary>
    [TestClass]
    public class RentMovieScenarioTest : DataTest
    {
        /// <summary>
        /// Purpose: Verify that it is possible to rent a movie.
        /// <para>
        /// Pre-condtions:
        ///     1. A user called "Smith" exists in the database.
        ///     2. A movie called "The Matrix" exists in the database.
        /// </para>
        /// <para>
        /// Steps:
        ///     1. Make sure the user and movie exists in the database.
        ///     2. Make sure the rental does not already exist.
        ///     3. Rent movie.
        ///     4. Make sure the new rental is in database.
        /// </para>
        /// </summary>
        [TestMethod]
        public void RentMovieTest()
        {
            // Arrange
            string testToken;
            int testID;
            int testTokenID;

            using (var db = new RentItContext())
            {
                var user = User.Login(TestUser.User.Username, TestUser.User.Password);
                var movie = db.Movies.First(m => m.Title.Equals("The Matrix"));

                testToken = user.Token;
                testID = movie.ID;
                testTokenID = User.GetByToken(testToken).ID;
                Assert.IsFalse(db.Rentals.Any(r => r.UserID == testTokenID & r.MovieID == testID), "Rental exists before call of RentMovie.");
            }

            // Act
            User.RentMovie(testToken, testID);

            // Assert
            using (var db = new RentItContext())
            {
                Assert.IsTrue(db.Rentals.Any(r => r.UserID == testTokenID & r.MovieID == testID), "The rental was not created.");
            }
        }

        /// <summary>
        /// Purpose: Verify that only users can rent movies.
        /// <para>
        /// Pre-condtions:
        ///     1. A user with user name "Universal" must exist in the database.
        ///     2. A movie with the title "The Matrix" must exist in the database.
        /// </para>
        /// <para>
        /// Steps:
        ///     1. Make sure the movie and the user exist in the database.
        ///     2. Attempt to rent the movie.
        ///     3. Catch the expected NotAUserException.
        /// </para>
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotAUserException))]
        public void NotAUserRentMovieTest()
        {
            using (var db = new RentItContext())
            {
                var user = User.Login(TestUser.ContentProvider.Username, TestUser.ContentProvider.Password);
                var movie = db.Movies.First(m => m.Title.Equals("The Matrix"));

                var testToken = user.Token;
                var testID = movie.ID;

                User.RentMovie(testToken, testID);
            }
        }

        /// <summary>
        /// Purpose: Verify that null values are not valid.
        /// <para>
        /// Pre-condtions:
        ///     1. A movie with the title "The Matrix" must exist in the database.
        /// </para>
        /// <para>
        /// Steps:
        ///     1. Make sure the database contains the required movie.
        ///     2. Attempt to rent movie with a null user.
        ///     3. Catch argument null exception.
        /// </para>
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InvalidInputRentMovieTest()
        {
            using (var db = new RentItContext())
            {
                var movie = db.Movies.First(m => m.Title.Equals("The Matrix"));

                int testID = movie.ID;

                User.RentMovie(null, testID);
            }
        }

        /// <summary>
        /// Purpose: Verify it is not possible to rent a movie 
        /// with a release date in the future.
        /// 
        /// Steps:
        ///     1. Create a movie with a release date in the future.
        ///     2. Log in as a user.
        ///     3. Try to rent the movie from step 1 with user from step 2.
        ///     4. Verify it is not possible.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NoMovieFoundException))]
        public void RentalOfMovieWithFutureRelease()
        {
            Movie movie;

            // Step 1
            using (var db = new RentItContext())
            {
                movie = new Movie
                {
                    Title = "Some unique movie title",
                    FilePath = "Not currently available",
                    OwnerID = TestUser.ContentProvider.ID,
                    Released = DateTime.Now.AddDays(14)
                };

                db.Movies.Add(movie);
                db.SaveChanges();
            }

            // Step 2
            var user = User.Login(TestUser.User.Username, TestUser.User.Password);

            // Step 3
            User.RentMovie(user.Token, movie.ID);
        }

        /// <summary>
        /// Purpose: Verify it is not possible to rent a movie 
        /// without a release date.
        /// 
        /// Steps:
        ///     1. Create a movie without a release date.
        ///     2. Log in as a user.
        ///     3. Try to rent the movie from step 1 with user from step 2.
        ///     4. Verify it is not possible.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NoMovieFoundException))]
        public void RentalOfMovieWithoutReleaseDate()
        {
            Movie movie;

            // Step 1
            using (var db = new RentItContext())
            {
                movie = new Movie
                {
                    Title = "Some unique movie title",
                    FilePath = "Not currently available",
                    OwnerID = TestUser.ContentProvider.ID
                };

                db.Movies.Add(movie);
                db.SaveChanges();
            }

            // Step 2
            var user = User.Login(TestUser.User.Username, TestUser.User.Password);

            // Step 3
            User.RentMovie(user.Token, movie.ID);
        }
    }
}