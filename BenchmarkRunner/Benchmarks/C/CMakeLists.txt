cmake_minimum_required(VERSION 3.24)
project(C C)
set(CMAKE_C_STANDARD 11)
add_compile_options(-march=ivybridge)

include_directories(include, include/include/apr-1)

add_executable(C main.c BinaryTrees.c BinaryTrees.h NBody.c NBody.h FannkuchRedux.c FannkuchRedux.h)
add_executable(fr FannkuchRedux.c FannkuchRedux.h)

target_link_libraries(C ${CMAKE_SOURCE_DIR}/include/lib/libapr-1.a)

target_link_libraries(C ${CMAKE_SOURCE_DIR}/include/libSocketComm.a)

target_include_directories(C PRIVATE include)