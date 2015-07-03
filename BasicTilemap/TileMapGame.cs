#region Using Statements
using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Nano.Engine.IO.Input;
using Nano.Engine.Graphics;
using Nano.Engine.Graphics.Tileset;
using Nano.Engine.Sys;
using Nano.Engine;
using System.Threading;
using System.Collections.Generic;
using Nano.Engine.Cameras;
using OpenTK.Input;

#endregion

namespace BasicTilemap
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class TileMapGame : Game
    {
        GraphicsDeviceManager m_Graphics;
        IInputService m_Input;
        ISpriteManager m_SpriteManager;
        ITileset m_Tileset;
        TileMap m_TileMap;
        ICamera m_Camera;
        Random m_RNG;

        public TileMapGame()
        {
            m_Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";	            
            m_Graphics.IsFullScreen = true;		
            m_RNG = new Random(12345);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            ServiceLocator.Instance.SetServiceContainer(Services);

            var input = new InputManager(this);
            Components.Add(input);
            m_Input = input;

            ServiceLocator.Services.AddService<IInputService>(m_Input);

            base.Initialize();	
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            int vpWidth = m_Graphics.GraphicsDevice.Viewport.Width;
            int vpHeight = m_Graphics.GraphicsDevice.Viewport.Height;
            var rect = new Rectangle(0, 0, vpWidth, vpHeight);

            m_Camera = new Camera2D(rect);

            m_SpriteManager = new SpriteManager(Content, new SpriteBatch(m_Graphics.GraphicsDevice));
            var tex2D = m_SpriteManager.CreateTexture2D("spritesheets/basic-tiles");

            m_Tileset = new RegularTileset("test-tileset", tex2D, 4, 4, 32, 32);

            var tilesets = new List<ITileset>();
            tilesets.Add(m_Tileset);

            MapLayer layer = new MapLayer("ground layer", 100, 100);

            for(int y = 0; y < 100; y++)
            {
                for(int x = 0;x < 100; x++)
                {
                    int idx = m_RNG.Next(0,3);
                    layer.SetTile(x,y,new TilesetTile(idx,0));
                }
            }

            var layers = new List<MapLayer>();
            layers.Add(layer);

            m_TileMap = new TileMap(m_SpriteManager, TileMapType.Square, 32, 32, tilesets, layers);
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

            float x = m_Camera.Position.X;
            float y = m_Camera.Position.Y;


            if (m_Input.KeyDown(Keys.D))
                x += 5;
            if (m_Input.KeyDown(Keys.A))
                x -= 5;
            if (m_Input.KeyDown(Keys.W))
                y -= 5;
            if (m_Input.KeyDown(Keys.S))
                y += 5;

            m_Camera.Position = new Vector2(x, y);
            m_Camera.Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            m_Graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
		
            m_TileMap.Draw(m_Camera);
            
            base.Draw(gameTime);
        }
    }
}

