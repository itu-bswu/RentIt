// -----------------------------------------------------------------------
// <copyright file="RegisterMovieScenarioTest.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace RentIt.Tests.Scenarios.ContentProvider
{
    using System;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RentIt.Tests.Utils;

    using RentItService;
    using RentItService.Entities;
    using RentItService.Exceptions;

    /// <summary>
    /// Scenario tests for the "Register Movie" feature.
    /// </summary>
    [TestClass]
    public class RegisterMovieScenarioTest : DataTest
    {
        /// <summary>
        /// Purpose: Verify that a content provider is able to register movies in the database.
        /// <para>
        /// Pre-condtions:
        ///     1. A content provider with the username "Universal" must exist in the database.
        /// </para>
        /// <para>
        /// Steps:
        ///     1. Get a token for the "Universal" user.
        ///     2. Create a new Movie object.
        ///     3. Verify that the movie does not already exist in the database.
        ///     4. Register it in the database with the RegisterMovie method.
        ///     5. Verify that it exists in the database.
        /// </para>
        /// </summary>
        [TestMethod]
        public void RegisterMovieTest()
        {
            var user = User.Login(TestUser.ContentProvider.Username, TestUser.ContentProvider.Password);

            var movie = new Movie
                {
                    Description = "testMovie",
                    ImagePath = "noImagePath",
                    Title = "testMovieTitle1337",
                    OwnerID = user.ID
                };

            using (var db = new RentItContext())
            {
                Assert.IsFalse(db.Movies.Any(m => m.Title == movie.Title), "Movie already exists in the database.");
            }

            Movie.RegisterMovie(user.Token, movie);

            using (var db = new RentItContext())
            {
                Assert.IsTrue(db.Movies.Any(m => m.Title == movie.Title), "Movie does not exist in database!");
            }
        }

        /// <summary>
        /// Purpose: Verify that it is not possible to use invalid values in the method.
        /// <para>
        /// Pre-condtions:
        ///     1. A content provider with the username "Universal" must exist in the database.
        /// </para>
        /// <para>
        /// Steps:
        ///     1. Get a token for the "Universal" user.
        ///     2. Create a new Movie object with invalid values.
        ///     3. Attempt to register the movie from 3.
        ///     4. Verify that ArgumentNullException is thrown.
        /// </para>
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InvalidValuesRegisterMovieTest()
        {
            var user = User.Login(TestUser.ContentProvider.Username, TestUser.ContentProvider.Password);

            var movie = new Movie
            {
                Description = "testMovie",
                ImagePath = "noImagePath",
                Title = null,
                OwnerID = user.ID
            };

            Movie.RegisterMovie(user.Token, movie);
        }

        /// <summary>
        /// Purpose: Verify that it is not possible to use the method as a user of type "User".
        /// <para>
        /// Pre-condtions:
        ///     1. A user with the username "Smith" must exist in the database.
        /// </para>
        /// <para>
        /// Steps:
        ///     1. Get a token for the "Smith" user.
        ///     2. Create a new Movie object.
        ///     3. Attempt to register it in the database with the RegisterMovie method.
        ///     4. Verify that a InsufficientRightsException is thrown.
        /// </para>
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InsufficientRightsException))]
        public void WrongUserTypeRegisterMovieTest()
        {
            var user = User.Login(TestUser.User.Username, TestUser.User.Password);

            var movie = new Movie
            {
                Description = "testMovie",
                ImagePath = "noImagePath",
                Title = "testMovie1337",
                OwnerID = user.ID
            };

            Movie.RegisterMovie(user.Token, movie);
        }

        /// <summary>
        /// Purpose: Verify that it is not possible to call the method with a null token.
        /// <para>
        /// Steps:
        ///     1. Create a new Movie object.
        ///     2. Attempt to register it in the database with the RegisterMovie method using a null token.
        ///     3. Verify that a ArgumentNullException is thrown.
        /// </para>
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullTokenRegisterMovieTest()
        {
            var movie = new Movie
            {
                Description = "testMovie",
                ImagePath = "noImagePath",
                Title = "testMovie1337",
                OwnerID = 10000
            };

            Movie.RegisterMovie(null, movie);
        }
    }
}
