using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using FCM.DTO;

namespace FCM.DAO
{
    class NodeMatchDAO
    {
        private static NodeMatchDAO instance;

        public static NodeMatchDAO Instance
        {
            get { if (instance == null) instance = new NodeMatchDAO(); return instance; }
            set => instance = value;
        }
        public int GetNewestId()
        {
            string query = "Select max(id) as id " +
                            " From NodeMatch  ";
            DataTable db = DataProvider.Instance.ExecuteQuery(query);
            return (int)db.Rows[0]["id"];
        }
        public void CreateNodeMatch(NodeMatch nodeMatch )
        {
            string query = "Insert into NodeMatch(idTree,idTeam,idNodeLeft,idNodeRight,high) " +
                           " Values ("  +
                           " "+ nodeMatch.idTree + ", "+
                           " "+ nodeMatch.idTeam + ", "+
                           " "+ nodeMatch.idNodeLeft + ", "+
                           " "+ nodeMatch.idNodeRight + ", "+
                           " "+ nodeMatch.high + ", "+
                           ")  ";
            DataTable db = DataProvider.Instance.ExecuteQuery(query);
        }
        public NodeMatch GetNodeMatch(int idTree)
        {

            string query = "Select * " +
                            " Form NodeMatch" +
                            " Where idTree = " + idTree;

            DataTable db = DataProvider.Instance.ExecuteQuery(query);

            return new NodeMatch(db.Rows[0]);
        }
        public void DeleteNodeMatch(NodeMatch node)
        {
            string query = "Delete NodeMatch" +
                          "  Where id = " + node.id;
            DataTable db = DataProvider.Instance.ExecuteQuery(query);
        }
    }
}
