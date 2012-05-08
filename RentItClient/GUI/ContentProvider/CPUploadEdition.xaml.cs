namespace RentItClient.GUI.ContentProvider
{
    using System;
    using System.Windows;

    /// <summary>
    /// Interaction logic for CPUploadMovies.xaml
    /// </summary>
    public partial class CPUploadMovies
    {
        public CPUploadMovies()
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

        private void RegisterMovie(object sender, RoutedEventArgs e)
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
                    this.NavigationService.Navigate(new CPRegisterMovie());
                    break;
                case MessageBoxResult.No:
                    // User pressed No button
                    break;
            }
        }

        private void Logout(object sender, RoutedEventArgs e)
        {

        }

        private void UploadEditionClick(object sender, RoutedEventArgs e)
        {
            //TODO: Upload edition to database
            //TODO: du skal hente data fra TextBox: textBoxTitle, textBoxGenre, textBoxDescription, textBoxFiletoUpload, textBoxCoverImage
        }

        private void BrowseClick(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension
            dlg.DefaultExt = ".avi";
            dlg.Filter = "movie files (.avi)|*.avi";

            // Display OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox
            if (result == true)
            {
                // Open document
                string filename = dlg.FileName;
                this.textBoxFiletoUpload.Text = filename;
            }
        }
    }
}
