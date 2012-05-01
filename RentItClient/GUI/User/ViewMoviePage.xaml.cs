using System.Windows.Controls;

namespace RentItClient
{
    /// <summary>
    /// Interaction logic for ViewMoviePage.xaml
    /// </summary>
    public partial class ViewMoviePage : Page
    {
        public ViewMoviePage()
        {
            InitializeComponent();
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


    }
}
