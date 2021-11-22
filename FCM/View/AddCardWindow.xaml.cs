using FCM.DAO;
using FCM.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FCM.View
{
    /// <summary>
    /// Interaction logic for AddCardWindow.xaml
    /// </summary>
    public partial class AddCardWindow : Window
    {
        bool isEdit = false; // true: đang thực hiện chức năng SỬA thẻ phạt, false: đang thực hiện chức năng THÊM thẻ phạt

        public ResultRecordingWindow resultWD;
        public bool isTeam1;
        public List<Player> players;
      
        public AddCardWindow(ResultRecordingWindow resultWD, bool isTeam1, Team team, bool isEdit = false)
        {
            InitializeComponent();

            this.resultWD = resultWD;
            this.isTeam1 = isTeam1;

            this.isEdit = isEdit;
            if (this.isEdit == true)
            {
                this.tblTitle.Text = "SỬA THẺ PHẠT";
                this.Title = "Sửa thẻ phạt";
                this.tblAddCard.Text = "Lưu";
            }

            this.tblName.Text = team.nameTeam;
            this.imgLogo.Source = ImageProcessing.Instance.Convert(ImageProcessing.Instance.ByteToImg(team.logo));

            players = PlayerDAO.Instance.GetListPlayer(team.id);

            this.cbPlayer.Items.Clear();

            for (int i = 0; i < players.Count; i++)
            {
                if (this.resultWD.GetTimePlayerReceiveRedCard(players[i]) != int.MaxValue)
                {
                    continue;
                }
                this.cbPlayer.Items.Add(players[i].namePlayer.ToString());
            }
        }

        public Player GetPlayerBySelectedIndex(int selectedIndex)
        {
            int j = 0;

            Player player = new Player();

            for (int i = 0; i < players.Count; i++)
            {
                if (this.resultWD.GetTimePlayerReceiveRedCard(players[i]) != int.MaxValue)
                {
                    continue;
                }

                if (j == selectedIndex)
                {
                    player = players[i];
                    break;
                }

                j++;
            }

            return player;
        }
    }
}
