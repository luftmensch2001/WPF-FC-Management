﻿using FCM.View;
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
using System.Globalization;

namespace FCM.ViewModel
{
    public class AddLeagueViewModel : BaseViewModel
    {
        public ICommand CancelAddLeagueCommand { get; set; }
        public ICommand AddLeagueCommand { get; set; }
        public ICommand AddLogoLeagueCommand { get; set; }
        public ICommand ChangeTypeLeagueCommand { get; set; }
        private System.Drawing.Image imaged;

        public AddLeagueViewModel()
        {
            CancelAddLeagueCommand = new RelayCommand<AddLeagueWindow>((parameter) => true, (parameter) => parameter.Close());
            AddLeagueCommand = new RelayCommand<AddLeagueWindow>((parameter) => true, (parameter) => AddLeague(parameter));
            AddLogoLeagueCommand = new RelayCommand<AddLeagueWindow>((parameter) => true, (parameter) => AddLogoLeague(parameter));
            ChangeTypeLeagueCommand = new RelayCommand<AddLeagueWindow>((parameter) => true, (parameter) => ChangeTypeLeague(parameter));
        }
        void ChangeTypeLeague(AddLeagueWindow parameter)
        {
            switch (parameter.cbTypeOfLeague.SelectedIndex)
            {
                case 0:
                    parameter.cbCountOfGroups.Visibility = Visibility.Hidden;
                    break;
                case 1:
                    parameter.cbCountOfGroups.Visibility = Visibility.Hidden;
                    break;
                case 2:
                    parameter.cbCountOfGroups.Visibility = Visibility.Visible;
                    break;
            }
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
        public bool IsValidInformation(string name, string sponsor, string countTeam, string time, string logo, string typeLeague, string countBoard)
        {
            if (name == "" || sponsor == "" || countTeam == "" || time == "" || logo == "pack://application:,,,/Resource/Images/NoLogoSelected.png" || typeLeague == "" || countBoard == "")
            {
                MessageBox.Show("Thiếu thông tin", "Lỗi");
                return false;
            }
            if (!InputFormat.Instance.isNumber(countTeam))
            {
                MessageBox.Show("Số đội tham gia chỉ nhận giá trị số nguyên >=2 và <=60");
                return false;
            }
            if (typeLeague != "Loại trực tiếp" && (Int32.Parse(countTeam) < 2 || Int32.Parse(countTeam) > 60))
            {
                MessageBox.Show("Số đội thể loại thi đấu này phải >=2 và <=60");
                return false;
            }
            if (typeLeague == "Loại trực tiếp" && (Int32.Parse(countTeam) < 2 || Int32.Parse(countTeam) > 16))
            {
                MessageBox.Show("Số đội thể loại thi đấu này phải >=2 và <=16");
                return false;
            }
            if (Int32.Parse(countTeam) < 2 || Int32.Parse(countTeam) > 60)
            {
                MessageBox.Show("Số đội thể loại thi đấu này phải >=2 và <=60");
                return false;
            }
            if (Int32.Parse(countTeam) < Int32.Parse(countBoard))
            {
                MessageBox.Show("Số đội tham gia không được nhỏ hơn số bảng đấu");
                return false;
            }
            return true;
        }
        void AddLeague(AddLeagueWindow parameter)
        {
            string name = InputFormat.Instance.FomartSpace(parameter.tbUsername.Text);
            string sponsor = InputFormat.Instance.FomartSpace(parameter.tbSponsor.Text);
            string countTeam = InputFormat.Instance.FomartSpace(parameter.tbCountOfTeams.Text);
            string countBoard = "1";
            if (parameter.cbTypeOfLeague.Text == "Chia bảng đấu")
                countBoard = parameter.cbCountOfGroups.Text[0].ToString() + parameter.cbCountOfGroups.Text[1].ToString();


            if (!IsValidInformation(name,
                               sponsor,
                               countTeam,
                               parameter.datePicker.Text,
                               parameter.imgLeagueLogo.Source.ToString(),
                               parameter.cbTypeOfLeague.Text,
                               countBoard))
                return;
            if (parameter.league == null)
            {
                //League league = new League(sponsor, name, 0, DateTime.Parse(parameter.datePicker.ToString()), ImageProcessing.Instance.convertImgToByte(imaged), Int32.Parse(countTeam));
                League league = new League(sponsor, name, 0, parameter.datePicker.SelectedDate.Value, ImageProcessing.Instance.convertImgToByte(imaged), Int32.Parse(countTeam), parameter.cbTypeOfLeague.SelectedIndex, Int32.Parse(countBoard));
                LeagueDAO.Instance.CreateLeague(league);
                league.id = LeagueDAO.Instance.GetNewestLeagurId();

                CreateBoard(parameter.cbTypeOfLeague.SelectedIndex, Int32.Parse(countTeam), Int32.Parse(countBoard), league.id);
                MessageBox.Show("Tạo mùa giải thành công");
                parameter.Close();
            }
            else
            {
                League league;
                if (imaged == null)
                    league = new League(sponsor, name, 0, DateTime.Parse(parameter.datePicker.ToString()), parameter.league.logo, Int32.Parse(countTeam), parameter.cbTypeOfLeague.SelectedIndex, Int32.Parse(countBoard));
                else
                    league = new League(sponsor, name, 0, DateTime.Parse(parameter.datePicker.ToString()), ImageProcessing.Instance.convertImgToByte(imaged), Int32.Parse(countTeam), parameter.cbTypeOfLeague.SelectedIndex, Int32.Parse(countBoard));
                league.id = parameter.league.id;
                SettingDAO.Instance.EditSetting(league.id, "NumberOfTeams", league.countTeam.ToString());
                if (parameter.league.countBoard != league.countBoard || parameter.league.typeLeague!= league.typeLeague)
                {
                    BoardDAO.Instance.DeleteBoardInLeague(league.id);
                    CreateBoard(parameter.cbTypeOfLeague.SelectedIndex, Int32.Parse(countTeam), Int32.Parse(countBoard), league.id);
                }

                LeagueDAO.Instance.UpdateLeague(league);
                List<Board> boards = BoardDAO.Instance.GetListBoard(league.id);
                foreach (Board board in boards)
                {
                    board.countTeam = league.countTeam / league.countBoard;
                    if (league.countTeam%league.countBoard>0)
                    {
                        board.countTeam++;
                    }    

                    BoardDAO.Instance.Update(board);
                }    
                MessageBox.Show("Sửa mùa giải thành công");
                parameter.Close();
            }
            void CreateBoard(int type, int countTeam, int countBoard, int idLeague)
            {
                int countTeamInBoard = countTeam / countBoard;
                if (countTeam % countBoard > 0)
                    countTeamInBoard++;
                switch (type)
                {
                    case 0:
                        Board board = new Board(idLeague, "Bảng đấu vòng", countTeam);
                        BoardDAO.Instance.CreateBoard(board);
                        break;
                    case 1:
                        board = new Board(idLeague, "Bảng đấu loại trực tiếp", countTeam);
                        BoardDAO.Instance.CreateBoard(board);
                        break;
                    case 2:
                        for (int i = 0; i < countBoard; i++)
                        {
                            board = new Board(idLeague,"Bảng " +((char)(i+65)).ToString(), countTeamInBoard);
                            BoardDAO.Instance.CreateBoard(board);
                        }
                        break;
                }
            }
        }
    }
}
