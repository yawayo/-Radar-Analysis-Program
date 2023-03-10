using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.Win32;
using MySql.Data.MySqlClient;

namespace Radar_Analysis_Program
{
    /// <summary>
    /// Window1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Window1 : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        public static List<MyDataModel> dataList = new List<MyDataModel>();

        public CheckBox[] checkBoxes;
        public String[] checkbox_name;

        TextBox[] textBoxes = new TextBox[41];
        Rectangle[] rectangles = new Rectangle[41];
        Polyline[] lines = new Polyline[41];
        DateTime[] dates = new DateTime[41];

        DateTime _starttime;
        DateTime _checktime;
        DateTime total_time;
        DateTime dbcompareDT;
        TimeSpan diff;
        TimeSpan diff2;

        private double max_lat = 20;
        private double max_long = 200;
        private Point shift_pos;

        int number = 0; // DB  n 번째 

        double _previousValue_check = 0; //슬라이더 값 +인지 - 인지 체크
        int drag_move_check = 0;
        int speed_check = 0;
        int drag_check = 0;

        string dbcomparetime;
        string text_str = "";
        string textblock1;
        string textblock2;
        string textblock3;
        string textblock4;
        string textblock5;

        string firsttime;
        string secondtime;

        double duration= 0;
        private MySqlConnection conn;

        private float[] Lane_width = new float[6] { 3.3f, 3.3f, 3.3f, 3.3f, 3.3f, 3.3f };
        private float[] Lane_shift = new float[9] { 0.0f, 1.0f, 0.0f, 0.0f, -1.0f, 0.0f, 0.0f, 1.0f, 0.0f};
        private float Dist_Lane_gap = 25.0f;

        #region Change_MIN
        public static string Change_Filter_NofObj_MIN_input = "0";
        public static string Change_Filter_Distance_MIN_input = "0";
        public static string Change_Filter_Azimuth_MIN_input = "-50";
        public static string Change_Filter_VrelOncome_MIN_input = "0";
        public static string Change_Filter_VrelDepart_MIN_input = "0";
        public static string Change_Filter_RCS_MIN_input = "-50";
        public static string Change_Filter_Lifetime_MIN_input = "0";
        public static string Change_Filter_Size_MIN_input = "0";
        public static string Change_Filter_ProbExists_MIN_input = "0";
        public static string Change_Filter_Y_MIN_input = "-409.5";
        public static string Change_Filter_X_MIN_input = "-500";
        public static string Change_Filter_VYRightLeft_MIN_input = "0";
        public static string Change_Filter_VXOncome_MIN_input = "0";
        public static string Change_Filter_VYLeftRight_MIN_input = "0";
        public static string Change_Filter_VXDepart_MIN_input = "0";
        #endregion
        #region Change_MAX
        public static string Change_Filter_NofObj_MAX_input = "4095";
        public static string Change_Filter_Distance_MAX_input = "409.5";
        public static string Change_Filter_Azimuth_MAX_input = "50.375";
        public static string Change_Filter_VrelOncome_MAX_input = "128.993";
        public static string Change_Filter_VrelDepart_MAX_input = "128.993";
        public static string Change_Filter_RCS_MAX_input = "52.375";
        public static string Change_Filter_Lifetime_MAX_input = "409.5";
        public static string Change_Filter_Size_MAX_input = "102.375";
        public static string Change_Filter_ProbExists_MAX_input = "7";
        public static string Change_Filter_Y_MAX_input = "409.5";
        public static string Change_Filter_X_MAX_input = "1138.2";
        public static string Change_Filter_VYRightLeft_MAX_input = "128.993";
        public static string Change_Filter_VXOncome_MAX_input = "128.993";
        public static string Change_Filter_VYLeftRight_MAX_input = "128.993";
        public static string Change_Filter_VXDepart_MAX_input = "128.993";
        #endregion


        public class MyDataModel
        {
            public DateTime time { get; set; }
            public int id { get; set; }
            public double DistLat { get; set; }
            public double DistLong { get; set; }
            public double VrelLat { get; set; }
            public double VrelLong { get; set; }
            public double Velocity { get; set; }
            public double RCS { get; set; }
            public int ProbOfExist { get; set; }
            public int Class { get; set; }
            public double Length { get; set; }
            public double Width { get; set; }
            public int Zone { get; set; }
            public int Lane { get; set; }
            public double distance { get; set; }
            public double size { get; set; }
            public DateTime Timestamp;
        }

        public Window1(MySqlConnection connection)
        {
            conn = connection;
            InitializeComponent();

            //db_connect(conn, firsttime, secondtime);
            CheckBox_setting();

            Update_LaneWidthText();
        }

        private void draw_map()
        {
            int point_num = (int)(max_long / Dist_Lane_gap) + 1;

            #region Lane
            //중앙선 
            for (int p = 0; p < point_num - 1; p++)
            {
                int X1 = (int)((Data_Draw.ActualWidth / 2) + (Data_Draw.ActualWidth / (max_lat * 2)) * Lane_shift[p]);
                int X2 = (int)((Data_Draw.ActualWidth / 2) + (Data_Draw.ActualWidth / (max_lat * 2)) * Lane_shift[p + 1]);
                int Y1 = (int)(Data_Draw.ActualHeight - ((Data_Draw.ActualHeight * Dist_Lane_gap * ((2 * p) + 1)) / (2 * (max_long + Dist_Lane_gap))));
                int Y2 = (int)(Data_Draw.ActualHeight - ((Data_Draw.ActualHeight * Dist_Lane_gap * ((2 * (p + 1)) + 1)) / (2 * (max_long + Dist_Lane_gap))));

                Polyline lane = new Polyline();
                lane.Stroke = Brushes.Yellow;
                lane.StrokeThickness = 3;
                lane.Points = new PointCollection()
                {
                    new Point(X1, Y1),
                    new Point(X2, Y2)
                };
                Data_Draw.Children.Add(lane);
            }

            for (int l = 0; l < 6; l++)
            {
                float d = 0.0f;
                if(l == 0)
                    d = -1.0f * (Lane_width[0] + Lane_width[1] + Lane_width[2]);
                else if (l == 1)
                    d = -1.0f * (Lane_width[1] + Lane_width[2]);
                else if (l == 2)
                    d = -1.0f * (Lane_width[2]);
                else if (l == 3)
                    d = (Lane_width[3] + Lane_width[4] + Lane_width[5]);
                else if (l == 4)
                    d = (Lane_width[3] + Lane_width[4]);
                else
                    d = (Lane_width[3]);

                for (int p = 0; p < point_num - 1; p++)
                {
                    int X1 = (int)((Data_Draw.ActualWidth / 2) + (Data_Draw.ActualWidth / (max_lat * 2)) * (d + Lane_shift[p]));
                    int X2 = (int)((Data_Draw.ActualWidth / 2) + (Data_Draw.ActualWidth / (max_lat * 2)) * (d + Lane_shift[p + 1]));
                    int Y1 = (int)(Data_Draw.ActualHeight - ((Data_Draw.ActualHeight * Dist_Lane_gap * ((2 * p) + 1)) / (2 * (max_long + Dist_Lane_gap))));
                    int Y2 = (int)(Data_Draw.ActualHeight - ((Data_Draw.ActualHeight * Dist_Lane_gap * ((2 * (p + 1)) + 1)) / (2 * (max_long + Dist_Lane_gap))));

                    Polyline lane = new Polyline();
                    lane.Points = new PointCollection()
                    {
                        new Point(X1, Y1),
                        new Point(X2, Y2)
                    };
                    lane.Stroke = Brushes.White;
                    lane.StrokeThickness = 3;
                    Data_Draw.Children.Add(lane);
                }
            }
            #endregion

            #region Dist Line
            for (int line_n = 0; line_n < point_num; line_n++)
            {
                int Y = (int)(Data_Draw.ActualHeight - ((Data_Draw.ActualHeight * Dist_Lane_gap * ((2 * line_n) + 1)) / (2 * (max_long + Dist_Lane_gap))));
                Polyline distline = new Polyline();
                distline.Points = new PointCollection()
                {
                    new Point(0, Y),
                    new Point(Data_Draw.ActualWidth, Y)
                };
                distline.Stroke = Brushes.Yellow;
                distline.StrokeThickness = 1;
                distline.StrokeDashArray = new DoubleCollection() { 15, 15 };
                Data_Draw.Children.Add(distline);


                Grid panel = new Grid(); //사각형을 감싸줄 Panel 생성
                TextBlock textBlock = new TextBlock();//Text 생성
                textBlock.Text = ((int)(Dist_Lane_gap * line_n)).ToString() + "m";
                textBlock.Foreground = Brushes.White;
                panel.Children.Add(textBlock);//Panel에 Text 추가
                Canvas.SetLeft(panel, Data_Draw.ActualWidth - (textBlock.Text.Length + 2) * 5);//Panel 위치 조정
                Canvas.SetTop(panel, Y - 9);
                Data_Draw.Children.Add(panel);//Panel 등록
                //System.Console.WriteLine(textBlock.Text.Length);
            }
            #endregion
        }
        private void draw()
        {
            //Data_Draw.Children.Clear();
                      
            double X;
            double Y;
            try
            {
                if (number == 0 && rectangles[dataList[number].id] == null)
                {
                    if (rectangles[dataList[number].id] == null && _starttime <= dbcompareDT)     //생성 
                    {
                        textblock2 = dataList[number].DistLat.ToString("0.0");
                        textblock3 = dataList[number].DistLong.ToString("0.0");
                        //textblock3.Text = dataList[number].DistLat.ToString("0.0");
                        //textblock2.Text = dataList[number].DistLong.ToString("0.0");

                        X = shift_pos.X + ((-1 * dataList[number].DistLat) * (Data_Draw.ActualWidth / max_lat)) + (Data_Draw.ActualWidth / 2);
                        Y = shift_pos.Y + dataList[number].DistLong * (Data_Draw.ActualHeight / max_long);

                        Rectangle rect = new Rectangle
                        {
                            Stroke = new SolidColorBrush(Color.FromRgb(244, 143, 61)),
                            StrokeThickness = 15
                        };

                        rect.Tag = dataList[number].id;
                        rectangles[dataList[number].id] = rect;

                        DateTime rect_date = DateTime.Now;
                        dates[dataList[number].id] = rect_date;



                        TextBox textBox = new TextBox();
                        textBoxes[dataList[number].id] = textBox;
                        //textBoxes[dataList[number].id].Text = "ID = " + dataList[number].id.ToString() + "\n" + "DistLat = " + dataList[number].DistLat.ToString("0.0") + "\n" + "DistLong = " + dataList[number].DistLong.ToString("0.0");
                        textBoxes[dataList[number].id].Text = CheckBox_print();
                        textBoxes[dataList[number].id].VerticalAlignment = VerticalAlignment.Center;
                        textBoxes[dataList[number].id].Margin = new Thickness(10, 0, 0, 0);

                        Canvas.SetLeft(rectangles[dataList[number].id], X);
                        Canvas.SetTop(rectangles[dataList[number].id], Data_Draw.ActualHeight - Y);

                        Canvas.SetLeft(textBox, Canvas.GetLeft(rect) + 10);
                        Canvas.SetTop(textBox, Data_Draw.ActualHeight - Y - 3);

                        Data_Draw.Children.Add(rectangles[dataList[number].id]);
                        Data_Draw.Children.Add(textBoxes[dataList[number].id]);


                        Filter();
                        number++;
                    }
                }
                else if (number == 0 && rectangles[dataList[number].id] != null)   //드래그 시 number가 0일 때 ,   
                {
                    for (int i = 0; i < 41; i++)
                    {
                        if (rectangles[dataList[number].id] != null && dataList[number].time <= dbcompareDT)  // 이미 존재한
                        {
                            if (rectangles[i] != null && dataList[number].time <= dbcompareDT)
                            {
                                textblock2 = dataList[number].DistLat.ToString("0.0");
                                textblock3 = dataList[number].DistLong.ToString("0.0");


                                X = shift_pos.X + ((-1 * dataList[number].DistLat) * (Data_Draw.ActualWidth / max_lat)) + (Data_Draw.ActualWidth / 2);
                                Y = shift_pos.Y + dataList[number].DistLong * (Data_Draw.ActualHeight / max_long);

                                //textBoxes[dataList[number].id].Text = "ID = " + dataList[number].id.ToString() + "\n" + "DistLat = " + dataList[number].DistLat.ToString("0.0") + "\n" + "DistLong = " + dataList[number].DistLong.ToString("0.0");
                                textBoxes[dataList[number].id].Text = CheckBox_print();
                                textBoxes[dataList[number].id].VerticalAlignment = VerticalAlignment.Center;
                                textBoxes[dataList[number].id].Margin = new Thickness(10, 0, 0, 0);

                                Canvas.SetLeft(rectangles[dataList[number].id], X);
                                Canvas.SetTop(rectangles[dataList[number].id], Data_Draw.ActualHeight - Y);

                                Canvas.SetLeft(textBoxes[dataList[number].id], Canvas.GetLeft(rectangles[dataList[number].id]) + 10);
                                Canvas.SetTop(textBoxes[dataList[number].id], Data_Draw.ActualHeight - Y - 3);

                                if (Data_Draw.Children.Contains(rectangles[dataList[number].id]) == false)
                                {
                                    Data_Draw.Children.Add(rectangles[dataList[number].id]);
                                    Data_Draw.Children.Add(textBoxes[dataList[number].id]);
                                }

                                if (dataList[number].DistLong < 10)
                                {
                                    Data_Draw.Children.Remove(rectangles[dataList[number].id]);
                                    Data_Draw.Children.Remove(textBoxes[dataList[number].id]);
                                }

                                DateTime rect_date = DateTime.Now;
                                dates[dataList[number].id] = rect_date;



                                Filter();
                                number++;

                            }

                        }
                        else if (rectangles[dataList[number].id] == null && dataList[number - 1].time < dbcompareDT && dataList[number].time <= dbcompareDT)     //생성 
                        {
                            textblock2 = dataList[number].DistLat.ToString("0.0");
                            textblock3 = dataList[number].DistLong.ToString("0.0");
                            //textblock3.Text = dataList[number].DistLat.ToString("0.0");
                            //textblock2.Text = dataList[number].DistLong.ToString("0.0");

                            X = shift_pos.X + ((-1 * dataList[number].DistLat) * (Data_Draw.ActualWidth / max_lat)) + (Data_Draw.ActualWidth / 2);
                            Y = shift_pos.Y + dataList[number].DistLong * (Data_Draw.ActualHeight / max_long);

                            Rectangle rect = new Rectangle
                            {
                                Stroke = new SolidColorBrush(Color.FromRgb(244, 143, 61)),
                                StrokeThickness = 15
                            };

                            rect.Tag = dataList[number].id;
                            rectangles[dataList[number].id] = rect;

                            DateTime rect_date = DateTime.Now;
                            dates[dataList[number].id] = rect_date;



                            TextBox textBox = new TextBox();
                            textBoxes[dataList[number].id] = textBox;
                            //textBoxes[dataList[number].id].Text = "ID = " + dataList[number].id.ToString() + "\n" + "DistLat = " + dataList[number].DistLat.ToString("0.0") + "\n" + "DistLong = " + dataList[number].DistLong.ToString("0.0");
                            textBoxes[dataList[number].id].Text = CheckBox_print();
                            textBoxes[dataList[number].id].VerticalAlignment = VerticalAlignment.Center;
                            textBoxes[dataList[number].id].Margin = new Thickness(10, 0, 0, 0);

                            Canvas.SetLeft(rectangles[dataList[number].id], X);
                            Canvas.SetTop(rectangles[dataList[number].id], Data_Draw.ActualHeight - Y);

                            Canvas.SetLeft(textBox, Canvas.GetLeft(rect) + 10);
                            Canvas.SetTop(textBox, Data_Draw.ActualHeight - Y - 3);

                            Data_Draw.Children.Add(rectangles[dataList[number].id]);
                            Data_Draw.Children.Add(textBoxes[dataList[number].id]);

                            if (dataList[number].DistLong < 10)
                            {
                                Data_Draw.Children.Remove(rectangles[dataList[number].id]);
                                Data_Draw.Children.Remove(textBoxes[dataList[number].id]);
                            }
                            //textblock4.Text = number.ToString();


                            Filter();
                            number++;
                        }
                    }
                    textblock4 = number.ToString();
                    textblock5 = dataList[number].distance.ToString("0.0");
                }
                else if (dataList[number - 1].time < dbcompareDT && dataList[number].time <= dbcompareDT)   // DB 안의 time == 지금 작동중인 시간(dbcomapretime)
                {

                    for (int i = 0; i < 41; i++)
                    {

                        if (rectangles[dataList[number].id] != null && dataList[number - 1].time < dbcompareDT && dataList[number].time <= dbcompareDT)  // 이미 존재한
                        {
                            if (rectangles[i] != null && dataList[number].time <= dbcompareDT)
                            {
                                textblock2 = dataList[number].DistLat.ToString("0.0");
                                textblock3 = dataList[number].DistLong.ToString("0.0");


                                X = shift_pos.X + ((-1 * dataList[number].DistLat) * (Data_Draw.ActualWidth / max_lat)) + (Data_Draw.ActualWidth / 2);
                                Y = shift_pos.Y + dataList[number].DistLong * (Data_Draw.ActualHeight / max_long);

                                //textBoxes[dataList[number].id].Text = "ID = " + dataList[number].id.ToString() + "\n" + "DistLat = " + dataList[number].DistLat.ToString("0.0") + "\n" + "DistLong = " + dataList[number].DistLong.ToString("0.0");
                                textBoxes[dataList[number].id].Text = CheckBox_print();
                                textBoxes[dataList[number].id].VerticalAlignment = VerticalAlignment.Center;
                                textBoxes[dataList[number].id].Margin = new Thickness(10, 0, 0, 0);

                                Canvas.SetLeft(rectangles[dataList[number].id], X);
                                Canvas.SetTop(rectangles[dataList[number].id], Data_Draw.ActualHeight - Y);

                                Canvas.SetLeft(textBoxes[dataList[number].id], Canvas.GetLeft(rectangles[dataList[number].id]) + 10);
                                Canvas.SetTop(textBoxes[dataList[number].id], Data_Draw.ActualHeight - Y - 3);

                                if (Data_Draw.Children.Contains(rectangles[dataList[number].id]) == false)
                                {
                                    Data_Draw.Children.Add(rectangles[dataList[number].id]);
                                    Data_Draw.Children.Add(textBoxes[dataList[number].id]);
                                }

                                if (dataList[number].DistLong < 10)
                                {
                                    Data_Draw.Children.Remove(rectangles[dataList[number].id]);
                                    Data_Draw.Children.Remove(textBoxes[dataList[number].id]);
                                }

                                DateTime rect_date = DateTime.Now;
                                dates[dataList[number].id] = rect_date;

                                Filter();
                                number++;

                            }

                        }
                        else if (rectangles[dataList[number].id] == null && dataList[number - 1].time < dbcompareDT && dataList[number].time <= dbcompareDT)     //생성 
                        {
                            textblock2 = dataList[number].DistLat.ToString("0.0");
                            textblock3 = dataList[number].DistLong.ToString("0.0");
                            //textblock3.Text = dataList[number].DistLat.ToString("0.0");
                            //textblock2.Text = dataList[number].DistLong.ToString("0.0");

                            X = shift_pos.X + ((-1 * dataList[number].DistLat) * (Data_Draw.ActualWidth / max_lat)) + (Data_Draw.ActualWidth / 2);
                            Y = shift_pos.Y + dataList[number].DistLong * (Data_Draw.ActualHeight / max_long);

                            Rectangle rect = new Rectangle
                            {
                                Stroke = new SolidColorBrush(Color.FromRgb(244, 143, 61)),
                                StrokeThickness = 15
                            };

                            rect.Tag = dataList[number].id;
                            rectangles[dataList[number].id] = rect;

                            DateTime rect_date = DateTime.Now;
                            dates[dataList[number].id] = rect_date;



                            TextBox textBox = new TextBox();
                            textBoxes[dataList[number].id] = textBox;

                            //textBoxes[dataList[number].id].Text = "ID = " + dataList[number].id.ToString() + "\n" + "DistLat = " + dataList[number].DistLat.ToString("0.0") + "\n" + "DistLong = " + dataList[number].DistLong.ToString("0.0");
                            textBoxes[dataList[number].id].Text = CheckBox_print();
                            textBoxes[dataList[number].id].VerticalAlignment = VerticalAlignment.Center;
                            textBoxes[dataList[number].id].Margin = new Thickness(10, 0, 0, 0);

                            Canvas.SetLeft(rectangles[dataList[number].id], X);
                            Canvas.SetTop(rectangles[dataList[number].id], Data_Draw.ActualHeight - Y);

                            Canvas.SetLeft(textBox, Canvas.GetLeft(rect) + 10);
                            Canvas.SetTop(textBox, Data_Draw.ActualHeight - Y - 3);

                            Data_Draw.Children.Add(rectangles[dataList[number].id]);
                            Data_Draw.Children.Add(textBoxes[dataList[number].id]);

                            if (dataList[number].DistLong < 10)
                            {
                                Data_Draw.Children.Remove(rectangles[dataList[number].id]);
                                Data_Draw.Children.Remove(textBoxes[dataList[number].id]);
                            }
                            //textblock4.Text = number.ToString();


                            Filter();
                            number++;
                        }
                    }
                    textblock4 = number.ToString();
                    textblock5 = dataList[number].distance.ToString("0.0");
                }

            }
            catch
            {

            }


            for (int i = 0; i < 41; i++)
            {
                TimeSpan difTime = DateTime.Now - dates[i];
                if ((difTime.Seconds > 3) || (difTime.Milliseconds > 3000))
                {
                    Data_Draw.Children.Remove(rectangles[i]);
                    Data_Draw.Children.Remove(textBoxes[i]);
                }
            }
            textblock1 = dbcomparetime;
           
           
            //draw_map();
        }
        void TimerTickHandler(object sender, EventArgs e)
        {

            double positionMs = mediaElement.Position.TotalMilliseconds;
            slider.Value = slider.Minimum + positionMs;

            diff = TimeSpan.FromMilliseconds(1);
            diff2 += diff;

            TimeSpan value_time = TimeSpan.FromMilliseconds(slider.Value - slider.Minimum);
         
            if (drag_check == 1)    // 드래그 했을 때 
            {
                if (drag_move_check == 2)   //앞으로 갔을 때 
                {
                    int a = 0;
                    while (a < dataList.Count)
                    {                  
                        try  // number 값이 없을 때 
                        {
                            _checktime = dataList[number].time;
                        }
                        catch
                        {
                            number = 0;
                            break;
                        }

                        if (_starttime.Add(value_time) <= _checktime)    //  _starttime.add(value_time) = 현재 시간  
                        {                                               //   _check_time = db 데이터의 시간 값
                            break;
                        }
                        else
                        {
                            number++;   // ++할수록 checktime 도 앞으로 커짐   
                        }
                        a++;
                    }
                }
                else if (drag_move_check == 1)    // 뒤로 갔을 때
                {
                    int a = 0;
                    while (a < dataList.Count)
                    {                   
                        try   // number 값이 없을 때 
                        {
                            _checktime = dataList[number].time;
                        }
                        catch
                        {
                            number = 0;
                            break;
                        }
                        if (_starttime.Add(value_time) > _checktime)  //  _starttime.add(value_time) = 현재 시간  
                        {                                                //   _check_time = db 데이터의 시간 값
                           
                            number++;
                            if (number == 1)
                            {
                                number = 0;
                            }
                            break;
                        }
                        else
                        {
                            if (number == 0) number = 1;
                            number--;   // --할수록 checktime 도 뒤로 점점 작아짐 
                        }
                        a++;
                    }
                }

                diff2 = value_time;
               
                dbcomparetime = _starttime.Add(diff2).ToString("yyyy-MM-dd HH:mm:ss.fff");
                dbcompareDT = _starttime.Add(diff2);
                drag_move_check = 0;   //움직인 다음 무브체크, 다시 0으로 초기화
                drag_check = 0;       //드래그 했는지 체크, 다시 0으로 초기화

            }
            else if (drag_check == 0)
            {
                dbcomparetime = _starttime.Add(diff2).ToString("yyyy-MM-dd HH:mm:ss.fff");
                dbcompareDT = _starttime.Add(diff2);
            }       // 드래그 안 했을 때


            draw();
           // System.Console.WriteLine(Change_Filter_Distance_MAX_input);

             text_str = textblock1 + "\n" + textblock2 + "\n" + textblock3 + "\n" + textblock4 + "\n" +textblock5;
            Data_Text.Text = text_str;
        }


        #region setting
        private void db_connect(MySqlConnection connection, string first, string second)
        {
            //first = firsttime;
            //second = secondtime;

            try
            {
                 connection.Open();
                string query = "SELECT * FROM obj_info where time BETWEEN" + "'" + first + "'" + "AND" + "'" + second + "'"+";";
                
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                         //List<MyDataModel> dataList = new List<MyDataModel>();

                        while (reader.Read())
                        {
                            DateTime date = (DateTime)reader["time"];

                            MyDataModel data = new MyDataModel();

                            string formattedDate = date.ToString("yyyy-MM-dd HH:mm:ss.fff");

                            data.time = date;
                            data.id = reader.GetInt32(1);
                            data.DistLat = reader.GetDouble(2);
                            data.DistLong = reader.GetDouble(3);
                            data.VrelLat = reader.GetDouble(4);
                            data.VrelLong = reader.GetDouble(5);
                            data.Velocity = reader.GetDouble(6);
                            data.RCS = reader.GetDouble(7);
                            data.ProbOfExist = reader.GetInt32(8);
                            data.Class = reader.GetInt32(11);
                            data.Length = reader.GetDouble(12);
                            data.Width = reader.GetDouble(13);
                            data.Zone = reader.GetInt32(14);
                            data.Lane = reader.GetInt32(15);

                            data.distance = Math.Sqrt(Math.Pow(data.DistLat,2)+ Math.Pow(data.DistLong,2));
                            data.size = data.Length * data.Width;
                         
                            dataList.Add(data);
                           
                            //if (dataList.Count > 1000)
                            //{
                            //    break;
                            //}
                        }
                    }
                  
                }
            }
            catch (Exception)
            {
                MessageBox.Show("접속 실패, 다시 시도");
            }
        }

        private void CheckBox_setting()
        {
            checkBoxes = new CheckBox[] { text_time, text_id, text_distlat, text_distlong, text_vrellat, text_vrellong, text_velocity, text_rsc, text_probofexist, text_class, text_zone, text_lane };
            checkbox_name = new String[] { "Time", "ID", "DistLat", "DistLong", "VrelLat", "VrelLong", "Velocity", "RCS", "ProbOfExist", "Class", "Zone", "Lane" };
            //checkBoxes[0] = text_time;
            //checkBoxes[1] = text_id;
            //checkBoxes[2] = text_distlat;
            //checkBoxes[3] = text_distlong;
            //checkBoxes[4] = text_vrellat;
            //checkBoxes[5] = text_vrellong;
            //checkBoxes[6] = text_velocity;
            //checkBoxes[7] = text_rsc;
            //checkBoxes[8] = text_probofexist;
            //checkBoxes[9] = text_class;
            //checkBoxes[10] = text_zone;
            //checkBoxes[11] = text_lane;
        }

        #region Set Lane Info
        #region Set Lane Width
        private void LaneWidth1Up_Click(object sender, RoutedEventArgs e)
        {
            Lane_width[0] += 0.1f;
            Update_LaneWidthText();
        }
        private void LaneWidth1Down_Click(object sender, RoutedEventArgs e)
        {
            Lane_width[0] -= 0.1f;
            Update_LaneWidthText();
        }
        private void LaneWidth2Up_Click(object sender, RoutedEventArgs e)
        {
            Lane_width[1] += 0.1f;
            Update_LaneWidthText();
        }
        private void LaneWidth2Down_Click(object sender, RoutedEventArgs e)
        {
            Lane_width[1] -= 0.1f;
            Update_LaneWidthText();
        }
        private void LaneWidth3Up_Click(object sender, RoutedEventArgs e)
        {
            Lane_width[2] += 0.1f;
            Update_LaneWidthText();
        }
        private void LaneWidth3Down_Click(object sender, RoutedEventArgs e)
        {
            Lane_width[2] -= 0.1f;
            Update_LaneWidthText();
        }
        private void LaneWidth4Up_Click(object sender, RoutedEventArgs e)
        {
            Lane_width[3] += 0.1f;
            Update_LaneWidthText();
        }
        private void LaneWidth4Down_Click(object sender, RoutedEventArgs e)
        {
            Lane_width[3] -= 0.1f;
            Update_LaneWidthText();
        }
        private void LaneWidth5Up_Click(object sender, RoutedEventArgs e)
        {
            Lane_width[4] += 0.1f;
            Update_LaneWidthText();
        }
        private void LaneWidth5Down_Click(object sender, RoutedEventArgs e)
        {
            Lane_width[4] -= 0.1f;
            Update_LaneWidthText();
        }
        private void LaneWidth6Up_Click(object sender, RoutedEventArgs e)
        {
            Data_Draw.Children.Clear();
            Lane_width[5] += 0.1f;
            Update_LaneWidthText();
            draw_map();
        }
        private void LaneWidth6Down_Click(object sender, RoutedEventArgs e)
        {
            Lane_width[5] -= 0.1f;
            Update_LaneWidthText();
        }
        private void Update_LaneWidthText()
        {
            LaneWidth1.Text = Lane_width[0].ToString();
            LaneWidth2.Text = Lane_width[1].ToString();
            LaneWidth3.Text = Lane_width[2].ToString();
            LaneWidth4.Text = Lane_width[3].ToString();
            LaneWidth5.Text = Lane_width[4].ToString();
            LaneWidth6.Text = Lane_width[5].ToString();
        }

        #endregion

        #region Set Lane Point
        private void LanePoint0Up_Click(object sender, RoutedEventArgs e)
        {
            Lane_shift[0] += 0.1f;
        }
        private void LanePoint0Down_Click(object sender, RoutedEventArgs e)
        {
            Lane_shift[0] -= 0.1f;
        }
        private void LanePoint25Up_Click(object sender, RoutedEventArgs e)
        {
            Lane_shift[1] += 0.1f;
        }
        private void LanePoint25Down_Click(object sender, RoutedEventArgs e)
        {
            Lane_shift[1] -= 0.1f;
        }
        private void LanePoint50Up_Click(object sender, RoutedEventArgs e)
        {
            Lane_shift[2] += 0.1f;
        }
        private void LanePoint50Down_Click(object sender, RoutedEventArgs e)
        {
            Lane_shift[2] -= 0.1f;
        }
        private void LanePoint75Up_Click(object sender, RoutedEventArgs e)
        {
            Lane_shift[3] += 0.1f;
        }
        private void LanePoint75Down_Click(object sender, RoutedEventArgs e)
        {
            Lane_shift[3] -= 0.1f;
        }
        private void LanePoint100Up_Click(object sender, RoutedEventArgs e)
        {
            Lane_shift[4] += 0.1f;
        }
        private void LanePoint100Down_Click(object sender, RoutedEventArgs e)
        {
            Lane_shift[4] -= 0.1f;
        }
        private void LanePoint125Up_Click(object sender, RoutedEventArgs e)
        {
            Lane_shift[5] += 0.1f;
        }
        private void LanePoint125Down_Click(object sender, RoutedEventArgs e)
        {
            Lane_shift[5] -= 0.1f;
        }
        private void LanePoint150Up_Click(object sender, RoutedEventArgs e)
        {
            Lane_shift[6] += 0.1f;
        }
        private void LanePoint150Down_Click(object sender, RoutedEventArgs e)
        {
            Lane_shift[6] -= 0.1f;
        }
        private void LanePoint175Up_Click(object sender, RoutedEventArgs e)
        {
            Lane_shift[7] += 0.1f;
        }
        private void LanePoint175Down_Click(object sender, RoutedEventArgs e)
        {
            Lane_shift[7] -= 0.1f;
        }
        private void LanePoint200Up_Click(object sender, RoutedEventArgs e)
        {
            Lane_shift[8] += 0.1f;
        }
        private void LanePoint200Down_Click(object sender, RoutedEventArgs e)
        {
            Lane_shift[8] -= 0.1f;
        }
        #endregion

        #endregion

        #endregion

        #region form_Click
        private void Form_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Form_Resize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == System.Windows.WindowState.Maximized)
            {
                this.WindowState = System.Windows.WindowState.Normal;
            }
            else if (this.WindowState == System.Windows.WindowState.Normal)
            {
                this.WindowState = System.Windows.WindowState.Maximized;
            }
        }
        private void Form_Hide_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }
        private void Setting_Click(object sender, RoutedEventArgs e)
        {
            fSetting testWindow2 = new fSetting();
            testWindow2.Show();
        }
        #endregion

        #region checkBox
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 41; i++)
            {
                if (textBoxes[i] == null) { }
                else textBoxes[i].Visibility = Visibility.Visible;
            }
        }
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 41; i++)
            {
                if (textBoxes[i] == null) { }
                else textBoxes[i].Visibility = Visibility.Collapsed;
            }
        }
        private string CheckBox_print()
        {
            string pprint = "";
            string db_data = "";
            for (int i = 0; i < 12; i++)
            {
                if (i == 0) db_data = dataList[number].time.ToString();
                else if (i == 1) db_data = dataList[number].id.ToString();
                else if (i == 2) db_data = dataList[number].DistLat.ToString("0.0");
                else if (i == 3) db_data = dataList[number].DistLong.ToString("0.0");
                else if (i == 4) db_data = dataList[number].VrelLat.ToString("0.0");
                else if (i == 5) db_data = dataList[number].VrelLong.ToString("0.0");
                else if (i == 6) db_data = dataList[number].Velocity.ToString("0.0");
                else if (i == 7) db_data = dataList[number].RCS.ToString();
                else if (i == 8) db_data = dataList[number].ProbOfExist.ToString();
                else if (i == 9) db_data = dataList[number].Class.ToString();
                else if (i == 10) db_data = dataList[number].Zone.ToString();
                else if (i == 11) db_data = dataList[number].Lane.ToString();

                if (checkBoxes[i].IsChecked == true)
                {
                    pprint += checkbox_name[i] + " = " + db_data + "\n";
                }
            }
            return pprint;
        }
        #endregion

        #region mediaElement
        private void mediaElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            //총 재생시간 가져옴  
            mediaElement.LoadedBehavior = MediaState.Pause;
            double durationMs = mediaElement.NaturalDuration.TimeSpan.TotalMilliseconds;  // 영상 길이를 tick으로 가져옴. 

            duration = durationMs;
            total_time = _starttime.AddMilliseconds(duration);
            secondtime = total_time.ToString("yyyy-MM-dd HH:mm:ss.fff");
            //
            //System.Console.WriteLine(secondtime);

            db_connect(conn, firsttime, secondtime);

            slider.Maximum = slider.Minimum + durationMs;
        }
        private void mediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            mediaElement.Stop();
            timer.Stop();
        }
        private void mediaElement_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            // 미디어 파일 실행 오류시
            MessageBox.Show("동영상 재생 실패 : " + e.ErrorException.Message.ToString());
        }
        #endregion

        #region btn
        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            draw_map();
            if (speed_check == 0)
            {
                timer.Interval = TimeSpan.FromMilliseconds(0.1);
                timer.Tick += TimerTickHandler;
                speed_check++;
            }

            mediaElement.LoadedBehavior = MediaState.Manual;
            mediaElement.Play();
            timer.Start();
        }
        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Pause();
            timer.Stop();
        }
        private void BtnSelectFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog()
            {
                DefaultExt = ".mkv",
                //Filter = "All files (*.*)|*.*",
                Filter = "Video Files (*.mp4, *.avi, *.wmv, *.mkv)|*.mp4;*.avi;*.wmv;*.mkv|All Files (*.*)|*.*",
                Multiselect = false
            };

            if (dlg.ShowDialog() == true)
            {
                // 선택한 파일 경로 가져오기
                string filePath = dlg.FileName;
                // MediaElement에 선택한 파일 설정
                mediaElement.Source = new Uri(filePath);
            }
            //double totalDuration;
            mediaElement.Pause();
            //totalDuration = mediaElement.NaturalDuration.TimeSpan.TotalMilliseconds;

            string filepp = dlg.FileName;

            string _start = System.IO.Path.GetFileNameWithoutExtension(filepp);



            if (DateTime.TryParseExact(_start, "yyyy-MM-dd HH-mm-ss", null, System.Globalization.DateTimeStyles.None, out _starttime))
            {
                // System.Console.WriteLine("time: {0}", _starttime.ToString());
            }
            else
            {
                // System.Console.WriteLine("tttttttttime: {0}", _starttime.ToString());
            }

            slider.Minimum = _starttime.Ticks;

            //total_time = _starttime.AddMilliseconds(totalDuration);

            firsttime = _starttime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            secondtime = total_time.ToString("yyyy-MM-dd HH:mm:ss.fff");
           // System.Console.WriteLine(firsttime);
           // System.Console.WriteLine(secondtime);
            // System.Console.WriteLine(total_time);

            

        }
        #endregion 

        #region slider
        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //Slider의 값이 변경될 때 MediaElement의 재생 위치를 변경
            //마우스로 움직였을 때가 아니라 그냥 움직여도 변하는 ...
            double currentValue = e.NewValue;
            double difference = currentValue - _previousValue_check;

            if (difference > 0)
            {
                drag_move_check = 2;
            }
            else if (difference < 0)
            {
                drag_move_check = 1;
            }
            else if (difference == 0)
            {
                drag_move_check = 0;
            }

            _previousValue_check = currentValue;

            mediaElement.Position = TimeSpan.FromMilliseconds(slider.Value - slider.Minimum);
            //  textblock.Text = TimeSpan.FromMilliseconds(slider.Value - slider.Minimum).ToString();
        }
        private void slider_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            mediaElement.Pause();
            timer.Stop();
            drag_check = 1;
            Data_Draw.Children.Clear();
        }
        private void slider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            draw_map();
            mediaElement.Play();
            timer.Start();

            mediaElement.Position = TimeSpan.FromMilliseconds(slider.Value - slider.Minimum);  //value 이동시 position 변경

        }




        #endregion

        #region Filter
        void Filter()
        {
            Filter_Distance();
            Filter_RCS();
            Filter_Size();
            Filter_ProbExists();

            //Filter_Nofobj();
            //Filter_Azimuth();
            //Filter_VrelOncome();
            //Filter_VrelDepart();  
            //Filter_Lifetime();       
            //Filter_Y();
            //Filter_X();
            //Filter_VYRightLeft();
            //Filter_VXOncome();
            //Filter_VYLeftRight();
            //Filter_VXDepart();
        }
        void Filter_Nofobj()
        {

        }
        void Filter_Distance()
        {
            if (Double.Parse(Change_Filter_Distance_MIN_input) < dataList[number].distance && dataList[number].distance < Double.Parse(Change_Filter_Distance_MAX_input))
            {
                // System.Console.WriteLine("aa");
            }
            else
            {
                //  System.Console.WriteLine("bb");
                Data_Draw.Children.Remove(rectangles[dataList[number].id]);
                Data_Draw.Children.Remove(textBoxes[dataList[number].id]);
            }
        }
        void Filter_Azimuth()
        {

        }
        void Filter_VrelOncome()
        {

        }
        void Filter_VrelDepart()
        {

        }
        void Filter_RCS()
        {
            if (Double.Parse(Change_Filter_RCS_MIN_input) < dataList[number].RCS && dataList[number].RCS < Double.Parse(Change_Filter_RCS_MAX_input))
            {
                // System.Console.WriteLine("aa");
            }
            else
            {
                //  System.Console.WriteLine("bb");
                Data_Draw.Children.Remove(rectangles[dataList[number].id]);
                Data_Draw.Children.Remove(textBoxes[dataList[number].id]);
            }
        }
        void Filter_Lifetime()
        {

        }
        void Filter_Size()
        {
            if (Double.Parse(Change_Filter_Size_MIN_input) <= dataList[number].size && dataList[number].size < Double.Parse(Change_Filter_Size_MAX_input))
            {
                // System.Console.WriteLine("aa");
            }
            else
            {
                //  System.Console.WriteLine("bb");
                Data_Draw.Children.Remove(rectangles[dataList[number].id]);
                Data_Draw.Children.Remove(textBoxes[dataList[number].id]);
            }
        }
        void Filter_ProbExists()
        {
            if (Double.Parse(Change_Filter_ProbExists_MIN_input) < dataList[number].ProbOfExist && dataList[number].ProbOfExist < Double.Parse(Change_Filter_ProbExists_MAX_input))
            {
                // System.Console.WriteLine("aa");
            }
            else
            {
                //  System.Console.WriteLine("bb");
                Data_Draw.Children.Remove(rectangles[dataList[number].id]);
                Data_Draw.Children.Remove(textBoxes[dataList[number].id]);
            }
        }
        void Filter_Y()
        {

        }
        void Filter_X()
        {

        }
        void Filter_VYRightLeft()
        {

        }
        void Filter_VXOncome()
        {

        }
        void Filter_VYLeftRight()
        {

        }
        void Filter_VXDepart()
        {

        }
        #endregion
    }

}
