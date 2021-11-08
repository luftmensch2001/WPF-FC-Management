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
            MessageBox.Show("xóa thông tin thay người");
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
            MessageBox.Show("Xóa bàn thắng");
        }
        
        public void DeleteCard(ucCard parameter)
        {
            MessageBox.Show("Xóa thẻ phạt");
        }
        
        public void AddGoal(AddGoalWindow parameter)
        {
            if (parameter.cbAsssist.SelectedIndex >= 0 && parameter.cbTypeOfGoal.SelectedIndex >= 0 && parameter.tbTime.Text != null)
            {
                int idTeam = parameter.isTeam1 ? parameter.resultWD.team1.id : parameter.resultWD.team2.id;
                parameter.resultWD.AddNewGoal(parameter.isTeam1, new Goal(parameter.resultWD.match.id,
                                                                          parameter.players[parameter.cbScored.SelectedIndex].id,
                                                                          parameter.players[parameter.cbAsssist.SelectedIndex].id,
                                                                          idTeam,
                                                                          parameter.typeOfGoals[parameter.cbTypeOfGoal.SelectedIndex].id,
                                                                          parameter.tbTime.Text)
                    );
                parameter.Close();
            }
        }
        public void CancelGoal(AddGoalWindow parameter)
        {
            parameter.Close();
        }
        public void AddCard(AddCardWindow parameter)
        {
            if (parameter.cbPlayer.SelectedIndex >= 0 && parameter.cbTypeOfCard.SelectedIndex >= 0 && parameter.tbTime != null)
            {
                ComboBoxItem cbxTypeOfCard = (ComboBoxItem)parameter.cbTypeOfCard.SelectedItem;
                string typeOfCard = cbxTypeOfCard.Content.ToString();


                parameter.resultWD.AddCard(parameter.isTeam1, 
                    new Card(parameter.resultWD.match.id, 
                             parameter.players[parameter.cbPlayer.SelectedIndex].id, 
                             (parameter.isTeam1 ? parameter.resultWD.team1.id : parameter.resultWD.team2.id), 
                             typeOfCard, 
                             parameter.tbTime.Text));
                parameter.Close();
            }
        }
        public void CancelCard(AddCardWindow parameter)
        {
            parameter.Close();
        }
        public void OKSwitchedPlayers(SwitchPlayersWindow parameter)
        {
            if (parameter.cbPlayer2.SelectedIndex >= 0 && parameter.cbPlayer1.SelectedIndex >= 0 && parameter.tbTime.Text != "")
            {
                parameter.resultWD.SwitchPlayer(parameter.cbPlayer2.SelectedIndex, parameter.cbPlayer1.SelectedIndex, parameter.tbTime.Text.ToString());
                parameter.Close();
            }
        }
        public void CancelSwitchedPlayers(SwitchPlayersWindow parameter)
        {
            parameter.Close();
        }
    }
}
