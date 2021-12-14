using System.Windows;

namespace FCM.View
{
    /// <summary>
    /// Interaction logic for MessageBoxWindow.xaml
    /// </summary>
    public partial class MessageBoxWindow : Window
    {
        public MessageBoxWindow()
        {
            InitializeComponent();
        }

        public MessageBoxWindow(bool isSuccess, string content)
        {
            InitializeComponent();
            if (isSuccess) 
            {
                imgSuccess.Visibility = Visibility.Visible;
                imgError.Visibility = Visibility.Hidden;
            }
            else
            {
                imgSuccess.Visibility = Visibility.Hidden;
                imgError.Visibility = Visibility.Visible;
            }
            tblMessage.Text = content;
        }
    }
}
