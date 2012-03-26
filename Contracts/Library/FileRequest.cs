//-------------------------------------------------------------------------------------------------
// <copyright file="FileRequest.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace Contracts.Library
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
        /// Initializes a new instance of the <see cref="FileRequest"/> class.
        /// </summary>
        /// <param name="fileName">
        /// The name of the file.
        /// </param>
        /// <author>Jakob Melnyk</author>
        public FileRequest(string fileName)
        {
            this.FileName = fileName;
        }

        /// <summary>
        /// Gets the name of the desired file.
        /// </summary>
        /// <author>Jakob Melnyk</author>
        [MessageBodyMember]
        public string FileName { get; private set; }
    }
}