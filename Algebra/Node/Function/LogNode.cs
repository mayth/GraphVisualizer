using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algebra
{
    class LogNode : FunctionNode
    {
        /// <summary>
        /// 自然対数関数ノードを初期化します。
        /// </summary>
        /// <param name="functionExpression">関数に与える式を表すノード</param>
        public LogNode(Node functionExpression) : base(functionExpression) { }

        /// <summary>
        /// このノードの値を計算します。
        /// </summary>
        /// <param name="parameter">計算に使用するパラメータの辞書</param>
        /// <returns>計算結果</returns>
        internal override double EvaluateNode(IDictionary<char, double> parameter)
        {
            return Math.Log(FunctionExpression.EvaluateNode(parameter));
        }

        /// <summary>
        /// このノードの文字列形式を取得します。
        /// </summary>
        /// <returns>このノードの文字列形式</returns>
        public override string ToString()
        {
            return "Log(" + FunctionExpression.ToString() + ")";
        }
    }
}
