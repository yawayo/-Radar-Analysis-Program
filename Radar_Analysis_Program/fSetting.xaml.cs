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

            #region Value 
            Filter_NofObj_MIN_value.Content = "0";
            Filter_NofObj_MAX_value.Content = "4095";
            Filter_Distance_MIN_value.Content = "0";
            Filter_Distance_MAX_value.Content = "409.5";
            Filter_Azimuth_MIN_value.Content = "-50";
            Filter_Azimuth_MAX_value.Content = "52.375";
            Filter_VrelOncome_MIN_value.Content = "0";
            Filter_VrelOncome_MAX_value.Content = "128.993";
            Filter_VrelDepart_MIN_value.Content = "0";
            Filter_VrelDepart_MAX_value.Content = "128.993";
            Filter_RCS_MIN_value.Content = "-50";
            Filter_RCS_MAX_value.Content = "52.375";
            Filter_Lifetime_MIN_value.Content = "0";
            Filter_Lifetime_MAX_value.Content = "409.5";
            Filter_Size_MIN_value.Content = "0";
            Filter_Size_MAX_value.Content = "102.375";
            Filter_ProbExists_MIN_value.Content = "0";
            Filter_ProbExists_MAX_value.Content = "7";
            Filter_Y_MIN_value.Content = "-409.5";
            Filter_Y_MAX_value.Content = "409.5";
            Filter_X_MIN_value.Content = "-500";
            Filter_X_MAX_value.Content = "1138.2";
            Filter_VYRightLeft_MIN_value.Content = "0";
            Filter_VYRightLeft_MAX_value.Content = "128.993";
            Filter_VXOncome_MIN_value.Content = "0";
            Filter_VXOncome_MAX_value.Content = "128.993";
            Filter_VYLeftRight_MIN_value.Content = "0";
            Filter_VYLeftRight_MAX_value.Content = "128.993";
            Filter_VXDepart_MIN_value.Content = "0";
            Filter_VXDepart_MAX_value.Content = "128.993";
            #endregion




        }

        #region Change_MIN
        public string Change_Filter_NofObj_MIN_input = "";
        public string Change_Filter_Distance_MIN_input = "";
        public string Change_Filter_Azimuth_MIN_input = "";
        public string Change_Filter_VrelOncome_MIN_input = "";
        public string Change_Filter_VrelDepart_MIN_input = "";
        public string Change_Filter_RCS_MIN_input = "";
        public string Change_Filter_Lifetime_MIN_input = "";
        public string Change_Filter_Size_MIN_input = "";
        public string Change_Filter_ProbExists_MIN_input = "";
        public string Change_Filter_Y_MIN_input = "";
        public string Change_Filter_X_MIN_input = "";
        public string Change_Filter_VYRightLeft_MIN_input = "";
        public string Change_Filter_VXOncome_MIN_input = "";
        public string Change_Filter_VYLeftRight_MIN_input = "";
        public string Change_Filter_VXDepart_MIN_input = "";
        #endregion
        #region Change_MAX
        public string Change_Filter_NofObj_MAX_input = "";
        public string Change_Filter_Distance_MAX_input = "";
        public string Change_Filter_Azimuth_MAX_input = "";
        public string Change_Filter_VrelOncome_MAX_input = "";
        public string Change_Filter_VrelDepart_MAX_input = "";
        public string Change_Filter_RCS_MAX_input = "";
        public string Change_Filter_Lifetime_MAX_input = "";
        public string Change_Filter_Size_MAX_input = "";
        public string Change_Filter_ProbExists_MAX_input = "";
        public string Change_Filter_Y_MAX_input = "";
        public string Change_Filter_X_MAX_input = "";
        public string Change_Filter_VYRightLeft_MAX_input = "";
        public string Change_Filter_VXOncome_MAX_input = "";
        public string Change_Filter_VYLeftRight_MAX_input = "";
        public string Change_Filter_VXDepart_MAX_input = "";
        #endregion
        #region form_Click
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
        #endregion

        #region min,max 저장
        private void NofObj_Active_btn_Click(object sender, RoutedEventArgs e)
        {
            Change_Filter_NofObj_MIN_input = Filter_NofObj_MIN_input.Text;
            Change_Filter_NofObj_MAX_input = Filter_NofObj_MAX_input.Text;
        }
        private void Distance_Active_btn_Click(object sender, RoutedEventArgs e)
        {
            Change_Filter_Distance_MIN_input = Filter_Distance_MIN_input.Text;
            Change_Filter_Distance_MAX_input = Filter_Distance_MAX_input.Text;
        }
        private void Azimuth_Active_btn_Click(object sender, RoutedEventArgs e)
        {
            Change_Filter_Azimuth_MIN_input = Filter_Azimuth_MIN_input.Text;
            Change_Filter_Azimuth_MAX_input = Filter_Azimuth_MAX_input.Text;
        } 
        private void VrelOncome_Active_btn_Click(object sender, RoutedEventArgs e)
        {
            Change_Filter_VrelOncome_MIN_input = Filter_VrelOncome_MIN_input.Text;
            Change_Filter_VrelOncome_MAX_input = Filter_VrelOncome_MAX_input.Text;
        }
        private void VrelDepart_Active_btn_Click(object sender, RoutedEventArgs e)
        {
            Change_Filter_VrelDepart_MIN_input = Filter_VrelDepart_MIN_input.Text;
            Change_Filter_VrelDepart_MAX_input = Filter_VrelDepart_MAX_input.Text;
        }
        private void RCS_Active_btn_Click(object sender, RoutedEventArgs e)
        {
            Change_Filter_RCS_MIN_input = Filter_RCS_MIN_input.Text;
            Change_Filter_RCS_MAX_input = Filter_RCS_MAX_input.Text;
        }
        private void Lifetime_Active_btn_Click(object sender, RoutedEventArgs e)
        {
            Change_Filter_Lifetime_MIN_input = Filter_Lifetime_MIN_input.Text;
            Change_Filter_Lifetime_MAX_input = Filter_Lifetime_MAX_input.Text;
        }
        private void Size_Active_btn_Click(object sender, RoutedEventArgs e)
        {
            Change_Filter_Size_MIN_input = Filter_Size_MIN_input.Text;
            Change_Filter_Size_MAX_input = Filter_Size_MAX_input.Text;
        }
        private void ProbExists_Active_btn_Click(object sender, RoutedEventArgs e)
        {
            Change_Filter_ProbExists_MIN_input = Filter_ProbExists_MIN_input.Text;
            Change_Filter_ProbExists_MAX_input = Filter_ProbExists_MAX_input.Text;
        }
        private void Y_Active_btn_Click(object sender, RoutedEventArgs e)
        {
            Change_Filter_Y_MIN_input = Filter_Y_MIN_input.Text;
            Change_Filter_Y_MAX_input = Filter_Y_MAX_input.Text;
        }
        private void X_Active_btn_Click(object sender, RoutedEventArgs e)
        {
            Change_Filter_X_MIN_input = Filter_X_MIN_input.Text;
            Change_Filter_X_MAX_input = Filter_X_MAX_input.Text;
        }
        private void VYRightLeft_Active_btn_Click(object sender, RoutedEventArgs e)
        {
            Change_Filter_VYRightLeft_MIN_input = Filter_VYRightLeft_MIN_input.Text;
            Change_Filter_VYRightLeft_MAX_input = Filter_VYRightLeft_MAX_input.Text;
        }
        private void VXOncome_Active_btn_Click(object sender, RoutedEventArgs e)
        {
            Change_Filter_VXOncome_MIN_input = Filter_VXOncome_MIN_input.Text;
            Change_Filter_VXOncome_MAX_input = Filter_VXOncome_MAX_input.Text;
        }
        private void VYLeftRight_Active_btn_Click(object sender, RoutedEventArgs e)
        {
            Change_Filter_VYLeftRight_MIN_input = Filter_VYLeftRight_MIN_input.Text;
            Change_Filter_VYLeftRight_MAX_input = Filter_VYLeftRight_MAX_input.Text;
        }
        private void VXDepart_Active_btn_Click(object sender, RoutedEventArgs e)
        {
            Change_Filter_VXDepart_MIN_input = Filter_VXDepart_MIN_input.Text;
            Change_Filter_VXDepart_MAX_input = Filter_VXDepart_MAX_input.Text;
        }
        #endregion


        private void Filter_Write_btn_Click(object sender, RoutedEventArgs e)  // NOW저장, 전달.
        {
          if(Filter_NofObj_Active_btn.IsChecked == true)
            {
                Filter_NofObj_MIN_value.Content = Filter_NofObj_MIN_input.Text;
                Filter_NofObj_MAX_value.Content = Filter_NofObj_MAX_input.Text;
                Window1.Change_Filter_NofObj_MIN_input = Filter_NofObj_MIN_input.Text;
                Window1.Change_Filter_NofObj_MAX_input = Filter_NofObj_MAX_input.Text;
            }
          if(Filter_Distance_Active_btn.IsChecked == true)
            {
                Filter_Distance_MIN_value.Content = Filter_Distance_MIN_input.Text;
                Filter_Distance_MAX_value.Content = Filter_Distance_MAX_input.Text;
                Window1.Change_Filter_Distance_MIN_input = Filter_Distance_MIN_input.Text;
                Window1.Change_Filter_Distance_MAX_input = Filter_Distance_MAX_input.Text;
            }
          if(Filter_Azimuth_Active_btn.IsChecked == true)
            {
                Filter_Azimuth_MIN_value.Content = Filter_Azimuth_MIN_input.Text;
                Filter_Azimuth_MAX_value.Content = Filter_Azimuth_MIN_input.Text;
                Window1.Change_Filter_Azimuth_MIN_input = Filter_Azimuth_MIN_input.Text;
                Window1.Change_Filter_Azimuth_MAX_input = Filter_Azimuth_MAX_input.Text;
            }
          if(Filter_VrelOncome_Active_btn.IsChecked == true)
            {
                Filter_VrelOncome_MIN_value.Content = Filter_VrelOncome_MIN_input.Text;
                Filter_VrelOncome_MAX_value.Content = Filter_VrelOncome_MAX_input.Text;
                Window1.Change_Filter_VrelOncome_MIN_input = Filter_VrelOncome_MIN_input.Text;
                Window1.Change_Filter_VrelOncome_MAX_input = Filter_VrelOncome_MAX_input.Text;
            }
          if(Filter_VrelDepart_Active_btn.IsChecked == true)
            {
                Filter_VrelDepart_MIN_value.Content = Filter_VrelDepart_MIN_input.Text;
                Filter_VrelDepart_MAX_value.Content = Filter_VrelDepart_MAX_input.Text;
                Window1.Change_Filter_VrelDepart_MIN_input = Filter_VrelDepart_MIN_input.Text;
                Window1.Change_Filter_VrelDepart_MAX_input = Filter_VrelDepart_MAX_input.Text;
            }
          if(Filter_RCS_Active_btn.IsChecked == true)
            {
                Filter_RCS_MIN_value.Content = Filter_RCS_MIN_input.Text;
                Filter_RCS_MAX_value.Content = Filter_RCS_MAX_input.Text;
                Window1.Change_Filter_RCS_MIN_input = Filter_RCS_MIN_input.Text;
                Window1.Change_Filter_RCS_MAX_input = Filter_RCS_MAX_input.Text;
            }
          if(Filter_Lifetime_Active_btn.IsChecked == true)
            {
                Filter_Lifetime_MIN_value.Content = Filter_Lifetime_MIN_input.Text;
                Filter_Lifetime_MAX_value.Content = Filter_Lifetime_MAX_input.Text;
                Window1.Change_Filter_Lifetime_MIN_input = Filter_Lifetime_MIN_input.Text;
                Window1.Change_Filter_Lifetime_MAX_input = Filter_Lifetime_MAX_input.Text;
            }
          if(Filter_Size_Active_btn.IsChecked == true)
            {
                Filter_Size_MIN_value.Content = Filter_Size_MIN_input.Text;
                Filter_Size_MAX_value.Content = Filter_Size_MAX_input.Text;
                Window1.Change_Filter_Size_MIN_input = Filter_Size_MIN_input.Text;
                Window1.Change_Filter_Size_MAX_input = Filter_Size_MAX_input.Text;
            }
          if(Filter_ProbExists_Active_btn.IsChecked == true)
            {
                String ProbExists_number_MIN = "a";
                String ProbExists_number_MAX = "a";

                if (Filter_ProbExists_MIN_input.Text == "0 %") ProbExists_number_MIN = "0";
                else if (Filter_ProbExists_MIN_input.Text == "25%") ProbExists_number_MIN = "1";
                else if (Filter_ProbExists_MIN_input.Text == "50%") ProbExists_number_MIN = "2";
                else if (Filter_ProbExists_MIN_input.Text == "75%") ProbExists_number_MIN = "3";
                else if (Filter_ProbExists_MIN_input.Text == "90%") ProbExists_number_MIN = "4";
                else if (Filter_ProbExists_MIN_input.Text == "99%") ProbExists_number_MIN = "5";
                else if (Filter_ProbExists_MIN_input.Text == "99.9%") ProbExists_number_MIN = "6";
                else if (Filter_ProbExists_MIN_input.Text == "100%") ProbExists_number_MIN = "7";

                if (Filter_ProbExists_MAX_input.Text == "0 %") ProbExists_number_MAX = "0";
                else if (Filter_ProbExists_MAX_input.Text == "25%") ProbExists_number_MAX = "1";
                else if (Filter_ProbExists_MAX_input.Text == "50%") ProbExists_number_MAX = "2";
                else if (Filter_ProbExists_MAX_input.Text == "75%") ProbExists_number_MAX = "3";
                else if (Filter_ProbExists_MAX_input.Text == "90%") ProbExists_number_MAX = "4";
                else if (Filter_ProbExists_MAX_input.Text == "99%") ProbExists_number_MAX = "5";
                else if (Filter_ProbExists_MAX_input.Text == "99.9%") ProbExists_number_MAX = "6";
                else if (Filter_ProbExists_MAX_input.Text == "100%") ProbExists_number_MAX = "7";


                Filter_ProbExists_MIN_value.Content = ProbExists_number_MIN;
                Filter_ProbExists_MAX_value.Content = ProbExists_number_MAX;
                Window1.Change_Filter_ProbExists_MIN_input = Filter_ProbExists_MIN_input.Text;
                Window1.Change_Filter_ProbExists_MAX_input = Filter_ProbExists_MAX_input.Text;
            }
          if(Filter_Y_Active_btn.IsChecked == true)
            {
                Filter_Y_MIN_value.Content = Filter_Y_MIN_input.Text;
                Filter_Y_MAX_value.Content = Filter_Y_MAX_input.Text;
                Window1.Change_Filter_Y_MIN_input = Filter_Y_MIN_input.Text;
                Window1.Change_Filter_Y_MAX_input = Filter_Y_MAX_input.Text;
            }
          if(Filter_X_Active_btn.IsChecked == true)
            {
                Filter_X_MIN_value.Content = Filter_X_MIN_input.Text;
                Filter_X_MAX_value.Content = Filter_X_MAX_input.Text;
                Window1.Change_Filter_X_MIN_input = Filter_X_MIN_input.Text;
                Window1.Change_Filter_X_MAX_input = Filter_X_MAX_input.Text;
            }
          if(Filter_VYRightLeft_Active_btn.IsChecked == true)
            {
                Filter_VYRightLeft_MIN_value.Content = Filter_VYRightLeft_MIN_input.Text;
                Filter_VYRightLeft_MAX_value.Content = Filter_VYRightLeft_MAX_input.Text;
                Window1.Change_Filter_VYRightLeft_MIN_input = Filter_VYRightLeft_MIN_input.Text;
                Window1.Change_Filter_VYRightLeft_MAX_input = Filter_VYRightLeft_MAX_input.Text;

            }
          if(Filter_VXOncome_Active_btn.IsChecked == true)
            {
                Filter_VXOncome_MIN_value.Content = Filter_VXOncome_MIN_input.Text;
                Filter_VXOncome_MAX_value.Content = Filter_VXOncome_MAX_input.Text;
                Window1.Change_Filter_VXOncome_MIN_input = Filter_VXOncome_MIN_input.Text;
                Window1.Change_Filter_VXOncome_MAX_input = Filter_VXOncome_MAX_input.Text;
            }
          if(Filter_VYLeftRight_Active_btn.IsChecked == true)
            {
                Filter_VYLeftRight_MIN_value.Content = Filter_VYLeftRight_MIN_input.Text;
                Filter_VYLeftRight_MAX_value.Content = Filter_VYLeftRight_MAX_input.Text;
                Window1.Change_Filter_VYLeftRight_MIN_input = Filter_VYLeftRight_MIN_input.Text;
                Window1.Change_Filter_VYLeftRight_MAX_input = Filter_VYLeftRight_MAX_input.Text;
            }
          if(Filter_VXDepart_Active_btn.IsChecked == true)
            {
                Filter_VXDepart_MIN_value.Content = Filter_VXDepart_MIN_input.Text;
                Filter_VXDepart_MAX_value.Content = Filter_VXDepart_MAX_input.Text;
                Window1.Change_Filter_VXDepart_MIN_input = Filter_VXDepart_MIN_input.Text;
                Window1.Change_Filter_VXDepart_MAX_input = Filter_VXDepart_MAX_input.Text;
            }

        }

      
    }
}
