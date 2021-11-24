using FCM.DAO;
using FCM.UserControls;
using FCM.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace FCM.ViewModel
{
    public class EditMatchInforViewModel : BaseViewModel
    {
        public ICommand OpenEditMatchWindowCommand { get; set; }
        public ICommand OpenResultRecordWindowCommand { get; set; }
        public ICommand CancelResultCommand { get; set; }
        
        public ICommand ExitCommand { get; set; }
        public ICommand SaveCommand { get; set; }


        public EditMatchInforViewModel()
        {
            OpenEditMatchWindowCommand = new RelayCommand<ucMatchDetail>((parameter) => true, (parameter) => OpenEditMatchWindow(parameter));
            OpenResultRecordWindowCommand = new RelayCommand<ucMatchDetail>((parameter) => true, (parameter) => OpenResultRecordWindow(parameter));
            CancelResultCommand = new RelayCommand<ucMatchDetail>((parameter) => true, (parameter) => CancelResult(parameter));

            ExitCommand = new RelayCommand<EditMatchInforWindow>((parameter) => true, (parameter) => parameter.Close());
            SaveCommand = new RelayCommand<EditMatchInforWindow>((parameter) => true, (parameter) => SaveNewInfor(parameter));
        }

        void OpenEditMatchWindow(ucMatchDetail parameter)
        {
            parameter.main.OpenEditMatchInfoWindow(parameter.mainWindow, parameter.match);
        }
        void OpenResultRecordWindow(ucMatchDetail parameter)
        {
            parameter.main.OpenResultRecordingWindow(parameter.mainWindow, parameter.match);
        }

        void CancelResult(ucMatchDetail parameter)
        {
            if (MessageBox.Show("Bạn thực sự muốn xóa kết quả của trận đấu này?", "Lưu ý", MessageBoxButton.OKCancel, MessageBoxImage.Question)
                == MessageBoxResult.OK)
            {
                parameter.main.CancelResultMatch(parameter.mainWindow, parameter.match);
            }
        }
        void SaveNewInfor(EditMatchInforWindow parameter)
        {
            parameter.match.statium = parameter.cbStadium.Text;
            parameter.match.date = DateTime.Parse(parameter.dpDate.Text);
            parameter.match.time = DateTime.Parse(parameter.tpTime.Text);


            MatchDAO.Instance.UpdateMatch(parameter.match);

            MessageBox.Show("Thay đổi thông tin trận đấu thành công", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            parameter.Close();
        }
    }
}
