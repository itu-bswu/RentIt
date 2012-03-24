namespace MelnykPersonalTesting
{
    using System.IO;

    class Program
    {
        static void Main(string[] args)
        {
            string filePath = Path.Combine(@"C:\Users\Melnyk\Desktop", @"TestFile.txt");
            FileInfo fileInfo = new FileInfo(filePath);
            FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            UpDownloadServiceClient udsc = new UpDownloadServiceClient();

            udsc.UploadFile(@"TestFile.txt", fileInfo.Length, stream);
        }
    }
}
