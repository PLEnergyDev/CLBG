using CsharpRAPL.Benchmarking.Attributes;
using CsharpRAPL.Benchmarking.Lifecycles;
using SocketComm;

namespace BenchmarkRunner.Benchmarks.C_;

public class CBenchmarks
{
    //[Benchmark("NBody", "NBody in C over IPC",typeof(IpcBenchmarkLifecycle),name: "C Nbody ", skip: false)]
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
    
    [Benchmark("NBody", "NBody in C over IPC",typeof(IpcBenchmarkLifecycle),name: "C Nbody O1", skip: false)]
    public static CState CNBodyO1(IpcState s)
    {
        var state = CNBody(s);
        state.AdditionalCompilerOptions += "-O1";
        return state;
    }
    
    [Benchmark("NBody", "NBody in C over IPC",typeof(IpcBenchmarkLifecycle),name: "C Nbody O2", skip: false)]
    public static CState CNBodyO2(IpcState s)
    {
        var state = CNBody(s);
        state.AdditionalCompilerOptions += "-O2";
        return state;
    }
    
    [Benchmark("NBody", "NBody in C over IPC",typeof(IpcBenchmarkLifecycle),name: "C Nbody O3", skip: false)]
    public static CState CNBodyO3(IpcState s)
    {
        var state = CNBody(s);
        state.AdditionalCompilerOptions += "-O3";
        return state;
    }
    
    //[Benchmark("Fasta", "Fasta in C over IPC", typeof(IpcBenchmarkLifecycle), name: "C Fasta ", skip: false)]
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

    [Benchmark("Fasta", "Fasta in C over IPC", typeof(IpcBenchmarkLifecycle), name: "C Fasta O1", skip: false)]
    public static CState CFastaO1(IpcState s)
    {
        var state = CFasta(s);
        state.AdditionalCompilerOptions = "-O1";
        return state;
    }
    
    [Benchmark("Fasta", "Fasta in C over IPC", typeof(IpcBenchmarkLifecycle), name: "C Fasta O2", skip: false)]
    public static CState CFastaO2(IpcState s)
    {
        var state = CFasta(s);
        state.AdditionalCompilerOptions = "-O2";
        return state;
    }
    
    [Benchmark("Fasta", "Fasta in C over IPC", typeof(IpcBenchmarkLifecycle), name: "C Fasta O3", skip: false)]
    public static CState CFastaO3(IpcState s)
    {
        var state = CFasta(s);
        state.AdditionalCompilerOptions = "-O3";
        return state;
    }
    
    //[Benchmark("Binary Trees", "Binary trees in C over IPC", typeof(IpcBenchmarkLifecycle), name: "C BT ", skip: false)]
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

    [Benchmark("Binary Trees", "Binary trees in C over IPC", typeof(IpcBenchmarkLifecycle), name: "C BT O1", skip: false)]
    public static CState CBTO1(IpcState s)
    {
        var state = CBT(s);
        state.AdditionalCompilerOptions = "-O1";
        return state;
    }
    
    [Benchmark("Binary Trees", "Binary trees in C over IPC", typeof(IpcBenchmarkLifecycle), name: "C BT O2", skip: false)]
    public static CState CBTO2(IpcState s)
    {
        var state = CBT(s);
        state.AdditionalCompilerOptions = "-O2";
        return state;
    }

    [Benchmark("Binary Trees", "Binary trees in C over IPC", typeof(IpcBenchmarkLifecycle), name: "C BT O3", skip: false)]
    public static CState CBTO3(IpcState s)
    {
        var state = CBT(s);
        state.AdditionalCompilerOptions = "-O3";
        return state;
    }

    
    //[Benchmark("Fannkuch Redux", "Fannkuch redux in C over IPC", typeof(IpcBenchmarkLifecycle), name: "C FR ", skip: false)]
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

    [Benchmark("Fannkuch Redux", "Fannkuch redux in C over IPC", typeof(IpcBenchmarkLifecycle), name: "C FR O1", skip: false)]
    public static CState CFRO1(IpcState s)
    {
        var state = CFR(s);
        state.AdditionalCompilerOptions += "-O1";
        return state;
    }
    
    [Benchmark("Fannkuch Redux", "Fannkuch redux in C over IPC", typeof(IpcBenchmarkLifecycle), name: "C FR O2", skip: false)]
    public static CState CFRO2(IpcState s)
    {
        var state = CFR(s);
        state.AdditionalCompilerOptions += "-O2";
        return state;
    }
    
    [Benchmark("Fannkuch Redux", "Fannkuch redux in C over IPC", typeof(IpcBenchmarkLifecycle), name: "C FR O3", skip: false)]
    public static CState CFRO3(IpcState s)
    {
        var state = CFR(s);
        state.AdditionalCompilerOptions += "-O3";
        return state;
    }
}