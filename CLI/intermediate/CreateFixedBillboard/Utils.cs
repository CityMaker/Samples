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
        /// 由16进制uint对应的字符串 转换成 Color对象
        /// 格式：ARGB
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Color HexNumberToColor(string value)
        {
            try
            {
                if (value.Length == 8)
                {
                    int a;
                    int r;
                    int g;
                    int b;
                    int.TryParse(value.Substring(0, 2), System.Globalization.NumberStyles.HexNumber, null, out a);
                    int.TryParse(value.Substring(2, 2), System.Globalization.NumberStyles.HexNumber, null, out r);
                    int.TryParse(value.Substring(4, 2), System.Globalization.NumberStyles.HexNumber, null, out g);
                    int.TryParse(value.Substring(6, 2), System.Globalization.NumberStyles.HexNumber, null, out b);
                    if (a == 0 && r == 0 && g == 0 && b == 0)
                        return Color.White;
                    return Color.FromArgb(a, r, g, b);
                }
                else if (value.Length == 7)
                {
                    int a;
                    int r;
                    int g;
                    int b;
                    int.TryParse(value.Substring(0, 1), System.Globalization.NumberStyles.HexNumber, null, out a);
                    int.TryParse(value.Substring(1, 2), System.Globalization.NumberStyles.HexNumber, null, out r);
                    int.TryParse(value.Substring(3, 2), System.Globalization.NumberStyles.HexNumber, null, out g);
                    int.TryParse(value.Substring(5, 2), System.Globalization.NumberStyles.HexNumber, null, out b);
                    if (a == 0 && r == 0 && g == 0 && b == 0)
                        return Color.White;
                    return Color.FromArgb(a, r, g, b);
                }
                else if (value.Length == 6)
                {
                    int r;
                    int g;
                    int b;
                    int.TryParse(value.Substring(0, 2), System.Globalization.NumberStyles.HexNumber, null, out r);
                    int.TryParse(value.Substring(2, 2), System.Globalization.NumberStyles.HexNumber, null, out g);
                    int.TryParse(value.Substring(4, 2), System.Globalization.NumberStyles.HexNumber, null, out b);
                    if (r == 0 && g == 0 && b == 0)
                        return Color.White;
                    return Color.FromArgb(0, r, g, b);
                }
                else if (value.Length == 1)
                    return Color.Black;
                return Color.White;
            }
            catch (System.Exception e)
            {
                return Color.White;
            }
        }

        /// <summary>
        /// 由int值转成uint
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static uint ToArgb(int a, int r, int g, int b)
        {
            uint color = (uint)(b | g << 8 | r << 16 | a << 24);
            return color;
        }

        /// <summary>
        /// 由Color对象转成uint
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static uint ToArgb(Color c)
        {
            int B = (int)c.B;
            int G = (int)c.G << 8;
            int R = (int)c.R << 16;
            int A = (int)(c.A == 0 ? 255 : c.A) << 24;

            uint color = (uint)(A | R | G | B);

            return color;
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
