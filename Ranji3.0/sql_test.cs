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
    public partial class sql_test : Form
    {
        Rectangle rect = System.Windows.Forms.SystemInformation.VirtualScreen;
        public MainView mv;
        DataSet myds;
        public sql_test(MainView mymv)
        {
            mv = mymv;
            InitializeComponent();
        }

        private void sql_test_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0;
            this.Width = rect.Width;
            this.Height = rect.Height;
            for (int i = 0; i < 10; i++)
            {
                listBox1.Items.Add(i.ToString());
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                dataGridView1.RowCount = 1;
                dataGridView1.ColumnCount = 1;
                myds= mv.mysqlconnection.sql_search_database("Wendu_Lishi"+listBox1.SelectedItem.ToString());
                dataGridView1.RowCount = myds.Tables[0].Rows.Count;
                dataGridView1.ColumnCount = myds.Tables[0].Columns.Count;
           
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    {
                        DataRow dr = myds.Tables[0].Rows[i];
                        dataGridView1[j, i].Value = dr[j];
                    }
                }
            }
            catch
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mv.mysqlconnection.excute_sql("update Wendu_Lishi" + listBox1.SelectedItem.ToString() + " set [Machine_num]=" + "'" + dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString().Trim() + "'");
        }


    }
}
