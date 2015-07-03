using System;
using Nano.StateManagement;
using Microsoft.Xna.Framework;
using Nano.Engine.IO.Input;
using Microsoft.Xna.Framework.Input;

namespace SimpleStateManagement
{
    public class GamePlayScreen : GameState
    {
        GraphicsDeviceManager m_Graphics;
        IInputService m_Input;

        public GamePlayScreen(IGameStateService manager, IInputService input, GraphicsDeviceManager graphics)
            :base(manager)
        {
            m_Graphics = graphics;
            m_Input = input;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (m_Input.KeyPressed(Keys.A))
                StateManager.PopState();

        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            m_Graphics.GraphicsDevice.Clear(Color.Green);
        }
    }
}

