// primes.c
// IJC - du 1, a), FIT 
// Autor: Radek Veverka, xvever13
// Datum 23.2.2019
// Preklad: gcc 7.4.0 (merlin), flagy: -O2 -std=c11 -Wall -Wextra -pedantic

#include <stdio.h>
#include "eratosthenes.h"

#define PRIMES_TO_WRITE 10
#define ARRAY_LENGTH 123000000

int main()
{
    bit_array_create(b_array, ARRAY_LENGTH);
    Eratosthenes(b_array);

    unsigned long primes[PRIMES_TO_WRITE] = { 0 };
    for (int i = bit_array_size(b_array) - 1, prime_count = 0; prime_count < 10 && i >= 0; i--)
    {
        if (bit_array_getbit(b_array, i) == 0)
        {
            prime_count++;  
            primes[PRIMES_TO_WRITE - prime_count] = i;
        }
    }

    // Vypis
    for (int i = 0; i < PRIMES_TO_WRITE; i++)
        printf("%lu\n", primes[i]);
}
