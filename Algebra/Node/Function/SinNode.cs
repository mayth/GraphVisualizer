using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algebra
{
    class SinNode : FunctionNode
    {
        public SinNode(Node functionExpression) : base(functionExpression) { }

        internal override double EvaluateNode(IDictionary<char, double> parameter)
        {
            return Math.Sin(FunctionExpression.EvaluateNode(parameter));
        }

        public override string ToString()
        {
            return "Sin(" + FunctionExpression.ToString() + ")";
        }
    }
}
