//
// Created by adrian on 3/7/23.
//

#ifndef C_NBODY_H
#define C_NBODY_H

#include <sys/types.h>
#include <stdio.h>
#include <x86intrin.h>
#define N 5
#define PI 3.141592653589793
#define SOLAR_MASS (4 * PI * PI)
#define DAYS_PER_YEAR 365.24
#define PAIRS (N*(N-1)/2)

int NBody(ulong loopIterations);

#endif //C_NBODY_H
