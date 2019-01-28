using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Mechdragon
{
    class GameObject
    {
        Vector2 position;
        Color spriteColour = Color.White;
        float x = 0;
        float y = 0;
        bool colliding = false;
        bool exists = true;
        Collision collision;
        Texture2D texture = new Texture2D(GameServices.GetService<GraphicsDevice>(), 50, 50);
        Sprite sprite;
        bool stopUpdate = false;
        public GameObject(Sprite _sprite)
        {
            sprite = _sprite;
            collision = new Collision(this);
        }


        public GameObject(Sprite _sprite, float _x, float _y)
        {
            sprite = _sprite;
            x = _x;
            y = _y;
            position = new Vector2(x, y);
            collision = new Collision(this);
        }

        void createBlankTexture()
        {
            Color[] data = new Color[50 * 50];
            for (int pixel = 0; pixel < data.Length; pixel++)
            {
                //the function applies the color according to the specified pixel
                data[pixel] = Color.White;
            }

            //set the color
            texture.SetData(data);
        }
        public void Initialize()
        {
            createBlankTexture();
            sprite = new Sprite(texture);

        }
        public bool Colliding(GameObject collisionPoint)
        {
            collision = new Collision(this);
            colliding = collision.Colliding(collisionPoint);

            return colliding;
        }

        public Collision Collision
        {
            get { return collision; }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public float X
        {
            get { return x; }
            set { x = value; }
        }

        public float Y
        {
            get { return y; }
            set { y = value; }
        }

        public Sprite Sprite
        {
            get { return sprite; }
            set { sprite = value; }
        }

        public void Update(GameTime gameTime)
        {
            if (!stopUpdate)
            {
                sprite.X = x;
                sprite.Y = y;
                collision.Update(gameTime);
                sprite.Update(gameTime);
                if (!exists)
                {
                    stopUpdate = true;
                }
            }
        }

        public bool Exists
        {
            get { return exists; }
        }

        public void Remove()
        {
            this.Equals(null);
            x = 0;
            y = 0;
            exists = false;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (exists)
            {
                sprite.Draw(spriteBatch);
            }
        }
    }

}

