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
            if (size <= 4)
                size = 4;
            else
                if (size < 8)
                size = 8;
            else
                if (size < 16)
                size = 16;
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
            this.idFirstNode = CreateTree(1);
        }
        public int CreateTree(int high)
        {
            NodeMatch nodeMatch;
            if (high < this.high)
            {
                nodeMatch = new NodeMatch(-1, -1, CreateTree(high + 1), CreateTree(high + 1), high);
            }
            else
            {
                nodeMatch = new NodeMatch(-1, idTeams[0], -1, -1, high);
                idTeams.RemoveAt(0);
            }
            NodeMatchDAO.Instance.CreateNodeMatch(nodeMatch);
            return NodeMatchDAO.Instance.GetNewestId();
        }
        public void CheckPriority(NodeMatch node)
        {
            if (node.idNodeLeft == -1 && node.idNodeRight == -1)
                return;
            NodeMatch nodeLeft = NodeMatchDAO.Instance.GetNodeById(node.idNodeLeft);
            NodeMatch nodeRight = NodeMatchDAO.Instance.GetNodeById(node.idNodeRight);
            CheckPriority(nodeLeft);
            CheckPriority(nodeRight);
            if (nodeLeft.idTeam == 0 && nodeRight.idTeam == 0)
            {
                node.idTeam = 0;
                NodeMatchDAO.Instance.UpdateNode(node);
            }
            if (nodeRight.idTeam == 0 && nodeLeft.idTeam != -1)
            {
                node.idTeam = nodeLeft.idTeam;
                NodeMatchDAO.Instance.UpdateNode(node);
            }
            if (nodeLeft.idTeam == 0 && nodeRight.idTeam != -1)
            {
                node.idTeam = nodeRight.idTeam;
                NodeMatchDAO.Instance.UpdateNode(node);
            }
        }
    }
}
