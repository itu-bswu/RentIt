namespace RentItClient.GUI.ContentProvider
{
    using System.Windows;

    using RentItClient.GUI.User;

    /// <summary>
    /// Interaction logic for CPRegisterMovie.xaml
    /// </summary>
    public partial class CPRegisterMovie
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CPRegisterMovie"/> class.
        /// </summary>
        public CPRegisterMovie()
        {
            this.InitializeComponent();
        }

        private void UploadMovie(object sender, RoutedEventArgs e)
        {
            CPUploadMovies uploadMovies = new CPUploadMovies();
            this.NavigationService.Navigate(uploadMovies);
        }

        private void YourMovies(object sender, RoutedEventArgs e)
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

        private void RegisterMovieClick(object sender, RoutedEventArgs e)
        {
            //TODO: Register movie in database
            //TODO: du skal hente info fra TextBox: textBoxTitle, textBoxGenre, textBoxDescription
            //TODO: og du skal hente info fra DatePicker: DatePickerReleaseDate
        }
    }
}
