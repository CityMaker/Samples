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
            catch (System.Exception)
            {
                return Color.White;
            }
        }
    }
}
