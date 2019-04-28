using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.Common;

namespace Ranji3._0
{
    public partial class Craft_Edit_View : Form
    {
        public MainView mv;
        private int Craft_num;                             //此页面所展示的工艺号

        public Craft_Edit_View(MainView mymv)
        {
            mv = mymv;
            InitializeComponent();
        }

        private void Craft_Edit_View_Load(object sender, EventArgs e)
        {
            // 在treeview中加入子结点
           int node_index= treeView1.Nodes.IndexOfKey("工艺1-20");
           TreeNode tn = treeView1.Nodes[node_index];
           for (int i = 1; i <= 20; i++)
           {
               tn.Nodes.Add(i.ToString().PadLeft(2,'0')+"号工艺");
           }

           node_index = treeView1.Nodes.IndexOfKey("工艺21-40");
           tn = treeView1.Nodes[node_index];
           for (int i = 21; i <=40; i++)
           {
               tn.Nodes.Add(i.ToString().PadLeft(2,'0')+"号工艺");
           }

           node_index = treeView1.Nodes.IndexOfKey("工艺41-60");
           tn = treeView1.Nodes[node_index];
           for (int i = 41; i <= 60; i++)
           {
               tn.Nodes.Add(i.ToString().PadLeft(2,'0') + "号工艺");
           }

           node_index = treeView1.Nodes.IndexOfKey("工艺61-80");
           tn = treeView1.Nodes[node_index];
           for (int i = 61; i <= 80; i++)
           {
               tn.Nodes.Add(i.ToString().PadLeft(2,'0') + "号工艺");
           }

           node_index = treeView1.Nodes.IndexOfKey("工艺81-100");
           tn = treeView1.Nodes[node_index];
           for (int i = 81; i <=100; i++)
           {
               if (i != 100)
                   tn.Nodes.Add(i.ToString().PadLeft(2, '0') + "号工艺");
               else
                   tn.Nodes.Add(i.ToString()+"号工艺");
           }

           node_index = treeView1.Nodes.IndexOfKey("工艺101-120");
           tn = treeView1.Nodes[node_index];
           for (int i = 101; i <= 120; i++)
           {
               tn.Nodes.Add(i.ToString() + "号工艺");
           }

           node_index = treeView1.Nodes.IndexOfKey("工艺121-140");
           tn = treeView1.Nodes[node_index];
           for (int i = 121; i <= 140; i++)
           {
               tn.Nodes.Add(i.ToString() + "号工艺");
           }

           node_index = treeView1.Nodes.IndexOfKey("工艺141-160");
           tn = treeView1.Nodes[node_index];
           for (int i = 141; i <= 160; i++)
           {
               tn.Nodes.Add(i.ToString() + "号工艺");
           }

           node_index = treeView1.Nodes.IndexOfKey("工艺161-180");
           tn = treeView1.Nodes[node_index];
           for (int i = 161; i <= 180; i++)
           {
               tn.Nodes.Add(i.ToString() + "号工艺");
           }

           node_index = treeView1.Nodes.IndexOfKey("工艺181-200");
           tn = treeView1.Nodes[node_index];
           for (int i = 181; i <= 200; i++)
           {
               tn.Nodes.Add(i.ToString() + "号工艺");
           }

           node_index = treeView1.Nodes.IndexOfKey("工艺201-220");
           tn = treeView1.Nodes[node_index];
           for (int i = 201; i <= 220; i++)
           {
               tn.Nodes.Add(i.ToString() + "号工艺");
           }

           node_index = treeView1.Nodes.IndexOfKey("工艺221-240");
           tn = treeView1.Nodes[node_index];
           for (int i = 221; i <= 240; i++)
           {
               tn.Nodes.Add(i.ToString() + "号工艺");
           }
           try
           {
               node_index = treeView1.Nodes.IndexOfKey("工艺241-260");
               tn = treeView1.Nodes[node_index];
               for (int i = 241; i <= 260; i++)
               {
                   tn.Nodes.Add(i.ToString() + "号工艺");
               }
           }
           catch { }

           node_index = treeView1.Nodes.IndexOfKey("工艺261-280");
           tn = treeView1.Nodes[node_index];
           for (int i = 261; i <= 280; i++)
           {
               tn.Nodes.Add(i.ToString() + "号工艺");
           }

           node_index = treeView1.Nodes.IndexOfKey("工艺281-300");
           tn = treeView1.Nodes[node_index];
           for (int i = 281; i <= 300; i++)
           {
               tn.Nodes.Add(i.ToString() + "号工艺");
           }

           node_index = treeView1.Nodes.IndexOfKey("工艺301-320");
           tn = treeView1.Nodes[node_index];
           for (int i = 301; i <= 320; i++)
           {
               tn.Nodes.Add(i.ToString() + "号工艺");
           }

           node_index = treeView1.Nodes.IndexOfKey("工艺321-340");
           tn = treeView1.Nodes[node_index];
           for (int i = 321; i <= 340; i++)
           {
               tn.Nodes.Add(i.ToString() + "号工艺");
           }

           node_index = treeView1.Nodes.IndexOfKey("工艺341-360");
           tn = treeView1.Nodes[node_index];
           for (int i = 281; i <= 300; i++)
           {
               tn.Nodes.Add(i.ToString() + "号工艺");
           }


           node_index = treeView1.Nodes.IndexOfKey("工艺361-380");
           tn = treeView1.Nodes[node_index];
           for (int i = 301; i <= 320; i++)
           {
               tn.Nodes.Add(i.ToString() + "号工艺");
           }

           node_index = treeView1.Nodes.IndexOfKey("工艺381-400");
           tn = treeView1.Nodes[node_index];
           for (int i = 321; i <= 340; i++)
           {
               tn.Nodes.Add(i.ToString() + "号工艺");
           }

           node_index = treeView1.Nodes.IndexOfKey("工艺401-420");
           tn = treeView1.Nodes[node_index];
           for (int i = 341; i <= 360; i++)
           {
               tn.Nodes.Add(i.ToString() + "号工艺");
           }

          
        }
        //
        private void setchart(Chart mychart)
        {
            // 针对一个图表和对应的机号的处理
            try
            {
                mychart.ChartAreas[0].AxisX.LabelStyle.Format = "HH:mm:ss";
                //DataSet ds = mv.mysqlconnection.sql_search_database("select * from start_time where machine_num=" + machine_num.ToString());
                //DataRow dr = ds.Tables[0].Rows[0];
                DateTime starttime = DateTime.Parse("00:00:00");
                mychart.ChartAreas[0].AxisX.Minimum = starttime.ToOADate();
                mychart.ChartAreas[0].AxisX.Maximum = starttime.AddHours(5).ToOADate();
                mychart.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Hours;
                mychart.ChartAreas[0].AxisX.Interval = 1;
            }
            catch
            {
            }
        }
        public void draw_line()
        {
            setchart(chart1);
            draw_chart(chart1);
        }

        //画设定曲线
        private void draw_chart(Chart mychart)
        {
            //将对应的开始时间读出来
            
            // 画设定曲线
            try
            {
                int datagridview_row_count = dataGridView1.Rows.Count;
                //DataSet ds = mv.mysqlconnection.sql_search_database("Select * from start_time where machine_num=" + machine_num.ToString());
                //DataRow dr = ds.Tables[0].Rows[0];
                DateTime starttime = DateTime.Parse("00:00:00"); //开始时间
                mychart.Series[0].Points.Clear();
                for (int i = 0; i < datagridview_row_count; i++)
                {
                    if (dataGridView1[4, i].Value.ToString() == "温控")
                    {
                        int wendu = int.Parse(dataGridView1[1, i].Value.ToString());
                        mychart.Series[0].Points.AddXY(starttime, wendu);
                        int timespan = int.Parse(dataGridView1[3, i].Value.ToString());
                        starttime = starttime.AddMinutes(timespan);
                    }
                }
            }
            catch
            {

            }

        }

        // 刷新表
        public void ReFlash_Craft_Chart()
        {
            // 根据机缸对应的工艺表刷新datagridview
            dataGridView1.Rows.Clear();
            // 读取对应的工艺表
            DataSet ds = mv.mysqlconnection.sql_search_database("select * from craft" + Craft_num.ToString());
            int row_count = ds.Tables[0].Rows.Count;
            if (row_count == 0) return;    //如果行数是0，那么退出,不显示
            else
            {
                dataGridView1.RowCount = row_count;
            }
            for (int i = 0; i < row_count; i++)
            {
                // 将数据填充入表格中
                try
                {
                    DataRow dr = ds.Tables[0].Rows[i];
                    dataGridView1[0, i].Value = i + 1;
                    dataGridView1[1, i].Value = dr[1];
                    dataGridView1[2, i].Value = dr[2];
                    dataGridView1[3, i].Value = dr[3];
                    dataGridView1[4, i].Value = dr[4];
                    dataGridView1[5, i].Value = dr[6];
                    dataGridView1[6, i].Value = dr[7];
                    dataGridView1[7, i].Value = dr[8];
                }
                catch
                {
                }
            }
        }
        public void ReFill_Database_from_grid(int craft_num)
        {
            //将读进来的表送给对应机号的数据库中 首先删除对应的数据库
            try
            {
                mv.mysqlconnection.excute_sql("delete from craft" + craft_num.ToString());
            }
            catch
            {
            }
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                //一行一行往上加
                try
                {
                    string craft = dataGridView1[4, i].Value.ToString();
                    DataSet ds = mv.mysqlconnection.sql_search_database("select craft_code from craftcode where craft_name='" + craft + "'");
                    DataRow newdr = ds.Tables[0].Rows[0];
                    int code = int.Parse(newdr[0].ToString());  // 工艺代码
                    mv.mysqlconnection.excute_sql("insert into craft" + craft_num.ToString() + "(wendu_shuiwei,speed,time,craft,nvarcharcode,rate,fengji_rate,tibu_rate) values ('" + dataGridView1[1, i].Value.ToString() + "','" + dataGridView1[2, i].Value.ToString() + "','" + dataGridView1[3, i].Value.ToString() + "','" + dataGridView1[4, i].Value.ToString() + "','" + code.ToString() + "','" + dataGridView1[5, i].Value.ToString() + "','" + dataGridView1[6, i].Value.ToString() + "','" + dataGridView1[7, i].Value.ToString() + "')");
                }
                catch
                {
                }
            }
        }

       

        //
        public void updata_datagridview()
        {
            // 更新一下，对于每一行都更新
            try
            {
                for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                {
                    //首先计算时间
                    if (dataGridView1[4, i].Value.ToString() == "温控" && dataGridView1[4, i + 1].Value.ToString() == "温控")
                    {
                        float wendu = float.Parse(dataGridView1[1, i].Value.ToString());
                        float mubiao_wendu = float.Parse(dataGridView1[1, i + 1].Value.ToString());
                        float speed = float.Parse(dataGridView1[2, i].Value.ToString());
                        if (speed != 0 && (wendu != mubiao_wendu))
                        {
                            dataGridView1[3, i].Value = (int)(Math.Abs(wendu - mubiao_wendu) / speed);
                        }
                    }
                }
                for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                {
                    // updata 将表格里面的值写入数据库中，使数据库更新
                    DataSet ds = mv.mysqlconnection.sql_search_database("select * from craft" + Craft_num.ToString());
                    DataRow dr = ds.Tables[0].Rows[i];
                    int key = int.Parse(dr[0].ToString()); //要修改的这一行的关键字
                    int wendu = int.Parse(dataGridView1[1, i].Value.ToString()); // 温度
                    float speed = float.Parse(dataGridView1[2, i].Value.ToString()); // 速度
                    int time = int.Parse(dataGridView1[3, i].Value.ToString());  // 时间
                    string craft = dataGridView1[4, i].Value.ToString();         // 工艺
                    // code
                    ds = mv.mysqlconnection.sql_search_database("select craft_code from craftcode where craft_name='" + craft + "'");
                    DataRow newdr = ds.Tables[0].Rows[0];
                    int code = int.Parse(newdr[0].ToString());  // 工艺代码
                    int rate = int.Parse(dataGridView1[5, i].Value.ToString());  // 频率
                    int fengji_rate = int.Parse(dataGridView1[6, i].Value.ToString()); // 风机频率
                    int tibu_rate = int.Parse(dataGridView1[7, i].Value.ToString());   // 提布频率
                    mv.mysqlconnection.excute_sql("update craft" + Craft_num.ToString() + " set wendu_shuiwei='" + wendu.ToString() + "',speed='" + speed.ToString() + "',time='" + time.ToString() + "',craft='" + craft.ToString() + "',nvarcharcode='" + code.ToString() + "',rate='" + rate.ToString() + "',fengji_rate='" + fengji_rate.ToString() + "',tibu_rate='" + tibu_rate.ToString() + "' where xuhao='" + key.ToString() + "'");
                    //
                }
            }
            catch
            {

            }

        }
        // 
        private void Read_Craft_Name()
        {
            try
            {
                textBox_craft_name.Text = "";
                DataSet ds = mv.mysqlconnection.sql_search_database("Select * from craft_name where craft_num='" + Craft_num.ToString() + "'");
                DataRow dr = ds.Tables[0].Rows[0];
                textBox_craft_name.Text = dr[1].ToString();
            }
            catch
            {
            }
        }
        //
        private void Set_Craft_Name()
        {
            try
            {
                DataSet ds = mv.mysqlconnection.sql_search_database("Select * from craft_name where craft_num='" + Craft_num.ToString() + "'");
                DataRow dr = ds.Tables[0].Rows[0];
                mv.mysqlconnection.excute_sql("update craft_name set craft_name='"+textBox_craft_name.Text+"' where craft_num="+Craft_num.ToString());
            }
            catch
            {
                //说明没有工艺名，在这里加入
                try
                {
                    mv.mysqlconnection.excute_sql("insert into craft_name values('"+Craft_num.ToString()+"','"+textBox_craft_name.Text +"')");
                }
                catch
                {
                }
            }
        }
        //
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                // 读取所选取的工艺号
                TreeNode tn = e.Node;
                string key = tn.Text;
                label3.Text = key;
                int length = key.Length;
                Craft_num = int.Parse(key.Substring(0, length - 3));
                ReFlash_Craft_Chart();
                updata_datagridview();
                draw_line();
                Read_Craft_Name();
            }
            catch
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            edit_craft myfrm = new edit_craft(mv, Craft_num, 2);
            myfrm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ReFill_Database_from_grid(Craft_num);
            draw_line();
        }

        public void delete_row()
        {
            // 删除指定的一行
            try
            {
                int delete_row_index = dataGridView1.CurrentRow.Index;  //要删除的行
                DataSet ds = mv.mysqlconnection.sql_search_database("select xuhao from craft" + Craft_num.ToString());
                // int xuhao =int.Parse( ds.Tables[0].Rows[delete_row_index].ToString());
                DataRow dr = ds.Tables[0].Rows[delete_row_index];
                int xuhao = int.Parse(dr[0].ToString());
                mv.mysqlconnection.excute_sql("delete from craft" + Craft_num.ToString() + " where xuhao='" + xuhao.ToString() + "'");

            }
            catch
            {
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            delete_row();
            ReFlash_Craft_Chart(); //刷新
            updata_datagridview();
            draw_line();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Set_Craft_Name();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                edit_craft myfrm = new edit_craft(mv, Craft_num, 4);
                myfrm.start_index = dataGridView1.SelectedCells[0].RowIndex;

                myfrm.ShowDialog();
            }
            catch
            {
                MessageBox.Show("请选择插入位置");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                int craft_num = int.Parse(textBox_fuzhi_craft_num.Text);
                ReFill_Database_from_grid(craft_num);
            }
            catch
            {
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // 读取access数据库将工艺与工艺号插入到相应的数据库中
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Access文件|*.mdb";
            ofd.ValidateNames = true;
            ofd.CheckPathExists = true;
            ofd.CheckFileExists = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string strFileName = ofd.FileName;
                //strFileName = strFileName.Replace("\\","\\\\");
                MessageBox.Show(strFileName);
                string connect_text = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source="+strFileName;
                OleDbConnection conn;
                OleDbCommand da;
                string cmd = "select 名称,代码 from 工艺代码表";
                conn = new OleDbConnection(connect_text);
                OleDbDataAdapter odda = new OleDbDataAdapter(cmd, conn);
                DataSet ds = new DataSet("ds");
                odda.Fill(ds, "工艺代码表");

                // 将表插入到工艺代码表中
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];
                    string insertcmd = "insert into craftcode values('" + dr[0].ToString() + "','" + dr[1].ToString() + "')";
                    mv.mysqlconnection.excute_sql(insertcmd);
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Access文件|*.mdb";
            ofd.ValidateNames = true;
            ofd.CheckPathExists = true;
            ofd.CheckFileExists = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string strFileName = ofd.FileName;
                //strFileName = strFileName.Replace("\\","\\\\");
                //MessageBox.Show(strFileName);
                string connect_text = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strFileName;
                OleDbConnection conn;
                OleDbCommand da;
                string cmd = "select * from 控制参数表";
                conn = new OleDbConnection(connect_text);
                OleDbDataAdapter odda = new OleDbDataAdapter(cmd, conn);
                DataSet ds = new DataSet("ds");
                odda.Fill(ds, "控制参数表");


                // 清空相应的表
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (Craft_num >= 1)
                    {
                        mv.mysqlconnection.excute_sql("delete from craft"+Craft_num.ToString());
                    }
                }
                // 将表插入到工艺代码表中
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];
                    string insertcmd = "insert into craft" + Craft_num.ToString() + " (wendu_shuiwei,speed,time,craft,nvarcharcode,rate,fengji_rate,tibu_rate) values('" + dr[3].ToString() + "','" + dr[5].ToString() + "','" + dr[6].ToString() + "','" + dr[8].ToString() + "','" + dr[7].ToString() + "','" + dr[5].ToString() + "','" + dr[10].ToString() + "','" + dr[11].ToString() + "')";
                    mv.mysqlconnection.excute_sql(insertcmd);
                }
            }
        }
    }
}
