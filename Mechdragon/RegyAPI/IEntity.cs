using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Mechdragon
{
    interface IEntity
    {
        void destroy();
        GameObject GameObject { get; set; }
        void Update(GameTime gameTime, Game1 game);
        void Draw(SpriteBatch spriteBatch);
    }
}
