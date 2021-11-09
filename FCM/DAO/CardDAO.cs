using FCM.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace FCM.DAO
{
    class CardDAO
    {
        private static CardDAO instance;

        public static CardDAO Instance
        {
            get { if (instance == null) instance = new CardDAO(); return instance; }
            set => instance = value;
        }
        public List<Card> GetListCards(int idMatchs, int idTeams)
        {
            List<Card> cards = new List<Card>();

            string query = "Select* " +
                          "From Cards  " +
                          " Where IdMatchs = " + idMatchs + " AND " + " IdTeams = " + idTeams;
            DataTable tb = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow row in tb.Rows)
            {
                Card card = new Card(row);
                cards.Add(card);
            }
            return cards;
        }
        public List<string> getListCardOfPlayer(int idMatch, Player p)
        {
            string query = "Select TypeOfCard " +
                " FROM CARDS " +
                " WHERE IdMatchs = " + idMatch + " AND " +
                " IdPlayers = " + p.id + " AND " +
                " IdTeams = " + p.idTeam;

            DataTable tb = DataProvider.Instance.ExecuteQuery(query);

            List<string> cardsList = new List<string>();

            foreach (DataRow row in tb.Rows)
            {
                cardsList.Add(row["TypeOfCard"].ToString());
            }

            return cardsList;
        }
        public void UpdateCard(Card card)
        {
            string query = "Update Cards" +
                "Set IdPlayers = " + card.idPlayer + " , " +
                " Time = " + "N'" + card.time + "' , " +
                " TypeOfCard = " + " N'" + card.typeOfCard + "' , " +
                " Where IdMatchs = " + card.idMatchs + " AND " + " IdTeams = " + card.idTeams;
            DataProvider.Instance.ExecuteQuery(query);
        }

        public void AddCard(Card c)
        {
            string query = "insert into Cards(IdMatchs, IdPlayers, IdTeams, Time, TypeOfCard) values (" +
                c.idMatchs + " , "
                + c.idPlayer + " , "
                + c.idTeams + " , "
                + c.time + " , "
                + "N'" + c.typeOfCard + "'"
                + ") ";
            DataProvider.Instance.ExecuteQuery(query);
        }

        public void DeleteCardByMatchID(int idMatch)
        {
            string query = "DELETE FROM Cards " +
                " WHERE IdMatchs = " + idMatch;
            DataProvider.Instance.ExecuteQuery(query);
        }
    }
}
