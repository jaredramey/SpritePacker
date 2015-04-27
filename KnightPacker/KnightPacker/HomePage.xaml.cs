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
using Microsoft.Win32;
using System.Xml;


namespace KnightPacker
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        List<WriteableBitmap> Images { set; get; }
        WriteableBitmap FinalImage { set; get; }
        ObservableCollection<string> FilePaths = new ObservableCollection<string>();
        int prevWidth;
        int prevHeight;
        

        public HomePage()
        {
            InitializeComponent();
            ImageListBox.ItemsSource = FilePaths;
            SpriteSheetImage.Source = FinalImage;
            //FinalImage.GetBitmapContext();
            Images = new List<WriteableBitmap>();
            UpdateImageDisplay();
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
            //CreateSpriteSheet();
            SaveSpriteSheet();
        }

        private void CreateSpriteSheet()
        {
            for (int i = 0; i < FilePaths.Count; i++)
            {
                FileStream stream = null;
                try
                {
                    stream = new FileStream(FilePaths[i].ToString(), FileMode.Open, FileAccess.Read, FileShare.Read);
                    WriteableBitmap loadedImage = BitmapFactory.New(1, 1).FromStream(stream);
                    Images.Add(loadedImage);
                }
                catch( Exception e ) {
                    MessageBox.Show("ERROR");
                }
                finally {
                    if (stream != null)
                    {
                        stream.Close();
                    }
                    UpdateImageDisplay();
                }
            }
        }

        private void UpdateImageDisplay()
        {
            int pix_Width = 0;
            int pix_Height = 0;

            foreach(WriteableBitmap image in Images)
            {
                pix_Width += image.PixelWidth;
                if(pix_Height < image.PixelHeight)
                {
                    pix_Height = image.PixelHeight;
                }
            }

            FinalImage = BitmapFactory.New(pix_Width, pix_Height);

            int pix_x = 0;
            int pix_y = 0;

            foreach(WriteableBitmap image in Images)
            {
                Rect imageRect = new Rect(pix_x, pix_y, image.PixelWidth, image.PixelHeight);
                FinalImage.Blit(imageRect, image, new Rect(0, 0, image.PixelWidth, image.PixelHeight));
                pix_x += image.PixelWidth;
                
            }

            SpriteSheetImage.Source = FinalImage;

        }

        private void SaveSpriteSheet()
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = ("Png (*.png)|*.png");
            XmlDocument doc = new XmlDocument();
            XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(docNode);

            XmlNode SpritesNode = doc.CreateElement("sprites");
            doc.AppendChild(SpritesNode);

            int pix_x = 0;
            int pix_y = 0;
            int ID = 0;

            foreach (WriteableBitmap image in Images)
            {
                XmlNode spriteNode = doc.CreateElement("sprite");
                XmlAttribute spriteID = doc.CreateAttribute("id");
                spriteID.Value = ID.ToString();
                ID++;
                spriteNode.Attributes.Append(spriteID);
                XmlAttribute spriteX = doc.CreateAttribute("x");
                spriteX.Value = pix_x.ToString();
                spriteNode.Attributes.Append(spriteX);
                pix_x += image.PixelWidth;
                XmlAttribute spriteY = doc.CreateAttribute("y");
                spriteY.Value = pix_y.ToString();
                spriteNode.Attributes.Append(spriteY);
                XmlAttribute spriteWidth = doc.CreateAttribute("width");
                spriteWidth.Value = image.Width.ToString();
                spriteNode.Attributes.Append(spriteWidth);
                XmlAttribute spriteHeight = doc.CreateAttribute("height");
                spriteHeight.Value = image.Height.ToString();
                spriteNode.Attributes.Append(spriteHeight);
                SpritesNode.AppendChild(spriteNode);
            }

            if(saveFile.ShowDialog() == true)
            {

                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(FinalImage));

                FileStream filestream = new FileStream(saveFile.FileName, FileMode.Create);
                encoder.Save(filestream);
                filestream.Close();
            }

            SaveFileDialog saveDoc = new SaveFileDialog();
            saveDoc.Filter = "Xml (*.xml)|*.xml";
            if(saveDoc.ShowDialog() == true)
            {
                doc.Save(saveDoc.FileName);
            }
        }
    }
}
