using FCM.DAO;
using FCM.DTO;
using FCM.ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FCM.UserControls
{
    /// <summary>
    /// Interaction logic for ucMatchDetail.xaml
    /// </summary>
    public partial class ucMatchDetail : UserControl
    {
        public Match match { get; set; }
        public MainWindow mainWindow { get; set; }
        public MainViewModel main { get; set; }

        public ucMatchDetail()
        {
            InitializeComponent();
        }

        public ucMatchDetail(int stt, Match match, MainWindow parameter, MainViewModel main)
        {
            InitializeComponent();
            this.mainWindow = parameter;
            this.main = main;
            this.match = match;

            this.tblSTT.Text = stt.ToString();
            this.tblDate.Text = match.date.ToString("dd/MM/yyyy");
            this.tblTime.Text = match.time.ToString("hh:mm");
            this.tblTeam1.Text = TeamDAO.Instance.GetTeamById(match.idTeam01).nameTeam;
            
            this.tblTeam2.Text = TeamDAO.Instance.GetTeamById(match.idTeam02).nameTeam;
            this.tblStadium.Text = MatchDAO.Instance.getMatchByID(match.id).statium.ToString();
            this.tblRound.Text = MatchDAO.Instance.getMatchByID(match.id).round.ToString();


            // Tính tỉ số
            int score1 = 0;
            int score2 = 0;

            List<Goal> g1 = GoalDAO.Instance.GetListGoals(match.id, match.idTeam01);
            List<Goal> g2 = GoalDAO.Instance.GetListGoals(match.id, match.idTeam02);

            score1 = g1.Count;
            score2 = g2.Count;

            this.tblScore.Text = score1 + " - " + score2;

        }
    }
}
