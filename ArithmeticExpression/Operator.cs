using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArithmeticExpression
{
    /// <summary>
    /// 演算子を表す列挙型。
    /// </summary>
    enum Operator
    {
        /// <summary>
        /// 加算
        /// </summary>
        Plus,
        /// <summary>
        /// 減算
        /// </summary>
        Minus,
        /// <summary>
        /// 乗算
        /// </summary>
        Multiply,
        /// <summary>
        /// 除算
        /// </summary>
        Divide,
        /// <summary>
        /// 累乗
        /// </summary>
        Power
    }
}
