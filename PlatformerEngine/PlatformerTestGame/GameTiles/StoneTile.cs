using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlatformerEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformerTestGame.GameObjects
{
    public class StoneTile : GameTile
    {
        public StoneTile(Room room, Vector2 pos) : base(room, pos)
        {
            Sprite.Size = new Vector2(32, 32);
        }
        public override void Load(AssetManager assets)
        {
            assets.RequestTexture("spr_stone", (tex) =>
            {
                Sprite.Change(tex);
            });
            bool left = Room.CheckTileAt(Position - new Vector2(Sprite.Size.X, 0));
            bool right = Room.CheckTileAt(Position + new Vector2(Sprite.Size.X, 0));
            bool top = Room.CheckTileAt(Position - new Vector2(0, Sprite.Size.Y));
            bool bottom = Room.CheckTileAt(Position + new Vector2(0, Sprite.Size.Y));
            LayerData nextLayer = new LayerData(Sprite.LayerData.Layer + 1); //todo
            if (!bottom)
            {
                GameTile tl = new StoneSideBottomTile(Room, Position);
                tl.Sprite.LayerData = nextLayer;
                Room.GameTileList.Add(tl);
            }
            if (!top)
            {
                GameTile tl = new StoneSideTopTile(Room, Position);
                tl.Sprite.LayerData = nextLayer;
                Room.GameTileList.Add(tl);
            }
            if (!left)
            {
                GameTile tl = new StoneSideLeftTile(Room, Position);
                tl.Sprite.LayerData = nextLayer;
                Room.GameTileList.Add(tl);
            }
            if (!right)
            {
                GameTile tl = new StoneSideRightTile(Room, Position);
                tl.Sprite.LayerData = nextLayer;
                Room.GameTileList.Add(tl);
            }
        }
    }
}
