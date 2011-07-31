using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algebra
{
    abstract class FunctionNode : Node
    {
        private Node _funcExpr;

        public Node FunctionExpression
        {
            get { return _funcExpr; }
        }

        /// <summary>
        /// 関数ノードを初期化します。
        /// </summary>
        /// <param name="functionExpression">関数に与える式を表すノード</param>
        /// <exception cref="ArgumentNullException"><paramref name="functionExpression"/>がnullです。</exception>
        protected FunctionNode(Node functionExpression)
        {
            if (functionExpression == null)
                throw new ArgumentNullException("functionExpression");
            _funcExpr = functionExpression;
        }

        public override ICollection<char> GetCharactersBase()
        {
            return FunctionExpression.GetCharactersBase();
        }
    }
}
