using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ranji3._0
{
    public partial class MyLabel : Label
    {
        public string value;
        //private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer1;
        private int machine_num = 1;      //本身的机号
        private string value_name = "";    //变量名称
        private int kind;             //表达式种类 kind==1 表示为取得本身的Text  
                                                // kind==2 表示为颜色 0为红 非0为绿
                                                
        public MainView mv;

        // 属性设置
        public int Machine_num
        {
            get { return machine_num; }
            set { machine_num = value; }
        }

        public string Value_name
        {
            get { return value_name; }
            set { value_name = value; }
        }

        public int Kind
        {
            get { return kind; }
            set { kind = value; }
        }
        public MyLabel()
        {
            //InitializeComponent();
            init();
        }
        private void init()
        {
            kind = 1;                //默认
            this.timer1 = new System.Windows.Forms.Timer();
            this.timer1.Enabled = true;
            this.timer1.Interval = 100;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            //Font = new System.Drawing.Font("宋体", 7, FontStyle.Bold);
            value = "0";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           
            if (kind == 1)
            {
                this.Text = get_value_R_DT();
            }
            if (kind == 2)
            {
                try
                {
                    if (int.Parse(get_value_R_DT()) == 0) this.BackColor = System.Drawing.Color.Gray;
                    else { this.BackColor = System.Drawing.Color.Green; }
                }
                catch
                {

                }
            }
            if (kind == 3)
            {
                try
                {
                    if (int.Parse(get_value_R_DT()) == 0) this.BackColor = System.Drawing.Color.Gray;
                    else { this.BackColor = System.Drawing.Color.Red; }
                }
                catch
                {

                }
            }
            if (kind == 4)
            {
                try
                {
                    float value = float.Parse(get_value_R_DT());
                    value = value / 10;
                    this.Text = value.ToString();
                }
                catch
                {
                }
            }
           
            if (kind == 5)
            {
                try
                {
                    int value = int.Parse(get_value_R_DT());
                    {
                        //value = 4800;
                        int secondes = value % 60;
                        int min = (value / 60) % 60;
                        int hour = value / 3600;
                        this.Text = hour.ToString().PadLeft(2, '0') + ":" + min.ToString().PadLeft(2, '0') + ":" + secondes.ToString().PadLeft(2, '0');
                    }
                }
                catch
                {
                }
            }
            if (kind == 6)
            {
                try
                {
                    this.Text = (int.Parse(get_value_R_DT())+1).ToString();
                }
                catch
                {
                }
            }
        }

        public void Set_value_R_DT(string value)
        {
            // 将DT或R的值改变
            string com_info = mv.myportconfig.port_info.IniReadValue("COM_info", machine_num.ToString() + "号机");
            port_moudbus myport = null;
            if (com_info == "com1") myport = mv.myportconfig.mysp1;
            if (com_info == "com2") myport = mv.myportconfig.mysp2;
            string key_string = machine_num.ToString() + "_" + value_name.ToString();
            if (mv != null)
            {
                try
                {
                    DataSet ds = mv.mysqlconnection.sql_search_database("Select * from data_table where data_name='" + key_string.ToString() + "'"); // ok
                    DataRow dr = ds.Tables[0].Rows[0];
                    string type_string = dr[1].ToString();
                    string address_value = dr[2].ToString();
                    // 计算地址
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
                    if (type_string == "DT")
                    {
                        address = int.Parse(address_value);
                    }
                    //
                    if (type_string == "R")
                    {
                        myport.Send_Write_Cmd5(Machine_num, (2048 + address).ToString("X").PadLeft(4, '0'), value);
                    }
                    if (type_string == "DT")
                    {
                        myport.Send_Write_Cmd6(Machine_num, address.ToString("X").PadLeft(4,'0'), value);
                    }
                }
                catch
                {
                }
            }

        }


        private string get_value_R_DT()
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
            return "0";

        }
    }
}
