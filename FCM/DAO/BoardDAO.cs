using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Media.Imaging;
using FCM.DAO;
using FCM.DTO;


namespace FCM.DAO
{
    class BoardDAO
    {
        private static BoardDAO instance;

        public static BoardDAO Instance
        {
            get { if (instance == null) instance = new BoardDAO(); return instance; }
            set => instance = value;
        }
        public Board GetBoardById(int id)
        {
            string query = "Select* " +
                         "From Board " +
                         "Where id= " + id;
            DataTable tb = DataProvider.Instance.ExecuteQuery(query);
            Board board = new Board(tb.Rows[0]);
            return board;
        }
        public List<Board> GetListBoard(int idTournament)
        {
            List<Board> boards = new List<Board>();

            string query = "Select* " +
                            "From Board " +
                            "Where idTournament = " + idTournament;
            DataTable tb = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in tb.Rows)
            {
                Board board = new Board(row);
                boards.Add(board);
            }
            return boards;
        }
        public void CreateBoard(Board board)
        {
            string query = "Insert into Board (idTournament,nameBoard, countTeam) " +
                         "Values (  " +
                         "" + board.idTournament + " ," +
                         "N'" + board.nameBoard + "' ," +
                         "" + board.countTeam + "" +
                         ")";
            DataProvider.Instance.ExecuteQuery(query);
        }
        public void DeleteBoardInLeague(int idTournament)
        {
            string query = "Delete " +
                            "From Board" +
                            " Where idLeague =" + idTournament;
            DataProvider.Instance.ExecuteQuery(query);
        }
        public int CountBoardFull(int idTournament)
        {
            List<Board> boards = GetListBoard(idTournament);
            int count = 0;
            for (int i = 0; i < boards.Count; i++)
            {
                int c = TeamDAO.Instance.GetCountTeam(boards[i].nameBoard);
                string query = "Select count(id) as count " +
                               "From Board " +
                               "Where idTournament =" + idTournament + " and " +
                               "countTeam = " + c;
                DataTable table =  DataProvider.Instance.ExecuteQuery(query);
                count += (int)table.Rows[0]["count"];
            }
            return count;
        }

        public void AddToKOBoard(int idTournament, int idTeam)
        {
            string query = "Insert into TeamIntoNextQualifyingRound (idTournaments, idTeams) " +
                         "Values ( " + idTournament + " , " + idTeam +  ")";
            DataProvider.Instance.ExecuteQuery(query);
        }
    }
}
