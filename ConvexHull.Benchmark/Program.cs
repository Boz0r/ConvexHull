using BenchmarkDotNet.Running;
using ConvexHull.Benchmark;

// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");

var bridgeSummary = BenchmarkRunner.Run<KSBridgeBenchmarks>();
var algorithmSummary = BenchmarkRunner.Run<AlgorithmBenchmarks>();