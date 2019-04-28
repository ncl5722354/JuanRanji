using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections;

/****************************************************/

// Coded by Ni Chenglong  5/28/2016
// 函数说明
// SQL_Connection(string connection_string) 构建函数，参数为连接字符
// ArrayList sql_select(string table_name) 返回对表<table_name>的查询，查询结果保存在一个嵌套了arraylist（row）的arraylist中。
// sql_insert(ArrayList addlist,string table_name) 对表<table_name>插入一行addlist
// sql_search_database(string table_name) //将<tabel_name>数据表以数据库的形式读出来

/****************************************************/

namespace Ranji3._0
{
    public class SQL_Connection
    {
        public SqlConnection myconnection = new SqlConnection();           //定义一个向数据库的连接
        //public SqlDataReader sqlreader=new SqlDataReader();
        
        public SQL_Connection(string connection_string)
        {
            myconnection.ConnectionString = connection_string;
            try
            {
                myconnection.Open();                                      //通过连接字符串进行连接
            }
            catch
            {
                MessageBox.Show("连接数据库失败！");
            }
        }
        

        public ArrayList sql_select(string table_name)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = myconnection;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from " + table_name;
                SqlDataReader sqlreader = cmd.ExecuteReader();
                ArrayList mylist = new ArrayList();
                while (sqlreader.Read())
                {
                    ArrayList rowlist = new ArrayList();                 //用来装一列的数据
                    int col_num = sqlreader.FieldCount;
                    for (int i = 0; i < col_num; i++)
                    {
                        rowlist.Add(sqlreader[i]);
                    }
                    mylist.Add(rowlist);                                //将每一列装入list中
                }
                return mylist;
            }                 // 将表中数据以arraylist arraylist的形式返回
            catch
            {
                return null; //出错的话返回空值
            }
        }

        public void sql_insert(ArrayList addlist,string table_name)
        {
            try
            {
                //在指定表内插入一行
                SqlCommand cmd = new SqlCommand();
                string canshu_string="";
                int listlength=addlist.Count;
                for(int i=0;i<listlength;i++)
                {
                    if (i < listlength - 1)
                    {
                        canshu_string = canshu_string + "'" + addlist[i].ToString() + "',";
                    }
                    else
                    {
                        canshu_string = canshu_string + "'" + addlist[i].ToString() + "'";
                    }
                }
                cmd.Connection = myconnection;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into " + table_name+" VALUES "+"("+canshu_string+")";
                cmd.ExecuteNonQuery();
            }
            catch
            {

            }
        }

        public DataSet sql_search_database(string cmd_string)
        {
            //通过表名，返回相应的数据库
            try
            {
                SqlCommand sqlcommand = new SqlCommand();
                SqlDataAdapter sqldataadapter=new SqlDataAdapter();
                DataSet sqldataset;
                sqlcommand.Connection = myconnection;
                sqlcommand.CommandType = CommandType.Text;
                sqlcommand.CommandText = cmd_string;
                sqldataadapter.SelectCommand = sqlcommand;
                sqldataset = new DataSet();
                sqldataset.Clear();
                sqldataadapter.Fill(sqldataset);
                return sqldataset;
            }
            catch
            {
                return null;
            }
            
        }

        public void excute_sql(string cmd_string)
        {
            try
            {
                // 执行一个SQL操作
                SqlCommand mysqlcommand = new SqlCommand();
                mysqlcommand.Connection = myconnection;
                mysqlcommand.CommandType = CommandType.Text;
                mysqlcommand.CommandText = cmd_string;
                mysqlcommand.ExecuteNonQuery();
            }
            catch
            {
            }
        }

    }
}
