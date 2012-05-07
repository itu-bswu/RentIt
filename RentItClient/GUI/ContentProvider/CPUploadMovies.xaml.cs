namespace RentItClient.GUI.ContentProvider
{
    using System;
    using System.Windows;

    /// <summary>
    /// Interaction logic for CPUploadMovies.xaml
    /// </summary>
    public partial class CPUploadMovies
    {
        public CPUploadMovies()
        {
            this.InitializeComponent();
        }

        private void YourMovies(object sender, RoutedEventArgs e)
        {
            //TODO: metoden skal give en CPs liste af oploaded film med hver gang den her knap bliver trykket
            //TODO: så Listboxen i CPYourMovies kan blive lavet med de elementer
            CPYourMovies yourMovies = new CPYourMovies();
            this.NavigationService.Navigate(yourMovies);
        }

        private void RegisterMovie(object sender, RoutedEventArgs e)
        {
            CPRegisterMovie registerMovie = new CPRegisterMovie();
            this.NavigationService.Navigate(registerMovie);
        }

        private void Logout(object sender, RoutedEventArgs e)
        {

        }

        private void UploadMovieClick(object sender, RoutedEventArgs e)
        {
            //TODO: Upload movie to database
            //TODO: du skal hente data fra TextBox: textBoxTitle, textBoxGenre, textBoxDescription, textBoxFiletoUpload, textBoxCoverImage
        }

        private void BrowseClick(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension
            dlg.DefaultExt = ".avi";
            dlg.Filter = "movie files (.avi)|*.avi";

            // Display OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox
            if (result == true)
            {
                // Open document
                string filename = dlg.FileName;
                this.textBoxFiletoUpload.Text = filename;
            }
        }

        private void BrowseImageClick(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension
            dlg.DefaultExt = ".png, .jpg";
            dlg.Filter = "Pictures (.png, .jpg)|*.png, .jpg";

            // Display OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox
            if (result == true)
            {
                // Open document
                string filename = dlg.FileName;
                this.textBoxCoverImage.Text = filename;
            }
        }
    }
}
