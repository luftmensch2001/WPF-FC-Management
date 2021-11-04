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
using FCM.DAO;
using FCM.DTO;

namespace FCM.View
{
    /// <summary>
    /// Interaction logic for AddTeamWindow.xaml
    /// </summary>
    public partial class AddTeamWindow : Window
    {
        public int idTournament { get; set; } = -1;
        public Team team { get; set; }
        public AddTeamWindow()
        {
            InitializeComponent();
        }
        public AddTeamWindow(int idTournament)
        {
            InitializeComponent();
            this.idTournament = idTournament;
        }
        public AddTeamWindow(int idTournament, Team team)
        {
            InitializeComponent();
            this.idTournament = idTournament;
            this.team = team;
        }
    }
}
