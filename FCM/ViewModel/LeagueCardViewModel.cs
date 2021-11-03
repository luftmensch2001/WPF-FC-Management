using FCM.View;
using Microsoft.Expression.Interactivity.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using FCM.DAO;
using FCM.DTO;
using FCM.UserControls;

namespace FCM.ViewModel
{
    public class LeagueCardViewModel : BaseViewModel
    {
        public ICommand GetDetailCommand { get; set; }
        public LeagueCardViewModel()
        {
            
            GetDetailCommand = new RelayCommand<ucLeagueCard>((parameter) => true, (parameter) => GetDetail(parameter));
        }
        void GetDetail(ucLeagueCard parameter)
        {
            parameter.main.LoadDetailLeague(parameter.league, parameter.mainWindow);
        }
    }
}
