using CsharpRAPL.Benchmarking.Attributes;
using CsharpRAPL.Benchmarking.Attributes.Parameters;
using CsharpRAPL.Benchmarking.Lifecycles;

namespace BenchmarkRunner.Benchmarks.C_;

public class BinaryTrees
{
    const int MinDepth = 4;
    const int NoTasks = 1;
    
    [Benchmark("Binary Trees","Binary trees in C#", name:"C sharp BT", skip: false)]
    public static int BinaryTree([BenchmarkLoopiterations] ulong LoopIterations)
    {
        int maxDepth = 8;
        int result = 0;

        //Console.WriteLine(string.Concat("stretch tree of depth ", maxDepth + 1,"\t check: ", (bottomUpTree(maxDepth + 1)).itemCheck()));
        var longLivedTree = bottomUpTree(maxDepth);
        for (ulong i = 0; i < LoopIterations; i++)
        {
            

            //var results = new string[(maxDepth - MinDepth) / 2 + 1];
            int depth = maxDepth;
            int n = (1 << depth + MinDepth) / NoTasks;
            var tasks = new int[NoTasks];
            for (int t = 0; t < tasks.Length; t++)
            {
                var checkt = 0;
                for (int it = n; it > 0; it--)
                    checkt += (bottomUpTree(depth)).itemCheck();
                tasks[t] = checkt;
            } 
            var check = tasks[0];
            for (int t = 1; t < tasks.Length; t++)
                check += tasks[t];
            result += check;
        }
        
            //results[i] = string.Concat(n * NoTasks, "\t trees of depth ",
              //  depth, "\t check: ", check);
              //for (int i = 0; i < results.Length; i++)
            //Console.WriteLine(results[i]);

        //Console.WriteLine(string.Concat("long lived tree of depth ", maxDepth,"\t check: ", longLivedTree.itemCheck()));
        return result;
    }
    
    static TreeNode bottomUpTree(int depth) 
    {
        if (0 < depth) {
            return new TreeNode(bottomUpTree(depth - 1), bottomUpTree(depth - 1));
        }
        return new TreeNode(null,null);
    }

    private class TreeNode
    {
        readonly TreeNode? left, right;
        
        internal TreeNode(TreeNode left, TreeNode right) 
        {
            this.left = left;
            this.right = right;
        }     

        internal int itemCheck()
        {
            if (left == null) {
                return 1;
            }
            return 1 + left.itemCheck() + right.itemCheck();
        }
    }   
}