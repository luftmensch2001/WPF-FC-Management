﻿using FCM.View;
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

        public SolidColorBrush lightGreen = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#52ff00"));
        public SolidColorBrush white = new SolidColorBrush(Colors.White);

        public string uid;
        public ResultViewModel()
        {
            SwitchTabCommand = new RelayCommand<ResultRecordingWindow>((parameter) => true, (parameter) => SwitchTab(parameter));
            GetUidCommand = new RelayCommand<Button>((parameter) => true, (parameter) => uid = parameter.Uid);
        }

        public void SwitchTab(ResultRecordingWindow parameter)
        {
            int index = int.Parse(uid); // tab index
            //Move Stroke Tab
            parameter.rtStroke.Margin = new Thickness((20 + 120 * index), 0, 0, 5);

            // Reset color
            parameter.btnGoalsTab.Foreground = white;
            parameter.btnCardsTab.Foreground = white;
            parameter.btnTeamsTab.Foreground = white;

            // Hide all screens
            parameter.grdGoalsTab.Visibility = Visibility.Hidden;
            parameter.grdCardsTab.Visibility = Visibility.Hidden;

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
                    parameter.btnTeamsTab.Foreground = lightGreen;
                    break;
            }


        }
    }
}
