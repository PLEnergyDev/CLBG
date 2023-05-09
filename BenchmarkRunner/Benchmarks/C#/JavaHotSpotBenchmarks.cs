using CsharpRAPL.Benchmarking.Attributes;
using CsharpRAPL.Benchmarking.Lifecycles;
using SocketComm;

namespace BenchmarkRunner.Benchmarks.C_;

public class JavaHotSpotBenchmarks
{
    [Benchmark("Binary Trees", "Binary Tress in Java HotSpot",
        typeof(IpcBenchmarkLifecycle), name: "Java HotSpot BT", skip: false)]
    public static JavaState JavaHotSpotBT(IpcState s)
    {
        var state = JavaBenchmarks.JavaBT(s);
        state.JavaPath = "/usr/lib/jvm/jdk-17/bin/java";
        return state;
    }
    
    [Benchmark("Fannkuch Redux", "Fannkuch Redux in Java HotSpot",
        typeof(IpcBenchmarkLifecycle), name: "Java HotSpot FR", skip: false)]
    public static JavaState JavaHotSpotFR(IpcState s)
    {
        var state = JavaBenchmarks.JavaFR(s);
        state.JavaPath = "/usr/lib/jvm/jdk-17/bin/java";
        return state;
    }
    
    [Benchmark("Fasta", "Fasta in Java HotSpot",
        typeof(IpcBenchmarkLifecycle), name: "Java HotSpot Fasta", skip: false)]
    public static JavaState JavaHotSpotFasta(IpcState s)
    {
        var state = JavaBenchmarks.JavaFasta(s);
        state.JavaPath = "/usr/lib/jvm/jdk-17/bin/java";
        return state;
    }
    
    [Benchmark("NBody", "NBody in Java HotSpot",
        typeof(IpcBenchmarkLifecycle), name: "Java HotSpot NB", skip: false)]
    public static JavaState JavaHotSpotNB(IpcState s)
    {
        var state = JavaBenchmarks.JavaNB(s);
        state.JavaPath = "/usr/lib/jvm/jdk-17/bin/java";
        return state;
    }
    
}