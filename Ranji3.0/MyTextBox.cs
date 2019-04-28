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
    public partial class MyTextBox :TextBox
    {
        public string value;
        //private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer1;
        private int machine_num=1;      //本身的机号
        private string value_name="";    //变量名称
        private int kind;             //表达式种类 kind==1 表示为取得本身的Text
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

        private void init()
        {
            kind = 1;                //默认
            this.timer1 = new System.Windows.Forms.Timer();
            this.timer1.Enabled = true;
            this.timer1.Interval = 100;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            value = "0";
        }


        public MyTextBox()
        {
            init();
            //InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // 在这里添加所需要的显示种类
            if (kind == 1)
            {
               this.Text = get_value_R_DT();
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
                        return RealTime_data.DT[machine_num,address].ToString();
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
                    return "??";
                }
                catch
                {
                    return "error";
                }
            }
            return "??";
            
        }
       
    }
}
