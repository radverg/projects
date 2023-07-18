// eratosthenes.c
// IJC - du 1, a), FIT 
// Autor: Radek Veverka, xvever13
// Datum 23.2.2019
// Preklad: gcc 7.4.0 (merlin), flagy: -O2 -std=c11 -Wall -Wextra -pedantic

#include "eratosthenes.h"

void Eratosthenes(bit_array_t pole)
{
    unsigned long array_size = bit_array_size(pole);
    bit_array_setbit(pole, 0, 1);
    bit_array_setbit(pole, 1, 1);

  //  int iterations = 0;

    unsigned long sqrt_limit = sqrt(array_size);
    for (unsigned long i = 2; i <= sqrt_limit; ++i)
    {
        if (bit_array_getbit(pole, i))
            continue; // Toto uz jasne neni prvocislo, takze preskocime
  
        // Udelat nasobky
        for (unsigned long n = i, index = n * i; index < array_size; index = ++n * i )
        {   
            bit_array_setbit(pole, index, 1);
         //   iterations++;
        }
    }

  //  printf("|%d|", iterations);
}