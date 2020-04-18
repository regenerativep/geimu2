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
    public abstract class EnemyObject : GameObject, IDamagable
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
            }
        }
    }
}
