﻿using System.Windows.Controls;

namespace RentItClient
{
    using System.Windows;

    using RentItClient.GUI.User;

    /// <summary>
    /// Interaction logic for ViewProfilePage.xaml
    /// </summary>
    public partial class ViewProfilePage : Page
    {
        public ViewProfilePage()
        {
            InitializeComponent();
        }

        private void mostRented(object sender, RoutedEventArgs e)
        {
            //TODO: skal hente en liste over mest downloadet film og give den videre som parameter
            this.NavigationService.Navigate(new MostRentedPage());
        }

        private void viewProfile(object sender, RoutedEventArgs e)
        {
            //TODO: skal tjekke hvilken bruger der logget ind og så give vedkommendes personlige oplysninger med som parameter
            this.NavigationService.Navigate(new ViewProfilePage());
        }

        private void yourRentals(object sender, RoutedEventArgs e)
        {
            //TODO: skal tjekke hvilken bruger der logget ind og så give vedkommendes list af rentals med som parameter
            this.NavigationService.Navigate(new RentalHistory());
        }

        private void searchClick(object sender, RoutedEventArgs e)
        {
            //TODO: skal tage informationen fra textBoxSearch og så giv det videre til servicen så der kan sendes en liste af resultater til ViewMovieListPage
            this.NavigationService.Navigate(new ViewMovieListPage());
        }

        private void logoutClick(object sender, RoutedEventArgs e)
        {
            //TODO: skal lukke connectionen til servicen ned
            this.NavigationService.Navigate(new LoginPage());
        }

        private void editProfileClick(object sender, System.Windows.RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new EditProfilePage());
        }
    }
}
