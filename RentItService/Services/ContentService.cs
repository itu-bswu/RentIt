// -----------------------------------------------------------------------
// <copyright file="ContentService.cs" company="">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace RentItService.Services
{
    using System;
    using System.Linq;

    using RentItService.Entities;
    using RentItService.Enums;
    using RentItService.Interfaces;

    /// <summary>
    /// Service for the content providers.
    /// </summary>
    public partial class Service : IContentService
    {
        /// <summary>
        /// Operation used to update movie information.
        /// </summary>
        /// <param name="token">The user token.</param>
        /// <param name="movieObject">The Movie object containing the ID of the movie to be changed and the updated information.</param>
        /// <exception cref="NotImplementedException">Not Yet Implemented.</exception>
        /// <author></author>
        public void EditMovieInformation(string token, Movie movieObject)
        {
            // TODO: Implement EditMovieInformation
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Deletes a movie from the service.
        /// </summary>
        /// <param name="token">The user token.</param>
        /// <param name="movieObject">The movie to be deleted.</param>
        /// <author></author>
        public void DeleteMovie(string token, Movie movieObject)
        {
            User user = User.GetByToken(token);
            if (user.Type != UserType.ContentProvider & user.Type != UserType.SystemAdmin)
            {
                throw new Exception(); // TODO: Throw better exception
            }

            using (var db = new RentItContext())
            {
                foreach (var r in db.Rentals.Where(r => r.MovieID == movieObject.ID))
                {
                    db.Rentals.Remove(r);
                }

                db.Movies.Remove(db.Movies.First(m => m.ID == movieObject.ID));
                db.SaveChanges();
            }
        }
    }
}