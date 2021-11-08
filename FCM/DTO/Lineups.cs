using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace FCM.DTO
{
    public class Lineups
    {
        public int idMatch, idPlayer, idTeam, isOfficial;

        public Lineups(int idMatch, int idPlayer, int idTeam, int isOfficial)
        {
            this.idMatch = idMatch;
            this.idPlayer = idPlayer;
            this.idTeam = idTeam;
            this.isOfficial = isOfficial;
        }
        public Lineups(DataRow row)
        {
            this.idMatch = (int)row["IdMatchs"];
            this.idPlayer = (int)row["IdPlayers"];
            this.idTeam = (int)row["IdTeams"];
            this.isOfficial = (int)row["isOfficial"];
        }
    }
}
