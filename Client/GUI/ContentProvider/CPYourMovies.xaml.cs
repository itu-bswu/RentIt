namespace RentItClient.GUI.ContentProvider
{
    using System.Windows;

    using RentItClient.GUI.User;

    /// <summary>
    /// Interaction logic for CPYourMovies.xaml
    /// </summary>
    public partial class CPYourMovies
    {
        public CPYourMovies()
        {
            this.InitializeComponent();
        }

        private void RegisterMovie(object sender, RoutedEventArgs e)
        {
            CPRegisterMovie registerMovie = new CPRegisterMovie();
            this.NavigationService.Navigate(registerMovie);
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new LoginPage());
            //TODO: luk forbindelsen til servicen
        }

        private void ViewClick(object sender, RoutedEventArgs e)
        {
            //TODO: Den skal på en eller anden måde give det element med der er selected i listboxen.
            this.NavigationService.Navigate(new CPViewMovie());
        }
    }
}
