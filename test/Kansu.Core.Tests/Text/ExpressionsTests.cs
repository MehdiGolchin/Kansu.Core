using System.Linq;
using Kansu.Collection;
using Shouldly;
using Xunit;
using static Kansu.Text.Expressions;

namespace Kansu.Text.Tests {

    public class ExpressionsTests {

        [Fact]
        public void Space_should_match() {
            // arrange
            var buffer = " n = 1".ToSinglyLinked();
            // act
            var (isMatch, start, end) = Space()(buffer);
            // assert
            isMatch.ShouldBeTrue();
            start.Head.ShouldBe(' ');
            end.Head.ShouldBe('n');
        }

        [Theory]
        [InlineData("")]
        [InlineData("n=1")]
        [InlineData("n = 1")]
        public void Space_should_not_match(string text) {
            // arrange
            var buffer = text.ToSinglyLinked();
            // act
            var (isMatch, start, end) = Space()(buffer);
            // assert
            isMatch.ShouldBeFalse();
            start.ShouldBe(buffer);
            end.ShouldBe(buffer);
        }
        
        [Theory]
        [InlineData("\n")]
        [InlineData("\r\n")]
        public void Newline_should_match(string text) {
            // arrange
            var buffer = text.ToSinglyLinked();
            // act
            var (isMatch, start, end) = Newline()(buffer);
            // assert
            isMatch.ShouldBeTrue();
            start.Head.ShouldBe(text[0]);
            end.ShouldBeNull();
        }

        [Theory]
        [InlineData("")]
        [InlineData("\r")]
        public void Newline_should_not_match(string text) {
            // arrange
            var buffer = text.ToSinglyLinked();
            // act
            var (isMatch, start, end) = Space()(buffer);
            // assert
            isMatch.ShouldBeFalse();
            start.ShouldBe(buffer);
            end.ShouldBe(buffer);
        }

        [Fact]
        public void Character_should_match() {
            // arrange
            var buffer = "abc".ToSinglyLinked();
            // act
            var (isMatch, start, end) = Character('a')(buffer);
            // assert
            isMatch.ShouldBeTrue();
            start.Head.ShouldBe('a');
            end.Head.ShouldBe('b');
        }

        [Theory]
        [InlineData("")]
        [InlineData(" a")]
        [InlineData("bac")]
        public void Character_should_not_match(string text) {
            // arrange
            var buffer = text.ToSinglyLinked();
            // act
            var (isMatch, start, end) = Character('a')(buffer);
            // assert
            isMatch.ShouldBeFalse();
            start.ShouldBe(end);
        }

        [Fact]
        public void ZeroOrMore_should_match_empty_string() {
            // arrange
            var buffer = "".ToSinglyLinked();
            // act
            var (isMatch, start, end) = ZeroOrMore(Space())(buffer);
            // assert
            isMatch.ShouldBeTrue();
            start.ShouldBeNull();
            end.ShouldBeNull();
        }
        
        [Theory]
        [InlineData(" ")]
        [InlineData("  ")]
        public void ZeroOrMore_should_match(string text) {
            // arrange
            var buffer = text.ToSinglyLinked();
            // act
            var (isMatch, start, end) = ZeroOrMore(Space())(buffer);
            // assert
            isMatch.ShouldBeTrue();
            start.Head.ShouldBe(text.First());
            end.ShouldBeNull();
        }

        [Theory]
        [InlineData(" ")]
        [InlineData("  ")]
        [InlineData("   ")]
        public void OneOrMore_should_match(string text) {
            // arrange
            var buffer = text.ToSinglyLinked();
            // act
            var (isMatch, start, end) = OneOrMore(Space())(buffer);
            // assert
            isMatch.ShouldBeTrue();
            start.Head.ShouldBe(' ');
            end.ShouldBeNull();
        }

        [Theory]
        [InlineData("")]
        [InlineData("a = 1")]
        public void OneOrMore_should_not_match(string text) {
            // arrange
            var buffer = text.ToSinglyLinked();
            // act
            var (isMatch, start, end) = OneOrMore(Space())(buffer);
            // assert
            isMatch.ShouldBeFalse();
            start.ShouldBe(end);
        }

        [Fact]
        public void Reduce_should_combine_two_expression() {
            // arrange
            var buffer = "abc".ToSinglyLinked();
            var exp = Reduce(Character('a'), Character('b'));
            // act
            var (isMatch, start, end) = exp(buffer);
            // assert
            isMatch.ShouldBeTrue();
            start.Head.ShouldBe('a');
            end.Head.ShouldBe('c');
        }

        [Fact]
        public void Sequence_should_match() {
            // arrange
            var buffer = "# a simple comment\n".ToSinglyLinked();
            var exp = Sequence(
                Character('#'),
                ZeroOrMore(Space()),
                AnythingBefore(Newline())
            );
            // act
            var (isMatch, start, end) = exp(buffer);
            // assert
            isMatch.ShouldBeTrue();
            start.Head.ShouldBe('#');
            end.ShouldBeNull();
        }

        [Theory]
        [InlineData("ac")]
        [InlineData("bc")]
        public void OneOf_should_match(string text) {
            // arrange
            var buffer = text.ToSinglyLinked();
            var exp = OneOf(
                Character('a'),
                Character('b')
            );
            // act
            var (isMatch, start, end) = exp(buffer);
            // assert
            isMatch.ShouldBeTrue();
            start.Head.ShouldBe(text[0]);
            end.Head.ShouldBe('c');
        }

        [Theory]
        [InlineData("Unix-based newline\n")]
        [InlineData("Windows-based newline\r\n")]
        [InlineData("Without newline")]
        public void AnythingBefore_should_match(string text) {
            // arrange
            var buffer = text.ToSinglyLinked();
            var exp = AnythingBefore(Newline());
            // act
            var (isMatch, start, end) = exp(buffer);
            // assert
            isMatch.ShouldBeTrue();
            start.Head.ShouldBe(text[0]);
            end.ShouldBeNull();
        }

    }

}