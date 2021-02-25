using System;

namespace IsoTilemap
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new IsoTilemapGame())
                game.Run();
        }
    }

}


