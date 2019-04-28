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
    public partial class Info : Form
    {
        public MainView mv;
        public Info(MainView mymv)
        {
            mv = mymv;
            InitializeComponent();
        }

        private void Info_Load(object sender, EventArgs e)
        {
            for (int i = 1; i <= 60; i++)
            {
                comboBox1.Items.Add(i.ToString());
            }
        }

        private void setchart(Chart mychart, int machine_num)
        {
            // 针对一个图表和对应的机号的处理
            try
            {
                mychart.ChartAreas[0].AxisX.LabelStyle.Format = "HH:mm:ss";
                DataSet ds = mv.mysqlconnection.sql_search_database("select * from start_time where machine_num=" + machine_num.ToString());
                DataRow dr = ds.Tables[0].Rows[0];
                DateTime starttime = DateTime.Parse(listBox1.Items[listBox1.SelectedIndex].ToString());
                mychart.ChartAreas[0].AxisX.Minimum = starttime.ToOADate();
                mychart.ChartAreas[0].AxisX.Maximum = starttime.AddHours(5).ToOADate();
                mychart.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Hours;
                mychart.ChartAreas[0].AxisX.Interval = 1;
            }
            catch
            {
            }
        }

        private void draw_chart(Chart mychart, int machine_num)
        {
            //将对应的开始时间读出来
            try
            {

                DataSet ds = mv.mysqlconnection.sql_search_database("Select * from start_time where machine_num=" + machine_num.ToString());
                DataRow dr = ds.Tables[0].Rows[0];
                DateTime starttime = DateTime.Parse(listBox1.Items[listBox1.SelectedIndex].ToString());
                // 将历史记录中相相应的都读出来
                ds = mv.mysqlconnection.sql_search_database("Select machine_num,value,save_time from history where machine_num=" + machine_num.ToString() + " and  save_time>" + "'" + starttime.ToString() + "' and save_time<'" + starttime.AddHours(4).ToString() + "'");
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

            // 画料缸液位曲线

            try
            {
                DataSet ds = mv.mysqlconnection.sql_search_database("Select * from start_time where machine_num=" + machine_num.ToString());
                DataRow dr = ds.Tables[0].Rows[0];
                DateTime starttime = DateTime.Parse(listBox1.Items[listBox1.SelectedIndex].ToString());

                ds = mv.mysqlconnection.sql_search_database("Select machine_num,value,save_time from liaogang where machine_num=" + machine_num.ToString() + " and save_time>" + "'" + starttime.ToString() + "' and save_time<'" + starttime.AddHours(4).ToString() + "'");
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
            //// 画设定曲线
            //try
            //{
            //    int datagridview_row_count = dataGridView1.Rows.Count;
            //    DataSet ds = mv.mysqlconnection.sql_search_database("Select * from start_time where machine_num=" + machine_num.ToString());
            //    DataRow dr = ds.Tables[0].Rows[0];
            //    DateTime starttime = DateTime.Parse(dr[1].ToString()); //开始时间
            //    mychart.Series[1].Points.Clear();
            //    for (int i = 0; i < datagridview_row_count; i++)
            //    {
            //        if (dataGridView1[4, i].Value.ToString() == "温控")
            //        {
            //            int wendu = int.Parse(dataGridView1[1, i].Value.ToString());
            //            mychart.Series[1].Points.AddXY(starttime, wendu);
            //            int timespan = int.Parse(dataGridView1[3, i].Value.ToString());
            //            starttime = starttime.AddMinutes(timespan);
            //        }
            //    }
            //}
            //catch
            //{

            //}

        }

      


        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                setchart(chart1,int.Parse(comboBox1.Text));
                draw_chart(chart1, int.Parse(comboBox1.Text));
                DataSet ds = mv.mysqlconnection.sql_search_database("select * from info_save where machine_num='" + comboBox1.Text + "' and start_time='"+listBox1.Items[listBox1.SelectedIndex].ToString()+"'");
                DataRow dr = ds.Tables[0].Rows[0];
                label3.Text = dr[3].ToString();
                DateTime selecttime = DateTime.Parse(listBox1.Items[listBox1.SelectedIndex].ToString());
                string selecttime_string = selecttime.ToString("yyyyMMddHHmmss");
                DataSet myds = mv.mysqlconnection.sql_search_database("select * from info"+comboBox1.Text+selecttime_string);
                dataGridView1.RowCount = 1;
                try
                {
                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {
                        for (int j = 0; j < dataGridView1.ColumnCount; j++)
                        {
                            dataGridView1[j, i].Value = "";
                        }
                    }
                    
                }
                catch { }
                dataGridView1.RowCount=myds.Tables[0].Rows.Count;
                dataGridView1.ColumnCount = myds.Tables[0].Columns.Count;
                for (int i = 0; i < myds.Tables[0].Rows.Count; i++)
                {
                    for (int j = 0; j < myds.Tables[0].Columns.Count; j++)
                    {
                        dataGridView1[j, i].Value = myds.Tables[0].Rows[i][j].ToString();
                    }
                }
                
            }
            catch
            {
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                listBox1.Items.Clear();
                DataSet ds = mv.mysqlconnection.sql_search_database("select * from info_save where machine_num='" + comboBox1.Text + "'");
                int count = ds.Tables[0].Rows.Count;
                for (int i = count-1; i >= 0; i--)
                {
                    listBox1.Items.Add(ds.Tables[0].Rows[i][2].ToString());
                }
            }
            catch
            {
            }
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
                label_show.Left = e.X + 50;
                label_show.Top = e.Y;
                chart1.ChartAreas[0].CursorX.Position = x_pos;
                chart1.ChartAreas[0].CursorY.Position = y_pos;
            }
            catch
            {
            }
        }
    }
}
