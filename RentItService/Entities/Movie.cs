//-------------------------------------------------------------------------------------------------
// <copyright file="Movie.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentItService.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics.Contracts;
    using System.IO;
    using System.Linq;

    using RentItService.Enums;
    using RentItService.Exceptions;

    using Tools;

    /// <summary>
    /// Movie entity (Entity Framework POCO class).
    /// </summary>
    public class Movie
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Movie"/> class.
        /// </summary>
        public Movie()
        {
            this.Rentals = new List<Rental>();
        }

        /// <summary>
        /// Gets or sets the movie's ID.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the movie title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the movie description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the image path.
        /// </summary>
        public string ImagePath { get; set; }

        /// <summary>
        /// Gets or sets the file path.
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Gets or sets the genre of the movie.
        /// </summary>
        public string Genre { get; set; }

        /// <summary>
        /// Gets or sets a list of rentals of the movie.
        /// </summary>
        public virtual ICollection<Rental> Rentals { get; set; }

        /// <summary>
        /// Deletes a movie from the service. 
        /// The movie is identified by the ID in the instance of the Movie class. 
        /// The other properties in the Movie instance are ignored.
        /// </summary>
        /// <param name="token">The session token.</param>
        /// <param name="movieObject">The movie to be deleted.</param>
        /// <author>Jakob Melnyk</author>
        public static void DeleteMovie(string token, Movie movieObject)
        {
            Contract.Requires<ArgumentNullException>(token != null);
            Contract.Requires<ArgumentNullException>(movieObject != null);

            Contract.Requires<InsufficientAccessLevelException>(User.GetByToken(token).Type == UserType.ContentProvider);

            using (var db = new RentItContext())
            {
                foreach (var r in db.Rentals.Where(r => r.MovieID == movieObject.ID))
                {
                    db.Rentals.Remove(r);
                }

                var filePath = Constants.UploadDownloadFileFolder + db.Movies.First(m => m.ID == movieObject.ID).FilePath;

                db.Movies.Remove(db.Movies.First(m => m.ID == movieObject.ID));
                db.SaveChanges();

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
        }

        /// <summary>
        /// Searches the database for a specific movie title.
        /// </summary>
        /// <param name="token">The session token.</param>
        /// <param name="search">The search string.</param>
        /// <returns>An IEnumerable containing the movies fitting the search.</returns>
        public static IEnumerable<Movie> Search(string token, string search)
        {
            Contract.Requires<ArgumentNullException>(token != null);
            Contract.Requires<ArgumentNullException>(search != null);

            User.GetByToken(token);

            using (var db = new RentItContext())
            {
                var searchTitle = search.ToLower();
                var components = searchTitle.Split(' ');

                return from movie in db.Movies
                       let title = movie.Title.ToLower()
                       let titleComponents = title.Split(' ')
                       where titleComponents.Any(components.Contains)
                       orderby title.Equals(searchTitle) descending
                       orderby titleComponents.Count(components.Contains) descending
                       select movie;
            }
        }

        /// <summary>
        /// Filters the list of movies into a particular genre.
        /// </summary>
        /// <param name="token">The session token.</param>
        /// <param name="genre">The genre to filter by.</param>
        /// <returns>An IEnumerable containing the filtered movies.</returns>
        public static IEnumerable<Movie> ByGenre(string token, string genre)
        {
            Contract.Requires<ArgumentNullException>(token != null);
            Contract.Requires<ArgumentNullException>(genre != null);

            User.GetByToken(token);

            using (var db = new RentItContext())
            {
                if (db.Movies.Count(movie => movie.Genre.Equals(genre)) == 0)
                {
                    throw new UnknownGenreException();
                }

                return db.Movies.Where(movie => movie.Genre.Equals(genre));
            }
        }

        /// <summary>
        /// Returns the newest added movies.
        /// </summary>
        /// <param name="limit">The maximum amount of movies to return (0 = unlimited).</param>
        /// <returns>An IEnumerable of the newest movies.</returns>
        public static IEnumerable<Movie> Newest(int limit = 0)
        {
            Contract.Requires<ArgumentException>(limit >= 0);

            using (var db = new RentItContext())
            {
                var movies = db.Movies.OrderByDescending(m => m.ID); // TODO: Add release date to movies.

                return limit > 0 ? movies.Take(limit) : movies;
            }
        }

        /// <summary>
        /// Gets all the movies in the database.
        /// </summary>
        /// <param name="token">
        /// The session token.
        /// </param>
        /// <returns>
        /// All the movie entries in the database.
        /// </returns>
        public static IEnumerable<Movie> GetAllMovies(string token)
        {
            Contract.Requires<ArgumentNullException>(token != null);
            Contract.Requires<ArgumentException>(User.GetByToken(token) != null);

            using (var db = new RentItContext())
            {
                return db.Movies.ToList();
            }
        }
    }
}