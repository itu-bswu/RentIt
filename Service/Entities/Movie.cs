//-------------------------------------------------------------------------------------------------
// <copyright file="Movie.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentItService.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Diagnostics.Contracts;
    using System.IO;
    using System.Linq;
    using Enums;
    using Exceptions;
    using Library;

    /// <summary>
    /// Movie entity (Entity Framework POCO class).
    /// </summary>
    public class Movie
    {
        #region Constructor(s)
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Movie"/> class.
        /// </summary>
        public Movie()
        {
            this.Genres = new List<Genre>();
            this.Editions = new List<Edition>();
        }

        #endregion Constructor(s)

        #region Properties

        /// <summary>
        /// Gets all the movies in the database.
        /// </summary>
        /// <returns>All the movie entries in the database.</returns>
        public static IEnumerable<Movie> All
        {
            get
            {
                return RentItContext.Db.Movies.Include("Editions").Include("Editions.Rentals").Include("Genres");
            }
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
        /// Gets or sets the release date.
        /// </summary>
        public DateTime? ReleaseDate { get; set; }

        /// <summary>
        /// Gets a value indicating whether or not a movie has been released.
        /// </summary>
        public bool Released
        {
            get
            {
                return (this.ReleaseDate != null && this.ReleaseDate <= DateTime.Now);
            }
        }

        /// <summary>
        /// Gets or sets the owner ID.
        /// </summary>
        public int OwnerID { get; set; }

        /// <summary>
        /// Gets or sets the owner of the movie.
        /// </summary>
        public virtual User Owner { get; set; }

        /// <summary>
        /// Gets or sets a list of movie editions.
        /// </summary>
        public virtual ICollection<Edition> Editions { get; set; } 

        /// <summary>
        /// Gets an enumerable of rentals of the movie.
        /// </summary>
        public IEnumerable<Rental> Rentals
        {
            get
            {
                return this.Editions.SelectMany(edition => edition.Rentals);
            }
        }

        /// <summary>
        /// Gets the movie's genres.
        /// </summary>
        public virtual ICollection<Genre> Genres { get; set; }

        #endregion Properties

        #region Static Methods

        /// <summary>
        /// Get movie information for movie with given ID.
        /// </summary>
        /// <param name="user">The user getting the movie information.</param>
        /// <param name="movieId">Movie to get information about.</param>
        /// <returns>Movie with given ID; null if not found.</returns>
        public static Movie Get(User user, int movieId)
        {
            Contract.Requires<ArgumentNullException>(user != null);

            var movie = All.FirstOrDefault(m => m.ID == movieId);
            if (movie != null && !movie.Released && movie.OwnerID != user.ID)
            {
                movie.Editions = new List<Edition>();
            }

            return movie;
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

            var result = from movie in All
                         let title = movie.Title.ToLower()
                         let titleComponents = title.Split(' ')
                         where titleComponents.Any(str => components.Any(str.Contains))
                         || titleComponents.Any(str => components.Any(str2 => str.DifferenceTo(str2) <= Math.Max(str.Length, str2.Length) / 4))
                         orderby title.Equals(searchTitle) descending, titleComponents.Count(str => components.Any(str.Equals)) descending
                         select movie;

            return (limit == 0 ? result : result.Take(limit));
        }

        /// <summary>
        /// Filters the list of movies into a particular genre.
        /// </summary>
        /// <param name="genre">The genre to filter by.</param>
        /// <returns>An IEnumerable containing the filtered movies.</returns>
        public static IEnumerable<Movie> ByGenre(string genre)
        {
            Contract.Requires<ArgumentNullException>(genre != null);

            return All.Where(movie => movie.Genres.Any(g => g.Name.Equals(genre))).ToList();
        }

        /// <summary>
        /// Returns the newest released movies.
        /// </summary>
        /// <param name="limit">The maximum amount of movies to return (0 = unlimited).</param>
        /// <returns>An IEnumerable of the newest movies.</returns>
        public static IEnumerable<Movie> Newest(int limit = 0)
        {
            Contract.Requires<ArgumentException>(limit >= 0);

            var movies = (from movie in All
                          where movie.ReleaseDate <= DateTime.Now
                          orderby movie.ReleaseDate descending
                          select movie).ToList();

            return (limit > 0 ? movies.Take(limit) : movies);
        }

        /// <summary>
        /// Returns a list of the most rented movies.
        /// </summary>
        /// <param name="limit">The maximum number of entries to return (0 = unlimited).</param>
        /// <returns>A list of movie objects.</returns>
        public static IEnumerable<Movie> MostDownloaded(int limit = 0)
        {
            var movies = (from movie in All
                          orderby movie.Editions.SelectMany(edition => edition.Rentals).Count() descending
                          select movie).ToList();

            return limit > 0 ? movies.Take(limit) : movies;
        }

        /// <summary>
        /// Get movies with specific parameters.
        /// </summary>
        /// <param name="sorting">How to sort the returned movies.</param>
        /// <param name="genre">What genre to limit to.</param>
        /// <param name="limit">The maximum number of elements to return (0 = no limit).</param>
        /// <returns>The found movies.</returns>
        public static IEnumerable<Movie> GetMovies(MovieSorting sorting = MovieSorting.Default, string genre = null, int limit = 0)
        {
            var movies = (sorting == MovieSorting.MostDownloaded ? MostDownloaded() : sorting == MovieSorting.Newest ? Newest(limit) : All);

            if (genre != null)
            {
                movies = movies.Where(movie => movie.Genres.Contains(Genre.GetOrCreateGenre(genre)));
            }

            return movies;
        }

        /// <summary>
        /// Registers a movie for later upload.
        /// </summary>
        /// <param name="user">The user registering the movie.</param>
        /// <param name="movieObject">The movie object to be registered.</param>
        /// <returns>Instance of Movie entity</returns>
        public static Movie Register(User user, Movie movieObject)
        {
            Contract.Requires<ArgumentNullException>(user != null);
            Contract.Requires<ArgumentNullException>(movieObject != null);
            Contract.Requires<ArgumentNullException>(movieObject.Title != null);
            Contract.Requires<ArgumentException>(movieObject.Title != string.Empty);
            Contract.Requires<InsufficientRightsException>(user.Type == UserType.ContentProvider);

            var newMovie = new Movie
            {
                Description = movieObject.Description,
                Title = movieObject.Title,
                OwnerID = user.ID,
                ReleaseDate = movieObject.ReleaseDate
            };

            foreach (var genre in movieObject.Genres.Select(genre => Genre.GetOrCreateGenre(genre.Name)))
            {
                newMovie.AddGenre(genre);
            }

            RentItContext.Db.Movies.Add(newMovie);
            RentItContext.Db.SaveChanges();

            return newMovie;
        }

        #endregion Static Methods

        #region Methods
        
        /// <summary>
        /// Add a genre to the movie.
        /// </summary>
        /// <param name="name">The genre to add.</param>
        public void AddGenre(string name)
        {
            Contract.Ensures(this.Genres.Count(g => g.Name.Equals(name)) == 1);

            if (!this.Genres.Any(g => g.Name.Equals(name)))
            {
                var genre = (Genre.HasGenre(name) ? RentItContext.Db.Genres.Single(g => g.Name.Equals(name)) : new Genre(name));

                this.Genres.Add(genre);

                RentItContext.Db.SaveChanges();
            }
        }

        /// <summary>
        /// Remove a genre from the movie
        /// </summary>
        /// <param name="genre">The Genre</param>
        public void RemoveGenre(string genre)
        {
            Contract.Ensures(this.Genres.Count(g => g.Name == genre) == 0);

            if (this.Genres.Any(g => g.Name.Equals(genre)))
            {
                this.Genres.Remove(this.Genres.Single(g => g.Name == genre));
            }
        }

        /// <summary>
        /// Checks if the movie has a genre
        /// </summary>
        /// <param name="genre">The genre</param>
        /// <returns>True if the movie has the genre</returns>
        public bool HasGenre(string genre)
        {
            return this.Genres.Count(g => g.Name == genre) == 1;
        }

        /// <summary>
        /// Creates a new movie edition with the information specified. 
        /// Uploading user and edition name must be set. RemoteFileStream must be 
        /// non-null and must contain FileByteStream and FileName. The uploading 
        /// user must be a content provider, and must be the owner of the movie.
        /// </summary>
        /// <param name="uploadingUser">The user uploading the file.</param>
        /// <param name="editionName">The name of the edition to be created.</param>
        /// <param name="uploadRequest">The upload remote file stream.</param>
        /// <returns>An instance of Edition entity containing all information about the new edition created.</returns>
        public Edition UploadEdition(User uploadingUser, string editionName, RemoteFileStream uploadRequest)
        {
            Contract.Requires<ArgumentNullException>(uploadingUser != null);
            Contract.Requires<ArgumentNullException>(editionName != null);
            Contract.Requires<ArgumentNullException>(uploadRequest != null);
            Contract.Requires<ArgumentNullException>(uploadRequest.FileByteStream != null);
            Contract.Requires<ArgumentNullException>(uploadRequest.FileName != null);
            Contract.Requires<InsufficientRightsException>(uploadingUser.Type == UserType.ContentProvider);
            Contract.Requires<InsufficientRightsException>(this.OwnerID == uploadingUser.ID);

            var movieFilePath = this.ID + "_" + this.Title + Path.GetExtension(uploadRequest.FileName);

            try
            {
                var filePath = Path.Combine(ConfigurationManager.AppSettings["BaseFilePath"], movieFilePath);
                FileStream targetStream;
                var sourceStream = uploadRequest.FileByteStream;
                using (targetStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    const int BufferLength = 8192;
                    var buffer = new byte[BufferLength];
                    int count;
                    while ((count = sourceStream.Read(buffer, 0, BufferLength)) > 0)
                    {
                        targetStream.Write(buffer, 0, count);
                    }

                    targetStream.Close();
                    sourceStream.Close();
                }
            }
            catch
            {
                return null;
            }

            var edition = new Edition
            {
                FilePath = movieFilePath,
                Name = editionName,
                MovieID = this.ID
            };

            this.Editions.Add(edition);
            RentItContext.Db.SaveChanges();

            return edition;
        }

        /// <summary>
        /// Edit a movie's information. Title, Description, ImagePath, ReleaseDate 
        /// and Genres will be updated. Movie will be identified by the ID in the 
        /// Movie instance passed to the method. The user editing must be the owner 
        /// of the movie, or a system administrator.
        /// </summary>
        /// <param name="user">User editing the movie. Must be the owner of the movie.</param>
        /// <param name="updatedMovie">Updated movie information.</param>
        public void Edit(User user, Movie updatedMovie)
        {
            Contract.Requires<ArgumentNullException>(user != null && updatedMovie != null);
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(updatedMovie.Title));
            Contract.Requires<InsufficientRightsException>((user.Type == UserType.ContentProvider && this.OwnerID == user.ID) ||
                                                            user.Type == UserType.SystemAdmin);

            foreach (var genre in updatedMovie.Genres)
            {
                Genre.GetOrCreateGenre(genre.Name);
            }

            this.Title = updatedMovie.Title;
            this.Description = updatedMovie.Description;
            this.ImagePath = updatedMovie.ImagePath;
            this.ReleaseDate = updatedMovie.ReleaseDate;

            var removeGenres = from genre in this.Genres
                               where !updatedMovie.Genres.Any(g => g.Name.Equals(genre.Name))
                               select Genre.All.Single(genre.Name.Equals);

            foreach (var genre in removeGenres.ToList())
            {
                this.RemoveGenre(genre);
            }

            foreach (var genre in updatedMovie.Genres)
            {
                this.AddGenre(Genre.All.Single(genre.Name.Equals));
            }

            RentItContext.Db.SaveChanges();
        }

        /// <summary>
        /// Deletes a movie from the service. User deleting the movie 
        /// must be a content provider and the owner of the movie. 
        /// System administrators can also delete movies.
        /// </summary>
        /// <param name="user">The user deleting the movie.</param>
        public void Delete(User user)
        {
            Contract.Requires<ArgumentNullException>(user != null);
            Contract.Requires<InsufficientRightsException>((user.Type == UserType.ContentProvider && this.OwnerID == user.ID) ||
                                                            user.Type == UserType.SystemAdmin);

            var files = this.Editions.Select(edition => edition.FilePath).ToList();

            RentItContext.Db.Movies.Remove(this);
            RentItContext.Db.SaveChanges();

            foreach (var filePath in files.Select(file => ConfigurationManager.AppSettings["BaseFilePath"] + file).Where(File.Exists))
            {
                File.Delete(filePath);
            }
        }

        #endregion Methods
    }
}