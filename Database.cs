using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer
{
    public class Database
    {
        public static List<int> cardsPlayed = new List<int>();
        public static List<Player> Players = new List<Player>();
        public static int whoTurns = 1;
        public Database(){
            
        }
    }
}
