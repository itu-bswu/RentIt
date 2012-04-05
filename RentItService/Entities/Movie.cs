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

                db.Movies.Remove(db.Movies.First(m => m.ID == movieObject.ID));
                db.SaveChanges();

                var filePath = Constants.UploadDownloadFileFolder + db.Movies.First(m => m.ID == movieObject.ID).FilePath;

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
        }
    }
}