// -----------------------------------------------------------------------
// <copyright file="AdministrationModel.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace RentItClient.Models
{
    using RentItService;

    /// <summary>
    /// Contains the logic for administrative users of the client.
    /// </summary>
    public static class AdministrationModel
    {
        private static readonly UserInformationClient Uic = new UserInformationClient();
    }
}
