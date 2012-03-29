// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ContentService.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the ContentService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RentItService.Services
{
    using System.Linq;

    using RentItService.Entities;
    using RentItService.Enums;
    using RentItService.Interfaces;

    /// <summary>
    /// The class that implements the methods needed to edit movie information 
    /// and delete movies.
    /// </summary>
    public class ContentService : IContentService
    {
        /// <summary>
        /// The database that holds the data.
        /// </summary>
        private RentItContext dbContext = new RentItContext();

        /// <summary>
        /// Edits the information of a movie
        /// </summary>
        /// <param name="token">
        /// The token that's used to verify whether a used is logged
        /// in or not.
        /// </param>
        /// <param name="movieObject">
        /// The movie object that holds the new information.
        /// </param>
        public void EditMovieInformation(string token, Movie movieObject)
        {
            User user = new User();

            if (user != null)
            {
                if (user.Type.Equals(UserType.ContentProvider) || user.Type.Equals(UserType.SystemAdmin))
                {
                    var movie = this.dbContext.Movies.Find(movieObject.ID);

                    movie.Description = movieObject.Description;
                    movie.ImagePath = movieObject.ImagePath;
                    movie.Title = movieObject.Title;
                    movie.Rentals = movieObject.Rentals;

                    this.dbContext.SaveChanges();
                }
            }
        }

        public void DeleteMovie(string token, Movie movieObject)
        {
            throw new System.NotImplementedException();
        }
    }
}