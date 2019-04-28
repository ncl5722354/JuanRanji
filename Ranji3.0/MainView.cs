using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Ranji3._0
{
    public partial class MainView : Form
    {
        public SQL_Connection mysqlconnection = new SQL_Connection("server=.;database=Ranji3.0;uid=sa;pwd=ncl5722354");

        // 通过远程登录工艺管理系统进行操作
        //public SQL_Connection craft_connection = new SQL_Connection("server=.;database=Craft_Config;uid=sa;pwd=ncl5722354");
       
        public IniFile myini = new IniFile("D:\\ranji3.0 config\\dataconfig.ini");
        Rectangle rect = System.Windows.Forms.SystemInformation.VirtualScreen;
        public data_config mydataconfig;             //数据确定页面!!
        public Port_config myportconfig;             //串口确定页面!!
        public General general;                      //总貌画面!!
        public history History;                      //史历记录画面!!
        public Detail detail;                        //详细信息页面!!
        public Craft_Edit_View craft_edit_view;
        public BanzuSet banzuset_view;                //班组设置界面
        public Baobiao baobiao_view;                  //报表界面
        public Info info_view;                        //信息查询
        public static bool passworld_is = false;                    //判断密码是否正确

        public MainView()
        {
            InitializeComponent();
            
        }
     

        //读取数值
         private string get_value_R_DT(int machine_num,string value_name)
        {
            // 通过机号名称找到R 或 DT值， 再通过R或DT值进行判断
            string key_string = machine_num.ToString() + "_" + value_name.ToString();
            
                try
                {
                    DataSet ds = mysqlconnection.sql_search_database("Select * from data_table where data_name='" + key_string.ToString() + "'"); // ok
                    DataRow dr = ds.Tables[0].Rows[0];
                    string type_string = dr[1].ToString();
                    string address_value = dr[2].ToString();
                    
                    
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
                     
                        int address= qianmian * 16 + last_num;
                        return RealTime_data.R_jicun[machine_num, address].ToString();
                    }
                    return "0";
                }
                catch
                {
                    return "0";
                }
        }


         public void play_stop_qingqiu(int machine_num)
         {
             if (player1.playState != WMPLib.WMPPlayState.wmppsPlaying && player1.playState != WMPLib.WMPPlayState.wmppsTransitioning)
             {
                 player1.URL = "D:/config/wav/qingqiu_pause_" + machine_num + ".wav";
                 player1.Ctlcontrols.play();
             }
         }
         public void play_start_qingqiu(int machine_num)
         {
             if (player1.playState != WMPLib.WMPPlayState.wmppsPlaying && player1.playState != WMPLib.WMPPlayState.wmppsTransitioning)
             {
                 player1.URL = "D:/config/wav/qingqiu_start_" + machine_num + ".WAV";
                 player1.Ctlcontrols.play();
             }
         }


        private void Form1_Load(object sender, EventArgs e)
        {
            // create database
            mysqlconnection.excute_sql("CREATE table data_table(data_name nvarchar(50) PRIMARY KEY,data_type nvarchar(50),data_address nvarchar(50),machine_num nvarchar(60))"); //建立数据词典表
            mysqlconnection.excute_sql("CREATE table start_time(machine_num nvarchar(50) PRIMARY KEY,start_time datetime)");  // 建立开始时间表
            mysqlconnection.excute_sql("CREATE table history(machine_num nvarchar(50),value nvarchar(50),save_time datetime,machine_num_and_save_time nvarchar(50) PRIMARY KEY)");
            mysqlconnection.excute_sql("CREATE table liaogang(machine_num nvarchar(50), value nvarchar(50),save_time datetime,machine_num_and_save_time nvarchar(50) PRIMARY KEY)");
            mysqlconnection.excute_sql("CREATE table craftcode(craft_name nvarchar(50)  PRIMARY KEY, craft_code nvarchar(50))");  //建立工艺与工艺代码对照表
            mysqlconnection.excute_sql("CREATE table craft_machine_num(machine_num int PRIMARY KEY, craft_name nvarchar(50))");   //建立染机号与工艺名称的对照表

            // 工艺命名
            mysqlconnection.excute_sql("CREATE table craft_name(craft_num int PRIMARY KEY,craft_name nvarchar(50))");

            // 建立班组数据库
            mysqlconnection.excute_sql("CREATE table banzu_time(banzu_name nvarchar(50) PRIMARY KEY,starttime datetime,endtime datetime)");

            // 建立流量统计数据库
            mysqlconnection.excute_sql("CREATE table liuliang_history(timemachine_num nvarchar(50) PRIMARY KEY,machine_num int,time datetime,banzu nvarchar(50),value float)");

            // 建立液位统计数据库
            mysqlconnection.excute_sql("CREATE table yewei_history(timemachine_num nvarchar(50) PRIMARY KEY,machine_num int,time datetime,value float)");


            // 建立信息查询数据库
            mysqlconnection.excute_sql("CREATE table info_save(timemachine_num nvarchar(50) PRIMARY KEY,machine_num int,start_time datetime,craft_name nvarchar(MAX))");

            // 对60个染机分别建立工艺数据库
            for (int i = 1; i <= 60; i++)
            {
                mysqlconnection.excute_sql("CREATE table craft_machine" + i.ToString() + "(xuhao int PRIMARY KEY identity(1,1),wendu_shuiwei int,speed float,time int,craft nvarchar(50),nvarcharcode int,rate int,fengji_rate int,tibu_rate int)");
            }

            // 对100个定义好的工艺建立数据库
            for (int i = 1; i <= 500; i++)
            {
                mysqlconnection.excute_sql("CREATE table craft" + i.ToString() + "(xuhao int PRIMARY KEY identity(1,1) ,wendu_shuiwei int,speed float,time int,craft nvarchar(50),nvarcharcode int,rate int,fengji_rate int,tibu_rate int)");
            }
            this.Top = 0;
            this.Left = 0;
            this.Width = rect.Width;
            this.Height = rect.Height;
            //初使化各个界面

            info_view = new Info(this);
            info_view.MdiParent = this;
            Show_Form(info_view);

            baobiao_view = new Baobiao(this);
            baobiao_view.MdiParent = this;
            Show_Form(baobiao_view);

            banzuset_view = new BanzuSet(this);
            banzuset_view.MdiParent = this;
            Show_Form(banzuset_view);

            mydataconfig = new data_config(this);
            mydataconfig.MdiParent = this;
           
            Show_Form(mydataconfig);

            detail = new Detail(this,1);
            detail.MdiParent = this;
            Show_Form(detail);

            myportconfig = new Port_config(this);
            myportconfig.MdiParent = this;
            Show_Form(myportconfig);

            History = new history(this);
            History.MdiParent = this;
            Show_Form(History);

            craft_edit_view = new Craft_Edit_View(this);
            craft_edit_view.MdiParent = this;
            Show_Form(craft_edit_view);

            general = new General(this);
            general.MdiParent = this;
            Show_Form(general);


        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void Show_Form(Form myfrm)
        {
            myfrm.Show();
            myfrm.Focus();
            myfrm.Top = 0;
            myfrm.Left = 0;
            myfrm.Width = rect.Width;
            myfrm.Height = rect.Height;
        }
        private void 数据词典ToolStripMenuItem_Click(object sender, EventArgs e)
        {     
             passform newfrm = new passform();
            newfrm.ShowDialog();
            if (passworld_is == true)
            {
                Show_Form(mydataconfig);
            }
            passworld_is = false;

        }

        private void 通讯设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
             passform newfrm = new passform();
            newfrm.ShowDialog();
            if (passworld_is == true)
            {
                Show_Form(myportconfig);
            }
            passworld_is = false;
        }

      

        private void 关闭ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void 总貌ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            general.Visible = true;
            Show_Form(general);
            general.draw_line();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //模拟历史数据
            //MessageBox.Show("error!!!");
            int count = 60;
            for (int i = 1; i <= 60; i++)
            {
                try
                {
                    DateTime thistime = DateTime.Now;
                     // int wendu_count = mysqlconnection.sql_search_database("select * from ");

                    int value = int.Parse(get_value_R_DT(i,"机缸温度"));
                    //int value = 0+i+DateTime.Now.Hour;
                    mysqlconnection.excute_sql("insert into history values ('" + i.ToString() + "','" + value.ToString() + "','" + thistime.ToString("yyyy-MM-dd HH:mm:ss") + "','" + i.ToString() + thistime.ToString("yyyy-MM-dd HH:mm:ss") + "')");
                    //DateTime nowtime = DateTime.Now;
                    
                   
                    // 读取相应机缸的开始时间
                    DataSet ds = mysqlconnection.sql_search_database("select * from start_time where machine_num='" + i.ToString() + "'");
                    DateTime dt = (DateTime)ds.Tables[0].Rows[0][1];
                    float liuliang = float.Parse(get_value_R_DT(i,"累计流量"));
                    
                    Save_liuliang_Info(dt, i, liuliang);

                    float liaogang_value = float.Parse(get_value_R_DT(i, "料缸液位"));
                   
                    
                   // mysqlconnection.excute_sql("insert into liaogang values('" + i.ToString() + "','" + liaogang_value.ToString() + "','" + thistime.ToString("yyyy-MM-dd HH:mm:ss") + "','" + i.ToString() + thistime.ToString("yyyy-MM-dd HH:mm:ss") + "')");

                    float jigangyewei_value = float.Parse(get_value_R_DT(i, "机缸液位"))/10;
                   // mysqlconnection.excute_sql("insert into yewei_history values('"+thistime.ToString("yyyy-MM-dd HH:mm:ss")+i.ToString()+"','"+i.ToString()+"','"+thistime.ToString("yyyy-MM-dd HH:mm:ss")+"','"+jigangyewei_value.ToString()+"')");

                }
                catch
                {
                }
            }
        }

        private void Save_liuliang_Info(DateTime dt,int machine_num,float value)
        {
            // 保存流量信息，供报表使用
            try
            {
                string banzu = Get_Banzu_Info(dt);
                
                // 判断是否有这行
                if (int.Parse(get_value_R_DT(machine_num, "启动"))!= 0)
                {
                    DataSet ds = mysqlconnection.sql_search_database("select * from liuliang_history where timemachine_num='" + dt.ToString("yyyy-MM-dd HH:mm:ss") + machine_num.ToString().PadLeft(2, '0') + "'");
                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        // 没有这一行
                        mysqlconnection.excute_sql("insert into liuliang_history values ('" + dt.ToString("yyyy-MM-dd HH:mm:ss") + machine_num.ToString().PadLeft(2, '0') + "','" + machine_num.ToString() + "','" +
                            dt.ToString("yyyy-MM-dd HH:mm:ss") + "','" + banzu + "','" + value.ToString() + "')"); //写入数据库
                    }
                    else
                    {
                        // 这一行存在，需要更新数据
                        mysqlconnection.excute_sql("update liuliang_history set time='" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',banzu='" + banzu + "',value='" + value.ToString() + "' where timemachine_num='" + dt.ToString("yyyy-MM-dd HH:mm:ss") + machine_num.ToString().PadLeft(2, '0') + "'");
                    }
                }
            }
            catch
            {
            }
        }

        private string Get_Banzu_Info(DateTime dt)
        {
            // 根据时间返回班组信息
            DateTime zao_start = BanzuSet.zaoban_start;
            DateTime zao_end = BanzuSet.zaoban_end;

            DateTime zhong_start = BanzuSet.zhongban_start;
            DateTime zhong_end = BanzuSet.zhongban_end;

            DateTime wan_start = BanzuSet.wanban_start;
            DateTime wan_end = BanzuSet.wanban_end;

           string mytime_stirng  ="1900/1/1 "+dt.Hour.ToString()+":"+dt.Minute.ToString();
           DateTime mytime = DateTime.Parse(mytime_stirng);
          
            if (isintimespan(mytime, zao_start, zao_end) == true) return "zaoban";
            if (isintimespan(mytime, zhong_start, zhong_end) == true) return "zhongban";
            if (isintimespan(mytime, wan_start, wan_end) == true) return "wanban";
            return "";
        }

        private bool isintimespan(DateTime nowtime, DateTime starttime, DateTime endtime)
        {
            // 判断一个时间点是否在一个时间段内
            bool isin = false;
            TimeSpan ts = endtime - starttime;
            if (ts.TotalSeconds >= 0)
            {
                // 不过夜的时间
                TimeSpan tostart = nowtime - starttime;
                TimeSpan toend = endtime - nowtime;
                if (tostart.TotalSeconds >= 0 && toend.TotalSeconds >= 0)
                {
                    isin = true;
                }

            }
            else
            {
                TimeSpan tostart = nowtime - starttime;
                TimeSpan toend = endtime - nowtime;
                if (tostart.TotalSeconds >= 0 || toend.TotalSeconds >= 0)
                {
                    isin = true;
                }
            }
            return isin;
        }

       

        private void 历史记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Show_Form(History);
            general.Visible = false;
        }

        private void 工艺编辑ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            passform newfrm = new passform();
            newfrm.ShowDialog();
            if (passworld_is == true)
            {
                Show_Form(craft_edit_view);
            }
            passworld_is = false;
        }

        private void 班组设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Show_Form(banzuset_view);
        }

        private void 流量报表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Show_Form(baobiao_view);
            general.Visible = false;
        }

        private void timer_qingqiu_Tick(object sender, EventArgs e)
        {
            for (int i = 1; i <= 60; i++)
            {
                if (int.Parse(get_value_R_DT(i,"请求开始"))!= 0)
                {
                    play_start_qingqiu(i);
                    return;
                }
                if (int.Parse(get_value_R_DT(i,"请求暂停"))!= 0)
                {
                    play_stop_qingqiu(i);
                    return;
                }
            }
        }

        private void 信息查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Show_Form(info_view);
            general.Visible = false;
        }
    }
}
