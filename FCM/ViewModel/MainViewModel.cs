﻿using FCM.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using FCM.DAO;
using FCM.DTO;
using FCM.UserControls;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.IO;

namespace FCM.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public ICommand SwitchTabCommand { get; set; }
        public ICommand SwitchTabStatisticsCommand { get; set; }
        public ICommand GetUidCommand { get; set; }
        public ICommand GetIdSettingCommand { get; set; }

        public ICommand OpenAddLeagueWindowCommand { get; set; }

        public ICommand OpenEditLeagueWindowCommand { get; set; }
        public ICommand DeleteLeagueCommand { get; set; }
        public ICommand DeleteTeamCommand { get; set; }
        public ICommand DeleteGoalTypeCommand { get; set; }
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
        public ICommand ExportTeamCommand { get; set; }
        public ICommand ChangeBoardCommand { get; set; }
        public ICommand ChangeRankingBoardCommand { get; set; }

        //public ICommand OpenEditMatchWindowCommand { get; set; }
        //public ICommand OpenResultRecordWindowCommand { get; set; }


        public int round = 0;

        public string uid;

        public string idSetting = "";


        public SolidColorBrush lightGreen = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#52ff00"));
        public SolidColorBrush white = new SolidColorBrush(Colors.White);
        public MainViewModel()
        {
            SwitchTabCommand = new RelayCommand<MainWindow>((parameter) => true, (parameter) => SwitchTab(parameter));
            SwitchTabStatisticsCommand = new RelayCommand<MainWindow>((parameter) => true, (parameter) => SwitchTabStatistics(parameter));
            GetUidCommand = new RelayCommand<System.Windows.Controls.Button>((parameter) => true, (parameter) => uid = parameter.Uid);
            GetIdSettingCommand = new RelayCommand<string>((parameter) => true, (parameter) => idSetting = parameter);
            OpenAddLeagueWindowCommand = new RelayCommand<MainWindow>((parameter) => true, (parameter) => OpenAddLeagueWindow(parameter));
            DeleteLeagueCommand = new RelayCommand<MainWindow>((parameter) => true, (parameter) => DeleteLeague(parameter));
            DeleteTeamCommand = new RelayCommand<MainWindow>((parameter) => true, (parameter) => DeleteTeam(parameter));
            DeleteGoalTypeCommand = new RelayCommand<MainWindow>((parameter) => true, (parameter) => DeleteGoalType(parameter));

            OpenEditDialogCommand = new RelayCommand<MainWindow>((parameter) => true, (parameter) => OpenEditDialogWindow(parameter));
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

            ExportTeamCommand = new RelayCommand<MainWindow>((parameter) => true, (parameter) => ExportTeam(parameter));
            ChangeBoardCommand = new RelayCommand<MainWindow>((parameter) => true, (parameter) => SearchBoard(parameter));
            ChangeRankingBoardCommand = new RelayCommand<MainWindow>((parameter) => true, (parameter) => LoadRanking(parameter));

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
                    parameter.btnTeams.Foreground = lightGreen;
                    parameter.icTeams.Foreground = lightGreen;
                    parameter.grdTeamsScreen.Visibility = Visibility.Visible;
                    if (parameter.currentAccount.roleLevel == 0)
                    {
                        parameter.btnAddPlayer.IsEnabled = false;
                        parameter.btnAddTeam.IsEnabled = false;
                        parameter.btnDeleteTeam.IsEnabled = false;
                        parameter.btnEditInforTeam.IsEnabled = false;
                    }
                    if (parameter.league.typeLeague == 0 || parameter.league.typeLeague == 1)
                    {
                        parameter.borderTeamList.Height = 480;
                        parameter.grdTeamList.Height = 475;
                        parameter.cbGroup.Visibility = Visibility.Hidden;
                        parameter.LbGroup.Text = "";
                        parameter.tblStatus.Text = "";
                    } else
                    {
                        parameter.borderTeamList.Height = 420;
                        parameter.grdTeamList.Height = 415;
                        parameter.cbGroup.Visibility = Visibility.Visible;
                        parameter.LbGroup.Text = "Bảng đấu";
                        GetBoards(parameter);
                    }
                    LoadListTeams(parameter);

                    break;
                case 4:
                    parameter.btnStanding.Foreground = lightGreen;
                    parameter.icStanding.Foreground = lightGreen;
                    parameter.grdStandingScreen.Visibility = Visibility.Visible;
                    InitCbbRanking(parameter);
                    LoadRanking(parameter);
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

                        bool btState = false;
                        if (TeamDAO.Instance.GetListTeamInLeague(parameter.league.id).Count > 0)
                            btState = false;
                        else
                            btState = true;

                        {
                            parameter.btEditS0.IsEnabled = btState;
                            parameter.btEditS1.IsEnabled = btState;
                            parameter.btEditS2.IsEnabled = btState;
                            parameter.btEditS3.IsEnabled = btState;
                            parameter.btEditS4.IsEnabled = btState;
                            parameter.btEditS5.IsEnabled = btState;
                            if (parameter.league.typeLeague == 0)
                            {
                                parameter.btEditS6.IsEnabled = false;
                            }
                            else
                                parameter.btEditS6.IsEnabled = btState;
                            parameter.btEditS7.IsEnabled = btState;
                            parameter.btEditS8.IsEnabled = btState;
                            parameter.btEditS9.IsEnabled = btState;
                            parameter.btnAddGoalType.IsEnabled = btState;
                            parameter.btnEditGoalType.IsEnabled = btState;
                            parameter.btnDeleteGoalType.IsEnabled = btState;
                        }
                        GetDetailSetting(parameter);
                        LoadTypesOfGoal(parameter);
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
                    parameter.btnStanding.IsEnabled = true;
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
        public void GetBoards(MainWindow mainWindow)
        {
            mainWindow.cbGroup.Items.Clear();
            mainWindow.cbGroup.Items.Add("Tất cả");
            mainWindow.cbGroup.SelectedItem = 0;
            foreach (Board board in mainWindow.boards)
            {
                mainWindow.cbGroup.Items.Add(board.nameBoard);
            }    
        }
        public void SearchBoard(MainWindow mainWindow)
        {
            if (mainWindow.cbGroup.SelectedItem!=null)
                ShowTeam(mainWindow, mainWindow.cbGroup.SelectedItem.ToString());

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


        #region League

        public List<League> leagues;
        public void OpenAddLeagueWindow(MainWindow parameter)
        {
            if (parameter.currentAccount.roleLevel == 1)
            {
                AddLeagueWindow wd = new AddLeagueWindow();
                wd.ShowDialog();
                LoadListLeague(parameter);
            }
        }
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
            window.boards = BoardDAO.Instance.GetListBoard(league.id);
            window.currentAccount.idLastLeague = league.id;
            AccountDAO.Instance.UpdateIdLastLeague(window.currentAccount.userName, league.id);
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
            LoadListTeams(window);
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
                    parameter.currentAccount.idLastLeague = -1;
                    AccountDAO.Instance.UpdateIdLastLeague(parameter.currentAccount.userName, -1);
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

        #endregion

        #region Team
        List<Team> teams;
        public void LoadListTeams(MainWindow parameter)
        {

            if (parameter.league != null)
            {
                teams = TeamDAO.Instance.GetListTeamInLeague(parameter.league.id);
                if (teams.Count == 0)
                {
                    ChangeStatus(0, parameter);
                    parameter.wpTeamsList.Children.Clear();
                    parameter.wpPlayersList.Children.Clear();
                    return;
                }

                foreach (Team team in teams)
                {
                    ucTeamButton teamButton = new ucTeamButton(team, parameter, this);
                    //parameter.wpTeamsList.Children.Add(teamButton);
                }
                if (teams.Count == parameter.setting.numberOfTeam)
                    parameter.btnAddTeam.Visibility = Visibility.Hidden;
                else
                    parameter.btnAddTeam.Visibility = Visibility.Visible;
                if (parameter.wpTeamsList.Children.Count > 0)
                {
                    if (parameter.team == null)
                        LoadDetailTeam(parameter, teams[0]);
                    else
                        LoadDetailTeam(parameter, parameter.team);
                }
                else
                    LoadListPlayer(parameter, -1);
                if (teams.Count == parameter.setting.numberOfTeam)
                    ChangeStatus(1, parameter);
                else
                    ChangeStatus(0, parameter);
                if (parameter.league.typeLeague == 0 || parameter.league.typeLeague == 1)
                    ShowTeam(parameter, "Tất cả");
                else
                    ShowTeam(parameter, parameter.cbGroup.Text);
            }
        }
        public void ShowTeam(MainWindow mainWindow, string name)
        {
            if (teams == null)
                return;
            mainWindow.wpTeamsList.Children.Clear();
            if (name == "Tất cả")
                foreach (Team team in teams)
                {
                    ucTeamButton teamButton = new ucTeamButton(team, mainWindow, this);
                    mainWindow.wpTeamsList.Children.Add(teamButton);
                }
            else
            {
                foreach (Team team in teams)
                    if (team.nameBoard == name)
                    {
                        ucTeamButton teamButton = new ucTeamButton(team, mainWindow, this);
                        mainWindow.wpTeamsList.Children.Add(teamButton);
                    }
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
            LoadListPlayer(parameter, team.id);
            parameter.tblCountOfMembers.Text = parameter.wpPlayersList.Children.Count.ToString();
            parameter.tblStatus.Text = parameter.team.nameBoard;
        }
        public void OpenAddTeamWindow(MainWindow parameter)
        {
            if (parameter.currentAccount.roleLevel == 1)
            {
                if (TeamDAO.Instance.GetListTeamInLeague(parameter.league.id).Count == 0)
                {
                    if (MessageBox.Show("Sau thi tạo đội bóng đầu tiên sẽ không thể thay đổi quy định của giải nữa \nXác nhận tạo đội?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                    {
                        return;
                    }
                    else
                    {

                    }
                }
                if (parameter.wpTeamsList.Children.Count == parameter.setting.numberOfTeam)
                {
                    MessageBox.Show("Số lượng đội bóng đá đạt tối đa");
                }
                AddTeamWindow wd = new AddTeamWindow(parameter.league.id, parameter.boards, parameter.setting);
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
                    AddTeamWindow wd = new AddTeamWindow(parameter.league.id, parameter.boards, parameter.setting, parameter.team);
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
                    parameter.team = TeamDAO.Instance.GetTeamById(parameter.team.id);
                    LoadDetailTeam(parameter, parameter.team);
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
                    TeamDAO.Instance.DeleteTeam(parameter.league.id, parameter.team.id);
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
        void ExportTeam(MainWindow parameter)
        {
            if (parameter.team != null)
            {
                ExcelProcessing.Instance.ExportFile(parameter.team);
            }
        }

        #endregion

        #region Player

        public void OpenAddPlayerWindow(MainWindow parameter)
        {
            if (parameter.currentAccount.roleLevel == 1)
            {
                if (parameter.setting.maxPlayerOfTeam == parameter.wpPlayersList.Children.Count)
                {
                    MessageBox.Show("Số lượng cầu thủ đã đạt tối đa");
                    return;
                }
                AddPlayerWindow wd = new AddPlayerWindow(parameter.team, parameter.setting, (CountNationatily(parameter) < parameter.setting.maxForeignPlayers));
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
            parameter.wpPlayersList.Children.Clear();
            if (idTeam < 0)
                return;
            List<Player> players = PlayerDAO.Instance.GetListPlayer(idTeam);
            for (int i = 0; i < players.Count; i++)
            {
                ucPlayer ucPlayer = new ucPlayer(players[i], parameter.currentAccount.roleLevel, i + 1, parameter, this);
                parameter.wpPlayersList.Children.Add(ucPlayer);
            }

        }
        public void OpenEditPlayerWindow(MainWindow parameter, Player player)
        {
            if (parameter.team != null)
            {
                AddPlayerWindow wd = new AddPlayerWindow(parameter.team, player, parameter.setting, (CountNationatily(parameter) < parameter.setting.maxForeignPlayers));
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

        #endregion

        #region SCHEDULE (Lịch thi đấu)
        // Tạo lịch thi đấu
        public void CreateSchedule(MainWindow parameter)
        {
            // Status = Đang đăng ký (Chưa đủ thông tin)
            if (parameter.league.status == 0)
            {
                MessageBox.Show("Giải đấu này đang trong tình trạng đăng ký!\nVui lòng cung cấp đầy đủ thông tin về đội bóng để tạo lịch thi đấu!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            // Status = Đã bắt đầu (Đã bắt đầu)
            if (parameter.league.status == 2)
            {
                MessageBox.Show("Giải đấu này đã được bắt đầu!\nKhông thể tạo lịch thi đấu!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Tiến hành tạo lịch
            if (MessageBox.Show("Sau khi tiến hành tạo lịch, các thông tin về Câu lạc bộ, Cầu thủ sẽ không được phép thay đổi nữa!\n" +
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
            if (parameter.league.countTeam % 2 == 1)
            {
                MessageBox.Show("Số đội bóng tham gia giải đấu phải là số chẵn!", "Lưu ý", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            List<Team> teams = TeamDAO.Instance.GetListTeamInLeague(parameter.league.id);

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

        #region Setting (Quy Định)

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
                parameter.tbNumberOfTeamsIn.Text = parameter.setting.NumberOfTeamIn.ToString();
            }
        }
        public void LoadTypesOfGoal(MainWindow parameter)
        {
            List<TypeOfGoal> data = new List<TypeOfGoal>();
            data = TypeOfGoalDAO.Instance.GetListTypeOfGoal(parameter.league.id);
            int i = 1;
            foreach (TypeOfGoal typeG in data.ToArray())
            {
                typeG.id = i;
                i++;
            }
            parameter.dgvTypeOfGoal.ItemsSource = data;
        }
        public void OpenEditDialogWindow(MainWindow parameter)
        {
            int index = Int32.Parse(idSetting);
            int idTournament = parameter.league.id;
            Setting curSetting = parameter.setting;
            EditDialogWindow wd = new EditDialogWindow(idTournament, index, curSetting);
            wd.ShowDialog();
            parameter.setting = SettingDAO.Instance.GetSetting(idTournament);
            GetDetailSetting(parameter);
        }
        public void OpenAddGoalTypeWindow(MainWindow parameter)
        {
            AddGoalTypeWindow wd = new AddGoalTypeWindow(parameter, "");
            wd.ShowDialog();
            LoadTypesOfGoal(parameter);
        }
        public void OpenEditGoalTypeWindow(MainWindow parameter)
        {
            if (parameter.dgvTypeOfGoal.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn loại bàn thắng cần sửa");
                return;
            }
            string name = (parameter.dgvTypeOfGoal.SelectedItem as TypeOfGoal).displayName;
            AddGoalTypeWindow wd = new AddGoalTypeWindow(parameter, name);
            wd.Title = "Sửa thông tin";
            wd.btnAdd.Content = "Xác nhận";
            wd.ShowDialog();
            LoadTypesOfGoal(parameter);
        }
        public void DeleteGoalType(MainWindow parameter)
        {
            if (parameter.dgvTypeOfGoal.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn loại bàn thắng cần xoá");
                return;
            }
            string name = (parameter.dgvTypeOfGoal.SelectedItem as TypeOfGoal).displayName;
            if (MessageBox.Show("Bạn có muốn xoá loại bàn thắng \"" + name + "\"", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                return;
            }
            else
            {
                try
                {
                    TypeOfGoalDAO.Instance.DeleteTypeGoal(parameter.league.id, name);
                    MessageBox.Show("Xoá loại bàn thắng thành công");
                }
                catch
                {
                    MessageBox.Show("Lỗi kết nối");
                }
                LoadTypesOfGoal(parameter);
            }
        }
        #endregion

        #region Ranking
        const string Pts = "Điểm";
        const string GD = "Hiệu số bàn thắng";
        const string Vs = "Hiệu số đối đầu";
        const string Fg = "Tổng số bàn thắng";
        const string Wn = "Tổng số trận thắng";
        string pathProject;

        System.Drawing.Image imgWin;
        System.Drawing.Image imgDraw;
        System.Drawing.Image imgLose;
        System.Drawing.Image imgEmpty;

        void InitCbbRanking(MainWindow parameter)
        {
            //Load Board
            parameter.cbSelectedGroupsStanding.Items.Clear();
            foreach (Board board in parameter.boards)
            {
                parameter.cbSelectedGroupsStanding.Items.Add(board.nameBoard);
            }
            if (parameter.boards.Count > 1)
                parameter.cbSelectedGroupsStanding.Visibility = Visibility.Visible;
            else
                parameter.cbSelectedGroupsStanding.Visibility = Visibility.Hidden;
            if (parameter.boards.Count >= 0)
                parameter.cbSelectedGroupsStanding.SelectedIndex = 0;
        }
        void LoadRanking(MainWindow parameter)
        {
            //Get Image
            pathProject = System.IO.Directory.GetCurrentDirectory();
            try
            {
                while (!File.Exists(pathProject + @"\Resource\Images\imgEmpty.png") && System.IO.Directory.GetParent(pathProject) != null)
                {
                    pathProject = System.IO.Directory.GetParent(pathProject).ToString();
                }
                imgWin = System.Drawing.Image.FromFile(pathProject + @"\Resource\Images\imgWin.png");
                imgDraw = System.Drawing.Image.FromFile(pathProject + @"\Resource\Images\imgDraw.png");
                imgLose = System.Drawing.Image.FromFile(pathProject + @"\Resource\Images\imgLose.png");
                imgEmpty = System.Drawing.Image.FromFile(pathProject + @"\Resource\Images\imgEmpty.png");
            }
            catch
            {
                MessageBox.Show("Không tìm thấy file, vui lòng liên hệ hỗ trợ hoặc cài đặt lại phần mềm");
                return;
            }
            ///Calc
            try
            {
                int i = -1;
                if (parameter.league != null)
                {
                    teams = TeamDAO.Instance.GetListTeamInLeague(parameter.league.id);
                }
                else
                    return;
                GetDetailSetting(parameter);
                List<TeamScoreDetails> rank = new List<TeamScoreDetails>();
                if (parameter.cbSelectedGroupsStanding.SelectedItem == null)
                    return;
                foreach (Team t in teams)
                {
                    if (t.nameBoard != parameter.cbSelectedGroupsStanding.SelectedItem.ToString())
                        continue;
                    BitmapImage logoteam = ImageProcessing.Instance.Convert(ImageProcessing.Instance.ByteToImg(t.logo));
                    rank.Add(new TeamScoreDetails(t.nameTeam, logoteam));
                    i++;
                    List<Match> matches = MatchDAO.Instance.GetListMatchByIDTeamWithOrder(t.idTournamnt, t.id);

                    //Tính 
                    foreach (Match m in matches)
                    {
                        int gF = 0; //Bàn thắng
                        int gA = 0; //Bàn thua
                        List<Goal> goals = GoalDAO.Instance.GetListGoalsByIDMatch(m.id);
                        foreach (Goal g in goals)
                        {
                            if (g.idTeams == t.id) //ghi bàn
                                gF++;
                            else
                                gA++;
                        }
                        // + điểm
                        if (gF > gA)
                            rank[i].pts += parameter.setting.scoreWin;
                        if (gF == gA)
                            rank[i].pts += parameter.setting.scoreDraw;
                        if (gF < gA)
                            rank[i].pts += parameter.setting.scoreLose;

                        rank[i].CalcDetails(gF, gA); //Tính hiệu số

                    }

                    rank[i].imageFLM = ImageProcessing.Instance.Convert(ImageProcessing.Instance.ByteToImg(ImageProcessing.Instance.convertImgToByte(ResToImageFLM(rank[i].fLM))));
                }

                //Sort Rank
                CaculateRanking(parameter.league.id, rank);

                parameter.dgvRanking.ItemsSource = rank;
            }
            catch
            {
                MessageBox.Show("Lỗi kết nối dữ liệu");
                return;
            }
        }

        void CaculateRanking(int idTournament, List<TeamScoreDetails> listTeamScoreDetails)
        {
            List<string> listPriorRank = new List<string>();
            listPriorRank.Add(Pts);
            listPriorRank.Add(GD);
            listPriorRank.Add(Vs);
            listPriorRank.Add(Fg);
            listPriorRank.Add(Wn);
            TeamScoreDetails tmp;
            int index = listTeamScoreDetails.Count;
            for (int i = 0; i < index - 1; i++)
                for (int j = i + 1; j < index; j++)
                {
                    foreach (string condition in listPriorRank)
                    {
                        int res = CheckCondition(condition, listTeamScoreDetails[i], listTeamScoreDetails[j], idTournament);
                        if (res == 0)
                            continue;
                        if (res == 2)
                        {
                            tmp = listTeamScoreDetails[i];
                            listTeamScoreDetails[i] = listTeamScoreDetails[j];
                            listTeamScoreDetails[j] = tmp;
                        }
                        break;
                    }
                }
            for (int i = 0; i < listTeamScoreDetails.Count; i++)
                listTeamScoreDetails[i].rankTeam = i + 1;
        }
        int CheckCondition(string condition, TeamScoreDetails team1, TeamScoreDetails team2, int idTournament)
        {
            switch (condition)
            {
                case Pts:
                    if (team1.pts == team2.pts)
                        return 0;
                    if (team1.pts < team2.pts)
                        return 2;
                    break;
                case GD:
                    if (team1.gD == team2.gD)
                        return 0;
                    if (team1.gD < team2.gD)
                        return 2;
                    break;
                case Fg:
                    if (team1.f == team2.f)
                        return 0;
                    if (team1.f < team2.f)
                        return 2;
                    break;
                case Wn:
                    if (team1.w == team2.w)
                        return 0;
                    if (team1.w < team2.w)
                        return 2;
                    break;
                case Vs:
                    return CheckVersus(idTournament, team1.nameTeam, team2.nameTeam);
            }
            return 1;
        }  
        int CheckVersus(int idTournament, string nameTeam1, string nameTeam2)
        {
            int idTeam1 = TeamDAO.Instance.GetTeamIDByName(idTournament, nameTeam1);
            int idTeam2 = TeamDAO.Instance.GetTeamIDByName(idTournament, nameTeam2);
            List<Match> matchid1 = MatchDAO.Instance.GetListMatchByID2Team(idTournament, idTeam1, idTeam2);
            List<Match> matchid2 = MatchDAO.Instance.GetListMatchByID2Team(idTournament, idTeam2, idTeam1);
            int gFH1 = 0; //Bàn thắng sân nhà team 1
            int gFH2 = 0; //Bàn thắng sân nhà team 2
            int gAH1 = 0; //Bàn thua sân nhà team 1
            int gAH2 = 0; //Bàn thua sân nhà team 2
            int W1 = 0; //Trận thắng team 1
            int W2 = 0; //Trận thắng team 2
            foreach (Match mid in matchid1) //Team 1 chủ nhà
            {
                int gF = 0;
                int gA = 0;
                List<Goal> goals = GoalDAO.Instance.GetListGoalsByIDMatch(mid.id);
                foreach (Goal g in goals)
                {
                    if (g.idTeams == idTeam1) //ghi bàn
                        gF++;
                    else
                        gA++;
                }
                if (gF > gA)
                {
                    W1++;
                }
                else
                {
                    W2++;
                }
                gFH1 += gF;
                gAH1 += gA;
            }
            foreach (Match mid in matchid2) //Team 2 chủ nhà
            {
                int gF = 0;
                int gA = 0;
                List<Goal> goals = GoalDAO.Instance.GetListGoalsByIDMatch(mid.id);
                foreach (Goal g in goals)
                {
                    if (g.idTeams == idTeam2) //ghi bàn
                        gF++;
                    else
                        gA++;
                }
                if (gF > gA)
                {
                    W2++;
                }
                else
                {
                    W1++;
                }
                gFH2 += gF;
                gAH2 += gA;
            }
            if (W1 < W2)
                return 2;
            if (W1 == W2)
            {
                if (gAH1 > gAH2)
                    return 2;
            }
            return 0;
        }
        System.Drawing.Image ResToImageFLM(string res)
        {
            int space = 2;
            int width = imgWin.Width * 6 + space * 7;
            int height = imgWin.Width + space * 2;

            System.Drawing.Bitmap imgres = new System.Drawing.Bitmap(width, height);
            Graphics g = Graphics.FromImage(imgres);

            for (int i = 0; i < 5; i++)
            {
                //get image
                Image imgResOfmatch = imgWin;
                if (i > res.Length - 1)
                    imgResOfmatch = imgEmpty;
                else
                {
                    if (res[i] == 'V')
                        imgResOfmatch = imgWin;
                    if (res[i] == '-')
                        imgResOfmatch = imgDraw;
                    if (res[i] == 'X')
                        imgResOfmatch = imgLose;
                }
                //draw image
                g.DrawImage(imgResOfmatch, new System.Drawing.Point(i * height + space, 2));
            }

            g.Dispose();

            return imgres;
        }
        #endregion
    }
}
