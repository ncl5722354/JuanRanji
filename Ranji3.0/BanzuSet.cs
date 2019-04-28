using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ranji3._0
{
    public partial class BanzuSet : Form
    {
        bool change_is=false;
        public MainView mv;
        
        //  关于时间的公共变量 
        static public DateTime zaoban_start;
        static public DateTime zaoban_end;
        static public DateTime zhongban_start;
        static public DateTime zhongban_end;
        static public DateTime wanban_start;
        static public DateTime wanban_end;

        public BanzuSet(MainView mymv)
        {
            InitializeComponent();
            mv = mymv;
        }

        public DateTime Read_Banzu_Info(string Banzu_Name,string banzu_info)
        {
            DateTime dt=DateTime.Parse("00:00");
            try
            {
                DataSet ds = mv.mysqlconnection.sql_search_database("select * from banzu_time where banzu_name='"+Banzu_Name+"'");
                DataRow dr = ds.Tables[0].Rows[0];
                if (banzu_info == "start") { return (DateTime)dr[1]; }
                if (banzu_info == "end") { return (DateTime)dr[2]; }
                return dt;
            }
            catch
            {
                return dt;
            }
        }


        public void Set_Banzu_info()
        {
            zaoban_start = Read_Banzu_Info("zaoban","start");
            zaoban_end = Read_Banzu_Info("zaoban","end");
            zhongban_start = Read_Banzu_Info("zhongban","start");
            zhongban_end = Read_Banzu_Info("zhongban","end");
            wanban_start = Read_Banzu_Info("wanban","start");
            wanban_end = Read_Banzu_Info("wanban","end");

            textBox_zao_start_hour.Text = zaoban_start.Hour.ToString();
            textBox_zao_start_min.Text = zaoban_start.Minute.ToString();

            textBox_zhong_start_hour.Text = zhongban_start.Hour.ToString();
            textBox_zhong_start_min.Text = zhongban_start.Minute.ToString();

            textBox_wan_start_hour.Text = wanban_start.Hour.ToString();
            textBox_wan_start_min.Text = wanban_start.Minute.ToString();

            textBox_zao_end_hour.Text = zaoban_end.Hour.ToString();
            textBox_zao_end_min.Text = zaoban_end.Minute.ToString();

            textBox_zhong_end_hour.Text = zhongban_end.Hour.ToString();
            textBox_zhong_end_min.Text = zhongban_end.Minute.ToString();

            textBox_wan_end_hour.Text = wanban_end.Hour.ToString();
            textBox_wan_end_min.Text = wanban_end.Minute.ToString();
        }

        private void BanzuSet_Load(object sender, EventArgs e)
        {
            Set_Banzu_info();
        }

        private void SetTime_Changed(object sender,EventArgs e)
        {
            if (change_is == false)
            {
                change_is = true;
                textBox_zhong_start_hour.Text = textBox_zao_end_hour.Text;
                textBox_zhong_start_min.Text = textBox_zao_end_min.Text;
                textBox_wan_start_hour.Text = textBox_zhong_end_hour.Text;
                textBox_wan_start_min.Text = textBox_zhong_end_min.Text;
                textBox_wan_end_hour.Text = textBox_zao_start_hour.Text;
                textBox_wan_end_min.Text = textBox_zao_start_min.Text;
                change_is = false;
            }

        }

        private void Save_Banzu(string Banzu_Name,DateTime starttime,DateTime endtime)
        {
            try
            {
                DataSet ds = mv.mysqlconnection.sql_search_database("select * from banzu_time where banzu_name='" + Banzu_Name + "'");
                if (ds.Tables[0].Rows.Count == 0)
                {
                    // 说明没有这行的数据
                    mv.mysqlconnection.excute_sql("insert into banzu_time values ('" + Banzu_Name + "','" + starttime.ToString("HH:mm") + "','" + endtime.ToString("HH:mm") + "')");
                }
                else
                {
                    // 有这一行的数据
                    mv.mysqlconnection.excute_sql("update banzu_time set starttime='" + starttime.ToString("HH:mm") + "',endtime='"+ endtime.ToString("HH:mm")+"' where banzu_name='"+Banzu_Name+"'");
                }
                Set_Banzu_info();
                MessageBox.Show("修改成功！");
            }
            catch
            {
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 班组时间
            try
            {
                DateTime zao_start = DateTime.Parse(textBox_zao_start_hour.Text+":"+textBox_zao_start_min.Text);
                DateTime zao_end = DateTime.Parse(textBox_zao_end_hour.Text +":"+textBox_zao_end_min.Text);
                
                DateTime zhong_start=DateTime.Parse(textBox_zhong_start_hour.Text+":"+textBox_zhong_start_min.Text);
                DateTime zhong_end = DateTime.Parse(textBox_zhong_end_hour.Text+":"+textBox_zhong_end_min.Text);

                DateTime wan_start = DateTime.Parse(textBox_wan_start_hour.Text +":"+textBox_wan_start_min.Text);
                DateTime wan_end = DateTime.Parse(textBox_wan_end_hour.Text +":"+textBox_wan_end_min.Text);

                Save_Banzu("zaoban",zao_start,zao_end);
                Save_Banzu("zhongban",zhong_start,zhong_end);
                Save_Banzu("wanban", wan_start, wan_end);

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
