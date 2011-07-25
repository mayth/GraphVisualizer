using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Algebra;

namespace ArithmeticExpressionTester
{
    class Program
    {
        static void Main(string[] args)
        {
            //string exprString = "(a+b)*(c-d)/(e+f)";
            string exprString = "sin(x)+cos(x)*2-(9.84/2)^(a)";
            List<Token> tokens = Tokenizer.Tokenize(exprString);
            Console.WriteLine("** Tokenize **");
            tokens.ForEach(x => Console.WriteLine(x.ToString()));
            Console.WriteLine("** Analyze **");
            Node root = new Node();
            root.Parse(tokens);
            Console.WriteLine(root.ToString());
            Console.WriteLine("** Evaluate **");
            Console.WriteLine("* Parameters *");
            Dictionary<char, double> parameter = new Dictionary<char, double>()
            {
                {'a', 1.4},
                {'b', 2.8},
                {'c', 2.2},
                {'d', 2.4},
                {'e', 5.1},
                {'f', 9.7},
                {'t', Math.PI / 4},
                {'x', 2.0}
            };
            foreach (var p in parameter)
                Console.WriteLine("{0} = {1}", p.Key, p.Value);
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            List<TimeSpan> elapsedTimes = new List<TimeSpan>();
            for (int i = 0; i < 1; i++)
            {
                sw.Start();
                root.Evaluate(parameter);
                Console.WriteLine("i = " + i + ": " + root.Evaluate(parameter));
                sw.Stop();
//                Console.WriteLine("  Elapsed Time: " + sw.Elapsed);
                elapsedTimes.Add(sw.Elapsed);
                sw.Reset();
            }
            Console.WriteLine("Average Elapsed Time: " + TimeSpan.FromMilliseconds(elapsedTimes.Average(ts => ts.Milliseconds)));
            
            //Expression expr = ArithmeticExpression.Expression.Parse(exprString);

            //Console.WriteLine("-- Original");
            //Console.WriteLine(exprString);
            //Console.WriteLine("-- Postorder");
            //Console.WriteLine(expr.Root.TraversePostorder());
            //Console.WriteLine();
            //Console.WriteLine("-- Inorder");
            //Console.WriteLine(expr.Root.TraverseInorder());
            //Console.WriteLine();
            //Console.WriteLine("-- Preorder");
            //Console.WriteLine(expr.Root.TraversePreorder());
            //Console.WriteLine();
            Console.ReadKey();
        }
    }
}
