using CsharpRAPL.Benchmarking.Attributes;
using CsharpRAPL.Benchmarking.Lifecycles;
using SocketComm;

namespace BenchmarkRunner.Benchmarks.C_;
[SkipBenchmarks]
public class CBenchmarks
{
    [Benchmark("NBody", "NBody in C over IPC",
        typeof(IpcBenchmarkLifecycle),
        name: "C Nbody ", skip: false)]
    public static CState CNBody(IpcState old)
    {
        var s = new CState(old)
        {
            //ExecutablePath = "../CSIPC/bin/Release/net6.0/CSIPC",
            BenchmarkSignature = "double result = NBody(loopIterations)",
            SendSignature = "sendValue(s, &result, sizeof(double), numberToByteGeneric);",
            LibPath = "Benchmarks/C",
            HeaderFile = "NBody.h", 
            CFile = "NBody.c",
            AdditionalCompilerOptions = "-march=ivybridge ",
            prerun = (s) =>
            {
                //s.Pipe.ExpectCmd(Cmd.Ready);
                //s.Pipe.SendValue(0,SimpleConversion.NumberToBytes);
                return s;
            },
            postrun = (s) =>
            {
                s.Pipe.WriteCmd(Cmd.Ready);
                s.Pipe.ExpectCmd(Cmd.Receive);
                var result = s.Pipe.ReceiveValue(SimpleConversion.BytesToNumber<double>);
                return s;
            }
        };

        return s;
    }
    
    [Benchmark("Fasta", "Fasta in C over IPC",
        typeof(IpcBenchmarkLifecycle),
        name: "C Fasta ", skip: false)]
    public static CState CFasta(IpcState old)
    {
        var s = new CState(old)
        {
            BenchmarkSignature = "int result = Fasta(loopIterations)",
            SendSignature = "sendValue(s, &result, sizeof(int), numberToByteGeneric);",
            LibPath = "Benchmarks/C",
            HeaderFile = "Fasta.h", 
            CFile = "Fasta.c",
            prerun = (s) =>
            {
                //s.Pipe.ExpectCmd(Cmd.Ready);
                //s.Pipe.SendValue(0,SimpleConversion.NumberToBytes);
                return s;
            },
            postrun = (s) =>
            {
                s.Pipe.WriteCmd(Cmd.Ready);
                s.Pipe.ExpectCmd(Cmd.Receive);
                var result = s.Pipe.ReceiveValue(SimpleConversion.BytesToNumber<int>);
                return s;
            }
        };

        return s;
    }
    
    [Benchmark("Binary Trees", "Binary trees in C over IPC",
        typeof(IpcBenchmarkLifecycle),
        name: "C BT ", skip: false)]
    public static CState CBT(IpcState old)
    {
        var s = new CState(old)
        {
            BenchmarkSignature = "long result = BinaryTrees(loopIterations)",
            SendSignature = $"sendValue(s, &result, sizeof(long), numberToByteGeneric);",
            LibPath = "Benchmarks/C",
            HeaderFile = "BinaryTrees.h", 
            CFile = "BinaryTrees.c",
            Includes = new List<string>{"apr-1"},
            Libs = new List<string>{"libapr-1.a"},
            prerun = (s) =>
            {
                //s.Pipe.ExpectCmd(Cmd.Ready);
                //s.Pipe.SendValue(0,SimpleConversion.NumberToBytes);
                return s;
            },
            postrun = (s) =>
            {
                s.Pipe.WriteCmd(Cmd.Ready);
                s.Pipe.ExpectCmd(Cmd.Receive);
                var result = s.Pipe.ReceiveValue(SimpleConversion.BytesToNumber<long>);
                return s;
            }
        };

        return s;
    }
    [Benchmark("Fannkuch Redux", "Fannkuch redux in C over IPC",
        typeof(IpcBenchmarkLifecycle),
        name: "C FR ", skip: false)]
    public static CState CFR(IpcState old)
    {
        var s = new CState(old)
        {
            BenchmarkSignature = "long result = FannkuchRedux(loopIterations)",
            SendSignature = "sendValue(s, &result, sizeof(long), numberToByteGeneric);",
            LibPath = "Benchmarks/C",
            HeaderFile = "FannkuchRedux.h", 
            CFile = "FannkuchRedux.c",
            AdditionalCompilerOptions = "-march=ivybridge ",
            prerun = (s) =>
            {
                return s;
            },
            postrun = (s) =>
            {
                s.Pipe.WriteCmd(Cmd.Ready);
                s.Pipe.ExpectCmd(Cmd.Receive);
                var result = s.Pipe.ReceiveValue(SimpleConversion.BytesToNumber<long>);
                return s;
            }
        };

        return s;
    }
}