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
            string name = InputFormat.Instance.FomartSpace(parameter.tbUsername.Text);
            string sponsor = InputFormat.Instance.FomartSpace(parameter.tbSponsor.Text);
            string countTeam = InputFormat.Instance.FomartSpace(parameter.tbCountOfTeams.Text);
            if (name == ""|| sponsor == ""|| parameter.datePicker.Text == ""|| countTeam == ""|| parameter.imgLeagueLogo.Source.ToString() == "pack://application:,,,/Resource/Images/NoLogoSelected.png")
            {
                MessageBox.Show("Thiếu thông tin","Lỗi");
                return;
            }
            if (InputFormat.Instance.isNumber(countTeam) || Int32.Parse(countTeam)<2 || Int32.Parse(countTeam) >24)
            {
                MessageBox.Show("Số đội tham gia chỉ nhận giá trị số nguyên >=2 và <64");
                return;
            }
            if (parameter.league==null)
            {
                League league = new League(sponsor, name, 0, DateTime.Parse(parameter.datePicker.ToString()), ImageProcessing.Instance.convertImgToByte(imaged), Int32.Parse(countTeam));
                LeagueDAO.Instance.CreateLeague(league);
                
                MessageBox.Show("Tạo mùa giải thành công");
                parameter.Close();
            } else
            {
                League league;
                if (imaged==null)
                    league = new League(sponsor,name, 0, DateTime.Parse(parameter.datePicker.ToString()), parameter.league.logo, Int32.Parse(countTeam));
                else
                    league = new League(sponsor, name, 0, DateTime.Parse(parameter.datePicker.ToString()), ImageProcessing.Instance.convertImgToByte(imaged), Int32.Parse(countTeam));
                league.id = parameter.league.id;
                LeagueDAO.Instance.UpdateLeague(league);
                MessageBox.Show("Sửa mùa giải thành công");
                parameter.Close();
            }

        }
    }
}
