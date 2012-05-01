using System.Windows;
using System.Windows.Controls;

namespace RentItClient
{
    /// <summary>
    /// Interaction logic for UserMostRented.xaml
    /// </summary>
    public partial class MostRentedPage : Page
    {
        public MostRentedPage()
        {
            InitializeComponent();
        }

        private void Logout_click(object sender, RoutedEventArgs e)
        {
        }

        private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

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
