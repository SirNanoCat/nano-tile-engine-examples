using System;

namespace Tileset
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new TilesetGame())
                game.Run();
        }
    }
}


