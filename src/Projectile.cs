using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using MonoGame.Extended;
namespace No_Ghosts
{
    public abstract class Projectile : GameObject, IDrawable
    {
        protected int speed;
        public Vector2 attackPoint{get; set;}
        public Texture2D texture {get; set;}
        public Vector2 textureScale{get; set;}
        public Single angle {get; set;}
        public Color textureColor{get; set;}
        public CollisionBehavior collisionBehavior;
        protected float attackAngle;
        public int damage {get; set;}
        public virtual void draw(ref SpriteBatch spBatch)
        {
            spBatch.Draw(
                this.texture, this.position, null,
                Color.White, this.angle, new Vector2(this.texture.Width / 2, this.texture.Height / 2),
                this.textureScale, SpriteEffects.None, 0f);
        }
        public bool collidedWith(RectangleF rect)
        {
            if(this.collisionBehavior.Bounds.Intersects(rect))
                return true;
            return false;
        }
        public abstract void loadContent(ContentManager manager);
        public virtual void setTexture(Texture2D texture)
        {
            this.texture = texture;
            collisionBehavior.setRectangleAttrs(new Vector2(
                position.X - (texture.Width * textureScale.X) / 2,
                position.Y - (texture.Height * textureScale.Y) / 2
            ), new Size2(
                texture.Width * textureScale.X,
                texture.Height * textureScale.Y
            ));
        }
        public abstract float getDrawAngle();
        public virtual float getAttackAngle()
        {
            return MathF.Atan2(
                this.position.Y - this.attackPoint.Y, 
                this.position.X - this.attackPoint.X
            );
        }
        public virtual void attack(ref GameTime gameTime)
        {
            this.position.X += -MathF.Cos(this.attackAngle) * this.speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            this.position.Y += -MathF.Sin(this.attackAngle) * this.speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            this.collisionBehavior.Bounds.Position = new Vector2(
                position.X - (texture.Width * textureScale.X) / 2,
                position.Y - (texture.Height * textureScale.Y) / 2
            );
        }
    }
}