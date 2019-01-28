using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Mechdragon
{
    class Asteroid : IEntity
    {
        int score;
        public GameObject asteroid;
        public int speed = 2;
        public int health = 2;
        public IList<IEntity> fireBallList = new List<IEntity>();
        public IList<IEntity> asteroidList = new List<IEntity>();
        public Random generator = new Random();
        public bool destroyed = false;
        public GraphicsDevice graphicDevice = GameServices.GetService<GraphicsDevice>();
        //public Texture2D texture2;
        SoundEffect destroyEffect;

        public Asteroid(Texture2D texture, SoundEffect _destroyEffect)
        {
            destroyEffect = _destroyEffect;
            Sprite asteroidSprite = new Sprite(texture);
            asteroid = new GameObject(asteroidSprite, graphicDevice.Viewport.Width - 20, generator.Next(0, graphicDevice.Viewport.Height));
            asteroid.Collision.BoundsWidth = 108;
            
        }
        public int Score
        {
            get { return score; }
            set { score = value; }
        }

        public GameObject GameObject
        {
            get { return asteroid; }
            set { asteroid = value; }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            asteroid.Draw(spriteBatch);
        }

        public void Initialize()
        {
            asteroid.Initialize();
        }

        public IList<IEntity> setFireballList
        {
            set { fireBallList = value; }
        }

        public void destroy()
        {
            score += 25;
            destroyed = true;
            destroyEffect.Play();
            asteroid.Remove();
        }

        public IList<IEntity> setAsteroidList
        {
            set { asteroidList = value; }
        }
        public void Update(GameTime gameTime, Game1 game)
        {
            foreach (FireBall f in fireBallList)
            {

                if (asteroid.Colliding(f.GameObject))
                {
                    asteroid.Sprite.SpriteColour = Color.Red;
                    f.GameObject.Remove();
                    health--;
                }
                else
                {
                    asteroid.Sprite.SpriteColour = Color.White;
                }
            }

            for (int i = 0; i < fireBallList.Count; i++)
            {
                if (!fireBallList[i].GameObject.Exists)
                {
                    fireBallList.RemoveAt(i);
                }
            }

            if (health <= 0)
            {
                destroy();
            }

            asteroid.X -= speed;
            if (asteroid.Exists)
            {
                asteroid.Update(gameTime);
            }
        }
    }
  
}

