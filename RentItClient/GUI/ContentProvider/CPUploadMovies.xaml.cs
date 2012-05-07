using System.Windows;
using System.Windows.Controls;

namespace RentItClient
{
    using System;

    /// <summary>
    /// Interaction logic for CPUploadMovies.xaml
    /// </summary>
    public partial class CPUploadMovies : Page
    {
        public CPUploadMovies()
        {
            InitializeComponent();
        }

        private void yourMovies(object sender, RoutedEventArgs e)
        {
            //TODO: metoden skal give en CPs liste af oploaded film med hver gang den her knap bliver trykket
            //TODO: så Listboxen i CPYourMovies kan blive lavet med de elementer
            CPYourMovies yourMovies = new CPYourMovies();
            this.NavigationService.Navigate(yourMovies);
        }

        private void registerMovie(object sender, RoutedEventArgs e)
        {
            CPRegisterMovie registerMovie = new CPRegisterMovie();
            this.NavigationService.Navigate(registerMovie);
        }

        private void logout(object sender, RoutedEventArgs e)
        {

        }

        private void uploadMovieClick(object sender, RoutedEventArgs e)
        {
            //TODO: Upload movie to database
            //TODO: du skal hente data fra TextBox: textBoxTitle, textBoxGenre, textBoxDescription, textBoxFiletoUpload, textBoxCoverImage
        }

        private void browseClick(object sender, RoutedEventArgs e)
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
                textBoxFiletoUpload.Text = filename;
            }
        }

        private void browseImageClick(object sender, RoutedEventArgs e)
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
                textBoxCoverImage.Text = filename;
            }
        }
    }
}
