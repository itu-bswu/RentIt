// -----------------------------------------------------------------------
// <copyright file="IGetMovieData.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace RentItService.Interfaces
{
    using System.Collections.Generic;
    using System.ServiceModel;

    using RentItService.Entities;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [ServiceContract]
    public interface IGetMovieData
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="movieId"></param>
        /// <returns></returns>
        [OperationContract]
        Movie GetMovieInformation(string token, int movieId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [OperationContract]
        IEnumerable<Movie> GetMostDownloaded(string token);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [OperationContract]
        IEnumerable<string> GetAllGenres(string token);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="genre"></param>
        /// <returns></returns>
        [OperationContract]
        IEnumerable<Movie> GetMoviesByGenre(string token, string genre);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        [OperationContract]
        IEnumerable<Movie> Search(string token, string search);
    }
}
