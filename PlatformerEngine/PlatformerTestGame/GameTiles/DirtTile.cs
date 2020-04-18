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
    public class DirtTile : GameTile
    {
        public DirtTile(Room room, Vector2 pos) : base(room, pos)
        {
            
        }
        public override void Load(AssetManager assets)
        {
            assets.RequestTexture(PlatformerMath.Choose("dirt", "dirt2"), (tex) =>
            {
                Sprite.Change(tex);
                Sprite.Size = new Vector2(32, 32);
            });
            bool left = Room.CheckTileAt(Position - new Vector2(Sprite.Size.X, 0));
            bool right = Room.CheckTileAt(Position + new Vector2(Sprite.Size.X, 0));
            bool top = Room.CheckTileAt(Position - new Vector2(0, Sprite.Size.Y));
            bool bottom = Room.CheckTileAt(Position + new Vector2(0, Sprite.Size.Y));
            LayerData nextLayer = new LayerData(Sprite.LayerData.Layer - 1); //todo
            if (!bottom)
            {
                GameTile tl = new DirtSideBottomTile(Room, Position);
                tl.Sprite.LayerData = nextLayer;
                Room.GameTileList.Add(tl);
            }
            if (!top)
            {
                GameTile tl = new DirtSideTopTile(Room, Position);
                tl.Sprite.LayerData = nextLayer;
                Room.GameTileList.Add(tl);
            }
            if (!left)
            {
                GameTile tl = new DirtSideLeftTile(Room, Position);
                tl.Sprite.LayerData = nextLayer;
                Room.GameTileList.Add(tl);
            }
            if (!right)
            {
                GameTile tl = new DirtSideRightTile(Room, Position);
                tl.Sprite.LayerData = nextLayer;
                Room.GameTileList.Add(tl);
            }
        }
    }
}
