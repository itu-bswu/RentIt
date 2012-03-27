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

    /// <summary>
    /// Contains the information necessary to up/down load a file.
    /// </summary>
    /// <author>Jakob Melnyk</author>
    [MessageContract]
    public class RemoteFileStream : IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RemoteFileStream"/> class. 
        /// </summary>
        /// <author>Jakob Melnyk</author>
        public RemoteFileStream()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoteFileStream"/> class. 
        /// </summary>
        /// <param name="name">
        /// The name of the file.
        /// </param>
        /// <param name="l">
        /// The length of the stream.
        /// </param>
        /// <param name="stream">
        /// The stream.
        /// </param>
        /// <author>Jakob Melnyk</author>
        public RemoteFileStream(string name, long l, Stream stream)
        {
            this.FileName = name;
            this.Length = l;
            this.FileByteStream = stream;
        }

        #region Fields

        /// <summary>
        /// Gets the location of the file on the source system.
        /// </summary>
        /// <author>Jakob Melnyk</author>
        [MessageHeader(MustUnderstand = true)]
        public string FileName { get; private set; }

        /// <summary>
        /// Gets the length of the file used in the stream.
        /// </summary>
        /// <author>Jakob Melnyk</author>
        [MessageHeader(MustUnderstand = true)]
        public long Length { get; private set; }

        /// <summary>
        /// Gets the stream used to up/down load the file.
        /// </summary>
        /// <author>Jakob Melnyk</author>
        [MessageBodyMember(Order = 1)]
        public Stream FileByteStream { get; private set; }
        #endregion

        /// <summary>
        /// Disposes of the stream. //TODO: Needs more precise summary.
        /// </summary>
        /// <author>Jakob Melnyk</author>
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