using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Shapes;
using FCM.DAO;
using FCM.DTO;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FCM.DTO
{
    public class League
    {
        public int id { get; set; }
        public string nameSpender { get; set; }
        public string nameLeague { get; set; }
        public int status { get; set; }
        public DateTime dateTime { get; set; }
        public BitmapImage logo { get; set; }
        public int countTeam { get; set; } = 0;

        public League(DataRow row)
        {
            this.id = (int)row["id"];
            this.nameSpender = (string)row["honors"];
            this.nameLeague = (string)row["displayname"];
            this.status = (int)row["status"];
            this.dateTime = (DateTime)row["time"];
            this.logo = ImageProcessing.Instance.LoadImage((byte[])row["logo"]);
        }
        public League(string nameSpender, string nameLeague, int status, DateTime dateTime, BitmapImage logo, int countTeam)
        {
            this.nameLeague = nameLeague;
            this.nameSpender = nameSpender;
            this.status = status;
            this.dateTime = dateTime;
            this.logo = logo;
            this.countTeam = countTeam;
        }
    }
}
