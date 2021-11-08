using FCM.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace FCM.DAO
{
    class GoalDAO
    {
        private static GoalDAO instance;

        public static GoalDAO Instance
        {
            get { if (instance == null) instance = new GoalDAO(); return instance; }
            set => instance = value;
        }
        public List<Goal> GetListGoals(int idMatchs, int idTeams)
        {
            List<Goal> goals = new List<Goal>();

            string query = "Select* " +
                          "From Goals  " +
                          " Where IdMatchs = " + idMatchs + " AND " + " IdTeams = " + idTeams;
            DataTable tb = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow row in tb.Rows)
            {
                Goal goal = new Goal(row);
                goals.Add(goal);
            }
            return goals;
        }
        public void UpdateGoal(Goal goal)
        {
            string query = "Update Goals" +
                "Set IdPlayerGoals = " + goal.idPlayerGoals + " , " +
                " IdPlayerAssist = " + goal.idPlayerAssist + " , " +
                " IdTypeOfGoals = " + goal.idTypeOfGoals + " , " +
                " Time = " + " N'" + goal.time + "' " +
                " Where IdMatchs = " + goal.idMatchs + " AND " + " IdTeams = " + goal.idTeams;
            DataProvider.Instance.ExecuteQuery(query);
        }

        public void AddGoal(Goal g)
        {
            string query = "insert into Goals(IdMatchs, IdPlayerGoals, IdPlayerAssist, IdTeams, IdTypeOfGoals, Time) values (" +
                g.idMatchs + " , "
                + g.idPlayerGoals + " , "
                + g.idPlayerAssist + " , "
                + g.idTeams + " , "
                + g.idTypeOfGoals + " , "
                + g.time
                + ") ";
            DataProvider.Instance.ExecuteQuery(query);
        }

        public void DeleteGoalByMatchID(int idMatch)
        {
            string query = "DELETE FROM Goals " +
                " WHERE IdMatchs = " + idMatch;
            DataProvider.Instance.ExecuteQuery(query);
        }
       
    }
}
