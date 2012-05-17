//-------------------------------------------------------------------------------------------------
// <copyright file="FileRequest.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentItService.Library
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Used to request an up/download.
    /// </summary>
    /// <author>Jakob Melnyk</author>
    [DataContract]
    public class FileRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileRequest"/> class.
        /// </summary>
        /// <param name="fileName">The name of the file.</param>
        public FileRequest(string fileName)
        {
            this.FileName = fileName;
        }

        /// <summary>
        /// Gets the name of the desired file.
        /// </summary>
        /// <author>Jakob Melnyk</author>
        [DataMember]
        public string FileName { get; private set; }
    }
}