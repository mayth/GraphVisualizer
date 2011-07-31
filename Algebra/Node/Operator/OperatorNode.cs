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
    /// 演算子ノードを表す抽象クラスです。
    /// </summary>
    abstract class OperatorNode : Node
    {
        private Node _left;
        private Node _right;

        /// <summary>
        /// 左側演算対象ノードを取得します。
        /// </summary>
        public Node Left
        {
            get { return _left; }
        }

        /// <summary>
        /// 右側演算対象ノードを取得します。
        /// </summary>
        public Node Right
        {
            get { return _right; }
        }

        /// <summary>
        /// 演算子ノードを初期化します。
        /// </summary>
        /// <param name="left">左側演算対象ノード</param>
        /// <param name="right">右側演算対象ノード</param>
        /// <exception cref="ArgumentNullException"><paramref name="left"/>または<paramref name="right"/>がnullです。</exception>
        protected OperatorNode(Node left, Node right)
        {
            if (left == null)
                throw new ArgumentNullException("left");
            if (right == null)
                throw new ArgumentNullException("right");

            _left = left;
            _right = right;
        }

        /// <summary>
        /// このノードの式を計算するのに必要な文字を取得します。
        /// </summary>
        /// <returns>このノードの式を計算するのに必要な文字のリスト。文字が必要でない場合は空のコレクションを返します。</returns>
        public override ICollection<char> GetCharactersBase()
        {
            List<char> result = new List<char>();
            result.AddRange(Left.GetCharactersBase());
            result.AddRange(Right.GetCharactersBase());
            return result;
        }
    }
}
