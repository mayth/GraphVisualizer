using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algebra
{
    class PowerNode : OperatorNode
    {
        public PowerNode(Node left, Node right) : base(left, right) { }

        internal override double  EvaluateNode(IDictionary<char,double> parameter)
        {
            double d1 = Left.EvaluateNode(parameter);
            if (d1 == 0.0) return 0.0;
            double d2 = Right.EvaluateNode(parameter);
            if (d2 == 0.0) return 1.0;
            return Math.Pow(d1, d2);
        }

        public override string ToString()
        {
            return string.Format("Power[{0}, {1}]", Left.ToString(), Right.ToString());
        }
    }
}
