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
using KnightPacker;
using System.Windows.Media.Imaging;
using System.Windows.Input;

namespace KnightPacker_UnitTest
{
    [TestClass]
    public class UnitTest1
    {

        List<WriteableBitmap> Images { set; get; }
        WriteableBitmap FinalImage { set; get; }
        ObservableCollection<string> FilePaths = new ObservableCollection<string>();
        
        [TestMethod]
        public void TestMethod1()
        {
            //Arrange
            //*Insert Code to Setup Tests*
            HomePage Packer = new HomePage();
            Type t = Packer.GetType();
            //Act
            //*Insert Code to call the method property under test*
            MethodInfo Packer_BrowserClick = t.GetMethod("Browser_Click");
            
            //Assert
            //*Insert code to verify the test compleeted as expected
            Assert.AreNotEqual(0, FilePaths.Count());
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
            MethodInfo Packer_BrowserClick = t.GetMethod("CreateSpriteSheet");
            Assert.AreNotEqual(0, Images.Count());
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
            MethodInfo Packer_BrowserClick = t.GetMethod("SaveSpriteSheet");
        }
    }

}
