// -----------------------------------------------------------------------
// <copyright file="GetAllMoviesScenarioTest.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace RentIt.Tests.Scenarios.User.Browsing
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RentItService;
    using RentItService.Entities;

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
        ///     2. Verify that the database values and the All method return the same result.
        ///     3. Add a movie to the database.
        ///     4. Verify that the database values and the All method return the same result.
        /// </para>
        /// </summary>
        [TestMethod]
        public void GetAllMovies()
        {
            var numberOfMoviesFromDb = RentItContext.Db.Movies.Count();
            var numberOfMoviesFromMethod = Movie.All().Count();

            Assert.AreEqual(numberOfMoviesFromDb, numberOfMoviesFromMethod, "The database does not contain the same amount of movies as returned by the All method.");

            var allMovies = RentItContext.Db.Movies;

            foreach (var m in Movie.All())
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

            RentItContext.Db.Movies.Add(movie2);
            RentItContext.Db.SaveChanges();
            RentItContext.ReloadDb();

            numberOfMoviesFromDb = RentItContext.Db.Movies.Count();
            numberOfMoviesFromMethod = Movie.All().Count();

            Assert.AreEqual(numberOfMoviesFromDb, numberOfMoviesFromMethod, "The database does not contain the same amount of movies as returned by the All method.");

            allMovies = RentItContext.Db.Movies;

            foreach (var m in Movie.All())
            {
                Assert.IsTrue(allMovies.Any(q => q.ID == m.ID), "The database does not contain a movie that is returned by the All method.");
            }
        }
    }
}