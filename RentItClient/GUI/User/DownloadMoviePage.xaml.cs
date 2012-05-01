using System.Windows.Controls;

namespace RentItClient
{
    /// <summary>
    /// Interaction logic for DownloadMoviePage.xaml
    /// </summary>
    public partial class DownloadMoviePage : Page
    {
        public DownloadMoviePage()
        {
            InitializeComponent();
            //TODO textBlockTitle skal indholde den givne films title
            //TODO textBlockRelease skal indholde den givne films release date
            //TODO textBoxDescription skal indholde den givne films description

        }

        private void mostRented(object sender, System.Windows.RoutedEventArgs e)
        {
            MostRentedPage mostRentedPage = new MostRentedPage();
            this.NavigationService.Navigate(mostRentedPage);
        }

        private void viewProfile(object sender, System.Windows.RoutedEventArgs e)
        {
            ViewProfilePage viewProfilePage = new ViewProfilePage();
            this.NavigationService.Navigate(viewProfilePage);
        }

        private void yourRentals(object sender, System.Windows.RoutedEventArgs e)
        {
            RentalHistory rentalHistory = new RentalHistory();
            this.NavigationService.Navigate(rentalHistory);
        }

        private void textBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
