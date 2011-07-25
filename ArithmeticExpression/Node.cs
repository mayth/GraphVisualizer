using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArithmeticExpression
{
    public class Node
    {
        public string Expression { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }

        public int ParseExpression()
        {
            int operatorPosition;

            operatorPosition = GetOperatorPosition(Expression);

            if (operatorPosition == -1)
            {
                Left = null;
                Right = null;

                return 0;
            }

            // 左側
            Left = new Node();
            Left.Expression = Expression.Substring(0, operatorPosition);

            Left.Expression = RemoveBracket(Left.Expression);

            if (Left.ParseExpression() < 0)
                return -1;

            // 右側
            Right = new Node();
            Right.Expression = Expression.Substring(operatorPosition + 1, Expression.Length - operatorPosition - 1);

            Right.Expression = RemoveBracket(Right.Expression);

            if (Right.ParseExpression() < 0)
                return -1;

            Expression = Expression[operatorPosition].ToString();
            return 0;
        }

        static string RemoveBracket(string expression)
        {
            int i;
            int nestLevel = 1;

            if (!expression.StartsWith("(") || !expression.EndsWith(")"))
                return expression;

            for (i = 1; i < expression.Length - 1; i++)
            {
                if (expression[i] == '(')
                    nestLevel++;
                else if (expression[i] == ')')
                    nestLevel--;

                if (nestLevel == 0)
                    return expression;
            }

            if (nestLevel != 1)
                throw new ArgumentException("括弧の数に過不足があります。", "expression");

            string tmp = expression.Substring(1, expression.Length - 2);

            if (tmp.StartsWith("("))
                return RemoveBracket(tmp);

            return tmp;
        }

        static int GetOperatorPosition(string expression)
        {
            int i;
            int operatorPosition = -1;
            int nestLevel = 0;
            int priority;
            int lowestPriority = 4;

            if (string.IsNullOrWhiteSpace(expression))
                return -1;

            for (i = 0; i < expression.Length; i++)
            {
                switch (expression[i])
                {
                    case '=':
                        priority = 1;
                        break;
                    case '+':
                        priority = 2;
                        break;
                    case '-':
                        priority = 2;
                        break;
                    case '*':
                        priority = 3;
                        break;
                    case '/':
                        priority = 3;
                        break;
                    case '^':
                        priority = 3;
                        break;
                    case '(':
                        nestLevel++;
                        continue;
                    case ')':
                        nestLevel--;
                        continue;
                    default:
                        continue;
                }

                if (nestLevel == 0 && priority <= lowestPriority)
                {
                    lowestPriority = priority;
                    operatorPosition = i;
                }
            }

            return operatorPosition;
        }

        public string TraversePostorder()
        {
            string result = string.Empty;

            if (Left != null)
                result += Left.TraversePostorder();
            if (Right != null)
                result += Right.TraversePostorder();

            result += Expression;

            return result;
        }

        public string TraverseInorder()
        {
            string result = string.Empty;
            if (Left != null && Right != null)
                result += "(";

            if (Left != null)
                result += Left.TraverseInorder();

            result += Expression;

            if (Right != null)
                result += Right.TraverseInorder();

            if (Left != null && Right != null)
                result += ")";

            return result;
        }

        public string TraversePreorder()
        {
            string result = Expression;

            if (Left != null)
                result += Left.TraversePreorder();

            if (Right != null)
                result += Right.TraversePreorder();

            return result;
        }

        public override string ToString()
        {
            string result = string.Empty;
            if (Left != null)
                result += Left.ToString();
            result += Expression;
            if (Right != null)
                result += Right.ToString();
            return result;
        }
    }
}
