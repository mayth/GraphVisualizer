using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algebra
{
	class NumberNode : Node
	{
        double _value;

        public double Value
        {
            get { return _value; }
        }

        /// <summary>
        /// 数値ノードを初期化します。
        /// </summary>
        /// <param name="value">このノードの値</param>
        public NumberNode(double value)
            : base()
        {
            _value = value;
        }

        internal override double EvaluateNode(IDictionary<char, double> parameter)
        {
            return Value;
        }

        public override ICollection<char> GetCharactersBase()
        {
            return new List<char>();
        }

        public override string ToString()
        {
            return "Number " + Value.ToString();
        }
	}
}
