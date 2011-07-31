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
    /// 計算を行うサポートクラスです。
    /// </summary>
    public class Calculator
    {
        Node rootNode;

        #region Properties
        /// <summary>
        /// パラメータの辞書を取得・設定します。
        /// </summary>
        public IDictionary<char, double> Parameter { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// 文字列で表された式を使用して<see cref="Calculator"/>クラスを初期化します。
        /// </summary>
        /// <param name="expression">式</param>
        public Calculator(string expression)
        {
            rootNode = Node.Parse(Tokenizer.Tokenize(expression));
            Parameter = new Dictionary<char, double>();
        }

        /// <summary>
        /// 文字列で表された式とパラメータの辞書を使用して<see cref="Calculator"/>クラスを初期化します。
        /// </summary>
        /// <param name="expression">式</param>
        /// <param name="parameter">パラメータの辞書</param>
        public Calculator(string expression, IDictionary<char, double> parameter)
            : this(expression)
        {
            Parameter = parameter;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// 式を設定します。
        /// </summary>
        /// <param name="expression">設定する式</param>
        /// <exception cref="ArgumentNullException"><paramref name="expression"/>がnullか空か、空白のみで構成されています。</exception>
        public void SetExpression(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression))
                throw new ArgumentNullException("expression", "式がnullか空、もしくは空白のみです。");
            rootNode = Node.Parse(Tokenizer.Tokenize(expression));
        }

        /// <summary>
        /// このノードの式を計算するのに必要な文字が含まれているかどうかを調べます。
        /// </summary>
        /// <param name="parameter">調べるパラメータのコレクション</param>
        /// <returns>不足がない場合はtrue、そうでない場合はfalseを返します。</returns>
        public bool CheckParameter(ICollection<char> parameter)
        {
            return rootNode.CheckParameter(parameter);
        }

        /// <summary>
        /// このノードの式を計算するのに必要な文字を取得します。
        /// </summary>
        /// <returns>このノードの式を計算するのに必要な文字のリスト。文字が必要でない場合は空のコレクションを返します。</returns>
        public ICollection<char> GetCharacters()
        {
            return rootNode.GetCharactersBase();
        }

        /// <summary>
        /// 変数の値を指定して計算します。
        /// </summary>
        /// <param name="x">変数の値</param>
        /// <returns>計算結果</returns>
        public double Calculate(double x)
        {
            Dictionary<char, double> arg;
            if (Parameter != null)
                arg = new Dictionary<char, double>(Parameter);
            else
                arg = new Dictionary<char, double>();
            arg['x'] = x;
            return rootNode.Evaluate(arg);
        }

        /// <summary>
        /// 変数の変域を指定して計算します。
        /// </summary>
        /// <param name="domain">変域</param>
        /// <returns>計算結果の辞書</returns>
        public IDictionary<double, double> CalculateRange(Range domain)
        {
            Dictionary<double, double> result = new Dictionary<double, double>();
            Dictionary<char, double> arg;
            if (Parameter != null)
                arg = new Dictionary<char, double>(Parameter);
            else
                arg = new Dictionary<char, double>();
            arg['e'] = Math.E;
            for (double d = domain.From; d < domain.To; d += domain.Step)
            {
                arg['x'] = d;
                result.Add(d, rootNode.Evaluate(arg));
            }
            Dictionary<double, double> temp = new Dictionary<double, double>();
            if (result.Any(x => double.IsNaN(x.Value)))
            {
                foreach (var kv in result.Where(x => !double.IsNaN(x.Value)))
                    temp.Add(kv.Key, kv.Value);
                return temp;
            }
            else
                return result;
        }
        #endregion
    }
}
