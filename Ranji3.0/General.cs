using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Threading;


namespace Ranji3._0
{
    public partial class General : Form
    {
       
        public MainView mv;
        public int page = 1;
        public General(MainView mymv)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            mv = mymv;
            
            InitializeComponent();
            chart_init();
        }

        public void setpage(int mypage)
        {
            //翻页的函数
            if (mypage <= 0) mypage = 1;
            page = mypage;
        }

        private void chart_init()
        {
             // 对16个chart图表进行初始化 假如没有开始时间，当前时间即为开始时间
             // 道先对开始时间进行判断
            for (int i = 1; i <= 60; i++)
            {
                DataSet ds = mv.mysqlconnection.sql_search_database("select * from start_time where machine_num='"+i.ToString()+"'");
                int rowcount = ds.Tables[0].Rows.Count;
                if (rowcount == 0)
                {
                    //表中没有开始时间
                    mv.mysqlconnection.excute_sql("insert into start_time values ('"+i.ToString()+"','"+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+"')");
                }
            }
            setchart(chart1, 1);
            setchart(chart2, 2);
            setchart(chart3, 3);
            setchart(chart4, 4);
            setchart(chart5, 5);
            setchart(chart6, 6);
            setchart(chart7, 7);
            setchart(chart8, 8);
            setchart(chart9, 9);
            setchart(chart10, 10);
            setchart(chart11, 11);
            setchart(chart12, 12);
            setchart(chart13, 13);
            setchart(chart14, 14);
            setchart(chart15, 15);
            setchart(chart16, 16);
            draw_line();
            //根据开始时间，设定间隔，最大值与最小值
        }

        private void setchart(Chart mychart, int machine_num)
        {
            // 针对一个图表和对应的机号的处理
            try
            {
                if (mychart.InvokeRequired)
                {
                    ThreadWork tw = new ThreadWork(setchart);
                    this.Invoke(tw, new object[2] { mychart, machine_num });
                }
                else
                {
                    mychart.ChartAreas[0].AxisX.LabelStyle.Format = "HH:mm:ss";
                    DataSet ds = mv.mysqlconnection.sql_search_database("select * from start_time where machine_num=" + machine_num.ToString());
                    DataRow dr = ds.Tables[0].Rows[0];
                    DateTime starttime = DateTime.Parse(dr[1].ToString());
                    mychart.ChartAreas[0].AxisX.Minimum = starttime.ToOADate();
                    mychart.ChartAreas[0].AxisX.Maximum = starttime.AddHours(5).ToOADate();
                    mychart.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Hours;
                    mychart.ChartAreas[0].AxisX.Interval = 2;
                }
                
            }
            catch
            {
            }
        }

        private delegate void ThreadWork(Chart mychart,int machine_num);

        private void draw_chart(Chart mychart,int machine_num)
        {
            //将对应的开始时间读出来
            if (true)
            {
                if (mychart.InvokeRequired)
                {
                    ThreadWork tw = new ThreadWork(draw_chart);
                    try
                    {
                        this.Invoke(tw, new object[2] { mychart, machine_num });
                    }
                    catch { }
                }
                else
                {
                    try
                    {

                        DataSet ds = mv.mysqlconnection.sql_search_database("Select * from start_time where machine_num=" + machine_num.ToString());
                        DataRow dr = ds.Tables[0].Rows[0];
                        DateTime starttime = DateTime.Parse(dr[1].ToString());
                        // 将历史记录中相相应的都读出来
                        ds = mv.mysqlconnection.sql_search_database("Select machine_num,value,save_time from history where machine_num=" + machine_num.ToString() + " and  save_time>" + "'" + starttime.ToString() + "' and save_time<'" + starttime.AddHours(5).ToString() + "' order by save_time");
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
                    // 画设定曲线
                    try
                    {
                        // int datagridview_row_count = dataGridView1.Rows.Count;
                        DataSet ds = mv.mysqlconnection.sql_search_database("Select * from start_time where machine_num=" + machine_num.ToString());
                        DataRow dr = ds.Tables[0].Rows[0];
                        DateTime starttime = DateTime.Parse(dr[1].ToString()); //开始时间
                        DataSet gongyi_ds = mv.mysqlconnection.sql_search_database("Select * from craft_machine" + machine_num.ToString());
                        int datagridview_row_count = gongyi_ds.Tables[0].Rows.Count;
                        mychart.Series[1].Points.Clear();
                        for (int i = 0; i < datagridview_row_count; i++)
                        {
                            if (gongyi_ds.Tables[0].Rows[i][4].ToString() == "温控")
                            {
                                int wendu = int.Parse(gongyi_ds.Tables[0].Rows[i][1].ToString());
                                mychart.Series[1].Points.AddXY(starttime, wendu);
                                int timespan = int.Parse(gongyi_ds.Tables[0].Rows[i][3].ToString());
                                starttime = starttime.AddMinutes(timespan);
                            }
                        }
                    }
                    catch
                    {

                    }
                }
                
            }

        }

        private void General_Load(object sender, EventArgs e)
        {
            // 标签值要加进来
            myLabel_wendu1.mv = this.mv;
            myLabel_wendu2.mv = this.mv;
            myLabel_wendu3.mv = this.mv;
            myLabel_wendu4.mv = this.mv;
            myLabel_wendu5.mv = this.mv;
            myLabel_wendu6.mv = this.mv;
            myLabel_wendu7.mv = this.mv;
            myLabel_wendu8.mv = this.mv;
            myLabel_wendu9.mv = this.mv;
            myLabel_wendu10.mv = this.mv;
            myLabel_wendu11.mv = this.mv;
            myLabel_wendu12.mv = this.mv;
            myLabel_wendu13.mv = this.mv;
            myLabel_wendu14.mv = this.mv;
            myLabel_wendu15.mv = this.mv;
            myLabel_wendu16.mv = this.mv;

            myLabel_yewei1.mv = this.mv;
            myLabel_yewei2.mv = this.mv;
            myLabel_yewei3.mv = this.mv;
            myLabel_yewei4.mv = this.mv;
            myLabel_yewei5.mv = this.mv;
            myLabel_yewei6.mv = this.mv;
            myLabel_yewei7.mv = this.mv;
            myLabel_yewei8.mv = this.mv;
            myLabel_yewei9.mv = this.mv;
            myLabel_yewei10.mv = this.mv;
            myLabel_yewei11.mv = this.mv;
            myLabel_yewei12.mv = this.mv;
            myLabel_yewei13.mv = this.mv;
            myLabel_yewei14.mv = this.mv;
            myLabel_yewei15.mv = this.mv;
            myLabel_yewei16.mv = this.mv;
        }

        private void chart1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            mv.detail.Set_machine_num(1 + (page - 1) * 16);
            mv.Show_Form(mv.detail);
            mv.detail.draw_line();
            mv.detail.Reset_Craft_ComboBox();
           // mv.detail.ReFlash_Craft_Chart();
        }

        private void chart2_Click(object sender, EventArgs e)
        {
            mv.detail.Set_machine_num(2 + (page - 1) * 16);
            mv.Show_Form(mv.detail);
            mv.detail.draw_line();
            mv.detail.Reset_Craft_ComboBox();
           // mv.detail.ReFlash_Craft_Chart();
        }

        private void chart5_Click(object sender, EventArgs e)
        {
            mv.detail.Set_machine_num(5 + (page - 1) * 16);
            mv.Show_Form(mv.detail);
            mv.detail.draw_line();
            mv.detail.Reset_Craft_ComboBox();
            //mv.detail.ReFlash_Craft_Chart();
        }

        private void chart3_Click(object sender, EventArgs e)
        {
            mv.detail.Set_machine_num(3 + (page - 1) * 16);
            mv.Show_Form(mv.detail);
            mv.detail.draw_line();
            mv.detail.Reset_Craft_ComboBox();
            //mv.detail.ReFlash_Craft_Chart();
        }

        private void chart6_Click(object sender, EventArgs e)
        {
            mv.detail.Set_machine_num(6 + (page - 1) * 16);
            mv.Show_Form(mv.detail);
            mv.detail.draw_line();
            mv.detail.Reset_Craft_ComboBox();
            //mv.detail.ReFlash_Craft_Chart();
        }

        private void chart7_Click(object sender, EventArgs e)
        {
            mv.detail.Set_machine_num(7 + (page - 1) * 16);
            mv.Show_Form(mv.detail);
            mv.detail.draw_line();
            mv.detail.Reset_Craft_ComboBox();
            //mv.detail.ReFlash_Craft_Chart();
        }

        private void chart8_Click(object sender, EventArgs e)
        {
            mv.detail.Set_machine_num(8 + (page - 1) * 16);
            mv.Show_Form(mv.detail);
            mv.detail.draw_line();
            mv.detail.Reset_Craft_ComboBox();
            //mv.detail.ReFlash_Craft_Chart();
        }

        private void chart4_Click(object sender, EventArgs e)
        {
            mv.detail.Set_machine_num(4 + (page - 1) * 16);
            mv.Show_Form(mv.detail);
            mv.detail.draw_line();
            mv.detail.Reset_Craft_ComboBox();
            //mv.detail.ReFlash_Craft_Chart();
        }

        private void chart16_Click(object sender, EventArgs e)
        {
            mv.detail.Set_machine_num(16 + (page - 1) * 16);
            mv.Show_Form(mv.detail);
            mv.detail.draw_line();
            mv.detail.Reset_Craft_ComboBox();
            //mv.detail.ReFlash_Craft_Chart();
        }

        private void chart15_Click(object sender, EventArgs e)
        {
            mv.detail.Set_machine_num(15 + (page - 1) * 16);
            mv.Show_Form(mv.detail);
            mv.detail.draw_line();
            mv.detail.Reset_Craft_ComboBox();
            //mv.detail.ReFlash_Craft_Chart();
        }

        private void chart10_Click(object sender, EventArgs e)
        {
            mv.detail.Set_machine_num(10 + (page - 1) * 16);
            mv.Show_Form(mv.detail);
            mv.detail.draw_line();
            mv.detail.Reset_Craft_ComboBox();
            //mv.detail.ReFlash_Craft_Chart();
        }

        private void chart9_Click(object sender, EventArgs e)
        {
            mv.detail.Set_machine_num(9 + (page - 1) * 16);
            mv.Show_Form(mv.detail);
            mv.detail.draw_line();
            mv.detail.Reset_Craft_ComboBox();
            //mv.detail.ReFlash_Craft_Chart();
        }

        private void chart13_Click(object sender, EventArgs e)
        {
            mv.detail.Set_machine_num(13 + (page - 1) * 16);
            mv.Show_Form(mv.detail);
            mv.detail.draw_line();
            mv.detail.Reset_Craft_ComboBox();
            //mv.detail.ReFlash_Craft_Chart();
        }

        private void chart14_Click(object sender, EventArgs e)
        {
            mv.detail.Set_machine_num(14 + (page - 1) * 16);
            mv.Show_Form(mv.detail);
            mv.detail.draw_line();
            mv.detail.Reset_Craft_ComboBox();
            //mv.detail.ReFlash_Craft_Chart();
        }

        public void draw_line()
        {
            //一次画线的过程
            Console.WriteLine("drawline starttime is "+DateTime.Now.ToString());
            setchart(chart1, 1 + (page - 1) * 16);
            setchart(chart2, 2 + (page - 1) * 16);
            setchart(chart3, 3 + (page - 1) * 16);
            setchart(chart4, 4 + (page - 1) * 16);
            setchart(chart5, 5 + (page - 1) * 16);
            setchart(chart6, 6 + (page - 1) * 16);
            setchart(chart7, 7 + (page - 1) * 16);
            setchart(chart8, 8 + (page - 1) * 16);
            setchart(chart9, 9 + (page - 1) * 16);
            setchart(chart10, 10 + (page - 1) * 16);
            setchart(chart11, 11 + (page - 1) * 16);
            setchart(chart12, 12 + (page - 1) * 16);
            setchart(chart13, 13 + (page - 1) * 16);
            setchart(chart14, 14 + (page - 1) * 16);
            setchart(chart15, 15 + (page - 1) * 16);
            setchart(chart16, 16 + (page - 1) * 16);

            draw_chart(chart1, 1+(page-1)*16);
            draw_chart(chart2, 2+(page-1)*16);
            draw_chart(chart3, 3+(page-1)*16);
            draw_chart(chart4, 4 + (page - 1) * 16);
            draw_chart(chart5, 5 + (page - 1) * 16);
            draw_chart(chart6, 6 + (page - 1) * 16);
            draw_chart(chart7, 7 + (page - 1) * 16);
            draw_chart(chart8, 8 + (page - 1) * 16);
            draw_chart(chart9, 9 + (page - 1) * 16);
            draw_chart(chart10, 10 + (page - 1) * 16);
            draw_chart(chart11, 11 + (page - 1) * 16);
            draw_chart(chart12, 12 + (page - 1) * 16);
            draw_chart(chart13, 13 + (page - 1) * 16);
            draw_chart(chart14, 14 + (page - 1) * 16);
            draw_chart(chart15, 15 + (page - 1) * 16);
            draw_chart(chart16, 16 + (page - 1) * 16);
            // 标签
            myLabel_wendu1.Machine_num = 1 + (page - 1) * 16;
            myLabel_wendu2.Machine_num = 2 + (page - 1) * 16;
            myLabel_wendu3.Machine_num = 3 + (page - 1) * 16;
            myLabel_wendu4.Machine_num = 4 + (page - 1) * 16;
            myLabel_wendu5.Machine_num = 5 + (page - 1) * 16;
            myLabel_wendu6.Machine_num = 6 + (page - 1) * 16;
            myLabel_wendu7.Machine_num = 7 + (page - 1) * 16;
            myLabel_wendu8.Machine_num = 8 + (page - 1) * 16;
            myLabel_wendu9.Machine_num = 9 + (page - 1) * 16;
            myLabel_wendu10.Machine_num = 10 + (page - 1) * 16;
            myLabel_wendu11.Machine_num = 11 + (page - 1) * 16;
            myLabel_wendu12.Machine_num = 12 + (page - 1) * 16;
            myLabel_wendu13.Machine_num = 13 + (page - 1) * 16;
            myLabel_wendu14.Machine_num = 14 + (page - 1) * 16;
            myLabel_wendu15.Machine_num = 15 + (page - 1) * 16;
            myLabel_wendu16.Machine_num = 16 + (page - 1) * 16;

            myLabel_yewei1.Machine_num = 1 + (page - 1) * 16;
            myLabel_yewei2.Machine_num = 2 + (page - 1) * 16;
            myLabel_yewei3.Machine_num = 3 + (page - 1) * 16;
            myLabel_yewei4.Machine_num = 4 + (page - 1) * 16;
            myLabel_yewei5.Machine_num = 5 + (page - 1) * 16;
            myLabel_yewei6.Machine_num = 6 + (page - 1) * 16;
            myLabel_yewei7.Machine_num = 7 + (page - 1) * 16;
            myLabel_yewei8.Machine_num = 8 + (page - 1) * 16;
            myLabel_yewei9.Machine_num = 9 + (page - 1) * 16;
            myLabel_yewei10.Machine_num = 10 + (page - 1) * 16;
            myLabel_yewei11.Machine_num = 11 + (page - 1) * 16;
            myLabel_yewei12.Machine_num = 12 + (page - 1) * 16;
            myLabel_yewei13.Machine_num = 13 + (page - 1) * 16;
            myLabel_yewei14.Machine_num = 14 + (page - 1) * 16;
            myLabel_yewei15.Machine_num = 15 + (page - 1) * 16;
            myLabel_yewei16.Machine_num = 16 + (page - 1) * 16; 

            //
            label_page.Text = page.ToString();
            if (page <= 1) { button1.Enabled = false; } else { button1.Enabled = true; }
            if (page >= 4) { button2.Enabled = false; } else { button2.Enabled = true; }
            label1.Text = (1 + (page - 1) * 16).ToString() + "号机";
            label2.Text = (2 + (page - 1) * 16).ToString() + "号机";
            label3.Text = (3 + (page - 1) * 16).ToString() + "号机";
            label4.Text = (4 + (page - 1) * 16).ToString() + "号机";
            label5.Text = (5 + (page - 1) * 16).ToString() + "号机";
            label6.Text = (6 + (page - 1) * 16).ToString() + "号机";
            label7.Text = (7 + (page - 1) * 16).ToString() + "号机";
            label8.Text = (8 + (page - 1) * 16).ToString() + "号机";
            label9.Text = (9 + (page - 1) * 16).ToString() + "号机";
            label10.Text = (10 + (page - 1) * 16).ToString() + "号机";
            label11.Text = (11 + (page - 1) * 16).ToString() + "号机";
            label12.Text = (12 + (page - 1) * 16).ToString() + "号机";
            label13.Text = (13 + (page - 1) * 16).ToString() + "号机";
            label14.Text = (14 + (page - 1) * 16).ToString() + "号机";
            label15.Text = (15 + (page - 1) * 16).ToString() + "号机";
            label16.Text = (16 + (page - 1) * 16).ToString() + "号机";
            Console.WriteLine("drawline endtime is " + DateTime.Now.ToString());
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                Thread newthread = new Thread(draw_line);
                newthread.Start();
            }

            // 删除两天以上的数据
            mv.mysqlconnection.excute_sql("delete from history where datediff(day,'"+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+"',save_time)>'3'");
            mv.mysqlconnection.excute_sql("delete from history where datediff(day,'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',save_time)<'-3'");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (page > 1) page = page - 1;
            if (page <= 1) button1.Enabled = false;
            draw_line();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (page < 4) page = page + 1;
            if (page >= 4) button2.Enabled = false;
            draw_line();
        }

        private void myLabel1_Click(object sender, EventArgs e)
        {

        }

        private void chart11_Click(object sender, EventArgs e)
        {
            mv.detail.Set_machine_num( 11 + (page - 1) * 16);
            mv.Show_Form(mv.detail);
            mv.detail.draw_line();
        }

        private void chart12_Click(object sender, EventArgs e)
        {
            mv.detail.Set_machine_num( 12 + (page - 1) * 16);
            mv.Show_Form(mv.detail);
            mv.detail.draw_line();
        }
    }
}
