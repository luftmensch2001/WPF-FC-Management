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
using FCM.DAO;
using FCM.DTO;

namespace FCM.UserControls
{
    /// <summary>
    /// Interaction logic for ucLeagueCard.xaml
    /// </summary>
    public partial class ucLeagueCard : UserControl
    {
        public ucLeagueCard()
        {
            InitializeComponent();
        }
        public ucLeagueCard(League league)
        {
            InitializeComponent();
            imgLeagueLogo.Source = league.logo;
            tblLeagueName.Text = league.nameLeague;
            tblLeagueStatus.Text = league.nameLeague.ToString();
            tblLeagueTeamsCount.Text = league.countTeam.ToString();
        }

    }
}
