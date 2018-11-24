using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossTheRoad
{
    public class Tile
    {
        // Texturen som laddas in i tilen.
        public Texture2D tex;

        // Själva lådan eller objektet.
        protected Rectangle box;
        // Position på tile. 
        public Vector2 pos;

        // Värdet som tileType får bestämmer vilken textur som erhålls¨.
        // Private set så att typen av tile inte kan ändras efter den skapats.
        public int type { get; private set; } 
        

        /// <summary>
        /// Konstrukor för tiles. 
        /// </summary>
        /// <param name="type"> Håller ett värde som kommer bestämma textur på tile.</param>
        /// <param name="pos"> Position som tilen kommer ha i spelrutan.</param>
        public Tile(int type, Vector2 pos) // Här ser vi att tileType kommer endast anges när objektet skapas.
        {
            // Ser till att de lokala variablerna får värden från konstrukorn.
            this.type = type;
            this.pos = pos;
            box.X = (int)pos.X;
            box.Y = (int)pos.Y;
            
        }
    }
}
