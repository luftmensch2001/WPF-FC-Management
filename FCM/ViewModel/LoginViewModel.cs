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
        public ICommand RegisterCommand { get; set; }
        public ICommand OpenLoginCommand { get; set; }
        public ICommand LoginCommand { get; set; }

        
        public LoginViewModel()
        {
            
            ExitLoginCommand = new RelayCommand<LoginWindow>((parameter) => true, (parameter) => parameter.Close());
            OpenRegisterCommand = new RelayCommand<LoginWindow>((parameter) => true, (parameter) => OpenRegister(parameter));
            RegisterCommand = new RelayCommand<LoginWindow>((parameter) => true, (parameter) => Register(parameter));
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

            string userName = parameter.tbUsername.Text;
            string password = AccountDAO.MD5Hash(AccountDAO.Base64Encode(parameter.pbPassword.Password));
            //string password = parameter.pbPassword.Password;
            List<Account> accounts = AccountDAO.Instance.GetListAccount();
            //MessageBox.Show(accounts.Count.ToString());
            foreach (Account account in accounts)
            {
                if (account.userName == userName && account.password == password)
                {
                    MainWindow mainWindow = new MainWindow(account);
                    parameter.Hide();
                    mainWindow.Show();
                    parameter.Close();
                    return;
                }
                if (account.userName == userName && account.password != password)
                {
                    MessageBox.Show("Sai mật khẫu");
                    return;
                }    
            }
            MessageBox.Show("Tài khoản không tồn tại");
        }
        public void Register(LoginWindow parameter)
        {
            if (parameter.tbRegUsername.Text == "")
            {
                MessageBox.Show("Vui lòng nhập tên tài khoản");
                return;
            }
            if (parameter.pbRegPassword.Password == "")
            {
                MessageBox.Show("Vui lòng nhập mật khẫu");
                return;
            }
            if (parameter.pbRePassword.Password == "")
            {
                MessageBox.Show("Vui lòng nhập mật khẫu xác nhận");
                return;
            }
            if (parameter.pbRePassword.Password != parameter.pbRegPassword.Password)
            {
                MessageBox.Show("Mật khẫu không khớp với Mật khẫu xác nhận");
                return;
            }
            if (parameter.pbAdminPassword.Password == "")
            {
                MessageBox.Show("Vui lòng nhập mật khẫu admin");
                return;
            }
            if (parameter.cbAcccountType.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn loại tài khoản");
                return;
            }

            string userName = parameter.tbRegUsername.Text;
            string password = AccountDAO.MD5Hash(AccountDAO.Base64Encode(parameter.pbRegPassword.Password));
            //string password = parameter.pbRegPassword.Password;
            string rePassword = parameter.pbRePassword.Password;
            string regPasswordAdmin = AccountDAO.MD5Hash(AccountDAO.Base64Encode(parameter.pbAdminPassword.Password));

            List<Account> accounts = AccountDAO.Instance.GetListAccount();
            foreach (Account account in accounts)
            {
                if (account.userName == userName)
                {
                    MessageBox.Show("Tài khoản đã tồn tại");
                    return;
                }
            }
            string passwordAdmin = AccountDAO.Instance.GetPasswordAdmin();
            if (passwordAdmin != regPasswordAdmin)
            {
                MessageBox.Show("Mật khẫu admin không chính xác    ");
                return;
            }
            Account account1 = new Account(userName, password, "",parameter.cbAcccountType.SelectedIndex+2, -1);
            AccountDAO.Instance.CreateAccount(account1);
            account1.id = AccountDAO.Instance.GetId(account1.userName);
            MainWindow mainWindow = new MainWindow(account1);
            parameter.Hide();
            mainWindow.Show();
            parameter.Close();

        }
    }
}