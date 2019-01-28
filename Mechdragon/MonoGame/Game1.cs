using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;


namespace Mechdragon
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        Song titleTheme;
        Song GameTheme;
        Song endTheme;
        SpriteFont fontImpacts;
        SoundEffect destroyEffect;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;
        Texture2D enemyTexture;
        Texture2D largeAsteroid;
        Texture2D mediumAsteroid;
        Texture2D smallAsteroid;
        Texture2D asteroidChunk;
        Texture2D startScreen;
        Texture2D spaceLayer1;
        Texture2D spaceLayer2;
        Texture2D spaceLayer3;
        Texture2D endScreen;
        Texture2D health;
        Sprite healthUI;
        Texture2D UI;
        Sprite uiSprite;
        Sprite SpaceLayer1;
        Sprite SpaceLayer2;
        Sprite SpaceLayer3;
        Color fontColor;
        Ball ball;
        IList <IEntity> asteroidList;
        IList<IEntity> enemyList;
        int score;
        Ball[] body;
        int midOffSetx;
        int midOffSety;
        int dbOffSetx;
        int dbOffSety;
        int enemy01Time;
        int enemy01Limit;
        int asteroidTimeLimit;
        int asteroidTime;
        int playerHealth;
        GameState gameState;
        private MouseState oldState;
        int lives;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 640;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = 480;
            Content.RootDirectory = "Content";
        }
       
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>

        protected override void Initialize()
        {
            asteroidList = new List<IEntity>();
            enemyList = new List<IEntity>();
            score = 0;
            body = new Ball[8];
            midOffSetx = 30;
            midOffSety = -5;
            dbOffSetx = 25;
            dbOffSety = 10;
            enemy01Time = 0;
            enemy01Limit = 200;
            asteroidTimeLimit = 100;
            asteroidTime = 0;
            fontColor = ColorConverter("#00FF99");
            gameState = GameState.Start;
            this.IsMouseVisible = true;
            // TODO: Add your initialization logic here
            GameServices.AddService<GraphicsDevice>(graphics.GraphicsDevice);
            player = new Player(this.Content);
            player.gameOver = false;
            for (int i = 0; i < 8; i++)
            {
                body[i] = ball = new Ball(this.Content);
            }
            player.Initialize();
            player.asteroidList = asteroidList;
            player.enemyList = enemyList;
            base.Initialize();
            //GameServices.AddService<ContentManager>(Content);
        }

        Color ColorConverter(string hex)
        {
            int r;
            int g;
            int b;
            string color;

            color = hex[1].ToString() + hex[2].ToString();
            r = Convert.ToInt32(color, 16);
            color = hex[3].ToString() + hex[4].ToString();
            g = Convert.ToInt32(color, 16);
            color = hex[5].ToString() + hex[6].ToString();
            b = Convert.ToInt32(color, 16);
            
            return new Color(r,g,b);
        }
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            player.LoadContent();
            titleTheme = Content.Load<Song>("Sound\\mainTheme");
            GameTheme = Content.Load<Song>("Sound\\flyingBattery");
            endTheme = Content.Load<Song>("Sound\\gameOver");
            destroyEffect = Content.Load<SoundEffect>("Sound\\snddebris");
            MediaPlayer.Play(titleTheme);
            MediaPlayer.IsRepeating = true;
            fontImpacts = Content.Load<SpriteFont>("Score");
            health = Content.Load<Texture2D>("Images\\health");
            spaceLayer1 = Content.Load<Texture2D>("Images\\spaceLayer1");
            spaceLayer2 = Content.Load<Texture2D>("Images\\spaceLayer2");
            spaceLayer3 = Content.Load<Texture2D>("Images\\spaceLayer3");
            enemyTexture = Content.Load<Texture2D>("Images\\spr_Enemy01");
            largeAsteroid = Content.Load<Texture2D>("Images\\spr_BigAsteroid");
            mediumAsteroid = Content.Load<Texture2D>("Images\\spr_MediumAsteroid");
            smallAsteroid = Content.Load<Texture2D>("Images\\spr_SmallAsteriod");
            asteroidChunk = Content.Load<Texture2D>("Images\\spr_AsteroidChunk");
            startScreen = Content.Load<Texture2D>("Images\\frontScreen");
            endScreen = Content.Load<Texture2D>("Images\\endScreen");
            UI = Content.Load<Texture2D>("Images\\ui");
            uiSprite = new Sprite(UI,0,0);
            SpaceLayer1 = new Sprite(spaceLayer1, GraphicsDevice.Viewport.Width/2, 0);
            SpaceLayer3 = new Sprite(spaceLayer2, GraphicsDevice.Viewport.Width / 2, 0);
            SpaceLayer2 = new Sprite(spaceLayer3, GraphicsDevice.Viewport.Width / 2, 0);
            healthUI = new Sprite(health, 260, 2);
           
            for (int i = 0; i < 8; i++)
            {
                body[i].LoadContent();
                if (i % 2 == 0)
                {
                    body[i].GameObject.Sprite.Texture = Content.Load<Texture2D>("Images\\spr_fin");
                    body[i].GameObject.Sprite.Scale = 1.5f;
                }
            }
            player.GameObject.Sprite.Scale = 1.5f;
            // TODO: use this.Content to load your game content here
        }

        void MediaPlayer_MediaStateChanged(object sender, System.EventArgs e)
        {
            // 0.0f is silent, 1.0f is full volume
            MediaPlayer.Volume -= 0.1f;
            MediaPlayer.Play(titleTheme);
        }
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            player.UnloadContent();
            // TODO: Unload any non ContentManager content here
        }

        void paralax(Sprite sprite, int speed)
        {
            if (-GraphicsDevice.Viewport.Width > sprite.X)
            {
                sprite.X = 0;
            }
            sprite.X -= speed;
            
        }
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            MouseState newState = Mouse.GetState();
 
            if (gameState == GameState.Start)
            {
                if (newState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
                {
                    gameState = GameState.Game;
                    MediaPlayer.Stop();
                    MediaPlayer.Play(GameTheme);
                    MediaPlayer.Volume = 0.5f;
                    //this.IsMouseVisible = false;
                    player.gameOver = false;
                }

            }
            if (gameState == GameState.Game)
            {
                player.Update(gameTime, this);
                paralax(SpaceLayer1, 2);
                paralax(SpaceLayer2, 4);
                paralax(SpaceLayer3, 6);

                SpaceLayer1.Update(gameTime);
                SpaceLayer2.Update(gameTime);
                SpaceLayer3.Update(gameTime);

                if (enemy01Time < enemy01Limit)
                {
                    enemy01Time++;
                }
                else
                {
                    Enemy01 enemy1 = new Enemy01(enemyTexture, destroyEffect);
                    enemyList.Add(enemy1);
                    enemy01Time = 0;
                }

                if (asteroidTime < asteroidTimeLimit)
                {
                    asteroidTime++;
                }
                else
                {
                    Asteroid asteroid1 = new Asteroid(largeAsteroid, destroyEffect);
                    asteroidList.Add(asteroid1);
                    asteroidTime = 0;
                }


                for (int i = 0; i < 8; i++)
                {
                    if (i == 0)
                    {
                        body[i].moveTotarget(player.GameObject, midOffSetx, midOffSety, player.GameObject);
                    }
                    else
                    {
                        if (i % 2 == 0)
                        {
                            body[i].moveTotarget(body[i - 1].GameObject, midOffSetx, midOffSety, player.GameObject);
                        }
                        else
                        {
                            body[i].moveTotarget(body[i - 1].GameObject, dbOffSetx, dbOffSety, player.GameObject);
                        }
                    }
                    body[i].Update(gameTime, this);
                }

                for (int i = 0; i < enemyList.Count; i++)
                {
                    if (!enemyList[i].GameObject.Exists)
                    {
                        enemyList.RemoveAt(i);
                    }
                }

                foreach (Enemy01 e in enemyList)
                {
                    e.setList = player.FireBallList;
                    e.Update(gameTime, this);
                    score += e.Score;
                }

                for (int i = 0; i < asteroidList.Count; i++)
                {
                    if (!asteroidList[i].GameObject.Exists)
                    {
                        asteroidList[i].destroy();
                        asteroidList.RemoveAt(i);
                    }
                }

                foreach (Asteroid a in asteroidList)
                {
                    a.setFireballList = player.FireBallList;
                    a.Update(gameTime, this);
                    a.setAsteroidList = asteroidList;
                    score += a.Score;
                }
                healthUI.Update(gameTime);
                playerHealth = player.health;
                lives = player.lives;
                if(player.gameOver)
                {
                    gameState = GameState.End;
                    MediaPlayer.Stop();
                    MediaPlayer.Play(endTheme);
                    MediaPlayer.Volume = 0.5f;
                    this.IsMouseVisible = true;
                }
            }
            if (gameState == GameState.End)
            {
                if (newState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
                {
                    MediaPlayer.Stop();
                    MediaPlayer.Play(titleTheme);
                    MediaPlayer.Volume = 0.5f;
                    this.IsMouseVisible = true;
                    Initialize();
                }
            }
            oldState = newState;
            base.Update(gameTime);

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            if (gameState == GameState.Start)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(startScreen, new Rectangle(0,0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
                spriteBatch.End();
            }

            if (gameState == GameState.Game)
            {
                SpaceLayer1.Draw(spriteBatch);
                SpaceLayer2.Draw(spriteBatch);
                SpaceLayer3.Draw(spriteBatch);
                uiSprite.Draw(spriteBatch);
                healthUI.Draw(spriteBatch);
                player.Draw(spriteBatch);

                foreach (Enemy01 e in enemyList)
                {
                    e.Draw(spriteBatch);
                }

                foreach (Asteroid a in asteroidList)
                {
                    a.Draw(spriteBatch);
                }

                for (int i = 0; i < 8; i++)
                {
                    body[i].Draw(spriteBatch);
                }
                spriteBatch.Begin();
                spriteBatch.DrawString(fontImpacts, "Score: ", new Vector2(50, 25), fontColor);
                spriteBatch.DrawString(fontImpacts, ""+ lives, new Vector2(550, 45), fontColor);
                spriteBatch.DrawString(fontImpacts, "Health: ", new Vector2(300, 27), fontColor);
                spriteBatch.DrawString(fontImpacts, "" + score, new Vector2(65, 45), fontColor);
                spriteBatch.DrawString(fontImpacts, "Lives: ", new Vector2(550, 27), fontColor);
                spriteBatch.DrawString(fontImpacts, "" + playerHealth, new Vector2(313, 46), fontColor);
                spriteBatch.End();
            }
            if (gameState == GameState.End)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(endScreen, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
                spriteBatch.End();
                GameServices.RemoveService<GraphicsDevice>();
            }
            base.Draw(gameTime);
        }
    }
}
