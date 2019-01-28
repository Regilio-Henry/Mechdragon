using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Mechdragon
{
    class Enemy01 : IEntity
    {
        
        GameObject enemy01;
        int speed = 2;
        int health = 2;
        IList<IEntity> fireBallList = new List<IEntity>();
        Random generator = new Random();
        GraphicsDevice graphicDevice = GameServices.GetService<GraphicsDevice>();
        SoundEffect destroyEffect;
        int score = 0;

        public Enemy01(Texture2D texture, SoundEffect _destroyEffect)
        {
            destroyEffect = _destroyEffect;
            Sprite enemy = new Sprite(texture);
            enemy01 = new GameObject(enemy, graphicDevice.Viewport.Width - 20, generator.Next(0, graphicDevice.Viewport.Height));
            enemy01.Collision.BoundsWidth = 108;
        }  

        public GameObject GameObject
        {
            get { return enemy01; }
            set { enemy01 = value; }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            enemy01.Draw(spriteBatch);
        }

        public int Score
        {
            get { return score; }
            set { score = value; }
        }

        public void Initialize()
        {
            
            enemy01.Initialize();
            
        }
    
        public IList<IEntity> setList
        {
            set { fireBallList = value; }
        }

        public void Update(GameTime gameTime, Game1 game)
        {
            
            foreach (FireBall f in fireBallList)
            {

                if (enemy01.Colliding(f.GameObject))
                {
                    enemy01.Sprite.SpriteColour = Color.Red;
                    f.GameObject.Remove();
                    health--;
                }
                else
                {
                    enemy01.Sprite.SpriteColour = Color.White;
                }
            }

            for (int i = 0;i < fireBallList.Count;i++)
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

            enemy01.X -= speed;
            if(enemy01.Exists)
            enemy01.Update(gameTime);
        }

        public void destroy()
        {
            score += 50;
            destroyEffect.Play();
            enemy01.Remove();
        }
    }
}
