using System;
using System.Windows;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using FCM.View;
using FCM.DAO;

namespace FCM.ViewModel
{
    class AddGoalTypeViewModel : BaseViewModel
    {
        public ICommand CancelAddGoalTypeCommand { get; set; }
        public ICommand AddGoalTypeCommand { get; set; }

        public AddGoalTypeViewModel()
        {
            AddGoalTypeCommand = new RelayCommand<AddGoalTypeWindow>((parameter) => true, (parameter) => AddGoalType(parameter));
            CancelAddGoalTypeCommand = new RelayCommand<AddGoalTypeWindow>((parameter) => true, (parameter) => parameter.Close());
        }

        public void AddGoalType(AddGoalTypeWindow parameter)
        {
            if (parameter.isAdd)
            {
                try
                {
                    string name = InputFormat.Instance.FomartSpace(parameter.tbName.Text);
                    if (name == "")
                    {
                        MessageBox.Show("Tên bàn thắng không được để trống");
                        return;
                    }
                    if (TypeOfGoalDAO.Instance.IsExistNameTypeGoal(parameter.idTournament, name))
                    {
                        MessageBox.Show("Loại bàn thắng đã tồn tại");
                    }
                    else
                    {
                        TypeOfGoalDAO.Instance.AddTypeGoal(parameter.idTournament, name);
                        MessageBox.Show("Thêm loại bàn thắng thành công");
                        parameter.Close();
                    }
                }
                catch
                {
                    MessageBox.Show("Lỗi kết nối");
                    return;
                }
            }
            else
            {
                try
                {
                    string oldName = parameter.oldName;
                    string name = InputFormat.Instance.FomartSpace(parameter.tbName.Text);
                    if (name == "")
                    {
                        MessageBox.Show("Tên bàn thắng không được để trống");
                        return;
                    }
                    if (name != oldName && TypeOfGoalDAO.Instance.IsExistNameTypeGoal(parameter.idTournament, name))
                    {
                        MessageBox.Show("Loại bàn thắng đã tồn tại");
                    }
                    else
                    {
                        TypeOfGoalDAO.Instance.EditNameTypeGoal(parameter.idTournament, name, oldName);
                        MessageBox.Show("Sửa loại bàn thắng thành công");
                        parameter.Close();
                    }
                }
                catch
                {
                    MessageBox.Show("Lỗi kết nối");
                    return;
                }
            }
        }

    }
}
