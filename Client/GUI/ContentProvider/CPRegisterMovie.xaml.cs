namespace RentItClient.GUI.ContentProvider
{
    using System.Windows;
    using System.Windows.Controls;

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

        private void YourMovies(object sender, RoutedEventArgs e)
        {
            const string MessageBoxText = "All unsaved information will be lost, are you sure you want to change window?";
            const string Caption = "Change window?";
            const MessageBoxButton Button = MessageBoxButton.YesNo;
            const MessageBoxImage Icon = MessageBoxImage.Warning;

            MessageBoxResult result = MessageBox.Show(MessageBoxText, Caption, Button, Icon);

            // Process message box results
            switch (result)
            {
                case MessageBoxResult.Yes:
                    // User pressed Yes button
                    this.NavigationService.Navigate(new CPYourMovies());
                    break;
                case MessageBoxResult.No:
                    // User pressed No button
                    break;
            }
            //TODO: metoden skal give en CPs liste af oploaded film med hver gang den her knap bliver trykket
            //TODO: så Listboxen i CPYourMovies kan blive lavet med de elementer
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new LoginPage());
            //TODO: luk forbindelsen til servicen
        }

        private void RegisterMovieClick(object sender, RoutedEventArgs e)
        {
            //TODO: Register movie in database
            string messageBoxText = "You are about to register a movie, would you like to upload an edition right away aswell?";
            string caption = "Upload edition?";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;

            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

            // Process message box results
            switch (result)
            {
                case MessageBoxResult.Yes:
                    // User pressed Yes button
                    this.NavigationService.Navigate(new CPUploadMovies());
                    //TODO: save the changes made on the user obejct to the database
                    break;
                case MessageBoxResult.No:
                    // User pressed No button
                    this.NavigationService.Navigate(new CPYourMovies());
                    break;
            }
            //TODO: du skal hente info fra TextBox: textBoxTitle, textBoxGenre, textBoxDescription
            //TODO: og du skal hente info fra DatePicker: DatePickerReleaseDate
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
        {

        }

        private void AddGenreClick(object sender, RoutedEventArgs e)
        {
            //TODO: skal tage stringen i textBoxGenre og add den til genrelisten
            this.NavigationService.Navigate(new CPRegisterMovie());
        }
    }
}
