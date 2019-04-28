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
    public partial class edit_craft : Form
    {
        public MainView mv;
        public int operation_kind = 1;     //operation_kind=1 为机缸添加操作   //operation_kind=2 为工艺添加操作  // operation_kind=3 为机缸插入操作  // operation_kind=4 为工艺添加操作
        public int operation_machine_num = 1; //将要操作的机号 默认为1
        public int start_index = 0;           // 插入时要用到的起始量

        public edit_craft(MainView mymv,int machine_num,int kind)
        {
            operation_machine_num = machine_num;
            operation_kind = kind;
            mv = mymv;
            InitializeComponent();
        }

        private void edit_craft_Load(object sender, EventArgs e)
        {
            //comboBox_wenkong_select.Items.Add("升温/降温");
            //comboBox_wenkong_select.Items.Add("保温");
            Set_Craft_Combo();
            //comboBox_shuiwei.Items.Add("进水一");
            //comboBox_shuiwei.Items.Add("进水二");
            //comboBox_shuiwei.Items.Add("进水三");
            //comboBox_shuiwei.Items.Add("进水四");
            //comboBox_shuiwei.Items.Add("排水一");
            //comboBox_shuiwei.Items.Add("排水二");
            //comboBox_shuiwei.Items.Add("排水三");
            //comboBox_shuiwei.Items.Add("排水四");
            //comboBox_qita.Items.Add("其他工艺一");
            //comboBox_qita.Items.Add("其他工艺二");
            //comboBox_qita.Items.Add("其他工艺三");
            //comboBox_qita.Items.Add("其他工艺四");
            textBox_qishiwendu.Text = "0";
            textBox_sulv.Text = "0";
            textBox_time.Text = "0";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void comboBox_wenkong_select_TextChanged(object sender, EventArgs e)
        {
        //{
        //    if (comboBox_wenkong_select.Text == "升温/降温")
        //    {
        //        textBox_time.Enabled = false;
        //        textBox_sulv.Enabled = true;
        //       // textBox_mubiaowendu.Enabled = true;
        //        textBox_qishiwendu.Enabled = true;
        //    }
        //    else if (comboBox_wenkong_select.Text == "保温")
        //    {
        //        textBox_sulv.Enabled = false;
        //       // textBox_mubiaowendu.Enabled = false;
        //        textBox_qishiwendu.Enabled = true;
        //        textBox_time.Enabled = true;
        //    }
        //    else
        //    {
        //        //textBox_mubiaowendu.Enabled = false;
        //        textBox_sulv.Enabled = false;
        //        textBox_time.Enabled = false;
        //        textBox_qishiwendu.Enabled = false;
        //    }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
           // comboBox_wenkong_select.Enabled = true;
        }

        private void Set_Craft_Combo()
        {
            // 更新工艺列表
            try
            {
               DataSet ds = mv.mysqlconnection.sql_search_database("Select * from craftcode");
               comboBox_craftlist.Items.Clear();    // 清楚工艺列表框
               for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
               {
                   comboBox_craftlist.Items.Add (ds.Tables[0].Rows[i][0].ToString());
               }

                //加料工艺
               DataSet ds_jialiao = mv.mysqlconnection.sql_search_database("Select * from craftcode where craft_kind='加料'");
               comboBox_jialiao.Items.Clear();
               for (int i = 0; i < ds_jialiao.Tables[0].Rows.Count; i++)
               {
                   comboBox_jialiao.Items.Add(ds_jialiao.Tables[0].Rows[i][0].ToString());
                  // string a = ds.Tables[0].Rows[i][0].ToString();
               }

               //手动加料工艺
               DataSet ds_shoudongjialiao = mv.mysqlconnection.sql_search_database("Select * from craftcode where craft_kind='手动加料'");
               comboBox_shoudongjialiao.Items.Clear();
               for (int i = 0; i < ds_shoudongjialiao.Tables[0].Rows.Count; i++)
               {
                   comboBox_shoudongjialiao.Items.Add(ds_shoudongjialiao.Tables[0].Rows[i][0].ToString());
                  // string a = ds.Tables[0].Rows[i][0].ToString();
               }

               //手动加料工艺
               DataSet ds_liucheng = mv.mysqlconnection.sql_search_database("Select * from craftcode where craft_kind='流程'");
               comboBox_liucheng.Items.Clear();
               for (int i = 0; i < ds_liucheng.Tables[0].Rows.Count; i++)
               {
                   comboBox_liucheng.Items.Add(ds_liucheng.Tables[0].Rows[i][0].ToString());
                   // string a = ds.Tables[0].Rows[i][0].ToString();
               }

               //料缸
               DataSet ds_liaogang = mv.mysqlconnection.sql_search_database("Select * from craftcode where craft_kind='料缸'");
               comboBox_liaogang.Items.Clear();
               for (int i = 0; i < ds_liaogang.Tables[0].Rows.Count; i++)
               {
                   comboBox_liaogang.Items.Add(ds_liaogang.Tables[0].Rows[i][0].ToString());
                   // string a = ds.Tables[0].Rows[i][0].ToString();
               }

               //进水
               DataSet ds_jinshui = mv.mysqlconnection.sql_search_database("Select * from craftcode where craft_kind='进水'");
               comboBox_jinshui.Items.Clear();
               for (int i = 0; i < ds_jinshui.Tables[0].Rows.Count; i++)
               {
                   comboBox_jinshui.Items.Add(ds_jinshui.Tables[0].Rows[i][0].ToString());
                   // string a = ds.Tables[0].Rows[i][0].ToString();
               }

               //排水
               DataSet ds_paishui = mv.mysqlconnection.sql_search_database("Select * from craftcode where craft_kind='排水'");
               comboBox_paishui.Items.Clear();
               for (int i = 0; i < ds_paishui.Tables[0].Rows.Count; i++)
               {
                   comboBox_paishui.Items.Add(ds_paishui.Tables[0].Rows[i][0].ToString());
                   // string a = ds.Tables[0].Rows[i][0].ToString();
               }

               //温控
               DataSet ds_wenkong = mv.mysqlconnection.sql_search_database("Select * from craftcode where craft_kind='温控'");
               comboBox_wenkong.Items.Clear();
               for (int i = 0; i < ds_wenkong.Tables[0].Rows.Count; i++)
               {
                   comboBox_wenkong.Items.Add(ds_wenkong.Tables[0].Rows[i][0].ToString());
                   // string a = ds.Tables[0].Rows[i][0].ToString();
               }

                //水洗
               DataSet ds_shuixi = mv.mysqlconnection.sql_search_database("Select * from craftcode where craft_kind='水洗'");
               comboBox_shuixi.Items.Clear();
               for (int i = 0; i < ds_shuixi.Tables[0].Rows.Count; i++)
               {
                   comboBox_shuixi.Items.Add(ds_shuixi.Tables[0].Rows[i][0].ToString());
                   // string a = ds.Tables[0].Rows[i][0].ToString();
               }
            }
            catch
            {
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (radioButton_shuixi.Checked == true)
            {
                // 水洗
                if (operation_kind == 1)
                {
                    try
                    {
                        DataSet ds = mv.mysqlconnection.sql_search_database("select * from craft_machine" + operation_machine_num.ToString());
                        int row_count = ds.Tables[0].Rows.Count;
                        DataRow craftrow = null;     //最后一行
                        if (row_count > 0) craftrow = ds.Tables[0].Rows[row_count - 1];
                        ds = mv.mysqlconnection.sql_search_database("select craft_code from craftcode where craft_name='" + comboBox_shuixi.Text + "'");
                        DataRow dr = ds.Tables[0].Rows[0];
                        int code = int.Parse(dr[0].ToString());
                        int xuhao = row_count + 1;
                        int shuiwei = int.Parse(textBox_qishiwendu.Text);
                        string craft = comboBox_shuixi.Text;
                        float speed = 0;
                        int time = 0;
                        if (textBox_time.Text != "")
                        {
                            time = int.Parse(textBox_time.Text);
                        }
                        int rate = 0;
                        if (textBox1_rate.Text != "")
                        {
                            rate = int.Parse(textBox1_rate.Text);
                        }
                        int fengji_rate = 0;
                        if (textBox_fengji_rate.Text != "")
                        {
                            fengji_rate = int.Parse(textBox_fengji_rate.Text);
                        }
                        int tibu_rate = 0;
                        if (textBox_tibu_rate.Text != "")
                        {
                            tibu_rate = int.Parse(textBox_tibu_rate.Text);
                        }
                        mv.mysqlconnection.excute_sql("insert into craft_machine" + operation_machine_num.ToString() + "(wendu_shuiwei,speed,time,craft,nvarcharcode,rate,fengji_rate,tibu_rate) values ('" + shuiwei.ToString() + "','" + speed.ToString() + "','" + time.ToString() + "','" + craft + "','" + code.ToString() + "','" + rate.ToString() + "','" + fengji_rate.ToString() + "','" + tibu_rate.ToString() + "')");
                        mv.detail.ReFlash_Craft_Chart();
                        mv.detail.updata_datagridview();
                        mv.detail.draw_line();
                        this.Dispose();
                    }
                    catch
                    {
                    }
                }
                if (operation_kind == 2)
                {
                    try
                    {
                        DataSet ds = mv.mysqlconnection.sql_search_database("select * from craft" + operation_machine_num.ToString());
                        int row_count = ds.Tables[0].Rows.Count;
                        DataRow craftrow = null;     //最后一行
                        if (row_count > 0) craftrow = ds.Tables[0].Rows[row_count - 1];
                        ds = mv.mysqlconnection.sql_search_database("select craft_code from craftcode where craft_name='" + comboBox_shuixi.Text + "'");
                        DataRow dr = ds.Tables[0].Rows[0];
                        int code = int.Parse(dr[0].ToString());
                        int xuhao = row_count + 1;
                        int shuiwei = int.Parse(textBox_qishiwendu.Text);
                        string craft = comboBox_shuixi.Text;
                        float speed = 0;
                        int time = 0;
                        if (textBox_time.Text != "")
                        {
                            time = int.Parse(textBox_time.Text);
                        }
                        int rate = 0;
                        if (textBox1_rate.Text != "")
                        {
                            rate = int.Parse(textBox1_rate.Text);
                        }
                        int fengji_rate = 0;
                        if (textBox_fengji_rate.Text != "")
                        {
                            fengji_rate = int.Parse(textBox_fengji_rate.Text);
                        }
                        int tibu_rate = 0;
                        if (textBox_tibu_rate.Text != "")
                        {
                            tibu_rate = int.Parse(textBox_tibu_rate.Text);
                        }
                        mv.mysqlconnection.excute_sql("insert into craft" + operation_machine_num.ToString() + "(wendu_shuiwei,speed,time,craft,nvarcharcode,rate,fengji_rate,tibu_rate) values ('" + shuiwei.ToString() + "','" + speed.ToString() + "','" + time.ToString() + "','" + craft + "','" + code.ToString() + "','" + rate.ToString() + "','" + fengji_rate.ToString() + "','" + tibu_rate.ToString() + "')");
                        mv.craft_edit_view.ReFlash_Craft_Chart();
                        mv.craft_edit_view.updata_datagridview();
                        mv.craft_edit_view.draw_line();
                        this.Dispose();
                    }
                    catch
                    {
                    }
                }
                if (operation_kind == 3)
                {
                    // 新建一行，并依次往后面排
                    try
                    {
                        
                        // 获取工艺号
                        //DataSet ds = mv.mysqlconnection.sql_search_database("select craft_code from craftcode where craft_name='"+comboBox_shuixi.Text +"'");
                        //DataRow dr = ds.Tables[0].Rows[0];
                        //int code = int.Parse(dr[0].ToString());

                        int shuiwei = int.Parse(textBox_qishiwendu.Text);
                        string craft = comboBox_shuixi.Text;
                        if(craft=="")return;
                        float speed = 0;
                        if (textBox_sulv.Text != "")
                        {
                            speed = float.Parse(textBox_sulv.Text);
                        }
                        int time = 0;
                        if (textBox_time.Text != "")
                        {
                            time = int.Parse(textBox_time.Text);
                        }
                        int rate = 0;
                        if (textBox1_rate.Text != "")
                        {
                            rate = int.Parse(textBox1_rate.Text);
                        }
                        int fengji_rate = 0;
                        if (textBox_fengji_rate.Text != "")
                        {
                            fengji_rate = int.Parse(textBox_fengji_rate.Text);
                        }
                        int tibu_rate = 0;
                        if (textBox_tibu_rate.Text != "")
                        {
                            tibu_rate = int.Parse(textBox_tibu_rate.Text);
                        }
                        mv.detail.dataGridView1.RowCount = mv.detail.dataGridView1.RowCount + 1;  //新增一行
                        for (int i = 0; i < mv.detail.dataGridView1.RowCount; i++)
                        {
                            mv.detail.dataGridView1[0, i].Value = i + 1;
                        }
                            for (int i = mv.detail.dataGridView1.RowCount - 2; i >= start_index; i--)
                            {
                                for (int j = 1; j < mv.detail.dataGridView1.ColumnCount; j++)
                                {
                                    mv.detail.dataGridView1[j, i + 1].Value = mv.detail.dataGridView1[j, i].Value;
                                }
                            }

                            mv.detail.dataGridView1[1, start_index].Value = shuiwei;
                            mv.detail.dataGridView1[2, start_index].Value = speed;
                            mv.detail.dataGridView1[3, start_index].Value = time;
                            mv.detail.dataGridView1[4, start_index].Value = craft;
                            mv.detail.dataGridView1[5, start_index].Value = rate;
                            mv.detail.dataGridView1[6, start_index].Value = fengji_rate;
                            mv.detail.dataGridView1[7, start_index].Value = tibu_rate;
                            mv.detail.draw_line();
                            this.Dispose();
                    }
                    catch
                    {
                    }
                }
                if (operation_kind == 4)
                {
                    // 新建一行，并依次往后面排
                    try
                    {

                        // 获取工艺号
                        //DataSet ds = mv.mysqlconnection.sql_search_database("select craft_code from craftcode where craft_name='"+comboBox_shuixi.Text +"'");
                        //DataRow dr = ds.Tables[0].Rows[0];
                        //int code = int.Parse(dr[0].ToString());

                        int shuiwei = int.Parse(textBox_qishiwendu.Text);
                        string craft = comboBox_shuixi.Text;
                        if (craft == "") return;
                        float speed = 0;
                        if (textBox_sulv.Text != "")
                        {
                            speed = float.Parse(textBox_sulv.Text);
                        }
                        int time = 0;
                        if (textBox_time.Text != "")
                        {
                            time = int.Parse(textBox_time.Text);
                        }
                        int rate = 0;
                        if (textBox1_rate.Text != "")
                        {
                            rate = int.Parse(textBox1_rate.Text);
                        }
                        int fengji_rate = 0;
                        if (textBox_fengji_rate.Text != "")
                        {
                            fengji_rate = int.Parse(textBox_fengji_rate.Text);
                        }
                        int tibu_rate = 0;
                        if (textBox_tibu_rate.Text != "")
                        {
                            tibu_rate = int.Parse(textBox_tibu_rate.Text);
                        }
                        mv.craft_edit_view.dataGridView1.RowCount = mv.craft_edit_view.dataGridView1.RowCount + 1;  //新增一行
                        for (int i = 0; i < mv.craft_edit_view.dataGridView1.RowCount; i++)
                        {
                            mv.craft_edit_view.dataGridView1[0, i].Value = i + 1;
                        }
                        for (int i = mv.craft_edit_view.dataGridView1.RowCount - 2; i >= start_index; i--)
                        {
                            for (int j = 1; j < mv.craft_edit_view.dataGridView1.ColumnCount; j++)
                            {
                                mv.craft_edit_view.dataGridView1[j, i + 1].Value = mv.craft_edit_view.dataGridView1[j, i].Value;
                            }
                        }

                        mv.craft_edit_view.dataGridView1[1, start_index].Value = shuiwei;
                        mv.craft_edit_view.dataGridView1[2, start_index].Value = speed;
                        mv.craft_edit_view.dataGridView1[3, start_index].Value = time;
                        mv.craft_edit_view.dataGridView1[4, start_index].Value = craft;
                        mv.craft_edit_view.dataGridView1[5, start_index].Value = rate;
                        mv.craft_edit_view.dataGridView1[6, start_index].Value = fengji_rate;
                        mv.craft_edit_view.dataGridView1[7, start_index].Value = tibu_rate;
                        mv.craft_edit_view.draw_line();
                        this.Dispose();
                    }
                    catch
                    {
                    }
                }
            }
            if (radioButton_wenkong.Checked == true)
            {
                // 排水
                if (operation_kind == 1)
                {
                    try
                    {
                        DataSet ds = mv.mysqlconnection.sql_search_database("select * from craft_machine" + operation_machine_num.ToString());
                        int row_count = ds.Tables[0].Rows.Count;
                        DataRow craftrow = null;     //最后一行
                        if (row_count > 0) craftrow = ds.Tables[0].Rows[row_count - 1];
                        ds = mv.mysqlconnection.sql_search_database("select craft_code from craftcode where craft_name='" + comboBox_wenkong.Text + "'");
                        DataRow dr = ds.Tables[0].Rows[0];
                        int code = int.Parse(dr[0].ToString());
                        int xuhao = row_count + 1;
                        int shuiwei = int.Parse(textBox_qishiwendu.Text);
                        string craft = comboBox_wenkong.Text;
                        float speed = 0;
                        int time = 0;
                        if (textBox_time.Text != "")
                        {
                            time = int.Parse(textBox_time.Text);
                        }
                        int rate = 0;
                        if (textBox1_rate.Text != "")
                        {
                            rate = int.Parse(textBox1_rate.Text);
                        }
                        int fengji_rate = 0;
                        if (textBox_fengji_rate.Text != "")
                        {
                            fengji_rate = int.Parse(textBox_fengji_rate.Text);
                        }
                        int tibu_rate = 0;
                        if (textBox_tibu_rate.Text != "")
                        {
                            tibu_rate = int.Parse(textBox_tibu_rate.Text);
                        }
                        mv.mysqlconnection.excute_sql("insert into craft_machine" + operation_machine_num.ToString() + "(wendu_shuiwei,speed,time,craft,nvarcharcode,rate,fengji_rate,tibu_rate) values ('" + shuiwei.ToString() + "','" + speed.ToString() + "','" + time.ToString() + "','" + craft + "','" + code.ToString() + "','" + rate.ToString() + "','" + fengji_rate.ToString() + "','" + tibu_rate.ToString() + "')");
                        mv.detail.ReFlash_Craft_Chart();
                        mv.detail.updata_datagridview();
                        mv.detail.draw_line();
                        this.Dispose();
                    }
                    catch
                    {
                    }
                }
                if (operation_kind == 2)
                {
                    try
                    {
                        DataSet ds = mv.mysqlconnection.sql_search_database("select * from craft" + operation_machine_num.ToString());
                        int row_count = ds.Tables[0].Rows.Count;
                        DataRow craftrow = null;     //最后一行
                        if (row_count > 0) craftrow = ds.Tables[0].Rows[row_count - 1];
                        ds = mv.mysqlconnection.sql_search_database("select craft_code from craftcode where craft_name='" + comboBox_wenkong.Text + "'");
                        DataRow dr = ds.Tables[0].Rows[0];
                        int code = int.Parse(dr[0].ToString());
                        int xuhao = row_count + 1;
                        int shuiwei = int.Parse(textBox_qishiwendu.Text);
                        string craft = comboBox_wenkong.Text;
                        float speed = 0;
                        int time = 0;
                        if (textBox_time.Text != "")
                        {
                            time = int.Parse(textBox_time.Text);
                        }
                        int rate = 0;
                        if (textBox1_rate.Text != "")
                        {
                            rate = int.Parse(textBox1_rate.Text);
                        }
                        int fengji_rate = 0;
                        if (textBox_fengji_rate.Text != "")
                        {
                            fengji_rate = int.Parse(textBox_fengji_rate.Text);
                        }
                        int tibu_rate = 0;
                        if (textBox_tibu_rate.Text != "")
                        {
                            tibu_rate = int.Parse(textBox_tibu_rate.Text);
                        }
                        mv.mysqlconnection.excute_sql("insert into craft" + operation_machine_num.ToString() + "(wendu_shuiwei,speed,time,craft,nvarcharcode,rate,fengji_rate,tibu_rate) values ('" + shuiwei.ToString() + "','" + speed.ToString() + "','" + time.ToString() + "','" + craft + "','" + code.ToString() + "','" + rate.ToString() + "','" + fengji_rate.ToString() + "','" + tibu_rate.ToString() + "')");
                        mv.craft_edit_view.ReFlash_Craft_Chart();
                        mv.craft_edit_view.updata_datagridview();
                        mv.craft_edit_view.draw_line();
                        this.Dispose();
                    }
                    catch
                    {
                    }
                }
                if (operation_kind == 3)
                {
                    // 新建一行，并依次往后面排
                    try
                    {

                        // 获取工艺号
                        //DataSet ds = mv.mysqlconnection.sql_search_database("select craft_code from craftcode where craft_name='"+comboBox_shuixi.Text +"'");
                        //DataRow dr = ds.Tables[0].Rows[0];
                        //int code = int.Parse(dr[0].ToString());

                        int shuiwei = int.Parse(textBox_qishiwendu.Text);
                        string craft = comboBox_wenkong.Text;
                        if (craft == "") return;
                        float speed = 0;
                        if (textBox_sulv.Text != "")
                        {
                            speed = float.Parse(textBox_sulv.Text);
                        }
                        int time = 0;
                        if (textBox_time.Text != "")
                        {
                            time = int.Parse(textBox_time.Text);
                        }
                        int rate = 0;
                        if (textBox1_rate.Text != "")
                        {
                            rate = int.Parse(textBox1_rate.Text);
                        }
                        int fengji_rate = 0;
                        if (textBox_fengji_rate.Text != "")
                        {
                            fengji_rate = int.Parse(textBox_fengji_rate.Text);
                        }
                        int tibu_rate = 0;
                        if (textBox_tibu_rate.Text != "")
                        {
                            tibu_rate = int.Parse(textBox_tibu_rate.Text);
                        }
                        mv.detail.dataGridView1.RowCount = mv.detail.dataGridView1.RowCount + 1;  //新增一行
                        for (int i = 0; i < mv.detail.dataGridView1.RowCount; i++)
                        {
                            mv.detail.dataGridView1[0, i].Value = i + 1;
                        }
                        for (int i = mv.detail.dataGridView1.RowCount - 2; i >= start_index; i--)
                        {
                            for (int j = 1; j < mv.detail.dataGridView1.ColumnCount; j++)
                            {
                                mv.detail.dataGridView1[j, i + 1].Value = mv.detail.dataGridView1[j, i].Value;
                            }
                        }

                        mv.detail.dataGridView1[1, start_index].Value = shuiwei;
                        mv.detail.dataGridView1[2, start_index].Value = speed;
                        mv.detail.dataGridView1[3, start_index].Value = time;
                        mv.detail.dataGridView1[4, start_index].Value = craft;
                        mv.detail.dataGridView1[5, start_index].Value = rate;
                        mv.detail.dataGridView1[6, start_index].Value = fengji_rate;
                        mv.detail.dataGridView1[7, start_index].Value = tibu_rate;
                        mv.detail.draw_line();
                        this.Dispose();
                    }
                    catch
                    {
                    }
                }
                if (operation_kind == 4)
                {
                    // 新建一行，并依次往后面排
                    try
                    {

                        // 获取工艺号
                        //DataSet ds = mv.mysqlconnection.sql_search_database("select craft_code from craftcode where craft_name='"+comboBox_shuixi.Text +"'");
                        //DataRow dr = ds.Tables[0].Rows[0];
                        //int code = int.Parse(dr[0].ToString());

                        int shuiwei = int.Parse(textBox_qishiwendu.Text);
                        string craft = comboBox_wenkong.Text;
                        if (craft == "") return;
                        float speed = 0;
                        if (textBox_sulv.Text != "")
                        {
                            speed = float.Parse(textBox_sulv.Text);
                        }
                        int time = 0;
                        if (textBox_time.Text != "")
                        {
                            time = int.Parse(textBox_time.Text);
                        }
                        int rate = 0;
                        if (textBox1_rate.Text != "")
                        {
                            rate = int.Parse(textBox1_rate.Text);
                        }
                        int fengji_rate = 0;
                        if (textBox_fengji_rate.Text != "")
                        {
                            fengji_rate = int.Parse(textBox_fengji_rate.Text);
                        }
                        int tibu_rate = 0;
                        if (textBox_tibu_rate.Text != "")
                        {
                            tibu_rate = int.Parse(textBox_tibu_rate.Text);
                        }
                        mv.craft_edit_view.dataGridView1.RowCount = mv.craft_edit_view.dataGridView1.RowCount + 1;  //新增一行
                        for (int i = 0; i < mv.craft_edit_view.dataGridView1.RowCount; i++)
                        {
                            mv.craft_edit_view.dataGridView1[0, i].Value = i + 1;
                        }
                        for (int i = mv.craft_edit_view.dataGridView1.RowCount - 2; i >= start_index; i--)
                        {
                            for (int j = 1; j < mv.craft_edit_view.dataGridView1.ColumnCount; j++)
                            {
                                mv.craft_edit_view.dataGridView1[j, i + 1].Value = mv.craft_edit_view.dataGridView1[j, i].Value;
                            }
                        }

                        mv.craft_edit_view.dataGridView1[1, start_index].Value = shuiwei;
                        mv.craft_edit_view.dataGridView1[2, start_index].Value = speed;
                        mv.craft_edit_view.dataGridView1[3, start_index].Value = time;
                        mv.craft_edit_view.dataGridView1[4, start_index].Value = craft;
                        mv.craft_edit_view.dataGridView1[5, start_index].Value = rate;
                        mv.craft_edit_view.dataGridView1[6, start_index].Value = fengji_rate;
                        mv.craft_edit_view.dataGridView1[7, start_index].Value = tibu_rate;
                        mv.craft_edit_view.draw_line();
                        this.Dispose();
                    }
                    catch
                    {
                    }
                }
            }
            if (radioButton_paishui.Checked == true)
            {
                // 排水
                if (operation_kind == 1)
                {
                    try
                    {
                        DataSet ds = mv.mysqlconnection.sql_search_database("select * from craft_machine" + operation_machine_num.ToString());
                        int row_count = ds.Tables[0].Rows.Count;
                        DataRow craftrow = null;     //最后一行
                        if (row_count > 0) craftrow = ds.Tables[0].Rows[row_count - 1];
                        ds = mv.mysqlconnection.sql_search_database("select craft_code from craftcode where craft_name='" + comboBox_paishui.Text + "'");
                        DataRow dr = ds.Tables[0].Rows[0];
                        int code = int.Parse(dr[0].ToString());
                        int xuhao = row_count + 1;
                        int shuiwei = int.Parse(textBox_qishiwendu.Text);
                        string craft = comboBox_paishui.Text;
                        float speed = 0;
                        int time = 0;
                        if (textBox_time.Text != "")
                        {
                            time = int.Parse(textBox_time.Text);
                        }
                        int rate = 0;
                        if (textBox1_rate.Text != "")
                        {
                            rate = int.Parse(textBox1_rate.Text);
                        }
                        int fengji_rate = 0;
                        if (textBox_fengji_rate.Text != "")
                        {
                            fengji_rate = int.Parse(textBox_fengji_rate.Text);
                        }
                        int tibu_rate = 0;
                        if (textBox_tibu_rate.Text != "")
                        {
                            tibu_rate = int.Parse(textBox_tibu_rate.Text);
                        }
                        mv.mysqlconnection.excute_sql("insert into craft_machine" + operation_machine_num.ToString() + "(wendu_shuiwei,speed,time,craft,nvarcharcode,rate,fengji_rate,tibu_rate) values ('" + shuiwei.ToString() + "','" + speed.ToString() + "','" + time.ToString() + "','" + craft + "','" + code.ToString() + "','" + rate.ToString() + "','" + fengji_rate.ToString() + "','" + tibu_rate.ToString() + "')");
                        mv.detail.ReFlash_Craft_Chart();
                        mv.detail.updata_datagridview();
                        mv.detail.draw_line();
                        this.Dispose();
                    }
                    catch
                    {
                    }
                }
                if (operation_kind == 2)
                {
                    try
                    {
                        DataSet ds = mv.mysqlconnection.sql_search_database("select * from craft" + operation_machine_num.ToString());
                        int row_count = ds.Tables[0].Rows.Count;
                        DataRow craftrow = null;     //最后一行
                        if (row_count > 0) craftrow = ds.Tables[0].Rows[row_count - 1];
                        ds = mv.mysqlconnection.sql_search_database("select craft_code from craftcode where craft_name='" + comboBox_paishui.Text + "'");
                        DataRow dr = ds.Tables[0].Rows[0];
                        int code = int.Parse(dr[0].ToString());
                        int xuhao = row_count + 1;
                        int shuiwei = int.Parse(textBox_qishiwendu.Text);
                        string craft = comboBox_paishui.Text;
                        float speed = 0;
                        int time = 0;
                        if (textBox_time.Text != "")
                        {
                            time = int.Parse(textBox_time.Text);
                        }
                        int rate = 0;
                        if (textBox1_rate.Text != "")
                        {
                            rate = int.Parse(textBox1_rate.Text);
                        }
                        int fengji_rate = 0;
                        if (textBox_fengji_rate.Text != "")
                        {
                            fengji_rate = int.Parse(textBox_fengji_rate.Text);
                        }
                        int tibu_rate = 0;
                        if (textBox_tibu_rate.Text != "")
                        {
                            tibu_rate = int.Parse(textBox_tibu_rate.Text);
                        }
                        mv.mysqlconnection.excute_sql("insert into craft" + operation_machine_num.ToString() + "(wendu_shuiwei,speed,time,craft,nvarcharcode,rate,fengji_rate,tibu_rate) values ('" + shuiwei.ToString() + "','" + speed.ToString() + "','" + time.ToString() + "','" + craft + "','" + code.ToString() + "','" + rate.ToString() + "','" + fengji_rate.ToString() + "','" + tibu_rate.ToString() + "')");
                        mv.craft_edit_view.ReFlash_Craft_Chart();
                        mv.craft_edit_view.updata_datagridview();
                        mv.craft_edit_view.draw_line();
                        this.Dispose();
                    }
                    catch
                    {
                    }
                }
                if (operation_kind == 3)
                {
                    // 新建一行，并依次往后面排
                    try
                    {

                        // 获取工艺号
                        //DataSet ds = mv.mysqlconnection.sql_search_database("select craft_code from craftcode where craft_name='"+comboBox_shuixi.Text +"'");
                        //DataRow dr = ds.Tables[0].Rows[0];
                        //int code = int.Parse(dr[0].ToString());

                        int shuiwei = int.Parse(textBox_qishiwendu.Text);
                        string craft = comboBox_paishui.Text;
                        if (craft == "") return;
                        float speed = 0;
                        if (textBox_sulv.Text != "")
                        {
                            speed = float.Parse(textBox_sulv.Text);
                        }
                        int time = 0;
                        if (textBox_time.Text != "")
                        {
                            time = int.Parse(textBox_time.Text);
                        }
                        int rate = 0;
                        if (textBox1_rate.Text != "")
                        {
                            rate = int.Parse(textBox1_rate.Text);
                        }
                        int fengji_rate = 0;
                        if (textBox_fengji_rate.Text != "")
                        {
                            fengji_rate = int.Parse(textBox_fengji_rate.Text);
                        }
                        int tibu_rate = 0;
                        if (textBox_tibu_rate.Text != "")
                        {
                            tibu_rate = int.Parse(textBox_tibu_rate.Text);
                        }
                        mv.detail.dataGridView1.RowCount = mv.detail.dataGridView1.RowCount + 1;  //新增一行
                        for (int i = 0; i < mv.detail.dataGridView1.RowCount; i++)
                        {
                            mv.detail.dataGridView1[0, i].Value = i + 1;
                        }
                        for (int i = mv.detail.dataGridView1.RowCount - 2; i >= start_index; i--)
                        {
                            for (int j = 1; j < mv.detail.dataGridView1.ColumnCount; j++)
                            {
                                mv.detail.dataGridView1[j, i + 1].Value = mv.detail.dataGridView1[j, i].Value;
                            }
                        }

                        mv.detail.dataGridView1[1, start_index].Value = shuiwei;
                        mv.detail.dataGridView1[2, start_index].Value = speed;
                        mv.detail.dataGridView1[3, start_index].Value = time;
                        mv.detail.dataGridView1[4, start_index].Value = craft;
                        mv.detail.dataGridView1[5, start_index].Value = rate;
                        mv.detail.dataGridView1[6, start_index].Value = fengji_rate;
                        mv.detail.dataGridView1[7, start_index].Value = tibu_rate;
                        mv.detail.draw_line();
                        this.Dispose();
                    }
                    catch
                    {
                    }
                }
                if (operation_kind == 4)
                {
                    // 新建一行，并依次往后面排
                    try
                    {

                        // 获取工艺号
                        //DataSet ds = mv.mysqlconnection.sql_search_database("select craft_code from craftcode where craft_name='"+comboBox_shuixi.Text +"'");
                        //DataRow dr = ds.Tables[0].Rows[0];
                        //int code = int.Parse(dr[0].ToString());

                        int shuiwei = int.Parse(textBox_qishiwendu.Text);
                        string craft = comboBox_paishui.Text;
                        if (craft == "") return;
                        float speed = 0;
                        if (textBox_sulv.Text != "")
                        {
                            speed = float.Parse(textBox_sulv.Text);
                        }
                        int time = 0;
                        if (textBox_time.Text != "")
                        {
                            time = int.Parse(textBox_time.Text);
                        }
                        int rate = 0;
                        if (textBox1_rate.Text != "")
                        {
                            rate = int.Parse(textBox1_rate.Text);
                        }
                        int fengji_rate = 0;
                        if (textBox_fengji_rate.Text != "")
                        {
                            fengji_rate = int.Parse(textBox_fengji_rate.Text);
                        }
                        int tibu_rate = 0;
                        if (textBox_tibu_rate.Text != "")
                        {
                            tibu_rate = int.Parse(textBox_tibu_rate.Text);
                        }
                        mv.craft_edit_view.dataGridView1.RowCount = mv.craft_edit_view.dataGridView1.RowCount + 1;  //新增一行
                        for (int i = 0; i < mv.craft_edit_view.dataGridView1.RowCount; i++)
                        {
                            mv.craft_edit_view.dataGridView1[0, i].Value = i + 1;
                        }
                        for (int i = mv.craft_edit_view.dataGridView1.RowCount - 2; i >= start_index; i--)
                        {
                            for (int j = 1; j < mv.craft_edit_view.dataGridView1.ColumnCount; j++)
                            {
                                mv.craft_edit_view.dataGridView1[j, i + 1].Value = mv.craft_edit_view.dataGridView1[j, i].Value;
                            }
                        }

                        mv.craft_edit_view.dataGridView1[1, start_index].Value = shuiwei;
                        mv.craft_edit_view.dataGridView1[2, start_index].Value = speed;
                        mv.craft_edit_view.dataGridView1[3, start_index].Value = time;
                        mv.craft_edit_view.dataGridView1[4, start_index].Value = craft;
                        mv.craft_edit_view.dataGridView1[5, start_index].Value = rate;
                        mv.craft_edit_view.dataGridView1[6, start_index].Value = fengji_rate;
                        mv.craft_edit_view.dataGridView1[7, start_index].Value = tibu_rate;
                        mv.craft_edit_view.draw_line();
                        this.Dispose();
                    }
                    catch
                    {
                    }
                }
            }
            if (radioButton_jinshui.Checked == true)
            {
                // 进水
                if (operation_kind == 1)
                {
                    try
                    {
                        DataSet ds = mv.mysqlconnection.sql_search_database("select * from craft_machine" + operation_machine_num.ToString());
                        int row_count = ds.Tables[0].Rows.Count;
                        DataRow craftrow = null;     //最后一行
                        if (row_count > 0) craftrow = ds.Tables[0].Rows[row_count - 1];
                        ds = mv.mysqlconnection.sql_search_database("select craft_code from craftcode where craft_name='" + comboBox_jinshui.Text + "'");
                        DataRow dr = ds.Tables[0].Rows[0];
                        int code = int.Parse(dr[0].ToString());
                        int xuhao = row_count + 1;
                        int shuiwei = int.Parse(textBox_qishiwendu.Text);
                        string craft = comboBox_jinshui.Text;
                        float speed = 0;
                        int time = 0;
                        if (textBox_time.Text != "")
                        {
                            time = int.Parse(textBox_time.Text);
                        }
                        int rate = 0;
                        if (textBox1_rate.Text != "")
                        {
                            rate = int.Parse(textBox1_rate.Text);
                        }
                        int fengji_rate = 0;
                        if (textBox_fengji_rate.Text != "")
                        {
                            fengji_rate = int.Parse(textBox_fengji_rate.Text);
                        }
                        int tibu_rate = 0;
                        if (textBox_tibu_rate.Text != "")
                        {
                            tibu_rate = int.Parse(textBox_tibu_rate.Text);
                        }
                        mv.mysqlconnection.excute_sql("insert into craft_machine" + operation_machine_num.ToString() + "(wendu_shuiwei,speed,time,craft,nvarcharcode,rate,fengji_rate,tibu_rate) values ('" + shuiwei.ToString() + "','" + speed.ToString() + "','" + time.ToString() + "','" + craft + "','" + code.ToString() + "','" + rate.ToString() + "','" + fengji_rate.ToString() + "','" + tibu_rate.ToString() + "')");
                        mv.detail.ReFlash_Craft_Chart();
                        mv.detail.updata_datagridview();
                        mv.detail.draw_line();
                        this.Dispose();
                    }
                    catch
                    {
                    }
                }
                if (operation_kind == 2)
                {
                    try
                    {
                        DataSet ds = mv.mysqlconnection.sql_search_database("select * from craft" + operation_machine_num.ToString());
                        int row_count = ds.Tables[0].Rows.Count;
                        DataRow craftrow = null;     //最后一行
                        if (row_count > 0) craftrow = ds.Tables[0].Rows[row_count - 1];
                        ds = mv.mysqlconnection.sql_search_database("select craft_code from craftcode where craft_name='" + comboBox_jinshui.Text + "'");
                        DataRow dr = ds.Tables[0].Rows[0];
                        int code = int.Parse(dr[0].ToString());
                        int xuhao = row_count + 1;
                        int shuiwei = int.Parse(textBox_qishiwendu.Text);
                        string craft = comboBox_jinshui.Text;
                        float speed = 0;
                        int time = 0;
                        if (textBox_time.Text != "")
                        {
                            time = int.Parse(textBox_time.Text);
                        }
                        int rate = 0;
                        if (textBox1_rate.Text != "")
                        {
                            rate = int.Parse(textBox1_rate.Text);
                        }
                        int fengji_rate = 0;
                        if (textBox_fengji_rate.Text != "")
                        {
                            fengji_rate = int.Parse(textBox_fengji_rate.Text);
                        }
                        int tibu_rate = 0;
                        if (textBox_tibu_rate.Text != "")
                        {
                            tibu_rate = int.Parse(textBox_tibu_rate.Text);
                        }
                        mv.mysqlconnection.excute_sql("insert into craft" + operation_machine_num.ToString() + "(wendu_shuiwei,speed,time,craft,nvarcharcode,rate,fengji_rate,tibu_rate) values ('" + shuiwei.ToString() + "','" + speed.ToString() + "','" + time.ToString() + "','" + craft + "','" + code.ToString() + "','" + rate.ToString() + "','" + fengji_rate.ToString() + "','" + tibu_rate.ToString() + "')");
                        mv.craft_edit_view.ReFlash_Craft_Chart();
                        mv.craft_edit_view.updata_datagridview();
                        mv.craft_edit_view.draw_line();
                        this.Dispose();
                    }
                    catch
                    {
                    }
                }
                if (operation_kind == 3)
                {
                    // 新建一行，并依次往后面排
                    try
                    {

                        // 获取工艺号
                        //DataSet ds = mv.mysqlconnection.sql_search_database("select craft_code from craftcode where craft_name='"+comboBox_shuixi.Text +"'");
                        //DataRow dr = ds.Tables[0].Rows[0];
                        //int code = int.Parse(dr[0].ToString());

                        int shuiwei = int.Parse(textBox_qishiwendu.Text);
                        string craft = comboBox_jinshui.Text;
                        if (craft == "") return;
                        float speed = 0;
                        if (textBox_sulv.Text != "")
                        {
                            speed = float.Parse(textBox_sulv.Text);
                        }
                        int time = 0;
                        if (textBox_time.Text != "")
                        {
                            time = int.Parse(textBox_time.Text);
                        }
                        int rate = 0;
                        if (textBox1_rate.Text != "")
                        {
                            rate = int.Parse(textBox1_rate.Text);
                        }
                        int fengji_rate = 0;
                        if (textBox_fengji_rate.Text != "")
                        {
                            fengji_rate = int.Parse(textBox_fengji_rate.Text);
                        }
                        int tibu_rate = 0;
                        if (textBox_tibu_rate.Text != "")
                        {
                            tibu_rate = int.Parse(textBox_tibu_rate.Text);
                        }
                        mv.detail.dataGridView1.RowCount = mv.detail.dataGridView1.RowCount + 1;  //新增一行
                        for (int i = 0; i < mv.detail.dataGridView1.RowCount; i++)
                        {
                            mv.detail.dataGridView1[0, i].Value = i + 1;
                        }
                        for (int i = mv.detail.dataGridView1.RowCount - 2; i >= start_index; i--)
                        {
                            for (int j = 1; j < mv.detail.dataGridView1.ColumnCount; j++)
                            {
                                mv.detail.dataGridView1[j, i + 1].Value = mv.detail.dataGridView1[j, i].Value;
                            }
                        }

                        mv.detail.dataGridView1[1, start_index].Value = shuiwei;
                        mv.detail.dataGridView1[2, start_index].Value = speed;
                        mv.detail.dataGridView1[3, start_index].Value = time;
                        mv.detail.dataGridView1[4, start_index].Value = craft;
                        mv.detail.dataGridView1[5, start_index].Value = rate;
                        mv.detail.dataGridView1[6, start_index].Value = fengji_rate;
                        mv.detail.dataGridView1[7, start_index].Value = tibu_rate;
                        mv.detail.draw_line();
                        this.Dispose();
                    }
                    catch
                    {
                    }
                }
                if (operation_kind == 4)
                {
                    // 新建一行，并依次往后面排
                    try
                    {

                        // 获取工艺号
                        //DataSet ds = mv.mysqlconnection.sql_search_database("select craft_code from craftcode where craft_name='"+comboBox_shuixi.Text +"'");
                        //DataRow dr = ds.Tables[0].Rows[0];
                        //int code = int.Parse(dr[0].ToString());

                        int shuiwei = int.Parse(textBox_qishiwendu.Text);
                        string craft = comboBox_jinshui.Text;
                        if (craft == "") return;
                        float speed = 0;
                        if (textBox_sulv.Text != "")
                        {
                            speed = float.Parse(textBox_sulv.Text);
                        }
                        int time = 0;
                        if (textBox_time.Text != "")
                        {
                            time = int.Parse(textBox_time.Text);
                        }
                        int rate = 0;
                        if (textBox1_rate.Text != "")
                        {
                            rate = int.Parse(textBox1_rate.Text);
                        }
                        int fengji_rate = 0;
                        if (textBox_fengji_rate.Text != "")
                        {
                            fengji_rate = int.Parse(textBox_fengji_rate.Text);
                        }
                        int tibu_rate = 0;
                        if (textBox_tibu_rate.Text != "")
                        {
                            tibu_rate = int.Parse(textBox_tibu_rate.Text);
                        }
                        mv.craft_edit_view.dataGridView1.RowCount = mv.craft_edit_view.dataGridView1.RowCount + 1;  //新增一行
                        for (int i = 0; i < mv.craft_edit_view.dataGridView1.RowCount; i++)
                        {
                            mv.craft_edit_view.dataGridView1[0, i].Value = i + 1;
                        }
                        for (int i = mv.craft_edit_view.dataGridView1.RowCount - 2; i >= start_index; i--)
                        {
                            for (int j = 1; j < mv.craft_edit_view.dataGridView1.ColumnCount; j++)
                            {
                                mv.craft_edit_view.dataGridView1[j, i + 1].Value = mv.craft_edit_view.dataGridView1[j, i].Value;
                            }
                        }

                        mv.craft_edit_view.dataGridView1[1, start_index].Value = shuiwei;
                        mv.craft_edit_view.dataGridView1[2, start_index].Value = speed;
                        mv.craft_edit_view.dataGridView1[3, start_index].Value = time;
                        mv.craft_edit_view.dataGridView1[4, start_index].Value = craft;
                        mv.craft_edit_view.dataGridView1[5, start_index].Value = rate;
                        mv.craft_edit_view.dataGridView1[6, start_index].Value = fengji_rate;
                        mv.craft_edit_view.dataGridView1[7, start_index].Value = tibu_rate;
                        mv.craft_edit_view.draw_line();
                        this.Dispose();
                    }
                    catch
                    {
                    }
                }
            }

            if (radioButton_liaogang.Checked==true)
            {
                // 流程
                if (operation_kind == 1)
                {
                    try
                    {
                        DataSet ds = mv.mysqlconnection.sql_search_database("select * from craft_machine" + operation_machine_num.ToString());
                        int row_count = ds.Tables[0].Rows.Count;
                        DataRow craftrow = null;     //最后一行
                        if (row_count > 0) craftrow = ds.Tables[0].Rows[row_count - 1];
                        ds = mv.mysqlconnection.sql_search_database("select craft_code from craftcode where craft_name='" + comboBox_liaogang.Text + "'");
                        DataRow dr = ds.Tables[0].Rows[0];
                        int code = int.Parse(dr[0].ToString());
                        int xuhao = row_count + 1;
                        int shuiwei = int.Parse(textBox_qishiwendu.Text);
                        string craft = comboBox_liaogang.Text;
                        float speed = 0;
                        int time = 0;
                        if (textBox_time.Text != "")
                        {
                            time = int.Parse(textBox_time.Text);
                        }
                        int rate = 0;
                        if (textBox1_rate.Text != "")
                        {
                            rate = int.Parse(textBox1_rate.Text);
                        }
                        int fengji_rate = 0;
                        if (textBox_fengji_rate.Text != "")
                        {
                            fengji_rate = int.Parse(textBox_fengji_rate.Text);
                        }
                        int tibu_rate = 0;
                        if (textBox_tibu_rate.Text != "")
                        {
                            tibu_rate = int.Parse(textBox_tibu_rate.Text);
                        }
                        mv.mysqlconnection.excute_sql("insert into craft_machine" + operation_machine_num.ToString() + "(wendu_shuiwei,speed,time,craft,nvarcharcode,rate,fengji_rate,tibu_rate) values ('" + shuiwei.ToString() + "','" + speed.ToString() + "','" + time.ToString() + "','" + craft + "','" + code.ToString() + "','" + rate.ToString() + "','" + fengji_rate.ToString() + "','" + tibu_rate.ToString() + "')");
                        mv.detail.ReFlash_Craft_Chart();
                        mv.detail.updata_datagridview();
                        mv.detail.draw_line();
                        this.Dispose();
                    }
                    catch
                    {
                    }
                }
                if (operation_kind == 2)
                {
                    try
                    {
                        DataSet ds = mv.mysqlconnection.sql_search_database("select * from craft" + operation_machine_num.ToString());
                        int row_count = ds.Tables[0].Rows.Count;
                        DataRow craftrow = null;     //最后一行
                        if (row_count > 0) craftrow = ds.Tables[0].Rows[row_count - 1];
                        ds = mv.mysqlconnection.sql_search_database("select craft_code from craftcode where craft_name='" + comboBox_liaogang.Text + "'");
                        DataRow dr = ds.Tables[0].Rows[0];
                        int code = int.Parse(dr[0].ToString());
                        int xuhao = row_count + 1;
                        int shuiwei = int.Parse(textBox_qishiwendu.Text);
                        string craft = comboBox_liaogang.Text;
                        float speed = 0;
                        int time = 0;
                        if (textBox_time.Text != "")
                        {
                            time = int.Parse(textBox_time.Text);
                        }
                        int rate = 0;
                        if (textBox1_rate.Text != "")
                        {
                            rate = int.Parse(textBox1_rate.Text);
                        }
                        int fengji_rate = 0;
                        if (textBox_fengji_rate.Text != "")
                        {
                            fengji_rate = int.Parse(textBox_fengji_rate.Text);
                        }
                        int tibu_rate = 0;
                        if (textBox_tibu_rate.Text != "")
                        {
                            tibu_rate = int.Parse(textBox_tibu_rate.Text);
                        }
                        mv.mysqlconnection.excute_sql("insert into craft" + operation_machine_num.ToString() + "(wendu_shuiwei,speed,time,craft,nvarcharcode,rate,fengji_rate,tibu_rate) values ('" + shuiwei.ToString() + "','" + speed.ToString() + "','" + time.ToString() + "','" + craft + "','" + code.ToString() + "','" + rate.ToString() + "','" + fengji_rate.ToString() + "','" + tibu_rate.ToString() + "')");
                        mv.craft_edit_view.ReFlash_Craft_Chart();
                        mv.craft_edit_view.updata_datagridview();
                        mv.craft_edit_view.draw_line();
                        this.Dispose();
                    }
                    catch
                    {
                    }
                }
                if (operation_kind == 3)
                {
                    // 新建一行，并依次往后面排
                    try
                    {

                        // 获取工艺号
                        //DataSet ds = mv.mysqlconnection.sql_search_database("select craft_code from craftcode where craft_name='"+comboBox_shuixi.Text +"'");
                        //DataRow dr = ds.Tables[0].Rows[0];
                        //int code = int.Parse(dr[0].ToString());

                        int shuiwei = int.Parse(textBox_qishiwendu.Text);
                        string craft = comboBox_liaogang.Text;
                        if (craft == "") return;
                        float speed = 0;
                        if (textBox_sulv.Text != "")
                        {
                            speed = float.Parse(textBox_sulv.Text);
                        }
                        int time = 0;
                        if (textBox_time.Text != "")
                        {
                            time = int.Parse(textBox_time.Text);
                        }
                        int rate = 0;
                        if (textBox1_rate.Text != "")
                        {
                            rate = int.Parse(textBox1_rate.Text);
                        }
                        int fengji_rate = 0;
                        if (textBox_fengji_rate.Text != "")
                        {
                            fengji_rate = int.Parse(textBox_fengji_rate.Text);
                        }
                        int tibu_rate = 0;
                        if (textBox_tibu_rate.Text != "")
                        {
                            tibu_rate = int.Parse(textBox_tibu_rate.Text);
                        }
                        mv.detail.dataGridView1.RowCount = mv.detail.dataGridView1.RowCount + 1;  //新增一行
                        for (int i = 0; i < mv.detail.dataGridView1.RowCount; i++)
                        {
                            mv.detail.dataGridView1[0, i].Value = i + 1;
                        }
                        for (int i = mv.detail.dataGridView1.RowCount - 2; i >= start_index; i--)
                        {
                            for (int j = 1; j < mv.detail.dataGridView1.ColumnCount; j++)
                            {
                                mv.detail.dataGridView1[j, i + 1].Value = mv.detail.dataGridView1[j, i].Value;
                            }
                        }

                        mv.detail.dataGridView1[1, start_index].Value = shuiwei;
                        mv.detail.dataGridView1[2, start_index].Value = speed;
                        mv.detail.dataGridView1[3, start_index].Value = time;
                        mv.detail.dataGridView1[4, start_index].Value = craft;
                        mv.detail.dataGridView1[5, start_index].Value = rate;
                        mv.detail.dataGridView1[6, start_index].Value = fengji_rate;
                        mv.detail.dataGridView1[7, start_index].Value = tibu_rate;
                        mv.detail.draw_line();
                        this.Dispose();
                    }
                    catch
                    {
                    }
                }
                if (operation_kind == 4)
                {
                    // 新建一行，并依次往后面排
                    try
                    {

                        // 获取工艺号
                        //DataSet ds = mv.mysqlconnection.sql_search_database("select craft_code from craftcode where craft_name='"+comboBox_shuixi.Text +"'");
                        //DataRow dr = ds.Tables[0].Rows[0];
                        //int code = int.Parse(dr[0].ToString());

                        int shuiwei = int.Parse(textBox_qishiwendu.Text);
                        string craft = comboBox_liaogang.Text;
                        if (craft == "") return;
                        float speed = 0;
                        if (textBox_sulv.Text != "")
                        {
                            speed = float.Parse(textBox_sulv.Text);
                        }
                        int time = 0;
                        if (textBox_time.Text != "")
                        {
                            time = int.Parse(textBox_time.Text);
                        }
                        int rate = 0;
                        if (textBox1_rate.Text != "")
                        {
                            rate = int.Parse(textBox1_rate.Text);
                        }
                        int fengji_rate = 0;
                        if (textBox_fengji_rate.Text != "")
                        {
                            fengji_rate = int.Parse(textBox_fengji_rate.Text);
                        }
                        int tibu_rate = 0;
                        if (textBox_tibu_rate.Text != "")
                        {
                            tibu_rate = int.Parse(textBox_tibu_rate.Text);
                        }
                        mv.craft_edit_view.dataGridView1.RowCount = mv.craft_edit_view.dataGridView1.RowCount + 1;  //新增一行
                        for (int i = 0; i < mv.craft_edit_view.dataGridView1.RowCount; i++)
                        {
                            mv.craft_edit_view.dataGridView1[0, i].Value = i + 1;
                        }
                        for (int i = mv.craft_edit_view.dataGridView1.RowCount - 2; i >= start_index; i--)
                        {
                            for (int j = 1; j < mv.craft_edit_view.dataGridView1.ColumnCount; j++)
                            {
                                mv.craft_edit_view.dataGridView1[j, i + 1].Value = mv.craft_edit_view.dataGridView1[j, i].Value;
                            }
                        }

                        mv.craft_edit_view.dataGridView1[1, start_index].Value = shuiwei;
                        mv.craft_edit_view.dataGridView1[2, start_index].Value = speed;
                        mv.craft_edit_view.dataGridView1[3, start_index].Value = time;
                        mv.craft_edit_view.dataGridView1[4, start_index].Value = craft;
                        mv.craft_edit_view.dataGridView1[5, start_index].Value = rate;
                        mv.craft_edit_view.dataGridView1[6, start_index].Value = fengji_rate;
                        mv.craft_edit_view.dataGridView1[7, start_index].Value = tibu_rate;
                        mv.craft_edit_view.draw_line();
                        this.Dispose();
                    }
                    catch
                    {
                    }
                }
            }
            if (radioButton_liucheng.Checked == true)
            {
                // 流程
                if (operation_kind == 1)
                {
                    try
                    {
                        DataSet ds = mv.mysqlconnection.sql_search_database("select * from craft_machine" + operation_machine_num.ToString());
                        int row_count = ds.Tables[0].Rows.Count;
                        DataRow craftrow = null;     //最后一行
                        if (row_count > 0) craftrow = ds.Tables[0].Rows[row_count - 1];
                        ds = mv.mysqlconnection.sql_search_database("select craft_code from craftcode where craft_name='" + comboBox_liucheng.Text + "'");
                        DataRow dr = ds.Tables[0].Rows[0];
                        int code = int.Parse(dr[0].ToString());
                        int xuhao = row_count + 1;
                        int shuiwei = int.Parse(textBox_qishiwendu.Text);
                        string craft = comboBox_liucheng.Text;
                        float speed = 0;
                        int time = 0;
                        if (textBox_time.Text != "")
                        {
                            time = int.Parse(textBox_time.Text);
                        }
                        int rate = 0;
                        if (textBox1_rate.Text != "")
                        {
                            rate = int.Parse(textBox1_rate.Text);
                        }
                        int fengji_rate = 0;
                        if (textBox_fengji_rate.Text != "")
                        {
                            fengji_rate = int.Parse(textBox_fengji_rate.Text);
                        }
                        int tibu_rate = 0;
                        if (textBox_tibu_rate.Text != "")
                        {
                            tibu_rate = int.Parse(textBox_tibu_rate.Text);
                        }
                        mv.mysqlconnection.excute_sql("insert into craft_machine" + operation_machine_num.ToString() + "(wendu_shuiwei,speed,time,craft,nvarcharcode,rate,fengji_rate,tibu_rate) values ('" + shuiwei.ToString() + "','" + speed.ToString() + "','" + time.ToString() + "','" + craft + "','" + code.ToString() + "','" + rate.ToString() + "','" + fengji_rate.ToString() + "','" + tibu_rate.ToString() + "')");
                        mv.detail.ReFlash_Craft_Chart();
                        mv.detail.updata_datagridview();
                        mv.detail.draw_line();
                        this.Dispose();
                    }
                    catch
                    {
                    }
                }
                if (operation_kind == 2)
                {
                    try
                    {
                        DataSet ds = mv.mysqlconnection.sql_search_database("select * from craft" + operation_machine_num.ToString());
                        int row_count = ds.Tables[0].Rows.Count;
                        DataRow craftrow = null;     //最后一行
                        if (row_count > 0) craftrow = ds.Tables[0].Rows[row_count - 1];
                        ds = mv.mysqlconnection.sql_search_database("select craft_code from craftcode where craft_name='" + comboBox_liucheng.Text  + "'");
                        DataRow dr = ds.Tables[0].Rows[0];
                        int code = int.Parse(dr[0].ToString());
                        int xuhao = row_count + 1;
                        int shuiwei = int.Parse(textBox_qishiwendu.Text);
                        string craft = comboBox_liucheng.Text;
                        float speed = 0;
                        int time = 0;
                        if (textBox_time.Text != "")
                        {
                            time = int.Parse(textBox_time.Text);
                        }
                        int rate = 0;
                        if (textBox1_rate.Text != "")
                        {
                            rate = int.Parse(textBox1_rate.Text);
                        }
                        int fengji_rate = 0;
                        if (textBox_fengji_rate.Text != "")
                        {
                            fengji_rate = int.Parse(textBox_fengji_rate.Text);
                        }
                        int tibu_rate = 0;
                        if (textBox_tibu_rate.Text != "")
                        {
                            tibu_rate = int.Parse(textBox_tibu_rate.Text);
                        }
                        mv.mysqlconnection.excute_sql("insert into craft" + operation_machine_num.ToString() + "(wendu_shuiwei,speed,time,craft,nvarcharcode,rate,fengji_rate,tibu_rate) values ('" + shuiwei.ToString() + "','" + speed.ToString() + "','" + time.ToString() + "','" + craft + "','" + code.ToString() + "','" + rate.ToString() + "','" + fengji_rate.ToString() + "','" + tibu_rate.ToString() + "')");
                        mv.craft_edit_view.ReFlash_Craft_Chart();
                        mv.craft_edit_view.updata_datagridview();
                        mv.craft_edit_view.draw_line();
                        this.Dispose();
                    }
                    catch
                    {
                    }
                }
                if (operation_kind == 3)
                {
                    // 新建一行，并依次往后面排
                    try
                    {

                        // 获取工艺号
                        //DataSet ds = mv.mysqlconnection.sql_search_database("select craft_code from craftcode where craft_name='"+comboBox_shuixi.Text +"'");
                        //DataRow dr = ds.Tables[0].Rows[0];
                        //int code = int.Parse(dr[0].ToString());

                        int shuiwei = int.Parse(textBox_qishiwendu.Text);
                        string craft = comboBox_liucheng.Text;
                        if (craft == "") return;
                        float speed = 0;
                        if (textBox_sulv.Text != "")
                        {
                            speed = float.Parse(textBox_sulv.Text);
                        }
                        int time = 0;
                        if (textBox_time.Text != "")
                        {
                            time = int.Parse(textBox_time.Text);
                        }
                        int rate = 0;
                        if (textBox1_rate.Text != "")
                        {
                            rate = int.Parse(textBox1_rate.Text);
                        }
                        int fengji_rate = 0;
                        if (textBox_fengji_rate.Text != "")
                        {
                            fengji_rate = int.Parse(textBox_fengji_rate.Text);
                        }
                        int tibu_rate = 0;
                        if (textBox_tibu_rate.Text != "")
                        {
                            tibu_rate = int.Parse(textBox_tibu_rate.Text);
                        }
                        mv.detail.dataGridView1.RowCount = mv.detail.dataGridView1.RowCount + 1;  //新增一行
                        for (int i = 0; i < mv.detail.dataGridView1.RowCount; i++)
                        {
                            mv.detail.dataGridView1[0, i].Value = i + 1;
                        }
                        for (int i = mv.detail.dataGridView1.RowCount - 2; i >= start_index; i--)
                        {
                            for (int j = 1; j < mv.detail.dataGridView1.ColumnCount; j++)
                            {
                                mv.detail.dataGridView1[j, i + 1].Value = mv.detail.dataGridView1[j, i].Value;
                            }
                        }

                        mv.detail.dataGridView1[1, start_index].Value = shuiwei;
                        mv.detail.dataGridView1[2, start_index].Value = speed;
                        mv.detail.dataGridView1[3, start_index].Value = time;
                        mv.detail.dataGridView1[4, start_index].Value = craft;
                        mv.detail.dataGridView1[5, start_index].Value = rate;
                        mv.detail.dataGridView1[6, start_index].Value = fengji_rate;
                        mv.detail.dataGridView1[7, start_index].Value = tibu_rate;
                        mv.detail.draw_line();
                        this.Dispose();
                    }
                    catch
                    {
                    }
                }
                if (operation_kind == 4)
                {
                    // 新建一行，并依次往后面排
                    try
                    {

                        // 获取工艺号
                        //DataSet ds = mv.mysqlconnection.sql_search_database("select craft_code from craftcode where craft_name='"+comboBox_shuixi.Text +"'");
                        //DataRow dr = ds.Tables[0].Rows[0];
                        //int code = int.Parse(dr[0].ToString());

                        int shuiwei = int.Parse(textBox_qishiwendu.Text);
                        string craft = comboBox_liucheng.Text;
                        if (craft == "") return;
                        float speed = 0;
                        if (textBox_sulv.Text != "")
                        {
                            speed = float.Parse(textBox_sulv.Text);
                        }
                        int time = 0;
                        if (textBox_time.Text != "")
                        {
                            time = int.Parse(textBox_time.Text);
                        }
                        int rate = 0;
                        if (textBox1_rate.Text != "")
                        {
                            rate = int.Parse(textBox1_rate.Text);
                        }
                        int fengji_rate = 0;
                        if (textBox_fengji_rate.Text != "")
                        {
                            fengji_rate = int.Parse(textBox_fengji_rate.Text);
                        }
                        int tibu_rate = 0;
                        if (textBox_tibu_rate.Text != "")
                        {
                            tibu_rate = int.Parse(textBox_tibu_rate.Text);
                        }
                        mv.craft_edit_view.dataGridView1.RowCount = mv.craft_edit_view.dataGridView1.RowCount + 1;  //新增一行
                        for (int i = 0; i < mv.craft_edit_view.dataGridView1.RowCount; i++)
                        {
                            mv.craft_edit_view.dataGridView1[0, i].Value = i + 1;
                        }
                        for (int i = mv.craft_edit_view.dataGridView1.RowCount - 2; i >= start_index; i--)
                        {
                            for (int j = 1; j < mv.craft_edit_view.dataGridView1.ColumnCount; j++)
                            {
                                mv.craft_edit_view.dataGridView1[j, i + 1].Value = mv.craft_edit_view.dataGridView1[j, i].Value;
                            }
                        }

                        mv.craft_edit_view.dataGridView1[1, start_index].Value = shuiwei;
                        mv.craft_edit_view.dataGridView1[2, start_index].Value = speed;
                        mv.craft_edit_view.dataGridView1[3, start_index].Value = time;
                        mv.craft_edit_view.dataGridView1[4, start_index].Value = craft;
                        mv.craft_edit_view.dataGridView1[5, start_index].Value = rate;
                        mv.craft_edit_view.dataGridView1[6, start_index].Value = fengji_rate;
                        mv.craft_edit_view.dataGridView1[7, start_index].Value = tibu_rate;
                        mv.craft_edit_view.draw_line();
                        this.Dispose();
                    }
                    catch
                    {
                    }
                }
            }
            if (radioButton_shoudongjialiao.Checked == true)
            {

                // 加料工艺
                if (operation_kind == 1)
                {
                    try
                    {
                        DataSet ds = mv.mysqlconnection.sql_search_database("select * from craft_machine" + operation_machine_num.ToString());
                        int row_count = ds.Tables[0].Rows.Count;
                        DataRow craftrow = null;     //最后一行
                        if (row_count > 0) craftrow = ds.Tables[0].Rows[row_count - 1];
                        ds = mv.mysqlconnection.sql_search_database("select craft_code from craftcode where craft_name='" +  comboBox_shoudongjialiao.Text + "'");
                        DataRow dr = ds.Tables[0].Rows[0];
                        int code = int.Parse(dr[0].ToString());
                        int xuhao = row_count + 1;
                        int shuiwei = int.Parse(textBox_qishiwendu.Text);
                        string craft = comboBox_shoudongjialiao.Text;
                        float speed = 0;
                        int time = 0;
                        if (textBox_time.Text != "")
                        {
                            time = int.Parse(textBox_time.Text);
                        }
                        int rate = 0;
                        if (textBox1_rate.Text != "")
                        {
                            rate = int.Parse(textBox1_rate.Text);
                        }
                        int fengji_rate = 0;
                        if (textBox_fengji_rate.Text != "")
                        {
                            fengji_rate = int.Parse(textBox_fengji_rate.Text);
                        }
                        int tibu_rate = 0;
                        if (textBox_tibu_rate.Text != "")
                        {
                            tibu_rate = int.Parse(textBox_tibu_rate.Text);
                        }
                        mv.mysqlconnection.excute_sql("insert into craft_machine" + operation_machine_num.ToString() + "(wendu_shuiwei,speed,time,craft,nvarcharcode,rate,fengji_rate,tibu_rate) values ('" + shuiwei.ToString() + "','" + speed.ToString() + "','" + time.ToString() + "','" + craft + "','" + code.ToString() + "','" + rate.ToString() + "','" + fengji_rate.ToString() + "','" + tibu_rate.ToString() + "')");
                        mv.detail.ReFlash_Craft_Chart();
                        mv.detail.updata_datagridview();
                        mv.detail.draw_line();
                        this.Dispose();
                    }
                    catch
                    {
                    }
                }
                if (operation_kind == 2)
                {
                    try
                    {
                        DataSet ds = mv.mysqlconnection.sql_search_database("select * from craft" + operation_machine_num.ToString());
                        int row_count = ds.Tables[0].Rows.Count;
                        DataRow craftrow = null;     //最后一行
                        if (row_count > 0) craftrow = ds.Tables[0].Rows[row_count - 1];
                        ds = mv.mysqlconnection.sql_search_database("select craft_code from craftcode where craft_name='" + comboBox_shoudongjialiao.Text  + "'");
                        DataRow dr = ds.Tables[0].Rows[0];
                        int code = int.Parse(dr[0].ToString());
                        int xuhao = row_count + 1;
                        int shuiwei = int.Parse(textBox_qishiwendu.Text);
                        string craft = comboBox_shoudongjialiao.Text;
                        float speed = 0;
                        int time = 0;
                        if (textBox_time.Text != "")
                        {
                            time = int.Parse(textBox_time.Text);
                        }
                        int rate = 0;
                        if (textBox1_rate.Text != "")
                        {
                            rate = int.Parse(textBox1_rate.Text);
                        }
                        int fengji_rate = 0;
                        if (textBox_fengji_rate.Text != "")
                        {
                            fengji_rate = int.Parse(textBox_fengji_rate.Text);
                        }
                        int tibu_rate = 0;
                        if (textBox_tibu_rate.Text != "")
                        {
                            tibu_rate = int.Parse(textBox_tibu_rate.Text);
                        }
                        mv.mysqlconnection.excute_sql("insert into craft" + operation_machine_num.ToString() + "(wendu_shuiwei,speed,time,craft,nvarcharcode,rate,fengji_rate,tibu_rate) values ('" + shuiwei.ToString() + "','" + speed.ToString() + "','" + time.ToString() + "','" + craft + "','" + code.ToString() + "','" + rate.ToString() + "','" + fengji_rate.ToString() + "','" + tibu_rate.ToString() + "')");
                        mv.craft_edit_view.ReFlash_Craft_Chart();
                        mv.craft_edit_view.updata_datagridview();
                        mv.craft_edit_view.draw_line();
                        this.Dispose();
                    }
                    catch
                    {
                    }
                }
                if (operation_kind == 3)
                {
                    // 新建一行，并依次往后面排
                    try
                    {

                        // 获取工艺号
                        //DataSet ds = mv.mysqlconnection.sql_search_database("select craft_code from craftcode where craft_name='"+comboBox_shuixi.Text +"'");
                        //DataRow dr = ds.Tables[0].Rows[0];
                        //int code = int.Parse(dr[0].ToString());

                        int shuiwei = int.Parse(textBox_qishiwendu.Text);
                        string craft = comboBox_shoudongjialiao.Text;
                        if (craft == "") return;
                        float speed = 0;
                        if (textBox_sulv.Text != "")
                        {
                            speed = float.Parse(textBox_sulv.Text);
                        }
                        int time = 0;
                        if (textBox_time.Text != "")
                        {
                            time = int.Parse(textBox_time.Text);
                        }
                        int rate = 0;
                        if (textBox1_rate.Text != "")
                        {
                            rate = int.Parse(textBox1_rate.Text);
                        }
                        int fengji_rate = 0;
                        if (textBox_fengji_rate.Text != "")
                        {
                            fengji_rate = int.Parse(textBox_fengji_rate.Text);
                        }
                        int tibu_rate = 0;
                        if (textBox_tibu_rate.Text != "")
                        {
                            tibu_rate = int.Parse(textBox_tibu_rate.Text);
                        }
                        mv.detail.dataGridView1.RowCount = mv.detail.dataGridView1.RowCount + 1;  //新增一行
                        for (int i = 0; i < mv.detail.dataGridView1.RowCount; i++)
                        {
                            mv.detail.dataGridView1[0, i].Value = i + 1;
                        }
                        for (int i = mv.detail.dataGridView1.RowCount - 2; i >= start_index; i--)
                        {
                            for (int j = 1; j < mv.detail.dataGridView1.ColumnCount; j++)
                            {
                                mv.detail.dataGridView1[j, i + 1].Value = mv.detail.dataGridView1[j, i].Value;
                            }
                        }

                        mv.detail.dataGridView1[1, start_index].Value = shuiwei;
                        mv.detail.dataGridView1[2, start_index].Value = speed;
                        mv.detail.dataGridView1[3, start_index].Value = time;
                        mv.detail.dataGridView1[4, start_index].Value = craft;
                        mv.detail.dataGridView1[5, start_index].Value = rate;
                        mv.detail.dataGridView1[6, start_index].Value = fengji_rate;
                        mv.detail.dataGridView1[7, start_index].Value = tibu_rate;
                        mv.detail.draw_line();
                        this.Dispose();
                    }
                    catch
                    {
                    }
                }
                if (operation_kind == 4)
                {
                    // 新建一行，并依次往后面排
                    try
                    {

                        // 获取工艺号
                        //DataSet ds = mv.mysqlconnection.sql_search_database("select craft_code from craftcode where craft_name='"+comboBox_shuixi.Text +"'");
                        //DataRow dr = ds.Tables[0].Rows[0];
                        //int code = int.Parse(dr[0].ToString());

                        int shuiwei = int.Parse(textBox_qishiwendu.Text);
                        string craft = comboBox_shoudongjialiao.Text;
                        if (craft == "") return;
                        float speed = 0;
                        if (textBox_sulv.Text != "")
                        {
                            speed = float.Parse(textBox_sulv.Text);
                        }
                        int time = 0;
                        if (textBox_time.Text != "")
                        {
                            time = int.Parse(textBox_time.Text);
                        }
                        int rate = 0;
                        if (textBox1_rate.Text != "")
                        {
                            rate = int.Parse(textBox1_rate.Text);
                        }
                        int fengji_rate = 0;
                        if (textBox_fengji_rate.Text != "")
                        {
                            fengji_rate = int.Parse(textBox_fengji_rate.Text);
                        }
                        int tibu_rate = 0;
                        if (textBox_tibu_rate.Text != "")
                        {
                            tibu_rate = int.Parse(textBox_tibu_rate.Text);
                        }
                        mv.craft_edit_view.dataGridView1.RowCount = mv.craft_edit_view.dataGridView1.RowCount + 1;  //新增一行
                        for (int i = 0; i < mv.craft_edit_view.dataGridView1.RowCount; i++)
                        {
                            mv.craft_edit_view.dataGridView1[0, i].Value = i + 1;
                        }
                        for (int i = mv.craft_edit_view.dataGridView1.RowCount - 2; i >= start_index; i--)
                        {
                            for (int j = 1; j < mv.craft_edit_view.dataGridView1.ColumnCount; j++)
                            {
                                mv.craft_edit_view.dataGridView1[j, i + 1].Value = mv.craft_edit_view .dataGridView1[j, i].Value;
                            }
                        }

                        mv.craft_edit_view.dataGridView1[1, start_index].Value = shuiwei;
                        mv.craft_edit_view.dataGridView1[2, start_index].Value = speed;
                        mv.craft_edit_view.dataGridView1[3, start_index].Value = time;
                        mv.craft_edit_view.dataGridView1[4, start_index].Value = craft;
                        mv.craft_edit_view.dataGridView1[5, start_index].Value = rate;
                        mv.craft_edit_view.dataGridView1[6, start_index].Value = fengji_rate;
                        mv.craft_edit_view.dataGridView1[7, start_index].Value = tibu_rate;
                        mv.craft_edit_view.draw_line();
                        this.Dispose();
                    }
                    catch
                    {
                    }
                }
            }
            if (radioButton_jialiao.Checked == true)
            {
                // 加料工艺
                if (operation_kind == 1)
                {
                    try
                    {
                        DataSet ds = mv.mysqlconnection.sql_search_database("select * from craft_machine" + operation_machine_num.ToString());
                        int row_count = ds.Tables[0].Rows.Count;
                        DataRow craftrow = null;     //最后一行
                        if (row_count > 0) craftrow = ds.Tables[0].Rows[row_count - 1];
                        ds = mv.mysqlconnection.sql_search_database("select craft_code from craftcode where craft_name='" + comboBox_jialiao.Text  + "'");
                        DataRow dr = ds.Tables[0].Rows[0];
                        int code = int.Parse(dr[0].ToString());
                        int xuhao = row_count + 1;
                        int shuiwei = int.Parse(textBox_qishiwendu.Text);
                        string craft = comboBox_jialiao.Text;
                        float speed = 0;
                        int time = 0;
                        if (textBox_time.Text != "")
                        {
                            time = int.Parse(textBox_time.Text);
                        }
                        int rate = 0;
                        if (textBox1_rate.Text != "")
                        {
                            rate = int.Parse(textBox1_rate.Text);
                        }
                        int fengji_rate = 0;
                        if (textBox_fengji_rate.Text != "")
                        {
                            fengji_rate = int.Parse(textBox_fengji_rate.Text);
                        }
                        int tibu_rate = 0;
                        if (textBox_tibu_rate.Text != "")
                        {
                            tibu_rate = int.Parse(textBox_tibu_rate.Text);
                        }
                        mv.mysqlconnection.excute_sql("insert into craft_machine" + operation_machine_num.ToString() + "(wendu_shuiwei,speed,time,craft,nvarcharcode,rate,fengji_rate,tibu_rate) values ('" + shuiwei.ToString() + "','" + speed.ToString() + "','" + time.ToString() + "','" + craft + "','" + code.ToString() + "','" + rate.ToString() + "','" + fengji_rate.ToString() + "','" + tibu_rate.ToString() + "')");
                        mv.detail.ReFlash_Craft_Chart();
                        mv.detail.updata_datagridview();
                        mv.detail.draw_line();
                        this.Dispose();
                    }
                    catch
                    {
                    }
                }
                if (operation_kind == 2)
                {
                    try
                    {
                        DataSet ds = mv.mysqlconnection.sql_search_database("select * from craft" + operation_machine_num.ToString());
                        int row_count = ds.Tables[0].Rows.Count;
                        DataRow craftrow = null;     //最后一行
                        if (row_count > 0) craftrow = ds.Tables[0].Rows[row_count - 1];
                        ds = mv.mysqlconnection.sql_search_database("select craft_code from craftcode where craft_name='" + comboBox_jialiao.Text  + "'");
                        DataRow dr = ds.Tables[0].Rows[0];
                        int code = int.Parse(dr[0].ToString());
                        int xuhao = row_count + 1;
                        int shuiwei = int.Parse(textBox_qishiwendu.Text);
                        string craft = comboBox_jialiao.Text;
                        float speed = 0;
                        int time = 0;
                        if (textBox_time.Text != "")
                        {
                            time = int.Parse(textBox_time.Text);
                        }
                        int rate = 0;
                        if (textBox1_rate.Text != "")
                        {
                            rate = int.Parse(textBox1_rate.Text);
                        }
                        int fengji_rate = 0;
                        if (textBox_fengji_rate.Text != "")
                        {
                            fengji_rate = int.Parse(textBox_fengji_rate.Text);
                        }
                        int tibu_rate = 0;
                        if (textBox_tibu_rate.Text != "")
                        {
                            tibu_rate = int.Parse(textBox_tibu_rate.Text);
                        }
                        mv.mysqlconnection.excute_sql("insert into craft" + operation_machine_num.ToString() + "(wendu_shuiwei,speed,time,craft,nvarcharcode,rate,fengji_rate,tibu_rate) values ('" + shuiwei.ToString() + "','" + speed.ToString() + "','" + time.ToString() + "','" + craft + "','" + code.ToString() + "','" + rate.ToString() + "','" + fengji_rate.ToString() + "','" + tibu_rate.ToString() + "')");
                        mv.craft_edit_view.ReFlash_Craft_Chart();
                        mv.craft_edit_view.updata_datagridview();
                        mv.craft_edit_view.draw_line();
                        this.Dispose();
                    }
                    catch
                    {
                    }
                }
                if (operation_kind == 3)
                {
                    // 新建一行，并依次往后面排
                    try
                    {

                        // 获取工艺号
                        //DataSet ds = mv.mysqlconnection.sql_search_database("select craft_code from craftcode where craft_name='"+comboBox_shuixi.Text +"'");
                        //DataRow dr = ds.Tables[0].Rows[0];
                        //int code = int.Parse(dr[0].ToString());

                        int shuiwei = int.Parse(textBox_qishiwendu.Text);
                        string craft = comboBox_jialiao.Text;
                        if (craft == "") return;
                        float speed = 0;
                        if (textBox_sulv.Text != "")
                        {
                            speed = float.Parse(textBox_sulv.Text);
                        }
                        int time = 0;
                        if (textBox_time.Text != "")
                        {
                            time = int.Parse(textBox_time.Text);
                        }
                        int rate = 0;
                        if (textBox1_rate.Text != "")
                        {
                            rate = int.Parse(textBox1_rate.Text);
                        }
                        int fengji_rate = 0;
                        if (textBox_fengji_rate.Text != "")
                        {
                            fengji_rate = int.Parse(textBox_fengji_rate.Text);
                        }
                        int tibu_rate = 0;
                        if (textBox_tibu_rate.Text != "")
                        {
                            tibu_rate = int.Parse(textBox_tibu_rate.Text);
                        }
                        mv.detail.dataGridView1.RowCount = mv.detail.dataGridView1.RowCount + 1;  //新增一行
                        for (int i = 0; i < mv.detail.dataGridView1.RowCount; i++)
                        {
                            mv.detail.dataGridView1[0, i].Value = i + 1;
                        }
                        for (int i = mv.detail.dataGridView1.RowCount - 2; i >= start_index; i--)
                        {
                            for (int j = 1; j < mv.detail.dataGridView1.ColumnCount; j++)
                            {
                                mv.detail.dataGridView1[j, i + 1].Value = mv.detail.dataGridView1[j, i].Value;
                            }
                        }

                        mv.detail.dataGridView1[1, start_index].Value = shuiwei;
                        mv.detail.dataGridView1[2, start_index].Value = speed;
                        mv.detail.dataGridView1[3, start_index].Value = time;
                        mv.detail.dataGridView1[4, start_index].Value = craft;
                        mv.detail.dataGridView1[5, start_index].Value = rate;
                        mv.detail.dataGridView1[6, start_index].Value = fengji_rate;
                        mv.detail.dataGridView1[7, start_index].Value = tibu_rate;
                        mv.detail.draw_line();
                        this.Dispose();
                    }
                    catch
                    {
                    }
                }
                if (operation_kind == 4)
                {
                    // 新建一行，并依次往后面排
                    try
                    {

                        // 获取工艺号
                        //DataSet ds = mv.mysqlconnection.sql_search_database("select craft_code from craftcode where craft_name='"+comboBox_shuixi.Text +"'");
                        //DataRow dr = ds.Tables[0].Rows[0];
                        //int code = int.Parse(dr[0].ToString());

                        int shuiwei = int.Parse(textBox_qishiwendu.Text);
                        string craft = comboBox_jialiao.Text;
                        if (craft == "") return;
                        float speed = 0;
                        if (textBox_sulv.Text != "")
                        {
                            speed = float.Parse(textBox_sulv.Text);
                        }
                        int time = 0;
                        if (textBox_time.Text != "")
                        {
                            time = int.Parse(textBox_time.Text);
                        }
                        int rate = 0;
                        if (textBox1_rate.Text != "")
                        {
                            rate = int.Parse(textBox1_rate.Text);
                        }
                        int fengji_rate = 0;
                        if (textBox_fengji_rate.Text != "")
                        {
                            fengji_rate = int.Parse(textBox_fengji_rate.Text);
                        }
                        int tibu_rate = 0;
                        if (textBox_tibu_rate.Text != "")
                        {
                            tibu_rate = int.Parse(textBox_tibu_rate.Text);
                        }
                        mv.craft_edit_view.dataGridView1.RowCount = mv.craft_edit_view.dataGridView1.RowCount + 1;  //新增一行
                        for (int i = 0; i < mv.craft_edit_view.dataGridView1.RowCount; i++)
                        {
                            mv.craft_edit_view.dataGridView1[0, i].Value = i + 1;
                        }
                        for (int i = mv.craft_edit_view.dataGridView1.RowCount - 2; i >= start_index; i--)
                        {
                            for (int j = 1; j < mv.craft_edit_view.dataGridView1.ColumnCount; j++)
                            {
                                mv.craft_edit_view.dataGridView1[j, i + 1].Value = mv.craft_edit_view.dataGridView1[j, i].Value;
                            }
                        }

                        mv.craft_edit_view.dataGridView1[1, start_index].Value = shuiwei;
                        mv.craft_edit_view.dataGridView1[2, start_index].Value = speed;
                        mv.craft_edit_view.dataGridView1[3, start_index].Value = time;
                        mv.craft_edit_view.dataGridView1[4, start_index].Value = craft;
                        mv.craft_edit_view.dataGridView1[5, start_index].Value = rate;
                        mv.craft_edit_view.dataGridView1[6, start_index].Value = fengji_rate;
                        mv.craft_edit_view.dataGridView1[7, start_index].Value = tibu_rate;
                        mv.craft_edit_view.draw_line();
                        this.Dispose();
                    }
                    catch
                    {
                    }
                }
            }
            if (radioButton_craft_name.Checked == true)
            {
                //为名字
                if (operation_kind == 1)
                {
                    try
                    {
                        DataSet ds = mv.mysqlconnection.sql_search_database("select * from craft_machine" + operation_machine_num.ToString());
                        int row_count = ds.Tables[0].Rows.Count;
                        DataRow craftrow = null;     //最后一行
                        if (row_count > 0) craftrow = ds.Tables[0].Rows[row_count - 1];
                        ds = mv.mysqlconnection.sql_search_database("select craft_code from craftcode where craft_name='" + comboBox_craftlist.Text + "'");
                        DataRow dr = ds.Tables[0].Rows[0];
                        int code = int.Parse(dr[0].ToString());
                        int xuhao = row_count + 1;
                        int shuiwei = int.Parse(textBox_qishiwendu.Text);
                        string craft = comboBox_craftlist.Text;
                        float speed = 0;
                        int time = 0;
                        if (textBox_time.Text != "")
                        {
                            time = int.Parse(textBox_time.Text);
                        }
                        int rate = 0;
                        if (textBox1_rate.Text != "")
                        {
                            rate = int.Parse(textBox1_rate.Text);
                        }
                        int fengji_rate = 0;
                        if (textBox_fengji_rate.Text != "")
                        {
                            fengji_rate = int.Parse(textBox_fengji_rate.Text);
                        }
                        int tibu_rate = 0;
                        if (textBox_tibu_rate.Text != "")
                        {
                            tibu_rate = int.Parse(textBox_tibu_rate.Text);
                        }
                        mv.mysqlconnection.excute_sql("insert into craft_machine" + operation_machine_num.ToString() + "(wendu_shuiwei,speed,time,craft,nvarcharcode,rate,fengji_rate,tibu_rate) values ('" + shuiwei.ToString() + "','" + speed.ToString() + "','" + time.ToString() + "','" + craft + "','" + code.ToString() + "','" + rate.ToString() + "','" + fengji_rate.ToString() + "','" + tibu_rate.ToString() + "')");
                        mv.detail.ReFlash_Craft_Chart();
                        mv.detail.updata_datagridview();
                        mv.detail.draw_line();
                        this.Dispose();
                    }
                    catch
                    {
                    }
                }
                if (operation_kind == 2)
                {
                    try
                    {
                        DataSet ds = mv.mysqlconnection.sql_search_database("select * from craft" + operation_machine_num.ToString());
                        int row_count = ds.Tables[0].Rows.Count;
                        DataRow craftrow = null;     //最后一行
                        if (row_count > 0) craftrow = ds.Tables[0].Rows[row_count - 1];
                        ds = mv.mysqlconnection.sql_search_database("select craft_code from craftcode where craft_name='" + comboBox_craftlist.Text + "'");
                        DataRow dr = ds.Tables[0].Rows[0];
                        int code = int.Parse(dr[0].ToString());
                        int xuhao = row_count + 1;
                        int shuiwei = int.Parse(textBox_qishiwendu.Text);
                        string craft = comboBox_craftlist.Text;
                        float speed = 0;
                        int time = 0;
                        if (textBox_time.Text != "")
                        {
                            time = int.Parse(textBox_time.Text);
                        }
                        int rate = 0;
                        if (textBox1_rate.Text != "")
                        {
                            rate = int.Parse(textBox1_rate.Text);
                        }
                        int fengji_rate = 0;
                        if (textBox_fengji_rate.Text != "")
                        {
                            fengji_rate = int.Parse(textBox_fengji_rate.Text);
                        }
                        int tibu_rate = 0;
                        if (textBox_tibu_rate.Text != "")
                        {
                            tibu_rate = int.Parse(textBox_tibu_rate.Text);
                        }
                        mv.mysqlconnection.excute_sql("insert into craft" + operation_machine_num.ToString() + "(wendu_shuiwei,speed,time,craft,nvarcharcode,rate,fengji_rate,tibu_rate) values ('" + shuiwei.ToString() + "','" + speed.ToString() + "','" + time.ToString() + "','" + craft + "','" + code.ToString() + "','" + rate.ToString() + "','" + fengji_rate.ToString() + "','" + tibu_rate.ToString() + "')");
                        mv.craft_edit_view.ReFlash_Craft_Chart();
                        mv.craft_edit_view.updata_datagridview();
                        mv.craft_edit_view.draw_line();
                        this.Dispose();
                    }
                    catch
                    {
                    }
                }
                if (operation_kind == 3)
                {
                    // 新建一行，并依次往后面排
                    try
                    {

                        // 获取工艺号
                        //DataSet ds = mv.mysqlconnection.sql_search_database("select craft_code from craftcode where craft_name='"+comboBox_shuixi.Text +"'");
                        //DataRow dr = ds.Tables[0].Rows[0];
                        //int code = int.Parse(dr[0].ToString());

                        int shuiwei = int.Parse(textBox_qishiwendu.Text);
                        string craft = comboBox_craftlist.Text;
                        if (craft == "") return;
                        float speed = 0;
                        if (textBox_sulv.Text != "")
                        {
                            speed = float.Parse(textBox_sulv.Text);
                        }
                        int time = 0;
                        if (textBox_time.Text != "")
                        {
                            time = int.Parse(textBox_time.Text);
                        }
                        int rate = 0;
                        if (textBox1_rate.Text != "")
                        {
                            rate = int.Parse(textBox1_rate.Text);
                        }
                        int fengji_rate = 0;
                        if (textBox_fengji_rate.Text != "")
                        {
                            fengji_rate = int.Parse(textBox_fengji_rate.Text);
                        }
                        int tibu_rate = 0;
                        if (textBox_tibu_rate.Text != "")
                        {
                            tibu_rate = int.Parse(textBox_tibu_rate.Text);
                        }
                        mv.detail.dataGridView1.RowCount = mv.detail.dataGridView1.RowCount + 1;  //新增一行
                        for (int i = 0; i < mv.detail.dataGridView1.RowCount; i++)
                        {
                            mv.detail.dataGridView1[0, i].Value = i + 1;
                        }
                        for (int i = mv.detail.dataGridView1.RowCount - 2; i >= start_index; i--)
                        {
                            for (int j = 1; j < mv.detail.dataGridView1.ColumnCount; j++)
                            {
                                mv.detail.dataGridView1[j, i + 1].Value = mv.detail.dataGridView1[j, i].Value;
                            }
                        }

                        mv.detail.dataGridView1[1, start_index].Value = shuiwei;
                        mv.detail.dataGridView1[2, start_index].Value = speed;
                        mv.detail.dataGridView1[3, start_index].Value = time;
                        mv.detail.dataGridView1[4, start_index].Value = craft;
                        mv.detail.dataGridView1[5, start_index].Value = rate;
                        mv.detail.dataGridView1[6, start_index].Value = fengji_rate;
                        mv.detail.dataGridView1[7, start_index].Value = tibu_rate;
                        mv.detail.draw_line();
                        this.Dispose();
                    }
                    catch
                    {
                    }
                }
                if (operation_kind == 4)
                {
                    // 新建一行，并依次往后面排
                    try
                    {

                        // 获取工艺号
                        //DataSet ds = mv.mysqlconnection.sql_search_database("select craft_code from craftcode where craft_name='"+comboBox_shuixi.Text +"'");
                        //DataRow dr = ds.Tables[0].Rows[0];
                        //int code = int.Parse(dr[0].ToString());

                        int shuiwei = int.Parse(textBox_qishiwendu.Text);
                        string craft = comboBox_craftlist.Text;
                        if (craft == "") return;
                        float speed = 0;
                        if (textBox_sulv.Text != "")
                        {
                            speed = float.Parse(textBox_sulv.Text);
                        }
                        int time = 0;
                        if (textBox_time.Text != "")
                        {
                            time = int.Parse(textBox_time.Text);
                        }
                        int rate = 0;
                        if (textBox1_rate.Text != "")
                        {
                            rate = int.Parse(textBox1_rate.Text);
                        }
                        int fengji_rate = 0;
                        if (textBox_fengji_rate.Text != "")
                        {
                            fengji_rate = int.Parse(textBox_fengji_rate.Text);
                        }
                        int tibu_rate = 0;
                        if (textBox_tibu_rate.Text != "")
                        {
                            tibu_rate = int.Parse(textBox_tibu_rate.Text);
                        }
                        mv.craft_edit_view.dataGridView1.RowCount = mv.craft_edit_view.dataGridView1.RowCount + 1;  //新增一行
                        for (int i = 0; i < mv.craft_edit_view.dataGridView1.RowCount; i++)
                        {
                            mv.detail.dataGridView1[0, i].Value = i + 1;
                        }
                        for (int i = mv.craft_edit_view.dataGridView1.RowCount - 2; i >= start_index; i--)
                        {
                            for (int j = 1; j < mv.craft_edit_view.dataGridView1.ColumnCount; j++)
                            {
                                mv.craft_edit_view.dataGridView1[j, i + 1].Value = mv.craft_edit_view.dataGridView1[j, i].Value;
                            }
                        }

                        mv.craft_edit_view.dataGridView1[1, start_index].Value = shuiwei;
                        mv.craft_edit_view.dataGridView1[2, start_index].Value = speed;
                        mv.craft_edit_view.dataGridView1[3, start_index].Value = time;
                        mv.craft_edit_view.dataGridView1[4, start_index].Value = craft;
                        mv.craft_edit_view.dataGridView1[5, start_index].Value = rate;
                        mv.craft_edit_view.dataGridView1[6, start_index].Value = fengji_rate;
                        mv.craft_edit_view.dataGridView1[7, start_index].Value = tibu_rate;
                        mv.craft_edit_view.draw_line();
                        this.Dispose();
                    }
                    catch
                    {
                    }
                }
            }
            if (radioButton_craft_num.Checked == true)
            {
                //为数字
                if (operation_kind == 1)
                {
                    try
                    {
                        DataSet ds = mv.mysqlconnection.sql_search_database("select * from craft_machine" + operation_machine_num.ToString());
                        int row_count = ds.Tables[0].Rows.Count;
                        DataRow craftrow = null;     //最后一行
                        if (row_count > 0) craftrow = ds.Tables[0].Rows[row_count - 1];
                        ds = mv.mysqlconnection.sql_search_database("select craft_code from craftcode where craft_name='" + comboBox_craftlist.Text + "'");
                        //DataRow dr = ds.Tables[0].Rows[0];
                        int code = int.Parse(textBox_code.Text);
                        int xuhao = row_count + 1;
                        int shuiwei = int.Parse(textBox_qishiwendu.Text);
                        string craft = textBox_code.Text;
                        float speed = 0;
                        int time = 0;
                        if (textBox_time.Text != "")
                        {
                            time = int.Parse(textBox_time.Text);
                        }
                        int rate = 0;
                        if (textBox1_rate.Text != "")
                        {
                            rate = int.Parse(textBox1_rate.Text);
                        }
                        int fengji_rate = 0;
                        if (textBox_fengji_rate.Text != "")
                        {
                            fengji_rate = int.Parse(textBox_fengji_rate.Text);
                        }
                        int tibu_rate = 0;
                        if (textBox_tibu_rate.Text != "")
                        {
                            tibu_rate = int.Parse(textBox_tibu_rate.Text);
                        }
                        mv.mysqlconnection.excute_sql("insert into craft_machine" + operation_machine_num.ToString() + "(wendu_shuiwei,speed,time,craft,nvarcharcode,rate,fengji_rate,tibu_rate) values ('" + shuiwei.ToString() + "','" + speed.ToString() + "','" + time.ToString() + "','" + craft + "','" + code.ToString() + "','" + rate.ToString() + "','" + fengji_rate.ToString() + "','" + tibu_rate.ToString() + "')");
                        mv.detail.ReFlash_Craft_Chart();
                        mv.detail.updata_datagridview();
                        mv.detail.draw_line();
                        this.Dispose();
                    }
                    catch
                    {
                    }
                }
                if (operation_kind == 2)
                {
                    try
                    {
                        DataSet ds = mv.mysqlconnection.sql_search_database("select * from craft" + operation_machine_num.ToString());
                        int row_count = ds.Tables[0].Rows.Count;
                        DataRow craftrow = null;     //最后一行
                        if (row_count > 0) craftrow = ds.Tables[0].Rows[row_count - 1];
                        ds = mv.mysqlconnection.sql_search_database("select craft_code from craftcode where craft_name='" + comboBox_craftlist.Text + "'");
                        //DataRow dr = ds.Tables[0].Rows[0];
                        int code = int.Parse(textBox_code.Text);
                        int xuhao = row_count + 1;
                        int shuiwei = int.Parse(textBox_qishiwendu.Text);
                        string craft = textBox_code.Text;
                        float speed = 0;
                        int time = 0;
                        if (textBox_time.Text != "")
                        {
                            time = int.Parse(textBox_time.Text);
                        }
                        int rate = 0;
                        if (textBox1_rate.Text != "")
                        {
                            rate = int.Parse(textBox1_rate.Text);
                        }
                        int fengji_rate = 0;
                        if (textBox_fengji_rate.Text != "")
                        {
                            fengji_rate = int.Parse(textBox_fengji_rate.Text);
                        }
                        int tibu_rate = 0;
                        if (textBox_tibu_rate.Text != "")
                        {
                            tibu_rate = int.Parse(textBox_tibu_rate.Text);
                        }
                        mv.mysqlconnection.excute_sql("insert into craft" + operation_machine_num.ToString() + "(wendu_shuiwei,speed,time,craft,nvarcharcode,rate,fengji_rate,tibu_rate) values ('" + shuiwei.ToString() + "','" + speed.ToString() + "','" + time.ToString() + "','" + craft + "','" + code.ToString() + "','" + rate.ToString() + "','" + fengji_rate.ToString() + "','" + tibu_rate.ToString() + "')");
                        mv.craft_edit_view.ReFlash_Craft_Chart();
                        mv.craft_edit_view.updata_datagridview();
                        mv.craft_edit_view.draw_line();
                        this.Dispose();
                    }
                    catch
                    {
                    }
                }
                if (operation_kind == 3)
                {
                    // 新建一行，并依次往后面排
                    try
                    {

                        // 获取工艺号
                        //DataSet ds = mv.mysqlconnection.sql_search_database("select craft_code from craftcode where craft_name='"+comboBox_shuixi.Text +"'");
                        //DataRow dr = ds.Tables[0].Rows[0];
                        //int code = int.Parse(dr[0].ToString());

                        int shuiwei = int.Parse(textBox_qishiwendu.Text);
                        string craft = textBox_code.Text;
                        if (craft == "") return;
                        float speed = 0;
                        if (textBox_sulv.Text != "")
                        {
                            speed = float.Parse(textBox_sulv.Text);
                        }
                        int time = 0;
                        if (textBox_time.Text != "")
                        {
                            time = int.Parse(textBox_time.Text);
                        }
                        int rate = 0;
                        if (textBox1_rate.Text != "")
                        {
                            rate = int.Parse(textBox1_rate.Text);
                        }
                        int fengji_rate = 0;
                        if (textBox_fengji_rate.Text != "")
                        {
                            fengji_rate = int.Parse(textBox_fengji_rate.Text);
                        }
                        int tibu_rate = 0;
                        if (textBox_tibu_rate.Text != "")
                        {
                            tibu_rate = int.Parse(textBox_tibu_rate.Text);
                        }
                        mv.detail.dataGridView1.RowCount = mv.detail.dataGridView1.RowCount + 1;  //新增一行
                        for (int i = 0; i < mv.detail.dataGridView1.RowCount; i++)
                        {
                            mv.detail.dataGridView1[0, i].Value = i + 1;
                        }
                        for (int i = mv.detail.dataGridView1.RowCount - 2; i >= start_index; i--)
                        {
                            for (int j = 1; j < mv.detail.dataGridView1.ColumnCount; j++)
                            {
                                mv.detail.dataGridView1[j, i + 1].Value = mv.detail.dataGridView1[j, i].Value;
                            }
                        }

                        mv.detail.dataGridView1[1, start_index].Value = shuiwei;
                        mv.detail.dataGridView1[2, start_index].Value = speed;
                        mv.detail.dataGridView1[3, start_index].Value = time;
                        mv.detail.dataGridView1[4, start_index].Value = craft;
                        mv.detail.dataGridView1[5, start_index].Value = rate;
                        mv.detail.dataGridView1[6, start_index].Value = fengji_rate;
                        mv.detail.dataGridView1[7, start_index].Value = tibu_rate;
                        mv.detail.draw_line();
                        this.Dispose();
                    }
                    catch
                    {
                    }
                }
                if (operation_kind == 4)
                {
                    // 新建一行，并依次往后面排
                    try
                    {

                        // 获取工艺号
                        //DataSet ds = mv.mysqlconnection.sql_search_database("select craft_code from craftcode where craft_name='"+comboBox_shuixi.Text +"'");
                        //DataRow dr = ds.Tables[0].Rows[0];
                        //int code = int.Parse(dr[0].ToString());

                        int shuiwei = int.Parse(textBox_qishiwendu.Text);
                        string craft = textBox_code.Text;
                        if (craft == "") return;
                        float speed = 0;
                        if (textBox_sulv.Text != "")
                        {
                            speed = float.Parse(textBox_sulv.Text);
                        }
                        int time = 0;
                        if (textBox_time.Text != "")
                        {
                            time = int.Parse(textBox_time.Text);
                        }
                        int rate = 0;
                        if (textBox1_rate.Text != "")
                        {
                            rate = int.Parse(textBox1_rate.Text);
                        }
                        int fengji_rate = 0;
                        if (textBox_fengji_rate.Text != "")
                        {
                            fengji_rate = int.Parse(textBox_fengji_rate.Text);
                        }
                        int tibu_rate = 0;
                        if (textBox_tibu_rate.Text != "")
                        {
                            tibu_rate = int.Parse(textBox_tibu_rate.Text);
                        }
                        mv.craft_edit_view.dataGridView1.RowCount = mv.craft_edit_view.dataGridView1.RowCount + 1;  //新增一行
                        for (int i = 0; i < mv.craft_edit_view.dataGridView1.RowCount; i++)
                        {
                            mv.craft_edit_view.dataGridView1[0, i].Value = i + 1;
                        }
                        for (int i = mv.craft_edit_view.dataGridView1.RowCount - 2; i >= start_index; i--)
                        {
                            for (int j = 1; j < mv.craft_edit_view.dataGridView1.ColumnCount; j++)
                            {
                                mv.craft_edit_view.dataGridView1[j, i + 1].Value = mv.craft_edit_view.dataGridView1[j, i].Value;
                            }
                        }

                        mv.craft_edit_view.dataGridView1[1, start_index].Value = shuiwei;
                        mv.craft_edit_view.dataGridView1[2, start_index].Value = speed;
                        mv.craft_edit_view.dataGridView1[3, start_index].Value = time;
                        mv.craft_edit_view.dataGridView1[4, start_index].Value = craft;
                        mv.craft_edit_view.dataGridView1[5, start_index].Value = rate;
                        mv.craft_edit_view.dataGridView1[6, start_index].Value = fengji_rate;
                        mv.craft_edit_view.dataGridView1[7, start_index].Value = tibu_rate;
                        mv.craft_edit_view.draw_line();
                        this.Dispose();
                    }
                    catch
                    {
                    }
                }
            }
            
            // 操作种类1 全部为远程的操作
            //if (radioButton3.Checked == true)
            //{
            //    // 其他操作
            //    if (operation_kind == 1)
            //    {
            //        try
            //        {
            //            DataSet ds = mv.mysqlconnection.sql_search_database("select * from craft_machine" + operation_machine_num.ToString());
            //            int row_count = ds.Tables[0].Rows.Count;
            //            DataRow craftrow = null;     //最后一行
            //            if (row_count > 0) craftrow = ds.Tables[0].Rows[row_count - 1];
            //            // 寻找温控的工艺代码
            //            ds = mv.mysqlconnection.sql_search_database("select craft_code from craftcode where craft_name='"+comboBox_qita.Text+"'");
            //            DataRow dr = ds.Tables[0].Rows[0];
            //            int code = int.Parse(dr[0].ToString());
            //            //加入水控一行

            //            int xuhao = row_count + 1;
            //            int shuiwei =0;
            //            string craft = comboBox_qita.Text;
            //            float speed = 0;
            //            int time = 0;
            //            int rate = 0;
            //            if (textBox1_rate.Text != "")
            //            {
            //                rate = int.Parse(textBox1_rate.Text);
            //            }
            //            int fengji_rate = 0;
            //            if (textBox_fengji_rate.Text != "")
            //            {
            //                fengji_rate = int.Parse(textBox_fengji_rate.Text);
            //            }
            //            int tibu_rate = 0;
            //            if (textBox_tibu_rate.Text != "")
            //            {
            //                tibu_rate = int.Parse(textBox_tibu_rate.Text);
            //            }
            //            mv.mysqlconnection.excute_sql("insert into craft_machine" + operation_machine_num.ToString() + "(wendu_shuiwei,speed,time,craft,nvarcharcode,rate,fengji_rate,tibu_rate) values ('" + shuiwei.ToString() + "','" + speed.ToString() + "','" + time.ToString() + "','" + craft + "','" + code.ToString() + "','" + rate.ToString() + "','" + fengji_rate.ToString() + "','" + tibu_rate.ToString() + "')");
            //            mv.detail.ReFlash_Craft_Chart();
            //            mv.detail.updata_datagridview();
            //            mv.detail.draw_line();
            //            this.Dispose();
            //        }
            //        catch
            //        {

            //        }
            //    }
            //    if (operation_kind == 2)
            //    {
            //        try
            //        {
            //            DataSet ds = mv.mysqlconnection.sql_search_database("select * from craft" + operation_machine_num.ToString());
            //            int row_count = ds.Tables[0].Rows.Count;
            //            DataRow craftrow = null;     //最后一行
            //            if (row_count > 0) craftrow = ds.Tables[0].Rows[row_count - 1];
            //            // 寻找温控的工艺代码
            //            ds = mv.mysqlconnection.sql_search_database("select craft_code from craftcode where craft_name='" + comboBox_qita.Text + "'");
            //            DataRow dr = ds.Tables[0].Rows[0];
            //            int code = int.Parse(dr[0].ToString());
            //            //加入水控一行

            //            int xuhao = row_count + 1;
            //            int shuiwei = 0;
            //            string craft = comboBox_qita.Text;
            //            float speed = 0;
            //            int time = 0;
            //            int rate = 0;
            //            if (textBox1_rate.Text != "")
            //            {
            //                rate = int.Parse(textBox1_rate.Text);
            //            }
            //            int fengji_rate = 0;
            //            if (textBox_fengji_rate.Text != "")
            //            {
            //                fengji_rate = int.Parse(textBox_fengji_rate.Text);
            //            }
            //            int tibu_rate = 0;
            //            if (textBox_tibu_rate.Text != "")
            //            {
            //                tibu_rate = int.Parse(textBox_tibu_rate.Text);
            //            }
            //            mv.mysqlconnection.excute_sql("insert into craft" + operation_machine_num.ToString() + "(wendu_shuiwei,speed,time,craft,nvarcharcode,rate,fengji_rate,tibu_rate) values ('" + shuiwei.ToString() + "','" + speed.ToString() + "','" + time.ToString() + "','" + craft + "','" + code.ToString() + "','" + rate.ToString() + "','" + fengji_rate.ToString() + "','" + tibu_rate.ToString() + "')");
            //            mv.craft_edit_view.ReFlash_Craft_Chart();
            //            mv.craft_edit_view.updata_datagridview();
            //            mv.craft_edit_view.draw_line();
            //            this.Dispose();
            //        }
            //        catch
            //        {

            //        }
            //    }
            //}
            //if (radioButton2.Checked == true)
            //{
            //    // 水控操作
            //    if (operation_kind == 1)
            //    {
            //        try
            //        {
            //            DataSet ds = mv.mysqlconnection.sql_search_database("select * from craft_machine" + operation_machine_num.ToString());
            //            int row_count = ds.Tables[0].Rows.Count;
            //            DataRow craftrow = null;     //最后一行
            //            if (row_count > 0) craftrow = ds.Tables[0].Rows[row_count - 1];
            //            // 寻找温控的工艺代码
            //            ds = mv.mysqlconnection.sql_search_database("select craft_code from craftcode where craft_name='"+comboBox_shuiwei.Text +"'");
            //            DataRow dr = ds.Tables[0].Rows[0];
            //            int code = int.Parse(dr[0].ToString());
            //            //加入水控一行

            //            int xuhao = row_count + 1;
            //            int shuiwei = int.Parse(textBox_shuiwei.Text);
            //            string craft = comboBox_shuiwei.Text;
            //            float speed = 0;
            //            int time = 0;
            //            int rate = 0;
            //            if (textBox1_rate.Text != "")
            //            {
            //                rate = int.Parse(textBox1_rate.Text);
            //            }
            //            int fengji_rate = 0;
            //            if (textBox_fengji_rate.Text!= "")
            //            {
            //                fengji_rate = int.Parse(textBox_fengji_rate.Text);
            //            }
            //            int tibu_rate = 0;
            //            if (textBox_tibu_rate.Text != "")
            //            {
            //                tibu_rate = int.Parse(textBox_tibu_rate.Text);
            //            }
            //            mv.mysqlconnection.excute_sql("insert into craft_machine" + operation_machine_num.ToString() + "(wendu_shuiwei,speed,time,craft,nvarcharcode,rate,fengji_rate,tibu_rate) values ('" + shuiwei.ToString() + "','" + speed.ToString() + "','" + time.ToString() + "','" + craft + "','" + code.ToString() + "','" + rate.ToString() + "','" + fengji_rate.ToString() + "','" + tibu_rate.ToString() + "')");
            //            mv.detail.ReFlash_Craft_Chart();
            //            mv.detail.updata_datagridview();
            //            mv.detail.draw_line();
            //            this.Dispose();

            //        }
            //        catch
            //        {
            //        }
            //    }
            //    // 水控操作
            //    if (operation_kind == 2)
            //    {
            //        try
            //        {
            //            DataSet ds = mv.mysqlconnection.sql_search_database("select * from craft" + operation_machine_num.ToString());
            //            int row_count = ds.Tables[0].Rows.Count;
            //            DataRow craftrow = null;     //最后一行
            //            if (row_count > 0) craftrow = ds.Tables[0].Rows[row_count - 1];
            //            // 寻找温控的工艺代码
            //            ds = mv.mysqlconnection.sql_search_database("select craft_code from craftcode where craft_name='"+comboBox_shuiwei.Text +"'");
            //            DataRow dr = ds.Tables[0].Rows[0];
            //            int code = int.Parse(dr[0].ToString());
            //            //加入水控一行

            //            int xuhao = row_count + 1;
            //            int shuiwei = int.Parse(textBox_shuiwei.Text);
            //            string craft = comboBox_shuiwei.Text;
            //            float speed = 0;
            //            int time = 0;
            //            int rate = 0;
            //            if (textBox1_rate.Text != "")
            //            {
            //                rate = int.Parse(textBox1_rate.Text);
            //            }
            //            int fengji_rate = 0;
            //            if (textBox_fengji_rate.Text != "")
            //            {
            //                fengji_rate = int.Parse(textBox_fengji_rate.Text);
            //            }
            //            int tibu_rate = 0;
            //            if (textBox_tibu_rate.Text != "")
            //            {
            //                tibu_rate = int.Parse(textBox_tibu_rate.Text);
            //            }
            //            mv.mysqlconnection.excute_sql("insert into craft" + operation_machine_num.ToString() + "(wendu_shuiwei,speed,time,craft,nvarcharcode,rate,fengji_rate,tibu_rate) values ('" + shuiwei.ToString() + "','" + speed.ToString() + "','" + time.ToString() + "','" + craft + "','" + code.ToString() + "','" + rate.ToString() + "','" + fengji_rate.ToString() + "','" + tibu_rate.ToString() + "')");
            //            mv.craft_edit_view.ReFlash_Craft_Chart();
            //            mv.craft_edit_view.updata_datagridview();
            //            mv.craft_edit_view.draw_line();
            //            this.Dispose();

            //        }
            //        catch
            //        {
            //        }
            //    }
            //}
            //if (radioButton1.Checked == true)
            //{
            //    //温控操作
            //    if (operation_kind == 1)
            //    {
            //        // 添加操作
            //        try
            //        {
            //            DataSet ds = mv.mysqlconnection.sql_search_database("select * from craft_machine" + operation_machine_num.ToString());
            //            int row_count = ds.Tables[0].Rows.Count;
            //            DataRow craftrow=null;     //最后一行
            //            if (row_count > 0) craftrow = ds.Tables[0].Rows[row_count - 1];
            //            // 寻找温控的工艺代码
            //            ds = mv.mysqlconnection.sql_search_database("select craft_code from craftcode where craft_name='温控'");
            //            DataRow dr = ds.Tables[0].Rows[0];
            //            int code = int.Parse(dr[0].ToString());
                        
            //            //控制第一行不能为保温
            //            if (row_count == 0 && comboBox_wenkong_select.Text == "保温")    //第一行不能加保温
            //            {
            //                MessageBox.Show("工艺第一步不能为保温!");
            //                return;
            //            }
            //            else if(row_count>0 && comboBox_wenkong_select.Text =="保温")
            //            {
            //                if (craftrow[4].ToString() != "温控")
            //                {
            //                    MessageBox.Show("保温上一步必须为温控!");
            //                    return;
            //                }
            //            }
            //            if (comboBox_wenkong_select.Text == "保温")
            //            {
            //                // 加入保温一行
            //                int xuhao = row_count + 1;  //序号
            //                int wendu = int.Parse(textBox_qishiwendu.Text);
            //                float speed = 0;
            //                int time = int.Parse(textBox_time.Text);
            //                string craft = "温控";
            //                // 主泵频率
            //                int rate = 0;
            //                if (textBox1_rate.Text != "")
            //                {
            //                    rate = int.Parse(textBox1_rate.Text);
            //                }
            //                else
            //                {
            //                    rate = 0;
            //                }
            //                // 风机频率
            //                int fengji_rate = 0;
            //                if (textBox_fengji_rate.Text != "")
            //                {
            //                    fengji_rate = int.Parse(textBox_fengji_rate.Text);
            //                }
            //                else
            //                {
            //                    fengji_rate = 0;
            //                }
            //                // 提布频率
            //                int tibu_rate = 0;
            //                if (textBox_tibu_rate.Text != "")
            //                {
            //                    tibu_rate = int.Parse(textBox_tibu_rate.Text);
            //                }
            //                else
            //                {
            //                    tibu_rate = 0;
            //                }
            //                //mv.mysqlconnection.excute_sql("insert into craft_machine" + operation_machine_num.ToString() + " values (wendu_shuiwei='" + wendu.ToString() + "',speed='" + speed.ToString() + "',time='" + time.ToString() + "',craft='" + craft + "',nvarcharcode='" + code.ToString() + "',rate='" + rate.ToString() + "',fengji_rate='" + fengji_rate.ToString() + "',tibu_rate='" + tibu_rate.ToString() + "')");
            //                mv.mysqlconnection.excute_sql("insert into craft_machine" + operation_machine_num.ToString() + "(wendu_shuiwei,speed,time,craft,nvarcharcode,rate,fengji_rate,tibu_rate) values ('" + wendu.ToString() + "','" + speed.ToString() + "','" + time.ToString() + "','" + craft + "','" + code.ToString() + "','" + rate.ToString() + "','" + fengji_rate.ToString() + "','" + tibu_rate.ToString() + "')");
            //                mv.detail.ReFlash_Craft_Chart();
            //                mv.detail.updata_datagridview();
            //                mv.detail.draw_line();
            //                this.Dispose();
            //            }
            //            if (comboBox_wenkong_select.Text == "升温/降温")
            //            {
            //                // 加入温控一行
            //                int xuhao = row_count + 1;  //序号
            //                //当有起始温度时
            //                int wendu = int.Parse(textBox_qishiwendu.Text);
            //                float speed =float.Parse(textBox_sulv.Text);
            //                int time = 0;
            //                string craft = "温控";
            //                // 主泵频率
            //                int rate = 0;
            //                if (textBox1_rate.Text != "")
            //                {
            //                    rate = int.Parse(textBox1_rate.Text);
            //                }
            //                else
            //                {
            //                    rate = 0;
            //                }
            //                // 风机频率
            //                int fengji_rate = 0;
            //                if (textBox_fengji_rate.Text != "")
            //                {
            //                    fengji_rate = int.Parse(textBox_fengji_rate.Text);
            //                }
            //                else
            //                {
            //                    fengji_rate = 0;
            //                }
            //                // 提布频率
            //                int tibu_rate = 0;
            //                if (textBox_tibu_rate.Text != "")
            //                {
            //                    tibu_rate = int.Parse(textBox_tibu_rate.Text);
            //                }
            //                else
            //                {
            //                    tibu_rate = 0;
            //                }
            //                //mv.mysqlconnection.excute_sql("insert into craft_machine" + operation_machine_num.ToString() + " values ('" + xuhao.ToString() + "','" + wendu.ToString() + "','" + speed.ToString() + "','" + time.ToString() + "','" + craft + "','" + code.ToString() + "','" + rate.ToString() + "','"+fengji_rate.ToString()+"','"+tibu_rate.ToString()+"')");
            //                mv.mysqlconnection.excute_sql("insert into craft_machine" + operation_machine_num.ToString() + "(wendu_shuiwei,speed,time,craft,nvarcharcode,rate,fengji_rate,tibu_rate) values ('" + wendu.ToString() + "','" + speed.ToString() + "','" + time.ToString() + "','" + craft + "','" + code.ToString() + "','" + rate.ToString() + "','" + fengji_rate.ToString() + "','" + tibu_rate.ToString() + "')");
            //                mv.detail.ReFlash_Craft_Chart();
            //                mv.detail.updata_datagridview();
            //                mv.detail.draw_line();
            //                this.Dispose();
            //            }
            //        }
            //        catch
            //        {
            //        }
            //    }
            //    if (operation_kind == 2)
            //    {
            //        // 添加操作
            //        try
            //        {
            //            DataSet ds = mv.mysqlconnection.sql_search_database("select * from craft" + operation_machine_num.ToString());
            //            int row_count = ds.Tables[0].Rows.Count;
            //            DataRow craftrow = null;     //最后一行
            //            if (row_count > 0) craftrow = ds.Tables[0].Rows[row_count - 1];
            //            // 寻找温控的工艺代码
            //            ds = mv.mysqlconnection.sql_search_database("select craft_code from craftcode where craft_name='温控'");
            //            DataRow dr = ds.Tables[0].Rows[0];
            //            int code = int.Parse(dr[0].ToString());

            //            //控制第一行不能为保温
            //            if (row_count == 0 && comboBox_wenkong_select.Text == "保温")    //第一行不能加保温
            //            {
            //                MessageBox.Show("工艺第一步不能为保温!");
            //                return;
            //            }
            //            else if (row_count > 0 && comboBox_wenkong_select.Text == "保温")
            //            {
            //                if (craftrow[4].ToString() != "温控")
            //                {
            //                    MessageBox.Show("保温上一步必须为温控!");
            //                    return;
            //                }
            //            }
            //            if (comboBox_wenkong_select.Text == "保温")
            //            {
            //                // 加入保温一行
            //                int xuhao = row_count + 1;  //序号
            //                int wendu = int.Parse(textBox_qishiwendu.Text);
            //                float speed = 0;
            //                int time = int.Parse(textBox_time.Text);
            //                string craft = "温控";
            //                // 主泵频率
            //                int rate = 0;
            //                if (textBox1_rate.Text != "")
            //                {
            //                    rate = int.Parse(textBox1_rate.Text);
            //                }
            //                else
            //                {
            //                    rate = 0;
            //                }
            //                // 风机频率
            //                int fengji_rate = 0;
            //                if (textBox_fengji_rate.Text != "")
            //                {
            //                    fengji_rate = int.Parse(textBox_fengji_rate.Text);
            //                }
            //                else
            //                {
            //                    fengji_rate = 0;
            //                }
            //                // 提布频率
            //                int tibu_rate = 0;
            //                if (textBox_tibu_rate.Text != "")
            //                {
            //                    tibu_rate = int.Parse(textBox_tibu_rate.Text);
            //                }
            //                else
            //                {
            //                    tibu_rate = 0;
            //                }
            //                //mv.mysqlconnection.excute_sql("insert into craft_machine" + operation_machine_num.ToString() + " values (wendu_shuiwei='" + wendu.ToString() + "',speed='" + speed.ToString() + "',time='" + time.ToString() + "',craft='" + craft + "',nvarcharcode='" + code.ToString() + "',rate='" + rate.ToString() + "',fengji_rate='" + fengji_rate.ToString() + "',tibu_rate='" + tibu_rate.ToString() + "')");
            //                mv.mysqlconnection.excute_sql("insert into craft" + operation_machine_num.ToString() + "(wendu_shuiwei,speed,time,craft,nvarcharcode,rate,fengji_rate,tibu_rate) values ('" + wendu.ToString() + "','" + speed.ToString() + "','" + time.ToString() + "','" + craft + "','" + code.ToString() + "','" + rate.ToString() + "','" + fengji_rate.ToString() + "','" + tibu_rate.ToString() + "')");
            //                mv.craft_edit_view.ReFlash_Craft_Chart();
            //                //
            //                mv.craft_edit_view.updata_datagridview();
            //                mv.craft_edit_view.draw_line();
            //                this.Dispose();
            //            }
            //            if (comboBox_wenkong_select.Text == "升温/降温")
            //            {
            //                // 加入温控一行
            //                int xuhao = row_count + 1;  //序号
            //                //当有起始温度时
            //                int wendu = int.Parse(textBox_qishiwendu.Text);
            //                float speed = float.Parse(textBox_sulv.Text);
            //                int time = 0;
            //                string craft = "温控";
            //                // 主泵频率
            //                int rate = 0;
            //                if (textBox1_rate.Text != "")
            //                {
            //                    rate = int.Parse(textBox1_rate.Text);
            //                }
            //                else
            //                {
            //                    rate = 0;
            //                }
            //                // 风机频率
            //                int fengji_rate = 0;
            //                if (textBox_fengji_rate.Text != "")
            //                {
            //                    fengji_rate = int.Parse(textBox_fengji_rate.Text);
            //                }
            //                else
            //                {
            //                    fengji_rate = 0;
            //                }
            //                // 提布频率
            //                int tibu_rate = 0;
            //                if (textBox_tibu_rate.Text != "")
            //                {
            //                    tibu_rate = int.Parse(textBox_tibu_rate.Text);
            //                }
            //                else
            //                {
            //                    tibu_rate = 0;
            //                }
            //                //mv.mysqlconnection.excute_sql("insert into craft_machine" + operation_machine_num.ToString() + " values ('" + xuhao.ToString() + "','" + wendu.ToString() + "','" + speed.ToString() + "','" + time.ToString() + "','" + craft + "','" + code.ToString() + "','" + rate.ToString() + "','"+fengji_rate.ToString()+"','"+tibu_rate.ToString()+"')");
            //                mv.mysqlconnection.excute_sql("insert into craft" + operation_machine_num.ToString() + "(wendu_shuiwei,speed,time,craft,nvarcharcode,rate,fengji_rate,tibu_rate) values ('" + wendu.ToString() + "','" + speed.ToString() + "','" + time.ToString() + "','" + craft + "','" + code.ToString() + "','" + rate.ToString() + "','" + fengji_rate.ToString() + "','" + tibu_rate.ToString() + "')");
            //                mv.craft_edit_view.ReFlash_Craft_Chart();
            //                mv.craft_edit_view.updata_datagridview();
            //                mv.craft_edit_view.draw_line();
            //                this.Dispose();
            //            }
            //        }
            //        catch
            //        {
            //        }
            //    }
            //}
            
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox_wenkong_select_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }
}
