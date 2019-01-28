using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Mechdragon.RegyAPI
{
    class UI : Sprite
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

        public UI(Texture2D _texture) : base(_texture)
        {
            texture = _texture;
            position = new Vector2(x, y);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack);

            if (animationSet)
            {
                if (!isSequence)
                {
                    spriteBatch.Draw(spriteAnimator.getCurrentTexure(), position, spriteColour);
                }
                else
                {
                    spriteBatch.Draw(spriteAnimator.getCurrentTexure(), position, spriteAnimator.getRectangle, spriteColour);
                }
            }
            else
            {
                spriteBatch.Draw(texture, position, new Rectangle(0, 0, texture.Width, texture.Height), spriteColour, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0);
            }

            spriteBatch.End();
        }

    }
}
