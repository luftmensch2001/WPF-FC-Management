﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Shapes;
using FCM.DAO;
using FCM.DTO;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FCM.DTO
{
    class Team
    {
        public int id { get; set; }
        public int idTournament { get; set; }
        public string nameTeam { get; set; }
        public string stadium { get; set; }
        public string coach { get; set; }
        public string nation { get; set; }
        public byte[] logo { get; set; }

        public Team(DataRow row)
        {
            this.id = (int)row["id"];
            this.idTournament = (int)row["idTournament"];
            this.nameTeam = (string)row["Displayname"];
            this.stadium = (string)row["stadium"];
            this.nation = (string)row["nation"];
            this.coach = (string)row["coach"];
            this.logo = (byte[])row["logo"];
        }
        public Team(int idTournament, string nameTeam,string coach, string stadium, string nation, byte[] logo )
        {
            this.idTournament = idTournament;
            this.nameTeam = nameTeam;
            this.stadium = stadium;
            this.nation = nation;
            this.coach = coach;
            this.logo = logo;
        }
    }
}
