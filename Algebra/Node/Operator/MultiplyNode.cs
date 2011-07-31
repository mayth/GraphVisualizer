using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algebra
{
    /// <summary>
    /// 乗算演算子ノードを表します。
    /// </summary>
    class MultiplyNode : OperatorNode
    {
        /// <summary>
        /// 乗算演算子ノードを初期化します。
        /// </summary>
        /// <param name="left">左側演算対象ノード</param>
        /// <param name="right">右側演算対象ノード</param>
        public MultiplyNode(Node left, Node right) : base(left, right) { }

        /// <summary>
        /// このノードの値を計算します。
        /// </summary>
        /// <param name="parameter">計算に使用するパラメータの辞書</param>
        /// <returns>計算結果</returns>
        internal override double EvaluateNode(IDictionary<char, double> parameter)
        {
            return Left.EvaluateNode(parameter) * Right.EvaluateNode(parameter);
        }

        /// <summary>
        /// このノードの文字列形式を取得します。
        /// </summary>
        /// <returns>このノードの文字列形式</returns>
        public override string ToString()
        {
            return string.Format("Multiply[{0}, {1}]", Left.ToString(), Right.ToString());
        }
    }
}
