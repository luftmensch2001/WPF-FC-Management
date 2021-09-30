using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace FCM.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        public ICommand ExitLoginCommand { get; set; }
        public LoginViewModel()
        {
            ExitLoginCommand = new RelayCommand<LoginWindow>((parameter) => true, (parameter) => parameter.Close());
        }
    }
}
