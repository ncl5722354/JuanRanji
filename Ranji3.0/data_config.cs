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
    public partial class data_config : Form
    {
        private Data_add add_view;
        public MainView mv;
        public data_config(MainView mymv)
        {
            mv = mymv;
            InitializeComponent();
        }

        private void data_config_Load(object sender, EventArgs e)
        {
            add_view = new Data_add(this);
            for (int i = 1; i <= 60; i++)
            {
                treeView1.Nodes.Add(i.ToString()+"号染机");
            }
            get_data_config(1);
        }

        

        public void get_data_config(int machine_num)
        {
            string cmd = "Select * from data_table where machine_num='" + machine_num.ToString()+"'";
            DataSet ds= mv.mysqlconnection.sql_search_database(cmd);
            dataset_fill_datagridview(ds);
        }

        public void dataset_fill_datagridview(DataSet ds)
        {
            if (ds != null)
            {
                //用数据库填充数据表
                try
                {
                    dataGridView1.RowCount = ds.Tables[0].Rows.Count;
                    int ds_row = ds.Tables[0].Rows.Count;
                    for (int i = 0; i < ds_row; i++)
                    {
                        DataRow dr = ds.Tables[0].Rows[i];
                        string itemname = dr[0].ToString().Trim();
                        for (int j = 0; j < itemname.Length; j++)
                        {
                            if (itemname[j] == '_')
                            {
                                itemname = itemname.Substring(j+1,itemname.Length-j-1);
                                break;
                            }
                        }
                        dataGridView1[0, i].Value = itemname;
                        dataGridView1[1, i].Value = dr[1].ToString().Trim();
                        dataGridView1[2, i].Value = dr[2].ToString().Trim();
                    }
                }
                catch
                {
                    dataGridView1.RowCount = 1;
                    dataGridView1.Rows.Clear();
                }
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

     

        private void button1_Click(object sender, EventArgs e)
        {
            int machine_num = treeView1.SelectedNode.Index+1;
            add_view.Machine_num = machine_num;
            add_view.Show();
            add_view.Focus();
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeView mytv = (TreeView)sender;
            int select_machine_num = mytv.SelectedNode.Index + 1;
            get_data_config(select_machine_num);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                //对于每一行就行更新
                if (dataGridView1[0, i].Value.ToString() != "")
                {
                    int machine_num=treeView1.SelectedNode.Index + 1;
                   
                    string key = machine_num.ToString() + "_" + dataGridView1[0, i].Value.ToString();
                    string data_type="";
                    try
                    {
                        data_type = dataGridView1[1, i].Value.ToString();
                    }
                    catch
                    {
                        MessageBox.Show("数据类型不能为空！"); return;
                    }
                    string data_address;
                    try
                    {
                        data_address = dataGridView1[2, i].Value.ToString();
                    }
                    catch
                    {
                        MessageBox.Show("数据地址不能为空！"); return;
                    }
                    
                    mv.mysqlconnection.excute_sql("update data_table set data_name='"+key.ToString()+"',data_type='"+data_type.ToString()+"',data_address='"+data_address.ToString()+"',machine_num='"+machine_num.ToString()+"' "+"where data_name="+"'"+key.ToString()+"'");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // 删除一行数据
            int select_row_index= dataGridView1.CurrentCell.RowIndex;
            string key = (treeView1.SelectedNode.Index+1).ToString()+"_"+dataGridView1[0,select_row_index].Value.ToString();
            mv.mysqlconnection.excute_sql("delete from data_table where data_name='"+key.ToString()+"'");
            get_data_config(treeView1.SelectedNode.Index+1);
        }
    }
}
