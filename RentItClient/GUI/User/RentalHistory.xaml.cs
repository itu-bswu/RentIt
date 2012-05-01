﻿using System.Windows;
using System.Windows.Controls;

namespace RentItClient
{

    /// <summary>
    /// Interaction logic for RentalHistory.xaml
    /// </summary>
    public partial class RentalHistory : Page
    {
        public RentalHistory()
        {
            InitializeComponent();
        }

        private void Logout_click(object sender, RoutedEventArgs e)
        {
        }

        private void ViewClick(object sender, RoutedEventArgs e)
        {
            ViewMoviePage viewMoviePage = new ViewMoviePage();
            this.NavigationService.Navigate(viewMoviePage);
        }

        private void mostRented(object sender, System.Windows.RoutedEventArgs e)
        {
            MostRentedPage mostRentedPage = new MostRentedPage();
            this.NavigationService.Navigate(mostRentedPage);
        }

        private void viewProfile(object sender, System.Windows.RoutedEventArgs e)
        {
            ViewProfilePage viewProfilePage = new ViewProfilePage();
            this.NavigationService.Navigate(viewProfilePage);
        }

        private void yourRentals(object sender, System.Windows.RoutedEventArgs e)
        {
            RentalHistory rentalHistory = new RentalHistory();
            this.NavigationService.Navigate(rentalHistory);
        }
    }
}
