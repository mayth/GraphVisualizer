using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algebra
{
    class DivideNode : OperatorNode
    {
        public DivideNode(Node left, Node right) : base(left, right) { }

        internal override double  EvaluateNode(IDictionary<char,double> parameter)
        {
            return Left.EvaluateNode(parameter) / Right.EvaluateNode(parameter);
        }

        public override string ToString()
        {
            return string.Format("Divide[{0}, {1}]", Left.ToString(), Right.ToString());
        }
    }
}
