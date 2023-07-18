// error.h
// IJC - du 1, b), FIT 
// Autor: Radek Veverka, xvever13
// Datum 23.2.2019

#include <stdarg.h>
#include <stdio.h>
#include <stdlib.h>

#ifndef __ERROR__
#define __ERROR__

    void warning_msg(const char *fmt, ...);
    void error_exit(const char *fmt, ...);

#endif