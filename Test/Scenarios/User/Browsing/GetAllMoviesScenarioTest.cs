// -----------------------------------------------------------------------
// <copyright file="GetAllMoviesScenarioTest.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentIt.Tests.Scenarios.User.Browsing
{
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

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
        /// Purpose: Verify that GetAllMovies work as intended.
        /// <para>
        /// Pre-condtions:
        ///     1. A movie with the title "The Matrix" must exist in the database.
        ///     2. 
        /// </para>
        /// <para>
        /// Steps:
        ///     1. Make sure the pre-conditions hold.
        ///     2. Get a user from the database who can access the method.
        ///     3. Verify that the database values and the GetAllMovies method return the same result.
        ///     4. Add a movie to the database.
        ///     5. Verify that the database values and the GetAllMovies method return the same result.
        /// </para>
        /// </summary>
        [TestMethod]
        public void GetAllMovies()
        {
            using (var db = new RentItContext())
            {
                User testUser = db.Users.First(u => u.Username == "Smith");
                int numberOfMoviesFromDb = db.Movies.Count();
                int numberOfMoviesFromMethod = Movie.GetAllMovies(testUser.Token).Count();

                Assert.AreEqual(numberOfMoviesFromDb, numberOfMoviesFromMethod, "The database does not contain the same amount of movies as returned by the GetAllMovies method.");

                var allMovies = db.Movies;

                foreach (Movie m in Movie.GetAllMovies(testUser.Token))
                {
                    Assert.IsTrue(allMovies.Any(q => q.ID == m.ID), "The database does not contain a movie that is returned by the GetAllMovies method.");
                }

                var movie2 = new Movie
                    {
                        Description = "testMovieForGetAllMovies",
                        FilePath = "EmptyEmptyEmpty",
                        Genre = "testGenre",
                        ImagePath = "EmptyEmptyEmpty",
                        Title = "testMovieGAM"
                    };

                db.Movies.Add(movie2);
                db.SaveChanges();

                numberOfMoviesFromDb = db.Movies.Count();
                numberOfMoviesFromMethod = Movie.GetAllMovies(testUser.Token).Count();

                Assert.AreEqual(numberOfMoviesFromDb, numberOfMoviesFromMethod, "The database does not contain the same amount of movies as returned by the GetAllMovies method.");

                allMovies = db.Movies;

                foreach (Movie m in Movie.GetAllMovies(testUser.Token))
                {
                    Assert.IsTrue(allMovies.Any(q => q.ID == m.ID), "The database does not contain a movie that is returned by the GetAllMovies method.");
                }
            }
        }

        /// <summary>
        /// Purpose: Verify that it is not posible to get the movies if valid token is not provided.
        /// <para>
        /// Steps:
        ///     1. Call GetAllMovies with a bad token.
        ///     2. Verify exception is thrown.
        /// </para>
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(UserNotFoundException))]
        public void InvalidTokenGetAllMoviesTest()
        {
            Movie.GetAllMovies("badtokenwhichcannotbevalidated");
        }
    }
}