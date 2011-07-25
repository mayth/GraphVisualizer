using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArithmeticExpression
{
    // http://smdn.jp/programming/tips/polish/
    public class Expression
    {
        public Node Root { get; set; }

        public Expression(Node root)
        {
            Root = root;
        }

        public static Expression Parse(string expression)
        {
            string expr = RemoveSpaces(expression);
            Node root = new Node();
            root.Expression = expr;
            root.ParseExpression();
            return new Expression(root);
        }

        static string RemoveSpaces(string str)
        {
            string result = string.Empty;
            foreach (char c in str)
                if (!char.IsWhiteSpace(c))
                    result += c;
            return result;
        }

        public double Calculate(ParameterResolver parameters)
        {
            // RPN記法で取得
            string parsedExpression = Root.TraversePostorder();

            Stack<double> calcStack = new Stack<double>();
            foreach (char c in parsedExpression)
            {
            }

            return 0.0;
        }

        double Add(double op1, double op2)
        {
            return op1 + op2;
        }

        double Subtract(double op1, double op2)
        {
            return op1 - op2;
        }

        double Multiply(double op1, double op2)
        {
            return op1 * op2;
        }

        double Divide(double op1, double op2)
        {
            if (op2 == 0)
                return double.NaN;
            return op1 / op2;
        }

        double Power(double op1, double op2)
        {
            return Math.Pow(op1, op2);
        }
    }
}
