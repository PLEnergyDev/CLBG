
using System.Text;
using SocketComm;
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
        //Console.WriteLine("sent write, expecting receive");
        pipe.ExpectCmd(Cmd.Receive);
        ulong loopiterations = pipe.ReceiveValue(SimpleConversion.BytesToNumber<ulong>);
        //o.Write(Encoding.ASCII.GetBytes($"Received loop iterations: {loopiterations}\n"));
        pipe.WriteCmd(Cmd.Ready);
        pipe.ExpectCmd(Cmd.Receive);
        int i = pipe.ReceiveValue(SimpleConversion.BytesToNumber<int>);
        //o.Write(Encoding.ASCII.GetBytes($"Received input: {i}\n"));
        pipe.WriteCmd(Cmd.Ready);
        pipe.ExpectCmd(Cmd.Go);
//RUn code
        for (ulong n = 0; n < loopiterations; n++)
        {
            i++;
        }

        pipe.WriteCmd(Cmd.Done);
        pipe.ExpectCmd(Cmd.Ready);
        //o.Write(Encoding.ASCII.GetBytes($"Sending result: {i}\n"));
        pipe.SendValue(i, SimpleConversion.NumberToBytes);
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