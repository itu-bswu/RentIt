using System.Linq;

namespace RentItClient.GUI.ContentProvider
{
    using System.Windows;
    using Types;
    using ViewModels.ProviderViewModels;

    /// <summary>
    /// Interaction logic for CPViewMoviePage.xaml
    /// </summary>
    public partial class CPViewMoviePage
    {
        #region Fields

        /// <summary>
        /// The movie shown by the page.
        /// </summary>
        private readonly Movie shownMovie;
        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CPViewMoviePage"/> class. 
        /// </summary>
        /// <param name="id">
        /// The id of the movie to show information about.
        /// </param>
        public CPViewMoviePage(int id)
            : this()
        {
            shownMovie = CPViewMovieViewModel.GetMovieInformation(id);
            textBoxDescription.Text = shownMovie.Description;
            textBoxTitle.Text = shownMovie.Title;

            var genres = shownMovie.Genres.Aggregate(string.Empty, (current, g) => current + (g + ", "));

            textBoxGenre.Text = genres;

            EditionListBox.ItemsSource = shownMovie.Editions;
            EditionListBox.DisplayMemberPath = "Item1";
            EditionListBox.SelectedValuePath = "Item2";
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="CPViewMoviePage"/> class from being created. 
        /// </summary>
        private CPViewMoviePage()
        {
            InitializeComponent();
        }
        #endregion

        #region Click methods

        private void YourMoviesClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CPYourMoviesPage());
        }

        private void RegisterMovieClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CPRegisterMoviePage());
        }

        private void LogoutClick(object sender, RoutedEventArgs e)
        {
            if (MainWindow.LogOut())
            {
                NavigationService.Navigate(new LoginPage());
            }
        }

        private void EditInformationClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CPEditMoviePage(shownMovie));
        }

        private void DeleteMovieClick(object sender, RoutedEventArgs e)
        {
            const string messageBoxText = "Are you sure you want to delete this movie, and all the editions aassociated with it?";
            const string caption = "Delete Movie?";
            const MessageBoxButton button = MessageBoxButton.YesNo;
            const MessageBoxImage icon = MessageBoxImage.Warning;

            var result = MessageBox.Show(messageBoxText, caption, button, icon);

            // Process message box results
            switch (result)
            {
                case MessageBoxResult.Yes:
                    if (CPViewMovieViewModel.DeleteMovie(shownMovie))
                    {
                        MessageBox.Show("Movie was succcessfully deleted.");
                        NavigationService.Navigate(new CPYourMoviesPage());
                    }
                    else
                    {
                        MessageBox.Show("Movie was not successfully deleted. Please try again." +
                                        "\n If trying again does not resolve the issue, please restart the client.");
                    }

                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

        private void DeleteEditionClick(object sender, RoutedEventArgs e)
        {
            if (EditionListBox.SelectedIndex < 0)
            {
                return;
            }

            const string messageBoxText = "Are you sure you want to delete the selected edition?";
            const string caption = "Delete edition?";
            const MessageBoxButton button = MessageBoxButton.YesNo;
            const MessageBoxImage icon = MessageBoxImage.Warning;

            var result = MessageBox.Show(messageBoxText, caption, button, icon);

            // Process message box results
            switch (result)
            {
                case MessageBoxResult.Yes:
                    var selectedId = (int)EditionListBox.SelectedValue;
                    CPViewMovieViewModel.DeleteEdition(selectedId);
                    NavigationService.Navigate(new CPViewMoviePage(shownMovie.ID));
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

        private void UploadEditionClick(object sender, RoutedEventArgs e)
        {
            const string messageBoxText = "Do you want to add an edition to the movie?";
            const string caption = "Add edition?";
            const MessageBoxButton button = MessageBoxButton.YesNo;

            var result = MessageBox.Show(messageBoxText, caption, button);

            // Process message box results
            switch (result)
            {
                case MessageBoxResult.Yes:
                    // User pressed Yes button
                    NavigationService.Navigate(new CPUploadEditionPage(shownMovie));
                    break;
                case MessageBoxResult.No:
                    // User pressed No button
                    break;
            }
        }

        #endregion
    }
}
