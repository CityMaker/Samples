// Copyright 2012 CityMaker SDK
// 
// All rights reserved under the copyright laws of the China
// and applicable international laws, treaties, and conventions.
// 
// You may freely redistribute and use this sample code, with or
// without modification, provided you include the original copyright
// notice and use restrictions.
// 
// See Sample at <your CityMaker install location>/CityMaker SDK/Samples.
// 
//author	gs
//date	2011/09/26
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MouseHover
{
    /// <summary>
    /// Description of ColorHelper.
    /// </summary>
    public class ColorHelper
    {
        /// <summary>
        /// 将Color值转换为对应的Uint值
        /// </summary>
        /// <param name="color">Color</param>
        /// <returns>返回值</returns>
        public static uint ColorToUint(Color color)
        {
            return ARGBToUint(color.A, color.R, color.G, color.B);
        }

        /// <summary>
        /// 将ARGB（0-1）转换为对应的Uint值
        /// </summary>
        /// <param name="a">颜色A(0-1)值，相对于255的小数</param>
        /// <param name="r">颜色A(0-1)值，相对于255的小数</param>
        /// <param name="g">颜色A(0-1)值，相对于255的小数</param>
        /// <param name="b">颜色A(0-1)值，相对于255的小数</param>
        /// <returns>返回值</returns>
        public static uint FloatToUint(float a, float r, float g, float b)
        {
            // 判断数据有效性
            if (a < 0) a = 0; if (a > 1) a = 1;
            if (r < 0) r = 0; if (r > 1) r = 1;
            if (g < 0) g = 0; if (g > 1) g = 1;
            if (b < 0) b = 0; if (b > 1) b = 1;
            return ARGBToUint((byte)(a * 255), (byte)(r * 255), (byte)(g * 255), (byte)(b * 255));
        }

        /// <summary>
        /// 将ARGB转换为对应的Uint值
        /// </summary>
        /// <param name="a">颜色A值</param>
        /// <param name="r">颜色R值</param>
        /// <param name="g">颜色G值</param>
        /// <param name="b">颜色B值</param>
        /// <returns>返回值</returns>
        public static uint ARGBToUint(byte a, byte r, byte g, byte b)
        {
            uint colorUint = (uint)(b | g << 8 | r << 16 | a << 24);
            return (uint)colorUint;
        }

        /// <summary>
        /// 根据Uint值返回Color值
        /// </summary>
        /// <param name="colorUint">颜色Uint值</param>
        /// <returns>返回值</returns>
        public static Color UintToColor(uint colorUint)
        {
            byte[] colorBytes = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                colorBytes[i] = (byte)((colorUint & 0x000000FF) % 256);
                colorUint = colorUint >> 8;
            }

            return Color.FromArgb(colorBytes[3], colorBytes[2], colorBytes[1], colorBytes[0]);
        }

        /// <summary>
        /// 得到对应颜色值的反色
        /// </summary>
        /// <param name="colorUint">颜色Uint值</param>
        /// <returns>返回值</returns>
        public static uint GetOppositeColor(uint colorUint)
        {
            return ((colorUint & 0x00FFFFFF) ^ 0xFFFFFF) | ((colorUint / 0xFFFFFF) << 24);
        }

        /// <summary>
        /// 将颜色Uint值的A值替换为给定的Alpha
        /// </summary>
        /// <param name="alpha">透明度</param>
        /// <param name="colorUint">颜色Uint值</param>
        /// <returns>返回值</returns>
        public static uint AlphaUintToUint(byte alpha, uint colorUint)
        {
            uint uAlpha = alpha;
            return (uint)((colorUint & 0x00FFFFFF) | (uAlpha << 24));
        }

        public static uint UintToFullAlpahUint(uint colorUint)
        {
            return AlphaUintToUint(255, colorUint);
        }

        public static uint GetHalfTransColor(uint colorUint)
        {
            return AlphaUintToUint(150, colorUint);
        }

        public static uint GetFlashHideColor(uint colorUint)
        {
            return AlphaUintToUint(50, colorUint);
        }
    }
}
