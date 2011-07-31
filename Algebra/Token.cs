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
    /// トークンの種類を表します。
    /// </summary>
    enum TokenType
    {
        /// <summary>
        /// 不明なトークン
        /// </summary>
        Unknown,
        /// <summary>
        /// 数値
        /// </summary>
        Number,
        /// <summary>
        /// 文字
        /// </summary>
        Character,
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
        /// 開丸括弧
        /// </summary>
        OpenParenthesis,
        /// <summary>
        /// 閉丸括弧
        /// </summary>
        CloseParenthesis,
    }

    /// <summary>
    /// トークンを表す構造体です。
    /// </summary>
    /// <remarks>この構造体が持つプロパティは全て読み取り専用です。デフォルトコンストラクタを使用すると値を設定できなくなります。</remarks>
    struct Token
    {
        #region Properties
        double value;
        /// <summary>
        /// 値を取得します。
        /// </summary>
        /// <remarks>
        /// <see cref="Type"/>がNumber以外の時は読み出すことができません。
        /// </remarks>
        /// <exception cref="InvalidOperationException"><see cref="Type"/>がNumber以外です。数値以外のトークンから値を読み出すことはできません。</exception>
        public double Value
        {
            get
            {
                if (Type != TokenType.Number)
                    throw new InvalidOperationException("数値以外のトークンから値を読み出すことはできません。");
                return value;
            }
        }

        private string character;
        /// <summary>
        /// 文字を取得します。
        /// </summary>
        /// <remarks>
        /// <see cref="Type"/>がCharacterの時以外は読み出すことができません。</remarks>
        /// <exception cref="InvalidOperationException"><see cref="Type"/>がCharacter以外です。文字以外のトークンから文字を読み出すことはできません。</exception>
        public string Character
        {
            get
            {
                if (Type != TokenType.Character)
                    throw new InvalidOperationException("文字以外のトークンから文字を読み出すことはできません。");
                return character;
            }
        }


        private TokenType type;
        /// <summary>
        /// このトークンの種類を取得します。
        /// </summary>
        public TokenType Type
        {
            get { return type; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// 数値トークンを初期化します。
        /// </summary>
        /// <param name="value">このトークンの値。</param>
        public Token(double value)
        {
            this.value = value;
            type = TokenType.Number;
            character = null;
        }

        /// <summary>
        /// 文字トークンを初期化します。
        /// </summary>
        /// <param name="character">このトークンの文字。</param>
        public Token(string character)
        {
            this.type = TokenType.Character;
            value = 0.0;
            this.character = character;
        }

        /// <summary>
        /// トークンを初期化します。
        /// </summary>
        /// <param name="type">トークンの種類。</param>
        /// <remarks>
        /// <paramref name="type"/>にNumberを指定した場合、値は0.0に設定されます。
        /// <paramref name="type"/>にCharacterを指定した場合、値はnullに設定されます。
        /// </remarks>
        public Token(TokenType type)
        {
            this.type = type;
            value = 0.0;
            character = null;
        }
        #endregion

        //
        // Static Public Method
        //
        /// <summary>
        /// 与えられた文字列をトークンに変換します。
        /// </summary>
        /// <param name="s">トークンに変換する文字列</param>
        /// <returns>トークン</returns>
        public static Token Parse(string s)
        {
            switch (s)
            {
                case "+":
                    return new Token(TokenType.Plus);
                case "-":
                    return new Token(TokenType.Minus);
                case "*":
                    return new Token(TokenType.Multiply);
                case "/":
                    return new Token(TokenType.Divide);
                case "^":
                    return new Token(TokenType.Power);
                case "(":
                    return new Token(TokenType.OpenParenthesis);
                case ")":
                    return new Token(TokenType.CloseParenthesis);
                default:
                    System.Text.RegularExpressions.Match match;
                    // 文字と数字の混合は認識不能
                    if (System.Text.RegularExpressions.Regex.IsMatch(s, @"[a-zA-Z]+[0-9]+") || System.Text.RegularExpressions.Regex.IsMatch(s, @"[0-9]+[a-zA-Z]+"))
                        throw new ArgumentException("トークンとして認識不能です。");

                    // 文字かどうか
                    match = System.Text.RegularExpressions.Regex.Match(s, @"[a-zA-Z]+");
                    if (match.Success)
                        return new Token(match.Value);

                    // 数値かどうか
                    match = System.Text.RegularExpressions.Regex.Match(s, @"[0-9]*\.?[0-9]+");
                    if (match.Success)
                    {
                        double d;
                        string matchString = match.Value;
                        while (true)
                        {
                            try
                            {
                                d = double.Parse(matchString);
                                break;
                            }
                            // double型の範囲を超えた場合、末尾を削って変換を試みる。
                            catch (OverflowException)
                            {
                                matchString = matchString.Remove(matchString.Length - 1, 1);
                            }
                        }
                        return new Token(d);
                    }

                    // 以上の正規表現にマッチしない場合はトークンとして認識不能。
                    throw new ArgumentException("トークンとして認識不能です。");
            }
        }

        /// <summary>
        /// このトークンの文字列形式を返します。
        /// </summary>
        /// <returns>このトークンの文字列形式</returns>
        public override string ToString()
        {
            if (Type == TokenType.Unknown)
                return "[Unknown Token]";

            if (Type == TokenType.Number)
                return "Number: " + Value.ToString();
            else if (Type == TokenType.Character)
                return "Character: " + Character;
            else
                return Type.ToString();
        }
    }
}
