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
    /// 式中の文字が解決できなかった際にスローされる例外。
    /// </summary>
    [Serializable]
    public class CharacterUnresolvedException : Exception
    {
        /// <summary>
        /// 解決できなかった文字を取得します。
        /// </summary>
        public char UnresolvedCharacter { get; private set; }

        /// <summary>
        /// 解決できなかった文字を指定して<see cref="CharacterUnresolvedException"/>クラスを初期化します。
        /// </summary>
        /// <param name="character">解決できなかった文字</param>
        public CharacterUnresolvedException(char character)
            : base("Character unresolved: '" + character.ToString() + "'")
        {
            UnresolvedCharacter = character;
        }

        /// <summary>
        /// 解決できなかった文字と例外を通知するメッセージを指定して<see cref="CharacterUnresolvedException"/>クラスを初期化します。
        /// </summary>
        /// <param name="character">解決できなかった文字</param>
        /// <param name="message">例外を通知するメッセージ</param>
        public CharacterUnresolvedException(char character, string message)
            : base(message)
        {
            UnresolvedCharacter = character;
        }

        /// <summary>
        /// <see cref="CharacterUnresolvedException"/>クラスを初期化します。
        /// </summary>
        /// <param name="info">シリアライズ情報</param>
        /// <param name="context">コンテキスト</param>
        protected CharacterUnresolvedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
