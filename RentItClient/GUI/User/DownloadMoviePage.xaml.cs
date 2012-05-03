using System.Windows.Controls;

namespace RentItClient
{
    using RentItClient.GUI.User;

    /// <summary>
    /// Interaction logic for DownloadMoviePage.xaml
    /// </summary>
    public partial class DownloadMoviePage : Page
    {
        private DownloadMoviePage()
        {
            InitializeComponent();
        }

        public DownloadMoviePage(int movieId)
            : this()
        {
            //TODO textBlockTitle skal indholde den givne films title
            //TODO textBlockRelease skal indholde den givne films release date
            //TODO textBoxDescription skal indholde den givne films description
        }

        private void mostRented(object sender, System.Windows.RoutedEventArgs e)
        {
            //TODO: skal hente en liste over mest downloadet film og give den videre som parameter
            this.NavigationService.Navigate(new MostRentedPage());
        }

        private void viewProfile(object sender, System.Windows.RoutedEventArgs e)
        {
            //TODO: skal tjekke hvilken bruger der logget ind og så give vedkommendes personlige oplysninger med som parameter
            this.NavigationService.Navigate(new ViewProfilePage());
        }

        private void yourRentals(object sender, System.Windows.RoutedEventArgs e)
        {
            //TODO: skal tjekke hvilken bruger der logget ind og så give vedkommendes list af rentals med som parameter
            this.NavigationService.Navigate(new RentalHistory());
        }

        private void searchClick(object sender, System.Windows.RoutedEventArgs e)
        {
            //TODO: skal tage informationen fra textBoxSearch og så giv det videre til servicen så der kan sendes en liste af resultater til ViewMovieListPage
            this.NavigationService.Navigate(new ViewMovieListPage());
        }

        private void logoutClick(object sender, System.Windows.RoutedEventArgs e)
        {
            //TODO: skal lukke connectionen til servicen ned
            this.NavigationService.Navigate(new LoginPage());
        }

        private void downloadMovieClick(object sender, System.Windows.RoutedEventArgs e)
        {
            //TODO: skal tage det object siden er blevet startet om med og hente dets fil.
            this.NavigationService.Navigate(new RentalHistory());
        }
    }
}
