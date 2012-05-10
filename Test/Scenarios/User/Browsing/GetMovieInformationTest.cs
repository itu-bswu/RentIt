// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetMovieInformationTest.cs" company="RentIt">
//   Copyright (c) RentIt. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace RentIt.Tests.Scenarios.User.Browsing
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RentIt.Tests.Utils;
    using RentItService;
    using RentItService.Entities;
    using RentItService.Exceptions;
    using RentItService.Services;

    /// <summary>
    /// Class for testing the GetMovieInformation method
    /// </summary>
    [TestClass]
    public class GetMovieInformationTest : DataTest
    {
        /// <summary>
        /// Purpose: Verify that the method returns the correct data.
        /// <para></para>
        /// Pre-condtions:
        ///     1. At least one user must exist in the database.
        ///     2. A movie must exist in the database.
        /// <para></para>
        /// Steps:
        ///     1. Assert that pre-conditions hold.
        ///     2. Login in with the user
        ///     3. Find a movie in the database.
        ///     4. Call GetMovieInformation with the token from the user and
        ///        the ID from the movie found.
        ///     5. Assert that the data returned is the same as the data from
        ///        testMovie1.
        /// </summary>
        [TestMethod]
        public void GetMovieInformationValidTest()
        {
            var service = new Service();

            using (var db = new RentItContext())
            {
                var testUser = TestUser.User;
                var testMovie = db.Movies.First();
                var loggedinUser = User.Login(testUser.Username, testUser.Password);

                var foundMovie = service.GetMovieInformation(loggedinUser.Token, testMovie.ID);

                Assert.AreEqual(testMovie.ID, foundMovie.ID, "The IDs doesn't match");
                Assert.AreEqual(testMovie.Title, foundMovie.Title, "The title doesn't match");
                Assert.AreEqual(testMovie.Description, foundMovie.Description, "The description doesn't match");

                Assert.AreEqual(testMovie.Genres.Count(), foundMovie.Genres.Count(), "The amount of genres doesn't match");

                foreach (var genre in testMovie.Genres)
                {
                    Assert.IsTrue(foundMovie.Genres.Contains(genre), "The genres doesn't match");
                }
            }
        }

        /// <summary>
        /// Purpose: Verify that the method throws the correct exception
        /// when calling it with an invalid token.
        /// <para></para>
        /// Pre-condtions:
        ///     1. A movie must exist in the databse
        /// <para></para>
        /// Steps:
        ///     1. Assert that pre-conditions hold.
        ///     2. Find a movie in the database.
        ///     3. Call GetMovieInformation with the test token and
        ///        the ID from the found movie.
        ///     4. Assert that a UserNotFoundException is thrown
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(UserNotFoundException))]
        public void GetMovieInformationInvalidTokenTest()
        {
            var service = new Service();

            string testToken = "Hello thar";

            using (var db = new RentItContext())
            {
                var testMovie = db.Movies.First();

                service.GetMovieInformation(testToken, testMovie.ID);
            }
        }

        /// <summary>
        /// Purpose: Verify that the method returns null when called with
        /// a movie ID that doesn't corrospond to a movie in the database
        /// <para></para>
        /// Pre-condtions:
        ///     1. A user must exist in the databse.
        ///     2. The test ID must not corrospond to a a movie in the
        ///        databse.
        /// <para></para>
        /// Steps:
        ///     1. Assert that pre-conditions hold.
        ///     2. Login with the user.
        ///     3. Call GetMovieInformation with the user token and
        ///        the test ID.
        ///     4. Assert that the result is null.
        /// </summary>
        [TestMethod]
        public void GetMovieInformationInvalidMovieIdTest()
        {
            var service = new Service();

            int testID = 178915368;

            using (var db = new RentItContext())
            {
                var testUser = TestUser.User;
                var loggedinUser = User.Login(testUser.Username, testUser.Password);

                Movie foundMovie = service.GetMovieInformation(loggedinUser.Token, testID);

                Assert.IsNull(foundMovie);
            }
        }

        /// <summary>
        /// Purpose: Verify that even though editions have been added 
        ///          to a movie, they will not appear / be passed to 
        ///          the clients, if the movie is not released.
        /// 
        /// Pre-condition:
        ///     1. A movie with a release date in the future exists in the database.
        ///     2. Movie from pre-condition 1 has one or more editions.
        /// 
        /// Steps:
        ///     1. Get movie information about movie from pre-condition 1.
        ///     2. Verify that no editions appears.
        /// </summary>
        [TestMethod]
        public void GetUnreleasedMovieInfoFutureRelease()
        {
            int movieId;
            var title = "Some movie";
            var desc = "Movie for testing purposes";
            var releaseDate = DateTime.Now.AddDays(14);

            var user = User.Login(TestUser.ContentProvider.Username, TestUser.ContentProvider.Password);

            // Pre-condition 1
            Movie.Register(user.Token, new Movie
            {
                Title = title,
                Description = desc,
                OwnerID = user.ID,
                ReleaseDate = releaseDate
            });

            // Pre-condition 2
            using (var db = new RentItContext())
            {
                var movie = Movie
                    .All()
                    .OrderByDescending(m => m.ID)
                    .First();

                Assert.AreEqual(title, movie.Title, "Wrong movie found.");
                Assert.AreEqual(desc, movie.Description, "Wrong movie found.");
                movieId = movie.ID;

                movie.Editions.Add(new Edition
                {
                    Name = "Super Hi-Res Retina Edition",
                    FilePath = "Does/Not/Exist.avi"
                });

                db.SaveChanges();
            }

            // Step 1
            var movieInfo = Movie.Get(movieId);

            // Step 2
            Assert.IsFalse(movieInfo.Editions.Any(), "Movie editions passed to client, even though movie is not released.");
        }

        /// <summary>
        /// Purpose: Verify that even though editions have been added 
        ///          to a movie, they will not appear / be passed to 
        ///          the clients, if the movie is not released.
        /// 
        /// Pre-condition:
        ///     1. A movie with no release date exists in the database.
        ///     2. Movie from pre-condition 1 has one or more editions.
        /// 
        /// Steps:
        ///     1. Get movie information about movie from pre-condition 1.
        ///     2. Verify that no editions appears.
        /// </summary>
        [TestMethod]
        public void GetUnreleasedMovieInfoNoReleaseDate()
        {
            int movieId;
            var title = "Some movie";
            var desc = "Movie for testing purposes";

            var user = User.Login(TestUser.ContentProvider.Username, TestUser.ContentProvider.Password);

            // Pre-condition 1
            Movie.Register(user.Token, new Movie
            {
                Title = title,
                Description = desc,
                OwnerID = user.ID
            });

            // Pre-condition 2
            using (var db = new RentItContext())
            {
                var movie = Movie
                    .All()
                    .OrderByDescending(m => m.ID)
                    .First();

                Assert.AreEqual(title, movie.Title, "Wrong movie found.");
                Assert.AreEqual(desc, movie.Description, "Wrong movie found.");
                movieId = movie.ID;

                movie.Editions.Add(new Edition
                {
                    Name = "Super Hi-Res Retina Edition",
                    FilePath = "Does/Not/Exist.avi"
                });

                db.SaveChanges();
            }

            // Step 1
            var movieInfo = Movie.Get(movieId);

            // Step 2
            Assert.IsFalse(movieInfo.Editions.Any(), "Movie editions passed to client, even though movie is not released.");
        }
    }
}