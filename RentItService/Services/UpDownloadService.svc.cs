namespace RentItService.Services
{
    using Interfaces;

    /// <summary>
    /// 
    /// </summary>
    public class UpDownloadService : IUpDownloadService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dlRequest"></param>
        /// <returns></returns>
        public RemoteFileStream DownloadFile(FileRequest dlRequest)
        {
            // TODO: Implement
            return new RemoteFileStream();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ulRequest"></param>
        public void UploadFile(RemoteFileStream ulRequest)
        {
            // TODO: Implement
        }
    }
}