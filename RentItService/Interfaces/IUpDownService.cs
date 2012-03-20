namespace RentItService.Interfaces
{
    using System;
    using System.ServiceModel;

    /// <summary>
    /// Interface of the download service.
    /// </summary>
    /// <author>Jakob Melnyk</author>
    [ServiceContract]
    public interface IUpDownloadService
    {
        /// <summary>
        /// Creates a stream for downloading a file from the server.
        /// </summary>
        /// <param name="dlRequest">The download request.</param>
        /// <returns>The stream information necessary for download.</returns>
        [OperationContract]
        RemoteFileStream DownloadFile(FileRequest dlRequest);

        /// <summary>
        /// Accepts a RemoteFileStream and saves the file to the server.
        /// </summary>
        /// <param name="ulRequest">The upload request</param>
        [OperationContract]
        void UploadFile(RemoteFileStream ulRequest);
    }

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
        public string FileName;
    }

    /// <summary>
    /// Contains the information necessary to up/down load a file.
    /// </summary>
    /// <author>Jakob Melnyk</author>
    [MessageContract]
    public class RemoteFileStream : IDisposable
    {
        /// <summary>
        /// Location of the file on the source system.
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public string FileName;

        /// <summary>
        /// The length of the file used in the stream.
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public long Length;

        /// <summary>
        /// The stream used to up/down load the file.
        /// </summary>
        [MessageBodyMember(Order = 1)]
        public System.IO.Stream FileByteStream;

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