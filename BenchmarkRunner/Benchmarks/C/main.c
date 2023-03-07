#include <stdlib.h>
#include <stdio.h>
#include <math.h>
#include <scomm.h>
#include <cmd.h>
#include "BinaryTrees.h"
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
    int LoopIterations = 1;
    int c;
    do{
        writeCmd(s, Ready);
        if(expectCmd(s, Go)){
            ///Compute benchmark here!
            BinaryTrees(LoopIterations);
        }else
        {
            exit(EXIT_FAILURE);
        }
        writeCmd(s, Done); //
        c = readCmd(s);
    } while(c == Ready);
    // we should have read done at this point
    printf("\ndone\n");
    closeSocket(s);
}