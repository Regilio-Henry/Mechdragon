using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Mechdragon
{
    public class Sprite
    {
        Texture2D texture;
        Vector2 position;
        Color spriteColour = Color.White;
        float x;
        float y;
        Animator spriteAnimator;
        bool animationSet = false;
        bool looping = true;
        bool isSequence = false;
        float scale = 1f;
        int spriteWidth;
        int spriteHeight;
        public Sprite(Texture2D _texture)
        {
            texture = _texture;
            position = new Vector2(x, y);
            spriteWidth = texture.Width;
            spriteHeight = texture.Height;
        }
        public Sprite(Texture2D _texture, int _x, int _y)
        {
            texture = _texture;
            x = _x;
            y = _y;
            position = new Vector2(x, y);
            spriteWidth = texture.Width;
            spriteHeight = texture.Height;
        }
        public Sprite(Texture2D _texture, float _x, float _y, Color _spriteColour, float _scale)
        {
            texture = _texture;
            x = _x;
            y = _y;
            position = new Vector2(x, y);
            spriteColour = _spriteColour;
            scale = _scale;
            spriteWidth = texture.Width;
            spriteHeight = texture.Height;
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public int Width
        {
            get { return spriteWidth; }
            set { spriteWidth = value; }
        }

        public int Height
        {
            get { return spriteHeight; }
            set { spriteHeight = value; }
        }

        public Color SpriteColour
        {
            get { return spriteColour; }
            set { spriteColour = value; }
        }

        public float X
        {
            get { return x; }
            set { x = value; }
        }

        public float Scale
        {
            get { return scale; }
            set { scale = value; }
        }
        public void PlaySequence(List<Texture2D> animationList, bool _looping)
        {
            looping = _looping;
            spriteAnimator = new Animator(animationList, texture, _looping);
            animationSet = true;
            isSequence = false;
        }

        public void Play(Texture2D spriteSheet, int frameWidth, int frameHeight, int frameTotal, bool _looping)
        {
            looping = _looping;
            spriteAnimator = new Animator(spriteSheet,frameWidth, frameHeight,frameTotal, _looping);
            animationSet = true;
            isSequence = true;
        }

        public float Y
        {
            get { return y; }
            set { y = value; }
        }

        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        public void Update(GameTime gameTime)
        {
            position = new Vector2(x, y);
            if (animationSet)
            {
                spriteAnimator.Update(gameTime);
            }

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            
            if (animationSet)
            {
                if (!isSequence)
                {
                    spriteBatch.Draw(spriteAnimator.getCurrentTexure(), position, spriteColour);
                }
                else
                {
                    spriteBatch.Draw(spriteAnimator.getCurrentTexure(),position,spriteAnimator.getRectangle, spriteColour);
                }
            }
            else
            {
                spriteBatch.Draw(texture, position, new Rectangle(0, 0, spriteWidth, spriteHeight), spriteColour, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0);
                //spriteBatch.Draw(texture,new Rectangle((int)x, (int)y, 1200, 540), new Rectangle(0, 0, sprite, texturesOriginalHeight),Color.White);
                //spriteBatch.Draw(texture, position, spriteColour);
                //spriteBatch.Draw(texture, position, new Rectangle(0,0,texture.Width, texture.Height), spriteColour, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0);
            }

            if (scale > 0)
            {
                spriteWidth = texture.Width;
                spriteHeight = texture.Height;
            }
            
            spriteBatch.End();
        }
    }
}
