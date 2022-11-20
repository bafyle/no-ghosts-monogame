using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace No_Ghosts
{
    public enum EnemyType
    {
        OrangeEnemy = 1,
        BlueEnemy = 2
    }
    public enum BulletType
    {
        PlayerRegularBullet = 1,
        PlayerSuperBullet = 2,
        BlueEnemyMissile = 3
    }
    public interface Factory
    {
        public abstract Enemy CreateEnemy(EnemyType enemyType);
        public abstract Projectile CreateBullet(BulletType bulletType, Vector2 position, Vector2 attackPoint);
    }
    public class GameFactory : Factory
    {
        public Player player{set; private get;}
        private Texture2D OrangeEnemyTexture;
        private Texture2D BlueEnemyTexture;
        private Texture2D RegularBulletTexture;
        private Random randomGenerator;
        private int EnemyCreationPositionXHigh;
        private int EnemyCreationPositionXLow;
        public GameFactory(Texture2D OET, Texture2D BET, Texture2D RBT)
        {
            randomGenerator = new Random();
            this.OrangeEnemyTexture = OET;
            this.BlueEnemyTexture = BET;
            this.RegularBulletTexture = RBT;
        }
        public void setEnemyCreationXPositionRange(int low, int high)
        {
            this.EnemyCreationPositionXHigh = high;
            this.EnemyCreationPositionXLow = low;
        }
        public Enemy CreateEnemy(EnemyType enemyType)
        {
            Vector2 position = new Vector2(randomGenerator.Next(EnemyCreationPositionXLow, EnemyCreationPositionXHigh), -50);
            
            Vector2 attackPoint = player.position;
            Enemy enemy;
            switch(enemyType)
            {
                case EnemyType.OrangeEnemy:
                    enemy = new OrangeEnemy(position, attackPoint);
                    enemy.setTexture(this.OrangeEnemyTexture);
                    break;
                case EnemyType.BlueEnemy:
                    enemy = new OrangeEnemy(position, attackPoint);
                    enemy.setTexture(this.OrangeEnemyTexture);
                    break;
                default:
                    enemy = null;
                    break;
            }
            return enemy;
        }
        public Projectile CreateBullet(BulletType bulletType, Vector2 position, Vector2 attackPoint)
        {
            Projectile bullet;
            switch(bulletType)
            {
                case BulletType.PlayerRegularBullet:
                    bullet = new RegularPlayerBullet(position, attackPoint);
                    bullet.setTexture(this.RegularBulletTexture);
                    break;
                
                default:
                    bullet = null;
                    break;
            }
            return bullet;
        }
    }
}