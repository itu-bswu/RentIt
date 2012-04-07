namespace RentIt.Tests.Scenarios
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RentItService;
    using RentItService.Entities;
    using RentItService.Exceptions;
    using RentItService.Services;

    [TestClass]
    public class SearchScenarioTest : DataTest
    {
        [TestMethod]
        public void SearchExcactTitle()
        {
            TestHelper.SetUpTestUsers();
            TestHelper.SetUpTestMovies();

            var service = new Service();

            using (var db = new RentItContext())
            {
                User user = db.Users.First(u => u.Username == "testUser");

                var movies = service.Search(user.Token, "testMovie1");

                Assert.IsTrue(movies.Single().Title.Equals("testMovie1"));
            }
        }

        [TestMethod]
        public void SearchPartlyTitle()
        {
            TestHelper.SetUpTestUsers();
            TestHelper.SetUpTestMovies();

            var service = new Service();

            using (var db = new RentItContext())
            {
                User user = db.Users.First(u => u.Username == "testUser");

                var movies = service.Search(user.Token, "Movie");

                Assert.IsTrue(movies.Single().Title.Equals("testMovie1"));
            }
        }
    }
}
