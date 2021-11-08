using FCM.DAO;
using FCM.DTO;
using FCM.UserControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FCM.View
{
    /// <summary>
    /// Interaction logic for ResultRecordingWindow.xaml
    /// </summary>
    public partial class ResultRecordingWindow : Window
    {
        public Match match;
        public Team team1, team2, teamNow;
        public bool isTeam1 = true;
        public int round;

        public List<Goal> listGoalsTeam1 = new List<Goal>();
        public List<Goal> listGoalsTeam2 = new List<Goal>();

        public List<Card> listCardsTeam1 = new List<Card>();
        public List<Card> listCardsTeam2 = new List<Card>();

        public List<Lineups> listLineups_Offical_Team1 = new List<Lineups>();
        public List<Lineups> listLineups_Offical_Team2 = new List<Lineups>();

        public List<Lineups> listLineups_Prep_Team1 = new List<Lineups>();
        public List<Lineups> listLineups_Prep_Team2 = new List<Lineups>();

        public List<SwitchedPlayer> listSwitchedPlayerTeam1 = new List<SwitchedPlayer>();
        public List<SwitchedPlayer> listSwitchedPlayerTeam2 = new List<SwitchedPlayer>();

        public List<TypeOfGoal> typeOfGoals = new List<TypeOfGoal>();

        public int ScoreTeam1 = 0;
        public int ScoreTeam2 = 0;

        public ResultRecordingWindow() 
        {
            InitializeComponent();
        }
       
        public ResultRecordingWindow(Match match)
        {
            InitializeComponent();

            Init(match);
        }

        void Init(Match match)
        {
            this.match = match;

            this.round = this.match.round;

            this.team1 = TeamDAO.Instance.GetTeamById(this.match.idTeam01);
            this.team2 = TeamDAO.Instance.GetTeamById(this.match.idTeam02);

            this.tblName1.Text = team1.nameTeam;
            this.tblName2.Text = team2.nameTeam;

            this.imgLogo1.Source = ImageProcessing.Instance.Convert(ImageProcessing.Instance.ByteToImg(team1.logo));
            this.imgLogo2.Source = ImageProcessing.Instance.Convert(ImageProcessing.Instance.ByteToImg(team2.logo));

            this.cbSelectedTeam.Items.Clear();
            this.cbSelectedTeam.Items.Add(team1.nameTeam);
            this.cbSelectedTeam.Items.Add(team2.nameTeam);
            this.cbSelectedTeam.SelectedIndex = 0;
           
            this.wpCardsTeam1.Children.Clear();
            this.wpCardsTeam2.Children.Clear();

            this.wpGoalsTeam1.Children.Clear();
            this.wpGoalsTeam2.Children.Clear();

            // Lấy dữ liệu lên
            GetDataFromDatabase();

            LoadLineups(0);
        }
        void GetDataFromDatabase()
        {
            this.listLineups_Offical_Team1 = LineupsDAO.Instance.GetListLineups(this.match.id, this.team1.id, 1);
            this.listLineups_Offical_Team2 = LineupsDAO.Instance.GetListLineups(this.match.id, this.team2.id, 1);

            this.listLineups_Prep_Team1 = LineupsDAO.Instance.GetListLineups(this.match.id, this.team1.id, 0);
            this.listLineups_Prep_Team2 = LineupsDAO.Instance.GetListLineups(this.match.id, this.team2.id, 0);

            // Trận đấu chưa diễn ra
            if (this.listLineups_Prep_Team1.Count == 0 && this.listLineups_Prep_Team2.Count == 0 && this.listLineups_Offical_Team1.Count == 0 && this.listLineups_Offical_Team2.Count == 0)
            {
                SetLineupsWhenNotStarted();
            }
            else
            {
                this.listGoalsTeam1 = GoalDAO.Instance.GetListGoals(this.match.id, this.team1.id);
                this.listGoalsTeam2 = GoalDAO.Instance.GetListGoals(this.match.id, this.team2.id);

                this.listCardsTeam1 = CardDAO.Instance.GetListCards(this.match.id, this.team1.id);
                this.listCardsTeam2 = CardDAO.Instance.GetListCards(this.match.id, this.team2.id);

                this.listSwitchedPlayerTeam1 = SwitchedPlayerDAO.Instance.GetListSwitchedPlayer(this.match.id, this.team1.id);
                this.listSwitchedPlayerTeam2 = SwitchedPlayerDAO.Instance.GetListSwitchedPlayer(this.match.id, this.team2.id);

                this.typeOfGoals = TypeOfGoalDAO.Instance.GetListTypeOfGoal();

                this.ScoreTeam1 = this.listGoalsTeam1.Count;
                this.ScoreTeam2 = this.listGoalsTeam2.Count;
            }
            LoadLineups(0);
            LoadCard();
            LoadGoalListToWindow();

        }
        public void UpdateDatabase()
        {
            // Xóa bỏ dữ liệu cũ
            LineupsDAO.Instance.DeleteLineupsByMatchID(this.match.id);
            GoalDAO.Instance.DeleteGoalByMatchID(this.match.id);
            CardDAO.Instance.DeleteCardByMatchID(this.match.id);
            SwitchedPlayerDAO.Instance.DeleteSwitchedPlayersByMatchID(this.match.id);

            // Đổ vào dữ liệu mới
            foreach (Lineups l in this.listLineups_Offical_Team1)
            {
                LineupsDAO.Instance.AddLinups(l);
            }
            foreach (Lineups l in this.listLineups_Offical_Team2)
            {
                LineupsDAO.Instance.AddLinups(l);
            }
            foreach (Lineups l in this.listLineups_Prep_Team1)
            {
                LineupsDAO.Instance.AddLinups(l);
            }
            foreach (Lineups l in this.listLineups_Prep_Team2)
            {
                LineupsDAO.Instance.AddLinups(l);
            }

            foreach (Goal g in this.listGoalsTeam1)
            {
                GoalDAO.Instance.AddGoal(g);
            }
            foreach (Goal g in this.listGoalsTeam2)
            {
                GoalDAO.Instance.AddGoal(g);
            }
            foreach (Card c in this.listCardsTeam1)
            {
                CardDAO.Instance.AddCard(c);
            }
            foreach (Card c in this.listCardsTeam2)
            {
                CardDAO.Instance.AddCard(c);
            }
            foreach (SwitchedPlayer s in this.listSwitchedPlayerTeam1)
            {
                SwitchedPlayerDAO.Instance.AddSwitchPlayer(s);
            }
            foreach (SwitchedPlayer s in this.listSwitchedPlayerTeam2)
            {
                SwitchedPlayerDAO.Instance.AddSwitchPlayer(s);
            }

        }
        public void WhatTeamIsChosen()
        {
            if (this.cbSelectedTeam.SelectedIndex == 0)
            {
                this.teamNow = team1;
                this.isTeam1 = true;
            }    
            else
            {
                this.teamNow = team2;
                this.isTeam1 = false;
            }    
        }
        void SetLineupsWhenNotStarted()
        {
            WhatTeamIsChosen();

            this.wpSwitched.Children.Clear();

            List<Lineups> lineupsPrep = new List<Lineups>();
            List<Lineups> lineupsOfficial = new List<Lineups>();
            

            List<Player> players = PlayerDAO.Instance.GetListPlayer(this.teamNow.id);

            foreach (Player player in players)
            {
                Lineups lineup = new Lineups(this.match.id, player.id, this.teamNow.id, 0);
                lineupsPrep.Add(lineup);
            }

            if (this.isTeam1)
            {
                listLineups_Offical_Team1 = lineupsOfficial;
                listLineups_Prep_Team1 = lineupsPrep;
            }
            else
            {
                listLineups_Offical_Team2 = lineupsOfficial;
                listLineups_Prep_Team2 = lineupsPrep;
            }
        }
        public void LoadLineups(int numTeam)
        {
            WhatTeamIsChosen();

            if (this.isTeam1)
            {
                if (this.listLineups_Prep_Team1.Count == 0 && this.listLineups_Offical_Team1.Count == 0)
                {
                    SetLineupsWhenNotStarted();
                }    
            }   
            else
            {
                if (this.listLineups_Prep_Team2.Count == 0 && this.listLineups_Offical_Team2.Count == 0)
                {
                    SetLineupsWhenNotStarted();
                }
            }    

            List<Lineups> lineupsOfficial = this.isTeam1 ? this.listLineups_Offical_Team1 : this.listLineups_Offical_Team2;
            List<Lineups> lineupsPrep = this.isTeam1 ? this.listLineups_Prep_Team1 : this.listLineups_Prep_Team2;
            List<SwitchedPlayer> switchedPlayers = this.isTeam1 ? this.listSwitchedPlayerTeam1 : this.listSwitchedPlayerTeam2;

            this.wpMainFormation.Children.Clear();
            this.wpReserveFormation.Children.Clear();
            this.wpSwitched.Children.Clear();

            // Thêm vào danh sách "Đội hình trên sân"
            for (int i = 0; i < lineupsOfficial.Count; i++)
            {
                ucFootballer ucFootballer = new ucFootballer(lineupsOfficial[i]);
                this.wpMainFormation.Children.Add(ucFootballer);
            }

            // Thêm vào danh sách "đội hình dự bị"
            for (int i = 0; i < lineupsPrep.Count; i++)
            {
                ucFootballer ucFootballer = new ucFootballer(lineupsPrep[i]);
                this.wpReserveFormation.Children.Add(ucFootballer);
            }
            
            if (switchedPlayers != null)
            for (int i = 0; i < switchedPlayers.Count; i++)
            {
                this.wpSwitched.Children.Add(new ucSwitchedPlayers(switchedPlayers[i]));
            }

            ResetComboboxAddPlayer();
        }

        public void ResetComboboxAddPlayer()
        {
            WhatTeamIsChosen();

            this.cbSelectPlayerAdd.Items.Clear();

            List<Lineups> lineups;

            if (this.isTeam1)
            {
                lineups = this.listLineups_Prep_Team1;
            }    
            else
            {
                lineups = this.listLineups_Prep_Team2;
            }    

            for (int i = 0; i < lineups.Count; i++)
            {
                // Thêm các cầu thủ ở đội hình dự bị vào combobox "Thêm người vào sân"
                string numberUniform = PlayerDAO.Instance.GetPlayerById(lineups[i].idPlayer).uniformNumber.ToString();
                string namePlayer = PlayerDAO.Instance.GetPlayerById(lineups[i].idPlayer).namePlayer.ToString();
                this.cbSelectPlayerAdd.Items.Add(numberUniform + ". " + namePlayer);
            }    
            
        }
        
        public void SwitchPlayer(int indexIn, int indexOut, string minute)
        {
            int idIn, idOut;

            if (this.isTeam1)
            {
                idIn = this.listLineups_Prep_Team1[indexIn].idPlayer;
                idOut = this.listLineups_Offical_Team1[indexOut].idPlayer;

                ChangeFromPrepToOfficial(this.listLineups_Prep_Team1[indexIn]);
                this.listLineups_Offical_Team1.RemoveAt(indexOut);
                this.listLineups_Prep_Team1.RemoveAt(indexIn);
                this.listLineups_Prep_Team1.Add(new Lineups(this.match.id, idOut, team1.id, 0));


                SwitchedPlayer switchedPlayer = new SwitchedPlayer(this.match.id, idIn, idOut, team1.id, minute);
                this.listSwitchedPlayerTeam1.Add(switchedPlayer);
            }
            else
            {
                idIn = this.listLineups_Prep_Team2[indexIn].idPlayer;
                idOut = this.listLineups_Offical_Team2[indexOut].idPlayer;

                ChangeFromPrepToOfficial(this.listLineups_Prep_Team2[indexIn]);
                this.listLineups_Offical_Team2.RemoveAt(indexOut);
                this.listLineups_Prep_Team2.RemoveAt(indexIn);
                this.listLineups_Prep_Team2.Add(new Lineups(this.match.id, idOut, team2.id, 0));

                SwitchedPlayer switchedPlayer = new SwitchedPlayer(this.match.id, idIn, idOut, team2.id, minute);
                this.listSwitchedPlayerTeam2.Add(switchedPlayer);
            }

            //this.wpSwitched.Children.Add(switchedPlayer);
            LoadLineups(this.cbSelectedTeam.SelectedIndex);
        }
        
        public void AddPlayerToOfficialLineups()
        {
            WhatTeamIsChosen();

            int index = this.cbSelectPlayerAdd.SelectedIndex;

            if (index < 0) return;

            if (this.isTeam1)
            {
                ChangeFromPrepToOfficial(this.listLineups_Prep_Team1[index]);
                this.listLineups_Prep_Team1.RemoveAt(index);
            }    
            else
            {
                ChangeFromPrepToOfficial(this.listLineups_Prep_Team2[index]);
                this.listLineups_Prep_Team2.RemoveAt(index);
            }    

            ResetComboboxAddPlayer();
            LoadLineups(this.cbSelectedTeam.SelectedIndex);
        }
        public void ChangeFromPrepToOfficial(Lineups lineup)
        {
            WhatTeamIsChosen();

            lineup.isOfficial = 1;

            if (this.isTeam1)
            {
                this.listLineups_Offical_Team1.Add(lineup);
            }
            else
            {
                this.listLineups_Offical_Team2.Add(lineup);
            }    
        }

        public void AddCard(bool isTeam1, Card card)
        {
            if (isTeam1)
            {
                this.listCardsTeam1.Add(card);
            }
            else
            {
                this.listCardsTeam2.Add(card);
            }
            LoadCard();
        }
        public void LoadCard()
        {
            this.wpCardsTeam1.Children.Clear();
            this.wpCardsTeam2.Children.Clear();

            for (int i = 0; i < this.listCardsTeam1.Count; i++)
            {
                Card card = this.listCardsTeam1[i];
                int idPlayer = card.idPlayer;
                string uniformNumber = PlayerDAO.Instance.GetPlayerById(idPlayer).uniformNumber.ToString();
                string namePlayer = PlayerDAO.Instance.GetPlayerById(idPlayer).namePlayer.ToString();

                ucCard ucCard = new ucCard(uniformNumber, namePlayer, card.time, card.typeOfCard);

                this.wpCardsTeam1.Children.Add(ucCard);
            }
            for (int i = 0; i < this.listCardsTeam2.Count; i++)
            {
                Card card = this.listCardsTeam2[i];
                int idPlayer = card.idPlayer;
                string uniformNumber = PlayerDAO.Instance.GetPlayerById(idPlayer).uniformNumber.ToString();
                string namePlayer = PlayerDAO.Instance.GetPlayerById(idPlayer).namePlayer.ToString();
                ucCard ucCard = new ucCard(uniformNumber, namePlayer, card.time, card.typeOfCard);

                this.wpCardsTeam2.Children.Add(ucCard);
            }
        }
        public void AddNewGoal(bool isTeam1, Goal newGoal)
        {
            if (isTeam1)
            {
                this.listGoalsTeam1.Add(newGoal);
                this.ScoreTeam1++;
            }
            else
            {
                this.listGoalsTeam2.Add(newGoal);
                this.ScoreTeam2++;
            }
            LoadGoalListToWindow();
        }
        public void LoadGoalListToWindow()
        {
            this.wpGoalsTeam1.Children.Clear();
            this.wpGoalsTeam2.Children.Clear();


            this.tblScore1.Text = this.ScoreTeam1.ToString();
            this.tblScore2.Text = this.ScoreTeam2.ToString();

            for (int i = 0; i < this.listGoalsTeam1.Count; i++)
            {
                ucGoal goal = new ucGoal(this.listGoalsTeam1[i]);
                this.wpGoalsTeam1.Children.Add(goal);
            }
            for (int i = 0; i < this.listGoalsTeam2.Count; i++)
            {
                ucGoal goal = new ucGoal(this.listGoalsTeam2[i]);
                this.wpGoalsTeam2.Children.Add(goal);
            }
        }

    }
}
