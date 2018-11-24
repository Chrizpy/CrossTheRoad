using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace CrossTheRoad
{
    class EndFont : SpriteFonte
    {
        // Konstruktor för EndFont, ärver värden från SpriteFonte
        public EndFont (string elmnt, int elmntValue):base(elmnt, elmntValue)
        {

        }

        /// <summary>
        /// Skriver över men använder samma villkor som metoden den skriver över i 
        /// SpriteFonte.
        /// </summary>
        /// <param name="content"> Gör det möjligt att ladda font med filen med egenskaper</param>
        public override void Load(ContentManager content)
        {
            base.Load(content);
        }

        /// <summary>
        /// Skriver ut själva texten i spelet
        /// när man antingen förlorat eller vunnit.
        /// </summary>
        /// <param name="spriteBatch"> Gör det möjligt att rita ut. </param>
        /// <param name="posOfText"> Positionen på text. </param>
        /// <param name="time"> Tiden då man förlorade eller vann. </param>
        /// <param name="ifWin"> Kollar ifall man vann eller förlorade </param>
        public void Draw (SpriteBatch spriteBatch, Vector2 posOfText, int time, bool? ifWin)
        {
            if (ifWin == true)
            {

                spriteBatch.DrawString(font, elmnt + time, posOfText, Color.Black);
                spriteBatch.DrawString(font, "Play again? press K", new Vector2(posOfText.X, posOfText.Y + 50), Color.Black);
            }
            else if (ifWin == false)
            {
                spriteBatch.DrawString(font, "Game Over", posOfText, Color.Red);
                spriteBatch.DrawString(font, "Play again? press K", new Vector2(posOfText.X, posOfText.Y + 50), Color.Black);
            }
        }
    }
}
