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
        public List<Board> boards { get; set; }
        public Setting setting { get; set; }
        public AddTeamWindow()
        {
            InitializeComponent();
        }
        public AddTeamWindow(int idTournament, List<Board> boards, Setting setting) // ADD
        {
            InitializeComponent();
            this.boards = boards;
            this.idTournament = idTournament;
            this.setting = setting;
            GetBoardsComobox();
        }
        public AddTeamWindow(int idTournament, List<Board> boards, Setting setting, Team team) //Edit
        {
            InitializeComponent();
            this.boards = boards;
            this.idTournament = idTournament;
            this.team = team;
            this.setting = setting;
            GetBoardsComobox();
        }
        public void GetBoardsComobox()
        {
            cbGroups.Items.Clear();
            MessageBox.Show(boards.Count.ToString());
            int max = boards[0].countTeam;
            if (BoardDAO.Instance.CountBoardFull(idTournament) == setting.numberOfTeam % boards.Count && setting.numberOfTeam % boards.Count>0)
                max--;
            //foreach (Board board in this.boards)
            //{
            //    if (max == TeamDAO.Instance.GetCountTeam(board.nameBoard))
            //        boards.Remove(board);
            //    else
            //        cbGroups.Items.Add(board.nameBoard);
            //}
            for(int i=0;i<boards.Count;i++)
            {
                if (team == null || team.nameBoard != boards[i].nameBoard)
                {
                    if (max == TeamDAO.Instance.GetCountTeam(boards[i].nameBoard))
                        boards.Remove(boards[i]);
                    else
                        cbGroups.Items.Add(boards[i].nameBoard);
                    continue;
                }
                cbGroups.Items.Add(boards[i].nameBoard);
            }
        }
    }
}
