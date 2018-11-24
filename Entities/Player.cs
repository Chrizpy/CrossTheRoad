using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossTheRoad
{
    class Player : Entity
    {
        // Hälsa för spelaren, datatyp int eftersom heltal räcker bra här.
        public int health;

        // Hur mycket spelaren accelerar.
        float acc;

        // Tangentbordets skick, kan se vilka knappar som trycks ned.
        KeyboardState state;

        // Kommer att spara information som talar om ifall knappar klickas eller inte.
        bool btn;

        // Konstruktorn har med ContentManager som gör det möjligt att ladda content från denna klass.
        public Player(Vector2 startPos, int playerWidth, int playerHeight) 
            :base(startPos) // Här ärvs startPos och moveSpeed från Entity.
        {
            // Så fort ett player
            moveSpeed = 1;
            health = 3;
            acc = 0.2F;
            box = new Rectangle((int)startPos.X, (int)startPos.Y, playerWidth, playerHeight); // Definierar spelarens "låda"
                  
        }

        /// <summary>
        /// Här kommer allt som har med spelarens rörlighet att finnas. 
        /// OBS att denna metod körs i updatemetoden i Game vilket gör att den kommer
        /// att uppdateras.
        /// </summary>
        public override void Move(float moveSpeed)
        {
            // Rektangeln som omfamnar spelaren
            box = new Rectangle((int)position.X - box.Height / 2, (int)position.Y - box.Height / 2, box.Width, box.Height);

            // Sparar tangentbordets skick i denna variabel.
            state = Keyboard.GetState(); 

            // Här kollas ifall någon utav WASD eller ↑←↓→ knapparna trycks ner.
            if (state.IsKeyDown(Keys.W) || state.IsKeyDown(Keys.Up) || state.IsKeyDown(Keys.S) || state.IsKeyDown(Keys.Down)
                || state.IsKeyDown(Keys.A) || state.IsKeyDown(Keys.Left) || state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.Right))
            {
                // Kollar ifall spelaren trycker ner någon utav styr tangenterna.
                btn = true;

                // Ifall så är fallet kommer acceleration hända.
                this.moveSpeed += acc; 

                // Beroende på vilken tangent som klickas så ändrar spelaren position enligt följande:
                if (state.IsKeyDown(Keys.W) || state.IsKeyDown(Keys.Up))
                {
                    // Rör spelaren uppåt.
                    position.Y -= moveSpeed;
                }
                else if (state.IsKeyDown(Keys.S) || state.IsKeyDown(Keys.Down))
                {
                    // Rör spelaren nedåt.
                    position.Y += moveSpeed;
                }
                else if (state.IsKeyDown(Keys.A) || state.IsKeyDown(Keys.Left))
                {
                    // Rör spelaren åt vänster.
                    position.X -= moveSpeed;
                }
                else if (state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.Right))
                {
                    // Rör spelaren åt höger.
                    position.X += moveSpeed;
                }


            }

            // Hastighetstaket ligger på 4, över 4 så springer spelaren konstant i 4.
            if (this.moveSpeed > 4)
                {      
                this.moveSpeed = 4;
                }

            // Om btn blir falsk måste en knapp släppts, moveSpeed behöver då startas om.
            else if (btn == false)
                    {
                
                this.moveSpeed = 1;
                    }
          else
          {
                // Så fort någon utav knapparna släpps så blir btn falsk.
                btn = false;
          }

        }

        /// <summary>
        /// Internmetod som laddar texturen för spelaren.
        /// </summary>
        /// <param name="content"> Tillåter texturtilldelning. </param>
        public override void Load(ContentManager content)
        {
            enTex = content.Load<Texture2D>("Assets/SpriteForwardPosted30x30");
        }
        /// <summary>
        /// Metoden har hand om allt som har med utritandet av spelkaraktären att göra.
        /// Körs sedan i spelets "Main" draw metod.
        /// </summary>
        /// <param name="spriteBatch"> Hanterar ritandet av texturer eller sprites.</param>
        /// <param name="gameTime"> Hanterar spelets uppdatering. </param>
        public  void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(enTex, position, null, null, new Vector2(enTex.Width / 2, enTex.Height / 2), 0f, new Vector2(1,1) ,  Color.White, SpriteEffects.None, 0);
        }


    }
}
