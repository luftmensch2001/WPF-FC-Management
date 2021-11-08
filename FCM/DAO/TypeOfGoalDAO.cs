using FCM.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace FCM.DAO
{
    class TypeOfGoalDAO
    {
        private static TypeOfGoalDAO instance;

        public static TypeOfGoalDAO Instance
        {
            get { if (instance == null) instance = new TypeOfGoalDAO(); return instance; }
            set => instance = value;
        }
        public List<TypeOfGoal> GetListTypeOfGoal()
        {
            List<TypeOfGoal> listTypeOfGoals = new List<TypeOfGoal>();

            string query = "Select* " +
                          "From TypeOfGoals";
            DataTable tb = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow row in tb.Rows)
            {
                TypeOfGoal typeOfGoal = new TypeOfGoal(row);
                listTypeOfGoals.Add(typeOfGoal);
            }
            return listTypeOfGoals;
        }
        public string GetTypeOfGoalNameByID(int id)
        {
            string query = "Select* " +
                          "From TypeOfGoals " +
                          "Where id = '" + id + "'";
            DataTable tb = DataProvider.Instance.ExecuteQuery(query);

            if (tb.Rows[0] != null)
                return tb.Rows[0]["DisplayName"].ToString();
            return "";
        }
    }
}
