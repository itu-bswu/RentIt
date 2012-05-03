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
        private ViewMoviePage()
        {
            InitializeComponent();
        }

        public ViewMoviePage(int movieId)
            : this()
        {

        }

        private void mostRented(object sender, RoutedEventArgs e)
        {
            //TODO: skal hente en liste over mest downloadet film og give den videre som parameter
            this.NavigationService.Navigate(new MostRentedPage());
        }

        private void viewProfile(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ViewProfilePage());
        }

        private void yourRentals(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new RentalHistory());
        }

        private void searchClick(object sender, RoutedEventArgs e)
        {
            //TODO: skal tage informationen fra textBoxSearch og så giv det videre til servicen så der kan sendes en liste af resultater til ViewMovieListPage
            this.NavigationService.Navigate(new ViewMovieListPage());
        }

        private void logoutClick(object sender, RoutedEventArgs e)
        {
            MasterViewModel.LogOut();
            this.NavigationService.Navigate(new LoginPage());
        }

        private void rentMovieClick(object sender, System.Windows.RoutedEventArgs e)
        {
            //TODO: skal tage det element som pagen er blevet oprettet med og tilføje det til brugerens liste og lejet film
            this.NavigationService.Navigate(new DownloadMoviePage());
        }
    }
}
