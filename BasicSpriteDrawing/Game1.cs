#region Using Statements
using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;

using Nano.Engine.Graphics.Sprites;
using Nano.Engine.Graphics;


#endregion

namespace BasicSpriteDrawing
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager m_Graphics;
        ISpriteManager m_SpriteManager;

        ISprite m_Sprite;

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
            // TODO: Add your initialization logic here
            base.Initialize();
		
            m_SpriteManager = new SpriteManager(Content, new SpriteBatch(m_Graphics.GraphicsDevice)); 
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            m_Sprite = m_SpriteManager.CreateSprite("ship",new Rectangle());
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if(Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            // TODO: Add your update logic here			
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            m_Graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
		
            //TODO: Add your drawing code here
            int x = m_Graphics.GraphicsDevice.Viewport.Width / 2;
            int y = m_Graphics.GraphicsDevice.Viewport.Height / 2;

            m_Sprite.Position = new Vector2(x,y);

            m_SpriteManager.StartBatch();
            m_SpriteManager.DrawSprite(m_Sprite);
            m_SpriteManager.EndBatch();

            base.Draw(gameTime);
        }
    }
}

