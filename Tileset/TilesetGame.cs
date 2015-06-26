#region Using Statements
using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Nano.Input;
using Nano.Engine.Sys;
using Nano.Engine.Graphics;
using Nano.Engine.Graphics.Tileset;
using System.Collections.Generic;
using Nano.Engine.Graphics.Sprites;

#endregion

namespace Tileset
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class TilesetGame : Game
    {
        GraphicsDeviceManager m_Graphics;
        IInputService m_Input;
        ISpriteManager m_SpriteManager;
        ITileset m_Tileset;
        List<ISprite> m_Sprites;
        Random m_RNG;

        public TilesetGame()
        {
            m_Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";	            
            m_Graphics.IsFullScreen = true;
            m_Sprites = new List<ISprite>();
            m_RNG = new Random(123);
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

            ServiceLocator.Instance.SetServiceContainer(Services);
		
            var input = new InputManager(this);
            Components.Add(input);
            m_Input = input;

            ServiceLocator.Services.AddService<IInputService>(m_Input);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            m_SpriteManager = new SpriteManager(Content, new SpriteBatch(m_Graphics.GraphicsDevice));
            var tex2D = m_SpriteManager.CreateTexture2D("spritesheets/test-sprite-sheet");
            m_Tileset = new RegularTileset("test-tileset", tex2D, 4, 4, 32, 32);

            int screenWidth = m_Graphics.GraphicsDevice.Viewport.Width;
            int screenHeight = m_Graphics.GraphicsDevice.Viewport.Height;
           
            foreach(var bounds in m_Tileset.Bounds)
            {
                ISprite sprite = m_SpriteManager.CreateSprite(tex2D, bounds);

                int x = m_RNG.Next(0,screenWidth);
                int y = m_RNG.Next(0, screenHeight);

                sprite.Position = new Vector2(x, y);

                m_Sprites.Add(sprite);
            }
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (m_Input.KeyDown(Keys.Escape))
                Exit();

            //randomise sprite positions when space is pressed
            if( m_Input.KeyPressed(Keys.Space))
            {
                int screenWidth = m_Graphics.GraphicsDevice.Viewport.Width;
                int screenHeight = m_Graphics.GraphicsDevice.Viewport.Height;

                foreach(var sprite in m_Sprites)
                {
                    int x = m_RNG.Next(0,screenWidth);
                    int y = m_RNG.Next(0, screenHeight);

                    sprite.Position = new Vector2(x, y);
                }
            }

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            m_Graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
		
            foreach(var sprite in m_Sprites)
            {
                sprite.Draw();
            }
            
            base.Draw(gameTime);
        }
    }
}

