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
                MessageBox.Show("Vui lòng nhập mật khẫu cũ");
                return;
            }
            if (parameter.pbNewPassword.Password == "")
            {
                MessageBox.Show("Vui lòng nhập mật khẫu mới");
                return;
            }
            if (parameter.pbRefillPassword.Password == "")
            {
                MessageBox.Show("Vui lòng nhập lại mật khẫu xác nhận");
                return;
            }
            if (parameter.pbNewPassword.Password != parameter.pbRefillPassword.Password)
            {
                MessageBox.Show("Mất khẫu mới không khớp với mật khẫu xác nhận");
                return;
            }

            string oldPass = parameter.pbOldPassword.Password;
            string newPass = parameter.pbNewPassword.Password;

            if (AccountDAO.MD5Hash(AccountDAO.Base64Encode(oldPass)) != AccountDAO.Instance.GetPassword(parameter.account.userName))
            {
                MessageBox.Show("Mật khẫu cũ không chính xác ");
                return;
            }
            AccountDAO.Instance.UpdatePassword(parameter.account.userName,AccountDAO.MD5Hash(AccountDAO.Base64Encode(newPass)));
            parameter.account.password = AccountDAO.MD5Hash(AccountDAO.Base64Encode(newPass));
            MessageBox.Show("Đổi mật khẫu thành công");
        }
    }
}
