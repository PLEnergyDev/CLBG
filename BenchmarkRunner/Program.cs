using System.Diagnostics;
using BenchmarkRunner.Benchmarks.C_;
using CsharpRAPL.Benchmarking;
using CsharpRAPL.CommandLine;
using SocketComm;

var options = CsharpRAPLCLI.Parse(args);
options.PlotResults = true;
options.ZipResults = true;

var suite = new BenchmarkCollector(options.Iterations, options.LoopIterations);
suite.RunAll(false);

/*var pipe = new FPipe("/tmp/test.pipe");
pipe.Connect();

pipe.ExpectCmd(Cmd.Ready);

pipe.ExpectCmd(Cmd.Ready);


ulong li = 10;

pipe.SendValue(li, SimpleConversion.NumberToBytes);

pipe.ExpectCmd(Cmd.Ready);

pipe.WriteCmd(Cmd.Go);

pipe.ExpectCmd(Cmd.Done);

pipe.WriteCmd(Cmd.Ready);

pipe.ExpectCmd(Cmd.Receive);

var res = pipe.ReceiveValue(SimpleConversion.BytesToNumber<long>);

Console.WriteLine(res);

pipe.WriteCmd(Cmd.Exit);


pipe.Dispose();*/