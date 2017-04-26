using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer
{
    class GameObject
    {
        
        int currentFrame = 0;
        int currentState = 0;
        
        float yVelocity = 0;
        bool jumping = false;
        bool isDead = false;

        int spriteWidth = 64;
        int spriteHeight = 64;
        Texture2D texture;
        public Vector2 position;
        
        public GameObject(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
        }

        public void Update(GameTime gameTime)
        {
            

            
            if (!isDead)
            {
                position.X += 4;

                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    Jump();
                }
            }

            
            position.Y -= yVelocity;
            yVelocity--;
        }

        public void TouchedGround(int groundY)
        {
            if (yVelocity < -30)
            {
                Kill();
            }
            else if (!isDead)
            {
                currentState = 0;
                jumping = false;
            }
            position.Y = groundY - spriteHeight;
            yVelocity = 0;
        }
        
        public void Jump()
        {
            if (!jumping)
            {
                yVelocity = 17;
                jumping = true;
                
            }
        }

        public void Kill()
        {
            isDead = true;
        }

        public Rectangle BoundingBox()
        {
            return new Rectangle(position.ToPoint(), new Point(spriteWidth, spriteHeight));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position,
                sourceRectangle: new Rectangle(
                                        currentFrame * spriteWidth,
                                        currentState * spriteHeight, 
                                        spriteWidth, 
                                        spriteHeight));
        }
    }
}
