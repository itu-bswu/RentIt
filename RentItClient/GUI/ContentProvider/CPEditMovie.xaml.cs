namespace RentItClient.GUI.ContentProvider
{
    using System.Windows;

    using RentItClient.GUI.User;

    /// <summary>
    /// Interaction logic for CPEditMovie.xaml
    /// </summary>
    public partial class CPEditMovie
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CPEditMovie"/> class.
        /// </summary>
        public CPEditMovie()
        {
            InitializeComponent();
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

        private void SaveChangesClick(object sender, RoutedEventArgs e)
        {
            const string MessageBoxText = "Do you want to save changes?";
            const string Caption = "Save Changes?";
            const MessageBoxButton Button = MessageBoxButton.YesNoCancel;
            const MessageBoxImage Icon = MessageBoxImage.Warning;
            MessageBox.Show(MessageBoxText, Caption, Button, Icon);

            MessageBoxResult result = MessageBox.Show(MessageBoxText, Caption, Button, Icon);

            // Process message box results
            switch (result)
            {
                case MessageBoxResult.Yes:
                    // User pressed Yes button
                    CPYourMovies yourMovies = new CPYourMovies();
                    this.NavigationService.Navigate(yourMovies);
                    //TODO: save the changes made on the movie obejct to the database
                    //TODO: Du skal hente info fra: TextBox: textBoxTitle, textBoxGenre og textBoxDescription
                    break;
                case MessageBoxResult.No:
                    // User pressed No button
                    CPYourMovies yourMovies1 = new CPYourMovies();
                    this.NavigationService.Navigate(yourMovies1);
                    break;
                case MessageBoxResult.Cancel:
                    // User pressed Cancel button
                    CPYourMovies yourMovies2 = new CPYourMovies();
                    this.NavigationService.Navigate(yourMovies2);
                    break;
            }
        }
    }
}
