// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetMovieInformationTest.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the GetMovieInformationTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RentIt.Tests.Scenarios.GetMovieData
{
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

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
        /// <summary>Tests the GetMovieInformation with valid inputs</summary>
        [TestMethod]
        public void GetMovieInformationTest1()
        {
            Service service = new Service();

            using (var db = new RentItContext())
            {
                TestHelper.SetUpTestMovies();
                TestHelper.SetUpTestUsers();

                User testUser = db.Users.First(u => u.Username == "testUser");
                Movie testMovie = db.Movies.First(u => u.Title == "testMovie1");

                Movie foundMovie = service.GetMovieInformation(testUser.Token, testMovie.ID);

                Assert.AreEqual(testMovie.ID, foundMovie.ID);
                Assert.AreEqual(testMovie.Title, foundMovie.Title);
                Assert.AreEqual(testMovie.Description, foundMovie.Description);
                Assert.AreEqual(testMovie.Genre, foundMovie.Genre);
            }
        }

        /// <summary>
        /// Tests if an exception is thrown when an invalid token is given
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(UserNotFoundException))]
        public void InvalidTokenGetMovieInformationTest()
        {
            Service service = new Service();

            using (var db = new RentItContext())
            {
                TestHelper.SetUpTestMovies();
                TestHelper.SetUpTestUsers();

                Movie testMovie = db.Movies.First(u => u.Title == "testMovie1");

                Movie foundMovie = service.GetMovieInformation("Hello thar", testMovie.ID);
            }
        }

        /// <summary>
        /// Tests if null is returned with an invalid movieId or not
        /// </summary>
        [TestMethod]
        public void InvalidMovieIdGetMovieInformation()
        {
            Service service = new Service();

            using (var db = new RentItContext())
            {
                TestHelper.SetUpTestMovies();
                TestHelper.SetUpTestUsers();

                User testUser = db.Users.First(u => u.Username == "testUser");

                Movie foundMovie = service.GetMovieInformation(testUser.Token, 1337);

                Assert.IsNull(foundMovie);
            }
        }
    }
}
