using FCM.DAO;
using FCM.DTO;
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
            if (parameter.match.date.ToString("dd/MM/yyyy") == "11/11/2000")
            {
                MessageBox.Show("Chưa chọn thời gian");
                return;
            }
            parameter.main.OpenResultRecordingWindow(parameter.mainWindow, parameter.match);
        }

        void CancelResult(ucMatchDetail parameter)
        {
            if (MessageBox.Show("Bạn thực sự muốn xóa kết quả của trận đấu này?", "Lưu ý", MessageBoxButton.OKCancel, MessageBoxImage.Question)
                == MessageBoxResult.OK)
            {
                parameter.main.CancelResultMatch(parameter.mainWindow, parameter.match);
                if (!parameter.match.allowDraw)
                {
                    TreeMatch tree = TreeMatchDAO.Instance.GetTree(parameter.match.idTournaments);
                    tree.DeleteNode(NodeMatchDAO.Instance.GetNodeById(tree.idFirstNode) ,parameter.match.id);
                }
                parameter.match.Score1 = -1;
                parameter.match.Score2 = -1;
                MatchDAO.Instance.UpdateMatch(parameter.match);
                parameter.main.LoadListMatchRound(parameter.mainWindow, "Tất cả vòng" , "Tất cả bảng đấu");
            }
        }
        void SaveNewInfor(EditMatchInforWindow parameter)
        {

            League league = LeagueDAO.Instance.GetLeagueById(parameter.match.idTournaments);
            if (DateTime.Compare(league.dateTime, DateTime.Parse(parameter.dpDate.Text)) > 0)
            {
                MessageBox.Show("Thời gian trận đấu phải sau thời gian giải khởi tranh");
                return;
            }
            if (league.typeLeague == 1)
            {
                DateTime date = MatchDAO.Instance.MaxTimeNockOut(parameter.match);
                date.AddHours(2);
                DateTime date1 = DateTime.Parse(parameter.dpDate.Text);
                date1.AddHours(-date1.Hour);
                date1.AddMinutes(-date1.Minute);
                //DateTime time = DateTime.Parse(parameter.tpTime.Text);
                //date1.AddHours(time.Hour);
                //date1.AddMinutes(time.Minute);
                MessageBox.Show(date1 + "             " + date);
                if (DateTime.Compare(date1, date) < 0)
                {
                    MessageBox.Show(date1 + "             " + date);
                    MessageBox.Show("Thời gian trận đấu phải sau thời gian vòng đấu trước");
                    return;
                }
            }
            if (league.typeLeague == 2)
            {
                if (DateTime.Compare(DateTime.Parse(parameter.dpDate.Text), MatchDAO.Instance.MaxTimeBoard(parameter.match)) < 0)
                {
                    MessageBox.Show("Thời gian trận đấu phải sau thời gian vòng đấu trước");
                    return;
                }
            }


            parameter.match.statium = parameter.cbStadium.Text;
            parameter.match.date = DateTime.Parse(parameter.dpDate.Text);
            parameter.match.time = DateTime.Parse(parameter.tpTime.Text);

            if (MatchDAO.Instance.IsExistTimeMatch(parameter.match))
            {
                MessageBox.Show("Trùng thời gian", "Lỗi");
                return;
            }
            MatchDAO.Instance.UpdateMatch(parameter.match);

            MessageBox.Show("Thay đổi thông tin trận đấu thành công", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            parameter.Close();
        }
    }
}
