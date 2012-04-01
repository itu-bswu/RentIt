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

    using RentItService.Entities;
    using RentItService.Enums;
    using RentItService.Exceptions;
    using RentItService.Interfaces;

    /// <summary>
    /// Service for the content providers.
    /// </summary>
    public partial class Service : IContentService
    {
        /// <summary>
        /// The context that grants access to the database
        /// </summary>
        private RentItContext db = new RentItContext();

        /// <summary>
        /// Operation used to update movie information.
        /// </summary>
        /// <param name="token">The user token.</param>
        /// <param name="movieObject">The Movie object containing the ID of the movie to be changed and the updated information.</param>
        /// <exception cref="NotImplementedException">Not Yet Implemented.</exception>
        /// <author></author>
        public void EditMovieInformation(string token, Movie movieObject)
        {
            try
            {
                User user = User.GetByToken(token);

                if (user.Type.Equals(UserType.ContentProvider) || user.Type.Equals(UserType.SystemAdmin))
                {
                    var movie = this.db.Movies.Find(movieObject.ID);

                    movie.Description = movieObject.Description;
                    movie.ImagePath = movieObject.ImagePath;
                    movie.Title = movieObject.Title;
                    movie.Rentals = movieObject.Rentals;

                    this.db.SaveChanges();
                }
            }
            catch (UserNotFoundException)
            {
                //
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