namespace RentIt.Tests.Scenarios
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RentItService;
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

            Service service = new Service();

            string testGenre = "testGenre";

            using (var db = new RentItContext())
            {
                User user = db.Users.First(u => u.Username == "testUser");

                IEnumerable<Movie> movies = service.GetMoviesByGenre(user.Token, testGenre);

                Assert.IsTrue(movies.Count() == 1 && movies.Single().Genre == testGenre);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(UnknownGenreException))]
        public void BrowseUnknownGenreTest()
        {
            TestHelper.SetUpTestUsers();

            Service service = new Service();

            using (var db = new RentItContext())
            {
                User user = db.Users.First(u => u.Username == "testUser");

                Assert.IsTrue(db.Movies.Count() == 0);
                service.GetMoviesByGenre(user.Token, "testGenre");
            }
        }
    }
}
