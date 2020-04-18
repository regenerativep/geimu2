using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PlatformerEngine;
using PlatformerEngine.Physics;
using PlatformerTestGame.GameObjects;
using System;

namespace PlatformerTestGame
{
    /// <summary>
    /// main area for the platformer game
    /// </summary>
    public class GeimuGame : Game
    {
        /// <summary>
        /// game's graphics device manager
        /// </summary>
        public GraphicsDeviceManager Graphics;
        public PEngine Engine;
        private SpriteBatch spriteBatch;
        /// <summary>
        /// creates a new instance of the platformer game
        /// </summary>
        public GeimuGame()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        /// <summary>
        /// initializes the platformer game
        /// </summary>
        protected override void Initialize()
        {
            IsMouseVisible = true;
            Window.Title = "Geimu 2";
            ChangeResolution(1024, 768);

            Engine = new PEngine(this);
            PEngine.NameToType["obj_block"] = typeof(BlockObject); //if there is a better way to go about doing this please tell
            PEngine.NameToType["obj_player"] = typeof(PlayerObject);
            PEngine.NameToType["obj_item"] = typeof(ItemObject);
            PEngine.NameToType["obj_upgrade"] = typeof(UpgradeObject);
            PEngine.NameToType["obj_enemy"] = typeof(EnemyObject);
            PEngine.NameToType["obj_boss"] = typeof(BossObject);
            PEngine.NameToType["obj_win"] = typeof(GameWinObject);
            PEngine.NameToType["obj_lose"] = typeof(GameOverObject);
            PEngine.NameToType["obj_camera"] = typeof(CameraObject);
            PEngine.NameToType["obj_crosshair"] = typeof(CrosshairObject);
            PEngine.NameToType["obj_jumpparticle"] = typeof(JumpParticleObject);
            PEngine.NameToType["obj_jumpreset"] = typeof(JumpResetObject);
            PEngine.NameToType["obj_goalblock"] = typeof(GoalBlockObject);
            PEngine.NameToType["obj_fairy"] = typeof(FairyObject);
            PEngine.NameToType["obj_damageblock"] = typeof(DamageBlockObject);
            PEngine.NameToType["tle_stonebrick"] = typeof(StoneBrickTile);

            Engine.ChangeRoom((new Room(Engine)).Load("levels/level1.json"));

            base.Initialize();
        }
        /// <summary>
        /// sets the game to a state of fullscreen
        /// </summary>
        /// <param name="full">whether to make fullscreen</param>
        public void SetFullscreen(bool full)
        {
            Graphics.IsFullScreen = full;
            Graphics.ApplyChanges();
        }
        /// <summary>
        /// changes the resolution of the game
        /// </summary>
        /// <param name="width">the new width</param>
        /// <param name="height">the new height</param>
        public void ChangeResolution(int width, int height)
        {
            Graphics.PreferredBackBufferWidth = width;
            Graphics.PreferredBackBufferHeight = height;
            Graphics.ApplyChanges();
        }
        /// <summary>
        /// loads game content
        /// LoadContent will be called once per game
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Engine.Load("Data/assets.json");
            Engine.CurrentRoom.LightEffect = Content.Load<Effect>("lighteffect"); //dont want to add another part to the asset manager just for this
        }

        /// <summary>
        /// unloads game content
        /// UnloadContent will be called once per game
        /// </summary>
        protected override void UnloadContent()
        {
        }

        /// <summary>
        /// called every tick event
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || keyState.IsKeyDown(Keys.Escape))
                Exit();
            Engine.Update();
            base.Update(gameTime);
        }
        
        /// <summary>
        /// called every frame draw event
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            Engine.Draw(spriteBatch);
            base.Draw(gameTime);
        }
    }
}
