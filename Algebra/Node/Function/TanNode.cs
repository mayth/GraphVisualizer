using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algebra
{
    class TanNode : FunctionNode
    {
        public TanNode(Node functionExpression) : base(functionExpression) { }

        internal override double EvaluateNode(IDictionary<char, double> parameter)
        {
            return Math.Tan(FunctionExpression.EvaluateNode(parameter));
        }

        public override string ToString()
        {
            return "Tan(" + FunctionExpression.ToString() + ")";
        }
    }
}
