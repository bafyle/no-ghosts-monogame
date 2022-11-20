using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
namespace No_Ghosts
{
    public abstract class Enemy : Projectile
    {
        public int health {get; set;}
        public override float getDrawAngle()
        {
            float angle = getAttackAngle()+MathF.PI;
            return angle;
        }
    }

    public class OrangeEnemy : Enemy
    {
        public OrangeEnemy(Vector2 position, Vector2 attackPoint)
        {
            collisionBehavior = new CollisionBehavior();
            this.position = position;
            this.attackPoint = attackPoint;
            this.health = 3;
            this.speed = 150;
        }
        public override void setTexture(Texture2D texture)
        {
            this.textureScale = new Vector2(0.2f, 0.2f);
            base.setTexture(texture);
            this.angle = this.getDrawAngle()-MathF.PI/2;
            this.attackAngle = this.getAttackAngle();
        }
        public override void loadContent(ContentManager manager)
        {
            throw new NotImplementedException();
        }
    }
    public class BlueEnemy : Enemy
    {
        public BlueEnemy(Vector2 position, Vector2 attackPoint)
        {
            collisionBehavior = new CollisionBehavior();
            this.position = position;
            this.attackPoint = attackPoint;
            this.health = 3;
            this.speed = 150;
        }
        public override void setTexture(Texture2D texture)
        {
            base.setTexture(texture);
            this.textureScale = new Vector2(0.2f, 0.2f);
            this.angle = this.getDrawAngle();
            this.attackAngle = this.getAttackAngle();
        }
        public override void loadContent(ContentManager manager)
        {
            throw new NotImplementedException();
        }
    }
    
}