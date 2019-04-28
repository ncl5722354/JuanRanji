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
    public partial class Data_add : Form
    {
        private data_config mydata_view;
        private IniFile myini;
        public int Machine_num; //所要处理的
        public Data_add(data_config parent_view)
        {
            mydata_view = parent_view;
            InitializeComponent();
            myini = mydata_view.mv.myini;
        }

        private void Data_add_Load(object sender, EventArgs e)
        {
            comboBox_type.Items.Add("DT");
            comboBox_type.Items.Add("R");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox_dataname.Text == "")
            {
                MessageBox.Show("数据名称不能为空!");
                return;
            }
            if (comboBox_type.Text == "")
            {
                MessageBox.Show("必须选择一个数据类型");
                return;
            }
            if (textBox_data_address.Text == "")
            {
                MessageBox.Show("数据地址不能为空!");
                return;
            }
            
            DataSet myds = mydata_view.mv.mysqlconnection.sql_search_database("select * from data_table");
            try
            {
                //  首先检查是否是
                int myds_rows = myds.Tables[0].Rows.Count;
                for (int i = 0; i < myds_rows; i++)
                {
                    DataRow mydr = myds.Tables[0].Rows[i];
                    if (mydr[0].ToString().Trim() == Machine_num.ToString()+"_"+ textBox_dataname.Text)
                    {
                        MessageBox.Show("变量名重复！");
                        return;
                    }
                    //if (mydr[1].ToString().Trim() == comboBox_type.Text && mydr[2].ToString().Trim() == textBox_data_address.Text)
                    //{
                    //    MessageBox.Show("变量地址重复！");
                    //    return;
                    //}
                }
                mydata_view.mv.mysqlconnection.excute_sql("Insert into data_table values ("+"'"+Machine_num.ToString()+"_"+textBox_dataname.Text+"','"+comboBox_type.Text+"','"+textBox_data_address.Text+"','"+Machine_num.ToString()+"')");
                mydata_view.get_data_config(Machine_num);
                this.Hide();
            }
            catch
            {
                this.Hide();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
