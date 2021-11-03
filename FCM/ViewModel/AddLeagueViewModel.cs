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
    public class AddLeagueViewModel : BaseViewModel
    {
        public ICommand CancelAddLeagueCommand { get; set; }
        public ICommand AddLeagueCommand { get; set; }
        public ICommand AddLogoLeagueCommand { get; set; }
        private string pathImage = "";
        private System.Drawing.Image imaged;

        public AddLeagueViewModel()
        {
            CancelAddLeagueCommand = new RelayCommand<AddLeagueWindow>((parameter) => true, (parameter) => parameter.Close());
            AddLeagueCommand = new RelayCommand<AddLeagueWindow>((parameter) => true, (parameter) => AddLeague(parameter));
            AddLogoLeagueCommand = new RelayCommand<AddLeagueWindow>((parameter) => true, (parameter) => AddLogoLeague(parameter));
        }
        void AddLogoLeague(AddLeagueWindow parameter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Image files(*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png", Multiselect = false };
            openFileDialog.ShowDialog();
            string path = openFileDialog.FileName;
            if (path != "")
                try
                {
                    pathImage = path;
                    imaged = Image.FromFile(path);
                    parameter.imgLeagueLogo.Source = ImageProcessing.Instance.Convert(imaged);
                }
                catch
                {
                    MessageBox.Show("File không hợp lệ, vui lòng chọn lại", "Lỗi");
                }

        }
        void AddLeague(AddLeagueWindow parameter)
        {
            if (parameter.tbUsername.Text == "")
            {
                MessageBox.Show("Vui lòng nhập tên giải đấu");
                return;
            }
            if (parameter.tbSponsor.Text == "")
            {
                MessageBox.Show("Vui lòng nhập tên nhà tài trợ");
                return;
            }
            if (parameter.datePicker.Text == "")
            {
                MessageBox.Show("Vui lòng nhập thời gian");
                return;
            }
            if (parameter.tbCountOfTeams.Text == "")
            {
                MessageBox.Show("Vui lòng nhập số đội tham gia");
                return;
            }
            bool isNumber = false;
            int number = -1;
            if (int.TryParse(parameter.tbCountOfTeams.Text, out number))
            {
                isNumber = true;
            }
            if (!isNumber || number <= 0 || number > 64)
            {
                MessageBox.Show("Số đội tham gia chỉ nhận giá trị số nguyên >0 và <64");
                return;
            }
            if (parameter.imgLeagueLogo.Source.ToString() == "pack://application:,,,/Resource/Images/NoLogoSelected.png")
            {
                MessageBox.Show("Vui lòng chọn logo giải đấu");
                return;
            }
            if (parameter.league==null)
            {
                League league = new League(parameter.tbSponsor.Text, parameter.tbUsername.Text, 0, DateTime.Parse(parameter.datePicker.ToString()), ImageProcessing.Instance.convertImgToByte(imaged), number);
                LeagueDAO.Instance.CreateLeague(league);
                SettingDAO.Instance.CreateSetting(LeagueDAO.Instance.GetNewestLeagurId(), league.countTeam);
                
                MessageBox.Show("Tạo mùa giải thành công");
                parameter.Close();
            } else
            {
                League league;
                if (imaged==null)
                    league = new League(parameter.tbSponsor.Text, parameter.tbUsername.Text, 0, DateTime.Parse(parameter.datePicker.ToString()), parameter.league.logo, number);
                else
                    league = new League(parameter.tbSponsor.Text, parameter.tbUsername.Text, 0, DateTime.Parse(parameter.datePicker.ToString()), ImageProcessing.Instance.convertImgToByte(imaged), number);
                league.id = parameter.league.id;
                LeagueDAO.Instance.UpdateLeague(league);
                SettingDAO.Instance.UpdateSetting_NumberOfTeams(league.id, league.countTeam);
                MessageBox.Show("Sửa mùa giải thành công");
                parameter.Close();
            }

        }
    }
}
