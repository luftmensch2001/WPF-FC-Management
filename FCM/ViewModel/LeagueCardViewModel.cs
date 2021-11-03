using FCM.View;
using Microsoft.Expression.Interactivity.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using FCM.DAO;
using FCM.DTO;

namespace FCM.ViewModel
{
    public class LeagueCardViewModel : BaseViewModel
    {
        public ICommand ExitLoginCommand { get; set; }
        public LeagueCardViewModel()
        {

        }
    }
}
