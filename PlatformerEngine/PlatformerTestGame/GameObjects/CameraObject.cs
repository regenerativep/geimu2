using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using PlatformerEngine;

namespace PlatformerTestGame.GameObjects
{
    public class CameraObject : GameObject
    {
        public GameObject Target { get; set; }
        public float TargetPreference { get; set; }
        public float SelfPreference { get; set; }
        public CameraObject(Room room, Vector2 pos) : base(room, pos)
        {
            Target = Room.FindObject("obj_player");
            TargetPreference = 1;
            SelfPreference = 2;
        }
        public override void Update()
        {
            if (Target == null)
            {
                Target = Room.FindObject("obj_player");
                return;
            }
            Vector2 pos = Position;
            pos.X = ((TargetPreference * (Target.Position.X)) + (SelfPreference * pos.X)) / (TargetPreference + SelfPreference);
            pos.Y = ((TargetPreference * (Target.Position.Y)) + (SelfPreference * pos.Y)) / (TargetPreference + SelfPreference);
            Position = pos;
            Room.ViewPosition = (Target.Position * 2 - Position) - (new Vector2(Room.Engine.Game.GraphicsDevice.Viewport.Width, Room.Engine.Game.GraphicsDevice.Viewport.Height) / 2);
        }
    }
}
