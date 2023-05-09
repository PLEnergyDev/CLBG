using CsharpRAPL.Benchmarking.Attributes;
using CsharpRAPL.Benchmarking.Lifecycles;
using SocketComm;

namespace BenchmarkRunner.Benchmarks.C_;

public class JavaGraalVMBenchmarks
{
    [Benchmark("Binary Trees", "Binary Tress in Java GraalVM",
        typeof(IpcBenchmarkLifecycle), name: "Java GraalVM BT", skip: false)]
    public static JavaState JavaGraalBT(IpcState s)
    {
        var state = JavaBenchmarks.JavaBT(s);
        state.JavaPath = "/usr/lib/jvm/graalvm-ce-java17-22.2.0/bin/java";
        return state;
    }
    
    [Benchmark("Fannkuch Redux", "Fannkuch Redux in Java OpenJDK",
        typeof(IpcBenchmarkLifecycle), name: "Java GraalVM FR", skip: false)]
    public static JavaState JavaGraalFR(IpcState s)
    {
        var state = JavaBenchmarks.JavaFR(s);
        state.JavaPath = "/usr/lib/jvm/graalvm-ce-java17-22.2.0/bin/java";
        return state;
    }
    
    [Benchmark("Fasta", "Fasta in Java GraalVM",
        typeof(IpcBenchmarkLifecycle), name: "Java GraalVM Fasta", skip: false)]
    public static JavaState JavaGraalVMFasta(IpcState s)
    {
        var state = JavaBenchmarks.JavaFasta(s);
        state.JavaPath = "/usr/lib/jvm/graalvm-ce-java17-22.2.0/bin/java";
        return state;
    }
    
    [Benchmark("NBody", "NBody in Java GraalVM",
        typeof(IpcBenchmarkLifecycle), name: "Java GraalVM NB", skip: false)]
    public static JavaState JavaGraalVMNB(IpcState s)
    {
        var state = JavaBenchmarks.JavaNB(s);
        state.JavaPath = "/usr/lib/jvm/graalvm-ce-java17-22.2.0/bin/java";
        return state;
    }
    
}