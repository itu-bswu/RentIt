namespace RentItService
{
    using System;
    using System.ServiceModel;
    using System.ServiceModel.Description;
    using RentItService.Services;

    /// <summary>
    /// TODO: Document Program
    /// </summary>
    public class Program
    {
        /// <summary>
        /// TODO: Document BaseAddress
        /// </summary>
        private static readonly Uri BaseAddress = new Uri("localhost:8080/RentItService");

        /// <summary>
        /// TODO: Document UpDownloadHost
        /// </summary>
        private static readonly ServiceHost UpDownloadHost = new ServiceHost(typeof(UpDownloadService), BaseAddress);

        /// <summary>
        /// TODO: Document main
        /// </summary>
        /// <param name="args"></param>
        static public void Main(string[] args)
        {
            try
            {
                //ServiceMetadataBehavior
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                UpDownloadHost.Description.Behaviors.Add(smb);

                //Open the service
                UpDownloadHost.Open();
                Console.WriteLine("The service is ready.");
                Console.WriteLine("Press <ENTER> to terminate service.");
                Console.WriteLine();
                Console.ReadLine();

                // Closing the service
                UpDownloadHost.Close();

            }
            catch (CommunicationException)
            {
                //TODO: Implement catch of communication exception
            }
        }
    }
}