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
    class EnemyCar : Entity
    {
        // Sparar fönstrets bredd fram till sista tile, med 30x30 tiles blir denna 1920-30 = 1890.
        protected int screenWidth; 

        // Kommer att kolla ifall texturen behöver speglas eller inte.
        bool flip;

        public EnemyCar( Vector2 startPos,int screenWidth,  int tileWidth) : base( startPos)
        {
            this.screenWidth = screenWidth - tileWidth; // Eftersom den sista tilen har en position på 1890 i full hd.
              
        }

        /// <summary>
        /// Allt som har med att ladda en fiende med att göra.
        /// </summary>
        /// <param name="content"> Den gör att content kan laddas i den här metoden.</param>
        public void LoadTex(Texture2D chosen)
        {
            enTex = chosen;
        }


        /// <summary>
        /// Bestämmer vilken hastighet den skapade bilen skall ha
        /// och ifall textur behöver speglas beroende på startposition.
        /// </summary>
        /// <param name="carChooser"> Heltal som bestämmer bil. </param>
        public void carChoose(int carChooser)
        {
            Width_Height(enTex); 

            switch (carChooser) // Bytte till case eftersom if-satserna översteg 3.
            {

                // Ifall carChooser är noll spawnas en röd bil.
                case 0: 
                    moveSpeed = 5; // Röda bilen ska ha hastigheten 5.

                    // Ifall bilens startposition är samma som screenWidth betyder det att bilen måste
                    // åka åt vänster, texturen behöver då spegelvändas.
                    if (startPos.X != this.screenWidth)
                    {
                        flip = true;
                    }

                    // Ifall bilen startposition inte är samma som screenWidth finns det bara ett utfall till,
                    // bilen måste komma från vänster och åka åt höger, behöver ej spegelvändas.
                    break;

                // Ifall carChooser är ett spawnas en röd grön.
                case 1: 
                    moveSpeed = 6; // Gröna bilen ska ha hastigheten 6.

                    if (startPos.X != this.screenWidth)
                    {
                        flip = true;
                    }
                    break;

                // Ifall carChooser är två spawnas en blå bil.
                case 2: 
                    moveSpeed = 7; // Blåa bilen ska ha hastigheten 7.

                    if (startPos.X != this.screenWidth)
                    {
                        flip = true;
                    }
                    break;

            }
        }
        /// <summary>
        /// Här antar spelobjektets rektangel samma bredd och längd
        /// som texturen, men med lite modifikationer
        /// </summary>
        /// <param name="enTex"> Tecturen till objektet.</param>
        public void Width_Height(Texture2D enTex)
        {
            box.Width = enTex.Width - 40;
            box.Height = enTex.Height - 50;
        }

        /// <summary>
        /// Rörelse för spelobjektet.
        /// Upddaterar positioner.
        /// </summary>
        /// <param name="moveSpeed"> Hastigheten på objektet</param>
        public override void Move(float moveSpeed)
        {
            box = new Rectangle((int)position.X - box.Width / 2, (int)position.Y - box.Height / 2, box.Width, box.Height);
            // Ifall spelobjektets startpositions x-värde har samma värde som bredden på spelfönstret
            // betyder det att objektet måste röra sig åt vänstrer, därför subtraheras spelobjektets position
            // med hastigheten på objektet.
            if (startPos.X == this.screenWidth)
            {
                position.X -= moveSpeed;
            }
            // När den inte är det betyder det att det motsatta måste hända. Bilen ska röra sig åt höger,
            // då adderas istället positionen med hastigheten.
            else if (startPos.X != this.screenWidth)
            {
                position.X += moveSpeed;
            }

        }

        /// <summary>
        /// Metoden har hand om allt som har med utritandet av spelkaraktären att göra.
        /// Körs sedan i spelets "Main" draw metod.
        /// </summary>
        /// <param name="spriteBatch"> Hanterar ritandet av texturer eller sprites.</param>
        /// <param name="gameTime"> Hanterar spelets uppdatering. </param>
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {

            if (flip)
            {
                spriteBatch.Draw(enTex, position, null, null, new Vector2(enTex.Width / 2, enTex.Height / 2), 0f, new Vector2(1, 1), Color.White, SpriteEffects.FlipHorizontally, 0);
            }
            else
            {
                spriteBatch.Draw(enTex, position, null, null, new Vector2(enTex.Width / 2, enTex.Height / 2), 0f, new Vector2(1, 1), Color.White, SpriteEffects.None, 0);
            }
        }

    }
}
