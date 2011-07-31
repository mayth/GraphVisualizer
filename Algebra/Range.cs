/*
 * Copyright (c) 2011 mayth
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"),
 * to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software,
 * and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
 * WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 *
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algebra
{
    /// <summary>
    /// 数の範囲を示すクラスです。
    /// </summary>
    public class Range : IEnumerable<double>
    {
        /// <summary>
        /// 開始値
        /// </summary>
        public double From { get; set; }
        /// <summary>
        /// 終値
        /// </summary>
        public double To { get; set; }
        /// <summary>
        /// 変化量
        /// </summary>
        public double Step { get; set; }

        /// <summary>
        /// 空の<see cref="Range"/>クラスを初期化します。
        /// </summary>
        public Range()
        {
            From = 0.0;
            To = 0.0;
            Step = 0.0;
        }

        /// <summary>
        /// 開始値と終値を指定して<see cref="Range"/>クラスを初期化します。
        /// </summary>
        /// <param name="from">開始値</param>
        /// <param name="to">終値</param>
        public Range(double from, double to)
        {
            From = from;
            To = to;
            Step = 1.0;
        }

        /// <summary>
        /// 開始値と終値、変化量を指定して<see cref="Range"/>クラスを初期化します。
        /// </summary>
        /// <param name="from">開始値</param>
        /// <param name="to">終値</param>
        /// <param name="step">変化量</param>
        public Range(double from, double to, double step)
        {
            From = from;
            To = to;
            Step = step;
        }

        /// <summary>
        /// 空の範囲を示すかどうかを調べます。
        /// </summary>
        /// <param name="range">調べる<see cref="Range"/>構造体</param>
        /// <returns>空であるならばtrue、そうでないならばfalseを返します。</returns>
        public static bool IsEmpty(Range range)
        {
            return range.From == 0.0 && range.To == 0.0 && range.Step == 0.0;
        }

        /// <summary>
        /// 開始値から終値までを列挙する列挙子を返します。
        /// </summary>
        public IEnumerator<double> GetEnumerator()
        {
            for (double d = From; d <= To; d += Step)
                yield return d;
        }

        /// <summary>
        /// 開始値から終値までを列挙する列挙子を返します。
        /// </summary>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            for (double d = From; d <= To; d += Step)
                yield return d;
        }
    }
}
