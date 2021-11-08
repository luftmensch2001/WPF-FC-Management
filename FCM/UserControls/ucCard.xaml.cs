using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FCM.UserControls
{
    /// <summary>
    /// Interaction logic for ucCard.xaml
    /// </summary>
    public partial class ucCard : UserControl
    {
        public ucCard()
        {
            InitializeComponent();
        }
        public ucCard(string numberUniform, string NamePlayer, string minute, string typeOfCard)
        {
            InitializeComponent();

            this.tblNumber.Text = numberUniform;
            this.tblFootballer.Text = NamePlayer;
            this.tblTime.Text = minute;
            this.tblTypeOfCard.Text = typeOfCard;
        }
    }
}
