namespace RentIt.Tests.Scenarios
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RentItService;
    using RentItService.Entities;
    using RentItService.Exceptions;

    [TestClass]
    public class BrowseByGenreScenario : DataTest
    {
        [TestMethod]
        public void BrowseKnownGenreTest()
        {
            TestHelper.SetUpTestUsers();
            TestHelper.SetUpTestMovies();
            const string testGenre = "testGenre";

            using (var db = new RentItContext())
            {
                var user = db.Users.First(u => u.Username == "testUser");
                var movies = Movie.ByGenre(user.Token, testGenre);

                Assert.IsTrue(movies.Single().Genre == testGenre);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(UnknownGenreException))]
        public void BrowseUnknownGenreTest()
        {
            TestHelper.SetUpTestUsers();

            using (var db = new RentItContext())
            {
                var user = db.Users.First(u => u.Username == "testUser");

                Assert.IsTrue(db.Movies.Single() != null);
                Movie.ByGenre(user.Token, "testGenre");
            }
        }
    }
}
