using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace FCM.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public ICommand SwitchTabCommand { get; set; }
        public ICommand GetUidCommand { get; set; }

        public string uid;

        public SolidColorBrush lightGreen = new SolidColorBrush((Color) ColorConverter.ConvertFromString("#52ff00"));
        public SolidColorBrush white = new SolidColorBrush(Colors.White);
        public MainViewModel()
        {
            SwitchTabCommand = new RelayCommand<MainWindow>((parameter) => true, (parameter) => SwitchTab(parameter));
            GetUidCommand = new RelayCommand<Button>((parameter) => true, (parameter) => uid = parameter.Uid);
        }

        public void SwitchTab(MainWindow parameter)
        {
            LoginWindow wd = new LoginWindow();
            wd.ShowDialog();

            int index = int.Parse(uid); // tab index
            //Move Stroke Menu
            parameter.grdStroke.Margin = new Thickness(0, (200 + 65 * index), 0, 0);

            //Reset Color items
            parameter.btnHome.Foreground = white;
            parameter.btnLeagues.Foreground = white;
            parameter.btnSchedule.Foreground = white;
            parameter.btnTeams.Foreground = white;
            parameter.btnStanding.Foreground = white;
            parameter.btnStatistics.Foreground = white;
            parameter.btnSetting.Foreground = white;
            parameter.btnHelp.Foreground = white;

            parameter.icHome.Foreground = white;
            parameter.icLeagues.Foreground = white;
            parameter.icSchedule.Foreground = white;
            parameter.icTeams.Foreground = white;
            parameter.icStanding.Foreground = white;
            parameter.icStatistics.Foreground = white;
            parameter.icSetting.Foreground = white;
            parameter.icHelp.Foreground = white;

            // Switch tab
            switch (index)
            {
                case 0:
                    parameter.btnHome.Foreground = lightGreen;
                    parameter.icHome.Foreground = lightGreen;
                    break;
                case 1:
                    parameter.btnLeagues.Foreground = lightGreen;
                    parameter.icLeagues.Foreground = lightGreen;
                    break;
                case 2:
                    parameter.btnSchedule.Foreground = lightGreen;
                    parameter.icSchedule.Foreground = lightGreen;
                    break;
                case 3:
                    parameter.btnTeams.Foreground = lightGreen;
                    parameter.icTeams.Foreground = lightGreen;
                    break;
                case 4:
                    parameter.btnStanding.Foreground = lightGreen;
                    parameter.icStanding.Foreground = lightGreen;
                    break;
                case 5:
                    parameter.btnStatistics.Foreground = lightGreen;
                    parameter.icStatistics.Foreground = lightGreen;
                    break;
                case 6:
                    parameter.btnSetting.Foreground = lightGreen;
                    parameter.icSetting.Foreground = lightGreen;
                    break;
                case 7:
                    parameter.btnHelp.Foreground = lightGreen;
                    parameter.icHelp.Foreground = lightGreen;
                    break;

                default:
                    break;
            }
        }

    }
}
