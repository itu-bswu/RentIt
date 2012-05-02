using System.Windows;
using System.Windows.Controls;

namespace RentItClient
{
    using RentItClient.GUI.User;

    /// <summary>
    /// Interaction logic for RentalHistory.xaml
    /// </summary>
    public partial class RentalHistory : Page
    {
        public RentalHistory()
        {
            InitializeComponent();
        }

        private void mostRented(object sender, RoutedEventArgs e)
        {
            //TODO: skal hente en liste over mest downloadet film og give den videre som parameter
            this.NavigationService.Navigate(new MostRentedPage());
        }

        private void viewProfile(object sender, RoutedEventArgs e)
        {
            //TODO: skal tjekke hvilken bruger der logget ind og så give vedkommendes personlige oplysninger med som parameter
            this.NavigationService.Navigate(new ViewProfilePage());
        }

        private void yourRentals(object sender, RoutedEventArgs e)
        {
            //TODO: skal tjekke hvilken bruger der logget ind og så give vedkommendes list af rentals med som parameter
            this.NavigationService.Navigate(new RentalHistory());
        }

        private void searchClick(object sender, RoutedEventArgs e)
        {
            //TODO: skal tage informationen fra textBoxSearch og så giv det videre til servicen så der kan sendes en liste af resultater til ViewMovieListPage
            this.NavigationService.Navigate(new ViewMovieListPage());
        }

        private void logoutClick(object sender, RoutedEventArgs e)
        {
            //TODO: skal lukke connectionen til servicen ned
            this.NavigationService.Navigate(new LoginPage());
        }

        private void ViewClick(object sender, RoutedEventArgs e)
        {
            //TODO: skal tage det element der er selecet i listboxen og give det videre som parameter til DownloadMoviePage hvis dens leje periode ikke er udløbe
            this.NavigationService.Navigate(new DownloadMoviePage());

            //TODO: skal tage det element der er select i listboxen og give det videre som parameter til ViewMoviePage hvis dens leje periode er udløbet
            this.NavigationService.Navigate(new ViewMoviePage());
        }
    }
}
