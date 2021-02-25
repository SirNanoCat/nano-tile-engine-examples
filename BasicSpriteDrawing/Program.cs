using System;

namespace BasicSpriteDrawing
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new SpriteDrawingGame())
                game.Run();
        }
    }
}