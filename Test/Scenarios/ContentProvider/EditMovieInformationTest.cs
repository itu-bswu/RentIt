// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EditMovieInformationTest.cs" company="RentIt">
//   Copyright (c) RentIt. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace RentIt.Tests.Scenarios.ContentProvider
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RentItService;
    using RentItService.Entities;
    using RentItService.Enums;
    using RentItService.Exceptions;
    using Utils;

    /// <summary>
    /// Class for testing the EditMovieInformation method
    /// </summary>
    [TestClass]
    public class EditMovieInformationTest : DataTest
    {
        /// <summary>
        /// Purpose: Verify that the method changes the values of the movie
        /// <para></para>
        /// Pre-condtions:
        ///     1. A movie must exist in the database.
        ///     2. An admin must exist in the database.
        /// <para></para>
        /// Steps:
        ///     1. Assert that pre-conditions hold.
        ///     2. Find a movie in the databse.
        ///     3. Login as the admin.
        ///     4. Create a new movie with new values, but with 
        ///        the ID from the found movie.
        ///     5. Call EditMovieInformation with the with the 
        ///        token from the admin and the new movie.
        ///     6. Assert that a movie with the name "Trolling 
        ///        for beginners" exists in the database.
        /// </summary>
        [TestMethod]
        public void EditMovieInformationValidTest()
        {
            var testUser = TestUser.SystemAdmin;
            var loggedinUser = User.Login(testUser.Username, testUser.Password);

            var testMovie = Movie.All.First();

            var newTitle = "Trolling for beginners";
            var newDescription = "How to troll, for people new to the art";
            var newReleaseDate = testMovie.ReleaseDate.HasValue
                                        ? testMovie.ReleaseDate.Value.AddDays(14)
                                        : DateTime.Now.AddDays(14);

            var newMovie = new Movie
            {
                ID = testMovie.ID,
                Description = newDescription,
                ImagePath = "N/A",
                Title = newTitle,
                ReleaseDate = newReleaseDate,
            };

            newMovie.Genres.Add(new Genre("new Genre"));
            testMovie.Edit(loggedinUser, newMovie);


            var foundMovie = Movie.All.First(m => m.ID == testMovie.ID);

            Assert.AreEqual(newTitle, foundMovie.Title, "The titles doesn't match");
            Assert.AreEqual(newDescription, foundMovie.Description, "The descriptions doesn't match");
            Assert.AreEqual(newReleaseDate, foundMovie.ReleaseDate, "Release date doesn't match");
            Assert.AreEqual(1, foundMovie.Genres.Count(), "Number of genres doesn't match");

            foreach (var genre in foundMovie.Genres)
            {
                Assert.IsTrue(newMovie.Genres.Contains(genre), "The genres doesn't match");
            }
        }

        /// <summary>
        /// Purpose: verify that you can add a genre to a movie
        /// 
        /// Steps:
        ///     Step 1: get a movie and a genre from the database
        ///     Step 2: add the genre to the movie
        ///     Step 3: get the movie from the database
        ///     Step 4: verify that the genre was added
        /// </summary>
        [TestMethod]
        public void AddGenreTest()
        {
            var movie = Movie.All.Single(m => m.Title.Equals("Die Hard"));
            var genre = "Sci-Fi2";

            movie.AddGenre(genre);

            RentItContext.ReloadDb();

            var foundMovie = Movie.All.Single(m => m.Title.Equals("Die Hard"));

            Assert.IsTrue(foundMovie.HasGenre(genre));
        }

        /// <summary>
        /// Purpose: verify that you can remove a genre from a movie
        /// 
        /// Steps:
        ///     Step 1: get a movie and a genre from the database
        ///     Step 2: add the genre to the movie
        ///     Step 3: get the movie from the database
        ///     Step 4: verify that the genre was added
        /// </summary>
        [TestMethod]
        public void RemoveGenreTest()
        {
            var movie = Movie.All.Single(m => m.Title.Equals("Die Hard"));
            var genre = Genre.GetOrCreateGenre("Action");

            Assert.IsTrue(genre != null);
            Assert.IsTrue(movie != null);

            movie.RemoveGenre(genre);

            RentItContext.Db.SaveChanges();
            RentItContext.ReloadDb();

            var foundMovie = Movie.All.Single(m => m.Title.Equals("Die Hard"));

            Assert.IsFalse(foundMovie.Genres.Any(g => g.Name.Equals("Action")));
        }

        /// <summary>
        /// Purpose: Verify that the method throws the correct exception
        /// when called by an account with an insufficient user type
        /// <para></para>
        /// Pre-condtions:
        ///     1. A movie must exist in teh database.
        ///     2. A user must exist in the database.
        /// <para></para>
        /// Steps:
        ///     1. Assert that pre-conditions hold.
        ///     2. Find a movie in the databse.
        ///     3. Login as the user.
        ///     4. Create a new movie with new values, but with 
        ///        the ID from the found movie.
        ///     5. Call EditMovieInformation with the with the 
        ///        token from the user and the new movie.
        ///     6. Assert that an InsufficientAccessLevelException
        ///        is thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InsufficientRightsException))]
        public void EditMovieInformationInvalidUserTypeTest()
        {
            var testUser = TestUser.User;
            var testMovie = Movie.All.First();

            var loggedinUser = User.Login(testUser.Username, testUser.Password);

            var newMovie = new Movie
                {
                    ID = testMovie.ID,
                    Description = "How to troll, for people new to the art",
                    ImagePath = "N/A",
                    Title = "Trolling for beginners"
                };

            newMovie.Edit(loggedinUser, newMovie);
        }

        /// <summary>
        /// Purpose: Verify that the method throws the correct exception
        /// when called by an account with an insufficient user type
        /// <para></para>
        /// Pre-condtions:
        ///     1. An admin must exist in the database.
        /// <para></para>
        /// Steps:
        ///     1. Assert that pre-conditions hold.
        ///     2. Login as the admin.
        ///     2. Create a new movie with new values, and an
        ///        ID that doesn't corrospond to a movie in the 
        ///        databse.
        ///     3. Call EditMovieInformation with the with the 
        ///        token from the admin and the new movie.
        ///     4. Assert that a NoMovieFoundException
        ///        is thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InsufficientRightsException))]
        public void EditMovieInformationInvalidMovieIdTypeTest()
        {
            var loggedinUser = User.Login(TestUser.ContentProvider.Username, TestUser.ContentProvider.Password);

            var newMovie = new Movie
                {
                    ID = 89485618,
                    Description = "How to troll, for people new to the art",
                    ImagePath = "N/A",
                    Title = "Trolling for beginners"
                };

            newMovie.Edit(loggedinUser, newMovie);
        }

        /// <summary>
        /// Purpose: Verify that it is not possible to edit a movie 
        /// uploaded by another content publisher.
        /// 
        /// Pre-condition:
        ///     1. A movie uploaded by some publisher exists in the database.
        /// 
        /// Steps:
        ///     1. Get a movie created by some user in the database.
        ///     2. Create a new content publisher.
        ///     3. Login as the new user.
        ///     4. Edit movie from step 1 with publisher from step 2.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InsufficientRightsException))]
        public void EditMovieFromOtherProvider()
        {
            const string Username = "SomeContentPublisher";
            const string Password = "12345";

            // Step 1
            var movie = Movie.All.First();

            // Step 2
            User.SignUp(new User
            {
                Username = Username,
                Password = Password,
                Email = "publisher@somecompany.org"
            });

            User.All.First(u => u.Username == Username).Type = UserType.ContentProvider;
            RentItContext.Db.SaveChanges();

            // Step 3
            var user = User.Login(Username, Password);

            // Step 4
            movie.Edit(user, movie);
        }

        /// <summary>
        /// Purpose: Verify that it is possible to only update 
        ///          part of the information about a movie.
        /// 
        /// Steps:
        ///     1. Log in as a content provider with movies.
        ///     2. Choose a movie to update.
        ///     3. Keep a copy of all old values of the movie.
        ///     4. Update the movie with another title and empty description.
        ///     5. Refresh movie information.
        ///     6. Verify that title has changed.
        ///     7. Verify that description is now empty.
        ///     8. Verify that the rest has not been updated.
        /// </summary>
        [TestMethod]
        public void EditMoviePartOfInfo()
        {
            string newTitle = "Awesome new movie", newDescription = string.Empty;

            // Step 1
            var user = User.Login(TestUser.ContentProvider);

            // Step 2
            var movie = Movie.All.First(m => m.OwnerID.Equals(user.ID));

            // Step 3
            var oldTitle = movie.Title;
            var oldDescription = movie.Description;
            var oldImagePath = movie.ImagePath;
            var oldReleaseDate = movie.ReleaseDate;

            // Step 4
            movie.Edit(
                user,
                new Movie
                {
                    Title = newTitle,
                    Description = newDescription
                });

            // Step 5
            movie = Movie.Get(user, movie.ID);

            // Step 6
            Assert.AreEqual(newTitle, movie.Title, "Title has incorrect value!");
            Assert.AreNotEqual(oldTitle, movie.Title, "Title has not changed!");

            // Step 7
            Assert.AreEqual(newDescription, movie.Description, "Description has incorrect value!");
            Assert.AreNotEqual(oldDescription, movie.Description, "Description has not changed!");

            // Step 8
            Assert.AreEqual(oldImagePath, movie.ImagePath, "Imagepath has changed!");
            Assert.AreEqual(oldReleaseDate, movie.ReleaseDate, "Release date has changed!");
        }

        /// <summary>
        /// Purpose: Verify that a field is only updated, if 
        ///          the new value is valid.
        /// 
        /// Steps:
        ///     1. Log in as a content provider with movies.
        ///     2. Choose a movie to update.
        ///     3. Keep a copy of all old values of the movie.
        ///     4. Update the movie with empty title and null description.
        ///     5. Refresh movie information.
        ///     6. Verify that title has not changed.
        ///     7. Verify that description has not changed.
        ///     8. Verify that the rest has not been updated.
        /// </summary>
        [TestMethod]
        public void EditMoviePartOfInfoInvalidValues()
        {
            string newTitle = string.Empty;

            // Step 1
            var user = User.Login(TestUser.ContentProvider);

            // Step 2
            var movie = Movie.All.First(m => m.OwnerID.Equals(user.ID));

            // Step 3
            var oldTitle = movie.Title;
            var oldDescription = movie.Description;
            var oldImagePath = movie.ImagePath;
            var oldReleaseDate = movie.ReleaseDate;

            // Step 4
            movie.Edit(
                user,
                new Movie
                {
                    Title = newTitle,
                    Description = null
                });

            // Step 5
            movie = Movie.Get(user, movie.ID);

            // Step 6
            Assert.AreNotEqual(newTitle, movie.Title, "Title has changed!");
            Assert.AreEqual(oldTitle, movie.Title, "Title has changed!");

            // Step 7
            Assert.IsNotNull(movie.Description, "Description has changed!");
            Assert.AreEqual(oldDescription, movie.Description, "Description has changed!");

            // Step 8
            Assert.AreEqual(oldImagePath, movie.ImagePath, "Imagepath has changed!");
            Assert.AreEqual(oldReleaseDate, movie.ReleaseDate, "Release date has changed!");
        }

        /// <summary>
        /// Purpose: Verify that fields with new valid values will 
        ///          be updated, even when other fields will not 
        ///          be updated, because of invalid values.
        /// 
        /// Steps:
        ///     1. Log in as a content provider with movies.
        ///     2. Choose a movie to update.
        ///     3. Keep a copy of all old values of the movie.
        ///     4. Update the movie with empty title (invalid) and empty description (valid).
        ///     5. Refresh movie information.
        ///     6. Verify that title has not changed.
        ///     7. Verify that description has changed.
        ///     8. Verify that the rest has not been updated.
        /// </summary>
        [TestMethod]
        public void EditMoviePartOfInfoMixedValidInvalid()
        {
            string newTitle = string.Empty, newDescription = string.Empty;

            // Step 1
            var user = User.Login(TestUser.ContentProvider);

            // Step 2
            var movie = Movie.All.First(m => m.OwnerID.Equals(user.ID));

            // Step 3
            var oldTitle = movie.Title;
            var oldDescription = movie.Description;
            var oldImagePath = movie.ImagePath;
            var oldReleaseDate = movie.ReleaseDate;

            // Step 4
            movie.Edit(
                user,
                new Movie
                {
                    Title = newTitle,
                    Description = newDescription
                });

            // Step 5
            movie = Movie.Get(user, movie.ID);

            // Step 6
            Assert.AreNotEqual(newTitle, movie.Title, "Title has changed!");
            Assert.AreEqual(oldTitle, movie.Title, "Title has changed!");

            // Step 7
            Assert.AreEqual(newDescription, movie.Description, "Description has incorrect value!");
            Assert.AreNotEqual(oldDescription, movie.Description, "Description has not changed!");

            // Step 8
            Assert.AreEqual(oldImagePath, movie.ImagePath, "Imagepath has changed!");
            Assert.AreEqual(oldReleaseDate, movie.ReleaseDate, "Release date has changed!");
        }
    }
}
