namespace RentItClient.GUI.ContentProvider
{
    using System.IO;
    using System.Windows;
    using Types;
    using ViewModels.ProviderViewModels;

    /// <summary>
    /// Interaction logic for CPUploadEditionPage.xaml
    /// </summary>
    public partial class CPUploadEditionPage
    {
        #region Fields

        /// <summary>
        /// The movie associated with the edition to upload.
        /// </summary>
        private readonly Movie associatedMovie;
        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CPUploadEditionPage"/> class.
        /// </summary>
        /// <param name="movie">
        /// The movie.
        /// </param>
        public CPUploadEditionPage(Movie movie)
            : this()
        {
            associatedMovie = movie;
            movieTextBox.Text = associatedMovie.Title;
        }

        private CPUploadEditionPage()
        {
            InitializeComponent();
        }
        #endregion

        #region Click methods

        private void YourMovies(object sender, RoutedEventArgs e)
        {
            if (MainWindow.ChangeWindow())
            {
                NavigationService.Navigate(new CPYourMoviesPage());
            }
        }

        private void RegisterMovieClick(object sender, RoutedEventArgs e)
        {
            if (MainWindow.ChangeWindow())
            {
                NavigationService.Navigate(new CPRegisterMoviePage());
            }
        }

        private void LogoutClick(object sender, RoutedEventArgs e)
        {
            if (MainWindow.LogOut())
            {
                NavigationService.Navigate(new LoginPage());
            }
        }

        private void UploadEditionClick(object sender, RoutedEventArgs e)
        {
            var fi = new FileInfo(textBoxFiletoUpload.Text);
            if (CPUploadEditionViewModel.UploadEdition(associatedMovie, textBoxTitle.Text, fi))
            {
                MessageBox.Show("Edition was uploaded successfully!");
                NavigationService.Navigate(new CPViewMoviePage(associatedMovie.ID));
            }
            else
            {
                MessageBox.Show("Something went wrong when trying to edit the movie. " +
                                "This error may occur if there is something wrong with the data you input" +
                                " or if there is a problem with the service." +
                                "\n If all data appears ok, please restart the client.");
            }
        }

        private void BrowseClick(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog
            var dlg = new Microsoft.Win32.OpenFileDialog { DefaultExt = ".avi", Filter = "movie files (.avi)|*.avi" };

            // Display OpenFileDialog by calling ShowDialog method
            var result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox
            if (result.Value)
            {
                // Open document
                textBoxFiletoUpload.Text = dlg.FileName;
            }
        }
        #endregion
    }
}
