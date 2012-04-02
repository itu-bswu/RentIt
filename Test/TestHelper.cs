// -----------------------------------------------------------------------
// <copyright file="TestHelper.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace RentIt.Tests
{
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
        public static void SetUpTestUsers()
        {
            using (var db = new RentItContext())
            {
                // User setup
                if (!db.Users.Any(a => a.Username == "testUser"))
                {
                    User u = new User()
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
                }
                else
                {
                    db.Users.First(a => a.Username == "testUser").Password = "test.dk";
                    db.Users.First(a => a.Username == "testUser").FullName = "Test User";
                    db.Users.First(a => a.Username == "testUser").Email = "testUser@testing.dk";
                    db.SaveChanges();
                }

                // ContentProvider setup
                if (!db.Users.Any(a => a.Username == "testContentProvider"))
                {
                    User u = new User()
                    {
                        Email = "testContentProvider@testing.dk",
                        FullName = "Test ContentProvider",
                        Password = "test.dk",
                        Type = UserType.User,
                        Token = "testContentProviderToken",
                        Username = "testContentProvider"
                    };
                    db.Users.Add(u);
                    db.SaveChanges();
                }
                else
                {
                    db.Users.First(a => a.Username == "testContentProvider").Password = "test.dk";
                    db.Users.First(a => a.Username == "testContentProvider").FullName = "Test ContentProvider";
                    db.Users.First(a => a.Username == "testContentProvider").Email = "testContentProvider@testing.dk";
                    db.SaveChanges();
                }

                // Admin setup
                if (!db.Users.Any(a => a.Username == "testAdmin"))
                {
                    User u = new User()
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
                }
                else
                {
                    db.Users.First(a => a.Username == "testAdmin").Password = "test.dk";
                    db.Users.First(a => a.Username == "testAdmin").FullName = "Test Admin";
                    db.Users.First(a => a.Username == "testAdmin").Email = "testAdmin@testing.dk";
                    db.SaveChanges();
                }
            }
        }

        public static void SetUpTestMovies()
        {
            using (var db = new RentItContext())
            {
                if (!db.Movies.Any(m => m.Description.Equals("testMovie1")))
                {
                    Movie movie = new Movie()
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
