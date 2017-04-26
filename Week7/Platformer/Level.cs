using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer
{
    class Level
    {
        public List<Rectangle> blocks = new List<Rectangle>();
        Texture2D blockTexture;

        public List<Rectangle> spikes = new List<Rectangle>();
        Texture2D spikeTexture;


        
        public Level(Texture2D blockTexture, Texture2D spikeTexture, string levelFilePath)
        {
            this.blockTexture = blockTexture;
            this.spikeTexture = spikeTexture;

            StreamReader streamReader = new StreamReader(levelFilePath);
            string currentLine;
            int currentY = 0;
            while ((currentLine = streamReader.ReadLine()) != null)
            {
                int currentX = 0;
                foreach (char c in currentLine)
                {
                    if (c == '0')
                    {

                    }
                    else if (c == '1')
                    {
                        blocks.Add(new Rectangle(currentX, currentY, 32, 32));
                    }
                    else
                    {
                        spikes.Add(new Rectangle(currentX, currentY, 32, 32));
                    }
                    currentX += 32;
                }
                currentX = 0;
                currentY += 32;
            }
        }

        public void Update(GameTime gameTime)
        {
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(Rectangle block in blocks)
            {
                spriteBatch.Draw(blockTexture, block, Color.Black);
            }
            foreach(Rectangle spike in spikes)
            {
                spriteBatch.Draw(spikeTexture, spike, Color.Gray);
            }
        }
    }
}
