using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossTheRoad
{
    class Timer : SpriteFonte
    {
        // Sparar tid som passerat i spelet.
        public float time;

        // Konstrukor för klassen Timer.
        public Timer (string elmnt, int elmntValue ):base(elmnt, elmntValue)
        {

        }

        /// <summary>
        /// Skriver över men använder samma villkor som metoden den skriver över i 
        /// SpriteFonte.
        /// </summary>
        /// <param name="content"> Gör det möjligt att ladda font med filen med egenskaper. </param>
        public override void Load(ContentManager content)
        {
            base.Load(content);
        }

        /// <summary>
        /// Skriver ut tiden som passerat i spelet i realtid.
        /// </summary>
        /// <param name="spriteBatch"> Gör det möjligt att rita ut. </param>
        /// <param name="gameTime"> Speltiden som gör det möjligt att få passerad tid.</param>
        public void Draw (SpriteBatch spriteBatch, GameTime gameTime)
        {
            time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            elmntValue = (int)time;
            spriteBatch.DrawString(font, elmnt + elmntValue, new Vector2(0, 0), Color.Black);
            
        }
    }
}
