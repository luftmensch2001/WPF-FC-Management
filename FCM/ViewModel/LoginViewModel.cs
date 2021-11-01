using FCM.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using FCM.DAO;
using FCM.DTO;

namespace FCM.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        public ICommand ExitLoginCommand { get; set; }
        public ICommand OpenRegisterCommand { get; set; }
        public ICommand OpenLoginCommand { get; set; }
        public ICommand LoginCommand { get; set; }

      //  public string UserName { get => UserName; set { UserName = value;OnPropertyChanged(); } } 
    //    public string Password { get => Password; set { Password = value;OnPropertyChanged(); } } 
        
        public LoginViewModel()
        {
            
            ExitLoginCommand = new RelayCommand<LoginWindow>((parameter) => true, (parameter) => parameter.Close());
            OpenRegisterCommand = new RelayCommand<LoginWindow>((parameter) => true, (parameter) => OpenRegister(parameter));
            OpenLoginCommand = new RelayCommand<LoginWindow>((parameter) => true, (parameter) => OpenLogin(parameter));
            LoginCommand = new RelayCommand<LoginWindow>((parameter) => true, (parameter) => Login(parameter));
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
        public void Login(LoginWindow parameter)
        {
            //   MessageBox.Show(UserName);
            if (parameter.tbUsername.Text == "")
            {
                MessageBox.Show("Vui lòng nhập tên tài khoản");
                return;
            }
            if (parameter.pbPassword.Password == "")
            {
                MessageBox.Show("Vui lòng nhập mật khẫu");
                return;
            }

          //  MessageBox.Show(parameter.tbUsername.Text);
           // MessageBox.Show(parameter.pbPassword.Password);

            string userName = parameter.tbUsername.Text;
            //string passwordEncode = FCM.DAO.UserDAO.MD5Hash(UserDAO.Base64Encode(parameter.pbPassword.Password));\
            string password = parameter.pbPassword.Password;
            List<Account> accounts = AccountDAO.Instance.GetListAccount();
            //MessageBox.Show(accounts.Count.ToString());
            foreach (Account account in accounts)
            {
                if (account.userName == userName && account.password == password)
                {
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.ShowDialog();
                    MessageBox.Show("TRUE");
                    return;
                }
            }
            MessageBox.Show("False");
        }
    }
}