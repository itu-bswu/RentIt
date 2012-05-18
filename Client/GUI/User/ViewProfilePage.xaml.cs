// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewProfilePage.xaml.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace RentItClient.GUI.User
{
    using System.Windows;

    using ViewModels;
    using ViewModels.UserViewModels;

    /// <summary>
    /// Interaction logic for ViewProfilePage.xaml
    /// </summary>
    public partial class ViewProfilePage
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewProfilePage"/> class.
        /// </summary>
        public ViewProfilePage()
        {
            InitializeComponent();
            var u = ViewProfileViewModel.GetCurrentUserInfo();
            textBoxEmail.Text = u.Email;
            textBoxFullName.Text = u.FullName;
            textBoxUserName.Text = u.Username;
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
            NavigationService.Navigate(new ListMoviesPage());
        }

        /// <summary>
        /// Method invoked when the "View Profile" button is clicked.
        /// </summary>
        /// <param name="sender">The object invoking the method.</param>
        /// <param name="e">The event arguments.</param>
        private void ViewProfileClick(object sender, RoutedEventArgs e)
        {
        }

        /// <summary>
        /// Method invoked when the "Your Rentals" button is clicked.
        /// </summary>
        /// <param name="sender">The object invoking the method.</param>
        /// <param name="e">The event arguments.</param>
        private void YourRentalsClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RentalHistoryPage());
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
        /// Method invoked when the "List Movies" button is clicked.
        /// </summary>
        /// <param name="sender">The object invoking the method.</param>
        /// <param name="e">The event arguments.</param>
        private void EditProfileClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditProfilePage());
        }
        #endregion
    }
}
