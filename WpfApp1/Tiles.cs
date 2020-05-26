using System.Collections.Generic;

namespace WpfApp1
{
    class Tiles
    {     
        public Tiles()
        {
            TileData = new List<int>();
        }

        public int Rows { get; set; }
        public int Collumns { get; set; }

        public List<int> TileData { get; set; }
    }
}
