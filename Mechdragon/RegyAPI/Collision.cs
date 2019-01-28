using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Mechdragon
{
    class Collision
    {
        Rectangle A = new Rectangle();
        Rectangle B;
        bool colliding;
        int width = 50;
        int height = 50;
        GameObject gameObject;
        public Collision(GameObject _A)
        {
            if (_A.Sprite != null)
            {
                width = _A.Sprite.Texture.Width;
                height = _A.Sprite.Texture.Height;
                A = new Rectangle((int)_A.X, (int)_A.Y, width, height);
                gameObject = _A;
            }
            else
            {
                A = new Rectangle((int)_A.X, (int)_A.Y, width, height);
            }
        }

        public Collision(GameObject _A, int _width, int _height)
        {
            width = _width;
            height = _height;
            A = new Rectangle((int)_A.X, (int)_A.Y, width, height);
        }

        public int BoundsWidth
        {
            set { width = value; }
            get { return width; }
        }

        public int BoundsHeigth
        {
            set { height = value; }
            get { return height; }
        }

        public Rectangle Bounds
        {
            set { A = value; }
            get { return A; }
        }
        public bool Colliding(GameObject _B)
        {
            B = _B.Collision.Bounds;

            if (A.Intersects(B))
            {
                colliding = true;
            }
            else
            {
                colliding = false;
            }

            return colliding;
        }

        public void Update(GameTime gameTime)
        {
            A = new Rectangle((int)gameObject.X, (int)gameObject.Y, width,height);
        }
    }
}
