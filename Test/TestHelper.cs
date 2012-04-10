﻿// -----------------------------------------------------------------------
// <copyright file="TestHelper.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace RentIt.Tests
{
    using System;
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

        /// <summary>
        /// Sets up testusers for testing of the rentalhistory.
        /// </summary>
        public static IEnumerable<User> SetUpRentalTestUsers()
        {
            List<User> users = new List<User>();

            using (var db = new RentItContext())
            {

                if (!db.Users.Any(a => a.Username == "testUserRent1"))
                {
                    User u = new User
                        {
                            Email = "testUser1@testing.dk",
                            FullName = "Test1 User",
                            Password = "test.dk",
                            Type = UserType.User,
                            Token = "testUserToken",
                            Username = "testUserRent1"
                        };
                    db.Users.Add(u);
                    db.SaveChanges();
                    users.Add(u);
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
                    User u = new User
                        {
                            Email = "testUser1@testing.dk",
                            FullName = "Test1 User",
                            Password = "test.dk",
                            Type = UserType.User,
                            Token = "testUserToken",
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
                    User u = new User
                        {
                            Email = "testUser1@testing.dk",
                            FullName = "Test1 User",
                            Password = "test.dk",
                            Type = UserType.User,
                            Token = "testUserToken",
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
                    User u = new User
                        {
                            Email = "testUser1@testing.dk",
                            FullName = "Test1 User",
                            Password = "test.dk",
                            Type = UserType.User,
                            Token = "testUserToken",
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



                return users;
            }
        }

        /// <summary>
        /// Sets up test movies for testing of the rentalhistory
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Movie> SetUpMoviesForRentalTest()
        {
            List<Movie> movies = new List<Movie>();
            using (var db = new RentItContext())
            {
                Movie batman = new Movie
                    {
                        Description = "BatmanTest1337",
                        FilePath = "no file location1",
                        Genre = "testGenre1",
                        ImagePath = "no image location1",
                        Rentals = new Collection<Rental>(),
                        Title = "Batman1337"
                    };

                movies.Add(batman);
                db.Movies.Add(batman);
                db.SaveChanges();

                Movie superman = new Movie
                    {
                        Description = "SupermanTest1337",
                        FilePath = "no file location2",
                        Genre = "testGenre2",
                        ImagePath = "no image location2",
                        Rentals = new Collection<Rental>(),
                        Title = "Superman1337"
                    };

                movies.Add(superman);
                db.Movies.Add(superman);
                db.SaveChanges();

                Movie spiderman = new Movie
                    {
                        Description = "SpidermanTest1337",
                        FilePath = "no file location3",
                        Genre = "testGenre3",
                        ImagePath = "no image location3",
                        Rentals = new Collection<Rental>(),
                        Title = "Spiderman1337"
                    };

                movies.Add(spiderman);
                db.Movies.Add(spiderman);
                db.SaveChanges();
            }
            return movies;
        }


        public static IEnumerable<Rental> TestRentalsMostDownloaded()
        {
            SetUpRentalTestUsers();
            SetUpMoviesForRentalTest();

            User testUser2;
            User testUser3;
            User testUser4;

            Movie batman;
            Movie superman;
            Movie spiderman;

            List<Rental> rentals = new List<Rental>();

            using (var db = new RentItContext())
            {
                testUser2 = db.Users.First(u => u.Username == "testUserRent2");
                testUser3 = db.Users.First(u => u.Username == "testUserRent3");
                testUser4 = db.Users.First(u => u.Username == "testUserRent4");

                batman = db.Movies.First(m => m.Title == "Batman1337");
                superman = db.Movies.First(m => m.Title == "Superman1337");
                spiderman = db.Movies.First(m => m.Title == "Spiderman1337");

                Rental rentOne = new Rental { Movie = batman, User = testUser2, Time = new DateTime(2012, 3, 15, 10, 55, 23), };

                Rental rentTwo = new Rental { Movie = superman, User = testUser3, Time = new DateTime(2012, 4, 21, 5, 55, 23), };

                Rental rentThree = new Rental { Movie = superman, User = testUser3, Time = new DateTime(2012, 2, 20, 5, 55, 23), };

                Rental rentFour = new Rental { Movie = batman, User = testUser3, Time = new DateTime(2012, 3, 15, 10, 55, 23), };

                Rental rentFive = new Rental { Movie = spiderman, User = testUser4, Time = new DateTime(2012, 5, 20, 5, 55, 23) };

                Rental rentSix = new Rental { Movie = batman, User = testUser4, Time = new DateTime(2012, 4, 15, 10, 55, 23) };

                db.Rentals.Add(rentOne);
                db.Rentals.Add(rentTwo);
                db.Rentals.Add(rentThree);
                db.Rentals.Add(rentFour);
                db.Rentals.Add(rentFive);
                db.Rentals.Add(rentSix);
                db.SaveChanges();

                return rentals;
            }
        }
    }
}