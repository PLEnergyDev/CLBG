//
// Created by adrian on 3/7/23.
//

#ifndef C_FANNKUCHREDUX_H
#define C_FANNKUCHREDUX_H

#include <stdio.h>
#include <stdint.h>
#include <string.h>
#include <pthread.h>
#include <smmintrin.h>  /* SSE 4.1 */

#define MAX_N 16
#define MAX_BLOCKS 24
#define ALIGN(n) __attribute__((aligned(n)))
#define LIKELY(x) __builtin_expect(!!(x), 1)
#define UNLIKELY(x) __builtin_expect(!!(x), 0)
#define RAMP16 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15

int FannkuchRedux(ulong loopIterations);

#endif //C_FANNKUCHREDUX_H
