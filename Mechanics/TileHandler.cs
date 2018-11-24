using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossTheRoad
{
    /// <summary>
    /// TileHandler har hand om allt som har med att generera världen med att göra.
    /// Ser till så att skärmen fylls med rutor.
    /// </summary>
    class TileHandler
    {
        Tile[,] tileArray; // Array av tiles som uppgör en samling tiles. OBS multidimensionell array.

        int tileDim0; // Hur stor arrayen skall vara i X-led.
        int tileDim1; // Hur stor arrayen skall vara Y-led.
        int tileWidth; // Hur stor varje tile skall vara i bredd.
        int tileHeight; // Hur lång varje tile skalla vara.
        public List<Vector2> redCarSpawnPositions;
        public List<Vector2> greenCarSpawnPositions;
        public List<Vector2> blueCarSpawnPositions;
        public List<Vector2> finishLine;
        string[] tileChooser; // Sparar siffror i x-led från textfiler som level.
        string filePath = Path.GetFullPath("Content/"); // Filväg till banan.

        /// <summary>
        /// När en TileHandler skapas kommer den behöva arrayens dimensioner och
        /// hur stor i bredd och höjd varje tile behöver vara.
        /// </summary>
        public TileHandler(int tileDim0, int tileDim1, int tileWidth, int tileHeight, string level)
        {
            // Här antar de lokala variablerna värden som fås av konstrukorn.
            this.tileDim0 = tileDim0;
            this.tileDim1 = tileDim1;
            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;

            filePath = Path.Combine(filePath, level);
            tileArray = new Tile[tileDim0, tileDim1]; // Själva arrayen skapas med massor av tomrum.
            

        }

        /// <summary>
        /// BuilTiles initierar och bygger upp alla tilesen.
        /// Metoden körs sedan i Games egna initialize metod.
        /// </summary>
        public void BuildTiles()
        {
            redCarSpawnPositions = new List<Vector2>();
            greenCarSpawnPositions = new List<Vector2>();
            blueCarSpawnPositions = new List<Vector2>();
            finishLine = new List<Vector2>();
            // För tillfället läser bara level, ska ändra så att man kan bestämma i ett menysystem
            StreamReader fileReader = new StreamReader(filePath);  
            
            // Denna loop är en "nested loop" eller loop i en loop. Det betyder att den övre kör ett varv, sen kör den inre alla sina varv
            // tills denna nested loop är över.
            for (int i = 0; i < tileArray.GetLength(1); i++) // Loopar antalet gånger som står i 2d arrayens y dimension "[x,y]"
            {
                tileChooser = fileReader.ReadLine().Split(' ');
                // Vid varje space så pausar den och lägger en string i ett array-index.
                // Eftersom tileChooser är en string array behöver varje index parsas till int.
               
                for (int j = 0; j < tileArray.GetLength(0); j++)
                // En loop som körs så många gånger det finns x-index i 2d arrayern "[x,y]"
                {
                    // Här ser vi att alla positioner gås igenom i x-led på string arrayen som innehåller siffor.
                    // Dessa behövs för att programmet ska förstå vilken tiletextur som behövs.
                    tileArray[j, i] = new Tile (int.Parse(tileChooser[j]), new Vector2(tileWidth * j, tileHeight * i));

                    switch ((tileChooser[j]))
                    {
                        case "5":
                        case "6":
                            redCarSpawnPositions.Add(new Vector2(tileWidth * j, tileHeight * i));
                            break;

                        case "7":
                        case "8":
                            greenCarSpawnPositions.Add(new Vector2(tileWidth * j, tileHeight * i));
                            break;

                        case "9":
                            blueCarSpawnPositions.Add(new Vector2(tileWidth * j, tileHeight * i));
                            break;

                        case "10":
                            finishLine.Add(new Vector2(tileWidth * j, tileHeight * i));
                            break;

                    }
                   
                }
            }

            
        }

        /// <summary>
        /// LoadTiles laddar alla tiles i tilearrayen med textur beroende på vilken tile typ den har.
        /// </summary>
        /// <param name="content"> En ContentManager behöver hänga med som hanterar texturer.</param>
        public void LoadTiles(ContentManager content)
        {
            
            // Går igenom alla tileobjekt i tileArray, laddar en texture beroende på vilken tile.type int den har.
            foreach (Tile tile in tileArray)
            {
                switch (tile.type)
                {
                    case 1:
                        tile.tex = content.Load<Texture2D>("Assets/Grass30x30");
                        break;

                    case 0:
                        tile.tex = content.Load<Texture2D>("Road/RoadTop30x30");
                        break;

                    // Varför det finns så många olika gråa tiles är för att dessa håller positioner
                    // för vart bilar skall spawna, de ska alltså inte märkas att det är olika tiles.
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                    case 3:
                        tile.tex = content.Load<Texture2D>("Road/RoadFill30x30");
                        break;

                    case 2:
                        tile.tex = content.Load<Texture2D>("Road/RoadBottom30x30");
                        break;

                    case 4:
                        tile.tex = content.Load<Texture2D>("Road/RoadFillWithLine30x30");
                        break;

                    case 10:
                        tile.tex = content.Load<Texture2D>("Road/FinishLine30x30");
                        break;

                    default: // Ifall inte något annat är bestämt så får tilen en vanlig grå ruta.
                        tile.tex = content.Load<Texture2D>("Road/RoadFill30x30");
                        break;
                }
            }
        }

        /// <summary>
        /// Ritar ut alla texturer sen när den kallas i game.
        /// </summary>
        /// <param name="spriteBatch"> Hanterar ritandet av sprites.</param>
        /// <param name="gameTime"> Hanterar speltid, ser till så att uppdatering sker.</param>
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (Tile tile in tileArray)
            {
                spriteBatch.Draw(tile.tex, tile.pos, Color.White);
            }
        }
    }
}
