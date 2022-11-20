using System;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using MonoGame.Extended;
namespace No_Ghosts;

public class Game1 : Game
{
    Point attackPoint;
    GameFactory gameFactory;
    ArrayList enemies;
    ArrayList bullets;
    float currentTime;
    Random randomNumberGenerator;
    Texture2D playerTexture;
    GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    GrayPlayer mainPlayer ;
    SpriteFont mainFont;
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        _graphics.PreferredBackBufferWidth = 800;
        _graphics.PreferredBackBufferHeight = 600;
        _graphics.ApplyChanges();
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        // var mouseState = Mouse.GetState();
        enemies = new ArrayList();
        bullets = new ArrayList();
        randomNumberGenerator = new Random();
        mainPlayer = new GrayPlayer(new Vector2(400, 550));
        mainPlayer.speed = 150f;
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        this.playerTexture = Content.Load<Texture2D>("Textures/PlayerTextures/GrayPlayerAlive");
        gameFactory = new GameFactory(
            Content.Load<Texture2D>("Textures/EnemyTextures/OrangeEnemy"),
            this.playerTexture, 
            Content.Load<Texture2D>("Textures/BulletTextures/PlayerBullet"));
        gameFactory.player = mainPlayer;
        gameFactory.setEnemyCreationXPositionRange(-100, 900);
        mainPlayer.loadContent(Content);

        mainFont = Content.Load<SpriteFont>("fonts/Main Font");

    }

    protected override void Update(GameTime gameTime)
    {
        KeyboardState keyboardState = Keyboard.GetState();
        MouseState mouse = Mouse.GetState();
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || keyboardState.IsKeyDown(Keys.Escape))
            Exit();
        mainPlayer.update(gameTime, gameFactory, keyboardState, mouse, _graphics);
        Projectile newBullet = mainPlayer.shoot(gameTime, gameFactory, mouse);
        if(newBullet != null)
        {
            this.bullets.Add(newBullet);
        }
        currentTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (currentTime >= 0.5f)
        {
            currentTime = 0;
            enemies.Add(gameFactory.CreateEnemy(EnemyType.OrangeEnemy));
        }
        for(int i = 0; i < enemies.Count; i++)
        {
            Enemy e = (Enemy)enemies[i];
            e.attack(ref gameTime);
            if(
                e.position.X > _graphics.PreferredBackBufferWidth+20 || 
                e.position.Y > _graphics.PreferredBackBufferHeight+20 
            )
                enemies.RemoveAt(i);
            if(e.collidedWith(mainPlayer.collisionBehavior.Bounds))
                enemies.RemoveAt(i);
        }
        for(int i = 0; i < bullets.Count; i++)
        {
            PlayerBullet pb = (PlayerBullet)bullets[i];
            if(
                pb.position.X > _graphics.PreferredBackBufferWidth+20 || 
                pb.position.Y > _graphics.PreferredBackBufferHeight+20 || 
                pb.position.X < 0 + (pb.texture.Width / 2) * pb.textureScale.X ||
                pb.position.Y < 0 + (pb.texture.Height / 2) * pb.textureScale.Y 
            )
            {
                bullets.RemoveAt(i);
            }
            pb.attack(ref gameTime);
        }
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        
        foreach(Enemy e in enemies)
        {
            e.draw(ref _spriteBatch);
            // e.collisionBehavior.draw(_spriteBatch);
        }
        _spriteBatch.DrawRectangle(new RectangleF(0, 550, 800, 60), Color.Black, 50);
        for(int f = 0; f < bullets.Count; f++)
        {
            PlayerBullet pb = (PlayerBullet)bullets[f];
            pb.draw(ref _spriteBatch);
            // pb.collisionBehavior.draw(_spriteBatch);
            for(int i = 0; i < enemies.Count; i++)
            {
                Enemy e = (Enemy)enemies[i];
                if(e.collidedWith(pb.collisionBehavior.Bounds))
                {
                    bullets.RemoveAt(f);
                    e.health -= 1;
                    if(e.health == 0)
                    {
                        enemies.RemoveAt(i);
                    }
                }
                
            }
            
        }
        mainPlayer.draw(ref _spriteBatch);
        mainPlayer.collisionBehavior.draw(_spriteBatch);




        // ### Text Draw

        _spriteBatch.DrawString(mainFont, String.Format("Bullets: {0}", mainPlayer.bulletsInMagazine), new Vector2(50, 550), Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}
