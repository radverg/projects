// error.c
// IJC - du 1, b), FIT 
// Autor: Radek Veverka, xvever13
// Datum 23.2.2019
// Preklad: gcc 7.4.0 (merlin), flagy: -O2 -std=c11 -Wall -Wextra -pedantic


#include "error.h"

void error_exit(const char *fmt, ...)
{
    va_list args;
    va_start(args, fmt);

    fprintf(stderr, "CHYBA: ");
    vfprintf(stderr, fmt, args);

    va_end(args);
    exit(1);
}

void warning_msg(const char *fmt, ...)
{
    va_list args;
    va_start(args, fmt);

    fprintf(stderr, "CHYBA: ");
    vfprintf(stderr, fmt, args);

    va_end(args);
}