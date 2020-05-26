using Microsoft.VisualStudio.TestTools.UnitTesting;
using WpfApp1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Controls;

namespace WpfApp1.TileEditorTests
{
    [TestClass()]
    public class MainWindowTests
    {
        [TestMethod()]
        public void GridColorNameTest()
        {
            MainWindow mainWindowz = new MainWindow();
            List<Color> colorCheck = new List<Color> { Colors.White, Colors.Red, Colors.Blue, Colors.Green, Colors.Yellow, Colors.Black, Colors.Pink, Colors.Violet };
            for (int i = 0; i < colorCheck.Count; i++) { Assert.AreEqual(colorCheck[i], mainWindowz.GridColorName(i)); }
        }

        [TestMethod()]
        public void GetSpriteTest()
        {
            MainWindow mainWindowz = new MainWindow();
            for (int i = 8; i < 23; i++) { Assert.IsInstanceOfType(mainWindowz.GetSprite(i), typeof(Image)); }
        }

        [TestMethod()]
        public void CreateTilesTest()
        {
            MainWindow mainWindowz = new MainWindow();
            mainWindowz.CreateTiles(5, 5);
            Assert.AreEqual(25, mainWindowz.mapData.Count);
        }

        [TestMethod()]
        public void TileButtonMakerTest()
        {
            try
            {
                MainWindow mainWindowz = new MainWindow();
                mainWindowz.TileButtonMaker(1, 1, 1);
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void ChangeNeiboursTest()
        {
            try
            {
                MainWindow mainWindowz = new MainWindow();
                mainWindowz.CreateTiles(5, 5);
                mainWindowz.ChangeNeibours(8);
            }
            catch
            {
                Assert.Fail();
            }
        }
    }
}