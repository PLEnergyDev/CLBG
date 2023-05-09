using CsharpRAPL.Benchmarking.Attributes;
using CsharpRAPL.Benchmarking.Lifecycles;
using SocketComm;

namespace BenchmarkRunner.Benchmarks.C_;

public class JavaIBMBenchmarks
{
    [Benchmark("Binary Trees", "Binary Tress in Java IBM",
        typeof(IpcBenchmarkLifecycle), name: "Java IBM BT", skip: false)]
    public static JavaState JavaIBMBT(IpcState s)
    {
        var state = JavaBenchmarks.JavaBT(s);
        state.JavaPath = "/usr/lib/jvm/ibm-semeru-certified-17-jre/bin/java";
        return state;
    }
    
    [Benchmark("Fannkuch Redux", "Fannkuch Redux in Java IBM",
        typeof(IpcBenchmarkLifecycle), name: "Java IBM FR", skip: false)]
    public static JavaState JavaIBMFR(IpcState s)
    {
        var state = JavaBenchmarks.JavaFR(s);
        state.JavaPath = "/usr/lib/jvm/ibm-semeru-certified-17-jre/bin/java";
        return state;
    }
    
    [Benchmark("Fasta", "Fasta in Java IBM",
        typeof(IpcBenchmarkLifecycle), name: "Java IBM Fasta", skip: false)]
    public static JavaState JavaIBMFasta(IpcState s)
    {
        var state = JavaBenchmarks.JavaFasta(s);
        state.JavaPath = "/usr/lib/jvm/ibm-semeru-certified-17-jre/bin/java";
        return state;
    }
    
    [Benchmark("NBody", "NBody in Java IBM",
        typeof(IpcBenchmarkLifecycle), name: "Java IBM NB", skip: false)]
    public static JavaState JavaIBMNB(IpcState s)
    {
        var state = JavaBenchmarks.JavaNB(s);
        state.JavaPath = "/usr/lib/jvm/ibm-semeru-certified-17-jre/bin/java";
        return state;
    }
    
}