using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CrossTheRoad
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // Spelobjekten
        Player player;
        List<EnemyCar> listOfEnemies;

        // Allting som har med skapandet av spelvärlden att göra
        FTP ftp;
        TileHandler allTiles;
        Collision collision;
        Random rnd;

        // Kollar tid får spawntime
        TimeSpan previousTime;
        TimeSpan spawnTime;

        // Alla olika spritefonts
        Timer timer;
        HPcount hp;
        EndFont end;

        // Texturer som används av sprites
        // Texture2D texture; För debug
        Texture2D red;
        Texture2D green;
        Texture2D blue;
        Texture2D chosen;

        // Objekt av GameState enumen
        GameState _state;

        // En offset så att spelaren hamnar i bild.
        int playerOffset;

        // Kollar ifall man vunnit eller inte.
        bool? ifWin; 

        // Olika gamestates, när spelet är igång eller inte
        enum GameState
        {
            GamePlay,
            End,
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            //graphics.IsFullScreen = true;
            graphics.ApplyChanges();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

            // Objekt av FTP klassen som hanterar nedladdning av banor.
            // Tänkte ha med så att man kunde välja flera olika banor, har gjort det lätt att
            // implementera detta men har inte hunnit göra det
            ftp = new FTP();
            ftp.DownLoadFile("ftp://127.0.0.1/Levels/", "anon", "", "level.txt");

            // Offset så att spelaren hamnar bättre i bild.
            playerOffset = 50;

            // Håller en tidigare variant på total gametime.
            previousTime = TimeSpan.Zero;

            // Håller sju sekunder.
            spawnTime = TimeSpan.FromSeconds(0.7);

            // Objekt av random klassen.    
            rnd = new Random();

            // Listobjekt av EnemyCar klassen.
            listOfEnemies = new List<EnemyCar>();

            // Objekt av collision klassen.
            collision = new Collision();

            // Objekt av alltiles klassen, kräver en del parametrar
            allTiles = new TileHandler(graphics.PreferredBackBufferWidth / 30
                , graphics.PreferredBackBufferHeight / 30
                , 30
                , 30
                , ftp.lookForFile);

            // Objekt av player klassen, kräver också en del parametrar
            player = new Player(new Vector2(graphics.PreferredBackBufferWidth / 2
                , graphics.PreferredBackBufferHeight - playerOffset)
                , 30
                , 30);

            // Objekt för olika spritefont klasser.
            timer = new Timer("Time: ", 0);
            hp = new HPcount("Health: ", player.health);
            end = new EndFont("Your time:", 0);

            // Kör Build metoden i TileHandler som skapar världen.
            allTiles.BuildTiles();

            base.Initialize();

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {


            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Content skickas främst med till objektens egna laddningsmetoder.
            player.Load(this.Content);
            allTiles.LoadTiles(this.Content);

            timer.Load(this.Content);
            hp.Load(this.Content);
            end.Load(this.Content);



            // Bilarna laddas in här så att inte en ny textur laddas varje gång en bil
            // skapas.
            red = Content.Load<Texture2D>("Cars/RedCarFirstL");
            green = Content.Load<Texture2D>("Cars/GreenCarFirstL");
            blue = Content.Load<Texture2D>("Cars/BlueCarFirstL");

            //texture = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color); För debug
            //texture.SetData<Color>(new Color[] { Color.White }); För debug

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// Lägger till fiender beroende på det slumpade värdet som skickas med.
        /// 0, blir en röd bil med hastighet 5.
        /// 1, blir en grön bil med hastighet 6.
        /// 2, blir en blå bil med hastighet 7.
        /// </summary>
        /// <param name="carChooser"> Integer som bestämmer bil.</param>
        protected void AddEnemy(int carChooser)
        {
            switch (carChooser)
            {
                case 0:
                    listOfEnemies.Add(new EnemyCar(allTiles.redCarSpawnPositions[rnd.Next(allTiles.redCarSpawnPositions.Count)]
                    , graphics.PreferredBackBufferWidth
                    , 30));

                    chosen = red;
                    break;

                case 1:
                    listOfEnemies.Add(new EnemyCar(allTiles.greenCarSpawnPositions[rnd.Next(allTiles.greenCarSpawnPositions.Count)]
                        , graphics.PreferredBackBufferWidth
                        , 30));

                    chosen = green;
                    break;

                case 2:
                    listOfEnemies.Add(new EnemyCar(allTiles.blueCarSpawnPositions[rnd.Next(allTiles.blueCarSpawnPositions.Count)]
                    , graphics.PreferredBackBufferWidth
                    , 30));

                    chosen = blue;
                    break;
            }

            // Sparar vald textur till enemyobjektet som sedan används vit utritning.
            listOfEnemies[listOfEnemies.Count - 1].LoadTex(chosen);

            // Kollar ifall texturen behöver speglas
            listOfEnemies[listOfEnemies.Count - 1].carChoose(carChooser); 
        }

        /// <summary>
        /// Uppdateringsmetod för fiender
        /// </summary>
        /// <param name="gameTime"> Speltiden som gör det möjligt att få passerad tid.</param>
        protected void EnemyUpdate()
        {
            // För varje EnemyCar objekt i listan.
            foreach (EnemyCar car in listOfEnemies)
            {
                // Så körs deras rörelsemetod, samt att deras hastighets-
                // parameter skickas med.
                car.Move(car.moveSpeed);
            }

            // Alla bilar i listan itereras.
            for (int j = 0; j < listOfEnemies.Count; j++)
            {
                // Om en bil har hamnat så pass långt till höger eller vänster
                // Att den inte längre syns så tas bilen bort från listan.
                if (listOfEnemies[j].box.X > 2000 || listOfEnemies[j].box.X < -100)
                {
                    listOfEnemies.Remove(listOfEnemies[j]);
                }
            }
        }
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
                || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Beroende på vilken GameState som körs så
            // körs olika metoder.
            switch (_state)
            {
                // GamePlay staten som spelet utgår ifrån
                // Kommer köra spelet som vanligt.
                case GameState.GamePlay:
                    UpdateGame(gameTime);
                    break;
                
                // Däremot ifall End staten har triggats igång
                // Händer lite andra saker.
                case GameState.End:
                    GameIsEnded(gameTime);
                    break;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Kör spelet som vanligt.
        /// Bilar spawnas.
        /// Spelaren är spawnad och kan röra på sig.
        /// </summary>
        /// <param name="gameTime">Speltiden som gör det möjligt att få passerad tid. </param>
        protected void UpdateGame(GameTime gameTime)
        {
            // Kör spelarens rörelsemetod samt skickar med spelarens hastighets parameter.
            player.Move(player.moveSpeed);

            // Metoden som står för uppdateringen av fiender körs.
            EnemyUpdate();

            // Metoden som står för kollision körs.
            // För att den skall fungera korrekt skickas gameTime, objekt av spelaren
            // och hela listan med fiender som finns just då.
            collision.CollisonUpdate(gameTime, player, listOfEnemies);

            // Iterar alla positioner i finishTile listan.
            foreach (Vector2 finishTile in allTiles.finishLine)
            {
                // Ifall spelarens liv blir noll så förlorar man
                // State ändras till End och ifWin blir falsk.
                if (player.health <= 0)
                {
                    ifWin = false;
                    _state = GameState.End;
                }

                // Ifall spelarens position når någon position bland
                // finishTilesen så vinner man.
                // State ändras också till end men ifWin blir sann.
                else if (player.position.Y <= finishTile.Y)
                {
                    ifWin = true;
                    _state = GameState.End;
                }
            }

            // Ifall den totala speltiden subtraherat med den gamla sparade totala speltiden
            // är större än den tid som är satt för spawntime så har tillräckligt tid gått 
            // och fler bilar kan spawnas samt att previous time kan uppdateras.
            if (gameTime.TotalGameTime - previousTime > spawnTime)
            {
                for (int i = 0; i < 10; i++)
                {
                    previousTime = gameTime.TotalGameTime;
                    AddEnemy(rnd.Next(0, 3));
                }

            }
        }

        /// <summary>
        /// Metoden som körs så fort spelet är klart.
        /// </summary>
        /// <param name="gameTime">Speltiden som gör det möjligt att få passerad tid</param>
        protected void GameIsEnded(GameTime gameTime)
        {
            // Ingenting händer förens spelaren gör ett val.
            // Spelaren får en fråga ifall hen vill spela igen.
            // Ifall spelaren då klickar k kommer..
            if (Keyboard.GetState().IsKeyDown(Keys.K))
            {
                // ifWin att nullas, alltså kan man antingen vinna eller förlora igen.
                ifWin = null;

                // Spelarens position återställs.
                player.position = player.startPos;

                // Spelarens liv blir återigen tre.
                player.health = 3;

                // Passerad tid återställs till noll.
                timer.time = 0;
                
                // GameState ändras tillbaka till GamePlay.
                _state = GameState.GamePlay;

                // Rensar fiende listan
                listOfEnemies.Clear();
            }
        }

  

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            // Beroende på vilken state som är aktiv händer lite olika saker.
            switch (_state)
            {
                // Är GamePlay aktiv kommer världen att ritas ut
                // spelaren, tiden, liv och alla bilar. Spelet är igång
                // med andra ord.
                case GameState.GamePlay:
                    allTiles.Draw(spriteBatch, gameTime);
                    player.Draw(spriteBatch, gameTime);

                    timer.Draw(spriteBatch, gameTime);
                    hp.Draw(spriteBatch, gameTime, player.health, new Vector2(1710, 0));
                    //player.DrawRectangle(spriteBatch, GraphicsDevice, texture); För debug
                    foreach (EnemyCar car in listOfEnemies)
                    {
                        car.Draw(spriteBatch, gameTime);
                        //car.DrawRectangle(spriteBatch, GraphicsDevice, texture); För debug
                    }
                    break;

                // Däremot ifall End är aktiv kommer endast världen att ritas ut tillsammans
                // spriteFont som antingen säger Game Over eller ger dig den tid du vann med.
                case GameState.End:
                    allTiles.Draw(spriteBatch, gameTime);
                    end.Draw(spriteBatch, new Vector2(graphics.PreferredBackBufferWidth / 2,
                        graphics.PreferredBackBufferHeight / 2), (int)timer.time, ifWin);
                    break;

            }

            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
