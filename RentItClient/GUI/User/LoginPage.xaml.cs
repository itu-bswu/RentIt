
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
            //TODO: skal hente info fra textBoxEmail og passwordBox

            //TODO: make call to service to verify user
            //TODO: tjek om det er en user eller en contentprovider
            //hvis det er en user skal den gå til følgende:
            this.NavigationService.Navigate(new MostRentedPage());
            //hvis det er en contentprovider skal den gå til:
            this.NavigationService.Navigate(new CPYourMovies());
        }

        private void SignupClick(object sender, RoutedEventArgs e)
        {
            RegistrationPage registrationPage = new RegistrationPage();
            this.NavigationService.Navigate(registrationPage);
        }
    }
}
