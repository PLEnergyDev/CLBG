#include <stdlib.h>
#include <stdio.h>
#include <scomm.h>
#include <cmd.h>
#include <stdbool.h>
#include <simpleConversion.h>
///Includes here


int main(int argc, char **argv){
    if(argc<=1){
        fprintf(stderr,"\nUsage: %s [socket]\n\n", argv[0]);
        exit(EXIT_FAILURE);
    }
    char* pipe = argv[1];
    int s = connectTo(pipe);
    if(s<0){
        printf("Bad socket...");
        exit(EXIT_FAILURE);
    }
    writeCmd(s, Ready);
    bool flag = true;
    while(flag){
        writeCmd(s, Ready);

        expectCmd(s, Receive);

        ulong loopIterations = *((ulong*)receiveValue(s,byteToNumberGeneric, sizeof(ulong)));

        writeCmd(s, Ready);
        expectCmd(s, Go);

        ///Compute benchmark here



        writeCmd(s, Done);
        expectCmd(s, Ready);

        ///Send return value here


        CMD c = readCmd(s);
        flag = c == Ready;
    }

    printf("\ndone\n");
    closeSocket(s);
}