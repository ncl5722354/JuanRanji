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
using System.IO;

namespace Ranji3._0
{
    public partial class Detail : Form
    {

        // 定义变量
        int[] data_2000 = new int[1];
        int[] data_2500 = new int[1];
        int[] data_3000 = new int[1];
        int[] data_3500 = new int[1];
        int datalength;
        int time_count = 0;
        port_moudbus myport = null;

        public MainView mv;
        public int machine_num=0;
        public static int step = 0;        // 下载的步数   
        public static int nowstep = 0;     // 现在正在下载的步
        public int chongfacishu = 0;
        public Detail(MainView mymv,int mymachine_num)
        {
           // Control.CheckForIllegalCrossThreadCalls = false;
            mv = mymv;
            machine_num = mymachine_num;
            InitializeComponent();
        }

        public void Set_machine_num(int set_machine_num)
        {
            // 变化机号时要设置所有自定义控件的站号
            machine_num = set_machine_num;
            myLabel_jiare.Machine_num = set_machine_num;
            myButton_start.Machine_num = set_machine_num;
            myButton_stop.Machine_num = set_machine_num;
            myLabel_lengque.Machine_num = set_machine_num;
            myLabel_zhubeng.Machine_num = set_machine_num;
            myLabel_shouzidong.Machine_num = set_machine_num;
            myLabel_jinshui1.Machine_num = set_machine_num;
            myLabel_jinshui2.Machine_num = set_machine_num;
            myLabel_jinshui3.Machine_num = set_machine_num;
            myLabel_jinshui4.Machine_num = set_machine_num;
            myLabel_paishui1.Machine_num = set_machine_num;
            myLabel_paishui2.Machine_num = set_machine_num;
            myLabel_paishui3.Machine_num = set_machine_num;
            myLabel_paishui4.Machine_num = set_machine_num;
            myLabel_shushui.Machine_num = set_machine_num;
            myLabel_yiliu.Machine_num = set_machine_num;
            myLabel_paiqi.Machine_num = set_machine_num;
            mybutton_shouzidong_open.Machine_num = set_machine_num;
            myButton_shouzidong_close.Machine_num = set_machine_num;
            myButton_xunhuanbeng_open.Machine_num = set_machine_num;
            myButton_xunhuanbeng_close.Machine_num = set_machine_num;
            myLabel_jigangwendu.Machine_num = set_machine_num;
            myLabel_jigangshuiwei.Machine_num = set_machine_num;
            myLabel_jigangyali.Machine_num = set_machine_num;
            myLabel_tiaoduan.Machine_num = set_machine_num;
            myLabel_leijiliuliang.Machine_num = set_machine_num;
            myLabel_liaogangyewei.Machine_num = set_machine_num;
            myLabel_liaoganghuiliu.Machine_num = set_machine_num;
            myLabel_zongruntime.Machine_num = set_machine_num;
            myLabel_runtime.Machine_num = set_machine_num;
            myLabel_jinliao.Machine_num = set_machine_num;
            myLabel_jiaoban.Machine_num = set_machine_num;
            myLabel_xunhuan.Machine_num = set_machine_num;
            myButton_qingqiukaishi.Machine_num = set_machine_num;
            myButton_qingqiuzanting.Machine_num = set_machine_num;
            myButton1.Machine_num = set_machine_num;
            myLabel_zhuansu1.Machine_num = set_machine_num;
            myLabel_zhuansu2.Machine_num = set_machine_num;
            myLabel_zhangli.Machine_num = set_machine_num;

            
            label_machine_num.Text = machine_num.ToString() + " 号 机";
            ReFlash_Craft_Chart();
            updata_datagridview();
        }

        private delegate void ThreadWork(Chart mychart, int machine_num);
        private void Detail_Load(object sender, EventArgs e)
        {
            // 在此处加入设置自定义控件的初始化
            myLabel_jiare.mv = mv;
            myButton_start.mv = mv;
            myButton_stop.mv = mv;
            myLabel_lengque.mv = mv;
            myLabel_zhubeng.mv = mv;
            myLabel_shouzidong.mv = mv;
            myLabel_jinshui1.mv = mv;
            myLabel_jinshui2.mv = mv;
            myLabel_jinshui3.mv = mv;
            myLabel_jinshui4.mv = mv;
            myLabel_paishui1.mv = mv;
            myLabel_paishui2.mv = mv;
            myLabel_paishui3.mv = mv;
            myLabel_paishui4.mv = mv;
            myLabel_shushui.mv = mv;
            myLabel_yiliu.mv = mv;
            myLabel_paiqi.mv = mv;
            mybutton_shouzidong_open.mv = mv;
            myButton_shouzidong_close.mv = mv;
            myButton_xunhuanbeng_open.mv = mv;
            myButton_xunhuanbeng_close.mv = mv;
            myLabel_jigangwendu.mv = mv;
            myLabel_jigangshuiwei.mv = mv;
            myLabel_jigangyali.mv = mv;
            myLabel_tiaoduan.mv = mv;
            myLabel_leijiliuliang.mv = mv;
            myLabel_liaogangyewei.mv = mv;
            myLabel_liaoganghuiliu.mv = mv;
            myLabel_zongruntime.mv = mv;
            myLabel_runtime.mv = mv;
            myLabel_jinliao.mv = mv;
            myLabel_jiaoban.mv = mv;
            myLabel_xunhuan.mv = mv;
            myButton_qingqiukaishi.mv = mv;
            myButton_qingqiuzanting.mv = mv;
            myButton1.mv = mv;
            myLabel_zhuansu1.mv = mv;
            myLabel_zhuansu2.mv = mv;
            myLabel_zhangli.mv = mv;

            timer3.Enabled = false;
            timer3.Interval = 10;
            timer4.Enabled = false;
            timer4.Interval = 10;
            timer5.Enabled = false;
            timer5.Interval = 10;
            timer6.Enabled = false;
            timer6.Interval = 10;

            //刷新工艺表
            draw_line();
        }

        public void Show_Craft_Name()
        {
            // 将对应机号的工艺名称显示出来
            try
            {
                DataSet ds = mv.mysqlconnection.sql_search_database("Select * from craft_machine_num where machine_num='" + machine_num.ToString() + "'");
                DataRow dr = ds.Tables[0].Rows[0];
                string craft_name = dr[1].ToString();
                label_now_craft.Text = craft_name;
            }
            catch
            {
                label_now_craft.Text = "";
            }
        }
        public void Reset_Craft_ComboBox()
        {
            // 刷新列表中的内容
            try
            {
                comboBox_craft.Items.Clear();
                for (int i = 1; i <=420; i++)
                {
                    string Craft_Name = "";
                    try
                    {
                        DataSet ds = mv.mysqlconnection.sql_search_database("Select * from craft_name where craft_num='" + i.ToString() + "'");
                        DataRow dr = ds.Tables[0].Rows[0];
                        Craft_Name = dr[1].ToString();

                    }
                    catch
                    {
                    }
                    comboBox_craft.Items.Add(i.ToString() + "   " + Craft_Name);
                }
                Show_Craft_Name();// 显示工艺名称
            }
            catch 
            {
            }
           
        }
        public void draw_line()
        {
            Thread newthread = new Thread(draw_reflush);
            newthread.Start();
        }
        private void draw_reflush()
        {
            setchart(chart1, machine_num);
            draw_chart(chart1, machine_num);
        }
        private void setchart(Chart mychart, int machine_num)
        {
            // 针对一个图表和对应的机号的处理
            if (mychart.InvokeRequired)
            {
                ThreadWork tw = new ThreadWork(setchart);
                this.Invoke(tw, new object[2] { mychart, machine_num });
            }
            else
            {
                try
                {
                    mychart.ChartAreas[0].AxisX.LabelStyle.Format = "HH:mm:ss";
                    DataSet ds = mv.mysqlconnection.sql_search_database("select * from start_time where machine_num=" + machine_num.ToString());
                    DataRow dr = ds.Tables[0].Rows[0];
                    DateTime starttime = DateTime.Parse(dr[1].ToString());
                    mychart.ChartAreas[0].AxisX.Minimum = starttime.ToOADate();
                    mychart.ChartAreas[0].AxisX.Maximum = starttime.AddHours(5).ToOADate();
                    mychart.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Hours;
                    mychart.ChartAreas[0].AxisX.Interval = 1;
                }
                catch
                {
                }
            }
        }

        public void Fill_Grid_From_Craft(int craft_num)
        {
            try
            {
                // 将表格用工艺列表填满
                dataGridView1.Rows.Clear();
                // 读取对应的工艺表
                DataSet ds = mv.mysqlconnection.sql_search_database("select * from craft" + craft_num.ToString());
                
                int row_count = ds.Tables[0].Rows.Count;

                // 将相应的信息保存至染机号与对应的工艺号表格中
                try
                {
                    //看是否能读出来表中内容
                    DataSet newds = mv.mysqlconnection.sql_search_database("Select * from craft_machine_num where machine_num='"+machine_num.ToString()+"'");
                    DataRow newdr = newds.Tables[0].Rows[0];
                    // 能够读出来，执行更新操作
                    mv.mysqlconnection.excute_sql("update craft_machine_num set craft_name='"+comboBox_craft.Text+"' where machine_num='"+machine_num.ToString()+"'");
                }
                catch
                {
                    //不能够读出来，就要插入一行
                    mv.mysqlconnection.excute_sql("insert into craft_machine_num(machine_num,craft_name) values ('"+machine_num.ToString()+"','"+comboBox_craft.Text+"')");
                }
                if (row_count == 0) return;    //如果行数是0，那么退出,不显示
                else
                {
                    dataGridView1.RowCount = row_count;
                }
                for (int i = 0; i < row_count; i++)
                {
                    // 将数据填充入表格中
                    try
                    {
                        DataRow dr = ds.Tables[0].Rows[i];
                        dataGridView1[0, i].Value = i + 1;
                        dataGridView1[1, i].Value = dr[1];
                        dataGridView1[2, i].Value = dr[2];
                        dataGridView1[3, i].Value = dr[3];
                        dataGridView1[4, i].Value = dr[4];
                        dataGridView1[5, i].Value = dr[6];
                        dataGridView1[6, i].Value = dr[7];
                        dataGridView1[7, i].Value = dr[8];
                    }
                    catch
                    {
                    }
                }
            }
            catch
            {
            }
        }

        public void ReFlash_Craft_Chart()
        {
            // 根据机缸对应的工艺表刷新datagridview
            try
            {
                dataGridView1.Rows.Clear();
                // 读取对应的工艺表
                DataSet ds = mv.mysqlconnection.sql_search_database("select * from craft_machine" + machine_num.ToString());
                int row_count = ds.Tables[0].Rows.Count;
                if (row_count == 0) return;    //如果行数是0，那么退出,不显示
                else
                {
                    dataGridView1.RowCount = row_count;
                }
                for (int i = 0; i < row_count; i++)
                {
                    // 将数据填充入表格中
                    try
                    {
                        DataRow dr = ds.Tables[0].Rows[i];
                        dataGridView1[0, i].Value = i + 1;
                        dataGridView1[1, i].Value = dr[1];
                        dataGridView1[2, i].Value = dr[2];
                        dataGridView1[3, i].Value = dr[3];
                        dataGridView1[4, i].Value = dr[4];
                        dataGridView1[5, i].Value = dr[6];
                        dataGridView1[6, i].Value = dr[7];
                        dataGridView1[7, i].Value = dr[8];
                    }
                    catch
                    {
                    }
                }
            }
            catch
            {
            }
        }
        private void draw_chart(Chart mychart, int machine_num)
        {
            //将对应的开始时间读出来
            if (mychart.InvokeRequired)
            {
                ThreadWork tw = new ThreadWork(draw_chart);
                this.Invoke(tw, new object[2] { mychart, machine_num });
            }
            else
            {
                try
                {

                    DataSet ds = mv.mysqlconnection.sql_search_database("Select * from start_time where machine_num=" + machine_num.ToString());
                    DataRow dr = ds.Tables[0].Rows[0];
                    DateTime starttime = DateTime.Parse(dr[1].ToString());
                    // 将历史记录中相相应的都读出来
                    ds = mv.mysqlconnection.sql_search_database("Select machine_num,value,save_time from history where machine_num=" + machine_num.ToString() + " and  save_time>" + "'" + starttime.ToString() + "' and save_time<'" + starttime.AddHours(4).ToString() + "' order by save_time");
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
                    //mychart.Series[0].Points.Clear();
                }

                // 画料缸液位曲线

                try
                {
                    DataSet ds = mv.mysqlconnection.sql_search_database("Select * from start_time where machine_num=" + machine_num.ToString());
                    DataRow dr = ds.Tables[0].Rows[0];
                    DateTime starttime = DateTime.Parse(dr[1].ToString());

                    ds = mv.mysqlconnection.sql_search_database("Select machine_num,value,save_time from liaogang where machine_num=" + machine_num.ToString() + " and save_time>" + "'" + starttime.ToString() + "' and save_time<'" + starttime.AddHours(4).ToString() + "' order by save_time");
                    int count = ds.Tables[0].Rows.Count;
                    mychart.Series[2].Points.Clear();
                    for (int i = 0; i < count; i++)
                    {
                        DataRow mydr = ds.Tables[0].Rows[i];
                        float myvalue = float.Parse(mydr[1].ToString()) / 10;
                        DateTime mytime = DateTime.Parse(mydr[2].ToString());
                        mychart.Series[2].Points.AddXY(mytime, myvalue);
                    }
                }
                catch
                {
                }
                // 画设定曲线
                try
                {
                    int datagridview_row_count = dataGridView1.Rows.Count;
                    DataSet ds = mv.mysqlconnection.sql_search_database("Select * from start_time where machine_num=" + machine_num.ToString());
                    DataRow dr = ds.Tables[0].Rows[0];
                    DateTime starttime = DateTime.Parse(dr[1].ToString()); //开始时间
                    mychart.Series[1].Points.Clear();
                    for (int i = 0; i < datagridview_row_count; i++)
                    {
                        if (dataGridView1[4, i].Value.ToString() == "温控")
                        {
                            int wendu = int.Parse(dataGridView1[1, i].Value.ToString());
                            mychart.Series[1].Points.AddXY(starttime, wendu);
                            int timespan = int.Parse(dataGridView1[3, i].Value.ToString());
                            starttime = starttime.AddMinutes(timespan);
                        }
                    }
                }
                catch
                {

                }
            }

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            draw_line();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void myLabel1_Click(object sender, EventArgs e)
        {

        }

        private void myLabel_lengque_Click(object sender, EventArgs e)
        {

        }

        private void myLabel_shushui_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void myButton1_Click(object sender, EventArgs e)
        {
            myButton_xunhuanbeng_close.Set_value_R_DT("0000");
        }

        private void myButton1_Click_1(object sender, EventArgs e)
        {
            if (myButton_stop.get_value_R_DT() == "0") { myButton_stop.Set_value_R_DT("ff00"); myButton_shouzidong_close.Set_value_R_DT("0000"); }
            else { myButton_stop.Set_value_R_DT("0000"); }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void save_info()
        {
            // 将信息记录下来
            try
            {
                DataSet ds = mv.mysqlconnection.sql_search_database("select * from start_time where machine_num='" + machine_num.ToString()+"'");
                string thisstart_time = ds.Tables[0].Rows[0][1].ToString();
                DateTime mydatetime = DateTime.Parse(thisstart_time);
                string nowtime = mydatetime.ToString("yyyy-MM-dd HH:mm:ss");
                string craft_name = label_now_craft.Text;
                mv.mysqlconnection.excute_sql("insert into info_save values ('"+thisstart_time+machine_num.ToString()+"','"+machine_num.ToString()+"','"+nowtime+"','"+craft_name+"')");
            }
            catch
            {
            }
        }

        public static Array Redim(Array origArray, int desiredSize)
        {
            //determine the type of element
            Type t = origArray.GetType().GetElementType();
            //create a number of elements with a new array of expectations
            //new array type must match the type of the original array
            Array newArray = Array.CreateInstance(t, desiredSize);
            //copy the original elements of the array to the new array
            Array.Copy(origArray, 0, newArray, 0, Math.Min(origArray.Length, desiredSize));
            //return new array
            return newArray;
        }
        private void myButton_start_Click(object sender, EventArgs e)
        {
            //只是暂时的，到时候要加入下载信息
            // 首先相应的机号的端口号
            try
            {
                Port_config.canxunjian = false;
                step = 0;   
                string com_info = mv.myportconfig.port_info.IniReadValue("COM_info", machine_num.ToString() + "号机");
                myport = null;
                if (com_info == "com1") myport = mv.myportconfig.mysp1;
                if (com_info == "com2") myport = mv.myportconfig.mysp2;

                label_step.Text = "开始下载工艺……";
                // 取得要下载的数据
                datalength = dataGridView1.Rows.Count;
                //int[] data_2000 = new int[datalength];
                //int[] data_2500 = new int[datalength];
                //int[] data_3000 = new int[datalength];
                //int[] data_3500 = new int[datalength];
                data_2000 =(int[])Redim(data_2000,datalength);
                data_2500 = (int[])Redim(data_2500,datalength);
                data_3000 = (int[])Redim(data_3000,datalength);
                data_3500 = (int[])Redim(data_3500,datalength);

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    DataSet ds = mv.mysqlconnection.sql_search_database("select * from craft_machine" + machine_num.ToString());
                    int row_count = ds.Tables[0].Rows.Count;
                    DataRow craftrow = null;     //最后一行
                    if (row_count > 0) craftrow = ds.Tables[0].Rows[row_count - 1];
                    // 寻找温控的工艺代码
                    int code=0;
                    try
                    {
                        ds = mv.mysqlconnection.sql_search_database("select craft_code from craftcode where craft_name='" + dataGridView1[4, i].Value.ToString().Trim() + "'");
                        DataRow dr = ds.Tables[0].Rows[0];
                        code = int.Parse(dr[0].ToString()); //在工艺列表里找到
                    }
                    catch
                    {
                        code = int.Parse(dataGridView1[4,i].Value.ToString());
                    }
                    data_2000[i] = int.Parse(dataGridView1[1, i].Value.ToString());
                    data_2500[i] = int.Parse(dataGridView1[3, i].Value.ToString());
                    data_3000[i] = code;
                    data_3500[i] = int.Parse(dataGridView1[5, i].Value.ToString());
                }
                progressBar1.Value = 10;
                myport.Send_Write_Cmd16(machine_num, datalength, "07D0", data_2000);
                
                timer3.Enabled = true;


               // //label_step.Text = "下载进度1";
               // progressBar1.Value = 20;
               // myport.Send_Write_Cmd16(machine_num, datalength, "09C4", data_2500);
               //// label_step.Text = "下载进度2";
               // progressBar1.Value = 30;
               // myport.Send_Write_Cmd16(machine_num, datalength, "0BB8", data_3000);
               //// label_step.Text = "下载进度3";
               // progressBar1.Value = 40;
               // myport.Send_Write_Cmd16(machine_num, datalength, "0DAC", data_3500);
               //// label_step.Text = "下载进度4";
               // progressBar1.Value = 70;


               // myButton_start.Set_value_R_DT("ff00");

               // Thread.Sleep(800);

               // if (step == 4) { progressBar1.Value = 100; }
               // else { MessageBox.Show("请检查下载情况!"); }
               // step = 0;

                //开始时间信息
                DateTime dt = DateTime.Now;

                string nowtime = dt.ToString("yyyyMMddHHmmss");
                string nowstart_time = dt.ToString("yyyy-MM-dd HH:mm:ss");
               
                    mv.mysqlconnection.excute_sql("update start_time set start_time='" + nowstart_time + "' where machine_num='" + machine_num.ToString() + "'");
                    try
                    {
                        mv.mysqlconnection.excute_sql("Create table info"+machine_num.ToString()+nowtime+"(duanhao int primary key,endtime datetime,data1 nvarchar(50),data2 nvarchar(50),data3 nvarchar(50),data4 nvarchar(50))");
                    }
                    catch
                    {
                    }
                
               // draw_line();
               // label_step.Text = "";
               // save_info();
               // progressBar1.Value = 0;
               // step = 0;
               //// MessageBox.Show("下载完成！");
                
            }
            catch(Exception ex)
            {
                MessageBox.Show("通讯错误！");
              
                progressBar1.Value = 0;
            }
        }

        private void mybutton_shouzidong_open_Click(object sender, EventArgs e)
        {
             mybutton_shouzidong_open.Set_value_R_DT("ff00");
        }

        private void myButton_shouzidong_close_Click(object sender, EventArgs e)
        {
            myButton_shouzidong_close.Set_value_R_DT("0000");
        }

        private void myButton_xunhuanbeng_open_Click(object sender, EventArgs e)
        {
            myButton_xunhuanbeng_open.Set_value_R_DT("ff00");
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            edit_craft myfrm = new edit_craft(mv,machine_num,1);
            myfrm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            delete_row();
            ReFlash_Craft_Chart(); //刷新
            updata_datagridview();
            draw_line();
        }

        public void delete_row()
        {
            // 删除指定的一行
            try
            {
                int delete_row_index = dataGridView1.CurrentRow.Index;  //要删除的行
                DataSet ds = mv.mysqlconnection.sql_search_database("select xuhao from craft_machine"+machine_num.ToString());
               // int xuhao =int.Parse( ds.Tables[0].Rows[delete_row_index].ToString());
                DataRow dr = ds.Tables[0].Rows[delete_row_index];
                int xuhao = int.Parse(dr[0].ToString());
                mv.mysqlconnection.excute_sql("delete from craft_machine" + machine_num.ToString() + " where xuhao='" + xuhao.ToString() + "'");

            }
            catch
            {
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            updata_datagridview();
            draw_line();
        }

        public void ReFill_Database_from_grid()
        {
            //将读进来的表送给对应机号的数据库中 首先删除对应的数据库
            try
            {
                mv.mysqlconnection.excute_sql("delete from craft_machine"+machine_num.ToString());
            }
            catch
            {
            }
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                //一行一行往上加
                try
                {
                    string craft = dataGridView1[4, i].Value.ToString();
                    DataSet ds = mv.mysqlconnection.sql_search_database("select craft_code from craftcode where craft_name='" + craft + "'");
                    DataRow newdr = ds.Tables[0].Rows[0];
                    int code = int.Parse(newdr[0].ToString());  // 工艺代码
                    mv.mysqlconnection.excute_sql("insert into craft_machine" + machine_num.ToString() + "(wendu_shuiwei,speed,time,craft,nvarcharcode,rate,fengji_rate,tibu_rate) values ('" + dataGridView1[1,i].Value.ToString() + "','" +  dataGridView1[2,i].Value.ToString() + "','" + dataGridView1[3,i].Value.ToString() + "','" + dataGridView1[4,i].Value.ToString() + "','" + code.ToString() + "','" + dataGridView1[5,i].Value.ToString() + "','" + dataGridView1[6,i].Value.ToString() + "','" + dataGridView1[7,i].Value.ToString() + "')");
                }
                catch
                {
                }
            }
        }

        public void updata_datagridview()
        {
            // 更新一下，对于每一行都更新
            try
            {
                for (int i = 0; i < dataGridView1.RowCount-1; i++)
                {
                    //首先计算时间
                    if (dataGridView1[4, i].Value.ToString() == "温控" && dataGridView1[4, i + 1].Value.ToString() == "温控")
                    {
                        float wendu = float.Parse(dataGridView1[1,i].Value.ToString());
                        float mubiao_wendu = float.Parse(dataGridView1[1,i+1].Value.ToString());
                        float speed = float.Parse(dataGridView1[2,i].Value.ToString());
                        if (speed != 0 && (wendu != mubiao_wendu))
                        {
                            dataGridView1[3, i].Value =(int)(Math.Abs(wendu-mubiao_wendu)/speed);
                        }
                    }
                }

                DataSet ds = mv.mysqlconnection.sql_search_database("select * from craft_machine" + machine_num.ToString());
                //DataRow dr = ds.Tables[0].Rows[i];
                int nowrows = ds.Tables[0].Rows.Count;  //根据现有的行数来判断是更新还是插入
                ReFill_Database_from_grid();

                
            }
            catch
            {

            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Fill_Grid_From_Craft(comboBox_craft.SelectedIndex+1);
            ReFill_Database_from_grid();
            Show_Craft_Name();
            draw_line();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                int value = int.Parse(textBox_tiaoduan.Text);
                value = value - 1;
                myLabel_tiaoduan.Set_value_R_DT(value.ToString("X").PadLeft(4, '0'));
            }
            catch
            {
            }
        }

        private string get_value_R_DT(int machine_num,string value_name)
        {
            // 通过机号名称找到R 或 DT值， 再通过R或DT值进行判断
            string key_string = machine_num.ToString() + "_" + value_name.ToString();
            if (mv != null)
            {
                try
                {
                    DataSet ds = mv.mysqlconnection.sql_search_database("Select * from data_table where data_name='" + key_string.ToString() + "'"); // ok
                    DataRow dr = ds.Tables[0].Rows[0];
                    string type_string = dr[1].ToString();
                    string address_value = dr[2].ToString();
                    //int ma = int.Parse(dr[3].ToString());

                    if (type_string == "DT")
                    {
                        int address = int.Parse(address_value);

                        return RealTime_data.DT[machine_num, address].ToString();
                    }
                    if (type_string == "R")
                    {
                        int length = address_value.Length;
                        int last_num = 0;
                        if (length > 1)
                        {
                            string address_string = address_value.Substring(1, length - 1);  //16进制处理
                            int int_length = address_string.Length;
                            string last_string = address_string.Substring(int_length - 1, 1);
                            last_num = Convert.ToInt32(last_string, 16);
                        }
                        else if (length == 1)
                        {
                            last_num = Convert.ToInt32(address_value, 16);
                        }
                        int qianmian = 0;
                        try
                        {
                            qianmian = int.Parse(address_value.Substring(0, length - 1));
                        }
                        catch
                        {
                            qianmian = 0;
                        }

                        int address = qianmian * 16 + last_num;
                        return RealTime_data.R_jicun[machine_num, address].ToString();
                    }
                    return "0";
                }
                catch
                {
                    return "0";
                }
            }
            return "0";

        }


        private void timer2_Tick(object sender, EventArgs e)
        {


            for (int i = 0; i < 60; i++)
            {
                try
                {
                    if (i == 14)
                    {
                        int b = 0;
                    }
                    DataSet ds = mv.mysqlconnection.sql_search_database("select * from start_time where machine_num='" + (i + 1).ToString() + "'");
                    string thisstart_time = ds.Tables[0].Rows[0][1].ToString();
                    DateTime dt = DateTime.Parse(thisstart_time);
                    //  寻找对应的数据库
                   
                    string table_name = "info" + (i+1).ToString() + dt.ToString("yyyyMMddHHmmss");

                    DataSet myyunxing_craft = mv.mysqlconnection.sql_search_database("select * from craft_machine"+(i+1).ToString());
                    string zhengzaiyunxing = get_value_R_DT(i+1, "跳段");
                    int now_duanhao = int.Parse(zhengzaiyunxing) + 1;
                    string data1 = myyunxing_craft.Tables[0].Rows[now_duanhao - 1][1].ToString();
                    string data2 = myyunxing_craft.Tables[0].Rows[now_duanhao - 1][2].ToString();
                    string data3 = myyunxing_craft.Tables[0].Rows[now_duanhao - 1][3].ToString();
                    string data4 = myyunxing_craft.Tables[0].Rows[now_duanhao - 1][4].ToString();
                    mv.mysqlconnection.excute_sql("insert into " + table_name + " values('" + now_duanhao.ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','"+data1+"','"+data2+"','"+data3+"','"+data4+"')");
                }
                catch { }
            }
            
            
            try
            {
                label_now_craft.Text = Read_Craft();
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    if (i <= int.Parse(myLabel_tiaoduan.Text)-1)
                    {
                        for (int j = 0; j < dataGridView1.ColumnCount; j++)
                        {
                            dataGridView1.Rows[i].Cells[j].Style.BackColor = System.Drawing.Color.Gray;
                        }
                    }
                    else
                    {
                        for (int j = 0; j < dataGridView1.ColumnCount; j++)
                        {
                            dataGridView1.Rows[i].Cells[j].Style.BackColor = System.Drawing.Color.White;
                        }
                    }
                }
            }
            catch
            {

            }
            if (myButton_start.get_value_R_DT() == "0")
            {
                // 没有开始，可以编辑
                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
                button6.Enabled = true;
                button4.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                button6.Enabled = false;
                button4.Enabled = false;
            }

            
        }
        public string Read_Craft()
        {
            //读取这个界面的工艺名称
            string craft_name = "";
            try
            {
               DataSet ds= mv.mysqlconnection.sql_search_database("select * from craft_machine_num where machine_num='"+machine_num.ToString()+"'");
               craft_name = ds.Tables[0].Rows[0][1].ToString();
            }
            catch
            {

            }
            return craft_name;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                edit_craft myfrm = new edit_craft(mv, machine_num, 3);
                myfrm.start_index = dataGridView1.SelectedCells[0].RowIndex;
                
                myfrm.ShowDialog();
            }
            catch
            {
                MessageBox.Show("请选择插入位置");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                int jinshui_shuiwei = int.Parse(textBox_jinshuishuiwei.Text);
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    string craft_string = "";
                    try
                    {
                        craft_string = dataGridView1[4, i].Value.ToString().Substring(0, 4);
                    }
                    catch
                    {
                    }

                    if (craft_string == "机缸进水" || craft_string == "停泵进水")
                    {
                        dataGridView1[1, i].Value = jinshui_shuiwei;
                    }
                }
            }
            catch
            {
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                int jinshui_shuiwei = int.Parse(textBox1_paishuishuiwei.Text);
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    string craft_string = "";
                    try
                    {
                        craft_string = dataGridView1[4, i].Value.ToString().Substring(0, 4);
                    }
                    catch
                    {
                    }

                    if (craft_string == "机缸排水" || craft_string == "停泵进水")
                    {
                        dataGridView1[1, i].Value = jinshui_shuiwei;
                    }
                }
            }
            catch
            {
            }
        }

        private void myButton_qingqiukaishi_Click(object sender, EventArgs e)
        {
            myButton_qingqiukaishi.Set_value_R_DT("0000");
        }

        private void myButton_qingqiuzanting_Click(object sender, EventArgs e)
        {
            myButton_qingqiuzanting.Set_value_R_DT("0000");
        }

        private void myButton1_Click_2(object sender, EventArgs e)
        {
            if (myButton1.get_value_R_DT() == "0") myButton1.Set_value_R_DT("ff00");
            else { myButton1.Set_value_R_DT("0000"); }
        }

       

        private void timer3_Tick_1(object sender, EventArgs e)
        {
            if (step == 1)
            {
                progressBar1.Value = 20;
                myport.Send_Write_Cmd16(machine_num, datalength, "09C4", data_2500);
                time_count=0;
                timer3.Enabled = false;
                timer4.Enabled = true;
 
            }
            time_count = time_count + 1;
            if (time_count >= 200)
            {
                timer3.Enabled = false;
                time_count = 0;
                chongfacishu = chongfacishu + 1;
                 if(chongfacishu>=3)
                 {
                     MessageBox.Show("重试大于三次！超时！");
                     chongfacishu = 0;
                     timer3.Enabled = false;
                     step = 0;
                     Port_config.canxunjian = true;
                     return;
                 }
                timer3.Enabled = true;
                
            }
        }

        private void timer4_Tick(object sender, EventArgs e)
        {

        }

        private void timer5_Tick(object sender, EventArgs e)
        {

        }

        private void timer4_Tick_1(object sender, EventArgs e)
        {
            if (step == 2)
            {
                progressBar1.Value = 30;
                myport.Send_Write_Cmd16(machine_num, datalength, "0BB8", data_3000);
                time_count = 0;
                timer4.Enabled = false;
                timer5.Enabled = true;
            }
            time_count = time_count + 1;
            if (time_count >= 200)
            {
                timer4.Enabled = false;
                time_count = 0;
                chongfacishu = chongfacishu + 1;
                if (chongfacishu >= 3)
                {
                    MessageBox.Show("重试大于三次！超时！");
                    chongfacishu = 0;
                    timer4.Enabled = false;
                    step = 0;
                    Port_config.canxunjian = true;
                    return;
                }
                timer4.Enabled = true;
            }
        }

        private void timer5_Tick_1(object sender, EventArgs e)
        {
            if (step == 3)
            {
                progressBar1.Value = 40;
                myport.Send_Write_Cmd16(machine_num, datalength, "0DAC", data_3500);
                time_count = 0;
                timer5.Enabled = false;
                timer6.Enabled = true;
            }
            time_count = time_count + 1;
            if (time_count >= 200)
            {
                timer5.Enabled = false;
                time_count = 0;
                chongfacishu = chongfacishu + 1;
                if (chongfacishu >= 3)
                {
                    MessageBox.Show("重试大于三次！超时！");
                    chongfacishu = 0;
                    timer5.Enabled = false;
                    step = 0;
                    Port_config.canxunjian = true;
                    return;
                }
                timer5.Enabled = true;
            }
        }

        private void timer6_Tick(object sender, EventArgs e)
        {
            if (step == 4)
            {
                step = 0;
                progressBar1.Value = 70;
                 myButton_start.Set_value_R_DT("ff00");
                 Port_config.canxunjian = true;
                 time_count = 0;
                 timer6.Enabled = false;
                draw_line();
                // label_step.Text = "";
                 save_info();
                 progressBar1.Value = 0;
            }
            time_count = time_count + 1;
            if (time_count >= 200)
            {
            //    timer6.Enabled = false;
            //    time_count = 0;
            //    step = 0;
            //    MessageBox.Show("下载超时，请重试！");
                timer6.Enabled = false;
                time_count = 0;
                chongfacishu = chongfacishu + 1;
                if (chongfacishu >= 3)
                {
                    MessageBox.Show("重试大于三次！超时！");
                    chongfacishu = 0;
                    timer6.Enabled = false;
                    step = 0;
                    Port_config.canxunjian = true;
                    return;
                }
                timer6.Enabled = true;
            }
        }

        private void myLabel_tiaoduan_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Visible == true)
            {
                pictureBox1.Visible = false;
                label18.Visible = false;
                myLabel_zhuansu1.Visible = false;
                label19.Visible = false;
                myLabel_zhuansu2.Visible = false;
                label20.Visible = false;
                myLabel_zhangli.Visible = false;
            }
            else
            {
                pictureBox1.Visible = true;
                label18.Visible = true;
                myLabel_zhuansu1.Visible = true;
                label19.Visible = true;
                myLabel_zhuansu2.Visible = true;
                label20.Visible = true;
                myLabel_zhangli.Visible = true;
            }

        }
    }
}
