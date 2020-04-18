using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformerEngine
{
    public class ParallaxBackground : Background
    {
        public string[] AssetNames;
        public float[] Speeds;
        public string TargetObjectName;
        public SpriteData[] Images;
        private Room room;
        public ParallaxBackground(Room room, string[] assetNames, float[] speeds, string targetObject)
        {
            AssetNames = assetNames;
            Speeds = speeds;
            TargetObjectName = targetObject;
            this.room = room;
        }
        public void Load(AssetManager assets)
        {
            Images = new SpriteData[AssetNames.Length];
            for(int i = 0; i < AssetNames.Length; i++)
            {
                var name = AssetNames[i];
                var sprite = new SpriteData();
                sprite.LayerData = new LayerData(2 + i);
                Images[i] = sprite;
                assets.RequestTexture(name, (tex) =>
                {
                    sprite.Change(tex);
                    sprite.Size = new Vector2(room.Engine.Game.GraphicsDevice.Viewport.Width, room.Engine.Game.GraphicsDevice.Viewport.Height) * 1.1f;
                    //sprite.Offset = sprite.Size / 2; //new Vector2((room.Engine.Game.GraphicsDevice.Viewport.Width - tex.Width) / 2, (room.Engine.Game.GraphicsDevice.Viewport.Height - tex.Height) / 2);
                });
            }
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 offset)
        {
            if (Images == null || Images.Length == 0) return;
            //find target
            GameObject target = room.FindObject(TargetObjectName);
            //calculate positions and draw images
            for(int i = 0; i < Images.Length; i++)
            {
                Images[i].Draw(spriteBatch, -(target.Position * Speeds[i])); //i hope this works
            }
        }
    }
}
