using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Platformer
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D playerSprite;
        Texture2D blockSprite;
        GameObject playerObject;
        Level level;
        Texture2D spikeSprite;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            playerSprite = Content.Load<Texture2D>("player");
            blockSprite = Content.Load<Texture2D>("block");
            spikeSprite = Content.Load<Texture2D>("spike");
            playerObject = new GameObject(playerSprite, new Vector2(50, 360));
            level = new Level(blockSprite, spikeSprite, "Level1.txt");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.R))
            {
                playerObject = new GameObject(playerSprite, new Vector2(50, 360));
                level = new Level(blockSprite, spikeSprite, "Level1.txt");
            }
                

            playerObject.Update(gameTime);
            level.Update(gameTime);

            foreach (Rectangle block in level.blocks)
            {
                if (block.Contains(playerObject.position.X + 32,
                    playerObject.position.Y + 64))
                {
                    playerObject.TouchedGround(block.Y);
                    break;
                }     
            }

            foreach (Rectangle spike in level.spikes)
            {
                if (spike.Intersects(playerObject.BoundingBox()))
                {
                    playerObject.TouchedGround(spike.Y);
                    playerObject.Kill();
                    break;
                }
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(transformMatrix: 
                Matrix.CreateTranslation(
                    new Vector3((playerObject.position.X * -1) + 
                        Window.ClientBounds.Width / 2, 0, 0)));

            playerObject.Draw(spriteBatch);
            level.Draw(spriteBatch);
            
            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
