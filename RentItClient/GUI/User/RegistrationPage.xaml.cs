using System.Windows;
using System.Windows.Controls;

namespace RentItClient
{
    using RentItClient.ViewModels.AdminViewModels;

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
            this.ResetAll();
        }

        private void ResetAll()
        {
            textBoxFirstName.Text = string.Empty;
            textBoxLastName.Text = string.Empty;
            textBoxEmail.Text = string.Empty;
            passwordBox1.Password = string.Empty;
            passwordBoxConfirm.Password = string.Empty;
            textBoxUsername.Text = string.Empty;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new LoginPage());
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            RegistrationViewModel.SignUp(
                textBoxEmail.Text,
                textBoxFirstName.Text + " " + textBoxLastName.Text,
                passwordBox1.Password,
                textBoxUsername.Text);
            this.NavigationService.Navigate(new LoginPage());
        }
    }
}
