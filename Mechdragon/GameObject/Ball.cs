using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Mechdragon
{
    class Ball : IEntity
    {
        Sprite ball;
        GameObject objBall;
        ContentManager Content;
        float ease = 0.55f;
        float x;
        float y;

        public Ball(ContentManager _content)
        {
            Content = _content;
        }

        public GameObject GameObject
        {
            get { return objBall; }
            set { objBall = value; }
        }

        public void Initialize()
        {
            
        }

        public void LoadContent()
        {
            ball = new Sprite(Content.Load<Texture2D>("Images\\spr_ballLit"),0,0,Color.White,0.6f);
            objBall = new GameObject(ball, 100, 100);
        }

        public void UnloadContent()
        {
            throw new NotImplementedException();
        }

        public void Update(GameTime gameTime, Game1 game)
        {
            objBall.X = x;
            objBall.Y = y;

            objBall.Update(gameTime);
        }

        //This moves the ball object towards the player
        public void moveTotarget(GameObject target,int posx,int posy, GameObject topTarget)
        {
            
            if(topTarget.X <64)
            {
                posy -=40;
            }

            if(topTarget.X<64)
            {
                posy +=40;
                posx -=30;
            }
            
            float offsetX = target.X - posx;
            float offsetY = target.Y + posy;

            var xDistance = offsetX - objBall.X;
            var yDistance = offsetY - objBall.Y;
            var distance = Math.Sqrt((xDistance * xDistance) + (yDistance * yDistance));

            if (distance>1)
            {              
                x +=xDistance* ease;
                y +=yDistance* ease;
            }
        }
    

        public void Draw(SpriteBatch spriteBatch)
        {
            ball.Draw(spriteBatch);
        }

        public void destroy()
        {

        }
    }
}
