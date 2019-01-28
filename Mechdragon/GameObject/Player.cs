using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;

namespace Mechdragon
{
    class Player : IEntity
    {
        private MouseState oldState;
        int score;
        int bulletTimer = 0;
        int bulletLimit = 20;
        GraphicsDevice graphicDevice = GameServices.GetService<GraphicsDevice>();
        float playerMoveSpeed = 5f;
        SoundEffect fireSoundEffect;
        Sprite dragonHead;
        GameObject dragon;
        ContentManager Content;
        Texture2D fireballTexture;
        KeyboardState currentKeyboardState = Keyboard.GetState();
        KeyboardState previousKeyboardState = Keyboard.GetState();
        IList<IEntity> fireBallList = new List<IEntity>();
        List<Sprite> spriteList = new List<Sprite>();
        List<Texture2D> idle = new List<Texture2D>();
        List<Texture2D> dragonHeadAnimation = new List<Texture2D>();
        List<List<Texture2D>> dragonAnimations = new List<List<Texture2D>>();
        public IList<IEntity> asteroidList = new List<IEntity>();
        public IList<IEntity> enemyList = new List<IEntity>();
        public int lives = 3;
        public int health = 100;
        public bool gameOver = false;

        public Player(ContentManager _content)
        {
            Content = _content;
        }
        public void Initialize()
        {
            spriteList.Add(dragonHead);
        }


        public void LoadContent()
        {
            dragonHead = new Sprite(Content.Load<Texture2D>("Images\\spr_head"), 0, 0);
            dragonHeadAnimation.Add(Content.Load<Texture2D>("Images\\spr_head"));
            dragonHeadAnimation.Add(Content.Load<Texture2D>("Images\\spr_headOpen"));
            idle.Add(Content.Load<Texture2D>("Images\\spr_head"));
            dragon = new GameObject(dragonHead, 100, 100);
            dragonAnimations.Add(idle);
            dragonAnimations.Add(dragonHeadAnimation);
            fireballTexture = Content.Load<Texture2D>("Images\\spr_fireBalls");
            fireSoundEffect = Content.Load<SoundEffect>("Sound\\sndShoot");
            
        }

       public IList<IEntity> FireBallList
        {
            get { return fireBallList; }
        }
       public GameObject GameObject
        {
            get { return dragon; }
            set { dragon = value; }
        }
        
        public int Score
        {
            get { return score; }
            set { score = value; }
        }
        public List<IEntity> setAsteroidList
        {
            set { asteroidList = value; }
        }

        public List<IEntity> setEnemyList
        {
            set { enemyList = value; }
        }

        public void UnloadContent()
        {

        }
        public void Update(GameTime gameTime, Game1 game)
        {
            //dragonHead.Height = 50;
            //dragonHead.Width = 50;
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
            MouseState newState = Mouse.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                game.Exit();

            if (dragon.X < 0)
            {
                dragon.X += playerMoveSpeed;

            }

            if (dragon.X >=graphicDevice.Viewport.Width)
            {
                dragon.X -= playerMoveSpeed;

            }

            if (dragon.Y > 415)
            {
                dragon.Y -= playerMoveSpeed;
            }

            if (70 > dragon.Y)
            {
                dragon.Y += playerMoveSpeed;
            }

            // TODO: Add your update logic here
            if (currentKeyboardState.IsKeyDown(Keys.Left))
            {
                dragon.X -= playerMoveSpeed;
            }

            if (currentKeyboardState.IsKeyDown(Keys.Right))
            {
                dragon.X += playerMoveSpeed;
            }

            if (currentKeyboardState.IsKeyDown(Keys.Up))
            {
                dragon.Y -= playerMoveSpeed;
            }

            if (currentKeyboardState.IsKeyDown(Keys.Down))
            {
                dragon.Y += playerMoveSpeed;
            }

           
            //if (newState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
            //{
                
            //    dragon.Sprite.Texture = Content.Load<Texture2D>("Images\\spr_headOpen");
            //    Shoot();
            //}
            //else
            //{
            //    for (int i = 0; i < 5000; i++) { }
            //    dragon.Sprite.Texture = Content.Load<Texture2D>("Images\\spr_head");
                  
            //}
            //if (dragon.Collision.Bounds.Intersects(new Rectangle(newState.X, newState.Y, 5000, 5000)))
            //{
            //    dragon.X = oldState.X;
            //    dragon.Y = oldState.Y;
            //    dragon.Sprite.SpriteColour = Color.Black;
            //}
            //else
            //{
            //    dragon.Sprite.SpriteColour = Color.White;
            //}

            
            if (currentKeyboardState.IsKeyDown(Keys.Space))
            {
                dragon.Sprite.Texture = Content.Load<Texture2D>("Images\\spr_headOpen");
                Shoot();
            }
            else
            {
                dragon.Sprite.Texture = Content.Load<Texture2D>("Images\\spr_head");
            }

            SetTimer();

            foreach (FireBall f in fireBallList)
            {
                f.Update(gameTime, game);
            }

            checkCollisions(enemyList);
            checkCollisions(asteroidList);
            if (health <= 0)
            {
                health = 100;
                lives--;
            }

            if (lives <= 0)
            {
                gameOver = true;
            }

            oldState = newState;
            dragon.Update(gameTime);
            
        }

        public void checkCollisions(IList<IEntity> listEntity)
        {
            foreach (IEntity e in listEntity)
            {
                
                if (dragon.Colliding(e.GameObject))
                {
                    dragon.Sprite.SpriteColour = Color.Red;
                    e.GameObject.Remove();
                    health -= 10;
                }
                else
                {
                    dragon.Sprite.SpriteColour = Color.White;
                }
            }

            for (int i = 0; i < listEntity.Count; i++)
            {
                if (!listEntity[i].GameObject.Exists)
                {
                    listEntity[i].destroy();
                    listEntity.RemoveAt(i);
                }
            }
        }
        private void SetTimer()
        {
            if (bulletTimer < bulletLimit)
            {
                bulletTimer++;
            }
            else
            {
                bulletTimer = bulletLimit;
            }
        }

        private void Shoot()
        {
            if (bulletTimer >= bulletLimit)
            {
                bulletTimer = 0;
                FireBall newFireball = new FireBall(fireballTexture);
                newFireball.GameObject.X = dragon.X;
                newFireball.GameObject.Y = dragon.Y;
                fireSoundEffect.Play();
                fireBallList.Add(newFireball);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (FireBall f in fireBallList)
            {
                f.Draw(spriteBatch);
            }

            dragon.Draw(spriteBatch);
        }

        public void destroy()
        {
            
        }
    }
}
