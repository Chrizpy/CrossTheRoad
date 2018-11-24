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
    class Entity
    {
        public float moveSpeed; // Rörelsehastighet på objektet.
        public Vector2 startPos; // Startposition för objektet.
        public Texture2D enTex; // Textur för objektet.
        public Rectangle box; // Spelarens position och objektstorlek
        public Vector2 position; // Positionen som rektangeln antar.

        /// <summary>
        /// Konstrukorn till klassen Entity.
        /// </summary>
        /// <param name="startPos"> Startpositionen för objekt, vart den hamnar först i rutan.</param>
        public Entity( Vector2 startPos)
        {
            // Lokala variabler antar värden från konstrukorn.
            this.startPos = startPos;
            position.X = startPos.X; 
            position.Y = startPos.Y;
        }

        /// <summary>
        /// Movement metod, här sker all logik för hur objekt kommer röra sig.
        /// </summary>
        /// <param name="moveSpeed"> Hastigheten som objektet använder.</param>
        public virtual void Move(float moveSpeed)
        {
            // Spelobjekt ärver denna metod för att använda till movement.
        }

        /// <summary>
        /// I load metoden så laddas alla texturer med en textur
        /// </summary>
        /// <param name="content"> Gör det möjligt att ladda texturer</param>
        public virtual void Load (ContentManager content)
        {

        }

        /// <summary>
        /// Uppdateringsmetoden, så att inputs eller fienders movement logik uppdateras.
        /// Annars blir spelet statiskt och inget händer.
        /// </summary>
        /// <param name="gameTime"> Har hand om spelets uppdateringar.</param>
        protected virtual void Update(GameTime gameTime)
        {
           
            Move(moveSpeed); // Här körs den lokala movemetoden.
        }

    }
}
