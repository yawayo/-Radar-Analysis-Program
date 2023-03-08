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
using System.Windows.Shapes;

namespace Radar_Analysis_Program
{
    /// <summary>
    /// fSetting.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class fSetting : Window
    {
        public fSetting()
        {
            InitializeComponent();
        }



        private void Form_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Form_Resize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == System.Windows.WindowState.Maximized)
                this.WindowState = System.Windows.WindowState.Normal;
            else if (this.WindowState == System.Windows.WindowState.Normal)
                this.WindowState = System.Windows.WindowState.Maximized;
        }
        private void Form_Hide_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }
    }
}
