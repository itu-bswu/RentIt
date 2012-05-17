//-------------------------------------------------------------------------------------------------
// <copyright file="Edition.cs" company="RentIt">
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
    /// Movie edition entity (Entity Framework POCO class).
    /// </summary>
    public class Edition
    {
        #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of the <see cref="Edition"/> class.
        /// </summary>
        public Edition()
        {
            this.Rentals = new List<Rental>();
        }

        #endregion Constructor(s)

        #region Properties
        
        /// <summary>
        /// Gets all editions.
        /// </summary>
        public static IEnumerable<Edition> All
        {
            get
            {
                return RentItContext.Db.Editions;
            }
        }

        /// <summary>
        /// Gets or sets the ID of the movie edition.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the name of the movie edition.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the filepath of the movie edition.
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Gets or sets the ID of the associated movie.
        /// </summary>
        public int MovieID { get; set; }

        /// <summary>
        /// Gets or sets the movie.
        /// </summary>
        public virtual Movie Movie { get; set; }

        /// <summary>
        /// Gets or sets a list of rentals for this movie edition.
        /// </summary>
        public virtual ICollection<Rental> Rentals { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Gets the edition with the provided ID.
        /// </summary>
        /// <param name="user">The user getting the information.</param>
        /// <param name="editionId">The ID of the edition to find.</param>
        /// <returns>The edition with the ID provided.</returns>
        public static Edition Get(User user, int editionId)
        {
            Contract.Requires<ArgumentNullException>(user != null);
            Contract.Requires<ArgumentException>(editionId > 0);

            var edition = RentItContext.Db.Editions.FirstOrDefault(e => e.ID == editionId);
            if (edition != null && !edition.Movie.Released && edition.Movie.OwnerID != user.ID)
            {
                return null;
            }

            return edition;
        }

        /// <summary>
        /// Creates a stream for downloading a file from the server. 
        /// The movie is identified by the ID in the instance of the Movie class.
        /// </summary>
        /// <param name="downloadingUser">The user downloading the file</param>
        /// <returns>Remote File Stream</returns>
        public RemoteFileStream Download(User downloadingUser)
        {
            Contract.Requires<ArgumentNullException>(downloadingUser != null);
            Contract.Requires<InsufficientRightsException>(downloadingUser.Rentals.Any(x => x.EditionID == this.ID & x.UserID == downloadingUser.ID));

            var filePath = Path.Combine(ConfigurationManager.AppSettings["BaseFilePath"], this.FilePath);
            var fileInfo = new FileInfo(filePath);

            // Check to see if file exists.
            if (!fileInfo.Exists)
            {
                throw new FileNotFoundException("File not found", fileInfo.Name);
            }

            // Open stream
            var stream = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read);

            // Set up rfs
            return new RemoteFileStream(this.FilePath, fileInfo.Length, stream);
        }

        /// <summary>
        /// Delete the current edition. The deleting user has to 
        /// be owner of the movie of which the edition belongs.
        /// </summary>
        /// <param name="deletingUser">The user deleting the movie edition.</param>
        public void Delete(User deletingUser)
        {
            Contract.Requires<ArgumentNullException>(deletingUser != null);
            Contract.Requires<InsufficientRightsException>(deletingUser.Type == UserType.ContentProvider);
            Contract.Requires<InsufficientRightsException>(this.Movie.OwnerID == deletingUser.ID);

            var filePath = ConfigurationManager.AppSettings["BaseFilePath"] + this.FilePath;

            RentItContext.Db.Editions.Remove(this);
            RentItContext.Db.SaveChanges();

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        #endregion Methods
    }
}