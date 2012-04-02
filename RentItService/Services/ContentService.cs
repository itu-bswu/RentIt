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

    using RentItService.Entities;
    using RentItService.Enums;
    using RentItService.Exceptions;
    using RentItService.Interfaces;

    /// <summary>
    /// Service for the content providers.
    /// </summary>
    public partial class Service : IContentService
    {

        /// <summary>Operation used to update movie information.</summary>
        /// <param name="token">The user token.</param>
        /// <param name="movieObject">The Movie object containing the ID of the movie to be changed and the updated information.</param>
        /// <exception cref="NotImplementedException">Not Yet Implemented.</exception>
        /// <author></author>
        public void EditMovieInformation(string token, Movie movieObject)
        {
            Contract.Requires(token != null);
            Contract.Requires<UserNotFoundException>(User.GetByToken(token) != null);
            Contract.Requires<UserNotFoundException>(User.GetByToken(token).Type == UserType.ContentProvider || User.GetByToken(token).Type == UserType.SystemAdmin);
            Contract.Requires(movieObject.Description != null);
            Contract.Requires(movieObject.ImagePath != null);
            Contract.Requires(movieObject.Title != null);
            Contract.Requires(movieObject.Genre != null);

            User user = User.GetByToken(token);

            using (var db = new RentItContext())
            {
                var movie = db.Movies.Find(movieObject.ID);

                movie.Description = movieObject.Description;
                movie.ImagePath = movieObject.ImagePath;
                movie.Title = movieObject.Title;
                movie.Genre = movieObject.Genre;

                db.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes a movie from the service.
        /// </summary>
        /// <param name="token">The user token.</param>
        /// <param name="movieObject">The movie to be deleted.</param>
        /// <exception cref="NotImplementedException">Not Yet Implemented.</exception>
        /// <author></author>
        public void DeleteMovie(string token, Movie movieObject)
        {
            // TODO: Implement DeleteMovie
            throw new System.NotImplementedException();
        }
    }
}