using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlatformerEngine;

namespace PlatformerTestGame.GameObjects
{
    public class CrosshairObject : GameObject
    {
        private MouseState mouseState;
        public CrosshairObject(Room room) : base(room, new Vector2(0,0))
        {
            Sprite = new SpriteData();
            Sprite.Size = new Vector2(24, 24);
            Sprite.LayerData = new LayerData(1);
            Sprite.Speed = 0.1f;
            Sprite.Offset = new Vector2(6, 6);
        }

        public override void Load(AssetManager assets)
        {
            assets.RequestFramedTexture("spr_crosshair", (frames) =>
            {
                Sprite.Change(frames);
            });
        }

        public override void Update()
        {
            mouseState = Mouse.GetState();
            Position = new Vector2(mouseState.X, mouseState.Y);
            base.Update();
        }

        public override void Draw(SpriteBatch batch, Vector2 offset)
        {
            Sprite.SpriteEffect = SpriteEffects.None;
            base.Draw(batch, Vector2.Zero);
        }
    }
}
