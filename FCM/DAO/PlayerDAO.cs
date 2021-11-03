using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Media.Imaging;
using FCM.DAO;
using FCM.DTO;

namespace FCM.DAO
{
    class PlayerDAO
    {
        private static SettingDAO instance;

        public static SettingDAO Instance
        {
            get { if (instance == null) instance = new SettingDAO(); return instance; }
            set => instance = value;
        }
        public Player GetPlayerById(int id)
        {
            string query = "Select* " +
                         "From Players " +
                         "Where id= " + id;
            DataTable tb = DataProvider.Instance.ExecuteQuery(query);
            Player player = new Player(tb.Rows[0]);
            return player;
        }
        public List<Player> GetListPlayer(int idTeams)
        {
            List<Player> players = new List<Player>();

            string query = "Select* " +
                            "From Players" +
                            "Where idTeams = " + idTeams;
            DataTable tb = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in tb.Rows)
            {
                Player player = new Player(row);
                players.Add(player);
            }
            return players;
        }
        public void DeletePlayer(int id)
        {
            string query = "Delete " +
                            "From Players " +
                            "Where id = " + id;
            DataProvider.Instance.ExecuteQuery(query);
        }
        public void CreatePlayers(Player player)
        {
            string query = "Insert into Players (IdTeams,DisplayName,UniformNumber,Birthday,Position,Nationality,Note,Imagee) " +
                         "Values (  " +
                         "" + player.idTeam + " ," +
                         "'" + player.namePlayer + "' ," +
                         "" + player.uniformNumber + " ," +
                         "'" + player.birthDay + "' ," +
                         "'" + player.position + "' ," +
                         "'" + player.nationality + "' ," +
                         "'" + player.note + "' ," +
                         ")";
            DataProvider.Instance.ExecuteQuery(query);
            query = "UPDATE players SET imagee = @img WHERE ID = (SELECT MAX(Id) FROM Players)";
            DataProvider.Instance.ExecuteQuery(query, new object[] { player.image });
        }
        public void UpdatePlayer(Player player)
        {
            string query = "Update Players " +
                            "Set " +
                            " idteams = " + "" + player.idTeam + " ," +
                            " Displayname = " + "'" + player.namePlayer + "' ," +
                            " uniformnumber = " + "" + player.uniformNumber + " ," +
                            " birthDay = " + "'" + player.birthDay + "' ," +
                            " position = " + "'" + player.position + "' ," +
                            " nationality = " + "'" + player.note + "' ," +
                            " Where id = " + player.id;
            DataProvider.Instance.ExecuteQuery(query);
            query = "UPDATE players SET imagee = @img WHERE ID = " + player.id;
            DataProvider.Instance.ExecuteQuery(query, new object[] { player.image });
        }
    }
}
