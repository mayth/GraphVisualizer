using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algebra
{
    abstract class OperatorNode : Node
    {
        private Node _left;
        private Node _right;

        public Node Left
        {
            get { return _left; }
        }

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
