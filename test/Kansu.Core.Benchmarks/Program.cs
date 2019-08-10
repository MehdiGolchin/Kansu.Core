using BenchmarkDotNet.Running;

namespace Kansu.Benchmarks {

    class Program {

        static void Main(string[] args) {
            BenchmarkRunner.Run<RegexVsExpressions>();
        }

    }

}