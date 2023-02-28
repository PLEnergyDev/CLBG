using CsharpRAPL.Benchmarking.Attributes;
using CsharpRAPL.Benchmarking.Attributes.Parameters;
using CsharpRAPL.Benchmarking.Lifecycles;
using SocketComm;

namespace BenchmarkRunner.Benchmarks.C_;

public class IPCTest
{
    //[Benchmark("IPC test", "Test IPC",
     //   typeof(IpcBenchmarkLifecycle),
     //   name: "Cs IPC test", skip: true)]
    public static IpcState IPCTestFunc(IpcState s)
    {
        s.ExecutablePath = "../CSIPC/bin/Release/net6.0/CSIPC";
        s.prerun = (s) =>
        {
            //s.Pipe.ExpectCmd(Cmd.Ready);
            //s.Pipe.SendValue(0,SimpleConversion.NumberToBytes);
            return s;
        };
        s.postrun = (s) =>
        {
            s.Pipe.WriteCmd(Cmd.Ready);
            s.Pipe.ExpectCmd(Cmd.Receive);
            int result = s.Pipe.ReceiveValue(SimpleConversion.BytesToNumber<int>);
            return s;
        };
        
        return s;
    }
    
    [Benchmark("NBody", "NBody in C# over IPC",
        typeof(IpcBenchmarkLifecycle),
        name: "C sharp Nbody IPC", skip: false)]
    public static IpcState IPCNBody(IpcState s)
    {
        s.ExecutablePath = "../CSIPC/bin/Release/net6.0/CSIPC";
        s.prerun = (s) =>
        {
            //s.Pipe.ExpectCmd(Cmd.Ready);
            //s.Pipe.SendValue(0,SimpleConversion.NumberToBytes);
            return s;
        };
        s.postrun = (s) =>
        {
            s.Pipe.WriteCmd(Cmd.Ready);
            s.Pipe.ExpectCmd(Cmd.Receive);
            var result = s.Pipe.ReceiveValue(SimpleConversion.BytesToNumber<double>);
            return s;
        };
        
        return s;
    }

    [Benchmark("Binary Trees", "Binary trees in C# over IPC",
        typeof(IpcBenchmarkLifecycle),
        name: "C sharp BT IPC", skip: false)]
    public static IpcState IPCBinaryTrees(IpcState s)
    {
        s.ExecutablePath = "../CSIPC/bin/Release/net6.0/CSIPC";
        s.prerun = (s) =>
        {
            //s.Pipe.ExpectCmd(Cmd.Ready);
            //s.Pipe.SendValue(0,SimpleConversion.NumberToBytes);
            return s;
        };
        s.postrun = (s) =>
        {
            s.Pipe.WriteCmd(Cmd.Ready);
            s.Pipe.ExpectCmd(Cmd.Receive);
            var result = s.Pipe.ReceiveValue(SimpleConversion.BytesToNumber<int>);
            return s;
        };
        
        return s;
    }

    [Benchmark("Fannkuch redux", "Fannkuch redux in C# over IPC", typeof(IpcBenchmarkLifecycle),
        name: "C sharp FR IPC")]
    public static IpcState IPCFR(IpcState s)
    {
        return IPCBinaryTrees(s);
    }
    [Benchmark("Fasta", "Fasta in C# over IPC", typeof(IpcBenchmarkLifecycle), name:"C sharp FAS IPC") ]
    public static IpcState IPCFasta(IpcState s)
    {
        s.ExecutablePath = "../CSIPC/bin/Release/net6.0/CSIPC";
        s.prerun = (s) =>
        {
            //s.Pipe.ExpectCmd(Cmd.Ready);
            //s.Pipe.SendValue(0,SimpleConversion.NumberToBytes);
            return s;
        };
        s.postrun = (s) =>
        {
            s.Pipe.WriteCmd(Cmd.Ready);
            s.Pipe.ExpectCmd(Cmd.Receive);
            var result = s.Pipe.ReceiveValue(SimpleConversion.BytesToNumber<long>);
            return s;
        };
        
        return s;
    }

    [Benchmark("Fasta", "Fasta optimized in C# over IPC", typeof(IpcBenchmarkLifecycle),name:"C sharp FAS opt IPC")]
    public static IpcState IPCFastaOptimized(IpcState s)
    {
        return IPCFasta(s);
    }

    [Benchmark("Fannkuch redux", "Fannkuch redux optimized in C# over IPC", typeof(IpcBenchmarkLifecycle), name:"C sharp FR opt IPC")]
    public static IpcState IpcFROptimized(IpcState s)
    {
        return IPCFR(s);
    }
    
    
    
    
    
    
}