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
using iTextSharp.text.pdf;
using iTextSharp.text;
using Microsoft.Win32;
using System.Collections;
using System.Windows.Controls;

namespace FCM.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        #region Command
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
        public ICommand ExportRankingBoardCommand { get; set; }

        public ICommand CancelCreateScheduleCommand { get; set; }
        public ICommand CreateScheduleNockOutCommand { get; set; }
        public ICommand ViewScheduleNockOutCommand { get; set; }

        //public ICommand OpenEditMatchWindowCommand { get; set; }
        //public ICommand OpenResultRecordWindowCommand { get; set; }

        #endregion

        #region Init
        public int round = 0;

        public string uid;

        public string idSetting = "";

        bool isClickExportRanking = false;

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

            OpenEditDialogCommand = new RelayCommand<MainWindow>((parameter) => true, (parameter) => OpenEditDialogWindow(parameter));
            OpenEditLeagueWindowCommand = new RelayCommand<MainWindow>((parameter) => true, (parameter) => OpenEditLeagueWindow(parameter));
            OpenAddTeamWindowCommand = new RelayCommand<MainWindow>((parameter) => true, (parameter) => OpenAddTeamWindow(parameter));
            OpenEditTeamWindowCommand = new RelayCommand<MainWindow>((parameter) => true, (parameter) => OpenEditTeamWindow(parameter));
            OpenAddPlayerWindowCommand = new RelayCommand<MainWindow>((parameter) => true, (parameter) => OpenAddPlayerWindow(parameter));
            OpenAddGoalTypeCommand = new RelayCommand<MainWindow>((parameter) => true, (parameter) => OpenAddGoalTypeWindow(parameter));
            OpenEditGoalTypeCommand = new RelayCommand<MainWindow>((parameter) => true, (parameter) => OpenEditGoalTypeWindow(parameter));
            DeleteGoalTypeCommand = new RelayCommand<MainWindow>((parameter) => true, (parameter) => DeleteGoalType(parameter));
            OpenChangePasswordCommand = new RelayCommand<MainWindow>((parameter) => true, (parameter) => OpenChangePasswordWindow(parameter));
            OpenLoginCommand = new RelayCommand<MainWindow>((parameter) => true, (parameter) => OpenLoginWindow(parameter));
            SearchLeagueCommand = new RelayCommand<MainWindow>((parameter) => true, (parameter) => SearchLeague(parameter));

            CreateScheduleCommand = new RelayCommand<MainWindow>((parameter) => true, (parameter) => CreateSchedule(parameter));
            ChangeRoundCommand = new RelayCommand<MainWindow>((parameter) => true, (parameter) => ChangeCbxRound(parameter));

            ExportTeamCommand = new RelayCommand<MainWindow>((parameter) => true, (parameter) => ExportTeam(parameter));
            ChangeBoardCommand = new RelayCommand<MainWindow>((parameter) => true, (parameter) => SearchBoard(parameter));
            ChangeRankingBoardCommand = new RelayCommand<MainWindow>((parameter) => true, (parameter) => LoadRanking(parameter));
            ExportRankingBoardCommand = new RelayCommand<MainWindow>((parameter) => true, (parameter) => ExportRanking(parameter));

            CancelCreateScheduleCommand = new RelayCommand<MainWindow>((parameter) => true, (parameter) => OpenScheduleMatch(parameter));
            CreateScheduleNockOutCommand = new RelayCommand<MainWindow>((parameter) => true, (parameter) => CreateScheduleNockOut(parameter));
            ViewScheduleNockOutCommand = new RelayCommand<MainWindow>((parameter) => true, (parameter) => ViewSchedule(parameter));

            //OpenEditMatchWindowCommand = new RelayCommand<MainWindow>((parameter) => true, (parameter) => OpenEditMatchInfoWindow(parameter));
            //OpenResultRecordWindowCommand = new RelayCommand<MainWindow>((parameter) => true, (parameter) => OpenResultRecordingWindow(parameter));
            GetImageResultMatch();
        }
        #endregion

        #region Tab
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
                    }
                    else
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
                    GetDetailSetting(parameter);
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
        #endregion

        #region League
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
        void ExportTeam(MainWindow parameter)
        {
            if (parameter.team != null)
            {
                ExcelProcessing.Instance.ExportFile(parameter.team);
            }
        }
        public void LoadListTeams(MainWindow parameter)
        {

            if (parameter.league != null)
            {
                teams = TeamDAO.Instance.GetListTeamInLeague(parameter.league.id);
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
                    {
                        //LoadDetailTeam(parameter, teams[0]);
                    }
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
        public void GetBoards(MainWindow mainWindow)
        {
            mainWindow.cbGroup.Items.Clear();
            mainWindow.cbGroup.Items.Add("Tất cả");
            mainWindow.cbGroup.SelectedItem = 0;
            mainWindow.cbGroup.SelectedIndex = 0;
            foreach (Board board in mainWindow.boards)
            {
                mainWindow.cbGroup.Items.Add(board.nameBoard);
            }
        }
        public void SearchBoard(MainWindow mainWindow)
        {
            if (mainWindow.cbGroup.SelectedItem != null)
                ShowTeam(mainWindow, mainWindow.cbGroup.SelectedItem.ToString());

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
                if (TeamDAO.Instance.GetListTeamInLeague(parameter.league.id).Count == 0)
                {
                    if (MessageBox.Show("Sau khi tạo đội bóng đầu tiên sẽ không thể thay đổi quy định của giải nữa \nXác nhận tạo đội?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
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
                if (parameter.team != null)
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

        #region Account
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
        #endregion

        #region NockOut

        void CreateBoardKnockOut(MainWindow parameter)
        {
            int cntBoard = parameter.cbSelectedGroupsStanding.Items.Count;
            if (cntBoard == 0)
                return;
            GetDetailSetting(parameter);
            int teamIn = parameter.setting.NumberOfTeamIn;
            int teamPerGroupIn = teamIn / cntBoard;
            int slotLeft = teamIn - teamPerGroupIn * cntBoard;
            List<string> listName = new List<string>();
            List<TeamScoreDetails> listTeamCalc = new List<TeamScoreDetails>();

            try
            {
                //Get list top
                for (int i = 0; i < cntBoard; i++)
                {
                    string nameBoard = parameter.cbSelectedGroupsStanding.Items[i].ToString();
                    List<TeamScoreDetails> list = CalcDetails(parameter, nameBoard);
                    list = CalcRanking(parameter.league.id, list);
                    for (int ii = 0; ii < teamPerGroupIn; ii++)
                    {
                        listName.Add(list[ii].nameTeam);
                    }
                    if (list.Count > teamPerGroupIn)
                        listTeamCalc.Add(list[teamPerGroupIn]);
                }
                //Get list left
                if (slotLeft > 0)
                {
                    listTeamCalc = CalcRanking(parameter.league.id, listTeamCalc);
                    for (int i = 0; i < slotLeft; i++)
                        listName.Add(listTeamCalc[i].nameTeam);
                }
                //Add to board
                BoardDAO.Instance.DeleteKOBoard(parameter.league.id);
                for (int i = 0; i < listName.Count; i++)
                {
                    int idTeam = TeamDAO.Instance.GetTeamIDByName(parameter.league.id, listName[i]);
                    BoardDAO.Instance.AddToKOBoard(parameter.league.id, idTeam);
                }
                MessageBox.Show("Tạo danh sách vào vòng loại trực tiếp thành công");
            }
            catch
            {
                MessageBox.Show("Lỗi kết nối dữ liệu");
            }
        }
        public void OpenScheduleMatch(MainWindow mainWindow)
        {
            mainWindow.grdScheduleScreen.Visibility = Visibility.Visible;
            mainWindow.grdScheduleChart.Visibility = Visibility.Hidden;
            LoadListMatch(mainWindow, 0);
        }
        public void CreateScheduleNockOut(MainWindow mainWindow)
        {
            if (mainWindow.btnCancelCreateScheduleChart.Visibility == Visibility.Hidden)
            {
                OpenScheduleMatch(mainWindow);
                return;
            }
            int size = 0;
            if (mainWindow.league.countTeam <= 16)
                size = 16;
            if (mainWindow.league.countTeam <= 8)
                size = 8;
            if (mainWindow.league.countTeam <= 4)
                size = 4;
            //try
            //{
            switch (size)
            {
                case 4:
                    List<int> index4 = new List<int>();
                    int count4 = 0;
                    index4.Add(mainWindow.cb4Team1.SelectedIndex);
                    index4.Add(mainWindow.cb4Team2.SelectedIndex);
                    index4.Add(mainWindow.cb4Team3.SelectedIndex);
                    index4.Add(mainWindow.cb4Team4.SelectedIndex);
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < i; j++)
                        {
                            if (index4[i] != 0 && index4[i] == index4[j])
                            {
                                MessageBox.Show("Trùng đội bóng");
                                return;
                            }
                        }
                        if (index4[i] != 0)
                            count4++;
                    }
                    // MessageBox.Show(count4.ToString());
                    if (count4 < mainWindow.league.countTeam)
                    {
                        MessageBox.Show("Thiếu đội bóng");
                        return;
                    }
                    else
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            if (index4[i] != 0)
                            {
                                index4[i] = teamsInNockOut[index4[i] - 1].id;
                            }
                        }
                        TreeMatch treeMatch = new TreeMatch(mainWindow.league.id, 4, index4);
                        TreeMatchDAO.Instance.CreateTreeMatch(treeMatch);
                        NodeMatchDAO.Instance.SetIdTree(TreeMatchDAO.Instance.GetNewestIdTreeNode());
                    }
                    break;
                case 8:
                    List<int> index8 = new List<int>();
                    int count8 = 0;
                    index8.Add(mainWindow.cb8Team1.SelectedIndex);
                    index8.Add(mainWindow.cb8Team2.SelectedIndex);
                    index8.Add(mainWindow.cb8Team3.SelectedIndex);
                    index8.Add(mainWindow.cb8Team4.SelectedIndex);
                    index8.Add(mainWindow.cb8Team5.SelectedIndex);
                    index8.Add(mainWindow.cb8Team6.SelectedIndex);
                    index8.Add(mainWindow.cb8Team7.SelectedIndex);
                    index8.Add(mainWindow.cb8Team8.SelectedIndex);
                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 0; j < i; j++)
                        {
                            if (index8[i] != 0 && index8[i] == index8[j])
                            {
                                MessageBox.Show("Trùng đội bóng");
                                return;
                            }
                        }
                        if (index8[i] != 0)
                            count8++;
                    }
                     MessageBox.Show(count8.ToString());
                    if (count8 < mainWindow.league.countTeam)
                    {
                        MessageBox.Show("Thiếu đội bóng");
                        return;
                    }
                    else
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            if (index8[i] > 0)
                            {
                                index8[i] = teamsInNockOut[index8[i] - 1].id;
                            }
                        }
                        TreeMatch treeMatch = new TreeMatch(mainWindow.league.id, 8, index8);
                        TreeMatchDAO.Instance.CreateTreeMatch(treeMatch);
                        NodeMatchDAO.Instance.SetIdTree(TreeMatchDAO.Instance.GetNewestIdTreeNode());
                    }
                    break;
                case 16:
                    List<int> index16 = new List<int>();
                    int count16 = 0;
                    index16.Add(mainWindow.cb16Team1.SelectedIndex);
                    index16.Add(mainWindow.cb16Team2.SelectedIndex);
                    index16.Add(mainWindow.cb16Team3.SelectedIndex);
                    index16.Add(mainWindow.cb16Team4.SelectedIndex);
                    index16.Add(mainWindow.cb16Team5.SelectedIndex);
                    index16.Add(mainWindow.cb16Team6.SelectedIndex);
                    index16.Add(mainWindow.cb16Team7.SelectedIndex);
                    index16.Add(mainWindow.cb16Team8.SelectedIndex);
                    index16.Add(mainWindow.cb16Team9.SelectedIndex);
                    index16.Add(mainWindow.cb16Team10.SelectedIndex);
                    index16.Add(mainWindow.cb16Team11.SelectedIndex);
                    index16.Add(mainWindow.cb16Team12.SelectedIndex);
                    index16.Add(mainWindow.cb16Team13.SelectedIndex);
                    index16.Add(mainWindow.cb16Team14.SelectedIndex);
                    index16.Add(mainWindow.cb16Team15.SelectedIndex);
                    index16.Add(mainWindow.cb16Team16.SelectedIndex);
                    for (int i = 0; i < 16; i++)
                    {
                        for (int j = 0; j < i; j++)
                        {
                            if (index16[i] != 0 && index16[i] == index16[j])
                            {
                                MessageBox.Show("Trùng đội bóng");
                                return;
                            }
                        }
                        if (index16[i] != 0)
                            count16++;
                    }
                    // MessageBox.Show(count8.ToString());
                    if (count16 < mainWindow.league.countTeam)
                    {
                        MessageBox.Show("Thiếu đội bóng");
                        return;
                    }
                    else
                    {
                        for (int i = 0; i < 16; i++)
                        {
                            if (index16[i] != 0)
                            {
                                index16[i] = teamsInNockOut[index16[i] - 1].id;
                            }
                        }
                        TreeMatch treeMatch = new TreeMatch(mainWindow.league.id, 16, index16);
                        TreeMatchDAO.Instance.CreateTreeMatch(treeMatch);
                        NodeMatchDAO.Instance.SetIdTree(TreeMatchDAO.Instance.GetNewestIdTreeNode());
                    }
                    break;
            }
            OpenScheduleMatch(mainWindow);
            //}
            //catch
            //{
            //    MessageBox.Show("Chưa chọn đủ đội bóng");
            //    return;
            //}
        }
        public void ViewSchedule(MainWindow mainWindow)
        {
            TreeMatch treeMatch = TreeMatchDAO.Instance.GetTree(mainWindow.league.id);
            int size = 0;
            if (mainWindow.league.countTeam <= 16)
                size = 16;
            if (mainWindow.league.countTeam <= 8)
                size = 8;
            if (mainWindow.league.countTeam <= 4)
                size = 4;
            List<ComboBox> comboBoxes = new List<ComboBox>();
            List<TextBlock> textBlockes = new List<TextBlock>();
            switch (size)
            {
                case 4:
                    comboBoxes.Add(mainWindow.cb4Team1);
                    comboBoxes.Add(mainWindow.cb4Team2);
                    comboBoxes.Add(mainWindow.cb4Team3);
                    comboBoxes.Add(mainWindow.cb4Team4);
                    textBlockes.Add(mainWindow.tbl4Team1);
                    textBlockes.Add(mainWindow.tbl4Team2);
                    textBlockes.Add(mainWindow.tbl4Team3);
                    break;
                case 8:
                    comboBoxes.Add(mainWindow.cb8Team1);
                    comboBoxes.Add(mainWindow.cb8Team2);
                    comboBoxes.Add(mainWindow.cb8Team3);
                    comboBoxes.Add(mainWindow.cb8Team4);
                    comboBoxes.Add(mainWindow.cb8Team5);
                    comboBoxes.Add(mainWindow.cb8Team6);
                    comboBoxes.Add(mainWindow.cb8Team7);
                    comboBoxes.Add(mainWindow.cb8Team8);
                    textBlockes.Add(mainWindow.tbl8Team1);
                    textBlockes.Add(mainWindow.tbl8Team2);
                    textBlockes.Add(mainWindow.tbl8Team5);
                    textBlockes.Add(mainWindow.tbl8Team3);
                    textBlockes.Add(mainWindow.tbl8Team4);
                    textBlockes.Add(mainWindow.tbl8Team6);
                    textBlockes.Add(mainWindow.tbl8Team7);
                    break;
                case 16:
                    comboBoxes.Add(mainWindow.cb16Team1);
                    comboBoxes.Add(mainWindow.cb16Team2);
                    comboBoxes.Add(mainWindow.cb16Team3);
                    comboBoxes.Add(mainWindow.cb16Team4);
                    comboBoxes.Add(mainWindow.cb16Team5);
                    comboBoxes.Add(mainWindow.cb16Team6);
                    comboBoxes.Add(mainWindow.cb16Team7);
                    comboBoxes.Add(mainWindow.cb16Team8);
                    comboBoxes.Add(mainWindow.cb16Team9);
                    comboBoxes.Add(mainWindow.cb16Team10);
                    comboBoxes.Add(mainWindow.cb16Team11);
                    comboBoxes.Add(mainWindow.cb16Team12);
                    comboBoxes.Add(mainWindow.cb16Team13);
                    comboBoxes.Add(mainWindow.cb16Team14);
                    comboBoxes.Add(mainWindow.cb16Team15);
                    comboBoxes.Add(mainWindow.cb16Team16);
                    textBlockes.Add(mainWindow.tbl16Team1);
                    textBlockes.Add(mainWindow.tbl16Team2);
                    textBlockes.Add(mainWindow.tbl16Team9);
                    textBlockes.Add(mainWindow.tbl16Team3);
                    textBlockes.Add(mainWindow.tbl16Team4);
                    textBlockes.Add(mainWindow.tbl16Team10);
                    textBlockes.Add(mainWindow.tbl16Team13);
                    textBlockes.Add(mainWindow.tbl16Team5);
                    textBlockes.Add(mainWindow.tbl16Team6);
                    textBlockes.Add(mainWindow.tbl16Team11);
                    textBlockes.Add(mainWindow.tbl16Team7);
                    textBlockes.Add(mainWindow.tbl16Team8);
                    textBlockes.Add(mainWindow.tbl16Team12);
                    textBlockes.Add(mainWindow.tbl16Team14);
                    textBlockes.Add(mainWindow.tbl16Team15);
                    break;
            }
            TreeMatch tree = TreeMatchDAO.Instance.GetTree(mainWindow.league.id);
            NodeMatch nodeMatch = NodeMatchDAO.Instance.GetNodeById(tree.idFirstNode);
            LoadNodeToScreen(nodeMatch, comboBoxes, textBlockes);
            mainWindow.btnCancelCreateScheduleChart.Visibility = Visibility.Hidden;
            mainWindow.btnScheduleChart.Content = "Xem lịch thi đấu";
            switch (size)
            {
                case 4:
                    mainWindow.grdScheduleScreen.Visibility = Visibility.Hidden;
                    mainWindow.grdScheduleChart.Visibility = Visibility.Visible;
                    mainWindow.grdSchedule4.Visibility = Visibility.Visible;
                    mainWindow.grdSchedule8.Visibility = Visibility.Hidden;
                    mainWindow.grdSchedule16.Visibility = Visibility.Hidden;
                    break;
                case 8:
                    mainWindow.grdScheduleScreen.Visibility = Visibility.Hidden;
                    mainWindow.grdScheduleChart.Visibility = Visibility.Visible;
                    mainWindow.grdSchedule4.Visibility = Visibility.Hidden;
                    mainWindow.grdSchedule8.Visibility = Visibility.Visible;
                    mainWindow.grdSchedule16.Visibility = Visibility.Hidden;
                    break;
                case 16:
                    mainWindow.grdScheduleScreen.Visibility = Visibility.Hidden;
                    mainWindow.grdScheduleChart.Visibility = Visibility.Visible;
                    mainWindow.grdSchedule4.Visibility = Visibility.Hidden;
                    mainWindow.grdSchedule8.Visibility = Visibility.Hidden;
                    mainWindow.grdSchedule16.Visibility = Visibility.Visible;
                    break;
            }
        }
        void LoadNodeToScreen(NodeMatch nodeMatch, List<ComboBox> comboBoxes, List<TextBlock> textBlocks)
        {
            if (nodeMatch.idNodeLeft == -1 && nodeMatch.idNodeRight == -1)
            {
                if (nodeMatch.idTeam == 0)
                {
                    if (comboBoxes.Count > 0)
                    {
                        comboBoxes[0].Items.Clear();
                        comboBoxes[0].Items.Add("Ưu tiên");
                        comboBoxes[0].SelectedIndex = 0;
                        comboBoxes[0].IsEnabled = false;
                        comboBoxes.RemoveAt(0);
                    }

                }
                else
                {
                    if (comboBoxes.Count > 0)
                    {
                        comboBoxes[0].Items.Clear();
                        Team team = TeamDAO.Instance.GetTeamById(nodeMatch.idTeam);
                        comboBoxes[0].Items.Add(team.nameTeam);
                        comboBoxes[0].SelectedIndex = 0;
                        comboBoxes[0].IsEnabled = false;
                        comboBoxes.RemoveAt(0);
                    }

                }
            }
            else
            {
                LoadNodeToScreen(NodeMatchDAO.Instance.GetNodeById(nodeMatch.idNodeLeft), comboBoxes, textBlocks);
                LoadNodeToScreen(NodeMatchDAO.Instance.GetNodeById(nodeMatch.idNodeRight), comboBoxes, textBlocks);
                if (nodeMatch.idTeam == -1)
                {
                    if (textBlocks.Count > 0)
                    {
                        textBlocks[0].Text = "";
                        textBlocks.RemoveAt(0);
                    }
                }
                else
                if (nodeMatch.idTeam == 0)
                {
                    if (textBlocks.Count > 0)
                    {
                        textBlocks[0].Text = "Ưu tiên";
                        textBlocks.RemoveAt(0);
                    }
                }
                else
                {
                    if (textBlocks.Count > 0)
                    {
                        Team team = TeamDAO.Instance.GetTeamById(nodeMatch.idTeam);
                        textBlocks[0].Text = team.nameTeam;
                        textBlocks.RemoveAt(0);
                    }
                }
            }

        }
        List<Team> teamsInNockOut = new List<Team>();
        void OpenNockOutScreen(MainWindow parameter)
        {
            teamsInNockOut = TeamDAO.Instance.GetListTeam("Bảng đấu loại trực tiếp", parameter.league.id);
            parameter.btnScheduleChart.Content = "Tạo lịch thi đấu";
            parameter.btnCancelCreateScheduleChart.Visibility = Visibility.Visible;
            parameter.btnCancelCreateScheduleChart.Content = "Hủy";
            int size = 0;
            if (parameter.league.countTeam <= 16)
                size = 16;
            if (parameter.league.countTeam <= 8)
                size = 8;
            if (parameter.league.countTeam <= 4)
                size = 4;
            switch (size)
            {
                case 4:
                    parameter.grdScheduleScreen.Visibility = Visibility.Hidden;
                    parameter.grdScheduleChart.Visibility = Visibility.Visible;
                    parameter.grdSchedule4.Visibility = Visibility.Visible;
                    parameter.grdSchedule8.Visibility = Visibility.Hidden;
                    parameter.grdSchedule16.Visibility = Visibility.Hidden;
                    parameter.cb4Team1.Items.Clear(); parameter.cb4Team1.Items.Add("Ưu tiên");
                    parameter.cb4Team2.Items.Clear(); parameter.cb4Team2.Items.Add("Ưu tiên");
                    parameter.cb4Team3.Items.Clear(); parameter.cb4Team3.Items.Add("Ưu tiên");
                    parameter.cb4Team4.Items.Clear(); parameter.cb4Team4.Items.Add("Ưu tiên");
                    parameter.tbl4Team1.Text = "";
                    parameter.tbl4Team2.Text = "";
                    parameter.tbl4Team3.Text = "";
                    foreach (Team team in teamsInNockOut)
                    {
                        parameter.cb4Team1.Items.Add(team.nameTeam);
                        parameter.cb4Team2.Items.Add(team.nameTeam);
                        parameter.cb4Team3.Items.Add(team.nameTeam);
                        parameter.cb4Team4.Items.Add(team.nameTeam);
                    }
                    break;
                case 8:
                    parameter.grdScheduleScreen.Visibility = Visibility.Hidden;
                    parameter.grdScheduleChart.Visibility = Visibility.Visible;
                    parameter.grdSchedule4.Visibility = Visibility.Hidden;
                    parameter.grdSchedule8.Visibility = Visibility.Visible;
                    parameter.grdSchedule16.Visibility = Visibility.Hidden;
                    parameter.cb8Team1.Items.Clear(); parameter.cb8Team1.Items.Add("Ưu tiên");
                    parameter.cb8Team2.Items.Clear(); parameter.cb8Team2.Items.Add("Ưu tiên");
                    parameter.cb8Team3.Items.Clear(); parameter.cb8Team3.Items.Add("Ưu tiên");
                    parameter.cb8Team4.Items.Clear(); parameter.cb8Team4.Items.Add("Ưu tiên");
                    parameter.cb8Team5.Items.Clear(); parameter.cb8Team5.Items.Add("Ưu tiên");
                    parameter.cb8Team6.Items.Clear(); parameter.cb8Team6.Items.Add("Ưu tiên");
                    parameter.cb8Team7.Items.Clear(); parameter.cb8Team7.Items.Add("Ưu tiên");
                    parameter.cb8Team8.Items.Clear(); parameter.cb8Team8.Items.Add("Ưu tiên");
                    parameter.tbl8Team1.Text = "";
                    parameter.tbl8Team2.Text = "";
                    parameter.tbl8Team3.Text = "";
                    parameter.tbl8Team4.Text = "";
                    parameter.tbl8Team5.Text = "";
                    parameter.tbl8Team6.Text = "";
                    parameter.tbl8Team7.Text = "";
                    foreach (Team team in teamsInNockOut)
                    {
                        parameter.cb8Team1.Items.Add(team.nameTeam);
                        parameter.cb8Team2.Items.Add(team.nameTeam);
                        parameter.cb8Team3.Items.Add(team.nameTeam);
                        parameter.cb8Team4.Items.Add(team.nameTeam);
                        parameter.cb8Team5.Items.Add(team.nameTeam);
                        parameter.cb8Team6.Items.Add(team.nameTeam);
                        parameter.cb8Team7.Items.Add(team.nameTeam);
                        parameter.cb8Team8.Items.Add(team.nameTeam);
                    }
                    break;
                case 16:
                    parameter.grdScheduleScreen.Visibility = Visibility.Hidden;
                    parameter.grdScheduleChart.Visibility = Visibility.Visible;
                    parameter.grdSchedule4.Visibility = Visibility.Hidden;
                    parameter.grdSchedule8.Visibility = Visibility.Hidden;
                    parameter.grdSchedule16.Visibility = Visibility.Visible;
                    parameter.cb16Team1.Items.Clear(); parameter.cb16Team1.Items.Add("Ưu tiên");
                    parameter.cb16Team2.Items.Clear(); parameter.cb16Team2.Items.Add("Ưu tiên");
                    parameter.cb16Team3.Items.Clear(); parameter.cb16Team3.Items.Add("Ưu tiên");
                    parameter.cb16Team4.Items.Clear(); parameter.cb16Team4.Items.Add("Ưu tiên");
                    parameter.cb16Team5.Items.Clear(); parameter.cb16Team5.Items.Add("Ưu tiên");
                    parameter.cb16Team6.Items.Clear(); parameter.cb16Team6.Items.Add("Ưu tiên");
                    parameter.cb16Team7.Items.Clear(); parameter.cb16Team7.Items.Add("Ưu tiên");
                    parameter.cb16Team8.Items.Clear(); parameter.cb16Team8.Items.Add("Ưu tiên");
                    parameter.cb16Team9.Items.Clear(); parameter.cb16Team9.Items.Add("Ưu tiên");
                    parameter.cb16Team10.Items.Clear(); parameter.cb16Team10.Items.Add("Ưu tiên");
                    parameter.cb16Team11.Items.Clear(); parameter.cb16Team11.Items.Add("Ưu tiên");
                    parameter.cb16Team12.Items.Clear(); parameter.cb16Team12.Items.Add("Ưu tiên");
                    parameter.cb16Team13.Items.Clear(); parameter.cb16Team13.Items.Add("Ưu tiên");
                    parameter.cb16Team14.Items.Clear(); parameter.cb16Team14.Items.Add("Ưu tiên");
                    parameter.cb16Team15.Items.Clear(); parameter.cb16Team15.Items.Add("Ưu tiên");
                    parameter.cb16Team16.Items.Clear(); parameter.cb16Team16.Items.Add("Ưu tiên");
                    parameter.tbl16Team1.Text = "";
                    parameter.tbl16Team2.Text = "";
                    parameter.tbl16Team3.Text = "";
                    parameter.tbl16Team4.Text = "";
                    parameter.tbl16Team5.Text = "";
                    parameter.tbl16Team6.Text = "";
                    parameter.tbl16Team7.Text = "";
                    parameter.tbl16Team8.Text = "";
                    parameter.tbl16Team9.Text = "";
                    parameter.tbl16Team10.Text = "";
                    parameter.tbl16Team11.Text = "";
                    parameter.tbl16Team12.Text = "";
                    parameter.tbl16Team13.Text = "";
                    parameter.tbl16Team14.Text = "";
                    parameter.tbl16Team15.Text = "";
                    foreach (Team team in teamsInNockOut)
                    {
                        parameter.cb16Team1.Items.Add(team.nameTeam);
                        parameter.cb16Team2.Items.Add(team.nameTeam);
                        parameter.cb16Team3.Items.Add(team.nameTeam);
                        parameter.cb16Team4.Items.Add(team.nameTeam);
                        parameter.cb16Team5.Items.Add(team.nameTeam);
                        parameter.cb16Team6.Items.Add(team.nameTeam);
                        parameter.cb16Team7.Items.Add(team.nameTeam);
                        parameter.cb16Team8.Items.Add(team.nameTeam);
                        parameter.cb16Team9.Items.Add(team.nameTeam);
                        parameter.cb16Team10.Items.Add(team.nameTeam);
                        parameter.cb16Team11.Items.Add(team.nameTeam);
                        parameter.cb16Team12.Items.Add(team.nameTeam);
                        parameter.cb16Team13.Items.Add(team.nameTeam);
                        parameter.cb16Team14.Items.Add(team.nameTeam);
                        parameter.cb16Team15.Items.Add(team.nameTeam);
                        parameter.cb16Team16.Items.Add(team.nameTeam);
                    }
                    break;
            }
        }
        #endregion

        #region Tạo lịch thi đấu
        // Tạo lịch thi đấu
        public void CreateSchedule(MainWindow parameter)
        {
            //// Status = Đang đăng ký (Chưa đủ thông tin)
            //if (parameter.league.status == 0)
            //{
            //    MessageBox.Show("Giải đấu này đang trong tình trạng đăng ký!\nVui lòng cung cấp đầy đủ thông tin về đội bóng để tạo lịch thi đấu!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}
            //// Status = Đã bắt đầu (Đã bắt đầu)
            //if (parameter.league.status == 2)
            //{
            //    MessageBox.Show("Giải đấu này đã được bắt đầu!\nKhông thể tạo lịch thi đấu!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}

            // Tiến hành tạo lịch
            if (MessageBox.Show("Sau khi tiến hành tạo lịch, các thông tin về Câu lạc bộ, Cầu thủ sẽ không được phép thay đổi nữa!\n" +
                "Các trận đấu sẽ được tạo ngẫu nhiên theo nguyên tắc vòng tròn tính điểm.\n" +
                "Bạn có muốn tạo lịch thi đấu?", "Lưu ý", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {

                // trường hợp đấu vòng tròn
                if (parameter.league.typeLeague == 0)
                {
                    // Hàm tạo lịch theo Vòng tròn tính điểm
                    CreateScheduleWithCircle(parameter);
                }
                // trường hợp đấu bảng
                if (parameter.league.typeLeague == 2)
                {
                    // Hàm tạo lịch theo bảng đấu
                    CreateScheduleWithBoard(parameter);
                }

                // trường hợp Đấu loại
                if (parameter.league.typeLeague == 1)
                {
                    // Hàm tạo lịch theo bảng đấu
                    OpenNockOutScreen(parameter);
                }

                // Thay đổi status của giải đấu = 2 (Đã bắt đầu khởi tranh)
                LeagueDAO.Instance.UpdateStatusOfLeague(parameter.league.id, 2);

                MessageBox.Show("Tạo lịch thi đấu thành công!", "Thành công", MessageBoxButton.OK);

                LoadListMatch(parameter, 0);
            }
        }

        // Tạo lịch thi đấu vòng tròn tại mỗi bảng đấu (Cho trường hợp giải đấu chia bảng)
        public void CreateScheduleWithBoard(MainWindow parameter)
        {
            List<Board> boards = BoardDAO.Instance.GetListBoard(parameter.league.id);

            int nBoard = boards.Count;

            // Duyệt qua mỗi bảng đấu và tạo lịch
            for (int iBoard = 0; iBoard < nBoard; iBoard++)
            {
                List<Team> teams = TeamDAO.Instance.GetListTeam(boards[iBoard].nameBoard, parameter.league.id);
                int nTeams = teams.Count;

                bool[,] haveMet = new bool[nTeams, nTeams];
                // lưu trữ xem đội i đã từng đá với đội j (sân nhà đội i) hay chưa. have[i,j] = true : đã gặp nhau trên sân của i
                for (int i = 0; i < nTeams; i++) for (int j = 0; j < nTeams; j++) haveMet[i, j] = false;

                int nRound = nTeams % 2 == 0 ? (nTeams - 1) * 2 : nTeams * 2;

                bool[] isAttended = new bool[nTeams + 1];
                // Tại 1 vòng đấu nào đóm đội i nếu đã tham gia thì isAttended[i] = true;

                // Danh sách trận đấu
                Match[] matches = new Match[nRound * (nTeams / 2) + 1];
                int nMatch = 0;

                // Duyệt qua mỗi vòng đấu và tạo lịch
                for (int iRound = 1; iRound <= nRound / 2; iRound++)
                {
                    // Đánh dấu rằng: tại vòng đấu thứ round, tất cả các đội bóng đều chưa tham gia
                    for (int attended = 0; attended <= nTeams; attended++)
                    {
                        isAttended[attended] = false;
                    }

                    for (int iTeam1 = 0; iTeam1 < nTeams; iTeam1++)
                    {
                        if (!isAttended[iTeam1])        //  Nếu đội iTeam1 chưa tham gia trong vòng đấu này
                        {
                            for (int iTeam2 = iTeam1 + 1; iTeam2 < nTeams; iTeam2++)
                            {
                                if (!isAttended[iTeam2])    // Nếu đội iTeam2 chưa tham gia vòng đấu này
                                {
                                    if (!haveMet[iTeam1, iTeam2])
                                    {
                                        Match match = new Match(parameter.league.id, teams[iTeam1].id, teams[iTeam2].id, iRound, teams[iTeam1].stadium);

                                        matches[nMatch] = match;

                                        nMatch++;

                                        // đánh dấu lại iTeam1 và iTeam2 đã tham gia vòng đấu thứ iRound và lưu lại trận iTeam1 vs iTeam2 đã diễn ra
                                        // đồng thời thoát khỏi vòng lặp
                                        isAttended[iTeam1] = true;
                                        isAttended[iTeam2] = true;
                                        haveMet[iTeam1, iTeam2] = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                // Thêm vào database danh sách trận đấu (lượt đi)
                for (int iMatch = 0; iMatch < nMatch; iMatch++)
                {
                    Match match = matches[iMatch];
                    match.date = DateTime.Now;
                    match.time = DateTime.Now;

                    MatchDAO.Instance.AddMatch(match);
                }

                // Thêm vào database danh sách trận đấu (lượt về)
                for (int iMatch = 0; iMatch < nMatch; iMatch++)
                {
                    // Đổi ngược lại vị trí idTeam1 và idTeam2, đồng thời đổi cả sân đấu
                    string stadium = TeamDAO.Instance.GetTeamById(matches[iMatch].idTeam02).stadium;
                    Match match = new Match(parameter.league.id, matches[iMatch].idTeam02, matches[iMatch].idTeam01, matches[iMatch].round + nRound / 2, stadium);
                    match.date = DateTime.Now;
                    match.time = DateTime.Now;

                    MatchDAO.Instance.AddMatch(match);
                }
            }
        }

        // Tạo lịch thi đấu với cách vòng tròn tính điểm (Cho trường hợp giải đấu vòng tròn tính điểm)
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
                matches[i].date = DateTime.Now;
                matches[i].time = DateTime.Now;
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

        // Hủy kết quả trận đấu
        public void CancelResultMatch(MainWindow parameter, Match match)
        {
            ResultRecordingWindow resultWD = new ResultRecordingWindow(match);

            resultWD.DeleteOldInfor();

            LoadListMatch(parameter, parameter.cbxRound.SelectedIndex);
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

        void ExportRanking(MainWindow parameter)
        {
            //if (parameter.cbSelectedGroupsStanding.SelectedItem == null)
            //    return;
            //string nameBoard = "";
            //if (parameter.league.countBoard > 1)
            //    nameBoard = parameter.cbSelectedGroupsStanding.SelectedItem.ToString();
            //List<TeamScoreDetails> rank = CalcDetails(parameter, nameBoard);
            //rank = CalcRanking(parameter.league.id, rank);
            //ExportToPdf(parameter.dgvRanking, rank, " " + nameBoard);
            CreateBoardKnockOut(parameter);
        }
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
            try
            {
                if (parameter.cbSelectedGroupsStanding.SelectedItem == null)
                    return;
                string nameBoard = parameter.cbSelectedGroupsStanding.SelectedItem.ToString();
                GetDetailSetting(parameter);
                List<TeamScoreDetails> rank = CalcDetails(parameter, nameBoard);
                rank = CalcRanking(parameter.league.id, rank);
                foreach (TeamScoreDetails t in rank)
                {
                    t.imageFLM = ImageProcessing.Instance.Convert(ResToImageFLM(t.fLM));
                }
                parameter.dgvRanking.ItemsSource = rank;
            }
            catch
            {
                MessageBox.Show("Lỗi kết nối dữ liệu");
                return;
            }
        }
        List<TeamScoreDetails> CalcDetails(MainWindow parameter, string nameBoard)
        {
            List<TeamScoreDetails> list = new List<TeamScoreDetails>();
            List<Team> team = TeamDAO.Instance.GetListTeam(nameBoard, parameter.league.id);
            int i = -1;
            foreach (Team t in team)
            {
                BitmapImage logoteam = ImageProcessing.Instance.Convert(ImageProcessing.Instance.ByteToImg(t.logo));
                list.Add(new TeamScoreDetails(t.nameTeam, logoteam));
                i++;
                List<Match> matches = MatchDAO.Instance.GetListMatchStartedByIDTeamWithOrder(t.idTournamnt, t.id);

                //Tính 
                foreach (Match m in matches)
                {

                    int gF = 0; //Bàn thắng
                    int gA = 0; //Bàn thua

                    if (m.idTeam01 == t.id)
                    {
                        gF = m.Score1;
                        gA = m.Score2;
                    }
                    else
                    {
                        gF = m.Score2;
                        gA = m.Score1;
                    }

                    // + điểm
                    if (gF > gA)
                        list[i].pts += parameter.setting.scoreWin;
                    if (gF == gA)
                        list[i].pts += parameter.setting.scoreDraw;
                    if (gF < gA)
                        list[i].pts += parameter.setting.scoreLose;

                    list[i].CalcDetails(gF, gA); //Tính hiệu số

                }
            }
            return list;
        }
        List<TeamScoreDetails> CalcRanking(int idTournament, List<TeamScoreDetails> listTeamScoreDetails)
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
            return listTeamScoreDetails;
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
            List<Match> matchid1 = MatchDAO.Instance.GetListMatchStartedByID2Team(idTournament, idTeam1, idTeam2);
            List<Match> matchid2 = MatchDAO.Instance.GetListMatchStartedByID2Team(idTournament, idTeam2, idTeam1);
            int gFH1 = 0; //Bàn thắng sân nhà team 1
            int gFH2 = 0; //Bàn thắng sân nhà team 2
            int gAH1 = 0; //Bàn thua sân nhà team 1
            int gAH2 = 0; //Bàn thua sân nhà team 2
            int W1 = 0; //Trận thắng team 1
            int W2 = 0; //Trận thắng team 2
            foreach (Match mid in matchid1) //Team 1 chủ nhà
            {
                int gF = mid.Score1;
                int gA = mid.Score2;

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
                int gF = mid.Score1;
                int gA = mid.Score2;

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
            int width = imgWin.Width * 6 + space * 2;
            int height = imgWin.Width + space * 2;

            System.Drawing.Bitmap imgres = new System.Drawing.Bitmap(width, height);
            Graphics g = Graphics.FromImage(imgres);

            for (int i = 0; i < 5; i++)
            {
                //get image
                System.Drawing.Image imgResOfmatch = imgWin;
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
        void GetImageResultMatch()
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
        }
        #endregion

    }
}
