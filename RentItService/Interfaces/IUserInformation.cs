// -----------------------------------------------------------------------
// <copyright file="IUserInformation.cs" company="">
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
    public interface IUserInformation
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userObject"></param>
        /// <returns></returns>
        [OperationContract]
        string SignUp(User userObject);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [OperationContract]
        string LogIn(string userName, string password);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="userObject"></param>
        [OperationContract]
        void EditProfile(string token, User userObject);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [OperationContract]
        IEnumerable<Movie> GetRentalHistory(string token);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [OperationContract]
        IEnumerable<Movie> GetCurrentRentals(string token);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="movieId"></param>
        [OperationContract]
        void RentMovie(string token, int movieId);
    }
}
