using System;
using System.Windows;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using FCM.DAO;
using FCM.DTO;
using FCM.View;

namespace FCM.ViewModel
{
    class EditDialogViewModel : BaseViewModel
    {
        public ICommand CancelEditDialogCommand { get; set; }
        public ICommand EditDialogCommand { get; set; }

        public EditDialogViewModel()
        { 
            CancelEditDialogCommand = new RelayCommand<EditDialogWindow>((parameter) => true, (parameter) => parameter.Close());
            EditDialogCommand = new RelayCommand<EditDialogWindow>((parameter) => true, (parameter) => EditDialog(parameter));
        }

        void EditDialog(EditDialogWindow parameter)
        {
            int value;
            try
            {
                value = Int32.Parse(parameter.tbValue.Text);
            }
            catch
            {
                MessageBox.Show("Định dạng không hợp lệ");
                return;
            }

            //Logic
            switch (parameter.idSetting)
            {
                case 0:
                    if (value < 2)
                    {
                        MessageBox.Show("Số lượng đội bóng phải lớn hơn 1");
                        return;
                    }

                    break;
                case 1:
                    if (parameter.curSetting.minPlayerOfTeam < 1)
                    {
                        MessageBox.Show("Số lượng cầu thủ tối thiểu phải lớn hơn 0", "Lỗi");
                        return;
                    }
                    if (value > parameter.curSetting.maxPlayerOfTeam)
                    {
                        MessageBox.Show("Số lượng cầu thủ tối thiểu không được lớn hơn số lượng cầu thủ tối đa", "Lỗi");
                        return;
                    }
                    break;
                case 2:
                    if (parameter.curSetting.maxPlayerOfTeam < 1)
                    {
                        MessageBox.Show("Số lượng cầu thủ tối đa phải lớn hơn 0", "Lỗi");
                        return;
                    }
                    if (value < parameter.curSetting.minPlayerOfTeam)
                    {
                        MessageBox.Show("Số lượng cầu thủ tối đa không được nhỏ hơn số lượng cầu thủ tối thiểu", "Lỗi");
                        return;
                    }
                    break;
                case 3:
                    if (value < 1)
                    {
                        MessageBox.Show("Số tuổi phải lớn hơn 0", "Lỗi");
                        return;
                    }
                    if (value > parameter.curSetting.maxAge)
                    {
                        MessageBox.Show("Tuổi tối thiểu không được lớn hơn tuổi tối đa", "Lỗi");
                        return;
                    }
                    break;
                case 4:
                    if (value < 1)
                    {
                        MessageBox.Show("Số tuổi phải lớn hơn 0", "Lỗi");
                        return;
                    }
                    if (value < parameter.curSetting.minAge)
                    {
                        MessageBox.Show("Tuổi tối đa không được nhỏ hơn tuổi tối thiểu", "Lỗi");
                        return;
                    }
                    break;
                case 5:
                    if (value < 0)
                    {
                        MessageBox.Show("Số lượng tối thiểu là 0", "Lỗi");
                        return;
                    }
                    break;
                case 6:
                    if (value < 2)
                    {
                        MessageBox.Show("Số lượng tối thiểu là 2", "Lỗi");
                        return;
                    }
                    if (value % 2 != 0)
                    {
                        MessageBox.Show("Số lượng đội phải chia hết cho 2", "Lỗi");
                        return;
                    }
                    if (value >= parameter.curSetting.numberOfTeam)
                    {
                        MessageBox.Show("Số lượng đội vào vòng trong phải nhỏ hơn số đội tham gia giải", "Lỗi");
                        return;
                    }
                    break;
                case 7:
                    if (value <= parameter.curSetting.scoreDraw || value <= parameter.curSetting.scoreLose)
                    {
                        MessageBox.Show("Điểm thắng > Điểm Hoà > Điểm Thua", "Lỗi");
                        return;
                    }
                    break;
                case 8:
                    if (value <= parameter.curSetting.scoreLose || value >= parameter.curSetting.scoreWin)
                    {
                        MessageBox.Show("Điểm thắng > Điểm Hoà > Điểm Thua", "Lỗi");
                        return;
                    }
                    break;
                case 9:
                    if (value >= parameter.curSetting.scoreDraw || value >= parameter.curSetting.scoreWin)
                    {
                        MessageBox.Show("Điểm thắng > Điểm Hoà > Điểm Thua", "Lỗi");
                        return;
                    }
                    break;
            }

            string nameCol = "";

            //Edit
            try
            {
                switch(parameter.idSetting)
                {
                    case 0:
                        nameCol = "NumberofTeams";
                        break;
                    case 1:
                        nameCol = "MinplayerofTeams";
                        break;
                    case 2:
                        nameCol = "MaxplayerofTeams";
                        break;
                    case 3:
                        nameCol = "MinAge";
                        break;
                    case 4:
                        nameCol = "MaxAge";
                        break;
                    case 5:
                        nameCol = "MaxNumberForeignPlayers";
                        break;
                    case 6:
                        nameCol = "NumberOfTeamsIn";
                        break;
                    case 7:
                        nameCol = "Score_Win";
                        break;
                    case 8:
                        nameCol = "Score_Draw";
                        break;
                    case 9:
                        nameCol = "Score_Lose";
                        break;
                }
                SettingDAO.Instance.EditSetting(parameter.idTournament, nameCol, parameter.tbValue.Text);

                //
                if (parameter.idSetting == 0)
                {
                    int cb = LeagueDAO.Instance.GetLeagueById(parameter.idTournament).countBoard;
                    if (cb == 1)
                        SettingDAO.Instance.EditSetting(parameter.idTournament, "NumberOfTeamsIn", parameter.tbValue.Text);
                }

                MessageBox.Show("Cập nhập quy định thành công");
                parameter.Close();
            }
            catch
            {
                MessageBox.Show("Lỗi kết nối dữ liệu");
                return;
            }
        }
    }
}
