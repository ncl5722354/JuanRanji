using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Windows.Forms;
using System.Threading;
using System.IO;

// 封装串口，实现串口的设置，发送，接收

namespace Ranji3._0
{
    public class port_tianxinliuliang
    {

        // 用来作为天信流量计的串口通讯模块
        public SerialPort sp = new SerialPort();

        public bool send_is = false;      // 发送数据标志
        public bool receive_is = false;   // 接收数据标志
        public int send_machine_num = 0;  // 所处理的机号
        public int send_gongnengma = 0;   // 所处理的功能码
        public int send_address_high = 0; // 所处理的高位地址
        public int send_address_low = 0;  // 所处理的低位地址
        public int time_out = 0;          // 设定的超时时间
        public int cmd_num = 1;           // 第几条命令
        public bool xunjian_is = true;


        public port_tianxinliuliang()
        {
            sp.DataReceived+=new SerialDataReceivedEventHandler(sp_DataReceived);
        }

        
        // 接收数据事件
        private void sp_DataReceived(object sender,SerialDataReceivedEventArgs e)
        {
            MessageBox.Show("data messge!");
            int count = 0;
            byte[] receive_char = new byte[1024];
            int ruffer_length = sp.BytesToRead;
            string a = Port_config.mycmd;
            byte[] array = new byte[1024];
            // 接数据回收
            while (true)
            {
                try
                {
                    array[count] = (byte)sp.ReadByte();
                    count++;
                    if (count > array[2] + 4 && (array[1] == 3 || array[1] == 1))
                    {
                        break;
                    }
                    else if (count > 7 && (array[1] == 6 || array[1] == 5))
                    {
                        break;
                    }
                    else if (array[1] == 16 && count > 7)
                    {
                        break;
                    }

                }
                catch
                {
                    break;
                }
            }
           

            // 发送命令


           // string canshu_type = myini.IniReadValue("Ranji_Type", array[0] + "号染机");
            //if (array[0] == Opened_machine_num && array[1] == 16)
            //{
            //    int myaddress = array[2] * 256 + array[3];
            //    if (myaddress == 2000) { down_wendu_is = true; download_datareceived = true; }
            //}

            if (array[1] == 5)
            {
                // 单独修改R值后的处理
                int highaddress = array[2];
                int lowaddress = array[3];
                int start_address = (256 * highaddress + lowaddress - 2048);
               // RealTime_data.R_jicun[array[0],start_address] = array[4];
            }
            if (array[1] == 6)
            {
                //单独修改DT值后的处理
                int highaddress = array[2];
                int lowaddress = array[3];
                int start_address = (256 * highaddress + lowaddress);
               // RealTime_data.DT[array[0],start_address] = array[5];
            }
            if (array[1] == 16)
            {
                //Detail.step = Detail.step + 1;
                if (array[2] == 7 && array[3] == 208) Detail.step = 1;
                if (array[2] == 9 && array[3] == 196) Detail.step = 2;
                if (array[2] == 11 && array[3] == 184) Detail.step = 3;
                if (array[2] == 13 && array[3] == 172) Detail.step = 4;
            }
            if (send_machine_num == array[0] && send_gongnengma == array[1] && send_is == true && receive_is == false)
            {
                //如果有接收 判断函数
                time_out = 0;
                string cmd = "";

                for (int i = 0; i < count; i++)
                {
                    cmd = cmd + array[i].ToString("X").PadLeft(2, '0');
                }
                if (array[1] == 3)
                {
                    int highaddress = send_address_high;
                    int lowaddress = send_address_low;
                    int start_address = 256 * highaddress + lowaddress;
                    int data_num = array[2];
                    for (int i = 0; i < data_num / 2; i++)
                    {

                       // RealTime_data.DT[array[0], start_address + i] = array[3 + i * 2] * 256 + array[3 + i * 2 + 1];
                        if (i == 40 && start_address+i == 702)
                        {
                            Console.WriteLine(RealTime_data.DT[1, 104] + "  " + array[3 + i * 2] * 256 + array[3 + i * 2 + 1]);
                        }

                        //int a = RunTime_data.DT[1, 0];
                    }
                }
                //解析R的值并根据R值在面页上做出变化
                
                if (array[1] == 1)
                {
                    //读取R的值
                    int highaddress = send_address_high;
                    int lowaddress = send_address_low;
                    int start_address = (256 * highaddress + lowaddress - 2048) / 8;

                    int data_num = array[2];
                    for (int i = 0; i < data_num; i++)
                    {
                        if (i % 2 == 0)
                        {
                            RealTime_data.R[array[0], start_address / 2 * 16 + i / 2] = (int)array[3 + i + 1] * 256 + array[3 + i];//!!!!!!!!!!!!!!!!!!! 43 65 if i%2 ==0

                        }
                        int b = RealTime_data.R[array[0], start_address / 2 * 16 + i / 2];
                        string erjinzhi = Convert.ToString(b, 2).PadLeft(16, '0');

                        for (int j = 0; j < 16; j++)
                        {
                           // RealTime_data.R_jicun[array[0], start_address / 2 * 16 + i / 2 * 16 + j] = int.Parse(erjinzhi.Substring(15 - j, 1));
                        }
                    }
                }
                receive_is = true; //接收置为真
            }
        }


        // 此串口发送命令
        public void Send_Write_Cmd6(int machine_num, string address, string data)
        {
            //发送修改DT命令
            //SerialPort sp = new SerialPort();
            
            string cmd = machine_num.ToString("X").PadLeft(2, '0') + "06" + address + data;
            xunjian_is = false;
            Thread.Sleep(300);
            send_data(cmd);
            Thread.Sleep(300);
            xunjian_is = true;
        }


        public void Send_Write_Cmd5(int machine_num, string address, string data)
        {
            //发送修改R命令
            SerialPort sp = new SerialPort();
            string cmd = machine_num.ToString("X").PadLeft(2, '0') + "05" + address + data;
            xunjian_is = false;
            Thread.Sleep(300);
            send_data(cmd);
            Thread.Sleep(300);
            xunjian_is = true;
        }

        public void Send_Write_Cmd16(int machine_num, int datalength, string address, int[] Date)
        {
            // 发送下载命令
            SerialPort sp = new SerialPort();
            string data_string = "";
            for (int i = 0; i < Date.Length; i++)
            {
                data_string = data_string + (Date[i] / 256).ToString("X").PadLeft(2, '0') + (Date[i] % 256).ToString("X").PadLeft(2, '0');
            }
            string cmd_string;
            cmd_string = machine_num.ToString("X").PadLeft(2, '0') + "10" + address + (datalength / 256).ToString("X").PadLeft(2, '0') + (datalength % 256).ToString("X").PadLeft(2, '0') + (2 * datalength).ToString("X").PadLeft(2, '0') + data_string;
            xunjian_is = false;
            Thread.Sleep(500);
            send_data(cmd_string);
            try 
            {
                StreamWriter sw = new StreamWriter("D:\\info.txt",true);
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss  ")+cmd_string+"\n");
                sw.Close();
            }
            catch { }
            //Thread.Sleep(600);
            xunjian_is = true;
        }
        // 串口属性设定
        public void sp_config(string COM_string, string botelv, string jiaoyan, string datalength, string stopbit)
        {
            if (botelv != "" && jiaoyan != "" && datalength != "" && stopbit != "" && sp.IsOpen == false)
            {
                //都不为空打开串口
                sp.PortName = COM_string;
                if (botelv == "2400bps") sp.BaudRate = 2400;
                if (botelv == "4800bps") sp.BaudRate = 4800;
                if (botelv == "9600bps") sp.BaudRate = 9600;
                if (botelv == "19200bps") sp.BaudRate = 19200;
                if (botelv == "38400bps") sp.BaudRate = 38400;
                if (jiaoyan == "奇校验") sp.Parity = System.IO.Ports.Parity.Odd;
                if (jiaoyan == "偶校验") sp.Parity = System.IO.Ports.Parity.Even;
                if (jiaoyan == "无校验") sp.Parity = System.IO.Ports.Parity.None;
                if (datalength == "8bits") sp.DataBits = 8;
                if (datalength == "7bits") sp.DataBits = 7;
                if (stopbit == "1bit") sp.StopBits = System.IO.Ports.StopBits.One;
                if (stopbit == "2bit") sp.StopBits = System.IO.Ports.StopBits.Two;
                try
                {
                    sp.Open();
                }
                catch
                {
                    MessageBox.Show("串口打开失败!");
                }
            }
        }

        // 通过串口将数据发送出去

        //public void Send_data(SerialPort sp, string send_string)
        //{
        //    //将send_string作为内容通过sp发送出去
        //    if (sp != null)
        //    {
        //        int data_length = send_string.Length / 2;
        //        byte[] data = new byte[data_length + 2];
        //        for (int i = 0; i < data_length; i++)
        //        {
        //            data[i] = Convert.ToByte(send_string.Substring(i * 2, 2), 16);
        //        }
        //        uint a = crc16_modbus(data, (uint)data_length);
        //        string byte_64 = a.ToString("X").PadLeft(4, '0');
        //        data[data_length] = Convert.ToByte(byte_64.Substring(2, 2), 16);
        //        data[data_length + 1] = Convert.ToByte(byte_64.Substring(0, 2), 16);
        //        if (sp.IsOpen == true && sp != null)
        //        {
        //            try
        //            {
        //                sp.Write(data, 0, data_length + 2); //要加处理
        //            }
        //            catch
        //            {
        //            }
        //        }
        //    }
        //}
        public void close()
        {
            sp.Close();
        }
        public void send_data(string mycmd)
        {
            try
            {
                if (sp != null)
                {
                    int data_length = mycmd.Length / 2;
                    byte[] data = new byte[data_length + 2];
                    for (int i = 0; i < data_length; i++)
                    {
                        data[i] = Convert.ToByte(mycmd.Substring(i * 2, 2), 16);
                    }
                    uint a = crc16_modbus(data, (uint)data_length);
                    string byte_64 = a.ToString("X").PadLeft(4, '0');
                    data[data_length] = Convert.ToByte(byte_64.Substring(2, 2), 16);
                    data[data_length + 1] = Convert.ToByte(byte_64.Substring(0, 2), 16);
                    if (sp.IsOpen == true && sp != null)
                    {
                        try
                        {
                            sp.Write(data, 0, data_length + 2); //要加处理
                        }
                        catch
                        {

                        }
                    }
                }
            }
            catch
            {

            }
        }
        // CRC16运算
        public uint crc16_modbus(byte[] modbusdata, uint length)
        {
            uint i, j;
            uint crc16 = 0xffff;
            for (i = 0; i < length; i++)
            {
                crc16 ^= modbusdata[i];
                for (j = 0; j < 8; j++)
                {
                    if ((crc16 & 0x01) == 1)
                    {
                        crc16 = (crc16 >> 1) ^ 0xA001;
                    }
                    else
                    {
                        crc16 = crc16 >> 1;
                    }
                }
            }
            return crc16;
        }
    }
}
