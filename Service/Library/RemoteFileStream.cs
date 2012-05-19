//-------------------------------------------------------------------------------------------------
// <copyright file="RemoteFileStream.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentItService.Library
{
    using System;
    using System.IO;
    using System.ServiceModel;
    using Entities;

    /// <summary>
    /// Contains the information necessary to up/down load a file.
    /// </summary>
    /// <author>Jakob Melnyk</author>
    [MessageContract]
    public class RemoteFileStream : IDisposable
    {
        #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoteFileStream"/> class. 
        /// </summary>
        public RemoteFileStream()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoteFileStream"/> class. 
        /// </summary>
        /// <param name="token">The user's token</param>
        /// <param name="name">The name of the file.</param>
        /// <param name="length">The length of the stream.</param>
        /// <param name="edition">The edition containing name and movie id.</param>
        /// <param name="stream">The stream.</param>
        public RemoteFileStream(string token, string name, long length, Edition edition, Stream stream)
        {
            this.Token = token;
            this.FileName = name;
            this.Length = length;
            this.Edition = edition;
            this.FileByteStream = stream;
        }

        #endregion Constructor(s)
        
        #region Properties

        /// <summary>
        /// Gets the location of the file on the source system.
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public string FileName { get; private set; }

        /// <summary>
        /// Gets the length of the file used in the stream.
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public long Length { get; private set; }

        /// <summary>
        /// Gets the user's token.
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public string Token { get; private set; }

        /// <summary>
        /// Gets the edition. 
        /// Must contain name and movie id. Rest will be ignored.
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public Edition Edition { get; private set; }

        /// <summary>
        /// Gets the stream used to up/down load the file.
        /// </summary>
        [MessageBodyMember(Order = 1)]
        public Stream FileByteStream { get; private set; }

        #endregion Properties

        /// <summary>
        /// Disposal of the stream.
        /// </summary>
        public void Dispose()
        {
            if (this.FileByteStream != null)
            {
                this.FileByteStream.Close();
                this.FileByteStream = null;
            }
        }
    }
}