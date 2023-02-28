using CsharpRAPL.Benchmarking.Attributes;
using CsharpRAPL.Benchmarking.Attributes.Parameters;
using CsharpRAPL.Benchmarking.Lifecycles;
using SocketComm;

namespace BenchmarkRunner.Benchmarks.C_;

public class IPCTest
{
    [Benchmark("IPC test", "Test IPC",
        typeof(IpcBenchmarkLifecycle),
        name: "Cs IPC test", skip: false)]
    public static IpcState IPCTestFunc(IpcState s)
    {
        s.ExecutablePath = "../CSIPC/bin/Release/net6.0/CSIPC";
        s.prerun = (s) =>
        {
            s.Pipe.ExpectCmd(Cmd.Ready);
            s.Pipe.SendValue(0,SimpleConversion.NumberToBytes);
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
}