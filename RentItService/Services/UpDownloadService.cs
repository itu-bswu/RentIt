namespace RentItService.Services
{
    using System;
    using System.IO;

    using RentItService.Interfaces;
    using RentItService.NeedsRename;

    using Tools;

    /// <summary>
    /// TODO: Write summary
    /// </summary>
    public partial class Service : IUpDownloadService
    {
        /// <summary>
        /// TODO: Write summary
        /// </summary>
        /// <param name="downloadRequest">The file to be downloaded.</param>
        /// <returns>TODO: Write return</returns>
        /// <author>Jakob Melnyk</author>
        public RemoteFileStream DownloadFile(FileRequest downloadRequest)
        {
            try
            {
                string filePath = Path.Combine(Constants.UploadDownloadFileFolder, downloadRequest.FileName);
                FileInfo fileInfo = new FileInfo(filePath);

                // Check to see if file exists.
                if (!fileInfo.Exists)
                {
                    throw new FileNotFoundException("File not found", downloadRequest.FileName);
                }

                // Open stream
                FileStream stream = new FileStream(
                    filePath, FileMode.Open, FileAccess.Read);

                // Set up rfs
                return new RemoteFileStream(downloadRequest.FileName, fileInfo.Length, stream);
            }
            catch (Exception e)
            {
                throw new Exception("Could not create the stream.", e);
            }
        }

        /// <summary>
        /// TODO: Write summary
        /// </summary>
        /// <param name="uploadRequest">The file name and the stream to upload.</param>
        public void UploadFile(RemoteFileStream uploadRequest)
        {
            FileStream targetStream;
            Stream sourceStream = uploadRequest.FileByteStream;

            string filePath = Path.Combine(Constants.UploadDownloadFileFolder, uploadRequest.FileName);

            using (targetStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                const int BufferLength = 8192;
                byte[] buffer = new byte[BufferLength];
                int count;
                while ((count = sourceStream.Read(buffer, 0, BufferLength)) > 0)
                {
                    targetStream.Write(buffer, 0, count);
                }

                targetStream.Close();
                sourceStream.Close();
            }
        }
    }
}