using CsharpRAPL.Benchmarking.Attributes;
using CsharpRAPL.Benchmarking.Lifecycles;
using SocketComm;

namespace BenchmarkRunner.Benchmarks.C_;

public class JavaBenchmarks
{
    [Benchmark("Binary Trees", "Binary Tress in Java OpenJDK",
        typeof(IpcBenchmarkLifecycle), name: "Java OpenJDK BT", skip: false)]
    public static JavaState JavaBT(IpcState s)
    {
        return new JavaState(s)
        {
            LibPath = "Benchmarks/Java",
            JavaFile = "BinaryTrees.java",
            BenchmarkSignature = "var result = BinaryTrees.binaryTrees(loopIterations);",
            SendSignature = "pipe.SendValue(result, (val) -> ByteBuffer.allocate(8).putLong(val));",
            postrun = (s) =>
            {
                s.Pipe.WriteCmd(Cmd.Ready);
                s.Pipe.ExpectCmd(Cmd.Receive);
                var result = s.Pipe.ReceiveValue(SimpleConversion.BytesToNumber<long>);
                return s;
            }
        };
    }
    
    [Benchmark("Fannkuch Redux", "Fannkuch Redux in Java OpenJDK",
        typeof(IpcBenchmarkLifecycle), name: "Java OpenJDK FR", skip: false)]
    public static JavaState JavaFR(IpcState s)
    {
        return new JavaState(s)
        {
            LibPath = "Benchmarks/Java",
            JavaFile = "FannkuchRedux.java",
            BenchmarkSignature = "var result = FannkuchRedux.fannkuchRedux(loopIterations);",
            SendSignature = "pipe.SendValue(result, (val) -> ByteBuffer.allocate(4).putInt(val));",
            postrun = (s) =>
            {
                s.Pipe.WriteCmd(Cmd.Ready);
                s.Pipe.ExpectCmd(Cmd.Receive);
                var result = s.Pipe.ReceiveValue(SimpleConversion.BytesToNumber<int>);
                return s;
            }
        };
    }
    
    [Benchmark("Fasta", "Fasta in Java OpenJDK",
        typeof(IpcBenchmarkLifecycle), name: "Java OpenJDK Fasta", skip: false)]
    public static JavaState JavaFasta(IpcState s)
    {
        return new JavaState(s)
        {
            LibPath = "Benchmarks/Java",
            JavaFile = "Fasta.java",
            BenchmarkSignature = "var result = Fasta.fasta(loopIterations);",
            SendSignature = "pipe.SendValue(result, (val) -> ByteBuffer.allocate(8).putLong(val));",
            postrun = (s) =>
            {
                s.Pipe.WriteCmd(Cmd.Ready);
                s.Pipe.ExpectCmd(Cmd.Receive);
                var result = s.Pipe.ReceiveValue(SimpleConversion.BytesToNumber<long>);
                return s;
            }
        };
    }
    
    [Benchmark("NBody", "NBody in Java OpenJDK",
        typeof(IpcBenchmarkLifecycle), name: "Java OpenJDK NB", skip: false)]
    public static JavaState JavaNB(IpcState s)
    {
        return new JavaState(s)
        {
            LibPath = "Benchmarks/Java",
            JavaFile = "NBody.java",
            BenchmarkSignature = "var result = NBody.nbody(loopIterations);",
            SendSignature = "pipe.SendValue(result, (val) -> ByteBuffer.allocate(8).putDouble(val));",
            postrun = (s) =>
            {
                s.Pipe.WriteCmd(Cmd.Ready);
                s.Pipe.ExpectCmd(Cmd.Receive);
                var result = s.Pipe.ReceiveValue(SimpleConversion.BytesToNumber<double>);
                return s;
            }
        };
    }
    
}