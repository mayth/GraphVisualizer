using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algebra
{
    class MultiplyNode : OperatorNode
    {
        public MultiplyNode(Node left, Node right) : base(left, right) { }

        internal override double  EvaluateNode(IDictionary<char,double> parameter)
        {
            return Left.EvaluateNode(parameter) * Right.EvaluateNode(parameter);
        }
    }
}
