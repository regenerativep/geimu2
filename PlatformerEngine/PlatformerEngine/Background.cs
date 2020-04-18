using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformerEngine
{
    public interface Background
    {
        void Load(AssetManager assets);
        void Draw(SpriteBatch spriteBatch, Vector2 viewOffset);
    }
}
