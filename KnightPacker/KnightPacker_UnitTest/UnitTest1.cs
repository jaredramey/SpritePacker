using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using Microsoft.Win32;
using System.Xaml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITest;
using KnightPacker;
using System.Windows.Media.Imaging;
using System.Windows.Forms;

namespace KnightPacker_UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        private Type PackerType;
        private ConstructorInfo PackerConstructor;
        private MethodInfo BrowserClick;
        private MethodInfo CreateSpriteSheet;
        private MethodInfo SaveSpriteSheet;
        object Packer;
        PrivateObject newPageClass = new PrivateObject(typeof(HomePage));

        List<WriteableBitmap> Images { set; get; }
        WriteableBitmap FinalImage { set; get; }
        ObservableCollection<string> FilePaths = new ObservableCollection<string>();
        
        public UnitTest1()
        {
            HomePage newPage = new HomePage();

            

            PackerType = newPage.GetType();

            //PackerConstructor = PackerType.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public, null, new Type[] {typeof(object)}, null);
            PackerConstructor = PackerType.GetConstructor(Type.EmptyTypes);

            Packer = PackerConstructor.Invoke(new object[]{});

            BrowserClick = PackerType.GetMethod("Browser_Click", BindingFlags.Instance | BindingFlags.NonPublic);

            CreateSpriteSheet = PackerType.GetMethod("CreateSpriteSheet", BindingFlags.Instance | BindingFlags.NonPublic);

            SaveSpriteSheet = PackerType.GetMethod("SaveSpriteSheet", BindingFlags.Instance | BindingFlags.NonPublic);
        }

        [TestMethod]
        public void TestMethod1()
        {
            //Arrange
            //*Insert Code to Setup Tests*
            newPageClass.Invoke("Browser_Click", null, null);
            ////HomePage Packer = new HomePage();
            ////Type t = Packer.GetType();
            //Button myButton = new Button();
            //RoutedEventArgs myArg = new RoutedEventArgs();
            ////Act
            ////*Insert Code to call the method property under test*
            //MethodInfo Packer_BrowserClick = PackerType.GetMethod("Browser_Click", BindingFlags.Instance | BindingFlags.NonPublic);
            //Packer_BrowserClick.Invoke(Packer, new object[]{myArg});
        }

        [TestMethod]
        public void TestMethod2()
        {
            //Arrange
            //*Insert Code to Setup Tests*
            HomePage Packer = new HomePage();
            Type t = Packer.GetType();
            //Act
            //*Insert Code to call the method property under test*
            MethodInfo Packer_Create = t.GetMethod("CreateSpriteSheet");
            //Packer_Create.Invoke(t, null);
        }

        [TestMethod]
        public void TestMethod3()
        {
            //Arrange
            //*Insert Code to Setup Tests*
            HomePage Packer = new HomePage();
            Type t = Packer.GetType();
            //Act
            //*Insert Code to call the method property under test*
            MethodInfo Packer_Save = t.GetMethod("SaveSpriteSheet");
            Packer_Save.Invoke(t, null);
        }

       
    }

}
