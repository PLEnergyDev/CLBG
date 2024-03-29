using CsharpRAPL.Benchmarking.Attributes;
using CsharpRAPL.Benchmarking.Attributes.Parameters;
using System;
using System.Text;
using System.Buffers;
using System.Threading;
using System.Runtime.CompilerServices;

namespace BenchmarkRunner.Benchmarks.C_;

public class Fasta_optimized
{
     const int Width = 60;
    const int Width1 = 61;
    const int LinesPerBlock = 2048;
    const int BlockSize = Width * LinesPerBlock;
    const int BlockSize1 = Width1 * LinesPerBlock;
    const int IM = 139968;
    const float FIM = 1F/139968F;
    const int IA = 3877;
    const int IC = 29573;
    const int SEED = 42;
    static readonly ArrayPool<byte> bytePool = ArrayPool<byte>.Shared;
    static readonly ArrayPool<int> intPool = ArrayPool<int>.Shared;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static byte[] Bytes(int i, int[] rnds, float[] ps, byte[] vs)
    {
        var a = bytePool.Rent(BlockSize1);
        var s = a.AsSpan(0, i);
        for (i = 1; i < s.Length; i++)
        {
            var p = rnds[i] * FIM;
            int j = 0;
            while (ps[j] < p) j++;
            s[i] = vs[j];
        }
        return a;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int[] Rnds(int i, int j, ref int seed)
    {
        var a = intPool.Rent(BlockSize1);
        var s = a.AsSpan(0, i);
        s[0] = j;
        for (i = 1, j = Width; i < s.Length; i++)
        {
            if (j-- == 0)
            {
                j = Width;
                s[i] = IM * 3 / 2;
            }
            else
            {
                s[i] = seed = (seed * IA + IC) % IM;
            }
        }
        return a;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int WriteRandom(int n, int offset, int seed, byte[] vs, float[] ps,
        Tuple<byte[], int>[] blocks)
    {
        // make cumulative
        var total = ps[0];
        for (int i = 1; i < ps.Length; i++)
            ps[i] = total += ps[i];

        void create(object o)
        {
            var rnds = (int[])o;
            blocks[rnds[0]] =
                Tuple.Create(Bytes(BlockSize1, rnds, ps, vs), BlockSize1);
            intPool.Return(rnds);
        }

        var createDel = (WaitCallback)create;

        for (int i = offset; i < offset + (n - 1) / BlockSize; i++)
        {
            createDel(Rnds(BlockSize1, i, ref seed));
            //ThreadPool.QueueUserWorkItem(createDel,
            //    Rnds(BlockSize1, i, ref seed));
        }

        var remaining = (n - 1) % BlockSize + 1;
        var l = remaining + (remaining - 1) / Width + 1;

        var rnds = Rnds(l, offset + (n - 1) / BlockSize, ref seed);
        blocks[rnds[0]] = Tuple.Create(Bytes(l, rnds, ps, vs), l);
        intPool.Return(rnds);
        
        /*ThreadPool.QueueUserWorkItem(o =>
        {
            var rnds = (int[])o;
            blocks[rnds[0]] = Tuple.Create(Bytes(l, rnds, ps, vs), l);
            intPool.Return(rnds);
        }, Rnds(l, offset + (n - 1) / BlockSize, ref seed));*/

        return seed;
    }
    
    [Benchmark("Fasta","Fasta in C# with optimizations", name:"C sharp FAS opt", skip: false)]
    public static long Main([BenchmarkLoopiterations] ulong LoopIterations)
    {
        int n = 800;
        long result = 0;
        for (ulong u = 0; u < LoopIterations; u++)
        {
            var o = new MemoryStream();
            var blocks = new Tuple<byte[], int>[
            (3 * n - 1) / BlockSize + (5 * n - 1) / BlockSize + 3];
            
            var seed = WriteRandom(3 * n, 0, SEED,
                new[] { (byte)'a', (byte)'c', (byte)'g', (byte)'t',
                    (byte)'B', (byte)'D', (byte)'H', (byte)'K', (byte)'M',
                    (byte)'N', (byte)'R', (byte)'S', (byte)'V', (byte)'W',
                    (byte)'Y', (byte)'\n' },
                new[] { 0.27F,0.12F,0.12F,0.27F,0.02F,0.02F,0.02F,0.02F,0.02F,
                    0.02F,0.02F,0.02F,0.02F,0.02F,0.02F,1.00F }, blocks);

            WriteRandom(5 * n, (3 * n - 1) / BlockSize + 2, seed,
                new byte[] { (byte)'a', (byte)'c', (byte)'g', (byte)'t',
                    (byte)'\n' },
                new[] { 0.3029549426680F, 0.1979883004921F,
                    0.1975473066391F, 0.3015094502008F,
                    1.0F }, blocks);
            
            /*ThreadPool.QueueUserWorkItem(_ =>
            {
                var seed = WriteRandom(3 * n, 0, SEED,
                new[] { (byte)'a', (byte)'c', (byte)'g', (byte)'t',
                    (byte)'B', (byte)'D', (byte)'H', (byte)'K', (byte)'M',
                    (byte)'N', (byte)'R', (byte)'S', (byte)'V', (byte)'W',
                    (byte)'Y', (byte)'\n' },
                new[] { 0.27F,0.12F,0.12F,0.27F,0.02F,0.02F,0.02F,0.02F,0.02F,
                    0.02F,0.02F,0.02F,0.02F,0.02F,0.02F,1.00F }, blocks);

                WriteRandom(5 * n, (3 * n - 1) / BlockSize + 2, seed,
                new byte[] { (byte)'a', (byte)'c', (byte)'g', (byte)'t',
                    (byte)'\n' },
                new[] { 0.3029549426680F, 0.1979883004921F,
                        0.1975473066391F, 0.3015094502008F,
                        1.0F }, blocks);
            });*/

            o.Write(Encoding.ASCII.GetBytes(">ONE Homo sapiens alu"), 0, 21);
            var table = Encoding.ASCII.GetBytes(
            "GGCCGGGCGCGGTGGCTCACGCCTGTAATCCCAGCACTTTGG" +
            "GAGGCCGAGGCGGGCGGATCACCTGAGGTCAGGAGTTCGAGA" +
            "CCAGCCTGGCCAACATGGTGAAACCCCGTCTCTACTAAAAAT" +
            "ACAAAAATTAGCCGGGCGTGGTGGCGCGCGCCTGTAATCCCA" +
            "GCTACTCGGGAGGCTGAGGCAGGAGAATCGCTTGAACCCGGG" +
            "AGGCGGAGGTTGCAGTGAGCCGAGATCGCGCCACTGCACTCC" +
            "AGCCTGGGCGACAGAGCGAGACTCCGTCTCAAAAA");
            var linesPerBlock = (LinesPerBlock / 287 + 1) * 287;
            var repeatedBytes = bytePool.Rent(Width1 * linesPerBlock);
            for (int i = 0; i <= linesPerBlock * Width - 1; i++)
                repeatedBytes[1 + i + i / Width] = table[i % 287];
            for (int i = 0; i <= (Width * linesPerBlock - 1) / Width; i++)
                repeatedBytes[i * Width1] = (byte)'\n';
            for (int i = 1; i <= (2 * n - 1) / (Width * linesPerBlock); i++)
                o.Write(repeatedBytes, 0, Width1 * linesPerBlock);
            var remaining = (2 * n - 1) % (Width * linesPerBlock) + 1;
            o.Write(repeatedBytes, 0, remaining + (remaining - 1) / Width + 1);
            bytePool.Return(repeatedBytes);
            o.Write(Encoding.ASCII.GetBytes("\n>TWO IUB ambiguity codes"), 0, 25);

            blocks[(3 * n - 1) / BlockSize + 1] = Tuple.Create
                (Encoding.ASCII.GetBytes("\n>THREE Homo sapiens frequency"), 30);

            for (int i = 0; i < blocks.Length; i++)
            {
                Tuple<byte[], int> t;
                while ((t = blocks[i]) == null) Thread.Sleep(0);
                t.Item1[0] = (byte)'\n';
                o.Write(t.Item1, 0, t.Item2);
                if (t.Item2 == BlockSize1) bytePool.Return(t.Item1);
            }

            o.WriteByte((byte)'\n');
            result += o.Length;
        }

        return result;

    }
}