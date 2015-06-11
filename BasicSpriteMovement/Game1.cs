#region Using Statements
using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;

using Nano.Engine.Sprites;
using Nano.Input;
using Nano.Engine.Sys;

#endregion

namespace BasicSpriteMovement
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager m_Graphics;
        SpriteBatch m_SpriteBatch;

        ISprite m_Sprite;
        IInputService m_Input;

        public Game1()
        {
            m_Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";              
            m_Graphics.IsFullScreen = true;     
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            var input = new InputManager(this);
            Components.Add(input);
            m_Input = input;

            int x = m_Graphics.GraphicsDevice.Viewport.Width / 2;
            int y = m_Graphics.GraphicsDevice.Viewport.Height / 2;

            m_Sprite.Position = new Vector2(x,y);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            m_SpriteBatch = new SpriteBatch(GraphicsDevice);

            Texture2D texture = Content.Load<Texture2D>("ship");
            m_Sprite = new BasicSprite(texture);
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {  
            if (m_Input.KeyDown(Keys.Escape))
                Exit();
            
            base.Update(gameTime);

            float x = m_Sprite.Position.X;
            float y = m_Sprite.Position.Y;
            float rotation = m_Sprite.Rotation;

            if (m_Input.KeyDown(Keys.D))
                x += 5;
            if (m_Input.KeyDown(Keys.A))
                x -= 5;
            if (m_Input.KeyDown(Keys.W))
                y -= 5;
            if (m_Input.KeyDown(Keys.S))
                y += 5;
            if (m_Input.KeyDown(Keys.E))
                rotation += 0.1f;
            if (m_Input.KeyDown(Keys.Q))
                rotation -= 0.1f;

            m_Sprite.Position = new Vector2(x,y);
            m_Sprite.Rotation = rotation;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            m_Graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            m_SpriteBatch.Begin();
            m_Sprite.Draw(m_SpriteBatch);
            m_SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

