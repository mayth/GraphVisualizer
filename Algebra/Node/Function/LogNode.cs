using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algebra
{
    class LogNode : FunctionNode
    {
        public LogNode(Node functionExpression) : base(functionExpression) { }

        internal override double EvaluateNode(IDictionary<char, double> parameter)
        {
            return Math.Log(FunctionExpression.EvaluateNode(parameter));
        }

        public override string ToString()
        {
            return "Log(" + FunctionExpression.ToString() + ")";
        }
    }
}
