using FCM.View;
using Microsoft.Expression.Interactivity.Core;
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
    class ChangePasswordViewModel : BaseViewModel
    {
        public ICommand ExitCommand { get; set; }
        public ICommand ChangePasswordCommand { get; set; }
        public ChangePasswordViewModel()
        {
            ExitCommand = new RelayCommand<ChangePasswordWindow>((parameter) => true, (parameter) => parameter.Close());
            ChangePasswordCommand = new RelayCommand<ChangePasswordWindow>((parameter) => true, (parameter) => RePassword(parameter));
        }
        public void RePassword(ChangePasswordWindow parameter)
        {
            if (parameter.pbOldPassword.Password == "")
            {
                MessageBox.Show("Vui lòng nhập mật khẩu cũ");
                return;
            }
            if (parameter.pbNewPassword.Password == "")
            {
                MessageBox.Show("Vui lòng nhập mật khẩu mới");
                return;
            }
            if (parameter.pbRefillPassword.Password == "")
            {
                MessageBox.Show("Vui lòng nhập lại mật khẩu xác nhận");
                return;
            }
            if (parameter.pbNewPassword.Password != parameter.pbRefillPassword.Password)
            {
                MessageBox.Show("Mất khẩu mới không khớp với mật khẩu xác nhận");
                return;
            }

            string oldPass = parameter.pbOldPassword.Password;
            string newPass = parameter.pbNewPassword.Password;

            if (AccountDAO.MD5Hash(AccountDAO.Base64Encode(oldPass)) != AccountDAO.Instance.GetPassword(parameter.account.userName))
            {
                MessageBox.Show("Mật khẩu cũ không chính xác ");
                return;
            }
            AccountDAO.Instance.UpdatePassword(parameter.account.userName,AccountDAO.MD5Hash(AccountDAO.Base64Encode(newPass)));
            parameter.account.password = AccountDAO.MD5Hash(AccountDAO.Base64Encode(newPass));
            MessageBox.Show("Đổi mật khẩu thành công");
            parameter.Close();
        }
    }
}
