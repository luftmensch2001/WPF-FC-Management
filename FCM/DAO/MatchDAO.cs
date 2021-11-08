using FCM.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace FCM.DAO
{
    class MatchDAO
    {
        private static MatchDAO instance;

        public static MatchDAO Instance
        {
            get { if (instance == null) instance = new MatchDAO(); return instance; }
            set => instance = value;
        }

        public void AddMatch(Match match)
        {
            string query = "Insert into Matchs (IdTournaments,IdTeam01,IdTeam02,Date,Time,Round,Stadium ) " +
                         "Values (  " +
                         "N'" + match.idTournaments + "' ," +
                         "N'" + match.idTeam01 + "' ," +
                         "N'" + match.idTeam02 + "' ," +
                         "'" + match.date.ToString("dd/MM/yyyy") + "' ," +
                         "'"  + match.time.ToString("hh:mm") + "' ," +
                         "N'" + match.round + "' ," +
                         "N'" + match.statium + "'" +
                         ")";
            DataProvider.Instance.ExecuteQuery(query);
        }
        public void UpdateMatch(Match match)
        {
            string query = "Update Matchs " +
                            "Set Date = '" + match.date.ToString("dd/MM/yyyy") + "' , " +
                            " Time = '" + match.time.ToString("hh:mm") + "' , " +
                            " Stadium = '" + match.statium + "' " +
                            " Where ID = " + match.id;
            DataProvider.Instance.ExecuteQuery(query);
        }

        public List<Match> GetListMatch(int idTournament)
        {
            List<Match> matches = new List<Match>();

            string query = "Select* " +
                            "From Matchs " +
                            "Where idTournaments = " + idTournament;
            DataTable tb = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in tb.Rows)
            {
                Match match = new Match(row);
                matches.Add(match);
            }
            return matches;
        }
        public List<Match> GetListMatchByRound(int idTournament, int round)
        {
            List<Match> matches = new List<Match>();

            string query = "Select* " +
                            "From Matchs " +
                            "Where idTournaments = " + idTournament + " AND " +
                            "Round = " + round;
            DataTable tb = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in tb.Rows)
            {
                Match match = new Match(row);
                matches.Add(match);
            }
            return matches;
        }
        public Match getMatchByID(int idMatch)
        {
            string query = "Select* " +
                         "From Matchs " +
                         "Where id= " + idMatch;
            DataTable tb = DataProvider.Instance.ExecuteQuery(query);
            Match match = new Match(tb.Rows[0]);
            return match;
        }
    }
}
