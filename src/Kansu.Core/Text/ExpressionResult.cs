using System;
using Kansu.Collection;

namespace Kansu.Text {

    /// <summary>
    /// 
    /// </summary>
    public struct ExpressionResult : IEquatable<ExpressionResult> {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isMatch"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public ExpressionResult(bool isMatch, SinglyLinked<char> start, SinglyLinked<char> end) {
            IsMatch = isMatch;
            Start = start;
            End = end;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsMatch { get; }

        /// <summary>
        /// 
        /// </summary>
        public SinglyLinked<char> Start { get; }

        /// <summary>
        /// 
        /// </summary>
        public SinglyLinked<char> End { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(ExpressionResult other) =>
            IsMatch == other.IsMatch && Equals(Start, other.Start) && Equals(End, other.End);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj) =>
            obj is ExpressionResult other && Equals(other);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() {
            unchecked {
                var hashCode = IsMatch.GetHashCode();
                hashCode = (hashCode * 397) ^ (Start != null ? Start.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (End != null ? End.GetHashCode() : 0);
                return hashCode;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static ExpressionResult Success(SinglyLinked<char> start, SinglyLinked<char> end) =>
            new ExpressionResult(true, start, end);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="chr"></param>
        /// <returns></returns>
        public static ExpressionResult Fail(SinglyLinked<char> chr) =>
            new ExpressionResult(false, chr, chr);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public ExpressionResult Bind(ExpressionResult other) =>
            new ExpressionResult(IsMatch && other.IsMatch, Start, other.End);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isMatch"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public void Deconstruct(out bool isMatch, out SinglyLinked<char> start, out SinglyLinked<char> end) {
            isMatch = IsMatch;
            start = Start;
            end = End;
        }

    }

}