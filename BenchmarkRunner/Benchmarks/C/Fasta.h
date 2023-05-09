//
// Created by adrian on 3/15/23.
//

#ifndef C_FASTA_H
#define C_FASTA_H
#include <stdint.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <unistd.h>


#define IM 139968
#define IA   3877
#define IC  29573
#define SEED   42
static uint32_t seed = SEED;
#define uint32_rand() ( seed = (seed * IA + IC ) % IM )

/* tune up or down to adjust speed vs memory/cache tradeoffs */
#ifndef BUFLINES
#define BUFLINES 100
#endif



static const char *alu =
        "GGCCGGGCGCGGTGGCTCACGCCTGTAATCCCAGCACTTTGG"
        "GAGGCCGAGGCGGGCGGATCACCTGAGGTCAGGAGTTCGAGA"
        "CCAGCCTGGCCAACATGGTGAAACCCCGTCTCTACTAAAAAT"
        "ACAAAAATTAGCCGGGCGTGGTGGCGCGCGCCTGTAATCCCA"
        "GCTACTCGGGAGGCTGAGGCAGGAGAATCGCTTGAACCCGGG"
        "AGGCGGAGGTTGCAGTGAGCCGAGATCGCGCCACTGCACTCC"
        "AGCCTGGGCGACAGAGCGAGACTCCGTCTCAAAAA";

static const char *iub = "acgtBDHKMNRSVWY";
static const float iub_p[] = {
        0.27,
        0.12,
        0.12,
        0.27,
        0.02,
        0.02,
        0.02,
        0.02,
        0.02,
        0.02,
        0.02,
        0.02,
        0.02,
        0.02,
        0.02 };

static const char *homosapiens = "acgt";
static const float homosapiens_p[] = {
        0.3029549426680,
        0.1979883004921,
        0.1975473066391,
        0.3015094502008
};

int Fasta(ulong loopIterations);

#define LINELEN 60

#endif //C_FASTA_H
