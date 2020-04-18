using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using PlatformerEngine;

namespace PlatformerTestGame.GameObjects
{
    public class JumpResetObject : GameObject
    {
        private static int StepsUntilReset = 180;
        public bool IsActive { get; set; }
        private int stepsRemaning;
        public JumpResetObject(Room room, Vector2 pos) : base(room, pos)
        {
            IsActive = true;
            stepsRemaning = 0;
        }
        public override void Load(AssetManager assets)
        {
            assets.RequestTexture("spr_jumpreset", (frames) =>
            {
                Sprite.Change(frames);
                Sprite.Speed = 0.5f;
                Sprite.Size = new Vector2(24, 24);
            });
        }
        public override void Update()
        {
            if(!IsActive)
            {
                if(stepsRemaning > 0)
                {
                    stepsRemaning--;
                }
                else
                {
                    IsActive = true;
                }
            }
            base.Update();
        }
        public void Use()
        {
            PlayerObject player = (PlayerObject)Room.FindObject("obj_player");
            player.ResetJumps();
            IsActive = false;
            stepsRemaning = StepsUntilReset;
        }
        public override void Draw(SpriteBatch batch, Vector2 offset)
        {
            Sprite.Draw(batch, Position - offset - Sprite.Offset, Color.White * (IsActive ? 1f : 0.5f));
        }
    }
}
