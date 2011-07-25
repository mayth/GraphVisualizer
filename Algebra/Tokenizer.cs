using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Algebra
{
    /// <summary>
    /// 与えられた数式文字列をトークン文字列に分解するクラスです。
    /// </summary>
    static class Tokenizer
    {
        /// <summary>
        /// 与えられた数式の文字列をトークン単位に分割します。
        /// </summary>
        /// <param name="expression">トークン単位に分割する数式の文字列</param>
        /// <returns>トークンのリスト</returns>
        public static List<Token> Tokenize(string expression)
        {
            // トークン分割の目印になる文字。これが現れたらその直前でトークンとして切り出す。この文字自体は単独のトークンとする。
            char[] tokenBreakCharacter = new char[]
            {
                '+', '-', '*', '/', '^', '(', ')'
            };
            
            List<Token> result = new List<Token>();
            string temp = string.Empty;

            foreach (char c in expression)
            {
                if (c == ' ')
                    continue;

                if (tokenBreakCharacter.Contains(c))
                {
                    if (!string.IsNullOrEmpty(temp))
                        result.Add(Token.Parse(temp));
                    result.Add(Token.Parse(c.ToString()));
                    temp = string.Empty;
                }
                else
                {
                    temp += c;
                }
            }
            if (!string.IsNullOrEmpty(temp))
                result.Add(Token.Parse(temp));

            return result;
        }
    }
}
