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
using System.IO;


namespace KnightPacker
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        WriteableBitmap writablebitmap = BitmapFactory.New(512, 512);
        ObservableCollection<string> FilePaths = new ObservableCollection<string>();
        int prevWidth;
        int prevHeight;
        

        public HomePage()
        {
            InitializeComponent();
            ImageListBox.ItemsSource = FilePaths;
            SpriteSheetImage.Source= writablebitmap;
            writablebitmap.GetBitmapContext();
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
            if(ImageListBox.SelectedItems.Count == 1)
            {
                ImageControl.Source = new BitmapImage(new Uri(ImageListBox.SelectedItem.ToString()));
            }
        }

        private void SpritePrev_Click(object sender, RoutedEventArgs e)
        {
            CreateSpriteSheet();
        }

        private void SpriteCreate_Click(object sender, RoutedEventArgs e)
        {
            CreateSpriteSheet();
            SaveSpriteSheet();
        }

        private void CreateSpriteSheet()
        {
            //Canvas drawing code
            for (int i = 0; i < FilePaths.Count; i++ )
            {
                using(var stream = new FileStream(FilePaths[i].ToString(), FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    var bitmapFrame = BitmapFrame.Create(stream, BitmapCreateOptions.DelayCreation, BitmapCacheOption.None);
                    var width = bitmapFrame.PixelWidth;
                    var height = bitmapFrame.PixelHeight;

                    Rectangle testRectangle = new Rectangle();
                    testRectangle.Width = width;
                    testRectangle.Height = height;

                    ImageBrush myBrush = new ImageBrush();
                    myBrush.ImageSource = new BitmapImage(new Uri(@FilePaths[i].ToString(), UriKind.Relative));

                    testRectangle.Fill = myBrush;

                    if(i == 0)
                    {
                        Canvas.SetLeft(testRectangle,0);
                        SpriteSheet.Children.Add(testRectangle);
                        prevWidth = width+10;
                        prevHeight = height+10;
                    }
                    else 
                    {
                        if(prevWidth < SpriteSheet.Width)
                        {  
                            Canvas.SetLeft(testRectangle, prevWidth);
                            SpriteSheet.Children.Add(testRectangle);
                            prevWidth += width + 10;
                        }

                        else
                        {
                            Canvas.SetTop(testRectangle, prevHeight);
                            SpriteSheet.Children.Add(testRectangle);
                            prevHeight += height + 10;
                            prevWidth = 0;
                        }
                        
                    }
                }
            }

            //for (int i = 0; i < FilePaths.Count; i++)
            //{
            //    using (var stream = new FileStream(FilePaths[i].ToString(), FileMode.Open, FileAccess.Read, FileShare.Read))
            //    {
            //        var bitmapFrame = BitmapFrame.Create(stream, BitmapCreateOptions.DelayCreation, BitmapCacheOption.None);
            //        var width = bitmapFrame.PixelWidth;
            //        var height = bitmapFrame.PixelHeight;

            //        Rectangle testRectangle = new Rectangle();
            //        testRectangle.Width = width;
            //        testRectangle.Height = height;

            //        writablebitmap = BitmapFactory.New(1, 1).FromResource(FilePaths[i]);
            //    }
            //}
        }

        private void SaveSpriteSheet()
        {

        }
    }
}
