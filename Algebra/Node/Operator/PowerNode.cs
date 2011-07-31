using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algebra
{
    class PowerNode : OperatorNode
    {
        /// <summary>
        /// 累乗演算子ノードを初期化します。
        /// </summary>
        /// <param name="left">左側演算対象ノード</param>
        /// <param name="right">右側演算対象ノード</param>
        public PowerNode(Node left, Node right) : base(left, right) { }

        /// <summary>
        /// このノードの値を計算します。
        /// </summary>
        /// <param name="parameter">計算に使用するパラメータの辞書</param>
        /// <returns>計算結果</returns>
        internal override double EvaluateNode(IDictionary<char, double> parameter)
        {
            double d1 = Left.EvaluateNode(parameter);
            if (d1 == 0.0) return 0.0;
            double d2 = Right.EvaluateNode(parameter);
            if (d2 == 0.0) return 1.0;
            return Math.Pow(d1, d2);
        }

        /// <summary>
        /// このノードの文字列形式を取得します。
        /// </summary>
        /// <returns>このノードの文字列形式</returns>
        public override string ToString()
        {
            return string.Format("Power[{0}, {1}]", Left.ToString(), Right.ToString());
        }
    }
}
