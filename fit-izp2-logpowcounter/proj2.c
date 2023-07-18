/*
    Jmeno: IZP - proj2
    Autor: Radek Veverka
    Login: xvever13
    Datum: listopad 2018 
*/

#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>
#include <string.h>
#include <math.h>
#include <errno.h>

double taylorcf_pow(double x, double y , unsigned int n);
double taylor_pow(double x, double y, unsigned int n);
double cfrac_log(double x, unsigned int n);
double taylor_log(double x, unsigned int n);

// Vrati double ziskany z retezec pouzitim funkce strtod
// Osetruje chybu a nastavuje ji pres pointer v druhem parametru
double parsujDouble(char *retezec, bool *vysledek)
{
    errno = 0; // Resetovat promennou errno pred procesem
    char *ptr; // Pro funkci strtod, ulozi sem zbytek retezce za cislem
    double x = strtod(retezec, &ptr);

    if (strlen(ptr) > 0 || retezec[0] == '\0') // Pokud je za cislem retezec, jde o nezadouci vstup
        errno = 1;

    (*vysledek) = (errno) ? false : true; // Nastavit chybovy stav
    return x;
}

// Vrati unsigned integer ziskany z retezce pouzitim funkce stroul
// Osetruje chybu a nastavuje ji pres pointer v druhem parametru
unsigned int parsujUInt(char *retezec, bool *vysledek)
{
    errno = 0; // Resetovat promennou errno pred procesem
    char *ptr; // Pro funkci strtod, ulozi sem zbytek retezce za cislem
    unsigned x = strtoul(retezec, &ptr, 10);

    if (strlen(ptr) > 0 || retezec[0] == '\0' || retezec[0] == '-' || x == 0) // Pokud je za cislem retezec, nebo je vstupni retezec prazdny jde o nezadouci vstup
        errno = 1; // Pro hodnotu 0 je to v tomto programu taky chyba

    (*vysledek) = (errno) ? false : true; // Nastavit chybovy stav
    return x;
}

// Funkce pro vypocet logaritmu pomoci taylorova polynomu s danym poctem iteraci
double taylor_log(double x, unsigned int n)
{
    double vysledek; 
    // Osetreni specialnich pripadu ---------
    if (isnan(x)) 
        return NAN;
    if (x == -INFINITY)
        return NAN;
    if (isinf(x))
        return INFINITY;
    if (x == 0)
        return -INFINITY;
    if (x < 0)
        return NAN;
    // --------------------------------------

    if (x > 0 && x <= 1) // Pro vzorec log(1 - x) = -x - (x^2 / 2) - (x^3 / 3) ...
    {
        double y = 1.0 - x; //  Prepocitat vstup protoze ve vzorci je log(1 - x) nikoliv log(x) 
        double soucet = -y;
        double zmena = y;
        double iterace = 2;

        while (iterace <= n)
        {
            zmena = zmena * y * ( (iterace - 1)  /  iterace );
            soucet -= zmena;
            iterace++;
        }
        
        vysledek = soucet;
    } else if (x > 1) // V tomto pripade se vyuzije jiny vzorec
    {
        double konstanta = (x - 1) / x;
        double zmena = konstanta;
        double soucet = konstanta;
        double iterace = 2;

        while (iterace <= n)
        {
            zmena = zmena * konstanta * ( (iterace - 1) / iterace );
            soucet += zmena;
            iterace++;
        }
        
        vysledek = soucet;
    }

    return vysledek;
}

// Funkce pro vypocet logaritmu pomoci zretezeneho zlomku
double cfrac_log(double x, unsigned int n)
{
    // Osetreni specialnich pripadu --------
    if (x == -INFINITY)
        return NAN;
    if (isinf(x))
        return INFINITY;
    if (x == 0)
        return -INFINITY;
    if (x < 0)
        return NAN;
    // ------------------------------------
        
    double y = (x - 1) / (x + 1); // Nutno prepocitat vstup dle vzorce
    double ynadruhou = y * y; // Neni nutno toto pocitat v kazde iteraci

    double cf = 0;
    for (unsigned i = n - 1; i > 0; --i)
        cf = (i * i * ynadruhou) / ( (2*(i + 1) - 1) - cf ); 
    
    cf = (2 * y) / (1 - cf); // Zaverecny zlomek
    return cf;
}

// Funkce pro vypocet exponencialni funkce pomoci taylorova polynomu s danym poctem iteraci
double taylor_pow(double x, double y, unsigned int n)
{
    // Osetreni specialnich pripadu --------------------
    if (x == 0 && y < 0)
        return INFINITY;
    if (y == 0 || x == 1) // Cokoliv na nultou je jedna a 1 na cokoliv je 1
        return 1;
    if (isnan(x) && isinf(y))
        return NAN;
    if (y == -INFINITY && x > 1)
        return 0;
    if (y == -INFINITY)
        return INFINITY;
    if (x == 0)
        return 0;
    // -------------------------------------------------

    double prirozenyLog = taylor_log(x, n);
    double soucet = 1;
    double zmena = 1;
    
    for (unsigned i = 1; i < n; ++i)
    {
        zmena = zmena * ( (y * prirozenyLog) / i);
        soucet += zmena;
    }

    return soucet;
}

// Funkce pro vypocet exponencialni funkce pomoci taylorova polynomu
// a s prirozenym logaritmem vypocitanym pomoci zretezenych zlomku
double taylorcf_pow(double x, double y , unsigned int n)
{
    if (x == 0 && y < 0)
        return INFINITY;
    if (y == 0 || x == 1) // Cokoliv na nultou je jedna a 1 na cokoliv je 1
        return 1;
    if (isnan(x) && isinf(y))
        return NAN;
    if (y == -INFINITY && x > 1)
        return 0;
    if (y == -INFINITY)
        return INFINITY;
    if (x == 0)
        return 0;

    double prirozenyLog = cfrac_log(x, n);
    double soucet = 1;
    double zmena = 1;
    
    for (unsigned i = 1; i < n; ++i)
    {
        zmena = zmena * ( (y * prirozenyLog) / i);
        soucet += zmena;
    }

    return soucet;
}

// Funkce ktera spusti vypocet logaritmu a zajisti veci okolo jako parsovani
// parametru a tisknuti vystupu
char delejLog(char *argx, char *argn)
{
    bool vyslX, vyslN; // Promenne pro ulozeni pripadneho chyboveho stavu konverze
    double x = parsujDouble(argx, &vyslX);
    unsigned n = parsujUInt(argn, &vyslN);

    if (!vyslX || !vyslN) 
    {
        fprintf(stderr, "Nespravne argumenty!\n");
        return EXIT_FAILURE;
    }

    // Vypocty potrebnych hodnot
    double logVysl = log(x);
    double taylorVysl = taylor_log(x, n);
    double cfracVysl = cfrac_log(x, n);

    printf ("       log(%g) = %.12g\n"
            " cfrac_log(%g) = %.12g\n"
            "taylor_log(%g) = %.12g\n",
            x, logVysl, x, cfracVysl, x, taylorVysl);

    return EXIT_SUCCESS;
}

// Funkce ktera spusti vypocet logaritmu a zajisti veci okolo jako parsovani
// parametru a tisknuti vystupu
char delejExp(char *argx, char *argy, char *argn)
{
    bool vyslX, vyslY, vyslN; // Promenne pro ulozeni pripadneho chyboveho stavu konverze
    double x = parsujDouble(argx, &vyslX);
    double y = parsujDouble(argy, &vyslY);
    unsigned n = parsujUInt(argn, &vyslN);

    if (!vyslN || !vyslY || !vyslX)
    {
        fprintf(stderr, "Nespravne argumenty!\n");
        return EXIT_FAILURE;
    }

    // Vypocty potrebnych hodnot
    double expVysl = pow(x, y);
    double taylorVysl = taylor_pow(x, y, n);
    double taylorcfVysl = taylorcf_pow(x, y, n);

     printf ("         pow(%g,%g) = %.12g\n"
             "  taylor_pow(%g,%g) = %.12g\n"
             "taylorcf_pow(%g,%g) = %.12g\n",
             x, y, expVysl, x, y, taylorVysl, x, y, taylorcfVysl);

    return EXIT_SUCCESS;

}

void tiskniNapovedu()
{
    printf("Napoveda k programu:\n"
            "--log X N\tSpocita prirozeny logaritmus z X pomoci N iteraci.\n"
            "--pow X Y N\tSpocita exponencialni funkci o zakladu X s exponentem Y pomoci N iteraci.\n"
    );
}

int main(int argc, char *argv[])
{
    if (argc < 4) // Prvotni kontrola, jestli ma cenu pokracovat
    {
        fprintf(stderr, "Nedostatek argumentu!\n");
        tiskniNapovedu();
        return EXIT_FAILURE;
    }

    // Spusteni logaritmoveho programu
    if (strcmp("--log", argv[1]) == 0)  
        return delejLog(argv[2], argv[3]);   
    
    // Spusteni omocnovaciho programu
    if (strcmp("--pow", argv[1]) == 0)
    {
        if (argc < 5)
        {
            fprintf(stderr, "Nedostatek argumentu!\n");
            return EXIT_FAILURE;
        }

        return delejExp(argv[2], argv[3], argv[4]);
    }

    // Neznamy prikaz? Tak vypiseme jak to pouzivat.
    tiskniNapovedu();
    return EXIT_FAILURE;
}