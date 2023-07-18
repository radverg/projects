// ppm.h
// IJC - du 1, b), FIT 
// Autor: Radek Veverka, xvever13
// Datum 24.2.2019
// Preklad: gcc 7.4.0 (merlin), flagy: -O2 -std=c11 -Wall -Wextra -pedantic

#include <stdio.h>
#include <stdlib.h>
#include "ppm.h"
#include "error.h"

struct ppm *ppm_read(const char *filename)
{
    FILE *file = fopen(filename, "r");

    if (file == NULL)
        goto err_fclose;

    int xsize = 0;
    int ysize = 0;

    int scanned = fscanf(file, "P6 %d %d 255 ", &xsize, &ysize);
    if (scanned != 2 || xsize <= 0 || ysize <= 0)
    {
        warning_msg("ppm_read: Chybny format hlavicky ppm souboru.");
        goto err_fclose;
    }

    struct ppm *ppmptr = malloc(sizeof(struct ppm) + 3 * xsize * ysize);
    if (ppmptr == NULL)
    {
        warning_msg("ppm_read: Chyba alokace pameti.");
        goto err_fclose;
    }

    ppmptr->xsize = xsize;
    ppmptr->ysize = ysize;

    // Precist binarni data
    size_t readResult = fread(&(ppmptr->data), 3, ppmptr->xsize * ppmptr->ysize, file);
    if (readResult != ppmptr->xsize * ppmptr->ysize)
    {
        warning_msg("ppm_read: Spatna binarni data v ppm souboru.");
        goto err_free;
    }

    fclose(file);
    return ppmptr;

    err_free:
        ppm_free(ppmptr);
    err_fclose:
        fclose(file);

    return NULL;
}

void ppm_free(struct ppm *p)
{
    free(p);
}