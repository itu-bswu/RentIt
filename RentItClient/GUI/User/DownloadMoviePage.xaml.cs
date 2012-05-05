namespace RentItClient.GUI.User
{
    using System.Windows;
    using System.Windows.Forms;

    using RentItClient.Types;
    using RentItClient.ViewModels;
    using RentItClient.ViewModels.UserViewModels;

    /// <summary>
    /// Interaction logic for DownloadMoviePage.xaml
    /// </summary>
    public partial class DownloadMoviePage
    {
        /// <summary>
        /// The movie being displayed.
        /// </summary>
        private readonly Movie movie;

        /// <summary>
        /// The text to dispaly if movie is not yet released.
        /// </summary>
        private const string NotYetReleased = "Not yet released";

        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadMoviePage"/> class.
        /// </summary>
        /// <param name="movieId">The id of the movie to show.</param>
        public DownloadMoviePage(int movieId)
            : this()
        {
            movie = ViewMovieViewModel.GetMovieInfo(movieId);
            textBoxDescription.Text = movie.Description;
            textBlockRelease.Text = movie.ReleaseDate.Year != 0001 ? movie.ReleaseDate.ToLongDateString() : NotYetReleased;
            textBlockTitle.Text = movie.Title;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="DownloadMoviePage"/> class from being created.
        /// </summary>
        private DownloadMoviePage()
        {
            InitializeComponent();
        }

        private void MostRented(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MostRentedPage());
        }

        private void ViewProfile(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ViewProfilePage());
        }

        private void YourRentals(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RentalHistory());
        }

        private void SearchClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ViewMovieListPage(MasterViewModel.Search(textBoxSearch.Text)));
        }

        private void LogoutClick(object sender, RoutedEventArgs e)
        {
            MasterViewModel.LogOut();
            NavigationService.Navigate(new LoginPage());
        }

        private void DownloadMovieClick(object sender, RoutedEventArgs e)
        {
            if (!textBlockRelease.Text.Equals(NotYetReleased))
            {
                string path;
                var dialog = new FolderBrowserDialog();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    path = dialog.SelectedPath;
                }
                else
                {
                    return;
                }

                DownloadMovieViewModel.DownloadMovie(movie.ID, path);
                NavigationService.Navigate(new RentalHistory());
            }
        }
    }
}
