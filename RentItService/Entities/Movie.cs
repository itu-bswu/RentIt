//-------------------------------------------------------------------------------------------------
// <copyright file="Movie.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentItService.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.IO;
    using System.Linq;
    using Enums;
    using Exceptions;
    using Library;
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
            this.Genres = new List<Genre>();
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
        /// Gets or sets the owner ID.
        /// </summary>
        public int OwnerID { get; set; }

        /// <summary>
        /// Gets or sets the owner of the movie.
        /// </summary>
        public virtual User Owner { get; set; }

        /// <summary>
        /// Gets or sets a list of rentals of the movie.
        /// </summary>
        public virtual ICollection<Rental> Rentals { get; set; }

        /// <summary>
        /// Gets or sets the movie's genres.
        /// </summary>
        public virtual ICollection<Genre> Genres { get; set; }

        /// <summary>
        /// Add a genre to the movie
        /// </summary>
        /// <param name="genre">The genre</param>
        public void AddGenre(Genre genre)
        {
            this.Genres.Add(genre);
        }

        /// <summary>
        /// Remove a genre from the movie
        /// </summary>
        /// <param name="genre">The Genre</param>
        public void RemoveGenre(Genre genre)
        {
            this.Genres.Remove(genre);
        }

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
            Contract.Requires<InsufficientRightsException>(User.GetByToken(token).Type == UserType.ContentProvider);

            using (var db = new RentItContext())
            {
                var movie = db.Movies.First(m => m.ID == movieObject.ID);
                var user = User.GetByToken(token);

                if (movie.OwnerID != user.ID && user.Type != UserType.SystemAdmin)
                {
                    throw new InsufficientRightsException("Cannot delete a movie belonging to another content provider!");
                }

                foreach (var r in db.Rentals.Where(r => r.MovieID == movie.ID))
                {
                    db.Rentals.Remove(r);
                }

                var filePath = Constants.UploadDownloadFileFolder + movie.FilePath;

                db.Movies.Remove(movie);
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
        /// <param name="search">The search string.</param>
        /// <returns>An IEnumerable containing the movies fitting the search.</returns>
        public static IEnumerable<Movie> Search(string search)
        {
            Contract.Requires<ArgumentNullException>(search != null);

            var searchTitle = search.ToLower();
            var components = searchTitle.Split(' ');

            IEnumerable<Movie> movies;

            using (var db = new RentItContext())
            {
                movies = db.Movies.ToList();
            }

            return from movie in movies
                   let title = movie.Title.ToLower()
                   let titleComponents = title.Split(' ')
                   where titleComponents.Any(str => components.Any(str.Contains))
                   orderby title.Equals(searchTitle) descending, titleComponents.Count(str => components.Any(str.Equals)) descending
                   select movie;
        }

        /// <summary>
        /// Filters the list of movies into a particular genre.
        /// </summary>
        /// <param name="genre">The genre to filter by.</param>
        /// <returns>An IEnumerable containing the filtered movies.</returns>
        public static IEnumerable<Movie> ByGenre(Genre genre)
        {
            Contract.Requires<ArgumentNullException>(genre != null);

            List<Movie> result;

            using (var db = new RentItContext())
            {
                result = db.Movies.ToList().Where(movie => movie.Genres.Any(dbgenre => dbgenre.Equals(genre))).ToList();
            }

            if (!result.Any())
            {
                throw new UnknownGenreException();
            }

            return result;
        }

        /// <summary>
        /// Returns all genres in the database
        /// </summary>
        /// <returns>An IEnumerable containing all genres as strings</returns>
        public static IEnumerable<Genre> GetAllGenres()
        {
            using (var db = new RentItContext())
            {
                return db.Genres.ToList();
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
                var movies = db.Movies.OrderByDescending(m => m.ID).ToList(); // TODO: Add release date to movies.

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

        /// <summary>
        /// Returns a list of the 10 most rented movies.
        /// </summary>
        /// <param name="token">The session token.</param>
        /// <returns>A list of movie objects.</returns>
        public static IEnumerable<Movie> MostDownloaded(string token)
        {
            using (var db = new RentItContext())
            {
                List<MovieDownload> md = new List<MovieDownload>();
                foreach (Movie m in db.Movies)
                {
                    md.Add(new MovieDownload(m, m.Rentals.Count));
                }

                List<Movie> movies = new List<Movie>();
                for (int i = 0; i < 10; i++)
                {
                    MovieDownload m = md.Max();
                    md.Remove(m);
                    movies.Add(m.Movie);
                }

                return movies;
            }
        }

        /// <summary>
        /// Registers a movie for later upload.
        /// </summary>
        /// <param name="token">The session token.</param>
        /// <param name="movieObject">The movie object to be registered.</param>
        public static void RegisterMovie(string token, Movie movieObject)
        {
            Contract.Requires<ArgumentNullException>(token != null);

            Contract.Requires<ArgumentNullException>(movieObject != null);
            Contract.Requires<ArgumentNullException>(
                movieObject.Description != null & movieObject.Title != null);

            Contract.Requires<InsufficientRightsException>(User.GetByToken(token).Type == UserType.ContentProvider);

            using (var db = new RentItContext())
            {
                var newMovie = new Movie
                    {
                        Description = movieObject.Description,
                        Title = movieObject.Title,
                        FilePath = "emptyFilePath",
                        OwnerID = User.GetByToken(token).ID
                    };
                db.Movies.Add(newMovie);
                db.SaveChanges();
            }
        }
    }
}