using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algebra
{
    /// <summary>
    /// 数の範囲を示す構造体です。
    /// </summary>
    public struct Range
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
        /// 開始値と終値を指定して<see cref="Range"/>構造体を初期化します。
        /// </summary>
        /// <param name="from">開始値</param>
        /// <param name="to">終値</param>
        public Range(double from, double to)
            : this()
        {
            From = from;
            To = to;
            Step = 1.0;
        }

        /// <summary>
        /// 開始値と終値、変化量を指定して<see cref="Range"/>構造体を初期化します。
        /// </summary>
        /// <param name="from">開始値</param>
        /// <param name="to">終値</param>
        /// <param name="step">変化量</param>
        public Range(double from, double to, double step)
            : this()
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
    }
}
