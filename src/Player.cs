using System;
using MonoGame.Extended;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
namespace No_Ghosts
{
    public abstract class Player : GameObject, IDrawable
    {
        public Texture2D texture {get; set;}
        public Vector2 textureScale{get; set;}
        public Single angle {get; set;}
        public Color textureColor{get; set;}
        public CollisionBehavior collisionBehavior;
        public Vector2 shootPoint;
        public int bulletsInMagazine{get; protected set;} = 30;
        public abstract void draw(ref SpriteBatch spriteBatch);
        public abstract void loadContent(ContentManager manager);
        public abstract void update(GameTime gameTime, GameFactory factory, KeyboardState keyboardState, MouseState mouse, GraphicsDeviceManager _graphics);
        public abstract Projectile shoot(GameTime gameTime, GameFactory factory, MouseState mouse);
        

    }
    public class GrayPlayer : Player
    {
        private const float GUN_SHOOTING_SPEED = 0.2f;
        private const float GUN_RELOAD_TIME = 1f;
        private float lastGunShotTime;
        private float reloadTime;
        public Texture2D currentTexture;
        public SpriteEffects textureEffect;
        Texture2D deadTexture;
        public Single speed{get; set;}
        public GrayPlayer(Vector2 position)
        {
            this.position = position;
            collisionBehavior = new CollisionBehavior();
            textureScale = new Vector2(0.2f, 0.2f);
            angle = 0;
            textureEffect = SpriteEffects.None;
        }
        public override void update(GameTime gameTime, GameFactory factory, KeyboardState keyboardState, MouseState mouse, GraphicsDeviceManager _graphics)
        {
            if (keyboardState.IsKeyDown(Keys.A))
                this.moveLeft(gameTime, _graphics);
            if (keyboardState.IsKeyDown(Keys.D))
                this.moveRight(gameTime, _graphics);
            this.changeTextureAccordingToMousePosition(mouse);
            lastGunShotTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        public override Projectile shoot(GameTime gameTime, GameFactory factory, MouseState mouse)
        {
            shootPoint = new Vector2(this.position.X, this.position.Y);
            if(mouse.LeftButton == ButtonState.Pressed)
            {
                if (lastGunShotTime >= GrayPlayer.GUN_SHOOTING_SPEED && bulletsInMagazine > 0)
                {
                    lastGunShotTime = 0;
                    bulletsInMagazine -= 1;
                    return factory.CreateBullet(BulletType.PlayerRegularBullet, shootPoint, new Vector2(mouse.X, mouse.Y));
                }
            }
            if(bulletsInMagazine == 0)
            {
                reloadTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (reloadTime >= GrayPlayer.GUN_RELOAD_TIME)
                {
                    reloadTime = 0;
                    bulletsInMagazine = 30;
                }
            }
            return null;
        }
        public override void loadContent(ContentManager manager)
        {
            this.texture = manager.Load<Texture2D>("Textures/PlayerTextures/GrayPlayerAlive");
            this.deadTexture = manager.Load<Texture2D>("Textures/PlayerTextures/GrayPlayerDead");
            currentTexture = texture;
            collisionBehavior.setRectangleAttrs(new Vector2(
                position.X - (currentTexture.Width * textureScale.X) / 2,
                position.Y - (currentTexture.Height * textureScale.Y) / 2
            ), new Size2(
                currentTexture.Width * textureScale.X,
                currentTexture.Height * textureScale.Y
            ));
        }
        public void moveRight(GameTime gameTime, GraphicsDeviceManager graphics)
        {
            float distanceToTravel = speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if(
                position.X + distanceToTravel + (texture.Width * textureScale.X / 2) < graphics.PreferredBackBufferWidth 
                //&& position.X + distanceToTravel >= (texture.Width * textureScale.X / 2)
            )
            {
                this.position.X += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                this.collisionBehavior.setRectangleAttrs(
                    new Vector2(
                        position.X - (currentTexture.Width * textureScale.X) / 2,
                        position.Y - (currentTexture.Height * textureScale.Y) / 2
                    ), this.collisionBehavior.Bounds.Size);
            }
        }
        public void moveLeft(GameTime gameTime, GraphicsDeviceManager graphics)
        {
            float distanceToTravel = speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if(
                //position.X + distanceToTravel <= graphics.PreferredBackBufferWidth + (texture.Width * textureScale.Y / 2) &&
                position.X + distanceToTravel >= (texture.Width * textureScale.Y / 2)
            )
            {
                this.position.X -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                this.collisionBehavior.setRectangleAttrs(
                    new Vector2(
                        position.X - (currentTexture.Width * textureScale.X) / 2,
                        position.Y - (currentTexture.Height * textureScale.Y) / 2
                    ), this.collisionBehavior.Bounds.Size);
            }
        }
        public void changeTextureAccordingToMousePosition(MouseState mouse)
        {
            if(mouse.X > position.X)
            {
                textureEffect = SpriteEffects.None;
            }
            else
            {
                textureEffect = SpriteEffects.FlipHorizontally;
            }
        }
        public override void draw(ref SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.texture, position, null, Color.White, angle, new Vector2(texture.Width / 2, texture.Height / 2), textureScale, this.textureEffect, 0f);
        }
    }
}