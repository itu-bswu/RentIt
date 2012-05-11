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
    using RentItService;
    using RentItService.Entities;
    using RentItService.Exceptions;
    using Utils;

    /// <summary>
    /// Scenario test for the Rent Movie functionality.
    /// </summary>
    [TestClass]
    public class RentMovieScenarioTest : DataTest
    {
        /// <summary>
        /// Purpose: Verify that it is possible to rent a movie.
        /// 
        /// Steps:
        ///     1. Log in with a test user.
        ///     2. Rent movie.
        ///     3. Make sure the new rental is in the database.
        /// </summary>
        [TestMethod]
        public void RentMovieTest()
        {
            // Arrange
            var user = User.Login(TestUser.User.Username, TestUser.User.Password);
            Movie movie;
            Edition edition;

            using (var db = new RentItContext())
            {
                movie = db.Movies.Include("Editions").First(m => m.Editions.Count >= 1);
                edition = movie.Editions.First();

                Assert.IsFalse(db.Rentals.Any(r => r.UserID == user.ID & r.EditionID == edition.ID), "Rental exists before call of RentMovie.");
            }

            // Act
            User.RentMovie(user.Token, edition.ID);

            // Assert
            using (var db = new RentItContext())
            {
                Assert.IsTrue(db.Rentals.Any(r => r.UserID == user.ID & r.EditionID == edition.ID), "The rental was not created.");
            }

            movie = Movie.Get(user, movie.ID);
            Assert.IsTrue(movie.Rentals.Any(), "No rentals found for the movie.");

            user = User.GetByToken(user.Token);
            Assert.IsTrue(user.Rentals.Any(), "No rentals found for the user.");
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
            var user = User.Login(TestUser.ContentProvider.Username, TestUser.ContentProvider.Password);
            var movie = Movie.All().First(m => m.Editions.Count > 0);

            User.RentMovie(user.Token, movie.Editions.First().ID);
        }

        /// <summary>
        /// Purpose: Verify that null values are not valid.
        /// 
        /// Pre-condtions:
        ///     1. A movie with the title "The Matrix" must exist in the database.
        /// 
        /// Steps:
        ///     1. Make sure the database contains the required movie.
        ///     2. Attempt to rent movie with a null user.
        ///     3. Catch argument null exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InvalidInputRentMovieTest()
        {
            using (var db = new RentItContext())
            {
                var movie = db.Movies.Include("Editions").First(m => m.Editions.Count > 0);

                User.RentMovie(null, movie.Editions.First().ID);
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
                    OwnerID = TestUser.ContentProvider.ID,
                    ReleaseDate = DateTime.Now.AddDays(14)
                };
                db.Movies.Add(movie);

                movie.Editions.Add(new Edition
                {
                    Name = "HD 1080p Ultra Hi-Res Retina edition",
                    FilePath = "This/Doesnt/Exist.exe"
                });

                db.SaveChanges();
            }

            // Step 2
            var user = User.Login(TestUser.User.Username, TestUser.User.Password);

            // Step 3
            User.RentMovie(user.Token, movie.Editions.First().ID);
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
                    OwnerID = TestUser.ContentProvider.ID
                };
                db.Movies.Add(movie);

                movie.Editions.Add(new Edition
                {
                    Name = "HD 1080p Ultra Hi-Res Retina edition",
                    FilePath = "This/Doesnt/Exist.exe"
                });

                db.SaveChanges();
            }

            // Step 2
            var user = User.Login(TestUser.User.Username, TestUser.User.Password);

            // Step 3
            User.RentMovie(user.Token, movie.Editions.First().ID);
        }
    }
}