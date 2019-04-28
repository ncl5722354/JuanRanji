using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ranji3._0
{
    class RealTime_data
    {
        public static int[,] DT=new int[61,1024];
        public static int[,] R = new int[61,1024];
        public static int[,] R_jicun = new int[61, 65535];
        public RealTime_data()
        {
        }
        public static double[] liulang = new double[61];
    }
}
