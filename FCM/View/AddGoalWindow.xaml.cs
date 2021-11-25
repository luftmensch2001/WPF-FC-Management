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
    /// Interaction logic for AddGoalWindow.xaml
    /// </summary>
    public partial class AddGoalWindow : Window
    {

        bool isEdit = false;    // true: màn hình chức năng SỬA bàn thắng, false: màn hình chức năng THÊM bàn thắng

        public ResultRecordingWindow resultWD;
        public bool isTeam1;
        public Team team;
        public List<Player> players = new List<Player>();
        public List<TypeOfGoal> typeOfGoals = new List<TypeOfGoal>();
        public AddGoalWindow(bool isTeam1, ResultRecordingWindow resultWD, bool isEdit = false)
        {
            InitializeComponent();

            this.isTeam1 = isTeam1;
            this.resultWD = resultWD;

            this.isEdit = isEdit;
            if (this.isEdit == true)
            {
                this.tblTitle.Text = "SỬA BÀN THẮNG";
                this.Title = "Sửa bàn thắng";
                this.tblAddGoal.Text = "Lưu";
            }

            if (this.isTeam1)
            {
                this.team = TeamDAO.Instance.GetTeamById(this.resultWD.match.idTeam01);
            }
            else
            {
                this.team = TeamDAO.Instance.GetTeamById(this.resultWD.match.idTeam02);
            }

            this.tblName.Text = team.nameTeam;
            this.imgLogo.Source = ImageProcessing.Instance.Convert(ImageProcessing.Instance.ByteToImg(this.team.logo));

            this.players = PlayerDAO.Instance.GetListPlayer(this.team.id);

            this.cbScored.Items.Clear();
            this.cbAsssist.Items.Clear();
            this.cbTypeOfGoal.Items.Clear();

            foreach (Player p in this.players)
            {
                this.cbScored.Items.Add(getNumberAndNameOfPlayer(p));
                this.cbAsssist.Items.Add(getNumberAndNameOfPlayer(p));
            }

            this.typeOfGoals = TypeOfGoalDAO.Instance.GetListTypeOfGoal(team.idTournamnt);

            foreach (TypeOfGoal t in this.typeOfGoals)
            {
                this.cbTypeOfGoal.Items.Add(t.displayName);
            }
        }
        string getNumberAndNameOfPlayer(Player player)
        {
            string numberUniform = player.uniformNumber.ToString();
            string name = player.namePlayer.ToString();
            return numberUniform + ". " + name;
        }

    }
}
