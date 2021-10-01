using FCM.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace FCM.ViewModel
{
    public class AddLeagueViewModel : BaseViewModel
    {
        public ICommand ExitAddLeagueCommand { get; set; }

        public AddLeagueViewModel()
        {
            ExitAddLeagueCommand = new RelayCommand<AddLeagueWindow>((parameter) => true, (parameter) => parameter.Close());
        }
    }
}
