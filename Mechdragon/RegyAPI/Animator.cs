using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Mechdragon
{
    class Animator
    {
        int frame = 0;
        List<Texture2D> animationList;
        float delay = 200;
        float elapsedTime = 1000;
        bool lastFrame;
        Texture2D currentTexture;
        bool looping;
        bool isSequence;
        Rectangle sourceRectangle = new Rectangle(0, 0, 48, 27);
        int frameWidth;
        int frameHeight;
        int frameTotal;

        //Constructor for sequence of images
        public Animator(List<Texture2D> _animationList, Texture2D texture, bool _looping)
        {
            looping = _looping;
            animationList = _animationList;
            currentTexture = texture;
            isSequence = true;
        }

        //Constructor for spriteSheet of images
        public Animator(Texture2D spriteSheet, int _frameWidth,int _frameHeight, int _frameTotal, bool _looping)
        {
            looping = _looping;
            currentTexture = spriteSheet;
            isSequence = false;
            frameWidth = _frameWidth;
            frameHeight = _frameHeight;
            frameTotal = _frameTotal-1;
        }

        
        public void Update(GameTime gameTime)
        {
            elapsedTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            //Will go through a sequence of images and set the texture
            if (isSequence)
            {
                if (elapsedTime >= delay)
                {
                    if (frame > animationList.Count - 1)
                    {
                        lastFrame = true;
                        if (!looping)
                        {
                            return;
                        }
                        frame = 0;
                    }
                    else
                    {
                        lastFrame = false;
                    }

                    currentTexture = animationList[frame];
                    frame++;
                    elapsedTime = 0;
                }
            }
            //Goes through sprite sheet and sets the texture
            else
            {
                if (elapsedTime > delay)
                {
                    if (frame >= frameTotal)
                    {
                        frame = 0;
                    }
                    else
                    {
                        frame++;
                    }
                    elapsedTime = 0;
                }

                sourceRectangle = new Rectangle(frameWidth*frame, 0, frameWidth, frameHeight);
            }
        }

        //Gets the viewing window for the animation
        public Rectangle getRectangle
        {
            get { return sourceRectangle; }
        }

        //Gets the current texture
        public Texture2D getCurrentTexure()
        {
            return currentTexture;
        }
        
        public bool isLastFrame()
        {
            return lastFrame;
        }
    }
}
