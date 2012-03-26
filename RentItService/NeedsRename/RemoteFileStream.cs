namespace RentItService.NeedsRename
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
        #region Fields

        /// <summary>
        /// Location of the file on the source system.
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public string FileName { get; private set; }

        /// <summary>
        /// The length of the file used in the stream.
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public long Length { get; private set; }

        /// <summary>
        /// The stream used to up/down load the file.
        /// </summary>
        [MessageBodyMember(Order = 1)]
        public Stream FileByteStream { get; private set; }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoteFileStream"/> class. 
        /// </summary>
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
        public RemoteFileStream(string name, long l, Stream stream)
        {
            FileName = name;
            Length = l;
            FileByteStream = stream;
        }

        /// <summary>
        /// Disposes of the stream. //TODO: Needs more precise summary.
        /// </summary>
        public void Dispose()
        {
            if (FileByteStream != null)
            {
                FileByteStream.Close();
                FileByteStream = null;
            }
        }
    }
}