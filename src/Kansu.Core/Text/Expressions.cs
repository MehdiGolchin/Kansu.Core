using System.Linq;
using Kansu.Collection;

namespace Kansu.Text {

    /// <summary>
    /// 
    /// </summary>
    /// <param name="buffer"></param>
    public delegate ExpressionResult Expression(SinglyLinked<char> buffer);

    /// <summary>
    /// 
    /// </summary>
    public static class Expressions {

        private const char SpaceChar = ' ';
        private const char CarriageReturnChar = '\r';
        private const char LineFeedChar = '\n';

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Expression Space() => Character(SpaceChar);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Expression Newline() => OneOf(
            Character(LineFeedChar),
            Sequence(Character(CarriageReturnChar), Character(LineFeedChar))
        );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expected"></param>
        /// <returns></returns>
        public static Expression Character(char expected) =>
            chr => chr?.Head switch {
                var c when c == expected => ExpressionResult.Success(chr, chr.Tail),
                _ => ExpressionResult.Fail(chr)};

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="chr"></param>
        /// <returns></returns>
        public static Expression ZeroOrMore(Expression expression) =>
            chr => ExpressionResult.Success(
                chr,
                expression(chr).IsMatch ? ZeroOrMore(expression)(chr.Tail).End : chr
            );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static Expression OneOrMore(Expression expression) =>
            chr => ZeroOrMore(expression)(chr) switch {
                var r when r.Start == r.End => ExpressionResult.Fail(chr),
                var r => r};

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expressions"></param>
        /// <returns></returns>
        public static Expression Sequence(params Expression[] expressions) =>
            expressions.Aggregate(Reduce);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expressions"></param>
        /// <returns></returns>
        public static Expression OneOf(params Expression[] expressions) =>
            chr => expressions.Aggregate(ExpressionResult.Fail(chr), (last, exp) => last.IsMatch ? last : exp(chr));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public static Expression AnythingBefore(Expression exp) =>
            chr => exp(chr) switch {
                var r when r.IsMatch => r,
                var r => ExpressionResult.Success(r.Start, chr.IsLast ? null : AnythingBefore(exp)(chr.Tail).End)};

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Expression Reduce(Expression left, Expression right) =>
            chr => left(chr) switch {
                var r when r.IsMatch => r.Bind(right(r.End)),
                var r => r};

    }

}