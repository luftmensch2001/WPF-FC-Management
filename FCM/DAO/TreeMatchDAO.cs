using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using FCM.DTO;

namespace FCM.DAO
{
    class TreeMatchDAO
    {
        private static TreeMatchDAO instance;

        public static TreeMatchDAO Instance
        {
            get { if (instance == null) instance = new TreeMatchDAO(); return instance; }
            set => instance = value;
        }
        public void CreateTreeMatch(TreeMatch tree)
        {
            string query = "Insert into TreeMatch(idleague, size, high,idFirstNode) " +
                        " Values (   " + " " +
                        "  "+tree.idLeague +", " +
                        "  "+tree.size +", " +
                        "  "+tree.high +", " +
                        "  "+tree.idFirstNode +" " +
                        ")  ";
            DataTable db = DataProvider.Instance.ExecuteQuery(query);
        }
        public void DeleteTree(TreeMatch tree)
        {
            string query = "Delete treeMatch" +
                            "  Where  id = " + tree.id;
            DataTable db = DataProvider.Instance.ExecuteQuery(query);
        }
    }
}
