﻿using FCM.DAO;
using FCM.DTO;
using FCM.UserControls;
using FCM.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace FCM.ViewModel
{
    public class ResultViewModel : BaseViewModel
    {
        public ICommand SwitchTabCommand { get; set; }
        public ICommand GetUidCommand { get; set; }
        public ICommand SwitchPlayersCommand { get; set; }
        public ICommand ExitCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand AddCardTeam1Command { get; set; }
        public ICommand AddCardTeam2Command { get; set; }
        public ICommand ChangeCbxTeamInLineupsWindowCommand { get; set; }
        public ICommand AddToOfficalLineupsCommand { get; set; }
        public ICommand AddScoreTeam1Command { get; set; }
        public ICommand AddScoreTeam2Command { get; set; }
        public ICommand EditGoalCommand { get; set; }
        public ICommand DeleteGoalCommand { get; set; }
        public ICommand EditCardInforCommand { get; set; }
        public ICommand DeleteCardCommand { get; set; }
        public ICommand EditSwitchedPlayerCommand { get; set; }
        public ICommand DeleteSwitchedPlayerCommand { get; set; }
        public ICommand AddGoalCommand { get; set; }
        public ICommand CancelGoalCommand { get; set; }
        public ICommand AddCardCommand { get; set; }
        public ICommand CancelCardCommand { get; set; }
        public ICommand OKSwitchedPlayersCommand { get; set; }
        public ICommand CancelSwitchedPlayersCommand { get; set; }

        public ICommand OpenPenaltyWindowCommand { get; set; }
        public ICommand ExitPenaltyCommand { get; set; }
        public ICommand SavePenaltyCommand { get; set; }
        
        public SolidColorBrush lightGreen = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#52ff00"));
        public SolidColorBrush white = new SolidColorBrush(Colors.White);

        public string uid;
        public ResultViewModel()
        {
            SwitchTabCommand = new RelayCommand<ResultRecordingWindow>((parameter) => true, (parameter) => SwitchTab(parameter));
            ExitCommand = new RelayCommand<ResultRecordingWindow>((parameter) => true, (parameter) => parameter.Close());
            GetUidCommand = new RelayCommand<Button>((parameter) => true, (parameter) => uid = parameter.Uid);
            SwitchPlayersCommand = new RelayCommand<ResultRecordingWindow>((parameter) => true, (parameter) => OpenSwitchPlayersWindow(parameter));

            SaveCommand = new RelayCommand<ResultRecordingWindow>((parameter) => true, (parameter) => SaveUpdateResultInfor(parameter));
            AddCardTeam1Command = new RelayCommand<ResultRecordingWindow>((parameter) => true, (parameter) => AddCardTeam1(parameter));
            AddCardTeam2Command = new RelayCommand<ResultRecordingWindow>((parameter) => true, (parameter) => AddCardTeam2(parameter));
            ChangeCbxTeamInLineupsWindowCommand = new RelayCommand<ResultRecordingWindow>((parameter) => true, (parameter) => ChangeCbxTeamInLineupsWindow(parameter));
            AddToOfficalLineupsCommand = new RelayCommand<ResultRecordingWindow>((parameter) => true, (parameter) => AddToOfficialLineups(parameter));
            AddScoreTeam1Command = new RelayCommand<ResultRecordingWindow>((parameter) => true, (parameter) => AddScoreTeam1(parameter));
            AddScoreTeam2Command = new RelayCommand<ResultRecordingWindow>((parameter) => true, (parameter) => AddScoreTeam2(parameter));
            EditGoalCommand = new RelayCommand<ucGoal>((parameter) => true, (parameter) => EditGoalInfor(parameter));
            DeleteGoalCommand = new RelayCommand<ucGoal>((parameter) => true, (parameter) => DeleteGoal(parameter));
            EditCardInforCommand = new RelayCommand<ucCard>((parameter) => true, (parameter) => EditCardInfor(parameter));
            DeleteCardCommand = new RelayCommand<ucCard>((parameter) => true, (parameter) => DeleteCard(parameter));
            
            EditSwitchedPlayerCommand = new RelayCommand<ucSwitchedPlayers>((parameter) => true, (parameter) => EditSwitchedPlayer(parameter));
            DeleteSwitchedPlayerCommand = new RelayCommand<ucSwitchedPlayers>((parameter) => true, (parameter) => DeleteSwitchedPlayer(parameter));
            AddGoalCommand = new RelayCommand<AddGoalWindow>((parameter) => true, (parameter) => AddGoal(parameter));
            CancelGoalCommand = new RelayCommand<AddGoalWindow>((parameter) => true, (parameter) => CancelGoal(parameter));

            AddCardCommand = new RelayCommand<AddCardWindow>((parameter) => true, (parameter) => AddCard(parameter));
            CancelCardCommand = new RelayCommand<AddCardWindow>((parameter) => true, (parameter) => CancelCard(parameter));

            OKSwitchedPlayersCommand = new RelayCommand<SwitchPlayersWindow>((parameter) => true, (parameter) => OKSwitchedPlayers(parameter));
            CancelSwitchedPlayersCommand = new RelayCommand<SwitchPlayersWindow>((parameter) => true, (parameter) => CancelSwitchedPlayers(parameter));

            OpenPenaltyWindowCommand = new RelayCommand<ResultRecordingWindow>((parameter) => true, (parameter) => OpenPenaltyWindow(parameter));
            ExitPenaltyCommand = new RelayCommand<PenaltyWindow>((parameter) => true, (parameter) => ClosePenaltyWindow(parameter));
            SavePenaltyCommand = new RelayCommand<PenaltyWindow>((parameter) => true, (parameter) => SavePenaltyWindow(parameter));
        }

        public void SavePenaltyWindow(PenaltyWindow parameter)
        {
            int penaltyScoreTeam1 = parameter.cbScoreTeam1.SelectedIndex;
            int penaltyScoreTeam2 = parameter.cbScoreTeam2.SelectedIndex;

            if (penaltyScoreTeam1 == penaltyScoreTeam2)
            {
                MessageBox.Show("Kết quả luân lưu không thể hòa", "Lưu ý", MessageBoxButton.OK, MessageBoxImage.Information);
            }    
            else
            {
                parameter.resultWD.penaltyTeam1 = penaltyScoreTeam1;
                parameter.resultWD.penaltyTeam2 = penaltyScoreTeam2;

                parameter.resultWD.ShowPenaltyResult();

                ClosePenaltyWindow(parameter);
            }    
        }

        public void ClosePenaltyWindow(PenaltyWindow parameter)
        {
            parameter.Close();
        }

        public void OpenPenaltyWindow(ResultRecordingWindow parameter)
        {
            if (parameter.match.allowDraw == true)
            {
                MessageBox.Show("Trận đấu này được phép hòa, do đó không cần tới kết quả luân lưu", "Lưu ý", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }    

            if (parameter.ScoreTeam1 == parameter.ScoreTeam2)
            {
                PenaltyWindow wd = new PenaltyWindow(parameter);
                wd.ShowDialog();
            }    
            else
            {
                MessageBox.Show("Bạn chỉ cần lưu kết quả luân lưu khi 2 đội hòa", "Lưu ý", MessageBoxButton.OK, MessageBoxImage.Information);
            }   
        }

        public void SwitchTab(ResultRecordingWindow parameter)
        {
            int index = int.Parse(uid); // tab index
            //Move Stroke Tab
            parameter.rtStroke.Margin = new Thickness((20 + 120 * index), 0, 0, 5);

            // Reset color
            parameter.btnGoalsTab.Foreground = white;
            parameter.btnCardsTab.Foreground = white;
            parameter.btnFormationsTab.Foreground = white;

            // Hide all screens
            parameter.grdGoalsTab.Visibility = Visibility.Hidden;
            parameter.grdCardsTab.Visibility = Visibility.Hidden;
            parameter.grdFormationsTab.Visibility = Visibility.Hidden;

            switch (index)
            {
                case 0:
                    parameter.btnGoalsTab.Foreground = lightGreen;
                    parameter.grdGoalsTab.Visibility = Visibility.Visible;
                    break;
                case 1:
                    parameter.btnCardsTab.Foreground = lightGreen;
                    parameter.grdCardsTab.Visibility = Visibility.Visible;
                    break;
                case 2:
                    parameter.btnFormationsTab.Foreground = lightGreen;
                    parameter.grdFormationsTab.Visibility = Visibility.Visible;
                    break;
            }
        }
        public void OpenSwitchPlayersWindow(ResultRecordingWindow parameter)
        {
            SwitchPlayersWindow wd = new SwitchPlayersWindow(parameter);
            wd.ShowDialog();
        }
        public void EditSwitchedPlayer(ucSwitchedPlayers parameter)
        {
            SwitchPlayersWindow wd = new SwitchPlayersWindow(parameter.resultWD);
            wd.ShowDialog();
        }
        public void DeleteSwitchedPlayer(ucSwitchedPlayers parameter)
        {
            //parameter.resultWD.ShowListSwitched();
            parameter.resultWD.DeleteSwitched(parameter.switchedPlayer);
        }
        public void SaveUpdateResultInfor(ResultRecordingWindow parameter)
        {
            parameter.UpdateDatabase();
            MessageBox.Show("Lưu thông tin mới thành công", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            parameter.Close();
        }
        public void AddCardTeam1(ResultRecordingWindow parameter)
        {
            AddCardWindow wd = new AddCardWindow(parameter, true, parameter.team1);
            wd.ShowDialog();
        }
        public void AddCardTeam2(ResultRecordingWindow parameter)
        {
            AddCardWindow wd = new AddCardWindow(parameter, false, parameter.team2);
            wd.ShowDialog();
        }
        public void EditCardInfor(ucCard parameter)
        {
            //AddCardWindow wd = new AddCardWindow(true);
            //wd.ShowDialog();
        }
        public void ChangeCbxTeamInLineupsWindow(ResultRecordingWindow parameter)
        {
            parameter.LoadLineups(parameter.cbSelectedTeam.SelectedIndex);
        }
        public void AddToOfficialLineups(ResultRecordingWindow parameter)
        {
            parameter.AddPlayerToOfficialLineups();
        }
        public void AddScoreTeam1(ResultRecordingWindow parameter)
        {
            AddGoalWindow wd = new AddGoalWindow(true, parameter);
            wd.ShowDialog();
        }
        public void AddScoreTeam2(ResultRecordingWindow parameter)
        {
            AddGoalWindow wd = new AddGoalWindow(false, parameter);
            wd.ShowDialog();
        }
        public void EditGoalInfor(ucGoal parameter)
        {
            //AddGoalWindow wd = new AddGoalWindow();
            //wd.ShowDialog();
        }
        public void DeleteGoal(ucGoal parameter)
        {
            parameter.resultWD.DeleteGoal(new Goal(parameter.resultWD.match.id,
                                                    parameter.goal.idPlayerGoals,
                                                    parameter.goal.idPlayerAssist,
                                                    parameter.goal.idTeams,
                                                    parameter.goal.idTypeOfGoals,
                                                    parameter.tblTime.Text));

            if (parameter.resultWD.match.idTeam01 == parameter.goal.idTeams)
            {
                parameter.resultWD.ScoreTeam1--;
            }
            else
            {
                parameter.resultWD.ScoreTeam2--;
            }

            parameter.resultWD.ShowResultMatch();
        }
        public void DeleteCard(ucCard parameter)
        {
            Card c = new Card(parameter.resultWD.match.id,
                                                    parameter.card.idPlayer,
                                                    parameter.card.idTeams,
                                                    parameter.card.typeOfCard,
                                                    parameter.card.time);

            parameter.resultWD.ChangeLineupsWhenChangeCardInfor(c, true);

            parameter.resultWD.Deletecard(c);

            
        }
        public void AddGoal(AddGoalWindow parameter)
        {
            if (parameter.cbTypeOfGoal.SelectedIndex < 0)
            {
                MessageBox.Show("Thiếu thông tin về loại bàn thắng", "Thiếu thông tin", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!isExactTime(parameter.tbTime.Text))
            {
                MessageBox.Show("Thông tin về thời gian phải là định dạng số và nằm trong khoảng [0,120]", "Sai thông tin", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int idMatch = parameter.resultWD.match.id;
            int idPlayerScored = parameter.players[parameter.cbScored.SelectedIndex].id;
            int idPlayerAssist = parameter.cbAsssist.SelectedIndex == -1 ? idPlayerScored : parameter.players[parameter.cbAsssist.SelectedIndex].id;
            int idTeam = parameter.isTeam1 ? parameter.resultWD.team1.id : parameter.resultWD.team2.id;
            int idTypeOfGoal = parameter.typeOfGoals[parameter.cbTypeOfGoal.SelectedIndex].id;
            string timeGoal = parameter.tbTime.Text;

            Player playerScore = PlayerDAO.Instance.GetPlayerById(idPlayerScored);
            Player playerAssist = PlayerDAO.Instance.GetPlayerById(idPlayerAssist);

            int timeReveiveRedCardOfScorePlayer = parameter.resultWD.GetTimePlayerReceiveRedCard(playerScore);
            int timeReveiveRedCardOfAssistPlayer = parameter.resultWD.GetTimePlayerReceiveRedCard(playerAssist);


            if (Int32.Parse(timeGoal) > timeReveiveRedCardOfScorePlayer)
            {
                MessageBox.Show("Cầu thủ ghi bàn đã bị nhận thẻ đỏ và ra khỏi sân trước đó!\nDo đó cầu thủ này không thể ghi bàn", "Sai thông tin thời gian", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (Int32.Parse(timeGoal) > timeReveiveRedCardOfAssistPlayer)
            {
                MessageBox.Show("Cầu thủ kiến tạo đã bị nhận thẻ đỏ và ra khỏi sân trước đó!\nDo đó cầu thủ này không thể kiến tạo", "Sai thông tin thời gian", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (parameter.tbTime.Text != null)
            {
                

                Goal goal = new Goal(idMatch, idPlayerScored, idPlayerAssist, idTeam, idTypeOfGoal, timeGoal);

                parameter.resultWD.AddNewGoal(parameter.isTeam1, goal);
                parameter.Close();
            }
        }
        public void CancelGoal(AddGoalWindow parameter)
        {
            parameter.Close();
        }
        public void AddCard(AddCardWindow parameter)
        {
            if (!isExactTime(parameter.tbTime.Text))
            {
                MessageBox.Show("Thông tin về thời gian phải là định dạng số và nằm trong khoảng [0,120]", "Sai thông tin", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            // *** Đổi thành radio nên đóng tạm câu if phía dưới để run
            //if (parameter.cbPlayer.SelectedIndex >= 0 && parameter.cbTypeOfCard.SelectedIndex >= 0)
            //{
            //    ComboBoxItem cbxTypeOfCard = (ComboBoxItem)parameter.cbTypeOfCard.SelectedItem;
            //    string typeOfCard = cbxTypeOfCard.Content.ToString();

            //    int idTeam = parameter.isTeam1 ? parameter.resultWD.team1.id : parameter.resultWD.team2.id;

            //    Player player = parameter.GetPlayerBySelectedIndex(parameter.cbPlayer.SelectedIndex);

            //    bool isRedCard = false;

            //    Card c = new Card(parameter.resultWD.match.id, player.id, idTeam,  typeOfCard, parameter.tbTime.Text);

            //    parameter.resultWD.AddCard(parameter.isTeam1, c);

            //    // Nếu đã từng nhận 1 thẻ vàng trước đó, bây giờ nhận tiếp 1 thẻ vàng thì trực tiếp thêm 1 thẻ đỏ
            //    if (typeOfCard == "Thẻ vàng" && parameter.resultWD.HaveAYellowCard(player) == true)
            //    {
            //        c = new Card(parameter.resultWD.match.id, player.id, idTeam, "Thẻ đỏ", parameter.tbTime.Text);

            //        isRedCard = true;

            //        parameter.resultWD.AddCard(parameter.isTeam1, c);
            //    }    

            //    if (typeOfCard == "Thẻ đỏ")
            //    {
            //        isRedCard = true;
            //    }    

            //    if (isRedCard)
            //    {
            //        parameter.resultWD.DeleteFromOfficial(player);
            //        parameter.resultWD.InsertIntoPrep(player);
            //    }    

            //    parameter.resultWD.ChangeLineupsWhenChangeCardInfor(c);

            //    parameter.Close();
            //}
        }
        public void CancelCard(AddCardWindow parameter)
        {
            parameter.Close();
        }
        public void OKSwitchedPlayers(SwitchPlayersWindow parameter)
        {
            if (!isExactTime(parameter.tbTime.Text))
            {
                MessageBox.Show("Thông tin về thời gian phải là định dạng số và nằm trong khoảng [0,120]", "Sai thông tin", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (parameter.cbPlayer2.SelectedIndex >= 0 && parameter.cbPlayer1.SelectedIndex >= 0)
            {
                parameter.resultWD.SwitchPlayer(parameter.cbPlayer2.SelectedIndex, parameter.cbPlayer1.SelectedIndex, parameter.tbTime.Text.ToString());
                parameter.Close();
            }
        }
        public void CancelSwitchedPlayers(SwitchPlayersWindow parameter)
        {
            parameter.Close();
        }

        bool isExactTime(string s)
        {
            if (s == "") return false;

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] < '0' || s[i] > '9')
                {
                    return false;
                }
            }

            int time = Int32.Parse(s);
            
            if (time > 120 || time < 0)
            {
                return false;
            }

            return true;
        }
    }
}
