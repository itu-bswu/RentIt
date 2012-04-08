// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ContentService.cs" company="">
//   
// </copyright>
// <summary>
//   Service for the content providers.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RentItService.Services
{
    using System;
    using System.Diagnostics.Contracts;

    using Entities;
    using Enums;
    using Interfaces;

    using RentItService.Exceptions;

    /// <summary>
    /// Service for the content providers.
    /// </summary>
    public partial class Service : IContentService
    {

        /// <summary>Operation used to update movie information.</summary>
        /// <param name="token">The user token.</param>
        /// <param name="movieObject">The Movie object containing the ID of the movie to be changed and the updated information.</param>
        /// <exception cref="NotImplementedException">Not Yet Implemented.</exception>
        /// <author>Jacob Grooss</author>
        public void EditMovieInformation(string token, Movie movieObject)
        {
            Contract.Requires(token != null);
            Contract.Requires<UserNotFoundException>(User.GetByToken(token) != null);
            Contract.Requires(movieObject.Description != null);
            Contract.Requires(movieObject.ImagePath != null);
            Contract.Requires(movieObject.Title != null);
            Contract.Requires(movieObject.Genre != null);
            Contract.Requires<InsufficientAccessLevelException>(User.GetByToken(token).Type != UserType.User);

            User user = User.GetByToken(token);

            using (var db = new RentItContext())
            {
                if (db.Movies.Find(movieObject.ID) == null)
                {
                    throw new NoMovieFoundException();
                }

                var movie = db.Movies.Find(movieObject.ID);

                movie.Description = movieObject.Description;
                movie.ImagePath = movieObject.ImagePath;
                movie.Title = movieObject.Title;
                movie.Genre = movieObject.Genre;

                db.Movies.Remove(db.Movies.Find(movieObject.ID));
                db.SaveChanges();

                db.Movies.Add(movie);
                db.SaveChanges();
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
        public void DeleteMovie(string token, Movie movieObject)
        {
            Contract.Requires<ArgumentNullException>(token != null);
            Contract.Requires<ArgumentNullException>(movieObject != null);

            Contract.Requires<InsufficientAccessLevelException>(User.GetByToken(token).Type == UserType.ContentProvider);

            Movie.DeleteMovie(token, movieObject);
        }
    }
}