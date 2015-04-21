using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;


namespace KnightPacker
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        ObservableCollection<string> FilePaths = new ObservableCollection<string>();

        public HomePage()
        {
            InitializeComponent();
            ImageListBox.ItemsSource = FilePaths;
        }

        private void Browser_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.DefaultExt = ".png";
            dlg.Filter = "JPG Files (*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                //do stuff
                FilePaths.Add(dlg.FileName); 
            }
        }

        private void Previewer_Click(object sender, RoutedEventArgs e)
        {
            ImageControl.Source = new BitmapImage(new Uri(ImageListBox.SelectedItem.ToString()));
        }

        private void SpritePrev_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SpriteCreate_Click(object sender, RoutedEventArgs e)
        {

        }

        
    }
}
