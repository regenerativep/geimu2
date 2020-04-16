using PlatformerEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PlatformerEngine.Physics;
using Microsoft.Xna.Framework.Graphics;
using PlatformerTestGame.Items;

namespace PlatformerTestGame.GameObjects
{
    public class EnemyObject : GameObject, IDamagable
    {
        public static float DefaultMaxHealth = 1;
        public Item Item;
        public InputManager Input;
        public KeyInputTrigger Left, Right, Jump;
        public float GroundSpeed, AirSpeed, JumpSpeed;
        public float MinDirectionChangeSpeed, MinRunAnimationSpeed;
        public float Health;
        public int MaxMeleeCooldown, MeleeCooldown;
        public bool Grounded;
        public Texture2D IdleImage;
        public Texture2D[] RunImage;
        public MouseState MouseState, PrevMouseState;
        private Room originalRoom;
        public EnemyObject(Room room, Vector2 position) : base(room, position)
        {
            originalRoom = Room;
            Input = new InputManager();
            GroundSpeed = 1f;
            AirSpeed = 0.1f;
            JumpSpeed = -14f;
            Grounded = false;
            Health = DefaultMaxHealth;
            Velocity = new Vector2(0, 0);
            MinDirectionChangeSpeed = 1f;
            MinRunAnimationSpeed = 2f;
            IdleImage = null;
            RunImage = null;
            MaxMeleeCooldown = 30;
            MeleeCooldown = 0;
        }
        public void Damage(float amount)
        {
            Health -= amount;
            if(Health <= 0)
            {
                Room.GameObjectList.Remove(this);
                originalRoom.GameObjectList.Remove(this);
                PlayerObject playerObject = (PlayerObject)Room.FindObject("obj_player");
                if(playerObject != null)
                {
                    playerObject.MaxHealth += 4;
                    playerObject.Health += 4;
                }
            }
        }
        
        public override void Update()
        {
            float moveSpeed = AirSpeed;
            if(Room.FindCollision(new Rectangle((Position + Sprite.Offset + new Vector2(0, 8)).ToPoint(), Sprite.Size.ToPoint()), "obj_block") != null)
            {
                Grounded = true;
                moveSpeed = GroundSpeed;
                if(Math.Abs(Velocity.X) > GroundFriction)
                {
                    Velocity.X -= Math.Sign(Velocity.X) * GroundFriction;
                }
                else
                {
                    Velocity.X = 0;
                }
            }
            else
            {
                Grounded = false;
            }

            //AI CODE HERE
            PlayerObject Player = (PlayerObject)Room.FindObject("obj_player");
            if (Player != null)
            {
                float TargetX = Player.Position.X;
                float TargetY = Player.Position.Y;
                if (Position.X > TargetX)
                {
                    Velocity.X = -(MaxVelocity.X);
                }
                else
                {
                    Velocity.X = MaxVelocity.X;
                }
                if (Position.Y > TargetY)
                {
                    Velocity.Y = -(MaxVelocity.Y);
                }
                else
                {
                    Velocity.Y = MaxVelocity.Y;
                }
                if (MeleeCooldown == 0 && Room.FindCollision(new Rectangle((Position + Sprite.Offset).ToPoint(), Sprite.Size.ToPoint()), "obj_player") != null)
                {
                    MeleeCooldown = MaxMeleeCooldown;
                    Player.Damage(2);
                }
            }
            else
            {
                Velocity.X = 0;
            }
            
            

            if (Velocity.X > MinDirectionChangeSpeed)
            {
                Sprite.SpriteEffect = SpriteEffects.None;
            }
            else if (Velocity.X < -MinDirectionChangeSpeed)
            {
                Sprite.SpriteEffect = SpriteEffects.FlipHorizontally;
            }
            Sprite.Change(RunImage);
            
            
            if (Position.X > Room.Width)
            {
                Room.GameObjectList.Remove(this); //might not work
            }
            if(MeleeCooldown > 0)
            {
                MeleeCooldown--;
            }
            
            base.Update();
        }
    }
}
