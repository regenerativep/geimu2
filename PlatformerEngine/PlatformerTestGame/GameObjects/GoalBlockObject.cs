using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlatformerEngine;

namespace PlatformerTestGame.GameObjects
{
    class GoalBlockObject : GameObject
    {
        public static bool HasPlayedSound = false;
        private SoundEffect levelCompleteSound;
        public GoalBlockObject(Room room, Vector2 pos) : base(room, pos)
        {
        }
        public override void Load(AssetManager assets)
        {
            assets.RequestSound("levelComplete", (sound) =>
            {
                levelCompleteSound = sound;
            });
        }
        public override void Update()
        {
            GameObject coll = Room.FindCollision(PlatformerMath.AddVectorToRect(GetHitbox(), Position), "reimu");
            if (coll != null)
            {
                if (Room.FindObject("fairy") == null && Room.FindObject("clownpiece") == null)
                {
                    if (!HasPlayedSound)
                    {
                        Room.Sounds.PlaySound(levelCompleteSound);
                        HasPlayedSound = true;
                    }
                    Room nextRoom = new Room(Room.Engine);
                    //find the room number
                    int num = Convert.ToInt32(Room.CurrentFileName.Substring(12, Room.CurrentFileName.IndexOf('.')));
                    nextRoom.Load("levels/level" + (num + 1) + ".json");
                    Room.Engine.ChangeRoom(nextRoom);
                }
            }
            base.Update();
        }
    }
}
