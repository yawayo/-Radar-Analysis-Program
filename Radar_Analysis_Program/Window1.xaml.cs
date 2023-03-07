using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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
using System.Windows.Threading;

namespace Radar_Analysis_Program
{
    /// <summary>
    /// Window1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Window1 : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        List<MyDataModel> dataList = new List<MyDataModel>();

        TextBox[] textBoxes = new TextBox[41];
        Rectangle[] rectangles = new Rectangle[41];
       
        DateTime[] dates = new DateTime[41];

        DateTime _starttime;
        DateTime _checktime;

        DateTime total_time;
        TimeSpan diff;
        TimeSpan diff2;

        private double max_lat = 10;
        private double max_long = 100;
        private Point shift_pos;

        int number = 0; // DB  n 번째 
        string dbcomparetime;
        DateTime dbcompareDT;

        double _previousValue_check = 0; //슬라이더 값 +인지 - 인지 체크
        int drag_move_check = 0;
        int speed_check = 0;
        int drag_check = 0;

        string text_str = "";
        string textblock1;
        string textblock2;
        string textblock3;
        string textblock4;

        string firsttime;
        string secondtime;

        double duration= 0;
        private MySqlConnection conn;


        public CheckBox[] checkBoxes;
        public String[] checkbox_name;
        

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
            public int Zone { get; set; }
            public int Lane { get; set; }
            public DateTime Timestamp;
        }
        //dataList[number].id.ToString()

        public Window1(MySqlConnection connection)
        {
            conn = connection;
            InitializeComponent();
            CheckBox_setting();
            map_setting();

        }

        private void draw()
        {
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

                        if (dataList[number].DistLong < 10)
                        {
                            Data_Draw.Children.Remove(rectangles[dataList[number].id]);
                            Data_Draw.Children.Remove(textBoxes[dataList[number].id]);
                        }
                        //textblock4.Text = number.ToString();
                        number++;
                    }
                }
                else if (number == 0 && rectangles[dataList[number].id] != null )   //드래그 시 number가 0일 때 ,   
                {
                    for (int i = 0; i < 41; i++)
                    {
                        if (rectangles[dataList[number].id] != null &&  dataList[number].time <= dbcompareDT )  // 이미 존재한
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
                            number++;
                        }
                    }
                    textblock4 = number.ToString();

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
                            number++;
                        }
                    }
                    textblock4 = number.ToString();

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

            text_str = textblock1 + "\n" + textblock2 + "\n" + textblock3 + "\n" + textblock4;
            Data_Text.Text = text_str;
        }
       
        #region setting
        private void db_connect(MySqlConnection connection ,string first, string second)
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
                            data.Zone = reader.GetInt32(14);
                            data.Lane = reader.GetInt32(15);
                            System.Console.WriteLine("{0}", data.Zone);
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
        private void map_setting()
        {
            //중앙선   
            Polyline polyline = new Polyline();
            polyline.Points = new PointCollection()  { new Point((Data_Draw.Width/2), (Data_Draw.Height/max_long)),
                new Point( (Data_Draw.Width / 2),  10*(Data_Draw.Height/max_long)),    // 600/100 6 
                new Point(  (Data_Draw.Width / 2),  50*(Data_Draw.Height/max_long)),
                new Point(  (Data_Draw.Width / 2),  100*(Data_Draw.Height/max_long)) };
            polyline.Stroke = Brushes.Yellow;
            polyline.StrokeThickness = 3;
            Data_Draw.Children.Add(polyline);

            //왼쪽선
            Polyline leftline = new Polyline();
            leftline.Points = new PointCollection()  { new Point(0, (Data_Draw.Height/max_long)),
                new Point( 0,  10*(Data_Draw.Height/max_long)),    // 600/100 6 
                new Point(  0,  50*(Data_Draw.Height/max_long)),
                new Point(  0,  100*(Data_Draw.Height/max_long)) };
            leftline.Stroke = Brushes.White;
            leftline.StrokeThickness = 3;
            Data_Draw.Children.Add(leftline);


            //왼쪽 1선
            Polyline leftline1 = new Polyline();
            leftline1.Points = new PointCollection()  { new Point(Data_Draw.Width/6, (Data_Draw.Height/max_long)),
                new Point(Data_Draw.Width/6,  10*(Data_Draw.Height/max_long)),    // 600/100 6 
                new Point(  Data_Draw.Width/6,  50*(Data_Draw.Height/max_long)),
                new Point( Data_Draw.Width/6,  100*(Data_Draw.Height/max_long)) };
            leftline1.Stroke = Brushes.White;
            leftline1.StrokeThickness = 2;
            leftline1.StrokeDashArray = new DoubleCollection() { 15, 10 };
            Data_Draw.Children.Add(leftline1);


            //왼쪽 2선
            Polyline leftline2 = new Polyline();
            leftline2.Points = new PointCollection()  { new Point(2*(Data_Draw.Width/6), (Data_Draw.Height/max_long)),
                new Point( 2*(Data_Draw.Width/6),  10*(Data_Draw.Height/max_long)),    // 600/100 6 
                new Point(  2*(Data_Draw.Width/6),  50*(Data_Draw.Height/max_long)),
                new Point(  2*(Data_Draw.Width/6),  100*(Data_Draw.Height/max_long)) };
            leftline2.Stroke = Brushes.White;
            leftline2.StrokeThickness = 2;
            leftline2.StrokeDashArray = new DoubleCollection() { 15, 10 };
            Data_Draw.Children.Add(leftline2);

            //오른선
            Polyline rightline = new Polyline();
            rightline.Points = new PointCollection()  { new Point(Data_Draw.Width , (Data_Draw.Height/max_long)),
                new Point(Data_Draw.Width ,  10*(Data_Draw.Height/max_long)),    // 600/100 6 
                new Point(  Data_Draw.Width ,  50*(Data_Draw.Height/max_long)),
                new Point(  Data_Draw.Width ,  100*(Data_Draw.Height/max_long)) };
            rightline.Stroke = Brushes.White;
            rightline.StrokeThickness = 3;
            Data_Draw.Children.Add(rightline);

            //오른 1선
            Polyline rightline1 = new Polyline();
            rightline1.Points = new PointCollection()  { new Point(4*(Data_Draw.Width/6) , (Data_Draw.Height/max_long)),
                new Point(4*(Data_Draw.Width/6),  10*(Data_Draw.Height/max_long)),    // 600/100 6 
                new Point(  4*(Data_Draw.Width/6) ,  50*(Data_Draw.Height/max_long)),
                new Point( 4*(Data_Draw.Width/6),  100*(Data_Draw.Height/max_long)) };
            rightline1.Stroke = Brushes.White;
            rightline1.StrokeThickness = 2;
            rightline1.StrokeDashArray = new DoubleCollection() { 15, 10 };
            Data_Draw.Children.Add(rightline1);


            //오른 2선
            Polyline rightline2 = new Polyline();
            rightline2.Points = new PointCollection()  { new Point(5*(Data_Draw.Width/6) , (Data_Draw.Height/max_long)),
                new Point(5*(Data_Draw.Width/6),  10*(Data_Draw.Height/max_long)),    // 600/100 6 
                new Point(  5*(Data_Draw.Width/6) ,  50*(Data_Draw.Height/max_long)),
                new Point( 5*(Data_Draw.Width/6),  100*(Data_Draw.Height/max_long)) };
            rightline2.Stroke = Brushes.White;
            rightline2.StrokeThickness = 2;
            rightline2.StrokeDashArray = new DoubleCollection() { 15, 10 };
            Data_Draw.Children.Add(rightline2);




            ////
            //Polyline rightline2 = new Polyline();
            //rightline2.Points = new PointCollection()  { new Point(5*(Data_Draw.Width/6) , (Data_Draw.Height/max_long)),
            //    new Point(5*(Data_Draw.Width/6),  10*(Data_Draw.Height/max_long)),    // 600/100 6 
            //    new Point(  5*(Data_Draw.Width/6) ,  50*(Data_Draw.Height/max_long)),
            //    new Point( 5*(Data_Draw.Width/6),  100*(Data_Draw.Height/max_long)) };
            //rightline2.Stroke = Brushes.White;
            //rightline2.StrokeThickness = 3;
            //rightline2.StrokeDashArray = new DoubleCollection() { 10, 5 };
            //Data_Draw.Children.Add(rightline2);


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
            mediaElement.Play();
            timer.Start();

            mediaElement.Position = TimeSpan.FromMilliseconds(slider.Value - slider.Minimum);  //value 이동시 position 변경

        }
        #endregion


      


    }


}
