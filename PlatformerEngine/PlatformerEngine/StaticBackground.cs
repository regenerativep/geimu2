using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformerEngine
{
    public class StaticBackground : Background
    {
        public SpriteData Image;
        public string assetName;
        public Room Room;
        public StaticBackground(Room room, string assetName)
        {
            Room = room;
            this.assetName = assetName;
            Image = new SpriteData();
        }
        public void Load(AssetManager assets)
        {
            assets.RequestTexture(assetName, (tex) =>
            {
                Image.Change(tex);
                Image.LayerData = new LayerData(2);
                Image.Size = new Vector2(Room.Engine.Game.GraphicsDevice.Viewport.Width, Room.Engine.Game.GraphicsDevice.Viewport.Height);
            });
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 offset)
        {
            Image?.Draw(spriteBatch, offset);
        }
    }
}
