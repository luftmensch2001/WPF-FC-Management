using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using FCM.DAO;

namespace FCM.DTO
{
    class TreeMatch
    {
        public int id { get; set; }
        public int idLeague { get; set; }
        public int size { get; set; }
        public int high { get; set; }
        public int idFirstNode { get; set; }
        public List<int> idTeams { get; set; }
        public List<NodeMatch> nodeMatches { get; set; }

        public TreeMatch(DataRow row)
        {
            this.id = (int)row["id"];
            this.idLeague = (int)row["idLeague"];
            this.size = (int)row["size"];
            this.high = (int)row["high"];
            this.idFirstNode = (int)row["idFirstNode"];
           
        }
        public TreeMatch(int idLeague, int size, List<int> idTeams)
        {
            this.idLeague = idLeague;
            this.size = size;
            this.idTeams = idTeams;
            switch (size)
            {
                case 4:
                    this.high = 3;
                    break;
                case 8:
                    this.high = 4;
                    break;
                case 16:
                    this.high = 5;
                    break;
            }
            this.idFirstNode = CreateTree(high);
        }
        public int CreateTree(int high)
        {
            NodeMatch nodeMatch;
            if (high < this.high)
            {
                nodeMatch = new NodeMatch(this.id, -1, CreateTree(high + 1), CreateTree(high + 1), this.high);
            }
            else
            {
                nodeMatch = new NodeMatch(this.id, idTeams[0], -1, -1, this.high);
                idTeams.RemoveAt(0);
            }
            NodeMatchDAO.Instance.CreateNodeMatch(nodeMatch);
            return NodeMatchDAO.Instance.GetNewestId();
        }
    }
}
