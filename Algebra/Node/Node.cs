/*
 * Copyright (c) 2011 mayth
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"),
 * to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software,
 * and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
 * WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 *
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algebra
{
    /// <summary>
    /// 構文木のノードを表す抽象クラスです。
    /// </summary>
    abstract class Node
    {
        /// <summary>
        /// 空のノードを初期化します。
        /// </summary>
        protected Node() { }

        /// <summary>
        /// 与えられたトークン列を解析して、このノードに格納します。
        /// </summary>
        /// <param name="tokens">解析するトークン列</param>
        /// <exception cref="FormatException">関数でも、定数でも、1文字でもない文字トークンが存在します。</exception>
        public static Node Parse(IList<Token> tokens)
        {
            return ParseBase(RemoveParenthesis(tokens));
        }

        /// <summary>
        /// 与えられたトークン列を解析して、このノードに格納します。
        /// </summary>
        /// <param name="tokens">解析するトークン列</param>
        /// <exception cref="FormatException">関数でも、定数でも、1文字でもない文字トークンが存在します。</exception>
        static Node ParseBase(IList<Token> tokens)
        {
            // 演算子位置取得
            int operatorPosition = GetOperatorPosition(tokens);

            #region can't find operator
            if (operatorPosition == -1)
            {
                if (tokens.Count == 1)
                {
                    switch (tokens[0].Type)
                    {
                        case TokenType.Number:
                            return new NumberNode(tokens[0].Value);
                        case TokenType.Character:
                            if (tokens[0].Character == "PI")
                                return new NumberNode(Math.PI);
                            else if (tokens[0].Character.Length > 1)
                                throw new FormatException("関数でも、定数でも、1文字でもない文字トークンが存在します。");
                            else
                                return new CharacterNode(char.Parse(tokens[0].Character));
                        default:
                            throw new FormatException("トークンが1つしかない状態で数値でも文字でもないトークンを検出しました。");
                    }
                }
                else if (tokens.First().Type == TokenType.Character)
                {
                    Node funcNode = Node.Parse(tokens.Skip(1).ToList());
                    switch (tokens.First().Character)
                    {
                        case "sin":
                            return new SinNode(funcNode);
                        case "cos":
                            return new CosNode(funcNode);
                        case "tan":
                            return new TanNode(funcNode);
                        case "log":
                            return new LogNode(funcNode);
                        default:
                            throw new FormatException("認識できない関数です。");
                    }
                }
            }
            #endregion
            else
            {
                Node left = Node.Parse(tokens.Take(operatorPosition).ToList());
                Node right = Node.Parse(tokens.Skip(operatorPosition + 1).ToList());

                switch (tokens[operatorPosition].Type)
                {
                    case TokenType.Plus:
                        return new PlusNode(left, right);
                    case TokenType.Minus:
                        return new SubtractNode(left, right);
                    case TokenType.Multiply:
                        return new MultiplyNode(left, right);
                    case TokenType.Divide:
                        return new DivideNode(left, right);
                    case TokenType.Power:
                        return new PowerNode(left, right);
                    default:
                        throw new InvalidOperationException("演算子の検出に失敗しました。演算子位置のトークンが演算子トークンではありません。");
                }
            }
            throw new InvalidOperationException("ノードの種別を特定できませんでした。");
        }

        /// <summary>
        /// このノードの値を計算します。
        /// </summary>
        /// <param name="parameter">計算に使用するパラメータの辞書</param>
        /// <returns>計算結果</returns>
        internal abstract double EvaluateNode(IDictionary<char, double> parameter);

        /// <summary>
        /// このノードの値を計算します。
        /// </summary>
        /// <param name="parameter">計算に使用するパラメータの辞書</param>
        /// <returns>計算結果</returns>
        public double Evaluate(IDictionary<char, double> parameter)
        {
            Dictionary<char, double> param = new Dictionary<char, double>(parameter);
            if (!param.ContainsKey('e'))
                param.Add('e', Math.E);

            return EvaluateNode(param);
        }

        /// <summary>
        /// このノードの式を計算するのに必要な文字が含まれているかどうかを調べます。
        /// </summary>
        /// <param name="parameter">調べるパラメータのコレクション</param>
        /// <returns>不足がない場合はtrue、そうでない場合はfalseを返します。</returns>
        public bool CheckParameter(ICollection<char> parameter)
        {
            List<char> neededCharacters = new List<char>(GetCharacters());
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
        public ICollection<char> GetCharacters()
        {
            var result = GetCharactersBase();
            result.Remove('x');
            return result;
        }

        /// <summary>
        /// このノードの式を計算するのに必要な文字を取得します。
        /// </summary>
        /// <returns>このノードの式を計算するのに必要な文字のリスト。文字が必要でない場合は空のコレクションを返します。</returns>
        public abstract ICollection<char> GetCharactersBase();

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
