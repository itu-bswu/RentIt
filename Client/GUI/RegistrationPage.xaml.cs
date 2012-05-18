// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RegistrationPage.xaml.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace RentItClient.GUI
{
    using System.Windows;
    using ViewModels.AdministrationViewModels;

    /// <summary>
    /// Interaction logic for RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationPage"/> class.
        /// </summary>
        public RegistrationPage()
        {
            InitializeComponent();
        }
        #endregion

        #region Click methods

        /// <summary>
        /// Method invoked when the "Reset" button is clicked.
        /// </summary>
        /// <param name="sender">The object invoking the method.</param>
        /// <param name="e">The event arguments.</param>
        private void ResetClick(object sender, RoutedEventArgs e)
        {
            ResetAll();
        }

        /// <summary>
        /// Method invoked when the "Cancel" button is clicked.
        /// </summary>
        /// <param name="sender">The object invoking the method.</param>
        /// <param name="e">The event arguments.</param>
        private void CancelClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LoginPage());
        }

        /// <summary>
        /// Method invoked when the "Submit" button is clicked.
        /// </summary>
        /// <param name="sender">The object invoking the method.</param>
        /// <param name="e">The event arguments.</param>
        private void SubmitClick(object sender, RoutedEventArgs e)
        {
            if (!passwordBox1.Password.Equals(passwordBoxConfirm.Password))
            {
                MessageBox.Show("Password and Confirm password did not match.");
                return;
            }
            var success = RegistrationViewModel.SignUp(
                textBoxEmail.Text,
                textBoxFullName.Text,
                passwordBox1.Password,
                textBoxUsername.Text);
            if (success)
            {
                MessageBox.Show("Registration was successful.");
                NavigationService.Navigate(new LoginPage());
            }
            else
            {
                MessageBox.Show("Registration failed. Please try again. If registration continues to fail, please restart the client.");
            }
        }
        #endregion

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
    }
}
