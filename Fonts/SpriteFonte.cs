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
    class SpriteFonte
    {
        // Sparas själva SpriteFont egenskaperna.
        protected SpriteFont font;

        // Vad spriteFonten ska skriva ut i text.
        protected string elmnt;

        // Vad spriteFonten skriver ut i heltal.
        protected int elmntValue; 

        // Konstrukor för spriteFont skapande.
        public SpriteFonte(string elmnt, int elmntValue)
        {
            this.elmnt = elmnt;
            this.elmntValue = elmntValue;
        }

        /// <summary>
        /// Laddar spriteFonten med egenskaperna från filen.
        /// </summary>
        /// <param name="content"> Gör det möjligt att ladda font. </param>
        public virtual void Load(ContentManager content)
        {
            font = content.Load<SpriteFont>("Spritefont/Default");
        }

    }
}
