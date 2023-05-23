
using SocketComm;
using BenchmarkRunner.Benchmarks.C_;
if (args.Length < 1)
{
    Console.Error.WriteLine("usage: [socket]");
    return;
}
//var o = Console.OpenStandardOutput();
var path = args[0];
var pipe = new FPipe(path);
pipe.Connect();
try
{
    pipe.WriteCmd(Cmd.Ready);
    //Console.WriteLine("sent write");
    bool flag = true;
    while (flag)
    {
        pipe.WriteCmd(Cmd.Ready);

        pipe.ExpectCmd(Cmd.Receive);
        ulong loopiterations = pipe.ReceiveValue(SimpleConversion.BytesToNumber<ulong>);
        //Console.WriteLine(loopiterations);
        pipe.WriteCmd(Cmd.Ready);
        pipe.ExpectCmd(Cmd.Go);
//RUn code
        switch (path)
        {
            case "/tmp/IPCNBody.pipe":
                var nbresult = n_body.Start(loopiterations);
                pipe.WriteCmd(Cmd.Done);
                pipe.ExpectCmd(Cmd.Ready);
                pipe.SendValue(nbresult, SimpleConversion.NumberToBytes);
                break;
            case "/tmp/IPCBinaryTrees.pipe":
                //Console.WriteLine("running");
                var btresult = BinaryTrees.BinaryTree(loopiterations);
                pipe.WriteCmd(Cmd.Done);
                pipe.ExpectCmd(Cmd.Ready);
                pipe.SendValue(btresult, SimpleConversion.NumberToBytes);
                break;
            case "/tmp/IPCFR.pipe":
                var frresult = fannkuch_redux.Main(loopiterations);
                pipe.WriteCmd(Cmd.Done);
                pipe.ExpectCmd(Cmd.Ready);
                pipe.SendValue(frresult, SimpleConversion.NumberToBytes);
                break;
            case "/tmp/IPCFasta.pipe":
                var fastaresult = Fasta.Main(loopiterations);
                pipe.WriteCmd(Cmd.Done);
                pipe.ExpectCmd(Cmd.Ready);
                pipe.SendValue(fastaresult, SimpleConversion.NumberToBytes);
                break;
            case "/tmp/IPCFastaOptimized.pipe":
                var fastaopt = Fasta_optimized.Main(loopiterations);
                pipe.WriteCmd(Cmd.Done);
                pipe.ExpectCmd(Cmd.Ready);
                pipe.SendValue(fastaopt, SimpleConversion.NumberToBytes);
                break;
            case "/tmp/IPCFROptimized.pipe":
                var frOpt = FR_optimized.Main(loopiterations);
                pipe.WriteCmd(Cmd.Done);
                pipe.ExpectCmd(Cmd.Ready);
                pipe.SendValue(frOpt, SimpleConversion.NumberToBytes);
                break;
        }
        Cmd c = pipe.ReadCmd();
        if (c != Cmd.Ready)
        {
            flag = false;
        } 
    }
    
    pipe.ExpectCmd(Cmd.Exit);
    pipe.Dispose();
    //o.Close();
    Console.WriteLine("Benchmark done!");
}
catch (Exception e){
    Console.WriteLine(e.ToString());
}