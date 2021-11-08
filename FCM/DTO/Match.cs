using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace FCM.DTO
{
    public class Match
    {
        public int id { get; set; }
        public int idTournaments { get; set; }
        public int idTeam01 { get; set; }
        public int idTeam02 { get; set; }
        public int round { get; set; }
        public DateTime date { get; set; }
        public DateTime time { get; set; }
        public string statium { get; set; }
        public Match(DataRow row)
        {
            this.id = (int)row["id"];
            this.idTournaments = (int)row["IdTournaments"];
            this.idTeam01 = (int)row["IdTeam01"];
            this.idTeam02 = (int)row["IdTeam02"];
            this.round = (int)row["Round"];
            this.date = (DateTime)row["Date"];
            this.time = (DateTime)row["Time"];
            this.statium = (string)row["Stadium"];
        }
        public Match(int id, int idTournaments, int idTeam01, int idTeam02, int round,DateTime date, DateTime time, string stadium)
        {
            this.id = id;
            this.idTournaments = idTournaments;
            this.idTeam01 = idTeam01;
            this.idTeam02 = idTeam02;
            this.round = round;
            this.date = date;
            this.time = time;
            this.statium = stadium;
        }
        public Match(int idTournaments, int idTeam01, int idTeam02, int round, string stadium)
        {
            this.idTournaments = idTournaments;
            this.idTeam01 = idTeam01;
            this.idTeam02 = idTeam02;
            this.round = round;
            this.statium = stadium;
        }

    }
}
