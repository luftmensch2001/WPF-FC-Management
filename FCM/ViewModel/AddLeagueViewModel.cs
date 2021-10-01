using FCM.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace FCM.ViewModel
{
    public class AddLeagueViewModel : BaseViewModel
    {
        public ICommand CancelAddLeagueCommand { get; set; }

        public AddLeagueViewModel()
        {
            CancelAddLeagueCommand = new RelayCommand<AddLeagueWindow>((parameter) => true, (parameter) => parameter.Close());
        }
    }
}
