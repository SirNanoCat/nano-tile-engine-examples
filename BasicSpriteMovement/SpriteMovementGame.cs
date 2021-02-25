#region Using Statements

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Nano.Engine.Graphics.Sprites;
using Nano.Engine.IO.Input;
using Nano.Engine.Graphics;

#endregion

namespace BasicSpriteMovement
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class SpriteMovementGame : Game
    {
        GraphicsDeviceManager m_Graphics;

        ISprite m_Sprite;
        IInputService m_Input;
        ISpriteManager m_SpriteManager;

        public SpriteMovementGame()
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
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            m_SpriteManager = new SpriteManager(Content, new SpriteBatch(m_Graphics.GraphicsDevice)); 

            m_Sprite = m_SpriteManager.CreateSprite("Sprites/ship");

            int x = m_Graphics.GraphicsDevice.Viewport.Width / 2;
            int y = m_Graphics.GraphicsDevice.Viewport.Height / 2;

            m_Sprite.Position = new Vector2(x,y);
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

            m_SpriteManager.StartBatch();
            m_SpriteManager.DrawSprite(m_Sprite);
            m_SpriteManager.EndBatch();

            base.Draw(gameTime);
        }
    }
}

