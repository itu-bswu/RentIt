//-------------------------------------------------------------------------------------------------
// <copyright file="RemoteFileStream.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentItService.Library
{
    using System;
    using System.IO;
    using System.Runtime.Serialization;

    /// <summary>
    /// Contains the information necessary to up/down load a file.
    /// </summary>
    /// <author>Jakob Melnyk</author>
    [DataContract]
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
        /// <param name="name">The name of the file.</param>
        /// <param name="length">The length of the stream.</param>
        /// <param name="stream">The stream.</param>
        public RemoteFileStream(string name, long length, Stream stream)
        {
            this.FileName = name;
            this.Length = length;
            this.FileByteStream = stream;
        }

        #endregion Constructor(s)
        
        #region Properties

        /// <summary>
        /// Gets the location of the file on the source system.
        /// </summary>
        [DataMember]
        public string FileName { get; private set; }

        /// <summary>
        /// Gets the length of the file used in the stream.
        /// </summary>
        [DataMember]
        public long Length { get; private set; }

        /// <summary>
        /// Gets the stream used to up/down load the file.
        /// </summary>
        [DataMember]
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