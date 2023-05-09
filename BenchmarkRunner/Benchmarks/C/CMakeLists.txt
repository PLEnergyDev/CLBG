cmake_minimum_required(VERSION 3.24)
project(C C)
set(CMAKE_C_STANDARD 11)
add_compile_options(-march=ivybridge)

include_directories(include/SocketComm, include/apr-1)

add_executable(C main.c BinaryTrees.c BinaryTrees.h NBody.c NBody.h FannkuchRedux.c FannkuchRedux.h Fasta.c Fasta.h)
add_executable(fasta Fasta.c Fasta.h)

target_link_libraries(C ${CMAKE_SOURCE_DIR}/lib/libapr-1.a)

target_link_libraries(C ${CMAKE_SOURCE_DIR}/lib/libSocketComm.a)

target_include_directories(C PRIVATE include/SocketComm)