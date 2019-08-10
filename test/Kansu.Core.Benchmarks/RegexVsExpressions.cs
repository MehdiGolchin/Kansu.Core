using System.Text.RegularExpressions;
using BenchmarkDotNet.Attributes;
using Kansu.Collection;
using Kansu.Text;
using static Kansu.Text.Expressions;

namespace Kansu.Benchmarks {

    [RankColumn, RPlotExporter]
    public class RegexVsExpressions {

        private const string MatchSpaces = "   abc";
        private readonly SinglyLinked<char> _chars = MatchSpaces.ToSinglyLinked();
        private readonly Regex _regex = new Regex("\\s+");
        private readonly Expression _expression = OneOrMore(Space());

        [Benchmark]
        public bool Regex() => _regex.IsMatch(MatchSpaces);
        
        [Benchmark]
        public bool Expressions() => _expression(_chars).IsMatch;

    }

}