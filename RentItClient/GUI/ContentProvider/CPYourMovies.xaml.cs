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

<<<<<<< HEAD
        private void yourMovies(object sender, RoutedEventArgs e)
=======
        private void UploadMovie(object sender, RoutedEventArgs e)
        {
            CPUploadMovies uploadMovies = new CPUploadMovies();
            this.NavigationService.Navigate(uploadMovies);
        }

        private void YourMovies(object sender, RoutedEventArgs e)
>>>>>>> d255a6110b48632aa61c6ff763010fb703f0e257
        {
            //TODO: metoden skal give en CPs liste af oploaded film med hver gang den her knap bliver trykket
            //TODO: så Listboxen i CPYourMovies kan blive lavet med de elementer
            CPYourMovies yourMovies = new CPYourMovies();
            this.NavigationService.Navigate(yourMovies);
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
