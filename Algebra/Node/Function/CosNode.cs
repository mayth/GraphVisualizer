using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algebra
{
    class CosNode : FunctionNode
    {
        public CosNode(Node functionExpression) : base(functionExpression) { }

        internal override double EvaluateNode(IDictionary<char, double> parameter)
        {
            return Math.Cos(FunctionExpression.EvaluateNode(parameter));
        }

        public override string ToString()
        {
            return "Cos(" + FunctionExpression.ToString() + ")";
        }
    }
}
