using FCM.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using FCM.DAO;
using FCM.DTO;
using FCM.UserControls;
using System.Windows.Media.Imaging;

namespace FCM.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public ICommand SwitchTabCommand { get; set; }
        public ICommand SwitchTabStatisticsCommand { get; set; }
        public ICommand GetUidCommand { get; set; }

        public ICommand OpenAddLeagueWindowCommand { get; set; }

        public ICommand OpenEditLeagueWindowCommand { get; set; }
        public ICommand DeleteLeagueCommand { get; set; }
        public ICommand DeleteTeamCommand { get; set; }
        public ICommand OpenAddTeamWindowCommand { get; set; }
        public ICommand OpenEditTeamWindowCommand { get; set; }
        public ICommand OpenAddPlayerWindowCommand { get; set; }
        public ICommand OpenEditDialogCommand { get; set; }
        public ICommand OpenAddGoalTypeCommand { get; set; }
        public ICommand OpenEditGoalTypeCommand { get; set; }
        public ICommand OpenChangePasswordCommand { get; set; }
        public ICommand OpenLoginCommand { get; set; }

        public ICommand SearchLeagueCommand { get; set; }

        public ICommand CreateScheduleCommand { get; set; }
        public ICommand ChangeRoundCommand { get; set; }

        //public ICommand OpenEditMatchWindowCommand { get; set; }
        //public ICommand OpenResultRecordWindowCommand { get; set; }


        public int round = 0;

        public string uid;

        public SolidColorBrush lightGreen = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#52ff00"));
        public SolidColorBrush white = new SolidColorBrush(Colors.White);
        public MainViewModel()
        {
            SwitchTabCommand = new RelayCommand<MainWindow>((parameter) => true, (parameter) => SwitchTab(parameter));
            SwitchTabStatisticsCommand = new RelayCommand<MainWindow>((parameter) => true, (parameter) => SwitchTabStatistics(parameter));
            GetUidCommand = new RelayCommand<Button>((parameter) => true, (parameter) => uid = parameter.Uid);
            OpenAddLeagueWindowCommand = new RelayCommand<MainWindow>((parameter) => true, (parameter) => OpenAddLeagueWindow(parameter));
            DeleteLeagueCommand = new RelayCommand<MainWindow>((parameter) => true, (parameter) => DeleteLeague(parameter));
            DeleteTeamCommand = new RelayCommand<MainWindow>((parameter) => true, (parameter) => DeleteTeam(parameter));

            OpenEditDialogCommand = new RelayCommand<string>((parameter) => true, (parameter) => OpenEditDialogWindow(parameter));
            OpenEditLeagueWindowCommand = new RelayCommand<MainWindow>((parameter) => true, (parameter) => OpenEditLeagueWindow(parameter));
            OpenAddTeamWindowCommand = new RelayCommand<MainWindow>((parameter) => true, (parameter) => OpenAddTeamWindow(parameter));
            OpenEditTeamWindowCommand = new RelayCommand<MainWindow>((parameter) => true, (parameter) => OpenEditTeamWindow(parameter));
            OpenAddPlayerWindowCommand = new RelayCommand<MainWindow>((parameter) => true, (parameter) => OpenAddPlayerWindow(parameter));
            OpenAddGoalTypeCommand = new RelayCommand<MainWindow>((parameter) => true, (parameter) => OpenAddGoalTypeWindow(parameter));
            OpenEditGoalTypeCommand = new RelayCommand<MainWindow>((parameter) => true, (parameter) => OpenEditGoalTypeWindow(parameter));
            OpenChangePasswordCommand = new RelayCommand<MainWindow>((parameter) => true, (parameter) => OpenChangePasswordWindow(parameter));
            OpenLoginCommand = new RelayCommand<MainWindow>((parameter) => true, (parameter) => OpenLoginWindow(parameter));
            SearchLeagueCommand = new RelayCommand<MainWindow>((parameter) => true, (parameter) => SearchLeague(parameter));

            CreateScheduleCommand = new RelayCommand<MainWindow>((parameter) => true, (parameter) => CreateSchedule(parameter));
            ChangeRoundCommand = new RelayCommand<MainWindow>((parameter) => true, (parameter) => ChangeCbxRound(parameter));

            //OpenEditMatchWindowCommand = new RelayCommand<MainWindow>((parameter) => true, (parameter) => OpenEditMatchInfoWindow(parameter));
            //OpenResultRecordWindowCommand = new RelayCommand<MainWindow>((parameter) => true, (parameter) => OpenResultRecordingWindow(parameter));
        }
        public void SwitchTab(MainWindow parameter)
        {
            int index = int.Parse(uid); // tab index
            //Move Stroke Menu
            parameter.grdStroke.Margin = new Thickness(0, (150 + 60 * index), 0, 0);

            //Reset Color items
            parameter.btnHome.Foreground = white;
            parameter.btnLeagues.Foreground = white;
            parameter.btnSchedule.Foreground = white;
            parameter.btnTeams.Foreground = white;
            parameter.btnStanding.Foreground = white;
            parameter.btnStatistics.Foreground = white;
            parameter.btnSetting.Foreground = white;
            parameter.btnHelp.Foreground = white;
            parameter.btnAccount.Foreground = white;

            parameter.icHome.Foreground = white;
            parameter.icLeagues.Foreground = white;
            parameter.icSchedule.Foreground = white;
            parameter.icTeams.Foreground = white;
            parameter.icStanding.Foreground = white;
            parameter.icStatistics.Foreground = white;
            parameter.icSetting.Foreground = white;
            parameter.icHelp.Foreground = white;
            parameter.icAccount.Foreground = white;

            // Disable all screen
            parameter.grdHomeScreen.Visibility = Visibility.Hidden;
            parameter.grdHomeNoLeagueScreen.Visibility = Visibility.Hidden;
            parameter.grdLeaguesScreen.Visibility = Visibility.Hidden;
            parameter.grdScheduleScreen.Visibility = Visibility.Hidden;
            parameter.grdTeamsScreen.Visibility = Visibility.Hidden;
            parameter.grdStandingScreen.Visibility = Visibility.Hidden;
            parameter.grdStatisticsScreen.Visibility = Visibility.Hidden;
            parameter.grdSettingScreen.Visibility = Visibility.Hidden;
            parameter.grdHelpsScreen.Visibility = Visibility.Hidden;
            parameter.grdAccountScreen.Visibility = Visibility.Hidden;
            parameter.grdScheduleChart.Visibility = Visibility.Hidden;

            // Switch tab - Show selected screen
            switch (index)
            {
                case 0:
                    parameter.btnHome.Foreground = lightGreen;
                    parameter.icHome.Foreground = lightGreen;
                    if (0 > 1) // Nếu có ít nhất 1 mùa giải
                    {
                        parameter.grdHomeScreen.Visibility = Visibility.Visible;
                    }
                    else
                        parameter.grdHomeNoLeagueScreen.Visibility = Visibility.Visible;
                    break;
                case 1:
                    LoadListLeague(parameter);
                    parameter.btnLeagues.Foreground = lightGreen;
                    parameter.icLeagues.Foreground = lightGreen;
                    parameter.grdLeaguesScreen.Visibility = Visibility.Visible;
                    if (parameter.currentAccount.roleLevel == 0)
                    {
                        parameter.btnDeleteLeague.IsEnabled = false;
                        parameter.btnEditLeague.IsEnabled = false;
                        parameter.btnCreateLeague.IsEnabled = false;
                    }
                    break;
                case 2:
                    parameter.btnSchedule.Foreground = lightGreen;
                    parameter.icSchedule.Foreground = lightGreen;
                    parameter.grdScheduleScreen.Visibility = Visibility.Visible;

                    
                    AddItemsForCbxRound(parameter);
                    parameter.cbxRound.SelectedIndex = 0;
                    LoadListMatch(parameter, 0);

                    break;
                case 3:
                    LoadListTeams(parameter);
                    parameter.btnTeams.Foreground = lightGreen;
                    parameter.icTeams.Foreground = lightGreen;
                    parameter.grdTeamsScreen.Visibility = Visibility.Visible;
                    if (parameter.currentAccount.roleLevel==0)
                    {
                        parameter.btnAddPlayer.IsEnabled = false;
                        parameter.btnAddTeam.IsEnabled = false;
                        parameter.btnDeleteTeam.IsEnabled = false;
                        parameter.btnEditInforTeam.IsEnabled = false;
                    }
                    break;
                case 4:
                    parameter.btnStanding.Foreground = lightGreen;
                    parameter.icStanding.Foreground = lightGreen;
                    parameter.grdStandingScreen.Visibility = Visibility.Visible;
                    break;
                case 5:
                    parameter.btnStatistics.Foreground = lightGreen;
                    parameter.icStatistics.Foreground = lightGreen;
                    parameter.grdStatisticsScreen.Visibility = Visibility.Visible;
                    break;
                case 6:
                    if (parameter.league != null)
                    {
                        parameter.btnSetting.Foreground = lightGreen;
                        parameter.icSetting.Foreground = lightGreen;
                        parameter.grdSettingScreen.Visibility = Visibility.Visible;
                        GetDetailSetting(parameter);
                    }
                    break;
                case 7:
                    parameter.btnHelp.Foreground = lightGreen;
                    parameter.icHelp.Foreground = lightGreen;
                    parameter.grdHelpsScreen.Visibility = Visibility.Visible;
                    break;
                case 8:
                    parameter.btnAccount.Foreground = lightGreen;
                    parameter.icAccount.Foreground = lightGreen;
                    parameter.grdAccountScreen.Visibility = Visibility.Visible;
                    break;

                default:
                    break;
            }
        }

        public void OpenAddLeagueWindow(MainWindow parameter)
        {
            if (parameter.currentAccount.roleLevel == 1)
            {
                AddLeagueWindow wd = new AddLeagueWindow();
                wd.ShowDialog();
                LoadListLeague(parameter);
            }
        }
        public List<League> leagues;
        public void LoadListLeague(MainWindow parameter)
        {
            leagues = LeagueDAO.Instance.GetListLeagues();
            LoadListLeagueToScreen(leagues, parameter);
        }
        public void LoadListLeagueToScreen(List<League> listLeagues, MainWindow parameter)
        {
            parameter.wpLeagueCards.Children.Clear();
            if (listLeagues != null)
            {
                if (parameter.league == null)
                {
                    if (leagues.Count > 0)
                    {
                        LoadDetailLeague(leagues[0], parameter);
                    }
                }
                if (listLeagues.Count == 0)
                    ChangeStatus(-1, parameter);
                foreach (League league in listLeagues)
                {
                    ucLeagueCard ucLeagueCard = new ucLeagueCard(league, parameter, this);
                    parameter.wpLeagueCards.Children.Add(ucLeagueCard);
                }
            }
        }
        public void LoadDetailLeague(League league, MainWindow window)
        {
            window.league = league;
            window.imgLeagueLogo.Source = ImageProcessing.Instance.Convert(ImageProcessing.Instance.ByteToImg(league.logo));
            window.tblLeagueName.Text = "Tên mùa giải: " + league.nameLeague;
            window.tblSponsor.Text = "Nhà tài trợ: " + league.nameSpender;
            window.setting = SettingDAO.Instance.GetSetting(league.id);

            switch (league.status)
            {
                case 0:
                    window.tblLeagueStatus.Text = "Trạng thái: Đang đăng ký";
                    break;
                case 1:
                    window.tblLeagueStatus.Text = "Trạng thái: Chuẩn bị bắt đầu";
                    break;
                case 2:
                    window.tblLeagueStatus.Text = "Trạng thái: Đã bắt đầu";
                    break;
            }
            ChangeStatus(league.status, window);
            window.tblLeagueTime.Text = "Thời gian: " + league.dateTime.ToString("M/d/yyyy");
        }
        public void ChangeStatus(int status, MainWindow parameter)
        {
            switch (status)
            {
                case -1:
                    parameter.btnSchedule.IsEnabled = false;
                    parameter.btnReport.IsEnabled = false;
                    parameter.btnTeams.IsEnabled = false;
                    parameter.btnStanding.IsEnabled = false;
                    parameter.btnSetting.IsEnabled = false;
                    break;
                case 0:
                    parameter.btnSchedule.IsEnabled = false;
                    parameter.btnReport.IsEnabled = false;
                    parameter.btnTeams.IsEnabled = true;
                    parameter.btnStanding.IsEnabled = false;
                    parameter.btnSetting.IsEnabled = true;
                    break;
                case 1:
                    parameter.btnSchedule.IsEnabled = true;
                    parameter.btnReport.IsEnabled = true;
                    parameter.btnTeams.IsEnabled = true;
                    parameter.btnStanding.IsEnabled = true;
                    parameter.btnSetting.IsEnabled = true;
                    break;
                case 2:
                    parameter.btnSchedule.IsEnabled = true;
                    parameter.btnReport.IsEnabled = true;
                    parameter.btnTeams.IsEnabled = true;
                    parameter.btnStanding.IsEnabled = true;
                    parameter.btnSetting.IsEnabled = true;
                    break;
            }
        }
        public void DeleteLeague(MainWindow parameter)
        {
            if (parameter.currentAccount.roleLevel == 1)
            {
                if (parameter.league == null)
                {
                }
                else
                {
                    LeagueDAO.Instance.DeleteLeague(parameter.league);
                    parameter.league = null;

                    parameter.imgLeagueLogo.Source = parameter.nullImage.Source;
                    parameter.tblLeagueName.Text = "";
                    parameter.tblSponsor.Text = "";
                    parameter.tblLeagueStatus.Text = "";
                    parameter.tblLeagueTime.Text = "";
                    LoadListLeague(parameter);
                }
            }
        }
        public void OpenEditLeagueWindow(MainWindow parameter)
        {
            if (parameter.currentAccount.roleLevel == 1)
            {
                if (parameter.league != null && parameter.league.status == 0)
                {
                    AddLeagueWindow wd = new AddLeagueWindow(parameter.league);
                    wd.tblTitle.Text = "SỬA THÔNG TIN GIẢI ĐẤU";
                    wd.btnCreateLeague.Content = "Sửa";
                    wd.tbSponsor.Text = parameter.league.nameSpender;
                    wd.tbUsername.Text = parameter.league.nameLeague;
                    wd.imgLeagueLogo.Source = parameter.imgLeagueLogo.Source;
                    wd.datePicker.SelectedDate = parameter.league.dateTime;
                    wd.tbCountOfTeams.Text = parameter.league.countTeam.ToString();
                    wd.ShowDialog();
                    LoadDetailLeague(LeagueDAO.Instance.GetLeagueById(parameter.league.id), parameter);
                    LoadListLeague(parameter);
                }
            }
        }

        public void SearchLeague(MainWindow parameter)
        {
            string name = InputFormat.Instance.FomartSpace(parameter.tbSearchLeague.Text);
            List<League> listLeague = new List<League>();
            foreach (League league in leagues)
            {
                if (league.nameLeague.Contains(name))
                    listLeague.Add(league);
            }
            LoadListLeagueToScreen(listLeague, parameter);
        }
        public void GetDetailSetting(MainWindow parameter)
        {
            if (parameter.setting != null)
            {
                parameter.tblCountOfTeams.Text = parameter.setting.numberOfTeam.ToString();
                parameter.tblMinCountPlayers.Text = parameter.setting.minPlayerOfTeam.ToString();
                parameter.tblMaxCountPlayers.Text = parameter.setting.maxPlayerOfTeam.ToString();
                parameter.tblMinAge.Text = parameter.setting.minAge.ToString();
                parameter.tblMaxAge.Text = parameter.setting.maxAge.ToString();
                parameter.tblMaxForeign.Text = parameter.setting.maxForeignPlayers.ToString();
                parameter.tbScoreWin.Text = parameter.setting.scoreWin.ToString();
                parameter.tbScoreDraw.Text = parameter.setting.scoreDraw.ToString();
                parameter.tbScoreLose.Text = parameter.setting.scoreLose.ToString();
            }
        }

        List<Team> teams;
        public void LoadListTeams(MainWindow parameter)
        {
            parameter.wpTeamsList.Children.Clear();
            if (parameter.league!=null)
            {
                teams = TeamDAO.Instance.GetListTeam(parameter.league.id);
                foreach (Team team in teams)
                {
                    ucTeamButton teamButton = new ucTeamButton(team, parameter, this);
                    parameter.wpTeamsList.Children.Add(teamButton);
                }
                if (parameter.wpTeamsList.Children.Count > 0)
                    LoadDetailTeam(parameter, teams[0]);
                else
                    LoadListPlayer(parameter, -1);
            }
        }
        public void LoadDetailTeam(MainWindow parameter, Team team)
        {
            parameter.team = team;
            parameter.tblTeamName.Text = team.nameTeam;
            parameter.tblCoach.Text = team.coach;
            parameter.tblNational.Text = team.nation;
            parameter.tblStadium.Text = team.stadium;
            parameter.imgTeamLogo.Source = ImageProcessing.Instance.Convert(ImageProcessing.Instance.ByteToImg(team.logo));
            LoadListPlayer(parameter,team.id);
            parameter.tblCountOfMembers.Text = parameter.wpPlayersList.Children.Count.ToString();
            if (parameter.wpPlayersList.Children.Count > parameter.setting.minPlayerOfTeam)
                parameter.tblStatus.Text = "Hợp lệ";
            else
                parameter.tblStatus.Text = "Chưa hợp lệ";
        }
        public int CountNationatily(MainWindow parameter)
        {
            int s = 0;
            foreach (ucPlayer ucPlayer in parameter.wpPlayersList.Children)
            {
                if (ucPlayer.player.nationality != parameter.team.nation)
                    s++;

            }    
            return s;
        }
        public void OpenAddTeamWindow(MainWindow parameter)
        {
            if (parameter.currentAccount.roleLevel == 1)
            {
                if (parameter.wpTeamsList.Children.Count==parameter.setting.numberOfTeam)
                {
                    MessageBox.Show("Số lượng đội bóng đá đạt tối đa");
                }
                AddTeamWindow wd = new AddTeamWindow(parameter.league.id);
                wd.ShowDialog();
                LoadListTeams(parameter);
            }
        }
        public void OpenEditTeamWindow(MainWindow parameter)
        {
            if (parameter.currentAccount.roleLevel == 1)
            {
                if (parameter.team != null)
                {
                    AddTeamWindow wd = new AddTeamWindow(parameter.league.id,parameter.team);
                    wd.tblTitle.Text = "SỬA THÔNG TIN ĐỘI BÓNG";
                    wd.btnAdd.Content = "Sửa";
                    wd.tbName.Text = parameter.team.nameTeam;
                    wd.tbCoach.Text = parameter.team.coach;
                    wd.tbNational.Text = parameter.team.nation;
                    wd.tbStadium.Text = parameter.team.stadium;
                    wd.imgTeamLogo.Source = ImageProcessing.Instance.Convert(ImageProcessing.Instance.ByteToImg(parameter.team.logo));
                    wd.btnImportTeam.Visibility = Visibility.Hidden;
                    wd.ShowDialog();
                    LoadListTeams(parameter);
                }
            }
        }
        public void DeleteTeam(MainWindow parameter)
        {
            if (parameter.currentAccount.roleLevel == 1)
            {
                if (parameter.team == null)
                {
                }
                else
                {
                    TeamDAO.Instance.DeleteTeam(parameter.league.id);
                    parameter.team = null;

                    parameter.tblTeamName.Text = "NULL";
                    parameter.tblStadium.Text = "NULL";
                    parameter.tblNational.Text = "NULL";
                    parameter.tblCoach.Text = "NULL";
                    parameter.tblCountOfMembers.Text = "NULL";
                    parameter.imgTeamLogo.Source = new BitmapImage(new Uri("pack://application:,,,/Resource/Images/software-logo.png"));
                    LoadListTeams(parameter);
                    LoadListPlayer(parameter, -1);
                }
            }
        }

        public void OpenAddPlayerWindow(MainWindow parameter)
        {
            if (parameter.currentAccount.roleLevel == 1)
            {
                if (parameter.setting.maxPlayerOfTeam == parameter.wpPlayersList.Children.Count)
                {
                    MessageBox.Show("Số lượng cầu thủ đã đạt tối đa");
                    return;
                }
                AddPlayerWindow wd = new AddPlayerWindow(parameter.team, parameter.setting,(CountNationatily(parameter)<parameter.setting.maxForeignPlayers));
                wd.ShowDialog();
                LoadListPlayer(parameter, parameter.team.id);

                parameter.league = LeagueDAO.Instance.GetLeagueById(parameter.league.id);

                if (parameter.league.status != 0)
                {
                    ChangeStatus(parameter.league.status, parameter);
                }    
            }
        }
        public void LoadListPlayer(MainWindow parameter, int idTeam)
        {
            if (idTeam < 0)
                return;
            parameter.wpPlayersList.Children.Clear();
            List<Player> players = PlayerDAO.Instance.GetListPlayer(idTeam);
            for (int i =0;i<players.Count;i++)
            {     
                ucPlayer ucPlayer = new ucPlayer(players[i], parameter.currentAccount.roleLevel,i+1, parameter,this);
                parameter.wpPlayersList.Children.Add(ucPlayer);
            }    
            
        }
        public void OpenEditPlayerWindow(MainWindow parameter, Player player)
        {
            if (parameter.team != null)
            {
                AddPlayerWindow wd = new AddPlayerWindow(parameter.team,player,parameter.setting,(CountNationatily(parameter)<parameter.setting.maxForeignPlayers));
                if (parameter.currentAccount.roleLevel == 1)
                {
                    wd.tblTitle.Text = "SỬA THÔNG TIN CẦU THỦ";
                    wd.btnAdd.Content = "Sửa";
                }
                else
                {
                    wd.tblTitle.Text = "THÔNG TIN CẦU THỦ";
                    wd.btnAdd.Content = "Thoát";
                    wd.btnAdd.Visibility = Visibility.Hidden;
                    wd.btnUploadImage.Visibility = Visibility.Hidden;
                }
                wd.ShowDialog();
                LoadListTeams(parameter);
            }
        }

        public void SwitchTabStatistics(MainWindow parameter)
        {
            int index = int.Parse(uid); // tab index
            //Move Stroke Tab
            parameter.rtStroke.Margin = new Thickness((20 + 120 * index), 0, 0, 5);

            // Reset color
            parameter.btnSttTeams.Foreground = white;
            parameter.btnSttPlayers.Foreground = white;
            parameter.btnSttCards.Foreground = white;

            // Hide all screens
            parameter.grdSttTeams.Visibility = Visibility.Hidden;
            parameter.grdSttPlayers.Visibility = Visibility.Hidden;
            parameter.grdSttCards.Visibility = Visibility.Hidden;

            switch (index)
            {
                case 0:
                    parameter.btnSttTeams.Foreground = lightGreen;
                    parameter.grdSttTeams.Visibility = Visibility.Visible;
                    break;
                case 1:
                    parameter.btnSttPlayers.Foreground = lightGreen;
                    parameter.grdSttPlayers.Visibility = Visibility.Visible;
                    break;
                case 2:
                    parameter.btnSttCards.Foreground = lightGreen;
                    parameter.grdSttCards.Visibility = Visibility.Visible;
                    break;
            }
        }
        public void OpenEditDialogWindow(string parameter)
        {
            // Dùng parameter để biết đã bấm vào nút Sửa nào
            EditDialogWindow wd = new EditDialogWindow(parameter);
            wd.ShowDialog();
        }

        public void OpenAddGoalTypeWindow(MainWindow parameter)
        {
            AddGoalTypeWindow wd = new AddGoalTypeWindow();
            wd.ShowDialog();
        }
        public void OpenEditGoalTypeWindow(MainWindow parameter)
        {
            AddGoalTypeWindow wd = new AddGoalTypeWindow();
            wd.Title = "Sửa thông tin";
            wd.btnAdd.Content = "Xác nhận";
            wd.ShowDialog();
        }
        public void OpenChangePasswordWindow(MainWindow parameter)
        {
            ChangePasswordWindow wd = new ChangePasswordWindow(parameter.currentAccount);
            wd.ShowDialog();
        }
        public void OpenLoginWindow(MainWindow parameter)
        {
            LoginWindow loginWindow = new LoginWindow();
            parameter.Hide();
            loginWindow.Show();
            parameter.Close();
        }



        #region SCHEDULE (Lịch thi đấu)
        // Tạo lịch thi đấu
        public void CreateSchedule(MainWindow parameter)
        {
            // Status = Đang đăng ký (Chưa đủ thông tin)
            if (parameter.league.status == 0)
            {
                MessageBox.Show("Giải đấu này đang trong tình trạng đăng ký!\nVui lòng cung cấp đầy đủ thông tin về đội bóng để tạo lịch thi đấu!", "Lỗi" , MessageBoxButton.OK,MessageBoxImage.Error);
                return;
            }
            // Status = Đã bắt đầu (Đã bắt đầu)
            if (parameter.league.status == 2)
            {
                MessageBox.Show("Giải đấu này đã được bắt đầu!\nKhông thể tạo lịch thi đấu!", "Lỗi" , MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Tiến hành tạo lịch
            if (MessageBox.Show("Sau khi tiến hành tạo lịch, các thông tin về Quy định giải đấu, Câu lạc bộ, Cầu thủ sẽ không được phép thay đổi nữa!\n" +
                "Các trận đấu sẽ được tạo ngẫu nhiên theo nguyên tắc vòng tròn tính điểm.\n" +
                "Bạn có muốn tạo lịch thi đấu?", "Lưu ý", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                // Hàm tạo lịch theo Vòng tròn tính điểm
                CreateScheduleWithCircle(parameter);

                // Thay đổi status của giải đấu = 2 (Đã bắt đầu khởi tranh)
                LeagueDAO.Instance.UpdateStatusOfLeague(parameter.league.id, 2);


                MessageBox.Show("Tạo lịch thi đấu thành công!", "Thành công", MessageBoxButton.OK);

                LoadListMatch(parameter, 0);
            }
        }

        // Tạo lịch thi đấu với cách vòng tròn tính điểm
        // Viết riêng hàm cho đẹp, hơn nữa để phát triển thêm nhiều các sắp lịch khác (nếu có phát triển thêm)
        public void CreateScheduleWithCircle(MainWindow parameter)
        {
            // Lưu ý: số lượng đội bóng BẮT BUỘC PHẢI là số CHẴN
            if (parameter.league.countTeam %2 == 1)
            {
                MessageBox.Show("Số đội bóng tham gia giải đấu phải là số chẵn!", "Lưu ý", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            List<Team> teams = TeamDAO.Instance.GetListTeam(parameter.league.id);

            // Số lượng đội bóng
            int nTeams = teams.Count;

            bool[,] homeStadium = new bool[nTeams + 1, nTeams + 1];
            // homeStadium[i,j] = true  : đã có 1 trận đấu đội i vs j và sân là sân nhà của đội i, = false : chưa diễn ra trận đấu giữa đội i và j
            for (int i = 0; i <= nTeams; i++) for (int j = 0; j <= nTeams; j++) homeStadium[i, j] = false;


            bool[] isAttended = new bool[nTeams + 1];
            // Tại 1 vòng đấu nào đóm đội i nếu đã tham gia thì isAttended[i] = true;

            Match[] matches = new Match[(nTeams - 1) * nTeams + 1];
            int nTrandau = 0;

            // Tạo lịch
            for (int vongdau = 1; vongdau <= (nTeams - 1) * 2; vongdau++) 
            {
                // Đánh dấu rằng: tại vòng đấu thứ vongdau, tất cả các đội bóng đều chưa tham gia
                for (int attended = 0; attended <= nTeams; attended++)
                {
                    isAttended[attended] = false;
                }

                //for (int i = 0; i < nTeams; i++)
                //{
                //    for (int j = i + 1; j < nTeams; j++)
                //    {
                //        if (i == j || isAttended[i] || isAttended[j] || homeStadium[i,j])
                //        {
                //            continue;
                //        }

                //        isAttended[i] = true;
                //        isAttended[j] = true;
                //        homeStadium[i, j] = true;
                //        nTrandau++;
                //        matches[nTrandau] = new Match(parameter.league.id, teams[i].id, teams[j].id, vongdau, teams[i].stadium);
                //    }    
                //}   

                int nAttended = 0; // Số đội bóng đã tham gia vòng đấu hiện tại

                Random rdTeam01 = new Random();
                Random rdTeam02 = new Random();

                while (nAttended < nTeams / 2) // Số trận đấu tối đa trong một vòng đấu = số đội bóng/2
                {
                    int team01 = rdTeam01.Next(1, nTeams + 1);
                    int team02 = rdTeam02.Next(1, nTeams + 1);

                    while (isAttended[team01] || isAttended[team02] || homeStadium[team01, team02] || team01 == team02)
                    {
                        team01 = rdTeam01.Next(1, nTeams + 1);
                        team02 = rdTeam02.Next(1, nTeams + 1);
                    }

                    isAttended[team01] = true;
                    isAttended[team02] = true;
                    homeStadium[team01, team02] = true;

                    nTrandau++;
                    matches[nTrandau] = new Match(parameter.league.id, teams[team01 - 1].id, teams[team02 - 1].id, vongdau, teams[team01 - 1].stadium);

                    nAttended++;
                }
            }

            for (int i = 1; i <= nTrandau; i++)
            {
                //matches[i].date = DateTime.Now.Date;
                matches[i].date = DateTime.ParseExact(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                matches[i].time = DateTime.ParseExact(DateTime.Now.ToString("HH:mm"), "HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                MatchDAO.Instance.AddMatch(matches[i]);
            }    
        }

        // Thêm dữ liệu vào thanh lọc Vòng đấu
        public void AddItemsForCbxRound(MainWindow parameter)
        {
            parameter.cbxRound.Items.Clear();
            parameter.cbxRound.Items.Add("Tất cả");
            for (int i = 1; i <= (parameter.league.countTeam - 1) * 2; i++)
            {
                string item = "Vòng " + i.ToString();
                parameter.cbxRound.Items.Add(item);
            }    
        }


        // Hiển thị danh sách trận đấu
        List<Match> listMatches;
        public void LoadListMatch(MainWindow parameter, int round)
        {
            if (round == 0)
            {
                listMatches = MatchDAO.Instance.GetListMatch(parameter.league.id);
            }    
            else
            {
                listMatches = MatchDAO.Instance.GetListMatchByRound(parameter.league.id, round);
            }
            
            parameter.wpSchedule.Children.Clear();

            int i = 0;
            foreach (Match match in listMatches)
            {
                i++;
                ucMatchDetail ucmatchDetail = new ucMatchDetail(i, match, parameter, this);
                parameter.wpSchedule.Children.Add(ucmatchDetail);
            }    

        }

        public void ChangeCbxRound(MainWindow parameter)
        {
            int round = parameter.cbxRound.SelectedIndex;
            this.round = round;
            LoadListMatch(parameter, round);
        }

        public void OpenEditMatchInfoWindow(MainWindow parameter, Match match)
        {
            EditMatchInforWindow wd = new EditMatchInforWindow(match);
            wd.ShowDialog();

            LoadListMatch(parameter, parameter.cbxRound.SelectedIndex);
        }
        public void OpenResultRecordingWindow(MainWindow parameter, Match match)
        {
            ResultRecordingWindow wd = new ResultRecordingWindow(match);
            wd.ShowDialog();
            LoadListMatch(parameter, this.round);
        }
        #endregion
    }
}
