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

        //public void AddMatch(Match match)
        //{
        //    string query = "Insert into Matchs (IdTournaments,IdTeam01,IdTeam02,Date,Time,Round,Stadium,allowDraw ) " +
        //                 "Values (  " +
        //                 "N'" + match.idTournaments + "' ," +
        //                 "N'" + match.idTeam01 + "' ," +
        //                 "N'" + match.idTeam02 + "' ," +
        //                 "N'" + match.date.ToString("yyyy-MM-dd HH:mm:ss") + "' ," +
        //                 "N'" + match.time.ToString("yyyy-MM-dd HH:mm:ss") + "' ," +
        //                 "N'" + match.round + "' ," +
        //                 "N'" + match.statium + "' ," +
        //                 "N'" + match.allowDraw + "'" +
        //                 ")";
        //    DataProvider.Instance.ExecuteQuery(query);
        //}
        public void AddMatch(Match match)
        {
            string query = "Insert into Matchs (IdTournaments,IdTeam01,IdTeam02,Date,Time,Round,Stadium,allowDraw,score1,score2 ) " +
                         "Values (  " +
                         "N'" + match.idTournaments + "' ," +
                         "N'" + match.idTeam01 + "' ," +
                         "N'" + match.idTeam02 + "' ," +
                         "N'" + "2000-11-11" + "' ," +
                         "N'" + "00:00:00" + "' ," +
                         "N'" + match.round + "' ," +
                         "N'" + match.statium + "' ," +
                         "N'" + match.allowDraw + "'," +
                         " -1 ," +
                         " -1 " +
                         ")";
            DataProvider.Instance.ExecuteQuery(query);
        }
        public void UpdateMatch(Match match)
        {
            string query = "Update Matchs " +
                            "Set Date = '" + match.date.ToString("yyyy-MM-dd HH:mm:ss") + "' , " +
                            " Time = N'" + match.time.ToString("yyyy-MM-dd HH:mm:ss") + "' , " +
                            " Stadium = N'" + match.statium + "' ," +
                            " PenaltyTeam1 = " + match.PenaltyTeam1 + " , " +
                            " PenaltyTeam2 = " + match.PenaltyTeam2 + ", " +
                            " isStarted = '" + match.isStarted + "' , " +
                            " Score1 = " + match.Score1 + " , " +
                            " Score2 = " + match.Score2 + " " +
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
        public List<Match> GetListMatchStartedByIDTeamWithOrder(int idTournament, int idTeam01)
        {
            List<Match> matches = new List<Match>();

            string query = "Select * From (" +
                            "Select* " +
                            "From Matchs " +
                            "Where idTournaments = " + idTournament +
                            " and isStarted = 1) lm" +
                            " Where lm.IdTeam01 = " + idTeam01 +
                            " or lm.IdTeam02 = " + idTeam01 +
                            " Order by lm.Date, lm.Time";
            DataTable tb = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in tb.Rows)
            {
                Match match = new Match(row);
                matches.Add(match);
            }
            return matches;
        }
        public List<Match> GetListMatchStartedByID2Team(int idTournament, int idTeam01, int idTeam02)
        {
            List<Match> matches = new List<Match>();

            string query = "Select* " +
                            "From Matchs " +
                            "Where idTournaments = " + idTournament +
                            " and IdTeam01 = " + idTeam01 +
                            " and IdTeam02 = " + idTeam02 +
                            " Order by Date, Time";
            DataTable tb = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in tb.Rows)
            {
                Match match = new Match(row);
                matches.Add(match);
            }
            return matches;
        }
        public bool IsExist(Match match)
        {
            string query = "Select* " +
                            "From Matchs " +
                            "Where idTournaments = " + match.idTournaments +
                            " and IdTeam01 = " + match.idTeam01 +
                            " and IdTeam02 = " + match.idTeam02 +
                            " and allowDraw = '" + match.allowDraw + "'";
            DataTable tb = DataProvider.Instance.ExecuteQuery(query);
            if (tb.Rows.Count > 0)
                return true;
            return false;
        }
        public int GetNewestMatchid()
        {
            string query = "SELECT MAX(Id) as id FROM Matchs";
            DataTable db = DataProvider.Instance.ExecuteQuery(query);
            return (int)db.Rows[0]["id"];
        }
        public void DeleteMatch(int id)
        {
            LineupsDAO.Instance.DeleteLineupsByMatchID(id);
            GoalDAO.Instance.DeleteGoalByMatchID(id);
            string query = "Delete Matchs " +
                "  where id = " + id;
            DataProvider.Instance.ExecuteQuery(query);
        }
        public DateTime MaxTimeBoard(Match match)
        {
            string query = " Select max(date) as date " +
                            " From Matchs " +
                            " Where idTournaments = " + match.idTournaments + " " +
                            " And allowDraw ='true' ";

            DateTime dateTime = DateTime.Now;

            DataTable tb = DataProvider.Instance.ExecuteQuery(query);
            dateTime = (DateTime)tb.Rows[0]["date"];
            return dateTime;
        }
        public DateTime MaxTimeNockOut(Match match)
        {
            string query = " Select max(date) as date " +
                            " From Matchs " +
                            " Where idTournaments = " + match.idTournaments + " " +
                            " And round < " + match.round;

            DateTime dateTime = DateTime.Now;

            DataTable tb = DataProvider.Instance.ExecuteQuery(query);
            if (tb.Rows[0]["date"] == DBNull.Value)
            {
                return LeagueDAO.Instance.GetLeagueById(match.idTournaments).dateTime;
            }
            dateTime = (DateTime)tb.Rows[0]["date"];
            return dateTime;
        }
        public bool IsExistTimeMatch(Match match)
        {
            string query = " Select date " +
                            " From Matchs " +
                            " Where idTournaments = " + match.idTournaments;

            DataTable tb = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow dataRow in tb.Rows)
            {
                if (((DateTime)dataRow["date"]).ToString("dd/MM/yyyy") == match.date.ToString("dd/MM/yyyy"))
                    return true;
            }
            return false;
        }
        public List<Match> GetListMatchDetail(int idLeague)
        {
            List<Match> matches = new List<Match>();
            string query = " Select top(6) * " +
                           " From Matchs " +
                           " where Date<> '2000-11-11' " + " " +
                           " And score1 = -1 " +
                           " And score2 = -1 " +
                           " And idTournaments = " + idLeague +
                           " Order by[date]";
            DataTable tb = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow dataRow in tb.Rows)
            {
                Match match = new Match(dataRow);
                matches.Add(match);
            }
            return matches;
        }
        public int GetCountMatchWait(int idleague)
        {
            string query = " Select count(id) as count " + "" +
                           " From matchs  " +
                           " Where idTournaments =   " + idleague + " " +
                           " and score1 =-1 " +
                           " and allowdraw = 1";
            DataTable tb = DataProvider.Instance.ExecuteQuery(query);
            return (int)tb.Rows[0]["count"];
        }
        public bool HaveMatch(int idLeague)
        {
            List<Match>  match  = GetListMatch(idLeague);
            if (match.Count > 0)
                return true;
            return false;
        }
    }
}
