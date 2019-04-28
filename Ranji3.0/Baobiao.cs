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
    public partial class Baobiao : Form
    {
        public MainView mv;
        public Baobiao(MainView mymv)
        {
            InitializeComponent();
            mv = mymv;
            for (int i = 1; i <= 60; i++)
            {
                comboBox_machine_num.Items.Add(i.ToString());
            }
            comboBox_banzu.Items.Add("早班");
            comboBox_banzu.Items.Add("中班");
            comboBox_banzu.Items.Add("晚班");
        }

        private void Baobiao_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker_end_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string banzu_string = "";
                dataGridView1.RowCount = 1;
                dataGridView1.ColumnCount = 4;
                dataGridView1[0, 0].Value = "";
                dataGridView1[1, 0].Value = "";
                dataGridView1[2, 0].Value = "";
                dataGridView1[3, 0].Value = "";
                if (radioButton_simple_banzu.Checked == true)
                {
                    if (comboBox_banzu.Text == "早班") banzu_string = " and banzu='zaoban'";
                    if (comboBox_banzu.Text == "中班") banzu_string = " and banzu='zhongban'";
                    if (comboBox_banzu.Text == "晚班") banzu_string = " and banzu='wanban'";
                }

                string jigang_string = "";
                if (radioButton_simaple_jigang.Checked == true)
                {
                    if (comboBox_machine_num.Text != "")
                    {
                        jigang_string = " and machine_num='" + comboBox_machine_num.Text + "'";
                    }
                }
                string cmd_string = "select * from liuliang_history where time>='" + dateTimePicker_start.Value.ToString("yyyy-MM-dd HH:mm:ss") +
                    "' and time<='" + dateTimePicker_end.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'" + banzu_string + jigang_string;

                DataSet ds = mv.mysqlconnection.sql_search_database(cmd_string);
                dataGridView1.RowCount = ds.Tables[0].Rows.Count;
                dataGridView1.ColumnCount = 4;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    dataGridView1[0, i].Value = ds.Tables[0].Rows[i][1];
                    dataGridView1[1, i].Value = ds.Tables[0].Rows[i][2];
                    if (ds.Tables[0].Rows[i][3].ToString() == "zaoban") dataGridView1[2, i].Value = "早班";
                    if (ds.Tables[0].Rows[i][3].ToString() == "zhongban") dataGridView1[2, i].Value = "中班";
                    if (ds.Tables[0].Rows[i][3].ToString() == "wanban") dataGridView1[2, i].Value = "晚班";
                    dataGridView1[3, i].Value = ds.Tables[0].Rows[i][4];
                }
            }
            catch
            {
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
