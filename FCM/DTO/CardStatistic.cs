using System.Data;
using System.Windows.Shapes;
using FCM.DAO;
using FCM.DTO;
using System.Windows.Media;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Drawing;

namespace FCM.DAO
{
    class CardStatistic
    {
        public int round { get; set; }
        public int rc { get; set; }
        public int yc { get; set; }
        public int sumc { get; set; }
        public CardStatistic(int Round)
        {
            this.round = Round;
            this.rc = 0;
            this.yc = 0;
            this.sumc = 0;
        }
    }
}
