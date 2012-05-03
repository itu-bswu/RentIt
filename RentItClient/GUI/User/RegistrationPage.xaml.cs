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
            textBoxUsername.Text = string.Empty;
            textBoxFirstName.Text = "";
            textBoxLastName.Text = "";
            textBoxEmail.Text = "";
            passwordBox1.Password = "";
            passwordBoxConfirm.Password = "";
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new LoginPage());
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            //TODO: hente data fra textBoxFirstName, textBoxLastName, textBoxEmail, passwordBox1, passwordBoxConfirm

            //TODO: Create user in database
            this.NavigationService.Navigate(new LoginPage());
        }
    }
}
