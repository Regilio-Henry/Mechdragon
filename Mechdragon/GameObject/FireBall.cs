using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Mechdragon
{
    class FireBall : IEntity
    {
        Sprite fireBall;
        GameObject objfireBall;
        int speed = 5;
        Texture2D tex;
        bool isVisible = true;

        public FireBall(Texture2D texture)
        {
            tex = texture;
            fireBall = new Sprite(texture);
            objfireBall = new GameObject(fireBall);
            objfireBall.Sprite.Play(tex, 51, 27, 3, true);
            objfireBall.Collision.BoundsWidth = 50;
        }

      

        public GameObject GameObject
        {
            get { return objfireBall; }
            set { objfireBall = value; }
        }

        public void Update(GameTime gameTime, Game1 game)
        {
            objfireBall.X += speed;
            objfireBall.Update(gameTime);
        }

        public bool IsVisible
        {
            set { isVisible = value; }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            objfireBall.Draw(spriteBatch);
        }

        public void destroy()
        {
            throw new NotImplementedException();
        }
    }
}
