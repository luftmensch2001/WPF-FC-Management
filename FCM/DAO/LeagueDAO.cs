﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Media.Imaging;
using FCM.DAO;
using FCM.DTO;

namespace FCM.DAO
{
    class LeagueDAO
    {
        private static LeagueDAO instance;

        public static LeagueDAO Instance
        {
            get { if (instance == null) instance = new LeagueDAO(); return instance; }
            set => instance = value;
        }
        public List<League> GetListLeagues()
        {
            List<League> leagues = new List<League>();

            string query = "Select* " +
                          "From Tournaments  ";
            DataTable tb = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow row in tb.Rows)
            {
                League league = new League(row);
                leagues.Add(league);
            }
            return leagues;
        }
        public League GetLeagueById(int id)
        {
            string query = "Select* " +
                         "From Tournaments " +
                         "Where id= "+ id;
            DataTable tb = DataProvider.Instance.ExecuteQuery(query);
            League league = new League(tb.Rows[0]);
            return league;
        }
        public int GetNewestLeagurId()
        {
            string query = "SELECT MAX(Id) as id FROM Tournaments";
            DataTable tb = DataProvider.Instance.ExecuteQuery(query);
            return (int)tb.Rows[0]["id"];
        }
        public void CreateLeague(League league)
        {
            string query = "Insert into Tournaments (Honors,DisplayName,Time,Status,countTeam ) " +
                         "Values (  " +
                         "N'" + league.nameSpender + "' ," +
                         "N'" + league.nameLeague + "' ," +
                         "N'" + league.dateTime + "' ," +
                         "N'" + league.status + "' ," +
                         " " + league.countTeam + "" +
                         ")";
            DataProvider.Instance.ExecuteQuery(query);
            query = "UPDATE Tournaments SET logo = @img WHERE ID = (SELECT MAX(Id) FROM Tournaments)";
            DataProvider.Instance.ExecuteQuery(query, new object[] { league.logo });
            SettingDAO.Instance.CreateSetting(GetNewestLeagurId(), league.countTeam);
        }
        public void UpdateLeague(League league)
        {
            string query = "Update Tournaments " +
                            "Set " +
                            " Honors = " + "N'" + league.nameSpender + "' ," +
                            " Displayname = " + "N'" + league.nameLeague + "' ," +
                            " Time = " + "'" + league.dateTime + "' ," +
                            " Status = " + "N'" + league.status + "' ," +
                            " countTeam = " + " " + league.countTeam + "" +
                            " Where id = " + league.id;
            DataProvider.Instance.ExecuteQuery(query);
            query = "UPDATE Tournaments SET logo = @img WHERE ID = " + league.id;
            DataProvider.Instance.ExecuteQuery(query, new object[] { league.logo });
            SettingDAO.Instance.UpdateSetting_NumberOfTeams(league.id, league.countTeam);
        }
        public void DeleteLeague(League league)
        {
            SettingDAO.Instance.DeleteSetting(league.id);
            TeamDAO.Instance.DeleteTeam(league.id);
            string query = "Delete " +
                            "From Tournaments " +
                            "Where id = " + league.id;
            DataProvider.Instance.ExecuteQuery(query);
        }    
    }
}
