using System.Windows;
using System.Windows.Controls;

namespace RentItClient
{
    /// <summary>
    /// Interaction logic for RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            ResetAll();
        }

        public void ResetAll()
        {
            textBoxFirstName.Text = "";
            textBoxLastName.Text = "";
            textBoxEmail.Text = "";
            passwordBox1.Password = "";
            passwordBoxConfirm.Password = "";
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Create user in database
            LoginPage loginPage = new LoginPage();
            this.NavigationService.Navigate(loginPage);
        }
    }
}
