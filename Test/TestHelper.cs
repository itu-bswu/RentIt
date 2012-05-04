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
    /// Class used for some tests.
    /// </summary>
    public class TestHelper
    {
        /// <summary>
        /// Sets up a test user, test content provider and a test admin
        /// </summary>
        /// <returns>The users that have been set up.</returns>
        public static IEnumerable<User> SetUpTestUsers()
        {
            var users = new List<User>();

            using (var db = new RentItContext())
            {
                // User setup
                if (!db.Users.Any(a => a.Username == "testUser"))
                {
                    var u = new User
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
                    var u = new User
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
                    var u = new User
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
                    var movie = new Movie
                        {
                            Description = "testMovie1",
                            FilePath = "no file location",
                            ImagePath = "no image location",
                            Rentals = new Collection<Rental>(),
                            Title = "testMovie1"
                        };
                    db.Movies.Add(movie);
                    db.SaveChanges();
                }
                else
                {
                    db.Movies.First(m => m.Description == "testMovie1").ImagePath = "no image location";
                    db.Movies.First(m => m.Description == "testMovie1").Title = "testMovie1";
                    db.Movies.First(m => m.Description == "testMovie1").FilePath = "no file location";
                }
            }
        }

        /// <summary>
        /// Sets up testusers for testing of the rentalhistory.
        /// </summary>
        /// <returns>The test rental users.</returns>
        public static IEnumerable<User> SetUpRentalTestUsers()
        {
            var users = new List<User>();

            using (var db = new RentItContext())
            {
                if (!db.Users.Any(a => a.Username == "testUserRent1"))
                {
                    var content = new User
                        {
                            Email = "testUser1@testing.dk",
                            FullName = "Test1 User",
                            Password = "test.dk",
                            Type = UserType.ContentProvider,
                            Token = "testUserToken1",
                            Username = "testUserRent1",
                        };
                    db.Users.Add(content);
                    db.SaveChanges();
                    users.Add(content);
                }
                else
                {
                    var user = db.Users.First(a => a.Username == "testUserRent1");
                    user.Password = "test.dk";
                    user.FullName = "Test1 User";
                    user.Email = "testUser1@testing.dk";
                    db.SaveChanges();
                    users.Add(user);
                }

                if (!db.Users.Any(a => a.Username == "testUserRent2"))
                {
                    var u = new User
                        {
                            Email = "testUser1@testing.dk",
                            FullName = "Test1 User",
                            Password = "test.dk",
                            Type = UserType.User,
                            Token = "testUserToken2",
                            Username = "testUserRent2",
                        };

                    db.Users.Add(u);
                    db.SaveChanges();
                    users.Add(u);
                }
                else
                {
                    var user = db.Users.First(a => a.Username == "testUserRent2");
                    user.Password = "test.dk";
                    user.FullName = "Test1 User";
                    user.Email = "testUser1@testing.dk";
                    db.SaveChanges();
                    users.Add(user);
                }

                if (!db.Users.Any(a => a.Username == "testUserRent3"))
                {
                    var u = new User
                        {
                            Email = "testUser1@testing.dk",
                            FullName = "Test1 User",
                            Password = "test.dk",
                            Type = UserType.User,
                            Token = "testUserToken3",
                            Username = "testUserRent3"
                        };
                    db.Users.Add(u);
                    db.SaveChanges();
                    users.Add(u);
                }
                else
                {
                    var user = db.Users.First(a => a.Username == "testUserRent3");
                    user.Password = "test.dk";
                    user.FullName = "Test1 User";
                    user.Email = "testUser1@testing.dk";
                    db.SaveChanges();
                    users.Add(user);
                }

                if (!db.Users.Any(a => a.Username == "testUserRent4"))
                {
                    var u = new User
                        {
                            Email = "testUser1@testing.dk",
                            FullName = "Test1 User",
                            Password = "test.dk",
                            Type = UserType.User,
                            Token = "testUserToken4",
                            Username = "testUserRent4"
                        };
                    db.Users.Add(u);
                    db.SaveChanges();
                    users.Add(u);
                }
                else
                {
                    var user = db.Users.First(a => a.Username == "testUserRent4");
                    user.Password = "test.dk";
                    user.FullName = "Test1 User";
                    user.Email = "testUser1@testing.dk";
                    db.SaveChanges();
                    users.Add(user);
                }

                if (!db.Users.Any(a => a.Username == "testContentRent"))
                {
                    var u = new User
                    {
                        Email = "testUser1@testing.dk",
                        FullName = "Test1 User",
                        Password = "test.dk",
                        Type = UserType.ContentProvider,
                        Token = "testUserToken5",
                        Username = "testContentRent",
                    };
                    db.Users.Add(u);
                    db.SaveChanges();
                    users.Add(u);
                }
                else
                {
                    var user = db.Users.First(a => a.Username == "testContentRent");
                    user.Password = "test.dk";
                    user.FullName = "Test1 User";
                    user.Email = "testUser1@testing.dk";
                    db.SaveChanges();
                    users.Add(user);
                }

                return users;
            }
        }

        /// <summary>
        /// Sets up test movies for testing of the rentalhistory
        /// </summary>
        public static void SetUpMoviesForRentalTest()
        {
            int oID;

            using (var db = new RentItContext())
            {
                var user = db.Users.First(u => u.Username == "testContentRent");
                oID = user.ID;
            }

            using (var db = new RentItContext())
            {
                if (!db.Movies.Any(m => m.Description.Equals("batman")))
                {
                    var movie = new Movie
                        {
                            Description = "batman",
                            FilePath = "no file location1",
                            ImagePath = "no image location1",
                            Title = "batman",
                            OwnerID = oID
                        };
                    db.Movies.Add(movie);
                    db.SaveChanges();
                }
                else
                {
                    db.Movies.First(m => m.Description == "batman").ImagePath = "no image location1";
                    db.Movies.First(m => m.Description == "batman").Title = "batman";
                    db.Movies.First(m => m.Description == "batman").FilePath = "no file location1";
                    db.Movies.First(m => m.Description == "batman").OwnerID = oID;
                }

                if (!db.Movies.Any(m => m.Description.Equals("superman")))
                {
                    var movie = new Movie
                        {
                            Description = "superman",
                            FilePath = "no file location2",
                            ImagePath = "no image location2",
                            Title = "superman",
                            OwnerID = oID
                        };
                    db.Movies.Add(movie);
                    db.SaveChanges();
                }
                else
                {
                    db.Movies.First(m => m.Description == "superman").ImagePath = "no image location2";
                    db.Movies.First(m => m.Description == "superman").Title = "superman";
                    db.Movies.First(m => m.Description == "superman").FilePath = "no file location2";
                    db.Movies.First(m => m.Description == "superman").OwnerID = oID;
                }

                if (!db.Movies.Any(m => m.Description.Equals("spiderman")))
                {
                    var movie = new Movie
                        {
                            Description = "spiderman",
                            FilePath = "no file location3",
                            ImagePath = "no image location3",
                            Title = "spiderman",
                            OwnerID = oID
                        };
                    db.Movies.Add(movie);
                    db.SaveChanges();
                }
                else
                {
                    db.Movies.First(m => m.Description == "spiderman").ImagePath = "no image location3";
                    db.Movies.First(m => m.Description == "spiderman").Title = "spiderman";
                    db.Movies.First(m => m.Description == "spiderman").FilePath = "no file location3";
                    db.Movies.First(m => m.Description == "spiderman").OwnerID = oID;
                }
            }
        }

        /// <summary>
        /// Sets up rentals for the most downloaded test.
        /// </summary>
        public static void TestRentalsMostDownloaded()
        {
            SetUpRentalTestUsers();
            SetUpMoviesForRentalTest();

            using (var db = new RentItContext())
            {
                var testUser2 = db.Users.First(u => u.Username == "testUserRent2");
                var testUser3 = db.Users.First(u => u.Username == "testUserRent3");
                var testUser4 = db.Users.First(u => u.Username == "testUserRent4");

                User.RentMovie(testUser2.Token, 1);
                User.RentMovie(testUser3.Token, 2);
                User.RentMovie(testUser3.Token, 2);
                User.RentMovie(testUser3.Token, 1);
                User.RentMovie(testUser4.Token, 3);
                User.RentMovie(testUser4.Token, 1);
            }
        }
    }
}