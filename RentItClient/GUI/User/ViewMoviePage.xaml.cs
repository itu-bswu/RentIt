using System.Windows.Controls;

namespace RentItClient
{
    using System.Windows;

    using RentItClient.GUI.User;
    using RentItClient.ViewModels;

    /// <summary>
    /// Interaction logic for ViewMoviePage.xaml
    /// </summary>
    public partial class ViewMoviePage : Page
    {
        /// <summary>
        /// The id of the movie being displayed.
        /// </summary>
        private int movieId;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewMoviePage"/> class.
        /// </summary>
        /// <param name="mId">
        /// The id of the movie to display.
        /// </param>
        public ViewMoviePage(int mId)
            : this()
        {
            movieId = mId;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="ViewMoviePage"/> class from being created.
        /// </summary>
        private ViewMoviePage()
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

        private void RentMovieClick(object sender, System.Windows.RoutedEventArgs e)
        {
            //TODO: skal tage det element som pagen er blevet oprettet med og tilføje det til brugerens liste og lejet film
            NavigationService.Navigate(new DownloadMoviePage(movieId));
        }
    }
}
