// -----------------------------------------------------------------------
// <copyright file="ContentService.cs" company="">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace RentItService.Services
{
    using System;
    using System.Linq;

    using Entities;
    using Enums;
    using Interfaces;

    /// <summary>
    /// Service for the content providers.
    /// </summary>
    public partial class Service : IContentService
    {
        /// <summary>
        /// Update information for a movie. 
        /// The movie is identified by the ID in the instance of the Movie class. 
        /// The other information is updated with the new values.
        /// </summary>
        /// <param name="token">The session token.</param>
        /// <param name="movieObject">The Movie object containing the ID of the movie to be changed and the updated information.</param>
        /// <exception cref="NotImplementedException">Not Yet Implemented.</exception>
        /// <author></author>
        public void EditMovieInformation(string token, Movie movieObject)
        {
            // TODO: Implement EditMovieInformation
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes a movie from the service. 
        /// The movie is identified by the ID in the instance of the Movie class. 
        /// The other properties in the Movie instance are ignored.
        /// </summary>
        /// <param name="token">The session token.</param>
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