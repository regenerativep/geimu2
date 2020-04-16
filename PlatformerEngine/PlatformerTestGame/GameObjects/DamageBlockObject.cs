using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlatformerEngine;

namespace PlatformerTestGame.GameObjects
{
    class DamageBlockObject : GameObject
    {
        public DamageBlockObject(Room room, Vector2 pos) : base(room, pos)
        {

        }

        public const float DamageAmount = 10f;

        /*public void ChangeHitbox(Rectangle rect)
        {
            Hitbox = rect;
        }*/

        public override void Update()
        {
            GameObject coll = Room.FindCollision(PlatformerMath.AddVectorToRect(GetHitbox(), Position), "obj_player");
            if (coll != null)
            {
                ((PlayerObject)coll).Damage(1);
            }
            base.Update();
        }
    }
}
