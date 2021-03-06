using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlatformerEngine;

namespace PlatformerTestGame.GameObjects
{
    public class StoneSideTopTile : GameTile
    {
        public StoneSideTopTile(Room room, Vector2 pos) : base(room, pos)
        {
        }
        public override void Load(AssetManager assets)
        {
            assets.RequestTexture("spr_stoneedgetop", (tex) =>
            {
                Sprite.Change(tex);
                Sprite.Size = new Vector2(32, 32);
                Sprite.Speed = 0;
            });
        }
    }
}