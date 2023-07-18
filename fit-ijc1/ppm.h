// ppm.h
// IJC - du 1, b), FIT 
// Autor: Radek Veverka, xvever13
// Datum 21.2019

#ifndef __ppm__
#define __ppm__

struct ppm 
{
    unsigned xsize;
    unsigned ysize;
    char data[];
};

struct ppm *ppm_read(const char *filename);
void ppm_free(struct ppm *p);

#endif