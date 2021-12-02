using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using FCM.ViewModel;

namespace FCM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Account currentAccount { get; set; }
        public League league { get; set; }

        public List<Board> boards { get; set; }
        public Team team { get; set; }
        public Setting setting { get; set; }
        public MainWindow()
        {
            MessageBox.Show(((char)0).ToString());
            InitializeComponent();
        }
        public MainWindow(Account account)
        {
            currentAccount = account;
            InitializeComponent();
            tblUsername.Text = "Xin chào " + account.userName;
            if (currentAccount.idLastLeague != -1)
            {
                league =  LeagueDAO.Instance.GetLeagueById(currentAccount.idLastLeague);
                MainViewModel mainViewModel = new MainViewModel();
                mainViewModel.LoadScreenHomeWithLeague(this);
                mainViewModel.LoadDetailLeague(league,this);
            }

        }
    }
}
