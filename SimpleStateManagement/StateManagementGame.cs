#region Using Statements
using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Nano.Engine.IO.Input;
using Nano.StateManagement;
using Nano.Engine.Sys;

#endregion

namespace SimpleStateManagement
{   
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class StateManagementGame : Game
    {
        GraphicsDeviceManager graphics;

        IInputService m_Input;
        IGameStateService m_StateService;
        IGameState m_TitleScreen;

        public StateManagementGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";              
            graphics.IsFullScreen = true;   
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

            ServiceLocator.Instance.SetServiceContainer(Services);

            var input = new InputManager(this);
            Components.Add(input);
            m_Input = input;
            ServiceLocator.Services.AddService<IInputService>(m_Input);

            var stateManager = new GameStateManager(this, new NullLogger());
            Components.Add(stateManager);
            m_StateService = stateManager;

            m_TitleScreen = new TitleScreen(m_StateService, m_Input, graphics);

            m_StateService.ChangeState(m_TitleScreen);

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
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
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            //TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}

