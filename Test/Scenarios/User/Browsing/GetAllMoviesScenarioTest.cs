// -----------------------------------------------------------------------
// <copyright file="GetAllMoviesScenarioTest.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace RentIt.Tests.Scenarios.User.Browsing
{
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RentIt.Tests.Utils;

    using RentItService;
    using RentItService.Entities;
    using RentItService.Exceptions;

    /// <summary>
    /// Scenario test for the "View All Movies" feature.
    /// </summary>
    [TestClass]
    public class GetAllMoviesScenarioTest : DataTest
    {
        /// <summary>
        /// Purpose: Verify that All work as intended.
        /// <para>
        /// Pre-condtions:
        ///     1. A movie with the title "The Matrix" must exist in the database.
        ///     2. 
        /// </para>
        /// <para>
        /// Steps:
        ///     1. Make sure the pre-conditions hold.
        ///     2. Get a user from the database who can access the method.
        ///     3. Verify that the database values and the All method return the same result.
        ///     4. Add a movie to the database.
        ///     5. Verify that the database values and the All method return the same result.
        /// </para>
        /// </summary>
        [TestMethod]
        public void GetAllMovies()
        {
            using (var db = new RentItContext())
            {
                var testUser = User.Login(TestUser.User.Username, TestUser.User.Password);
                var numberOfMoviesFromDb = db.Movies.Count();
                var numberOfMoviesFromMethod = Movie.All(testUser.Token).Count();

                Assert.AreEqual(numberOfMoviesFromDb, numberOfMoviesFromMethod, "The database does not contain the same amount of movies as returned by the All method.");

                var allMovies = db.Movies;

                foreach (var m in Movie.All(testUser.Token))
                {
                    Assert.IsTrue(allMovies.Any(q => q.ID == m.ID), "The database does not contain a movie that is returned by the All method.");
                }

                var movie2 = new Movie
                    {
                        Description = "testMovieForGetAllMovies",
                        ImagePath = "EmptyEmptyEmpty",
                        Title = "testMovieGAM",
                        OwnerID = 2
                    };

                db.Movies.Add(movie2);
                db.SaveChanges();

                numberOfMoviesFromDb = db.Movies.Count();
                numberOfMoviesFromMethod = Movie.All(testUser.Token).Count();

                Assert.AreEqual(numberOfMoviesFromDb, numberOfMoviesFromMethod, "The database does not contain the same amount of movies as returned by the All method.");

                allMovies = db.Movies;

                foreach (var m in Movie.All(testUser.Token))
                {
                    Assert.IsTrue(allMovies.Any(q => q.ID == m.ID), "The database does not contain a movie that is returned by the All method.");
                }
            }
        }

        /// <summary>
        /// Purpose: Verify that it is not posible to get the movies if valid token is not provided.
        /// <para>
        /// Steps:
        ///     1. Call All with a bad token.
        ///     2. Verify exception is thrown.
        /// </para>
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(UserNotFoundException))]
        public void InvalidTokenGetAllMoviesTest()
        {
            Movie.All("badtokenwhichcannotbevalidated");
        }
    }
}