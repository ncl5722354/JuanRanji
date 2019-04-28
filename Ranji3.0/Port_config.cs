using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;

namespace Ranji3._0
{
    public partial class Port_config : Form
    {
        MainView mv;
        
        public port_moudbus mysp1 = new port_moudbus(); //定义了一个串口1
        public port_moudbus mysp2 = new port_moudbus(); //定义了一个串口2
        public port_tianxinliuliang mysp_tianxin = new port_tianxinliuliang();   // 定义了一个天信的串口通讯
        public IniFile port_info = new IniFile("D:\\ranji3.0 config\\port_config.ini"); //
        private int cmd_num=1;
        int liuliang_send_machine = 1;

        public static string mycmd = "";

        public static bool canxunjian = true;
        public Port_config(MainView mymv)
        {
            mv = mymv;
            InitializeComponent();
        }

        private void Reset_Port_combo(ComboBox mycb,port_moudbus mypm)
        {
            mycb.Items.Clear();
            for (int i = 1; i <= 20; i++)
            {
                try
                {
                    mypm.sp.PortName = "COM" + i.ToString();
                    mypm.sp.Open();
                    mycb.Items.Add("COM"+i.ToString());
                    mypm.sp.Close();
                }
                catch
                {
                }
            }
        }

        private void Reset_Liuliang_Combo(ComboBox mycb, port_tianxinliuliang mypm)
        {
            mycb.Items.Clear();
            for (int i = 1; i <= 20; i++)
            {
                try
                {
                    mypm.sp.PortName = "COM" + i.ToString();
                    mypm.sp.Open();
                    mycb.Items.Add("COM" + i.ToString());
                    mypm.sp.Close();
                }
                catch { }
            }
        }

        private void Reset_BoundRate_combo(ComboBox mycb)
        {
            mycb.Items.Add("2400bps");
            mycb.Items.Add("4800bps");
            mycb.Items.Add("9600bps");
            mycb.Items.Add("19200bps");
            mycb.Items.Add("38400bps");
        }

        private void Reset_Check_combo(ComboBox mycb)
        {
            mycb.Items.Add("偶校验");
            mycb.Items.Add("奇校验");
            mycb.Items.Add("无校验");
        }
        
        private void Reset_DataBits_combo(ComboBox mycb)
        {
            mycb.Items.Add("8bits");
            mycb.Items.Add("7bits");
        }

        private void Reset_StopBits_combo(ComboBox mycb)
        {
            mycb.Items.Add("2bit");
            mycb.Items.Add("1bit");
        }

        


        private void Port_config_Load(object sender, EventArgs e)
        {
            //mv.myini.IniWriteValue("CMD", "cmd1", "0300000070");
            //mv.myini.IniWriteValue("CMD", "cmd2", "0300700070");
            //mv.myini.IniWriteValue("CMD", "cmd3", "0300E00070");
            //mv.myini.IniWriteValue("CMD", "cmd4", "0301500070");
            //mv.myini.IniWriteValue("CMD", "cmd5", "0301C00070");
            //mv.myini.IniWriteValue("CMD", "cmd6", "0108000070");
            //mv.myini.IniWriteValue("CMD", "cmd7", "0108700070");
            //mv.myini.IniWriteValue("CMD", "cmd8", "0109500070");
            //mv.myini.IniWriteValue("CMD", "cmd9", "0109400070");
            //mv.myini.IniWriteValue("CMD", "cmd10", "0109C00070");
            //mv.myini.IniWriteValue("CMD", "cmd11", "010A300070");

            Reset_Port_combo(comboBox_port1,mysp1);      //  设置串口信息与下拉菜单
            Reset_BoundRate_combo(comboBox_boundrate1);
            Reset_Check_combo(comboBox_jiaoyan1);
            Reset_DataBits_combo(comboBox_databits1);
            Reset_StopBits_combo(comboBox_stopbits1);

            Reset_Port_combo(comboBox_port2, mysp2);      //  设置串口信息与下拉菜单
            Reset_BoundRate_combo(comboBox_boundrate2);
            Reset_Check_combo(comboBox_jiaoyan2);
            Reset_DataBits_combo(comboBox_databits2);
            Reset_StopBits_combo(comboBox_stopbits2);

            // 设置流量计串口
            //Reset_Port_combo(comboBox_port3, mysp_tianxin);      //  设置串口信息与下拉菜单
            Reset_Liuliang_Combo(comboBox_port3, mysp_tianxin);
            Reset_BoundRate_combo(comboBox_boundrate3);
            Reset_Check_combo(comboBox_jiaoyan3);
            Reset_DataBits_combo(comboBox_databits3);
            Reset_StopBits_combo(comboBox_stopbits3);

            // 为列表添加60台染机
            for (int i = 1; i <= 60; i++)
            {
                comboBox_machine_num1.Items.Add(i.ToString()+"号机");
            }

            for (int i = 1; i <= 60; i++)
            {
                comboBox_machine_num2.Items.Add(i.ToString() + "号机");
            }
            Load_port_info();
            Load_machine_info();
            //串口1
            try
            {
                mysp1.sp_config(comboBox_port1.Text, comboBox_boundrate1.Text, comboBox_jiaoyan1.Text, comboBox_databits1.Text, comboBox_stopbits1.Text);
                if (mysp1.sp.IsOpen == true)
                {
                    label_port1_is.Text = "串口打开";
                }
                else
                {
                    label_port1_is.Text = "串口关闭";
                }

            }
            catch
            {
                label_port1_is.Text = "串口关闭";
            }
            //串口2
            try
            {
                mysp2.sp_config(comboBox_port2.Text, comboBox_boundrate2.Text, comboBox_jiaoyan2.Text, comboBox_databits2.Text, comboBox_stopbits2.Text);
                if (mysp2.sp.IsOpen == true)
                {
                    label_port2_is.Text = "串口打开";
                }
                else
                {
                    label_port2_is.Text = "串口关闭";
                }
            }
            catch
            {
                label_port2_is.Text = "串口关闭";
            }
        }

        private void Load_port_info()
        {
            // 载入串口1信息
            string COM1_name = port_info.IniReadValue("COM1","port_name");
            comboBox_port1.Text = COM1_name;
            string COM1_boundrate = port_info.IniReadValue("COM1", "port_boundrate");
            comboBox_boundrate1.Text = COM1_boundrate;
            string COM1_check = port_info.IniReadValue("COM1", "port_jiaoyan");
            comboBox_jiaoyan1.Text = COM1_check;
            string COM1_databits = port_info.IniReadValue("COM1", "port_databits");
            comboBox_databits1.Text = COM1_databits;
            string COM1_stopbits = port_info.IniReadValue("COM1", "port_stopbits");
            comboBox_stopbits1.Text = COM1_stopbits;

            // 载入串口2信息
            string COM2_name = port_info.IniReadValue("COM2", "port_name");
            comboBox_port2.Text = COM2_name;
            string COM2_boundrate = port_info.IniReadValue("COM2", "port_boundrate");
            comboBox_boundrate2.Text = COM2_boundrate;
            string COM2_check = port_info.IniReadValue("COM2", "port_jiaoyan");
            comboBox_jiaoyan2.Text = COM2_check;
            string COM2_databits = port_info.IniReadValue("COM2", "port_databits");
            comboBox_databits2.Text = COM2_databits;
            string COM2_stopbits = port_info.IniReadValue("COM2", "port_stopbits");
            comboBox_stopbits2.Text = COM2_stopbits;
        }
        private void Load_machine_info()
        {
            //判断是机站对应哪个串口
            listBox1.Items.Clear();     //清除每个串口对应的机站列表
            listBox2.Items.Clear();

            for (int i = 1; i <= 60; i++)
            {
                string com_string= port_info.IniReadValue("COM_info",i.ToString()+"号机");
                if (com_string == "com1")
                {
                    listBox1.Items.Add(i.ToString()+"号机");
                }

            }
            for (int i = 1; i <= 60; i++)
            {
                string com_string = port_info.IniReadValue("COM_info", i.ToString() + "号机");
                if (com_string == "com2")
                {
                    listBox2.Items.Add(i.ToString() + "号机");
                }

            }
        }


        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox_port1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox_port1.Text == "")
            {
                MessageBox.Show("串口号不能为空!");
                return;
            }
            if (comboBox_boundrate1.Text == "")
            {
                MessageBox.Show("波特率不能为空!");
                return;
            }
            if (comboBox_jiaoyan1.Text == "")
            {
                MessageBox.Show("校验不能为空!");
                return;
            }
            if (comboBox_databits1.Text == "")
            {
                MessageBox.Show("数据位不能为空!");
                return;
            }
            if (comboBox_stopbits1.Text == "")
            {
                MessageBox.Show("停止位不能为空!");
                return;
            }
            port_info.IniWriteValue("COM1","port_name",comboBox_port1.Text);
            port_info.IniWriteValue("COM1","port_boundrate",comboBox_boundrate1.Text);
            port_info.IniWriteValue("COM1","port_jiaoyan",comboBox_jiaoyan1.Text);
            port_info.IniWriteValue("COM1","port_databits",comboBox_databits1.Text);
            port_info.IniWriteValue("COM1","port_stopbits",comboBox_stopbits1.Text);
            mysp1.sp_config(comboBox_port1.Text ,comboBox_boundrate1.Text ,comboBox_jiaoyan1.Text ,comboBox_databits1.Text ,comboBox_stopbits1.Text);
            label_port1_is.Text = "串口打开";
            Reset_Port_combo(comboBox_port1, mysp1);
            Reset_Port_combo(comboBox_port2, mysp2); 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox_machine_num1.Text != "")
            {
                port_info.IniWriteValue("COM_info",comboBox_machine_num1.Text,"com1");
            }
            Load_machine_info();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string delete_key = listBox1.SelectedItem.ToString();
                port_info.IniWriteValue("COM_info", delete_key, "com0");
                Load_machine_info();
            }
            catch
            {
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //mysp1.send_data("010300000070");
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            mysp1.close();
            label_port1_is.Text = "串口关闭";
            Reset_Port_combo(comboBox_port1, mysp1);
            Reset_Port_combo(comboBox_port2, mysp2); 
        }
        private int gethighaddress(string cmd)
        {
            // 从命令里面获得高位地址
            int highaddress = 0;
            try
            {
                string address_string = cmd.Substring(4, 4);
                int address_int = Convert.ToInt32(address_string, 16);
                highaddress = address_int / 256;
            }
            catch
            {
            }
            return highaddress;
        }
        private int getgongnengma(string cmd)
        {
            // 从命令里面获得功能码
            int gongnengma = 0;
            try
            {
                gongnengma = int.Parse(cmd.Substring(2, 2));
            }
            catch
            {
            }
            return gongnengma;
        }
        private int getlowaddress(string cmd)
        {
            // 从命令里面获得低位地址
            int lowaddress = 0;
            try
            {
                string address_string = cmd.Substring(4, 4);
                int address_int = Convert.ToInt32(address_string, 16);
                lowaddress = address_int % 256;
            }
            catch
            {
            }
            return lowaddress;
        }
        private void timer_sendis_Tick(object sender, EventArgs e)
        {
            if (mysp1.sp.IsOpen == true && canxunjian==true)
            {
                if (mysp1.cmd_num > 18)
                {
                    mysp1.cmd_num = 1;
                    mysp1.send_machine_num = mysp1.send_machine_num + 1;
                }
                if (mysp1.send_machine_num > 60) mysp1.send_machine_num = 1;
                if (mysp1.send_is == false && mysp1.receive_is == false)
                {
                    //串口1没有数据发送 发送数据
                    //com1_biaozhi.BackColor = System.Drawing.Color.Red;
                    while (port_info.IniReadValue("COM_info", mysp1.send_machine_num + "号机") != "com1")
                    {
                        mysp1.send_machine_num = mysp1.send_machine_num + 1; //换下一号
                        if (mysp1.send_machine_num > 60)
                        {
                            mysp1.send_machine_num = 1;
                            return;
                        }
                    }

                    string cmd = mv.myini.IniReadValue("CMD", "cmd" + mysp1.cmd_num);
                    cmd = mysp1.send_machine_num.ToString("X").PadLeft(2, '0') + cmd;
                    mysp1.send_address_high = gethighaddress(cmd);
                    mysp1.send_address_low = getlowaddress(cmd);
                    mysp1.send_gongnengma = getgongnengma(cmd);
                    if (mysp1.xunjian_is == true)
                    {
                        mysp1.sp.DiscardInBuffer();
                        mysp1.send_data(cmd);
                        mysp1.time_out = 0;
                        mysp1.send_is = true;
                    }
                }
                if (mysp1.send_is == true && mysp1.receive_is == false)
                {
                    mysp1.time_out = mysp1.time_out + 1;
                    if (mysp1.time_out > 50)
                    {
                        //超时了
                        mysp1.send_is = false;
                        
                        mysp1.send_machine_num = mysp1.send_machine_num + 1;
                        mysp1.cmd_num = 1;
                       
                    }
                }
                if (mysp1.send_is == true && mysp1.receive_is == true)
                {
                    mysp1.cmd_num = mysp1.cmd_num + 1;
                    mysp1.time_out = 0;
                    mysp1.send_is = false;
                    mysp1.receive_is = false;
                    //mysp_timeout = 0;
                    //mysp_sendis = false;
                    //mysp_receiveis = false;
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (comboBox_port2.Text == "")
            {
                MessageBox.Show("串口号不能为空!");
                return;
            }
            if (comboBox_boundrate2.Text == "")
            {
                MessageBox.Show("波特率不能为空!");
                return;
            }
            if (comboBox_jiaoyan2.Text == "")
            {
                MessageBox.Show("校验不能为空!");
                return;
            }
            if (comboBox_databits2.Text == "")
            {
                MessageBox.Show("数据位不能为空!");
                return;
            }
            if (comboBox_stopbits2.Text == "")
            {
                MessageBox.Show("停止位不能为空!");
                return;
            }
            port_info.IniWriteValue("COM2", "port_name", comboBox_port2.Text);
            port_info.IniWriteValue("COM2", "port_boundrate", comboBox_boundrate2.Text);
            port_info.IniWriteValue("COM2", "port_jiaoyan", comboBox_jiaoyan2.Text);
            port_info.IniWriteValue("COM2", "port_databits", comboBox_databits2.Text);
            port_info.IniWriteValue("COM2", "port_stopbits", comboBox_stopbits2.Text);
            mysp2.sp_config(comboBox_port2.Text, comboBox_boundrate2.Text, comboBox_jiaoyan2.Text, comboBox_databits2.Text, comboBox_stopbits2.Text);
            label_port2_is.Text = "串口打开";
            Reset_Port_combo(comboBox_port1, mysp1);
            Reset_Port_combo(comboBox_port2, mysp2); 
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            mysp2.close();
            label_port2_is.Text = "串口关闭";
            Reset_Port_combo(comboBox_port1, mysp1);
            Reset_Port_combo(comboBox_port2, mysp2); 
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (comboBox_machine_num2.Text != "")
            {
                port_info.IniWriteValue("COM_info", comboBox_machine_num2.Text, "com2");
            }
            Load_machine_info();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                string delete_key = listBox2.SelectedItem.ToString();
                port_info.IniWriteValue("COM_info", delete_key, "com0");
                Load_machine_info();
            }
            catch
            {
            }
        }

        private void timer_sendis2_Tick(object sender, EventArgs e)
        {
            if (mysp2.sp.IsOpen == true && canxunjian==true)
            {
                if (mysp2.cmd_num > 18)
                {
                    mysp2.cmd_num = 1;
                    mysp2.send_machine_num = mysp2.send_machine_num + 1;
                }
                if (mysp2.send_machine_num > 60) mysp2.send_machine_num = 1;
                if (mysp2.send_is == false && mysp2.receive_is == false)
                {
                    //串口1没有数据发送 发送数据
                    //com1_biaozhi.BackColor = System.Drawing.Color.Red;
                    while (port_info.IniReadValue("COM_info", mysp2.send_machine_num + "号机") != "com2")
                    {
                        mysp2.send_machine_num = mysp2.send_machine_num + 1; //换下一号
                        if (mysp2.send_machine_num > 60)
                        {
                            mysp2.send_machine_num = 1;
                            return;
                        }
                    }

                    string cmd = mv.myini.IniReadValue("CMD", "cmd" + mysp2.cmd_num);
                    cmd = mysp2.send_machine_num.ToString("X").PadLeft(2, '0') + cmd;
                    mysp2.send_address_high = gethighaddress(cmd);
                    mysp2.send_address_low = getlowaddress(cmd);
                    mysp2.send_gongnengma = getgongnengma(cmd);
                    if (mysp2.xunjian_is == true)
                    {
                        mysp2.sp.DiscardInBuffer();
                        mysp2.send_data(cmd);
                        mysp2.time_out = 0;
                        mysp2.send_is = true;
                    }
                }
                if (mysp2.send_is == true && mysp2.receive_is == false)
                {
                    mysp2.time_out = mysp2.time_out + 1;
                    if (mysp2.time_out > 50)
                    {
                        //超时了
                        mysp2.send_is = false;
                        //Button mybutton = getbutton("btn_rj" + mysp_machine_num.ToString().PadLeft(2, '0'));
                        //if (mybutton != null)
                        //{
                        //    mybutton.BackColor = System.Drawing.Color.Gray;
                        //}
                        mysp2.send_machine_num = mysp2.send_machine_num + 1;
                        mysp2.cmd_num = 1;
                        //com1_biaozhi.BackColor = System.Drawing.Color.Gray;

                    }
                }
                if (mysp2.send_is == true && mysp2.receive_is == true)
                {
                    mysp2.cmd_num = mysp2.cmd_num + 1;
                    mysp2.time_out = 0;
                    mysp2.send_is = false;
                    mysp2.receive_is = false;
                    //mysp_timeout = 0;
                    //mysp_sendis = false;
                    //mysp_receiveis = false;
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (comboBox_port3.Text == "")
            {
                MessageBox.Show("串口号不能为空!");
                return;
            }
            if (comboBox_boundrate3.Text == "")
            {
                MessageBox.Show("波特率不能为空!");
                return;
            }
            if (comboBox_jiaoyan3.Text == "")
            {
                MessageBox.Show("校验不能为空!");
                return;
            }
            if (comboBox_databits3.Text == "")
            {
                MessageBox.Show("数据位不能为空!");
                return;
            }
            if (comboBox_stopbits3.Text == "")
            {
                MessageBox.Show("停止位不能为空!");
                return;
            }
            port_info.IniWriteValue("COM3", "port_name", comboBox_port2.Text);
            port_info.IniWriteValue("COM3", "port_boundrate", comboBox_boundrate2.Text);
            port_info.IniWriteValue("COM3", "port_jiaoyan", comboBox_jiaoyan2.Text);
            port_info.IniWriteValue("COM3", "port_databits", comboBox_databits2.Text);
            port_info.IniWriteValue("COM3", "port_stopbits", comboBox_stopbits2.Text);
            mysp_tianxin.sp_config(comboBox_port3.Text, comboBox_boundrate3.Text, comboBox_jiaoyan3.Text, comboBox_databits3.Text, comboBox_stopbits3.Text);
            label_port3_is.Text = "串口打开";
            Reset_Port_combo(comboBox_port1, mysp1);
            Reset_Port_combo(comboBox_port2, mysp2);
            Reset_Liuliang_Combo(comboBox_port3, mysp_tianxin);
        }

        private void timer_liuliang_Tick(object sender, EventArgs e)
        {
            try
            {

                mycmd = liuliang_send_machine.ToString("X").PadLeft(2, '0') + "0300040002";
                mysp_tianxin.send_data(mycmd);
                liuliang_send_machine++;
                //if (liuliang_send_machine > 3)
                //    liuliang_send_machine = 1;
            }
            catch 
            { 
                liuliang_send_machine++;
                if (liuliang_send_machine > 20)
                    liuliang_send_machine = 1;
            }
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            mysp_tianxin.close();
            label_port3_is.Text = "串口关闭";
            //label_port2_is.Text = "串口关闭";
            Reset_Port_combo(comboBox_port1, mysp1);
            Reset_Port_combo(comboBox_port2, mysp2);
            Reset_Liuliang_Combo(comboBox_port3,mysp_tianxin);
        }
    }
}
