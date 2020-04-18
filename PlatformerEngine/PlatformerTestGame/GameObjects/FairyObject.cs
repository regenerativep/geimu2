using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlatformerEngine;
using Microsoft.Xna.Framework.Audio;

namespace PlatformerTestGame.GameObjects
{
    public class FairyObject : EnemyObject
    {
        public SoundEffect ShootSound;
        public Texture2D BulletSprite;
        public static float Gravity = 0.6f;
        public static float MaxHealth = 16;
        public static float MoveSpeed = .8f;
        public static float AirFriction = 1;
        public static Vector2 MaxVelocity = new Vector2(4, 16);
        public static float IdleMaxSpeed = 3;

        private bool facingRight;
        private Texture2D[] fairySprite;
        private int animationindex;
        private bool goingUp;
        private bool moving;
        private int cooldown;
        private int life;

        public FairyObject(Room room, Vector2 pos) : base(room, pos)
        {
            life = 3;
            cooldown = 60;
            moving = true;
            Position -= new Vector2(0, 16);
            facingRight = true;
            animationindex = -16;
            goingUp = true;
            fairySprite = null;
            IdleImage = null;
            RunImage = null;
        }
        public override void Load(AssetManager assets)
        {
            assets.RequestFramedTexture("obj_enemy_walk", (frames) =>
            {
                RunImage = frames;
                IdleImage = frames[0];
                Sprite.Size = new Vector2(32, 64);
                Sprite.Offset = -(new Vector2(Sprite.Size.X / 2, Sprite.Size.Y / 2 + (Sprite.Size.Y - Sprite.Size.X) / 2));
                Sprite.Speed = 0.2f;
            });
            assets.RequestTexture("spr_rainboworb", (texture) =>
            {
                BulletSprite = texture;
            });
            assets.RequestSound("snd_jumpreset", (sound) =>
            {
                ShootSound = sound;
            });
        }

        public override void Update()
        {
            if (cooldown > 0)
                cooldown--;
            if (animationindex == -10)
                goingUp = true;
            if (animationindex == 14)
                goingUp = false;
            if (goingUp)
                animationindex++;
            else
                animationindex--;
            Vector2 vel = Velocity;
            PlayerObject reimu = (PlayerObject)Room.FindObject("reimu");
            if (moving)
            {
                if (facingRight)
                    vel.X = MoveSpeed;
                else
                    vel.X = -MoveSpeed;
                if ((Math.Pow(Position.X - reimu.Position.X, 2) + Math.Pow(Position.Y - reimu.Position.Y, 2)) < (400 * 400))
                {
                    if (cooldown <= 0)
                        moving = false;
                }

            }
            else
            {
                Rectangle hitbox = reimu.GetHitbox();
                Vector2 size = new Vector2(hitbox.Width, hitbox.Height);
                Vector2 playerPos = reimu.Position + (size / 2);
                Vector2 fairyPos = Position + (size / 2);
                /*Vector2 fairyOnScreenPos = fairyPos - Room.ViewOffset;
                Vector2 playerRelativePos = playerPos - fairyOnScreenPos;*/
                float dir = (float)Math.Atan2(playerPos.Y - Position.Y, playerPos.X - Position.X);

                ProjectileObject proj = new ProjectileObject(Room, fairyPos, dir, 3, 1, "obj_player");
                proj.TimeToLive = 1000;
                proj.Sprite.Change(BulletSprite);
                proj.Sprite.Angle = dir;
                proj.Sprite.Size = new Vector2(16, 16);
                proj.Sprite.Origin = proj.Sprite.Size / 2;
                proj.Sprite.Offset = -(new Vector2(Sprite.Size.X / 2, Sprite.Size.Y / 2 + (Sprite.Size.Y - Sprite.Size.X) / 2));
                Room.GameObjectList.Add(proj);
                Room.Sounds.PlaySound(ShootSound);

                moving = true;
                cooldown = 60;
            }
            Velocity = vel;
            if (Room.FindCollision(PlatformerMath.AddVectorToRect(GetHitbox(), Position, new Vector2(1, 0)), "obj_block") != null)
            {
                facingRight = false;

            }
            if (Room.FindCollision(PlatformerMath.AddVectorToRect(GetHitbox(), Position, new Vector2(-1, 0)), "obj_block") != null)
            {
                facingRight = true;
            }
            if (Room.FindCollision(PlatformerMath.AddVectorToRect(GetHitbox(), Position, new Vector2(0, 1)), "obj_block") != null && !(Room.FindCollision(PlatformerMath.AddVectorToRect(GetHitbox(), Position, new Vector2(-1, 1)),"obj_block")!=null))
            {
                facingRight = true;
            }
            if (Room.FindCollision(PlatformerMath.AddVectorToRect(GetHitbox(), Position, new Vector2(0, 1)),"obj_block") != null && !(Room.FindCollision(PlatformerMath.AddVectorToRect(GetHitbox(), Position, new Vector2(1, 1)),"obj_block")!=null))
            {
                facingRight = false;
            }

            base.Update();
        }

        public override void Draw(SpriteBatch batch, Vector2 offset)
        {
            if (facingRight)
            {
                Sprite.SpriteEffect = SpriteEffects.FlipHorizontally;
            }
            else
            {
                Sprite.SpriteEffect = SpriteEffects.None;
            }
            Sprite.Offset = new Vector2(0, animationindex/2);
            base.Draw(batch, offset);
        }

        public void Damage()
        {
            if (life <= 0)
                Room.GameObjectList.Remove(this);
            else
                life--;
        }
    }
}
