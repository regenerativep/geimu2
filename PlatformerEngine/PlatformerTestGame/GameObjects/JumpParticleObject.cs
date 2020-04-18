using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlatformerEngine;

namespace PlatformerTestGame.GameObjects
{
    public class JumpParticleObject : GameObject
    {
        private int lifetime;

        public JumpParticleObject(Room room, Vector2 pos) : base(room, pos)
        {
            lifetime = 20;
        }
        public override void Load(AssetManager assets)
        {
            assets.RequestFramedTexture("jumpParticle", (frames) =>
            {
                Sprite.Change(frames);
                Sprite.Size = new Vector2(30, 6);
                Sprite.LayerData = new LayerData(1);
            });
        }
        public override void Update()
        {
            lifetime--;
            if (lifetime <= 0)
                Room.GameObjectList.Remove(this);
            base.Update();
        }

        public override void Draw(SpriteBatch batch, Vector2 offset)
        {
            base.Draw(batch, offset);
        }
    }
}
