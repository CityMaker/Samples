using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Gvitech
{
    class Utils
    {
        public static int getIndexFromItems(object[] items, object value)
        {
            int index = 0;
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].Equals(value))
                {
                    index = i;
                    break;
                }
            }
            return index;
        }

        /// <summary>
        /// 求某一天到日期零点值相差的天数
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static double DateToDouble(DateTime date)
        {
            string strDate;
            double i = 0;
            do
            {
                i++;
                strDate = DateTime.FromOADate(i).ToString("d");
            } while (strDate != date.ToString("d"));

            return i;
        }


    }
}
