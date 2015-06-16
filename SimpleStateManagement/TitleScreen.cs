using System;
using Nano.StateManagement;
using Microsoft.Xna.Framework;
using Nano.Input;
using Microsoft.Xna.Framework.Input;

namespace SimpleStateManagement
{
    public class TitleScreen : GameState
    {
        GraphicsDeviceManager m_Graphics;
        IInputService m_Input;

        public TitleScreen(IGameStateService manager, IInputService input, GraphicsDeviceManager graphics)
            :base(manager)
        {
            m_Graphics = graphics;
            m_Input = input;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (Enabled)
            {
                if (m_Input.KeyPressed(Keys.A))
                    StateManager.PushState(new GamePlayScreen(StateManager, m_Input, m_Graphics));
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            m_Graphics.GraphicsDevice.Clear(Color.AntiqueWhite);
        }
    }
}

