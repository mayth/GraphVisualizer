using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algebra
{
    /// <summary>
    /// ノードの種類を表す列挙型。
    /// </summary>
    enum NodeType
    {
        /// <summary>
        /// 不明
        /// </summary>
        Unknown,
        /// <summary>
        /// 加算演算子
        /// </summary>
        Plus,
        /// <summary>
        /// 減算演算子
        /// </summary>
        Minus,
        /// <summary>
        /// 乗算演算子
        /// </summary>
        Multiply,
        /// <summary>
        /// 除算演算子
        /// </summary>
        Divide,
        /// <summary>
        /// 累乗演算子
        /// </summary>
        Power,
        /// <summary>
        /// 数値
        /// </summary>
        Number,
        /// <summary>
        /// 文字
        /// </summary>
        Character,
        /// <summary>
        /// 関数
        /// </summary>
        Function
    }

    /// <summary>
    /// 関数の種類を表す列挙型。
    /// </summary>
    enum FunctionType
    {
        /// <summary>
        /// 不明
        /// </summary>
        Unknown,
        /// <summary>
        /// sin
        /// </summary>
        Sin,
        /// <summary>
        /// cos
        /// </summary>
        Cos,
        /// <summary>
        /// tan
        /// </summary>
        Tan,
        /// <summary>
        /// 自然対数
        /// </summary>
        Log
    }

    /// <summary>
    /// 構文木のノードを表すクラスです。
    /// </summary>
    class Node
    {
        #region Properties
        private NodeType type;
        /// <summary>
        /// このノードの種類を取得します。
        /// </summary>
        public NodeType Type
        {
            get { return type; }
        }

        private FunctionType funcType;
        /// <summary>
        /// このノードが表す関数の種類を取得します。
        /// </summary>
        /// <exception cref="InvalidOperationException"><see cref="Type"/>がFunction以外です。</exception>
        public FunctionType FunctionType
        {
            get
            {
                if (Type != NodeType.Function)
                    throw new InvalidOperationException("このノードは関数を表していません。");
                return funcType;
            }
        }

        double value;
        /// <summary>
        /// 値を取得します。
        /// </summary>
        /// <exception cref="InvalidOperationException"><see cref="Type"/>がNumber以外です。数値以外のノードから値を読み出すことはできません。</exception>
        public double Value
        {
            get
            {
                if (Type != NodeType.Number)
                    throw new InvalidOperationException("数値以外のトークンから値を読み出すことはできません。");
                return value;
            }
        }

        char character;
        /// <summary>
        /// 文字を取得します。
        /// </summary>
        /// <exception cref="InvalidOperationException"><see cref="Type"/>がCharacter以外です。文字以外のノードから文字を読み出すことはできません。</exception>
        public char Character
        {
            get
            {
                if (Type != NodeType.Character)
                    throw new InvalidOperationException("文字以外のノードです。");
                return character;
            }
        }

        private Node left;
        /// <summary>
        /// 演算子の左側の式を取得します。
        /// </summary>
        /// <exception cref="InvalidOperationException"><see cref="Type"/>がPlus、Minus、Multiply、Divide、Powerのいずれでもありません。式以外を表すノードから演算子を取得することはできません。</exception>
        public Node Left
        {
            get
            {
                if (Type == NodeType.Plus
                    || Type == NodeType.Minus
                    || Type == NodeType.Multiply
                    || Type == NodeType.Divide
                    || Type == NodeType.Power)
                    return left;
                else
                    throw new InvalidOperationException("式以外を表すノードから演算子を取得することはできません。");
            }
        }

        private Node right;
        /// <summary>
        /// 演算子の右側の式を取得します。
        /// </summary>
        /// <exception cref="InvalidOperationException"><see cref="Type"/>がPlus、Minus、Multiply、Divide、Powerのいずれでもありません。式以外を表すノードから演算子を取得することはできません。</exception>
        public Node Right
        {
            get
            {
                if (Type == NodeType.Plus
                    || Type == NodeType.Minus
                    || Type == NodeType.Multiply
                    || Type == NodeType.Divide
                    || Type == NodeType.Power)
                    return right;
                else
                    throw new InvalidOperationException("式以外を表すノードから演算子を取得することはできません。");
            }
        }

        private Node funcExpr;
        /// <summary>
        /// 関数の式を取得します。
        /// </summary>
        /// <exception cref="InvalidOperationException"><see cref="Type"/>がFunction以外です。</exception>
        public Node FunctionExpression
        {
            get
            {
                if (Type != NodeType.Function)
                    throw new InvalidOperationException("このノードは関数を表していません。");
                return funcExpr;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// 空のノードを初期化します。
        /// </summary>
        public Node() { }

        /// <summary>
        /// 数値ノードを初期化します。
        /// </summary>
        /// <param name="value">このノードの数値</param>
        public Node(double value)
        {
            this.value = value;
            type = NodeType.Number;
            left = null;
            right = null;
            funcType = Algebra.FunctionType.Unknown;
            funcExpr = null;
        }

        /// <summary>
        /// 文字ノードを初期化します。
        /// </summary>
        /// <param name="character">このノードの文字</param>
        /// <exception cref="ArgumentException">文字ノードに使用できる文字は大文字か小文字のアルファベットに限られます。</exception>
        public Node(char character)
        {
            if (!char.IsLower(character) && !char.IsUpper(character))
                throw new ArgumentException("文字ノードに使用できる文字は大文字か小文字のアルファベットに限られます。");
            type = NodeType.Character;
            this.character = character;
            left = null;
            right = null;
            funcType = Algebra.FunctionType.Unknown;
            funcExpr = null;
        }

        /// <summary>
        /// 演算子ノードを初期化します。
        /// </summary>
        /// <param name="type">このノードの種類</param>
        /// <param name="left">左側演算対象ノード</param>
        /// <param name="right">右側演算対象ノード</param>
        /// <exception cref="ArgumentNullException"><paramref name="left"/>または<paramref name="right"/>がnullです。</exception>
        /// <exception cref="ArgumentException"><paramref name="type"/>に演算子以外の種類が指定されています。</exception>
        public Node(NodeType type, Node left, Node right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException("演算対象ノードをnullにすることはできません。");

            if (Type == NodeType.Plus
                || Type == NodeType.Minus
                || Type == NodeType.Multiply
                || Type == NodeType.Divide
                || Type == NodeType.Power)
            {
                this.type = type;
                this.left = left;
                this.right = right;
                value = 0.0;
                funcType = Algebra.FunctionType.Unknown;
                funcExpr = null;
            }
            else
                throw new ArgumentException("演算子ノード以外の初期化を行うことはできません。");
        }

        /// <summary>
        /// 関数ノードを初期化します。
        /// </summary>
        /// <param name="funcType">関数の種類</param>
        /// <param name="funcExpr">関数に与える式を表すノード</param>
        /// <exception cref="ArgumentNullException"><paramref name="funcExpr"/>がnullです。</exception>
        /// <exception cref="ArgumentException"><paramref name="funcType"/>がUnknownです。</exception>
        public Node(FunctionType funcType, Node funcExpr)
        {
            if (funcExpr == null)
                throw new ArgumentNullException("関数に与えるノードをnullにすることはできません。");
            if (funcType == Algebra.FunctionType.Unknown)
                throw new ArgumentException("不明な関数のノードを作成することはできません。");
            type = NodeType.Function;
            this.funcType = funcType;
            this.funcExpr = funcExpr;
            value = 0.0;
            left = null;
            right = null;
        }
        #endregion

        public bool Parse(IList<Token> tokens)
        {
            return ParseBase(RemoveParenthesis(tokens));
        }

        /// <summary>
        /// 与えられたトークン列を解析して、このノードに格納します。
        /// </summary>
        /// <param name="tokens">解析するトークン列</param>
        /// <returns>解析に成功した場合はtrue、そうでない場合はfalseを返します。</returns>
        /// <exception cref="FormatException">関数でも、定数でも、1文字でもない文字トークンが存在します。</exception>
        bool ParseBase(IList<Token> tokens)
        {
            // 演算子位置取得
            int operatorPosition = GetOperatorPosition(tokens);

            #region If can't find operator
            if (operatorPosition == -1)
            {
                left = null;
                right = null;
                if (tokens.Count == 1)
                {
                    switch (tokens[0].Type)
                    {
                        case TokenType.Number:
                            value = tokens[0].Value;
                            type = NodeType.Number;
                            break;
                        case TokenType.Character:
                            if (tokens[0].Character == "PI")
                            {
                                value = Math.PI;
                                type = NodeType.Number;
                            }
                            else if (tokens[0].Character.Length > 1)
                                throw new FormatException("関数でも、定数でも、1文字でもない文字トークンが存在します。");
                            else
                            {
                                character = char.Parse(tokens[0].Character);
                                type = NodeType.Character;
                            }
                            break;
                    }
                }
                else if (tokens.First().Type == TokenType.Character)
                {
                    switch (tokens.First().Character)
                    {
                        case "sin":
                            type = NodeType.Function;
                            funcType = Algebra.FunctionType.Sin;
                            break;
                        case "cos":
                            type = NodeType.Function;
                            funcType = Algebra.FunctionType.Cos;
                            break;
                        case "tan":
                            type = NodeType.Function;
                            funcType = Algebra.FunctionType.Tan;
                            break;
                        case "log":
                            type = NodeType.Function;
                            funcType = Algebra.FunctionType.Log;
                            break;
                        default:
                            throw new FormatException();
                    }
                    funcExpr = new Node();
                    funcExpr.ParseBase(RemoveParenthesis(tokens.Skip(1).ToList()));
                }

                return true;
            }
            #endregion

            switch (tokens[operatorPosition].Type)
            {
                case TokenType.Plus:
                    type = NodeType.Plus;
                    break;
                case TokenType.Minus:
                    type = NodeType.Minus;
                    break;
                case TokenType.Multiply:
                    type = NodeType.Multiply;
                    break;
                case TokenType.Divide:
                    type = NodeType.Divide;
                    break;
                case TokenType.Power:
                    type = NodeType.Power;
                    break;
            }

            left = new Node();
            if (!left.ParseBase(RemoveParenthesis(tokens.Take(operatorPosition).ToList())))
                return false;

            right = new Node();
            if (!right.ParseBase(RemoveParenthesis(tokens.Skip(operatorPosition + 1).ToList())))
                return false;

            return true;
        }

        /// <summary>
        /// このノードの値を計算します。
        /// </summary>
        /// <param name="parameter">計算に使用するパラメータの辞書</param>
        /// <returns>計算結果</returns>
        /// <exception cref="InvalidOperationException">
        /// 演算子ノードにおいて、オペランドが存在しません。
        /// または、関数ノードにおいて、関数の種類が不正です。
        /// または、ノードの種類が不正です。
        /// </exception>
        public double Evaluate(IDictionary<char, double> parameter)
        {
            Dictionary<char, double> param = new Dictionary<char, double>(parameter);
            if (!param.ContainsKey('e'))
                param.Add('e', Math.E);

            Dictionary<NodeType, Func<Node, Node, double>> operatorTable = new Dictionary<NodeType, Func<Node, Node, double>>()
            {
                {NodeType.Plus, (x, y) => x.Evaluate(param) + y.Evaluate(param)},
                {NodeType.Minus, (x, y) => x.Evaluate(param) - y.Evaluate(param)},
                {NodeType.Multiply, (x, y) => x.Evaluate(param) * y.Evaluate(param)},
                {NodeType.Divide, (x, y) => x.Evaluate(param) / y.Evaluate(param)},
                {NodeType.Power,
                    (x, y) =>
                        {
                            double d1 = Left.Evaluate(param);
                            if (d1 == 0.0) return 0;
                            double d2 = Right.Evaluate(param);
                            if (d2 == 0.0) return 1;
                            return Math.Pow(d1, d2);
                        }
                }
            };
            Dictionary<FunctionType, Func<Node, double>> functionTable = new Dictionary<FunctionType, Func<Node, double>>()
            {
                {FunctionType.Sin, x => Math.Sin(x.Evaluate(param))},
                {FunctionType.Cos, x => Math.Cos(x.Evaluate(param))},
                {FunctionType.Tan, x => Math.Tan(x.Evaluate(param))},
                {FunctionType.Log, x => Math.Log(x.Evaluate(param))},
            };

            switch (Type)
            {
                case NodeType.Plus:
                case NodeType.Minus:
                case NodeType.Multiply:
                case NodeType.Divide:
                case NodeType.Power:
                    if (Left == null || Right == null)
                        throw new InvalidOperationException("演算対象が存在しません。");
                    return operatorTable[Type](Left, Right);
                case NodeType.Number:
                    return value;
                case NodeType.Character:
                    if (!param.ContainsKey(Character))
                        throw new CharacterUnresolvedException(Character);
                    return param[Character];
                case NodeType.Function:
                    return functionTable[FunctionType](FunctionExpression);
                default:
                    throw new InvalidOperationException("不明なノードタイプです。");
            }
        }

        /// <summary>
        /// このノードの式を計算するのに必要な文字が含まれているかどうかを調べます。
        /// </summary>
        /// <param name="parameter">調べるパラメータのコレクション</param>
        /// <returns>不足がない場合はtrue、そうでない場合はfalseを返します。</returns>
        public bool CheckParameter(ICollection<char> parameter)
        {
            List<char> neededCharacters = GetCharacters();
            List<char> givenParameter = new List<char>(parameter);
            givenParameter.Add('x');
            foreach (char p in parameter)
                neededCharacters.Remove(p);
            return !neededCharacters.Any();
        }

        /// <summary>
        /// このノードの式を計算するのに必要な文字を取得します。
        /// </summary>
        /// <returns>このノードの式を計算するのに必要な文字のリスト。文字が必要でない場合は空のコレクションを返します。</returns>
        public List<char> GetCharacters()
        {
            List<char> result = new List<char>();
            switch (Type)
            {
                case NodeType.Character:
                    result.Add(Character);
                    break;
                case NodeType.Plus:
                case NodeType.Minus:
                case NodeType.Multiply:
                case NodeType.Divide:
                case NodeType.Power:
                    result.AddRange(Left.GetCharacters());
                    result.AddRange(Right.GetCharacters());
                    break;
                case NodeType.Function:
                    result.AddRange(FunctionExpression.GetCharacters());
                    break;
                case NodeType.Number:
                    break;
                default:
                    throw new InvalidOperationException("不明なノードです。");
            }
            result.Remove('x');
            return result;
        }

        /// <summary>
        /// このノードの文字列形式を返します。
        /// </summary>
        /// <returns>このノードの文字列形式</returns>
        /// <exception cref="InvalidOperationException">不明な種類のノードです。</exception>
        public override string ToString()
        {
            switch (Type)
            {
                case NodeType.Number:
                    //return "Number: " + value.ToString();
                    return value.ToString();
                case NodeType.Character:
                    //return "Character: " + character;
                    return character.ToString();
                case NodeType.Function:
                    //return "Function: <" + funcType.ToString() + "> [" + funcExpr.ToString() + "]";
                    return funcType.ToString() + "(" + funcExpr.ToString() + ")";
                case NodeType.Plus:
                case NodeType.Minus:
                case NodeType.Multiply:
                case NodeType.Divide:
                case NodeType.Power:
                    //return string.Format(
                    //    "Operator: {0} <{1}> {2}",
                    //    left == null ? string.Empty : left.ToString(),
                    //    Type.ToString(),
                    //    right == null ? string.Empty : right.ToString());
                    return string.Format("{0}[{1}, {2}]",
                        Type.ToString(),
                        left == null ? string.Empty : left.ToString(),
                        right == null ? string.Empty : right.ToString());
                case NodeType.Unknown:
                    return "[Unknown Node]";
                default:
                    throw new InvalidOperationException();
            }
        }

        /// <summary>
        /// トークン列中の演算子の位置を取得します。
        /// </summary>
        /// <param name="tokens">演算子の位置を取得するトークン列</param>
        /// <returns>演算子の位置</returns>
        static int GetOperatorPosition(IEnumerable<Token> tokens)
        {
            int i;
            int operatorPosition = -1;
            int parenNestLevel = 0;
            int priority;
            int lowestPriority = 5;

            if (tokens == null || tokens.Count() == 0)
                return -1;

            for (i = 0; i < tokens.Count(); i++)
            {
                switch (tokens.ElementAt(i).Type)
                {
                    case TokenType.Plus:
                    case TokenType.Minus:
                        priority = 2;
                        break;
                    case TokenType.Multiply:
                    case TokenType.Divide:
                        priority = 3;
                        break;
                    case TokenType.Power:
                        priority = 4;
                        break;
                    case TokenType.OpenParenthesis:
                        parenNestLevel++;
                        continue;
                    case TokenType.CloseParenthesis:
                        parenNestLevel--;
                        continue;
                    default:
                        continue;
                }

                if (parenNestLevel == 0 && priority <= lowestPriority)
                {
                    lowestPriority = priority;
                    operatorPosition = i;
                }
            }
            return operatorPosition;
        }

        /// <summary>
        /// トークン列の最も外側にある括弧を削除します。
        /// </summary>
        /// <param name="tokens">トークン列</param>
        /// <returns>括弧が削除されたトークン列</returns>
        /// <exception cref="ArgumentException">トークン列に含まれる括弧の数に過不足があります。</exception>
        static IList<Token> RemoveParenthesis(IList<Token> tokens)
        {
            int i;
            int nestLevel = 0;

            // 括弧がない
            int openParens = tokens.Count(x => x.Type == TokenType.OpenParenthesis);
            int closeParens = tokens.Count(x => x.Type == TokenType.CloseParenthesis);
            if (openParens == 0 && closeParens == 0)
                return tokens;
            if (openParens != closeParens)
                throw new ArgumentException("括弧の数に過不足があります。");

            if (tokens.First().Type != TokenType.OpenParenthesis || tokens.Last().Type != TokenType.CloseParenthesis)
                return tokens;

            for (i = 0; i < tokens.Count-1; i++)
            {
                if (tokens[i].Type == TokenType.OpenParenthesis)
                    nestLevel++;
                else if (tokens[i].Type == TokenType.CloseParenthesis)
                    nestLevel--;
                if (nestLevel == 0)
                    return tokens;
            }

            List<Token> tmp = tokens.Skip(1).Take(tokens.Count - 2).ToList();

            if (tmp.First().Type == TokenType.OpenParenthesis)
                return RemoveParenthesis(tmp);

            return tmp;
        }
    }
}
