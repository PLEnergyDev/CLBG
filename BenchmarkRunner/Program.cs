using System.Diagnostics;
using CsharpRAPL.Benchmarking;
using CsharpRAPL.CommandLine;

var options = CsharpRAPLCLI.Parse(args);
options.PlotResults = true;
options.ZipResults = true;

var suite = new BenchmarkCollector(options.Iterations, options.LoopIterations);
suite.RunAll(false);