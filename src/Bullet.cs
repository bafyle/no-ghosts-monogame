using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
namespace No_Ghosts
{
    public abstract class PlayerBullet : Projectile
    {
        public PlayerBullet(Vector2 position, Vector2 attackPoint)
        {
            this.position = position;
            this.attackPoint = attackPoint;
            this.speed = 300;
            this.collisionBehavior = new CollisionBehavior();
        }
        
    }
    public class RegularPlayerBullet : PlayerBullet
    {
        public RegularPlayerBullet(Vector2 position, Vector2 attackPoint) : base(position, attackPoint)
        {
            this.damage = 1;
        }
        public override float getDrawAngle()
        {
            float bustedAngleRad = getAttackAngle()+MathF.PI*2;
            return bustedAngleRad;
        }
        public override void setTexture(Texture2D texture)
        {
            this.textureScale = new Vector2(0.05f, 0.05f);
            base.setTexture(texture);
            this.angle = getDrawAngle();
            this.attackAngle = this.getAttackAngle();
        }
        public override void loadContent(ContentManager manager)
        {
            throw new NotImplementedException();
        }
    }
}