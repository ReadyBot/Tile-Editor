using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {        
        private int userColor, tileIndexer, collumn, row;
        public Color thisColor = Colors.White;
        private Image emptySprite = null, sprite0;
        private string textFileLocation = "../../SavedTile/SaveFile.JSON";

        public List<int> mapData = new List<int>();
        public List<int> listNeigbours = new List<int> { -1, -1, -1, -1 };
        private List<int> tempList = new List<int> { 0, 0, 0, 0};

        public MainWindow()
        {
            InitializeComponent();
            LoadSpriteImages();
            
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        public Color GridColorName(int input)
        {
            if (input == 0) { thisColor = Colors.White; }
            if (input == 1) { thisColor = Colors.Red; }
            if (input == 2) { thisColor = Colors.Blue; }
            if (input == 3) { thisColor = Colors.Green; }
            if (input == 4) { thisColor = Colors.Yellow; }
            if (input == 5) { thisColor = Colors.Black; }
            if (input == 6) { thisColor = Colors.Pink; }
            if (input == 7) { thisColor = Colors.Violet; }
            return thisColor;
        }      
        void LoadSpriteImages()
        {
            sprite0 = new Image();
            sprite0.Source = new BitmapImage(new Uri(System.IO.Path.Combine(Directory.GetCurrentDirectory(),"../../Resources/sprite0.bmp")));
            bSprite.Content = sprite0;
        }
        public void TileButtonMaker(int numberButton, int collumn, int row)
        {
            Button tileButton = new Button();
            tileButton.Name = "Button" + numberButton.ToString();
            Grid.SetColumn(tileButton, collumn);
            Grid.SetRow(tileButton, row);
            tileButton.HorizontalAlignment = HorizontalAlignment.Stretch;
            tileButton.VerticalAlignment = VerticalAlignment.Stretch;
            tileButton.BorderThickness = new Thickness(0);
            
            tileButton.Click+= TileButtonClick;
            TileGrid.Children.Add(tileButton);
        }        
        int GetNeiboursAndIndex(int index)
        {
            int top = index - 1;
            int right = index + collumn;
            int down = index + 1;
            int left = index - collumn;
            int temp = 8; //All sprites start from colorcode 8.

            //Top
            if (top >= 0 && mapData[top] > 7)   {   temp += 1; listNeigbours[0] = top;  } 
            else { listNeigbours[0] = -1; }

            //Right
            if (right <mapData.Count && mapData[right] > 7) {   temp += 2; listNeigbours[1] = right;    } 
            else {  listNeigbours[1] = -1;  }

            //Down
            if (down < mapData.Count && mapData[down] <= 7) {   temp += 4; listNeigbours[2] = down;     } 
            else {  listNeigbours[2] = -1;  }

            //Left
            if (left >= 0 && mapData[left] <= 7)    {   temp += 8; listNeigbours[3] = left;     } 
            else  {     listNeigbours[3] = -1;  }

            return temp;
        }
        public Image GetSprite(int index)
        {
            //Numberize every combination of 1, 2, 3 and 4 contacting nabours, then set correct sprite.            
            Image newSprite = new Image();
            
            switch (index)
            {
                case 8:
                    newSprite.Source = new BitmapImage(new Uri(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "../../Resources/sprite0.bmp")));
                    break;
                case 9:
                    newSprite.Source = new BitmapImage(new Uri(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "../../Resources/sprite1.bmp")));
                    break;
                case 10:
                    newSprite.Source = new BitmapImage(new Uri(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "../../Resources/sprite2.bmp")));
                    break;
                case 11:
                    newSprite.Source = new BitmapImage(new Uri(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "../../Resources/sprite3.bmp")));
                    break;
                case 12:
                    newSprite.Source = new BitmapImage(new Uri(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "../../Resources/sprite4.bmp")));
                    break;
                case 13:
                    newSprite.Source = new BitmapImage(new Uri(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "../../Resources/sprite5.bmp")));
                    break;
                case 14:
                    newSprite.Source = new BitmapImage(new Uri(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "../../Resources/sprite6.bmp")));
                    break;
                case 15:
                    newSprite.Source = new BitmapImage(new Uri(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "../../Resources/sprite7.bmp")));
                    break;
                case 16:
                    newSprite.Source = new BitmapImage(new Uri(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "../../Resources/sprite8.bmp")));
                    break;
                case 17:
                    newSprite.Source = new BitmapImage(new Uri(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "../../Resources/sprite9.bmp")));
                    break;
                case 18:
                    newSprite.Source = new BitmapImage(new Uri(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "../../Resources/sprite10.bmp")));
                    break;
                case 19:
                    newSprite.Source = new BitmapImage(new Uri(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "../../Resources/sprite11.bmp")));
                    break;
                case 20:
                    newSprite.Source = new BitmapImage(new Uri(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "../../Resources/sprite12.bmp")));
                    break;
                case 21:
                    newSprite.Source = new BitmapImage(new Uri(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "../../Resources/sprite13.bmp")));
                    break;
                case 22:
                    newSprite.Source = new BitmapImage(new Uri(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "../../Resources/sprite14.bmp")));
                    break;
                case 23:
                    newSprite.Source = new BitmapImage(new Uri(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "../../Resources/sprite15.bmp")));
                    break;
            }
            return newSprite;
        }
        public void ChangeNeibours(int index)
        {
            if (mapData[index] > 7)
            {
                Button button = TileGrid.Children[index] as Button;
                int tmp = GetNeiboursAndIndex(index);
                mapData[index] = tmp;
                button.Content = GetSprite(tmp);
            }
        }
        void ExportFile()
        {
            Tiles tiles = new Tiles();
            tiles.Rows = row;
            tiles.Collumns = collumn;

            foreach(int userColor in mapData)
            {
                tiles.TileData.Add(userColor);
            }
            JsonSerializerSettings exportJson = new JsonSerializerSettings();
            exportJson.Formatting = Formatting.None;
            string jsonString = JsonConvert.SerializeObject(tiles, exportJson);            
            File.WriteAllText(textFileLocation, jsonString);

            MessageBox.Show("Your progress has been saved!\n\n It's been named SaveFile.JSON and placed into the SaveTile folder");
        }
        void ImportFile()
        {
            string currDir = System.IO.Path.Combine(Directory.GetCurrentDirectory(), textFileLocation);

            if (!File.Exists(currDir))
            {
                MessageBox.Show("There is no saved file! \n If you want to manually import a file, name it SaveFile.JSON and place it into the SaveTile folder");
                return;
            }

            string jsonString = File.ReadAllText(textFileLocation);
            Tiles importedFile = JsonConvert.DeserializeObject<Tiles>(jsonString);
            collumn = importedFile.Collumns;
            row = importedFile.Rows;
            CreateTiles(collumn, row);
                       
            for (int i = 0; i < TileGrid.Children.Count; i++)
            {
                mapData[i] = importedFile.TileData[i];
                Button button = TileGrid.Children[i] as Button;
                if (mapData[i] <= 7) { button.Background = new SolidColorBrush(GridColorName(mapData[i])); button.Content = emptySprite; }
                if (mapData[i] >= 7) { button.Background = new SolidColorBrush(Colors.White); button.Content = GetSprite(mapData[i]); }
            }

            tCol.Text = collumn.ToString();
            tRow.Text = row.ToString();

            MessageBox.Show("Import complete!");
        }
        public void CreateTiles(int collumn, int row)
        {
            tileIndexer = 0;
            TileGrid.Children.Clear();
            TileGrid.RowDefinitions.Clear();
            TileGrid.ColumnDefinitions.Clear();

            for (int i = 0; i < row; i++) { TileGrid.RowDefinitions.Add(new RowDefinition()); }
            for (int i = 0; i < collumn; i++) { TileGrid.ColumnDefinitions.Add(new ColumnDefinition()); }

            for (int i=0; i< collumn; i++ )
            {
                for(int j=0; j< row; j++)
                {
                    TileButtonMaker(tileIndexer, i, j);
                    mapData.Add(0);
                    tileIndexer++;
                }
            }
        }
        void TileButtonClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            string buttonName = button.Name;
            int buttonNameLength = buttonName.Length - 6;
            string subName = buttonName.Substring(6, buttonNameLength);
            int indexList = Int32.Parse(subName);

            if (userColor < 8)
            {
                button.Background = new SolidColorBrush(GridColorName(userColor));
                mapData[indexList] = userColor;
                button.Content = emptySprite;
            }
            if (userColor > 7)
            {
                int tmp = GetNeiboursAndIndex(indexList);
                userColor = tmp;
                button.Content = GetSprite(userColor);

                for (int i = 0; i < listNeigbours.Count; i++) { tempList[i] = listNeigbours[i]; }
                mapData[indexList] = userColor;
                for (int i = 0; i < tempList.Count; i++) { if (tempList[i] >= 0) { ChangeNeibours(tempList[i]); } }
            }
        }
        private void uColor_Click(object sender, RoutedEventArgs e) { MessageBox.Show("This is your active color/sprite!"); }
        private void bSprite_Click(object sender, RoutedEventArgs e){ 
            userColor = 8; 
            sprite0 = new Image();
            sprite0.Source = new BitmapImage(new Uri(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "../../Resources/sprite0.bmp")));
            uColor.Content = sprite0; 
        }
        private void Color0_Click(object sender, RoutedEventArgs e) { userColor = 0; uColor.Background = new SolidColorBrush(GridColorName(userColor)); uColor.Content = emptySprite; }
        private void Color1_Click(object sender, RoutedEventArgs e) { userColor = 1; uColor.Background = new SolidColorBrush(GridColorName(userColor)); uColor.Content = emptySprite; }
        private void Color2_Click(object sender, RoutedEventArgs e) { userColor = 2; uColor.Background = new SolidColorBrush(GridColorName(userColor)); uColor.Content = emptySprite; }
        private void Color3_Click(object sender, RoutedEventArgs e) { userColor = 3; uColor.Background = new SolidColorBrush(GridColorName(userColor)); uColor.Content = emptySprite; }
        private void Color4_Click(object sender, RoutedEventArgs e) { userColor = 4; uColor.Background = new SolidColorBrush(GridColorName(userColor)); uColor.Content = emptySprite; }
        private void Color5_Click(object sender, RoutedEventArgs e) { userColor = 5; uColor.Background = new SolidColorBrush(GridColorName(userColor)); uColor.Content = emptySprite; }
        private void Color6_Click(object sender, RoutedEventArgs e) { userColor = 6; uColor.Background = new SolidColorBrush(GridColorName(userColor)); uColor.Content = emptySprite; }
        private void Color7_Click(object sender, RoutedEventArgs e) { userColor = 7; uColor.Background = new SolidColorBrush(GridColorName(userColor)); uColor.Content = emptySprite; }

        private void GenBtn_Click(object sender, RoutedEventArgs e)
        {
            collumn = int.Parse(tCol.Text);
            row = int.Parse(tRow.Text);
            if (collumn < 0) { collumn = 42; }
            if (row < 0) { row = 42; }
            CreateTiles(collumn, row);            
        }
        private void CABtn_Click(object sender, RoutedEventArgs e)
        {            
            for (int i = 0; i < TileGrid.Children.Count; i++)
            {
                Button button = TileGrid.Children[i] as Button;
                button.Background = new SolidColorBrush(GridColorName(userColor));
                mapData[i] = userColor;
            }
        }
        private void ImportBtn_Click(object sender, RoutedEventArgs e)
        {
            mapData.Clear();
            ImportFile();            
        }
        private void ExportBtn_Click(object sender, RoutedEventArgs e)
        {
            Thread t = new Thread(ExportFile);
            t.Start();
        }
        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("The end is near! Hope you saved your work, or else you will regret it!");
            this.Close();
        }
    }
}
