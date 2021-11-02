﻿using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace FCM.DTO
{
    public class Account
    {
        public int id { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string displayName { get; set; }
        public int roleLevel { get; set; }

        public Account(string userName, string password, string displayname, int roleLevel)
        {
            this.userName = userName;
            this.password = password;
            this.displayName = displayname;
            this.roleLevel = roleLevel;
        }
        public Account(int id, string userName, string password, string displayname, int roleLevel)
        {
            this.id = id;
            this.userName = userName;
            this.password = password;
            this.displayName = displayName;
            this.roleLevel = roleLevel;
        }
        public Account(DataRow row)
        {
            this.id = (int)row["id"];
            this.userName = (string)row["userName"];
            this.password = (string)row["password"];
            this.displayName = (string)row["displayName"];
            this.roleLevel = (int)row["roleLevel"];
        }
    }
}
