using FCM.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace FCM.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        public ICommand ExitLoginCommand { get; set; }
        public ICommand OpenRegisterCommand { get; set; }
        public ICommand OpenLoginCommand { get; set; }
        
        public LoginViewModel()
        {
            ExitLoginCommand = new RelayCommand<LoginWindow>((parameter) => true, (parameter) => parameter.Close());
            OpenRegisterCommand = new RelayCommand<LoginWindow>((parameter) => true, (parameter) => OpenRegister(parameter));
            OpenLoginCommand = new RelayCommand<LoginWindow>((parameter) => true, (parameter) => OpenLogin(parameter));
        }

        public void OpenRegister(LoginWindow parameter)
        {
            parameter.grdLogin.Visibility = Visibility.Hidden;
            parameter.grdRegister.Visibility = Visibility.Visible;
            parameter.tblTitle.Text = "ĐĂNG KÝ";
        }
        public void OpenLogin(LoginWindow parameter)
        {
            parameter.grdLogin.Visibility = Visibility.Visible;
            parameter.grdRegister.Visibility = Visibility.Hidden;
            parameter.tblTitle.Text = "ĐĂNG NHẬP";
        }
    }
}
