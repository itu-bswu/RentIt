namespace RentIt.Tests.Scenarios
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RentItService;
    using RentItService.Entities;
    using RentItService.Services;
    using RentItService.Exceptions;

    [TestClass]
    public class BrowseByGenreScenario
    {
        [TestMethod]
        public void BrowseKnownGenreTest()
        {
            TestHelper.SetUpTestUsers();
            TestHelper.SetUpTestMovies();

            var service = new Service();
            const string testGenre = "testGenre";

            using (var db = new RentItContext())
            {
                var user = db.Users.First(u => u.Username == "testUser");
                var movies = service.GetMoviesByGenre(user.Token, testGenre);

                Assert.IsTrue(movies.Single().Genre == testGenre);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(UnknownGenreException))]
        public void BrowseUnknownGenreTest()
        {
            TestHelper.SetUpTestUsers();

            var service = new Service();

            using (var db = new RentItContext())
            {
                var user = db.Users.First(u => u.Username == "testUser");

                Assert.IsTrue(db.Movies.Single() != null);
                service.GetMoviesByGenre(user.Token, "testGenre");
            }
        }
    }
}
