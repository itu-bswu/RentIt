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
        /// Gets or sets the release date.
        /// </summary>
        public DateTime? Released { get; set; }

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
            Contract.Ensures(Genres.Count(g => g.Name.Equals(genre.Name)) == 1);

            if (!Genres.Any(g => g.Name == genre.Name))
            {
                Genres.Add(genre);
            }
        }

        /// <summary>
        /// Remove a genre from the movie
        /// </summary>
        /// <param name="genre">The Genre</param>
        public void RemoveGenre(Genre genre)
        {
            Contract.Ensures(Genres.Count(g => g.Name.Equals(genre.Name)) == 0);

            if (Genres.Any(g => g.Name == genre.Name))
            {
                Genres.Remove(genre);
            }
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
        /// <param name="limit">The maximum number of entries to return.</param>
        /// <returns>An IEnumerable containing the movies fitting the search.</returns>
        public static IEnumerable<Movie> Search(string search, int limit = 0)
        {
            Contract.Requires<ArgumentNullException>(search != null);

            var searchTitle = search.ToLower();
            var components = searchTitle.Split(' ');

            IEnumerable<Movie> movies;

            using (var db = new RentItContext())
            {
                movies = db.Movies.ToList();
            }

            var result = from movie in movies
                         let title = movie.Title.ToLower()
                         let titleComponents = title.Split(' ')
                         where titleComponents.Any(str => components.Any(str.Contains))
                         || titleComponents.Any(str => components.Any(str2 => str.DifferenceTo(str2) <= Math.Max(str.Length, str2.Length) / 4))
                         orderby title.Equals(searchTitle) descending, titleComponents.Count(str => components.Any(str.Equals)) descending
                         select movie;

            if (limit == 0)
            {
                return result;
            }

            return result.Take(limit);
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
                // Warning: ugly fix
                result = db.Movies.Include("Genres").ToList().Where(movie => movie.Genres.Any(dbgenre => dbgenre.Equals(genre))).ToList();
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
        /// Returns the newest released movies.
        /// </summary>
        /// <param name="limit">The maximum amount of movies to return (0 = unlimited).</param>
        /// <returns>An IEnumerable of the newest movies.</returns>
        public static IEnumerable<Movie> Newest(int limit = 0)
        {
            Contract.Requires<ArgumentException>(limit >= 0);

            using (var db = new RentItContext())
            {
                var movies = (from movie in db.Movies
                              where movie.Released <= DateTime.Now
                              orderby movie.Released descending
                              select movie).ToList();

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
        /// Returns a list of the most rented movies.
        /// </summary>
        /// <param name="token">The session token.</param>
        /// <param name="limit">The maximum number of entries to return.</param>
        /// <returns>A list of movie objects.</returns>
        public static IEnumerable<Movie> MostDownloaded(string token, int limit = 0)
        {
            IList<Movie> movies;

            using (var db = new RentItContext())
            {
                var result = from movie in db.Movies
                             orderby movie.Rentals.Count() descending 
                             select movie;

                movies = result.ToList();
            }

            if (limit == 0)
            {
                return movies;
            }

            return movies.Take(limit);
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

                foreach (var genre in movieObject.Genres.Select(genre => Genre.GetOrCreateGenre(genre.Name)))
                {
                    newMovie.AddGenre(genre);
                }

                db.Movies.Add(newMovie);
                db.SaveChanges();
            }
        }
    }
}