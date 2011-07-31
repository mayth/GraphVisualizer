using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algebra
{
    /// <summary>
    /// 数値ノードを表します。
    /// </summary>
	class NumberNode : Node
	{
        double _value;

        /// <summary>
        /// このノードの値を取得します。
        /// </summary>
        public double Value
        {
            get { return _value; }
        }

        /// <summary>
        /// 数値ノードを初期化します。
        /// </summary>
        /// <param name="value">このノードの値</param>
        public NumberNode(double value)
            : base()
        {
            _value = value;
        }

        /// <summary>
        /// このノードの値を計算します。
        /// </summary>
        /// <param name="parameter">計算に使用するパラメータの辞書</param>
        /// <returns>計算結果</returns>
        internal override double EvaluateNode(IDictionary<char, double> parameter)
        {
            return Value;
        }

        /// <summary>
        /// このノードの式を計算するのに必要な文字を取得します。
        /// </summary>
        /// <returns>このノードの式を計算するのに必要な文字のリスト。文字が必要でない場合は空のコレクションを返します。</returns>
        public override ICollection<char> GetCharactersBase()
        {
            return new List<char>();
        }

        /// <summary>
        /// このノードの文字列形式を取得します。
        /// </summary>
        /// <returns>このノードの文字列形式</returns>
        public override string ToString()
        {
            return "Number " + Value.ToString();
        }
	}
}
