using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

// ini操作
// [DllImport("kernel32")] 
// private static extern long WritePrivateProfileString(string section, string key, string val, string filePath); 
// section: 要写入的段落名
// key: 要写入的键，如果该key存在则覆盖写入
// val: key所对应的值
// filePath: INI文件的完整路径和文件名


namespace Ranji3._0
{
    public class IniFile
    {
        public string Path;

        // 声明INI文件的写操作函数 WritePrivateProfileString()
        [System.Runtime.InteropServices.DllImport("kernel32")]
        public static extern long WritePrivateProfileString(string section, string key, string val, string filePath);


        public IniFile(string path)
        {
            this.Path = path;
        }
        // 声明INI文件的读操作函数 GetPrivateProfileString()

        [System.Runtime.InteropServices.DllImport("kernel32")]

        public static extern int GetPrivateProfileString(string section, string key, string def, System.Text.StringBuilder retVal, int size, string filePath);
       

        /// <summary>
        /// 写INI文件
        /// </summary>
        /// <param name="section">段落</param>
        /// <param name="key">键</param>
        /// <param name="iValue">值</param>
        public void IniWriteValue(string section, string key, string iValue) 
        {
            WritePrivateProfileString(section, key, iValue, this.Path);
        }

        /// <summary>
        /// 读取INI文件
        /// </summary>
        /// <param name="section">段落</param>
        /// <param name="key">键</param>
        /// <returns>返回的键值</returns>
        public string IniReadValue(string section, string key) 
        { 
            StringBuilder temp = new StringBuilder(255); 

            int i = GetPrivateProfileString(section, key, "", temp, 255, this.Path); 
            return temp.ToString();
        }

        /// <summary>
        /// 读取INI文件
        /// </summary>
        /// <param name="Section">段，格式[]</param>
        /// <param name="Key">键</param>
        /// <returns>返回byte类型的section组或键值组</returns>
        //public byte[] IniReadValues(string section, string key)
        //{
        //    byte[] temp = new byte[255];

        //    int i = GetPrivateProfileString(section,key,"", temp,255, this.Path);
        //    return temp;
        //}
    }
}
