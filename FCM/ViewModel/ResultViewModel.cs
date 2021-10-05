using FCM.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace FCM.ViewModel
{
    public class ResultViewModel : BaseViewModel
    {
        public ICommand SwitchTabCommand { get; set; }
        public ICommand GetUidCommand { get; set; }
        public ICommand SwitchPlayersCommand { get; set; }
        public ICommand ExitCommand { get; set; }


        public SolidColorBrush lightGreen = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#52ff00"));
        public SolidColorBrush white = new SolidColorBrush(Colors.White);

        public string uid;
        public ResultViewModel()
        {
            SwitchTabCommand = new RelayCommand<ResultRecordingWindow>((parameter) => true, (parameter) => SwitchTab(parameter));
            ExitCommand = new RelayCommand<ResultRecordingWindow>((parameter) => true, (parameter) => parameter.Close());
            GetUidCommand = new RelayCommand<Button>((parameter) => true, (parameter) => uid = parameter.Uid);
            SwitchPlayersCommand = new RelayCommand<string>((parameter) => true, (parameter) => OpenSwitchPlayersWindow());
        }

        public void SwitchTab(ResultRecordingWindow parameter)
        {
            int index = int.Parse(uid); // tab index
            //Move Stroke Tab
            parameter.rtStroke.Margin = new Thickness((20 + 120 * index), 0, 0, 5);

            // Reset color
            parameter.btnGoalsTab.Foreground = white;
            parameter.btnCardsTab.Foreground = white;
            parameter.btnFormationsTab.Foreground = white;

            // Hide all screens
            parameter.grdGoalsTab.Visibility = Visibility.Hidden;
            parameter.grdCardsTab.Visibility = Visibility.Hidden;
            parameter.grdFormationsTab.Visibility = Visibility.Hidden;

            switch (index)
            {
                case 0:
                    parameter.btnGoalsTab.Foreground = lightGreen;
                    parameter.grdGoalsTab.Visibility = Visibility.Visible;
                    break;
                case 1:
                    parameter.btnCardsTab.Foreground = lightGreen;
                    parameter.grdCardsTab.Visibility = Visibility.Visible;
                    break;
                case 2:
                    parameter.btnFormationsTab.Foreground = lightGreen;
                    parameter.grdFormationsTab.Visibility = Visibility.Visible;
                    break;
            }
        }
        public void OpenSwitchPlayersWindow()
        {
            SwitchPlayersWindow wd = new SwitchPlayersWindow();
            wd.ShowDialog();
        }
    }
}
