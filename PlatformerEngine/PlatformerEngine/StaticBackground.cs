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
        public StaticBackground(string assetName)
        {
            this.assetName = assetName;
            Image = new SpriteData();
        }
        public void Load(AssetManager assets)
        {
            assets.RequestTexture(assetName, (tex) =>
            {
                Image.Change(tex);
                Image.LayerData = new LayerData(2);
            });
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Image?.Draw(spriteBatch, new Vector2(0, 0));
        }
    }
}
