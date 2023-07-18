// steg-decode.c
// IJC - du 1, b), FIT 
// Autor: Radek Veverka, xvever13
// Datum 24.2.2019
// Preklad: gcc 7.4.0 (merlin), flagy: -O2 -std=c11 -Wall -Wextra -pedantic

#include <stdio.h>
#include <limits.h>
#include "ppm.h"
#include "eratosthenes.h"

#define BIT_ARRAY_OVERFLOW 0

// Pomocna funkce na najiti dalsiho prvocisla v bitovem poli, vraci BIT_ARRAY_OVERFLOW, pokud jsme presli pres posledni prvocislo v poli
int next_prime(unsigned long current, bit_array_t b_array)
{
    do 
    {
        current++;
        if (current > bit_array_size(b_array))
            return BIT_ARRAY_OVERFLOW;

    } while (bit_array_getbit(b_array, current) != 0);

    return current;
}

int main(int argc, char *argv[])
{
    if (argc != 2)
        error_exit("Program steg-decode vyzaduje prave jeden argument, coz je cesta k ppm souboru!\n");
      
    struct ppm *ppmptr = ppm_read(argv[1]);
    if (ppmptr == NULL)
        error_exit("Cteni souboru neprobehlo v poradku.");

    bit_array_alloc(b_array, ppmptr->xsize * ppmptr->ysize * 3);
    Eratosthenes(b_array);

    // Zacit dekodovani
    int current_char = 0;
    int current_prime = 19;
    char message_byte = 0;

    do
    {
        message_byte = 0;
        for(int i = 0; i < CHAR_BIT; i++)
        {
            if (ppmptr->data[current_prime] & 1) // Je na konci bytu jednicka?
            { 
                // Tak ji pridame do message byte na patricnou pozici
                message_byte |= (1 << i);
            }
            // Nahrajame dalsi prvocislo a zkontrolujeme zda se to povedlo (nejsme mimo bit pole)
            if ((current_prime = next_prime(current_prime, b_array)) == BIT_ARRAY_OVERFLOW)
            {
                bit_array_free(b_array);
                ppm_free(ppmptr);
                error_exit("Zprava nebyla korektne ukoncena a tudiz preteklo bitove pole.");
            }
        }

        // Byl precten znak vnitrnim cyklem, tisk na stdout
        putchar(message_byte);
        current_char++;
    } while (message_byte != 0);

    bit_array_free(b_array);
    ppm_free(ppmptr);
    return 0;
}