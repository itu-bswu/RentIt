
namespace RentItClient
{
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary> 

    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void LoginClick(object sender, RoutedEventArgs e)
        {
            //TODO: make call to service to verify user
            MostRentedPage mostRentedPage = new MostRentedPage();
            this.NavigationService.Navigate(mostRentedPage);
        }

        private void SignupClick(object sender, RoutedEventArgs e)
        {
            RegistrationPage registrationPage = new RegistrationPage();
            this.NavigationService.Navigate(registrationPage);
        }
    }
}
