import JSocketComm.Cmd;
import JSocketComm.FPipe;
import JSocketComm.PipeCmdException;

import java.io.IOException;
import java.nio.ByteBuffer;

public class Main {
    public static void main(String[] args) throws Exception {
        if(args.length == 0){
            System.err.printf("Usage: JavaBench [socket]%n");
            throw new IllegalArgumentException("Missing commandline argument [socket]");
        }
        try{
            var pipe = new FPipe(args[0]);

            pipe.WriteCmd(Cmd.Ready);

            var flag = true;
            while(flag){
                pipe.WriteCmd(Cmd.Ready);

                pipe.ExpectCmd(Cmd.Receive);

                var loopIterations = pipe.ReceiveValue(ByteBuffer::getLong);

                pipe.WriteCmd(Cmd.Ready);
                pipe.ExpectCmd(Cmd.Go);

                ///Compute benchmark here


                pipe.WriteCmd(Cmd.Done);
                pipe.ExpectCmd(Cmd.Ready);

                ///Send return value here



                var c = pipe.ReadCmd();

                flag = c == Cmd.Ready;
            }

            System.out.println("Done");
            pipe.close();

        } catch (IOException e){
            System.err.println(e.getMessage());
        }catch (PipeCmdException e ){
            System.err.println("Unrecognized CMD received, retry recommended");
        }
    }
}

