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
    class HPcount : SpriteFonte
    {
        // Konstruktor för HPcount.
        public HPcount(string elmnt, int elmntValue):base(elmnt, elmntValue)
        {

        }

        /// <summary>
        /// Skriver över men använder samma villkor som metoden den skriver över i 
        /// SpriteFonte.
        /// </summary>
        /// <param name="content"> Gör det möjligt att ladda font med filen med egenskaper </param>
        public override void Load(ContentManager content)
        {
            base.Load(content);
        }

        /// <summary>
        /// Skriver ut antalet liv som spelaren har
        /// </summary>
        /// <param name="spriteBatch"> Gör det möjligt att rita ut. </param>
        /// <param name="gameTime"> Speltiden som gör det möjligt att få passerad tid.</param>
        /// <param name="hp"> Heltalet som beskriver liven kvar. </param>
        /// <param name="posOfText"> Positionen på vart texten skall ritas ut.c</param>
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, int hp, Vector2 posOfText)
        {
            
            spriteBatch.DrawString(font, elmnt+ hp, posOfText, Color.Black);
        }
    }
}
