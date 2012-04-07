namespace RentIt.Tests.Scenarios
{
    using System;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RentItService;
    using RentItService.Services;

    [TestClass]
    public class SearchScenarioTest : DataTest
    {
        /// <summary>
        /// Searches for a movie with the exact name as the query
        /// </summary>
        [TestMethod]
        public void SearchExcactTitle()
        {
            TestHelper.SetUpTestUsers();
            TestHelper.SetUpTestMovies();

            var service = new Service();

            using (var db = new RentItContext())
            {
                var user = db.Users.First(u => u.Username == "testUser");
                var movies = service.Search(user.Token, "testMovie1");

                Assert.IsTrue(movies.Single().Title.Equals("testMovie1"));
            }
        }

        /// <summary>
        /// Searches for a movie with a part of the name as the query
        /// </summary>
        [TestMethod]
        public void SearchPartlyTitle()
        {
            TestHelper.SetUpTestUsers();
            TestHelper.SetUpTestMovies();

            var service = new Service();

            using (var db = new RentItContext())
            {
                var user = db.Users.First(u => u.Username == "testUser");
                var movies = service.Search(user.Token, "Movie");

                Assert.IsTrue(movies.Single().Title.Equals("testMovie1"));
            }
        }

        /// <summary>
        /// Searches for a movie with the name but with inverted upper/lower case.
        /// </summary>
        [TestMethod]
        public void SearchDifferenceCase()
        {
            TestHelper.SetUpTestUsers();
            TestHelper.SetUpTestMovies();

            var service = new Service();

            using (var db = new RentItContext())
            {
                var user = db.Users.First(u => u.Username == "testUser");
                var movies = service.Search(user.Token, "TESTmOVIE1");

                Assert.IsTrue(movies.Single().Title.Equals("testMovie1"));
            }
        }

        /// <summary>
        /// Searches for a movie that doesn't exists
        /// </summary>
        [TestMethod]
        public void SearchWithoutResult()
        {
            TestHelper.SetUpTestUsers();

            var service = new Service();

            using (var db = new RentItContext())
            {
                var user = db.Users.First(u => u.Username == "testUser");
                var movies = service.Search(user.Token, "movie");

                Assert.IsFalse(movies.Any());
            }
        }
    }
}
