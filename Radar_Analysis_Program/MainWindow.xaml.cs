using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Radar_Analysis_Program
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {


        public MySqlConnection connection;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            string serverNameTextBox = ServerNameTextBox.Text;
            string serverPortTextBox = ServerPortTextBox.Text;
            string dBnameTextBox = DBnameTextBox.Text;
            string iDTextBox = IDTextBox.Text;
            string passwordBox = PasswordBox.Password;

            string connectionString = "Server="+serverNameTextBox+";Port="+serverPortTextBox+ "; Database="+dBnameTextBox+ ";Uid="+iDTextBox+ ";Pwd="+passwordBox+";";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MessageBox.Show("접속 성공");
                    Window1 testWindow = new Window1(connection);
                    this.Close();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("접속 실패, 다시 시도");
            }
         

            //MessageBox.Show(passwordBox);
         

            //testWindow.Show();
        }
    }
}
