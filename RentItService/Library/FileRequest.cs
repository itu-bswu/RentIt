//-------------------------------------------------------------------------------------------------
// <copyright file="FileRequest.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentItService.Library
{
    using System.ServiceModel;

    /// <summary>
    /// Used to request an up/download.
    /// </summary>
    /// <author>Jakob Melnyk</author>
    [MessageContract]
    public class FileRequest
    {
        /// <summary>
        /// Name of the desired file.
        /// </summary>
        [MessageBodyMember]
        public string FileName { get; private set; }
    }
}