using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossTheRoad
{
    class Collision : Game
    {
        /// <summary>
        /// CollisionUpdate kollar ifall spelaren krockar
        /// med en bil.
        /// </summary>
        /// <param name="gameTime"> Speltiden som gör det möjligt att få passerad tid. </param>
        /// <param name="player"> Objekt av spelaren. </param>
        /// <param name="listOfEnemies"> Lista av alla bilar i spelet. </param>
        public void CollisonUpdate(GameTime gameTime, Player player, List<EnemyCar> listOfEnemies)
        {
            // Så länge listav av fiender inte är noll.
            if (listOfEnemies.Count != 0)
            {
                // Loopas dessa bilar igenom.
                for (int i = 0; i < listOfEnemies.Count; i++)
                {
                    // Och ifall spelarens rektangel krockar med någon bils rektangel.
                    if (player.box.Intersects(listOfEnemies[i].box))
                    {
                        // Så nollställs spelarens positioner.
                        player.position.X = player.startPos.X;
                        player.position.Y = player.startPos.Y;

                        // Den bil som krockade med spelaren tas bort från spelet.
                        listOfEnemies.Remove(listOfEnemies[i]);

                        // Spelaren blir av med ett liv som straff.
                        player.health -= 1;
                    }
                }
               
                
            }
          
        }
    }
}
