﻿using FCM.View;
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
        public ICommand OpenAddTeamWindowCommand { get; set; }
        public ICommand OpenAddPlayerWindowCommand { get; set; }
        public ICommand OpenEditDialogCommand { get; set; }
        public ICommand OpenAddGoalTypeCommand { get; set; }
        public ICommand OpenEditGoalTypeCommand { get; set; }
        public ICommand OpenChangePasswordCommand { get; set; }
        public ICommand OpenLoginCommand { get; set; }
       
        public ICommand SearchLeagueCommand { get; set; }

        public string uid;

        public SolidColorBrush lightGreen = new SolidColorBrush((Color) ColorConverter.ConvertFromString("#52ff00"));
        public SolidColorBrush white = new SolidColorBrush(Colors.White);
        public MainViewModel()
        {
            SwitchTabCommand = new RelayCommand<MainWindow>((parameter) => true, (parameter) => SwitchTab(parameter));
            SwitchTabStatisticsCommand = new RelayCommand<MainWindow>((parameter) => true, (parameter) => SwitchTabStatistics(parameter));
            GetUidCommand = new RelayCommand<Button>((parameter) => true, (parameter) => uid = parameter.Uid);
            OpenAddLeagueWindowCommand = new RelayCommand<MainWindow>((parameter) => true, (parameter) => OpenAddLeagueWindow(parameter));
            DeleteLeagueCommand = new RelayCommand<MainWindow>((parameter) => true, (parameter) => DeleteLeague(parameter));

            OpenEditDialogCommand = new RelayCommand<string>((parameter) => true, (parameter) => OpenEditDialogWindow(parameter));
            OpenEditLeagueWindowCommand = new RelayCommand<MainWindow>((parameter) => true, (parameter) => OpenEditLeagueWindow(parameter));
            OpenAddTeamWindowCommand = new RelayCommand<string>((parameter) => true, (parameter) => OpenAddTeamWindow());
            OpenAddPlayerWindowCommand = new RelayCommand<string>((parameter) => true, (parameter) => OpenAddPlayerWindow());
            OpenAddGoalTypeCommand = new RelayCommand<string>((parameter) => true, (parameter) => OpenAddGoalTypeWindow());
            OpenEditGoalTypeCommand = new RelayCommand<string>((parameter) => true, (parameter) => OpenEditGoalTypeWindow());
            OpenChangePasswordCommand = new RelayCommand<MainWindow>((parameter) => true, (parameter) => OpenChangePasswordWindow(parameter));
            OpenLoginCommand = new RelayCommand<MainWindow>((parameter) => true, (parameter) => OpenLoginWindow(parameter));
            SearchLeagueCommand = new RelayCommand<MainWindow>((parameter) => true, (parameter) => SearchLeague(parameter));
             
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

            // Switch tab - Show selected screen
            switch (index)
            {
                case 0:
                    parameter.btnHome.Foreground = lightGreen;
                    parameter.icHome.Foreground = lightGreen;
                    if (0>1) // Nếu có ít nhất 1 mùa giải
                        parameter.grdHomeScreen.Visibility = Visibility.Visible;
                    else
                        parameter.grdHomeNoLeagueScreen.Visibility = Visibility.Visible;
                    break;
                case 1:
                    parameter.btnLeagues.Foreground = lightGreen;
                    parameter.icLeagues.Foreground = lightGreen;
                    parameter.grdLeaguesScreen.Visibility = Visibility.Visible;
                    LoadListLeague(parameter);
                    break;
                case 2:
                    parameter.btnSchedule.Foreground = lightGreen;
                    parameter.icSchedule.Foreground = lightGreen;
                    parameter.grdScheduleScreen.Visibility = Visibility.Visible;
                    break;
                case 3:
                    parameter.btnTeams.Foreground = lightGreen;
                    parameter.icTeams.Foreground = lightGreen;
                    parameter.grdTeamsScreen.Visibility = Visibility.Visible;
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
                    parameter.btnSetting.Foreground = lightGreen;
                    parameter.icSetting.Foreground = lightGreen;
                    parameter.grdSettingScreen.Visibility = Visibility.Visible;
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
            AddLeagueWindow wd = new AddLeagueWindow();
            wd.ShowDialog();
            LoadListLeague(parameter);
        }
        public List<League> leagues;
        public void LoadListLeague(MainWindow parameter)
        {
            leagues = LeagueDAO.Instance.GetListLeagues();
            LoadListLeagueToScreen(leagues, parameter);
        }
        void LoadListLeagueToScreen(List<League> listLeagues, MainWindow parameter)
        {
            parameter.wpLeagueCards.Children.Clear();
            if (listLeagues!=null)
            if (parameter.league == null)
            {
                if (leagues.Count > 0)
                    LoadDetailLeague(leagues[0], parameter);
            }
            foreach (League league in listLeagues)
            {
                ucLeagueCard ucLeagueCard = new ucLeagueCard(league, parameter, this);
                parameter.wpLeagueCards.Children.Add(ucLeagueCard);
            }
        }
        public void LoadDetailLeague(League league, MainWindow window)
        {
            window.league = league;
            window.imgLeagueLogo.Source = ImageProcessing.Instance.Convert(ImageProcessing.Instance.ByteToImg(league.logo));
            window.tblLeagueName.Text = "Tên mùa giải: " +league.nameLeague;
            window.tblSponsor.Text = "Nhà tài trợ " +league.nameSpender;
            
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
            window.tblLeagueTime.Text = "Thời gian: " + league.dateTime.ToString("M/d/yyyy");
        }
        public void DeleteLeague(MainWindow parameter)
        {
            if (parameter.league==null)
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
        public void OpenEditLeagueWindow(MainWindow parameter)
        {
            if (parameter.league!=null && parameter.league.status==0)
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

        public void SearchLeague(MainWindow parameter)
        {  
            string name = StringFormat.Instance.FomartSpace(parameter.tbSearchLeague.Text);
            List<League> listLeague = new List<League>();
            foreach (League league in leagues)
            {
                if (league.nameLeague.Contains(name))
                    listLeague.Add(league);
            }    
            LoadListLeagueToScreen(listLeague,parameter);
        }

        public void OpenAddTeamWindow()
        {
            AddTeamWindow wd = new AddTeamWindow();
            wd.ShowDialog();
        }

        public void OpenAddPlayerWindow()
        {
            AddPlayerWindow wd = new AddPlayerWindow();
            wd.ShowDialog();
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

        public void OpenAddGoalTypeWindow()
        {
            AddGoalTypeWindow wd = new AddGoalTypeWindow();
            wd.ShowDialog();
        }
        public void OpenEditGoalTypeWindow()
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


    }   
}
