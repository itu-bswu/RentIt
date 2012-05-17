namespace RentItClient.GUI.ContentProvider
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using Types;
    using ViewModels.ProviderViewModels;

    /// <summary>
    /// Interaction logic for CPEditMoviePage.xaml
    /// </summary>
    public partial class CPEditMoviePage
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CPEditMoviePage"/> class.
        /// </summary>
        /// <param name="movie">The movie to show information about.</param>
        public CPEditMoviePage(Movie movie)
            : this()
        {
            textBoxDescription.Text = movie.Description;
            textBoxTitle.Text = movie.Title;
            if (DatePickerReleaseDate != null & movie.ReleaseDate != null)
            {
                DatePickerReleaseDate.DisplayDate = movie.ReleaseDate.Value;
            }

            var gcl = new GenreCheckList();

            foreach (var g in movie.Genres)
            {
                gcl.First(x => x.GenreName.Equals(g)).Checked = true;
            }

            GenreCheckList.ItemsSource = gcl;

        }

        /// <summary>
        /// Prevents a default instance of the <see cref="CPEditMoviePage"/> class from being created. 
        /// </summary>
        private CPEditMoviePage()
        {
            InitializeComponent();
        }

        #endregion

        #region Click methods

        private void YourMoviesClick(object sender, RoutedEventArgs e)
        {
            if (MainWindow.ChangeWindow())
            {
                NavigationService.Navigate(new CPYourMoviesPage());
            }
        }

        private void RegisterMovieClick(object sender, RoutedEventArgs e)
        {
            if (MainWindow.ChangeWindow())
            {
                NavigationService.Navigate(new CPRegisterMoviePage());
            }
        }

        private void LogoutClick(object sender, RoutedEventArgs e)
        {
            if (MainWindow.LogOut())
            {
                NavigationService.Navigate(new LoginPage());
            }
        }

        private void SaveChangesClick(object sender, RoutedEventArgs e)
        {
            const string messageBoxText = "Do you want to save changes?";
            const string caption = "Save Changes?";
            const MessageBoxButton button = MessageBoxButton.YesNoCancel;
            const MessageBoxImage icon = MessageBoxImage.Warning;

            var result = MessageBox.Show(messageBoxText, caption, button, icon);

            // Process message box results
            switch (result)
            {
                case MessageBoxResult.Yes:
                    var genres = (from GenreChecked gc in GenreCheckList.Items where gc.Checked select gc.GenreName).ToList();
                    var movie = new Movie(
                    0,
                    textBoxTitle.Text,
                    textBoxDescription.Text,
                    DatePickerReleaseDate.SelectedDate,
                    genres,
                    new List<Tuple<string, int>>());

                    if (CPEditMovieViewModel.EditMovie(movie))
                    {
                        MessageBox.Show("Edit was successful!");
                        NavigationService.Navigate(new CPYourMoviesPage());
                    }
                    else
                    {
                        MessageBox.Show("Something went wrong when trying to edit the movie. " +
                                "This error may occur if there is something wrong with the data you input" +
                                " or if there is a problem with the service." +
                                "\n If all data appears ok, please restart the client.");
                    }

                    break;
                case MessageBoxResult.No:
                    NavigationService.Navigate(new CPYourMoviesPage());
                    break;
                case MessageBoxResult.Cancel:
                    return;
            }
        }
        #endregion

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
        {

        }
    }
}
