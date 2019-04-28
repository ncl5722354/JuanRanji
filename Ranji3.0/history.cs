using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Ranji3._0
{
    public partial class history : Form
    {
        MainView mv;
        
        int machine_num = 1;                      //指定的站号
        DateTime nowtime = DateTime.Now;          //现在的时间
        double timespan = 5;                      //

        public history(MainView mymv)
        {
            mv = mymv;
            InitializeComponent();
        }


        private void history_Load(object sender, EventArgs e)
        {
            setchart(chart1,machine_num,nowtime,timespan);
            draw_chart(chart1,machine_num,nowtime,timespan);
        }

        private void draw_chart(Chart mychart, int machine_num,DateTime nowtime,double timespan)
        {
            //将对应的开始时间读出来
            try
            {
                DataSet ds = mv.mysqlconnection.sql_search_database("Select * from start_time where machine_num=" + machine_num.ToString());
                DataRow dr = ds.Tables[0].Rows[0];
                DateTime starttime = DateTime.Parse(dr[1].ToString());
                // 将历史记录中相相应的都读出来
                ds = mv.mysqlconnection.sql_search_database("Select * from history where machine_num=" + machine_num.ToString() + " and  save_time>" + "'" + nowtime.AddHours(-timespan / 2).ToString() + "' and save_time<'" + nowtime.AddHours(timespan / 2).ToString() + "' order by save_time");
                int count = ds.Tables[0].Rows.Count;
                mychart.Series[0].Points.Clear();
                for (int i = 0; i < count; i++)
                {
                    DataRow mydr = ds.Tables[0].Rows[i];
                    int myvalue = int.Parse(mydr[1].ToString());
                    DateTime mytime = DateTime.Parse(mydr[2].ToString());
                    mychart.Series[0].Points.AddXY(mytime, myvalue);

                }
            }
            catch
            {
                mychart.Series[0].Points.Clear();
            }

            try
            {
                DataSet ds = mv.mysqlconnection.sql_search_database("Select * from start_time where machine_num=" + machine_num.ToString());
                DataRow dr = ds.Tables[0].Rows[0];
                DateTime starttime = DateTime.Parse(dr[1].ToString());

                ds = mv.mysqlconnection.sql_search_database("Select machine_num,value,save_time from liaogang where machine_num=" + machine_num.ToString() + " and save_time>" + "'" + nowtime.AddHours(-timespan / 2).ToString() + "' and save_time<'" + nowtime.AddHours(timespan / 2).ToString() + "' order by save_time");
                int count = ds.Tables[0].Rows.Count;
                mychart.Series[1].Points.Clear();
                for (int i = 0; i < count; i++)
                {
                    DataRow mydr = ds.Tables[0].Rows[i];
                    float myvalue = float.Parse(mydr[1].ToString())/10;
                    DateTime mytime = DateTime.Parse(mydr[2].ToString());
                    mychart.Series[1].Points.AddXY(mytime, myvalue);
                }
            }
            catch
            {
            }

            try
            {
                DataSet ds = mv.mysqlconnection.sql_search_database("Select * from start_time where machine_num=" + machine_num.ToString());
                DataRow dr = ds.Tables[0].Rows[0];
                DateTime starttime = DateTime.Parse(dr[1].ToString());
                // 将历史记录中相相应的都读出来
                ds = mv.mysqlconnection.sql_search_database("Select * from yewei_history where machine_num=" + machine_num.ToString() + " and  time>" + "'" + nowtime.AddHours(-timespan / 2).ToString() + "' and time<'" + nowtime.AddHours(timespan / 2).ToString() + "' order by time");
                int count = ds.Tables[0].Rows.Count;
                mychart.Series[2].Points.Clear();
                for (int i = 0; i < count; i++)
                {
                    DataRow mydr = ds.Tables[0].Rows[i];
                    float myvalue = float.Parse(mydr[3].ToString());
                    DateTime mytime = DateTime.Parse(mydr[2].ToString());
                    mychart.Series[2].Points.AddXY(mytime, myvalue);

                }
            }
            catch
            {
                mychart.Series[2].Points.Clear();
            }

        }
        private void setchart(Chart mychart, int machine_num,DateTime nowtime,double timespan)
        {
            // 针对一个图表和对应的设定
            // 对于参数nowtime 历史曲线中间参数，前两个半小时，后两个半小时
            // 
            try
            {
                mychart.ChartAreas[0].AxisX.LabelStyle.Format = "yyyy-MM-dd HH:mm:ss";
                DataSet ds = mv.mysqlconnection.sql_search_database("select * from start_time where machine_num=" + machine_num.ToString());
                DataRow dr = ds.Tables[0].Rows[0];
                mychart.ChartAreas[0].AxisX.Minimum = nowtime.AddHours(-timespan/2).ToOADate();
                mychart.ChartAreas[0].AxisX.Maximum = nowtime.AddHours(timespan/2).ToOADate();
                mychart.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Hours;
                mychart.ChartAreas[0].AxisX.Interval = timespan/5;
            }
            catch
            {
            }
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripContainer1_TopToolStripPanel_Click(object sender, EventArgs e)
        {

        }

        private void toolStripContainer1_ContentPanel_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label_starttime_Click(object sender, EventArgs e)
        {

        }

        private void button_starttime_left_Click(object sender, EventArgs e)
        {
            nowtime = nowtime.AddMinutes(-10);
            setchart(chart1, machine_num, nowtime, timespan);
            draw_chart(chart1, machine_num, nowtime, timespan);
        }

        private void button_starttime_right_Click(object sender, EventArgs e)
        {
            nowtime = nowtime.AddMinutes(10);
            setchart(chart1, machine_num, nowtime, timespan);
            draw_chart(chart1, machine_num, nowtime, timespan);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            nowtime = nowtime.AddHours(-1);
            setchart(chart1, machine_num, nowtime, timespan);
            draw_chart(chart1, machine_num, nowtime, timespan);
        }

        private void button__Click(object sender, EventArgs e)
        {
            nowtime = nowtime.AddHours(1);
            setchart(chart1, machine_num, nowtime, timespan);
            draw_chart(chart1, machine_num, nowtime, timespan);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            nowtime = DateTime.Now;
            timespan = 5;
            setchart(chart1, machine_num, nowtime, timespan);
            draw_chart(chart1, machine_num, nowtime, timespan);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (timespan <= 64)
            {
                timespan = timespan * 2;
                setchart(chart1, machine_num, nowtime, timespan);
                draw_chart(chart1, machine_num, nowtime, timespan);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timespan = timespan / 2;
            setchart(chart1, machine_num, nowtime, timespan);
            draw_chart(chart1, machine_num, nowtime, timespan);
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Press_Button(object sender, EventArgs e)
        {
            try
            {
                Button mybutton = (Button)sender;
                int press_machine_num = int.Parse(mybutton.Text);
                machine_num = press_machine_num;
                setchart(chart1, machine_num, nowtime, timespan);
                draw_chart(chart1, machine_num, nowtime, timespan);
            }
            catch
            {

            }
        }

        private void flowLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void chart1_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                double x_pos = e.X;
                double y_pos = e.Y;
                x_pos = chart1.ChartAreas[0].AxisX.PixelPositionToValue(x_pos);
                DateTime mytime = DateTime.FromOADate(x_pos);
                y_pos = chart1.ChartAreas[0].AxisY.PixelPositionToValue(y_pos);
                int y_int = (int)y_pos;
                label_show.Text = mytime.ToString() + "     " + y_int.ToString() + "℃";
                label_show.Left = e.X+50;
                label_show.Top = e.Y;
                chart1.ChartAreas[0].CursorX.Position = x_pos;
                chart1.ChartAreas[0].CursorY.Position = y_pos;
            }
            catch
            {
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            nowtime = dateTimePicker1.Value;
            setchart(chart1, machine_num, nowtime, timespan);
            draw_chart(chart1, machine_num, nowtime, timespan);
        }
    }
}
