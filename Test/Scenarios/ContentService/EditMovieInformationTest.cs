// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EditMovieInformationTest.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the EditMovieInformationTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RentIt.Tests.Scenarios.ContentService
{
    using System.Collections.ObjectModel;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RentItService;
    using RentItService.Entities;
    using RentItService.Exceptions;
    using RentItService.Services;

    /// <summary>
    /// Class for testing the EditMovieInformation method
    /// </summary>
    [TestClass]
    public class EditMovieInformationTest : DataTest
    {
        /// <summary>
        /// Tests the editing of the information of a movie
        /// Fails because of whitespace issues currently
        /// </summary>
        [TestMethod]
        public void EditMovieInformationTest1()
        {
            Service service = new Service();

            using (var db = new RentItContext())
            {
                TestHelper.SetUpTestMovies();
                TestHelper.SetUpTestUsers();

                User testUser = db.Users.First(u => u.Username == "testAdmin");
                Movie testMovie = db.Movies.First(u => u.Title == "testMovie1");

                var newMovie = new Movie()
                {
                    ID = testMovie.ID,
                    Description = "How to troll, for people new to the art",
                    FilePath = "You no take file location!",
                    Genre = "NoGenre",
                    ImagePath = "N/A",
                    Rentals = new Collection<Rental>(),
                    Title = "Trolling for beginners"
                };

                service.EditMovieInformation(testUser.Token, newMovie);

                Movie foundMovie = db.Movies.First(u => u.Title == "Trolling for beginners");

                // This part can be removed when the whitespace issue has been fixed
                if (db.Movies.First(u => u.Title == "Trolling for beginners") != null)
                {
                    db.Movies.Remove(db.Movies.First(u => u.Title == "Trolling for beginners"));
                    db.SaveChanges();
                }

                Assert.AreEqual("Trolling for beginners", foundMovie.Title);
                Assert.AreEqual("How to troll, for people new to the art", foundMovie.Description);
                Assert.AreEqual("NoGenre", foundMovie.Genre);

                //                if (db.Movies.First(u => u.Title == "Trolling for beginners") != null)
                //                {
                //                    db.Movies.Remove(db.Movies.First(u => u.Title == "Trolling for beginners"));
                //                    db.SaveChanges();
                //                }
            }
        }

        /// <summary>
        /// Tests if an exception is thrown when trying to edit movie
        /// information as a normal user
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InsufficientAccessLevelException))]
        public void InvalidUserTypeEditMovieInformationTest()
        {
            Service service = new Service();

            using (var db = new RentItContext())
            {
                TestHelper.SetUpTestMovies();
                TestHelper.SetUpTestUsers();

                User testUser = db.Users.First(u => u.Username == "testUser");
                Movie testMovie = db.Movies.First(u => u.Title == "testMovie1");

                var newMovie = new Movie()
                {
                    ID = testMovie.ID,
                    Description = "How to troll, for people new to the art",
                    FilePath = "You no take file location!",
                    Genre = "NoGenre",
                    ImagePath = "N/A",
                    Rentals = new Collection<Rental>(),
                    Title = "Trolling for beginners"
                };

                service.EditMovieInformation(testUser.Token, newMovie);

                //                if (db.Movies.First(u => u.Title == "Trolling for beginners") != null)
                //                {
                //                    db.Movies.Remove(db.Movies.First(u => u.Title == "Trolling for beginners"));
                //                    db.SaveChanges();
                //                }
            }
        }

        /// <summary>
        /// Tests if the information is saved if an invalid movieId is given
        /// as a parameter. The information shouldn't be saved.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NoMovieFoundException))]
        public void InvalidMovieIdTypeEditMovieInformationTest()
        {
            Service service = new Service();

            using (var db = new RentItContext())
            {
                TestHelper.SetUpTestMovies();
                TestHelper.SetUpTestUsers();

                User testUser = db.Users.First(u => u.Username == "testAdmin");

                var newMovie = new Movie()
                {
                    ID = 1337,
                    Description = "How to troll, for people new to the art",
                    FilePath = "You no take file location!",
                    Genre = "NoGenre",
                    ImagePath = "N/A",
                    Rentals = new Collection<Rental>(),
                    Title = "Trolling for beginners"
                };

                service.EditMovieInformation(testUser.Token, newMovie);

                //                if (db.Movies.First(u => u.Title == "Trolling for beginners") != null)
                //                {
                //                    db.Movies.Remove(db.Movies.First(u => u.Title == "Trolling for beginners"));
                //                    db.SaveChanges();
                //                }
            }
        }
    }
}
