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

        private void Filter_Write_btn_Click(object sender, RoutedEventArgs e)  // NOW저장, 전달.
        {
            Set_Filter_Value();
            Set_Filter_Form();
        }
        public void Set_Filter_Value()
        {
            if (Filter_NofObj_Active_btn.IsChecked == true)
            {
                Filter_NofObj_MIN_value.Content = Filter_NofObj_MIN_input.Text;
                Filter_NofObj_MAX_value.Content = Filter_NofObj_MAX_input.Text;
                Window1.Filter_NofObj_MIN = int.Parse(Filter_NofObj_MIN_input.Text);
                Window1.Filter_NofObj_MAX = int.Parse(Filter_NofObj_MAX_input.Text);
            }
            if (Filter_Distance_Active_btn.IsChecked == true)
            {
                Filter_Distance_MIN_value.Content = Filter_Distance_MIN_input.Text;
                Filter_Distance_MAX_value.Content = Filter_Distance_MAX_input.Text;
                Window1.Filter_Distance_MIN = Double.Parse(Filter_Distance_MIN_input.Text);
                Window1.Filter_Distance_MAX = Double.Parse(Filter_Distance_MAX_input.Text);
            }
            if (Filter_Azimuth_Active_btn.IsChecked == true)
            {
                Filter_Azimuth_MIN_value.Content = Filter_Azimuth_MIN_input.Text;
                Filter_Azimuth_MAX_value.Content = Filter_Azimuth_MIN_input.Text;
                Window1.Filter_Azimuth_MIN = Double.Parse(Filter_Azimuth_MIN_input.Text);
                Window1.Filter_Azimuth_MAX = Double.Parse(Filter_Azimuth_MAX_input.Text);
            }
            if (Filter_VrelOncome_Active_btn.IsChecked == true)
            {
                Filter_VrelOncome_MIN_value.Content = Filter_VrelOncome_MIN_input.Text;
                Filter_VrelOncome_MAX_value.Content = Filter_VrelOncome_MAX_input.Text;
                Window1.Filter_VrelOncome_MIN = Double.Parse(Filter_VrelOncome_MIN_input.Text);
                Window1.Filter_VrelOncome_MAX = Double.Parse(Filter_VrelOncome_MAX_input.Text);
            }
            if (Filter_VrelDepart_Active_btn.IsChecked == true)
            {
                Filter_VrelDepart_MIN_value.Content = Filter_VrelDepart_MIN_input.Text;
                Filter_VrelDepart_MAX_value.Content = Filter_VrelDepart_MAX_input.Text;
                Window1.Filter_VrelDepart_MIN = Double.Parse(Filter_VrelDepart_MIN_input.Text);
                Window1.Filter_VrelDepart_MAX = Double.Parse(Filter_VrelDepart_MAX_input.Text);
            }
            if (Filter_RCS_Active_btn.IsChecked == true)
            {
                Filter_RCS_MIN_value.Content = Filter_RCS_MIN_input.Text;
                Filter_RCS_MAX_value.Content = Filter_RCS_MAX_input.Text;
                Window1.Filter_RCS_MIN = Double.Parse(Filter_RCS_MIN_input.Text);
                Window1.Filter_RCS_MAX = Double.Parse(Filter_RCS_MAX_input.Text);
            }
            if (Filter_Lifetime_Active_btn.IsChecked == true)
            {
                Filter_Lifetime_MIN_value.Content = Filter_Lifetime_MIN_input.Text;
                Filter_Lifetime_MAX_value.Content = Filter_Lifetime_MAX_input.Text;
                Window1.Filter_Lifetime_MIN = Double.Parse(Filter_Lifetime_MIN_input.Text);
                Window1.Filter_Lifetime_MAX = Double.Parse(Filter_Lifetime_MAX_input.Text);
            }
            if (Filter_Size_Active_btn.IsChecked == true)
            {
                Filter_Size_MIN_value.Content = Filter_Size_MIN_input.Text;
                Filter_Size_MAX_value.Content = Filter_Size_MAX_input.Text;
                Window1.Filter_Size_MIN = Double.Parse(Filter_Size_MIN_input.Text);
                Window1.Filter_Size_MAX = Double.Parse(Filter_Size_MAX_input.Text);
            }
            if (Filter_ProbExists_Active_btn.IsChecked == true)
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
                Window1.Filter_ProbExists_MIN = int.Parse(Filter_ProbExists_MIN_input.Text);
                Window1.Filter_ProbExists_MAX = int.Parse(Filter_ProbExists_MAX_input.Text);
            }
            if (Filter_Y_Active_btn.IsChecked == true)
            {
                Filter_Y_MIN_value.Content = Filter_Y_MIN_input.Text;
                Filter_Y_MAX_value.Content = Filter_Y_MAX_input.Text;
                Window1.Filter_Y_MIN = Double.Parse(Filter_Y_MIN_input.Text);
                Window1.Filter_Y_MAX = Double.Parse(Filter_Y_MAX_input.Text);
            }
            if (Filter_X_Active_btn.IsChecked == true)
            {
                Filter_X_MIN_value.Content = Filter_X_MIN_input.Text;
                Filter_X_MAX_value.Content = Filter_X_MAX_input.Text;
                Window1.Filter_X_MIN = Double.Parse(Filter_X_MIN_input.Text);
                Window1.Filter_X_MAX = Double.Parse(Filter_X_MAX_input.Text);
            }
            if (Filter_VYRightLeft_Active_btn.IsChecked == true)
            {
                Filter_VYRightLeft_MIN_value.Content = Filter_VYRightLeft_MIN_input.Text;
                Filter_VYRightLeft_MAX_value.Content = Filter_VYRightLeft_MAX_input.Text;
                Window1.Filter_VYRightLeft_MIN = Double.Parse(Filter_VYRightLeft_MIN_input.Text);
                Window1.Filter_VYRightLeft_MAX = Double.Parse(Filter_VYRightLeft_MAX_input.Text);

            }
            if (Filter_VXOncome_Active_btn.IsChecked == true)
            {
                Filter_VXOncome_MIN_value.Content = Filter_VXOncome_MIN_input.Text;
                Filter_VXOncome_MAX_value.Content = Filter_VXOncome_MAX_input.Text;
                Window1.Filter_VXOncome_MIN = Double.Parse(Filter_VXOncome_MIN_input.Text);
                Window1.Filter_VXOncome_MAX = Double.Parse(Filter_VXOncome_MAX_input.Text);
            }
            if (Filter_VYLeftRight_Active_btn.IsChecked == true)
            {
                Filter_VYLeftRight_MIN_value.Content = Filter_VYLeftRight_MIN_input.Text;
                Filter_VYLeftRight_MAX_value.Content = Filter_VYLeftRight_MAX_input.Text;
                Window1.Filter_VYLeftRight_MIN = Double.Parse(Filter_VYLeftRight_MIN_input.Text);
                Window1.Filter_VYLeftRight_MAX = Double.Parse(Filter_VYLeftRight_MAX_input.Text);
            }
            if (Filter_VXDepart_Active_btn.IsChecked == true)
            {
                Filter_VXDepart_MIN_value.Content = Filter_VXDepart_MIN_input.Text;
                Filter_VXDepart_MAX_value.Content = Filter_VXDepart_MAX_input.Text;
                Window1.Filter_VXDepart_MIN = Double.Parse(Filter_VXDepart_MIN_input.Text);
                Window1.Filter_VXDepart_MAX = Double.Parse(Filter_VXDepart_MAX_input.Text);
            }
        }
        public void Set_Filter_Form()
        {
            #region LEFT
            if (Window1.Filter_NofObj_ACTIVE)
            {
                Filter_NofObj_now_Label.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF48F3D"));
                Filter_NofObj_MIN_value.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF48F3D"));
                Filter_NofObj_MAX_value.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF48F3D"));
            }
            else
            {
                Filter_NofObj_now_Label.Foreground = Brushes.White;
                Filter_NofObj_MIN_value.Foreground = Brushes.White;
                Filter_NofObj_MAX_value.Foreground = Brushes.White;
            }
            if (Window1.Filter_Distance_ACTIVE)
            {
                Filter_Distance_now_Label.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF48F3D"));
                Filter_Distance_MIN_value.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF48F3D"));
                Filter_Distance_MAX_value.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF48F3D"));
            }
            else
            {
                Filter_Distance_now_Label.Foreground = Brushes.White;
                Filter_Distance_MIN_value.Foreground = Brushes.White;
                Filter_Distance_MAX_value.Foreground = Brushes.White;
            }
            if (Window1.Filter_Azimuth_ACTIVE)
            {
                Filter_Azimuth_now_Label.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF48F3D"));
                Filter_Azimuth_MIN_value.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF48F3D"));
                Filter_Azimuth_MAX_value.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF48F3D"));
            }
            else
            {
                Filter_Azimuth_now_Label.Foreground = Brushes.White;
                Filter_Azimuth_MIN_value.Foreground = Brushes.White;
                Filter_Azimuth_MAX_value.Foreground = Brushes.White;
            }
            if (Window1.Filter_VrelOncome_ACTIVE)
            {
                Filter_VrelOncome_now_Label.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF48F3D"));
                Filter_VrelOncome_MIN_value.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF48F3D"));
                Filter_VrelOncome_MAX_value.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF48F3D"));
            }
            else
            {
                Filter_VrelOncome_now_Label.Foreground = Brushes.White;
                Filter_VrelOncome_MIN_value.Foreground = Brushes.White;
                Filter_VrelOncome_MAX_value.Foreground = Brushes.White;
            }
            if (Window1.Filter_VrelDepart_ACTIVE)
            {
                Filter_VrelDepart_now_Label.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF48F3D"));
                Filter_VrelDepart_MIN_value.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF48F3D"));
                Filter_VrelDepart_MAX_value.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF48F3D"));
            }
            else
            {
                Filter_VrelDepart_now_Label.Foreground = Brushes.White;
                Filter_VrelDepart_MIN_value.Foreground = Brushes.White;
                Filter_VrelDepart_MAX_value.Foreground = Brushes.White;
            }
            if (Window1.Filter_RCS_ACTIVE)
            {
                Filter_RCS_now_Label.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF48F3D"));
                Filter_RCS_MIN_value.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF48F3D"));
                Filter_RCS_MAX_value.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF48F3D"));
            }
            else
            {
                Filter_RCS_now_Label.Foreground = Brushes.White;
                Filter_RCS_MIN_value.Foreground = Brushes.White;
                Filter_RCS_MAX_value.Foreground = Brushes.White;
            }
            if (Window1.Filter_Lifetime_ACTIVE)
            {
                Filter_Lifetime_now_Label.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF48F3D"));
                Filter_Lifetime_MIN_value.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF48F3D"));
                Filter_Lifetime_MAX_value.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF48F3D"));
            }
            else
            {
                Filter_Lifetime_now_Label.Foreground = Brushes.White;
                Filter_Lifetime_MIN_value.Foreground = Brushes.White;
                Filter_Lifetime_MAX_value.Foreground = Brushes.White;
            }
            if (Window1.Filter_Size_ACTIVE)
            {
                Filter_Size_now_Label.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF48F3D"));
                Filter_Size_MIN_value.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF48F3D"));
                Filter_Size_MAX_value.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF48F3D"));
            }
            else
            {
                Filter_Size_now_Label.Foreground = Brushes.White;
                Filter_Size_MIN_value.Foreground = Brushes.White;
                Filter_Size_MAX_value.Foreground = Brushes.White;
            }
            if (Window1.Filter_ProbExists_ACTIVE)
            {
                Filter_ProbExists_now_Label.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF48F3D"));
                Filter_ProbExists_MIN_value.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF48F3D"));
                Filter_ProbExists_MAX_value.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF48F3D"));
            }
            else
            {
                Filter_ProbExists_now_Label.Foreground = Brushes.White;
                Filter_ProbExists_MIN_value.Foreground = Brushes.White;
                Filter_ProbExists_MAX_value.Foreground = Brushes.White;
            }
            if (Window1.Filter_Y_ACTIVE)
            {
                Filter_Y_now_Label.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF48F3D"));
                Filter_Y_MIN_value.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF48F3D"));
                Filter_Y_MAX_value.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF48F3D"));
            }
            else
            {
                Filter_Y_now_Label.Foreground = Brushes.White;
                Filter_Y_MIN_value.Foreground = Brushes.White;
                Filter_Y_MAX_value.Foreground = Brushes.White;
            }
            if (Window1.Filter_X_ACTIVE)
            {
                Filter_X_now_Label.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF48F3D"));
                Filter_X_MIN_value.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF48F3D"));
                Filter_X_MAX_value.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF48F3D"));
            }
            else
            {
                Filter_X_now_Label.Foreground = Brushes.White;
                Filter_X_MIN_value.Foreground = Brushes.White;
                Filter_X_MAX_value.Foreground = Brushes.White;
            }
            if (Window1.Filter_VYRightLeft_ACTIVE)
            {
                Filter_VYRightLeft_now_Label.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF48F3D"));
                Filter_VYRightLeft_MIN_value.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF48F3D"));
                Filter_VYRightLeft_MAX_value.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF48F3D"));
            }
            else
            {
                Filter_VYRightLeft_now_Label.Foreground = Brushes.White;
                Filter_VYRightLeft_MIN_value.Foreground = Brushes.White;
                Filter_VYRightLeft_MAX_value.Foreground = Brushes.White;
            }
            if (Window1.Filter_VXOncome_ACTIVE)
            {
                Filter_VXOncome_now_Label.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF48F3D"));
                Filter_VXOncome_MIN_value.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF48F3D"));
                Filter_VXOncome_MAX_value.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF48F3D"));
            }
            else
            {
                Filter_VXOncome_now_Label.Foreground = Brushes.White;
                Filter_VXOncome_MIN_value.Foreground = Brushes.White;
                Filter_VXOncome_MAX_value.Foreground = Brushes.White;
            }
            if (Window1.Filter_VYLeftRight_ACTIVE)
            {
                Filter_VYLeftRight_now_Label.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF48F3D"));
                Filter_VYLeftRight_MIN_value.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF48F3D"));
                Filter_VYLeftRight_MAX_value.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF48F3D"));
            }
            else
            {
                Filter_VYLeftRight_now_Label.Foreground = Brushes.White;
                Filter_VYLeftRight_MIN_value.Foreground = Brushes.White;
                Filter_VYLeftRight_MAX_value.Foreground = Brushes.White;
            }
            if (Window1.Filter_VXDepart_ACTIVE)
            {
                Filter_VXDepart_now_Label.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF48F3D"));
                Filter_VXDepart_MIN_value.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF48F3D"));
                Filter_VXDepart_MAX_value.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF48F3D"));
            }
            else
            {
                Filter_VXDepart_now_Label.Foreground = Brushes.White;
                Filter_VXDepart_MIN_value.Foreground = Brushes.White;
                Filter_VXDepart_MAX_value.Foreground = Brushes.White;
            }
            Filter_NofObj_MIN_value.Content = Window1.Filter_NofObj_MIN.ToString();
            Filter_NofObj_MAX_value.Content = Window1.Filter_NofObj_MAX.ToString();
            Filter_Distance_MIN_value.Content = Window1.Filter_Distance_MIN.ToString("F1");
            Filter_Distance_MAX_value.Content = Window1.Filter_Distance_MAX.ToString("F1");
            Filter_Azimuth_MIN_value.Content = Window1.Filter_Azimuth_MIN.ToString("F3");
            Filter_Azimuth_MAX_value.Content = Window1.Filter_Azimuth_MAX.ToString("F3");
            Filter_VrelOncome_MIN_value.Content = Window1.Filter_VrelOncome_MIN.ToString("F4");
            Filter_VrelOncome_MAX_value.Content = Window1.Filter_VrelOncome_MAX.ToString("F4");
            Filter_VrelDepart_MIN_value.Content = Window1.Filter_VrelDepart_MIN.ToString("F4");
            Filter_VrelDepart_MAX_value.Content = Window1.Filter_VrelDepart_MAX.ToString("F4");
            Filter_RCS_MIN_value.Content = Window1.Filter_RCS_MIN.ToString("F3");
            Filter_RCS_MAX_value.Content = Window1.Filter_RCS_MAX.ToString("F3");
            Filter_Lifetime_MIN_value.Content = Window1.Filter_Lifetime_MIN.ToString("F1");
            Filter_Lifetime_MAX_value.Content = Window1.Filter_Lifetime_MAX.ToString("F1");
            Filter_Size_MIN_value.Content = Window1.Filter_Size_MIN.ToString("F3");
            Filter_Size_MAX_value.Content = Window1.Filter_Size_MAX.ToString("F3");
            Filter_ProbExists_MIN_value.Content = Window1.Filter_ProbExists_MIN.ToString();
            Filter_ProbExists_MAX_value.Content = Window1.Filter_ProbExists_MAX.ToString();
            Filter_Y_MIN_value.Content = Window1.Filter_Y_MIN.ToString("F1");
            Filter_Y_MAX_value.Content = Window1.Filter_Y_MAX.ToString("F1");
            Filter_X_MIN_value.Content = Window1.Filter_X_MIN.ToString("F1");
            Filter_X_MAX_value.Content = Window1.Filter_X_MAX.ToString("F1");
            Filter_VYRightLeft_MIN_value.Content = Window1.Filter_VYRightLeft_MIN.ToString("F4");
            Filter_VYRightLeft_MAX_value.Content = Window1.Filter_VYRightLeft_MAX.ToString("F4");
            Filter_VXOncome_MIN_value.Content = Window1.Filter_VXOncome_MIN.ToString("F4");
            Filter_VXOncome_MAX_value.Content = Window1.Filter_VXOncome_MAX.ToString("F4");
            Filter_VYLeftRight_MIN_value.Content = Window1.Filter_VYLeftRight_MIN.ToString("F4");
            Filter_VYLeftRight_MAX_value.Content = Window1.Filter_VYLeftRight_MAX.ToString("F4");
            Filter_VXDepart_MIN_value.Content = Window1.Filter_VXDepart_MIN.ToString("F4");
            Filter_VXDepart_MAX_value.Content = Window1.Filter_VXDepart_MAX.ToString("F4");
            #endregion

            #region RIGHT
            Filter_NofObj_Active_btn.IsChecked = Window1.Filter_NofObj_ACTIVE;
            Filter_Distance_Active_btn.IsChecked = Window1.Filter_Distance_ACTIVE;
            Filter_Azimuth_Active_btn.IsChecked = Window1.Filter_Azimuth_ACTIVE;
            Filter_VrelOncome_Active_btn.IsChecked = Window1.Filter_VrelOncome_ACTIVE;
            Filter_VrelDepart_Active_btn.IsChecked = Window1.Filter_VrelDepart_ACTIVE;
            Filter_RCS_Active_btn.IsChecked = Window1.Filter_RCS_ACTIVE;
            Filter_Lifetime_Active_btn.IsChecked = Window1.Filter_Lifetime_ACTIVE;
            Filter_Size_Active_btn.IsChecked = Window1.Filter_Size_ACTIVE;
            Filter_ProbExists_Active_btn.IsChecked = Window1.Filter_ProbExists_ACTIVE;
            Filter_Y_Active_btn.IsChecked = Window1.Filter_Y_ACTIVE;
            Filter_X_Active_btn.IsChecked = Window1.Filter_X_ACTIVE;
            Filter_VYRightLeft_Active_btn.IsChecked = Window1.Filter_VYRightLeft_ACTIVE;
            Filter_VXOncome_Active_btn.IsChecked = Window1.Filter_VXOncome_ACTIVE;
            Filter_VYLeftRight_Active_btn.IsChecked = Window1.Filter_VYLeftRight_ACTIVE;
            Filter_VXDepart_Active_btn.IsChecked = Window1.Filter_VXDepart_ACTIVE;

            if (Window1.Filter_NofObj_ACTIVE)
            {
                Filter_NofObj_MIN_input.Text = Window1.Filter_NofObj_MIN.ToString();
                Filter_NofObj_MAX_input.Text = Window1.Filter_NofObj_MAX.ToString();
            }
            else
            {
                Filter_NofObj_MIN_input.Text = "";
                Filter_NofObj_MAX_input.Text = "";
            }
            if (Window1.Filter_Distance_ACTIVE)
            {
                Filter_Distance_MIN_input.Text = Window1.Filter_Distance_MIN.ToString("F1");
                Filter_Distance_MAX_input.Text = Window1.Filter_Distance_MAX.ToString("F1");
            }
            else
            {
                Filter_Distance_MIN_input.Text = "";
                Filter_Distance_MAX_input.Text = "";
            }
            if (Window1.Filter_Azimuth_ACTIVE)
            {
                Filter_Azimuth_MIN_input.Text = Window1.Filter_Azimuth_MIN.ToString("F3");
                Filter_Azimuth_MAX_input.Text = Window1.Filter_Azimuth_MAX.ToString("F3");
            }
            else
            {
                Filter_Azimuth_MIN_input.Text = "";
                Filter_Azimuth_MAX_input.Text = "";
            }
            if (Window1.Filter_VrelOncome_ACTIVE)
            {
                Filter_VrelOncome_MIN_input.Text = Window1.Filter_VrelOncome_MIN.ToString("F4");
                Filter_VrelOncome_MAX_input.Text = Window1.Filter_VrelOncome_MAX.ToString("F4");
            }
            else
            {
                Filter_VrelOncome_MIN_input.Text = "";
                Filter_VrelOncome_MAX_input.Text = "";
            }
            if (Window1.Filter_VrelDepart_ACTIVE)
            {
                Filter_VrelDepart_MIN_input.Text = Window1.Filter_VrelDepart_MIN.ToString("F4");
                Filter_VrelDepart_MAX_input.Text = Window1.Filter_VrelDepart_MAX.ToString("F4");
            }
            else
            {
                Filter_VrelDepart_MIN_input.Text = "";
                Filter_VrelDepart_MAX_input.Text = "";
            }
            if (Window1.Filter_RCS_ACTIVE)
            {
                Filter_RCS_MIN_input.Text = Window1.Filter_RCS_MIN.ToString("F3");
                Filter_RCS_MAX_input.Text = Window1.Filter_RCS_MAX.ToString("F3");
            }
            else
            {
                Filter_RCS_MIN_input.Text = "";
                Filter_RCS_MAX_input.Text = "";
            }
            if (Window1.Filter_Lifetime_ACTIVE)
            {
                Filter_Lifetime_MIN_input.Text = Window1.Filter_Lifetime_MIN.ToString("F1");
                Filter_Lifetime_MAX_input.Text = Window1.Filter_Lifetime_MAX.ToString("F1");
            }
            else
            {
                Filter_Lifetime_MIN_input.Text = "";
                Filter_Lifetime_MAX_input.Text = "";
            }
            if (Window1.Filter_Size_ACTIVE)
            {
                Filter_Size_MIN_input.Text = Window1.Filter_Size_MIN.ToString("F3");
                Filter_Size_MAX_input.Text = Window1.Filter_Size_MAX.ToString("F3");
            }
            else
            {
                Filter_Size_MIN_input.Text = "";
                Filter_Size_MAX_input.Text = "";
            }
            if (Window1.Filter_ProbExists_ACTIVE)
            {
                Filter_ProbExists_MIN_input.Text = Window1.Filter_ProbExists_MIN.ToString();
                Filter_ProbExists_MAX_input.Text = Window1.Filter_ProbExists_MAX.ToString();
            }
            else
            {
                Filter_ProbExists_MIN_input.Text = "";
                Filter_ProbExists_MAX_input.Text = "";
            }
            if (Window1.Filter_Y_ACTIVE)
            {
                Filter_Y_MIN_input.Text = Window1.Filter_Y_MIN.ToString("F1");
                Filter_Y_MAX_input.Text = Window1.Filter_Y_MAX.ToString("F1");
            }
            else
            {
                Filter_Y_MIN_input.Text = "";
                Filter_Y_MAX_input.Text = "";
            }
            if (Window1.Filter_X_ACTIVE)
            {
                Filter_X_MIN_input.Text = Window1.Filter_X_MIN.ToString("F1");
                Filter_X_MAX_input.Text = Window1.Filter_X_MAX.ToString("F1");
            }
            else
            {
                Filter_X_MIN_input.Text = "";
                Filter_X_MAX_input.Text = "";
            }
            if (Window1.Filter_VYRightLeft_ACTIVE)
            {
                Filter_VYRightLeft_MIN_input.Text = Window1.Filter_VYRightLeft_MIN.ToString("F4");
                Filter_VYRightLeft_MAX_input.Text = Window1.Filter_VYRightLeft_MAX.ToString("F4");
            }
            else
            {
                Filter_VYRightLeft_MIN_input.Text = "";
                Filter_VYRightLeft_MAX_input.Text = "";
            }
            if (Window1.Filter_VXOncome_ACTIVE)
            {
                Filter_VXOncome_MIN_input.Text = Window1.Filter_VXOncome_MIN.ToString("F4");
                Filter_VXOncome_MAX_input.Text = Window1.Filter_VXOncome_MAX.ToString("F4");
            }
            else
            {
                Filter_VXOncome_MIN_input.Text = "";
                Filter_VXOncome_MAX_input.Text = "";
            }
            if (Window1.Filter_VYLeftRight_ACTIVE)
            {
                Filter_VYLeftRight_MIN_input.Text = Window1.Filter_VYLeftRight_MIN.ToString("F4");
                Filter_VYLeftRight_MAX_input.Text = Window1.Filter_VYLeftRight_MAX.ToString("F4");
            }
            else
            {
                Filter_VYLeftRight_MIN_input.Text = "";
                Filter_VYLeftRight_MAX_input.Text = "";
            }
            if (Window1.Filter_VXDepart_ACTIVE)
            {
                Filter_VXDepart_MIN_input.Text = Window1.Filter_VXDepart_MIN.ToString("F4");
                Filter_VXDepart_MAX_input.Text = Window1.Filter_VXDepart_MAX.ToString("F4");
            }
            else
            {
                Filter_VXDepart_MIN_input.Text = "";
                Filter_VXDepart_MAX_input.Text = "";
            }
            #endregion
        }

    }
}
