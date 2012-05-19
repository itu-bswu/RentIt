// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EditProfilePage.xaml.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace RentItClient.GUI.User
{
    using System.Windows;

    using ViewModels;
    using ViewModels.UserViewModels;

    /// <summary>
    /// Interaction logic for EditProfilePage.xaml
    /// </summary>
    public partial class EditProfilePage
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EditProfilePage"/> class.
        /// </summary>
        public EditProfilePage()
        {
            InitializeComponent();
            var u = ViewProfileViewModel.GetCurrentUserInfo();
            textBoxEmail.Text = u.Email;
            textBoxFullName.Text = u.FullName;
            passwordBoxPassword.Password = u.Password;
            passwordBoxConfirmPassword.Password = u.Password;
        }
        #endregion

        #region Click methods

        /// <summary>
        /// Method invoked when the "List Movies" button is clicked.
        /// </summary>
        /// <param name="sender">The object invoking the method.</param>
        /// <param name="e">The event arguments.</param>
        private void ListMoviesClick(object sender, RoutedEventArgs e)
        {
            if (MainWindow.ChangeWindow())
            {
                NavigationService.Navigate(new ListMoviesPage());
            }
        }

        /// <summary>
        /// Method invoked when the "View Profile" button is clicked.
        /// </summary>
        /// <param name="sender">The object invoking the method.</param>
        /// <param name="e">The event arguments.</param>
        private void ViewProfileClick(object sender, RoutedEventArgs e)
        {
            if (MainWindow.ChangeWindow())
            {
                NavigationService.Navigate(new ViewProfilePage());
            }
        }

        /// <summary>
        /// Method invoked when the "Your Rentals" button is clicked.
        /// </summary>
        /// <param name="sender">The object invoking the method.</param>
        /// <param name="e">The event arguments.</param>
        private void YourRentalsClick(object sender, RoutedEventArgs e)
        {
            if (MainWindow.ChangeWindow())
            {
                NavigationService.Navigate(new RentalHistoryPage());
            }
        }

        /// <summary>
        /// Method invoked when the "Search" button is clicked.
        /// </summary>
        /// <param name="sender">The object invoking the method.</param>
        /// <param name="e">The event arguments.</param>
        private void SearchClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ListMoviesPage(MasterViewModel.Search(textBoxSearch.Text)));
        }

        /// <summary>
        /// Method invoked when the "Logout" button is clicked.
        /// </summary>
        /// <param name="sender">The object invoking the method.</param>
        /// <param name="e">The event arguments.</param>
        private void LogoutClick(object sender, RoutedEventArgs e)
        {
            if (MainWindow.LogOut())
            {
                NavigationService.Navigate(new LoginPage());
            }
        }

        /// <summary>
        /// Method invoked when the "Save Changes" button is clicked.
        /// </summary>
        /// <param name="sender">The object invoking the method.</param>
        /// <param name="e">The event arguments.</param>
        private void SaveChangesClick(object sender, RoutedEventArgs e)
        {
            if (!passwordBoxPassword.Password.Equals(passwordBoxConfirmPassword.Password))
            {
                MessageBox.Show("Password and Confirm password did not match.");
                return;
            }

            const string messageBoxText = "Do you want to save changes?";
            const string caption = "Save Changes?";
            const MessageBoxButton button = MessageBoxButton.YesNoCancel;
            const MessageBoxImage icon = MessageBoxImage.Warning;
            var result = MessageBox.Show(messageBoxText, caption, button, icon);

            // Process message box results
            switch (result)
            {
                case MessageBoxResult.Yes:
                    if (EditProfileViewModel.EditUserProfile(textBoxEmail.Text, textBoxFullName.Text, passwordBoxPassword.Password))
                    {
                        NavigationService.Navigate(new ViewProfilePage());
                    }
                    else
                    {
                        MessageBox.Show("Something went wrong when trying edit your information. " +
                                "This error may occur if there is something wrong with the data you input" +
                                " or if there is a problem with the service." +
                                "\n If all data appears ok, please restart the client.");
                    }

                    NavigationService.Navigate(new ViewProfilePage());
                    break;
                case MessageBoxResult.No:
                    NavigationService.Navigate(new ViewProfilePage());
                    break;
                case MessageBoxResult.Cancel:
                    break;
            }
        }
        #endregion
    }
}
