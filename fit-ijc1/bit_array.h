// bit_array.h
// IJC - du 1, a), FIT 
// Autor: Radek Veverka, xvever13
// Datum 23.2.2019

#include <limits.h>
#include <stdlib.h>
#include "error.h"
#include <assert.h>

#ifndef __BIT_ARRAY__
#define __BIT_ARRAY__

// Ukazatel na bitove pole
typedef unsigned long* bit_array_t;

// Pomocna makra ------------------------------------------------------------------------------------------------------
    // Z poctu bitu vrati unsigned long offset v bitovem poli
    #define bit_size_to_ulong(bit_count) (unsigned long)((bit_count) / (CHAR_BIT * sizeof(unsigned long)) + 1)

    // Pocet bitu v unsigned longu
    #define ULONG_BSIZE (sizeof(unsigned long) * CHAR_BIT)

    // Vypocita sirku posunuti na zaklade indexu v poli
    #define SHIFT_WIDTH(index) (ULONG_BSIZE - (index) % ULONG_BSIZE - 1)
// --------------------------------------------------------------------------------------------------------------------

// Vytvori bitove pole s danym jmenem a bitovou velikosti na zasobniku 
    #define bit_array_create(jmeno_pole, velikost)                                                      \
            static_assert((velikost) > 0, "Velikost bitoveho pole musi byt vetsi nez 0!");              \
            unsigned long (jmeno_pole)[bit_size_to_ulong(velikost) + 1] = { (velikost), 0, }

// Vytvori bitove pole s danym jmene a bitovou velikosti na halde
// Je potreba volat bit_array_free po ukonceni prace s polem
    #define bit_array_alloc(jmeno_pole, velikost)                                                       \
        assert((velikost) > 0);                                                                         \
        unsigned long *(jmeno_pole) = calloc(bit_size_to_ulong(velikost) + 1, sizeof(unsigned long));   \
        if (jmeno_pole == NULL) {                                                                       \
            fprintf(stderr, "bit_array_alloc: Chyba alokace paměti\n");                                 \
            exit(1);                                                                                    \
        }                                                                                               \
        jmeno_pole[0] = (unsigned long)(velikost);


#ifndef USE_INLINE
// Varianty s makry -----------------------------------------------------
    #define bit_array_size(jmeno_pole) (unsigned long)jmeno_pole[0]

    #define bit_array_free(jmeno_pole) free(jmeno_pole)

    #define bit_array_setbit(jmeno_pole, index, vyraz) do {                                                         \
        if ((unsigned long)(index) >= jmeno_pole[0])                                                                \
            error_exit("bit_array_setbit: Index %lu mimo rozsah 0..%lu", (unsigned long)(index), jmeno_pole[0]);    \
                                                                                                                    \
        if ((vyraz))                                                                                                \
            jmeno_pole[bit_size_to_ulong(index)] |= (1L << SHIFT_WIDTH(index));                                     \
        else                                                                                                        \
            jmeno_pole[bit_size_to_ulong(index)] &= ~(1L << SHIFT_WIDTH(index));                                    \
    } while (0)

    #define bit_array_getbit(jmeno_pole, index)                                                                     \
        ((unsigned long)(index) > jmeno_pole[0]) ?                                                                  \
            (error_exit("bit_array_getbit: Index %lu mimo rozsah 0..%lu", (unsigned long)(index), jmeno_pole[0]), 1) : \
            ((jmeno_pole)[bit_size_to_ulong(index)] >> SHIFT_WIDTH(index) & 1)
// -----------------------------------------------------------
#else
// Varianty s inline funkcemi ---------------------------------------------
    inline unsigned long bit_array_size(bit_array_t jmeno_pole)
    {
        return jmeno_pole[0];
    }

    inline void bit_array_free(bit_array_t jmeno_pole)
    {
        free(jmeno_pole);
    }

    inline unsigned long bit_array_getbit(bit_array_t jmeno_pole, unsigned long index) 
    {
        if (index >= jmeno_pole[0])
            error_exit("bit_array_getbit: Index %lu mimo rozsah 0..%lu", index, jmeno_pole[0]);

        return (jmeno_pole[bit_size_to_ulong(index)] >> SHIFT_WIDTH(index)) & 1;
    }

    inline void bit_array_setbit(bit_array_t jmeno_pole, unsigned long index, int vyraz) 
    {
        if (index >= jmeno_pole[0])
            error_exit("bit_array_getbit: Index %lu mimo rozsah 0..%lu", index, jmeno_pole[0]);

        if (vyraz) 
            jmeno_pole[bit_size_to_ulong(index)] |= (1L << SHIFT_WIDTH(index));
        else                   
            jmeno_pole[bit_size_to_ulong(index)] &= ~(1L << SHIFT_WIDTH(index));
    }
// ------------------------------------------------------------------------
#endif
#endif