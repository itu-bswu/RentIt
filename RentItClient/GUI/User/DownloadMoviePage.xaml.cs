using System.Windows.Controls;

namespace RentItClient
{
    using System.Windows;

    using RentItClient.GUI.User;
    using RentItClient.Types;
    using RentItClient.ViewModels;
    using RentItClient.ViewModels.UserViewModels;

    /// <summary>
    /// Interaction logic for DownloadMoviePage.xaml
    /// </summary>
    public partial class DownloadMoviePage : Page
    {
        /// <summary>
        /// The movie being displayed.
        /// </summary>
        private readonly Movie movie;

        /// <summary>
        /// The text to dispaly if movie is not yet released.
        /// </summary>
        private readonly const string NotYetReleased = "Not yet released";

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
            NavigationService.Navigate(new ViewMovieListPage(MasterViewModel.Search(this.textBoxSearch.Text)));
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
                //TODO: skal tage det object siden er blevet startet om med og hente dets fil.
            }
            else
            {

            }
        }
    }
}
