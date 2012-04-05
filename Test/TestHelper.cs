// -----------------------------------------------------------------------
// <copyright file="TestHelper.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace RentIt.Tests
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using RentItService;
    using RentItService.Entities;
    using RentItService.Enums;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class TestHelper
    {
        /// <summary>
        /// Sets up a test user, test content provider and a test admin
        /// </summary>
        /// <returns>The users that have been set up.</returns>
        public static IEnumerable<User> SetUpTestUsers()
        {
            List<User> users = new List<User>();

            using (var db = new RentItContext())
            {
                // User setup
                if (!db.Users.Any(a => a.Username == "testUser"))
                {
                    User u = new User
                        {
                            Email = "testUser@testing.dk",
                            FullName = "Test User",
                            Password = "test.dk",
                            Type = UserType.User,
                            Token = "testUserToken",
                            Username = "testUser"
                        };
                    db.Users.Add(u);
                    db.SaveChanges();
                    users.Add(u);
                }
                else
                {
                    var user = db.Users.First(a => a.Username == "testUser");
                    user.Password = "test.dk";
                    user.FullName = "Test User";
                    user.Email = "testUser@testing.dk";
                    db.SaveChanges();
                    users.Add(user);
                }

                // ContentProvider setup
                if (!db.Users.Any(a => a.Username == "testContentProvider"))
                {
                    User u = new User
                    {
                        Email = "testContentProvider@testing.dk",
                        FullName = "Test ContentProvider",
                        Password = "test.dk",
                        Type = UserType.ContentProvider,
                        Token = "testContentProviderToken",
                        Username = "testContentProvider"
                    };
                    db.Users.Add(u);
                    db.SaveChanges();
                    users.Add(u);
                }
                else
                {
                    var user = db.Users.First(a => a.Username == "testContentProvider");
                    user.Password = "test.dk";
                    user.FullName = "Test ContentProvider";
                    user.Email = "testContentProvider@testing.dk";
                    db.SaveChanges();
                    users.Add(user);
                }

                // Admin setup
                if (!db.Users.Any(a => a.Username == "testAdmin"))
                {
                    User u = new User
                    {
                        Email = "testAdmin@testing.dk",
                        FullName = "Test Admin",
                        Password = "test.dk",
                        Type = UserType.SystemAdmin,
                        Token = "testAdminToken",
                        Username = "testAdmin"
                    };
                    db.Users.Add(u);
                    db.SaveChanges();
                    users.Add(u);
                }
                else
                {
                    var user = db.Users.First(a => a.Username == "testAdmin");
                    user.Password = "test.dk";
                    user.FullName = "Test Admin";
                    user.Email = "testAdmin@testing.dk";
                    db.SaveChanges();
                    users.Add(user);
                }
            }

            return users;
        }

        /// <summary>
        /// Sets up test data for the movie table.
        /// </summary>
        public static void SetUpTestMovies()
        {
            using (var db = new RentItContext())
            {
                if (!db.Movies.Any(m => m.Description.Equals("testMovie1")))
                {
                    Movie movie = new Movie
                    {
                        Description = "testMovie1",
                        FilePath = "no file location",
                        Genre = "testGenre",
                        ImagePath = "no image location",
                        Rentals = new Collection<Rental>(),
                        Title = "testMovie1"
                    };
                    db.Movies.Add(movie);
                    db.SaveChanges();
                }
                else
                {
                    db.Movies.First(m => m.Description == "testMovie1").Genre = "testGenre";
                    db.Movies.First(m => m.Description == "testMovie1").ImagePath = "no image location";
                    db.Movies.First(m => m.Description == "testMovie1").Title = "testMovie1";
                    db.Movies.First(m => m.Description == "testMovie1").FilePath = "no file location";
                }
            }
        }
    }
}
