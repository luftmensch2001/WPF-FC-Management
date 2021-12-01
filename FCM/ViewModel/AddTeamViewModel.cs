using FCM.View;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.IO;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using FCM.DAO;
using FCM.DTO;
using FCM.UserControls;

namespace FCM.ViewModel
{
    class AddTeamViewModel : BaseViewModel
    {

        public ICommand CancelAddTeamCommand { get; set; }
        public ICommand AddTeamCommand { get; set; }
        public ICommand AddLogoTeamCommand { get; set; }
        public ICommand ImportTeamCommand { get; set; }
        public ICommand TemplaceCommand { get; set; }
        private System.Drawing.Image imaged;

        public AddTeamViewModel()
        {
            CancelAddTeamCommand = new RelayCommand<AddTeamWindow>((parameter) => true, (parameter) => parameter.Close());
            AddTeamCommand = new RelayCommand<AddTeamWindow>((parameter) => true, (parameter) => AddTeam(parameter));
            AddLogoTeamCommand = new RelayCommand<AddTeamWindow>((parameter) => true, (parameter) => AddLogoTeam(parameter));
            ImportTeamCommand = new RelayCommand<AddTeamWindow>((parameter) => true, (parameter) => ImportTeam(parameter));
            TemplaceCommand = new RelayCommand<AddTeamWindow>((parameter) => true, (parameter) => Templace(parameter));
        }
        void AddLogoTeam(AddTeamWindow parameter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Image files(*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png", Multiselect = false };
            openFileDialog.ShowDialog();
            string path = openFileDialog.FileName;
            if (path != "")
                try
                {
                    imaged = Image.FromFile(path);
                    parameter.imgTeamLogo.Source = ImageProcessing.Instance.Convert(imaged);
                }
                catch
                {
                    MessageBox.Show("File không hợp lệ, vui lòng chọn lại", "Lỗi");
                }
        }
        void Templace(AddTeamWindow parameter)
        {
            ExcelProcessing.Instance.OpenTemPlace();
        }
        void ImportTeam(AddTeamWindow parameter)
        {
            if (parameter.cbGroups.SelectedItem != null)
            {
                if (ExcelProcessing.Instance.ImportTeam(parameter, parameter.cbGroups.SelectedItem.ToString()))
                    parameter.Close();
            } else
            {
                MessageBox.Show("Chưa chọn bảng đấu");
                return;
            }    
        }
        void AddTeam(AddTeamWindow parameter)
        {
            string name = InputFormat.Instance.FomartSpace(parameter.tbName.Text);
            string coach = InputFormat.Instance.FomartSpace(parameter.tbCoach.Text);
            string national = InputFormat.Instance.FomartSpace(parameter.tbNational.Text);
            string stadium = InputFormat.Instance.FomartSpace(parameter.tbStadium.Text);
            if (name == "" || coach == "" || national == "" || stadium == "" || parameter.imgTeamLogo.Source.ToString() == "pack://application:,,,/Resource/Images/NoLogoSelected.png")
            {
                MessageBox.Show("Thiếu thông tin", "Lỗi");
                return;
            }
            if (parameter.team == null && TeamDAO.Instance.IsExistTeamName(name, parameter.idTournament))
            {
                MessageBox.Show("Trùng tên đội bóng","Lỗi");
                return;
            }
            if (parameter.team == null)
            {
                Team team = new Team(parameter.idTournament, parameter.cbGroups.Text, name, coach, stadium, national, ImageProcessing.Instance.convertImgToByte(imaged));
                TeamDAO.Instance.CreateTeams(team);

                MessageBox.Show("Thêm đội bóng thành công");
                parameter.Close();
            }

            else
            {
                Team team;
                if (imaged == null)
                    team = new Team(parameter.idTournament, parameter.cbGroups.Text, name, coach, stadium, national, parameter.team.logo);
                else
                    team = new Team(parameter.idTournament, parameter.cbGroups.Text, name, coach, stadium, national, ImageProcessing.Instance.convertImgToByte(imaged));
                team.id = parameter.team.id;
                TeamDAO.Instance.UpdateTeam(team);
                MessageBox.Show("Sửa đội bóng thành công");
                parameter.Close();
            }
        }
    }
}
