# Makefile
# Úkol: IJC - projekt 1, FIT
# Autor: Radek Veverka, xvever13
# Datum: 21.2.2019
CC=gcc
CFLAGS=-O2 -std=c11 -Wall -Wextra -pedantic
LC_ALL=cs_CZ.utf8
SHELL=/bin/bash

all: primes primes-i steg-decode

primes: eratosthenes.o primes.o
	$(CC) $(CFLAGS) eratosthenes.o primes.o error.o -lm -o primes

primes.o: primes.c bit_array.h
	$(CC) $(CFLAGS) -c primes.c -o primes.o

eratosthenes.o: eratosthenes.c eratosthenes.h bit_array.h error.o
	$(CC) $(CFLAGS) -c eratosthenes.c -o eratosthenes.o


primes-i: eratosthenes-i.o primes-i.o
	$(CC) $(CFLAGS) eratosthenes-i.o primes-i.o error.o -lm -o primes-i

primes-i.o: primes.c bit_array.h
	$(CC) $(CFLAGS) -DUSE_INLINE -c primes.c -o primes-i.o

eratosthenes-i.o: eratosthenes.c eratosthenes.h bit_array.h
	$(CC) $(CFLAGS) -DUSE_INLINE -c eratosthenes.c -o eratosthenes-i.o

error.o: error.c error.h
	$(CC) $(CFLAGS) -c error.c -o error.o

# PPM ---------------------------------------------------------------
ppm.o: ppm.c ppm.h
	$(CC) $(CFLAGS) -c ppm.c -o ppm.o
# -------------------------------------------------------------------

# Steg decode --------------------------------------------------------
steg-decode.o: steg-decode.c ppm.o eratosthenes.o error.o
	$(CC) $(CFLAGS) -c steg-decode.c -o steg-decode.o

steg-decode: steg-decode.o
	$(CC) $(CFLAGS) steg-decode.o ppm.o eratosthenes.o error.o -lm -o steg-decode
# --------------------------------------------------------------------

clear: 
	rm -f *.o primes primes-i steg-decode

run: all
	time ./primes
	time ./primes-i

runsteg: steg-decode
	./steg-decode du1-obrazek.ppm

pack: 
	zip xvever13.zip *.c *.h Makefile