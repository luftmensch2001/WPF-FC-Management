using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Media.Imaging;
using FCM.DAO;
using FCM.DTO;

namespace FCM.DAO
{
    class TeamDAO
    {
        private static TeamDAO instance;

        public static TeamDAO Instance
        {
            get { if (instance == null) instance = new TeamDAO(); return instance; }
            set => instance = value;
        }
        public Team GetTeamById(int id)
        {
            string query = "Select* " +
                         "From Teams " +
                         "Where id= " + id;
            DataTable tb = DataProvider.Instance.ExecuteQuery(query);
            Team team = new Team(tb.Rows[0]);
            return team;
        }

        public int GetPlayerCountOfTeam(int id)
        {
            string query = "Select* " +
                         "From Players " +
                         "Where IdTeams= " + id;
            DataTable tb = DataProvider.Instance.ExecuteQuery(query);
            return tb.Rows.Count;
        }
        public List<Team> GetListTeam(int idTournament)
        {
            List<Team> teams = new List<Team>();

            string query = "Select* " +
                            "From Teams " +
                            "Where idTournaments = " + idTournament;
            DataTable tb = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in tb.Rows)
            {
                Team team = new Team(row);
                teams.Add(team);
            }
            return teams;
        }
        public void DeleteTeam(int idTournaments)
        {
            List<Team> teams = GetListTeam(idTournaments);
            foreach (Team team in teams)
            {
                PlayerDAO.Instance.DeletePlayer(team.id);
            }    
            string query = "Delete " +
                            "From Teams " +
                            "Where idTournaments = " + idTournaments;
            DataProvider.Instance.ExecuteQuery(query);
        }
        public void CreateTeams(Team team)
        {
            string query = "Insert into Teams (idTournaments,DisplayName,Coach,Stadium,nation) " +
                         "Values (  " +
                         "" + team.idTournament + " ," +
                         "N'" + team.nameTeam + "' ," +
                         "N'" + team.coach + "' ," +
                         "N'" + team.stadium + "' ," +
                         "N'" + team.nation + "' " +
                         ")";
            DataProvider.Instance.ExecuteQuery(query);
            query = "UPDATE Teams SET logo = @img WHERE ID = (SELECT MAX(Id) FROM Teams)";
            DataProvider.Instance.ExecuteQuery(query, new object[] { team.logo });
        }
        public void UpdateTeam(Team team)
        {
            string query = "Update Teams " +
                            "Set " +
                            " Displayname = " + "N'" + team.nameTeam + "' ," +
                            " coach = " + "N'" + team.coach + "' ," +
                            " stadium = " + "N'" + team.stadium + "' ," +
                            " nation = " + "N'" + team.nation + "' " +
                            " Where id = " + team.id;
            DataProvider.Instance.ExecuteQuery(query);
            query = "UPDATE Teams SET logo = @img WHERE ID = " + team.id;
            DataProvider.Instance.ExecuteQuery(query, new object[] { team.logo });
        }
    }
}
