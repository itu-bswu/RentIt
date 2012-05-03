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
        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationPage"/> class.
        /// </summary>
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            ResetAll();
        }

        /// <summary>
        /// Sets all the text boxes to empty strings.
        /// </summary>
        private void ResetAll()
        {
            textBoxEmail.Text = string.Empty;
            passwordBox1.Password = string.Empty;
            passwordBoxConfirm.Password = string.Empty;
            textBoxUsername.Text = string.Empty;
            textBoxFullName.Text = string.Empty;
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LoginPage());
        }

        private void SubmitClick(object sender, RoutedEventArgs e)
        {
            RegistrationViewModel.SignUp(
                textBoxEmail.Text,
                textBoxFullName.Text,
                passwordBox1.Password,
                textBoxUsername.Text);
            NavigationService.Navigate(new LoginPage());
        }
    }
}
