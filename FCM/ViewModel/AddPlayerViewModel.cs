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
    class AddPlayerViewModel : BaseViewModel
    {


        public ICommand CancelAddPlayerCommand { get; set; }
        public ICommand AddPlayerCommand { get; set; }
        public ICommand AddImagePlayerCommand { get; set; }
        private System.Drawing.Image imaged;

        public AddPlayerViewModel()
        {
            CancelAddPlayerCommand = new RelayCommand<AddPlayerWindow>((parameter) => true, (parameter) => parameter.Close());
            AddPlayerCommand = new RelayCommand<AddPlayerWindow>((parameter) => true, (parameter) => AddPlayer(parameter));
            AddImagePlayerCommand = new RelayCommand<AddPlayerWindow>((parameter) => true, (parameter) => AddImagePlayer(parameter));
        }
        void AddImagePlayer(AddPlayerWindow parameter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Image files(*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png", Multiselect = false };
            openFileDialog.ShowDialog();
            string path = openFileDialog.FileName;
            if (path != "")
                try
                {
                    imaged = Image.FromFile(path);
                    parameter.imgPlayerImage.Source = ImageProcessing.Instance.Convert(imaged);
                }
                catch
                {
                    MessageBox.Show("File không hợp lệ, vui lòng chọn lại", "Lỗi");
                }
        }
        void AddPlayer(AddPlayerWindow parameter)
        {
            string name = InputFormat.Instance.FomartSpace(parameter.tbName.Text);
            string unformNumber = InputFormat.Instance.FomartSpace(parameter.tbNumber.Text);
            string position = InputFormat.Instance.FomartSpace(parameter.tbPosition.Text);
            string nation = InputFormat.Instance.FomartSpace(parameter.tbNationality.Text);
            string note = InputFormat.Instance.FomartSpace(parameter.tbNote.Text);

            if (name == "" || unformNumber == "" ||parameter.dpDoB.SelectedDate.ToString()==""|| nation == "" || position == "" || parameter.imgPlayerImage.Source.ToString() == "pack://application:,,,/Resource/Images/NoImageSelected.png")
            {
                MessageBox.Show("Thiếu thông tin", "Lỗi");
                return;
            }
            if (!InputFormat.Instance.isNumber(unformNumber) || Int32.Parse(unformNumber) < 0)
            {
                MessageBox.Show("Số áo chỉ nhận giá trị là số nguyên dương >= 0");
                return;
            }
            if (parameter.player.uniformNumber != Int32.Parse(unformNumber) && PlayerDAO.Instance.IsHaveNumber(Int32.Parse(unformNumber), parameter.team.id))
            {
                MessageBox.Show("Số áo đã tồn tại");
                return;
            }
            var today = DateTime.Today;
            var age = today.Year -  DateTime.Parse(parameter.dpDoB.ToString()).Year;
            if ( DateTime.Parse(parameter.dpDoB.ToString()).Date > today.AddYears(-age)) age--;
            if (age<parameter.setting.minAge || age>parameter.setting.maxAge)
            {
                MessageBox.Show("Tuổi của cầu thủ phải >"+ parameter.setting.minAge +" và <= "+ parameter.setting.maxAge, "Lỗi");
                return;
            }    
            if (parameter.player == null)
            {
                if (!parameter.CanGetOutNation && parameter.team.nation!=nation)
                {
                    MessageBox.Show("Số cầu thủ ngước ngoài đã đạt tối đa");
                    return;
                }    
                Player player = new Player(parameter.team.id,name,Int32.Parse(unformNumber), DateTime.Parse(parameter.dpDoB.ToString()), position,nation,note, ImageProcessing.Instance.convertImgToByte(imaged));
                PlayerDAO.Instance.CreatePlayers(player);

                MessageBox.Show("Thêm cầu thủ thành công");
                parameter.Close();
            }
            else
            {
                Player player;
                if (imaged == null)
                    player = new Player(parameter.team.id, name, Int32.Parse(unformNumber), DateTime.Parse(parameter.dpDoB.ToString()), position, nation, note, parameter.player.image);
                else 
                    player = new Player(parameter.team.id, name, Int32.Parse(unformNumber), DateTime.Parse(parameter.dpDoB.ToString()), position, nation, note, ImageProcessing.Instance.convertImgToByte(imaged));
                player.id = parameter.player.id;
                if (!parameter.CanGetOutNation && parameter.team.nation != nation &&parameter.player.nationality!=nation)
                {
                    MessageBox.Show("Số cầu thủ ngước ngoài đã đạt tối đa");
                    return;
                }
                PlayerDAO.Instance.UpdatePlayer(player);
                MessageBox.Show("Sửa cầu thủ thành công");
                parameter.Close();
            }
        }
    }
}
