﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace FCM.DTO
{
    public class TypeOfGoal
    {
        public int id;
        public string displayName;
        public TypeOfGoal(int id, string displayName)
        {
            this.id = id;
            this.displayName = displayName;
        }
        public TypeOfGoal(DataRow row)
        {
            this.id = (int)row["Id"];
            this.displayName = (string)row["DisplayName"];
        }
    }
}
