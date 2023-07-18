/*
    Jmeno: IZP - proj1
    Autor: Radek Veverka
    Login: xvever13
    Datum: rijen a listopad 2018 
*/

#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>
#include <string.h>

#define MAX_DELKA_RADKU 1050
#define MAX_RADKU 120
#define USPECH 0
#define CHYBA -1
#define KONEC 1

// Vytiskne jednoduchou napovedu, volano pokud v programu neni potrebny pocet argumentu
void tiskniNapovedu()
{
    printf(
        "NAPOVEDA: \nPro pouziti programu zadejte jako prvni argument nazev souboru s prikazy."
        "\nSoubor s textem k uprave pouzijte jako standardni vstup.\n"
        "Seznam moznych prikazu: iCONTENT, bCONTENT, aCONTENT, d, dN, r,\n s PATTERN REPLACEMENT,"
        "s:PATTERN:REPLACEMENT, n, nN, q, gX, cX PATTERN, e, fPATTERN\n"
        "Konec programu...\n"
    );
}

// Vytiskne radek pomoci printf, funkce pouze pro prehlednost ve switchi
void tiskniRadek(char *radek) 
{
    printf("%s", radek);
}

// Prikaz iCONTENT - vytiskne string a pripoji k nemu znak konce radku, funkce pro prehlednost ve switchi
void tiskniPredRadek(char *string)
{
    printf("%s\n", string);
}

// Vytiskne zbyvajici stdin - volano pokud jiz nejsou prikazy ale zbyl vstup
void tiskniZbytek()
{
    char c;
    while ((c = getchar()) != EOF)
        putchar(c);
}

// Smaze '\n' z konce stringu, pokud tam byl, vrati 1, jinak 0
char smazEol(char *string)
{
    int delka = strlen(string); 

    if (delka > 0 && string[delka - 1] == '\n')
    {
        string[delka - 1] = '\0'; // Eol se smaze nahrazenim za nulty charakter
        return 1;
    }
    return 0;
}

// Z prikazu vytahne ciselny argument, pokud tam nic neni, vrati -1, pokud je zadan spatne, vrati 0
long ziskejKladnyCiselnyArg(char *prikaz)
{
    char *ptr; // Nutne pro pripadnou oriznutou cast strtolu
    if (strlen(prikaz) < 2)
        return -1; // V prikazu neni argument

    long vysledek = strtol(prikaz + 1, &ptr, 10);

    if (vysledek < 1 || strlen(ptr) > 0) // Vysledek musi byt kladne cislo, jinak je to nevalidni argument
        return 0;

    return vysledek;
}

// Nacte dalsi radek ze stdin, kontroluje preteceni radku a konec stdin.
char nactiRadekVstupu(char *string)
{
    char *vysledek = fgets(string, MAX_DELKA_RADKU, stdin);

    if (vysledek == NULL) // Konec vstupu?
        return KONEC;

    if (vysledek[strlen(vysledek) - 1] != '\n') // Neni moc dlouhy radek?
        return CHYBA;

    return USPECH;
}

// Funkce nacte prikazy ze souboru a vrati jejich pocet, v pripade chyby vrati -1
short nactiPrikazy(char *nazevSouboru, char polePrikazu[][MAX_DELKA_RADKU]) 
{
    FILE *souborPrikazu = fopen(nazevSouboru, "r");
    unsigned nactenoRadku = 0;
    
    if (souborPrikazu == NULL) // Podarilo se precist soubor?
    {
        fprintf(stderr, "Nelze precist soubor %s!\n", nazevSouboru);
        return CHYBA;
    }
    // Cyklus pro nacitani souboru
    while (fgets(polePrikazu[nactenoRadku], MAX_DELKA_RADKU, souborPrikazu) != NULL && nactenoRadku < MAX_RADKU) 
    {
        unsigned pocetZnaku = strlen(polePrikazu[nactenoRadku]);

        if (pocetZnaku >= MAX_DELKA_RADKU - 1) // Neni radek moc dlouhy?
        {
            fprintf(stderr, "Radek %d v souboru prikazu je prilis dlouhy.\n", nactenoRadku + 1);
            return CHYBA;
        }  

        // Je treba smazat \n z prikazu aby to pak nedelalo neplechu 
        if (smazEol(polePrikazu[nactenoRadku]))
            pocetZnaku--;
        if (pocetZnaku != 0) // Pokud v radku nic krome \n nebylo, neinkrementujeme a prepiseme to dalsim prikazem
            nactenoRadku++;
    }

    fclose(souborPrikazu);
    return nactenoRadku;
}
 
// Posune standartni vstup o n radku, je mozno zvolit jestli ma tisknout na stdout (pro prikaz n) nebo ne (pro prikaz d)
char posunRadek(char *aktualniRadek, long kolikrat, bool tiskni)
{
    if (kolikrat == -1) // Argument nezadan - nastavit na 1, to je vychozi hodnota pro posun bez argumentu
        kolikrat = 1;

    if (kolikrat == 0) // Argument zadan spatne
    {
        fprintf(stderr, "Byl zadan nevalidni argument pro posun radku!\n");
        return CHYBA;
    }

    for (int i = 0; i < kolikrat; i++) // Posouvame kolikrat bylo zadano v argumentu
    {
        if (tiskni) 
            tiskniRadek(aktualniRadek); // Vytisknout radek

        int vysledek = nactiRadekVstupu(aktualniRadek); // Nacist dalsi radek
        if (vysledek == CHYBA || vysledek == KONEC)
            return vysledek;
    }

    return USPECH;
}

// Funkce zjistuje cyklus pri poziti prikazu goto proiterovanim prikazu mezi goto a cilovym prikazem 
// Osetruje pouze jednoduche cykly, kombinace skoku mezi dva skoky a jeste podminene skoky nejsou osetreny
bool jeCyklus(int indexAktualni, int indexPozadovany, char polePrikazu[][MAX_DELKA_RADKU])
{
    if (polePrikazu[indexAktualni][0] == 'c' || indexPozadovany > indexAktualni)
        return false; // Pokud je to conditioned goto nebo skaceme dopredu tak cyklus neresime

    for (int i = indexPozadovany; i < indexAktualni; i++)
    {
        char comm = polePrikazu[i][0]; // Ziskat prvni pismeno prikazu - typ prikazu
        // Pokud se objevi nasledujici prikazy, cyklus to neni
        if (comm == 'n' || comm == 'd' || comm == 'i' || comm == 'f' || comm == 'q' || comm == 'c' || comm == 'g')
            return false;
    }

    return true; // Kdyby cyklus nebyl, prislo by se na to ve for cyklu a vratilo by se false
}

// Nastavi index radku na pozadovanou hodnotu pres ukazatel, kontroluje jestli je pozadovany radek v rozsahu
char gotoRadek(int *radek, int cilovyRadek, int celkemRadku, char polePrikazu[][MAX_DELKA_RADKU])
{
    if (cilovyRadek < 1 || cilovyRadek > celkemRadku) // Validace pozice cilovyho radku
    {
        fprintf(stderr, "Prikaz goto se nepovedl! Argument byl zadan spatne nebo mimo rozsah!\n");
        return CHYBA;
    }

    if (jeCyklus(*radek, cilovyRadek - 1, polePrikazu)) // Detekce zacykleni
    {
        fprintf(stderr, "Prikaz goto na radku %d zpusobuje zacykleni prikazu!\n", (*radek) + 1);
        return CHYBA;
    }

    *radek = (cilovyRadek - 1); // Je treba odecist jednicku nebot v goto prikazu cislujeme od 1
    return USPECH;
}

// Prikaz podminene goto
char podmineneGoto(int *indexRadku, int celkemRadku, char *aktualniRadek, char *textPrikazu, char polePrikazu[][MAX_DELKA_RADKU])
{
    char *mezera = strchr(textPrikazu, ' ');
    int cislo = ziskejKladnyCiselnyArg(textPrikazu);

    if (mezera == NULL || strlen(mezera + 1) == 0) 
    {
        fprintf(stderr, "Spatne zadany prikaz podminene goto!\n");
        return CHYBA;
    }

    if (strstr(aktualniRadek, mezera + 1) != NULL) // Nachazi se v aktualnim radku pattern?
        return gotoRadek(indexRadku, cislo, celkemRadku, polePrikazu);

    (*indexRadku)++;
    return USPECH; 
}

// Pripojuje string dle booleovskeho prepinace za nebo pred jiny string, kontroluje preteceni
char pripojString(char *co, char *kam, bool pred)
{
    unsigned delkaPuvodniho = strlen(kam);
    unsigned delkaPripojky = strlen(co);

    if (delkaPuvodniho + delkaPripojky > MAX_DELKA_RADKU - 2)
    {
        fprintf(stderr, "Chyba ve spojovani stringu! Vysledny string je moc dlouhy.\n");
        return CHYBA;
    }

    if (delkaPripojky < 1)
        return USPECH; // Neni co pripojovat

    if (pred == false)
    { // Budeme pripojovat za - zkontrolovat jestli tam je eol, pokud jo, tak ho posunout
        if (kam[delkaPuvodniho - 1] == '\n')
        {
            kam[delkaPuvodniho - 1] = '\0';
            strcat(kam, co);
            strcat(kam, "\n");
        } else 
            strcat(kam, co);
    } 
    else 
    { // Budeme pripojovat pred - nutno zkopirovat string
        char kopie[MAX_DELKA_RADKU];
        strcpy(kopie, kam);
        strcpy(kam, co);
        strcat(kam, kopie);
    }

    return USPECH;
}

// Nahradi prvni vyskyt stringu 'co' stringem 'cim' ve stringu 'vcem'. Kontroluje preteceni radku.
char nahradit(char *co, char *cim, char *vcem, bool vse)
{
    do {
        int delkaHledaneho = strlen(co);
        int delkaNahrazujiciho = strlen(cim);
        char *p_nalezeno = strstr(vcem, co);
        char *p_zbytek = p_nalezeno + delkaHledaneho;

        if (p_nalezeno == NULL || strcmp(co, cim) == 0)
            return KONEC; // Neni co nahrazovat nebo vzor a pattern jsou stejne
        if (strlen(vcem) + (delkaNahrazujiciho - delkaHledaneho) > MAX_DELKA_RADKU - 2)
        {
            fprintf(stderr, "Pretekl radek pri nahrazovani!\n");
            return CHYBA;
        }

        // Zkopirovat zbytek (bude prepsan v puvodnim)
        char zbytek[MAX_DELKA_RADKU];
        strcpy(zbytek, p_zbytek);

        // Provest nahrazeni
        strcpy(p_nalezeno, cim);
        strcpy(p_nalezeno + delkaNahrazujiciho, zbytek);

        // Posunout ukazatel abychom nenahrazovali jiz nahrazene
        vcem = p_nalezeno + delkaNahrazujiciho;
    } while (vse); // Z cyklu se vyskoci kdyz nebude co nahrazovat nebo parametr vse bude false
    
    return KONEC;
}

// Prikaz nahrazeni - analyzuje format zadaneho prikazu a bud zavola funkci nahradit nebo vrati chybu
char prikazNahradit(char *textPrikazu, char *radek)
{
    bool vse = (textPrikazu[0] == 'S') ? true : false;
    char *p_oddelovac; 
    // Zkontrolovat zda je prikaz spravny - existuje pattern? existuje druhy oddelovac?
    if (strlen(textPrikazu) < 4 || textPrikazu[1] == textPrikazu[2] || 
        (p_oddelovac = strchr(textPrikazu + 2, textPrikazu[1])) == NULL)
    {
        fprintf(stderr, "Prikaz nahrazeni je nespravny!\n");
        return CHYBA;
    }

    char oddelovac = *p_oddelovac; // Ulozit si oddelovac pro budouci vraceni
    *p_oddelovac = '\0'; // Nahradit oddelovac nultym charem - tim se stringy oddeli a pujdou snadno pouzit

    char *nahraditCo = textPrikazu + 2;
    char *nahraditCim = p_oddelovac + 1; 
    char vysledek = nahradit(nahraditCo, nahraditCim, radek, vse);
    *p_oddelovac = oddelovac; // Vratit zpatky do prikazu oddelovac pro pripad ze by se s nim znovu pracovalo
    return (vysledek == KONEC) ? USPECH : vysledek; 
}

// Prikaz f, posouva radky dokud nenajde vzor v nejakem radku
char najdiVzor(char *aktualniRadek, char *vzor)
{
    if (strlen(vzor) < 1) // Vzor je povinny argument - byl zadan?
    {
        fprintf(stderr, "Vzor pro hledani nebyl v prikazu zadan!\n");
        return CHYBA;
    }

    char vysledek;
    while ((vysledek = posunRadek(aktualniRadek, 1, true)) != KONEC && strstr(aktualniRadek, vzor) == NULL)
        ; // Cyklus se kona dokud nenarazime na konec vstupu nebo na vzor

    return vysledek;
}

// Volano z mainu - vybere a zavola prikaz dle prvniho pismena
char provedPrikaz(char polePrikazu[][MAX_DELKA_RADKU], char *aktualniRadek, int *p_indexPrikazu, int pocetPrikazu)
{
    if (*p_indexPrikazu >= pocetPrikazu) // Kontrola jestli nejsme na konci prikazu
    {
        tiskniRadek(aktualniRadek); // Vytisknout co zbylo v bufferu
        tiskniZbytek(); // Vytisknout zbytek stdin
        return KONEC;
    } 

    char *prikaz = polePrikazu[*p_indexPrikazu];
    char vysledekPrikazu = USPECH;
    // Vyber prikazu na zaklade prvniho pismena
    switch (prikaz[0])
    {
        case 'n': vysledekPrikazu = posunRadek(aktualniRadek, ziskejKladnyCiselnyArg(prikaz), true); break;
        case 'd': vysledekPrikazu = posunRadek(aktualniRadek, ziskejKladnyCiselnyArg(prikaz), false); break;
        case 'r':                   smazEol(aktualniRadek); break;
        case 'g': vysledekPrikazu = gotoRadek(p_indexPrikazu, ziskejKladnyCiselnyArg(prikaz), pocetPrikazu, polePrikazu); (*p_indexPrikazu)--; break;
        case 'a': vysledekPrikazu = pripojString(prikaz + 1, aktualniRadek, false); break;
        case 's': 
        case 'S': vysledekPrikazu = prikazNahradit(prikaz, aktualniRadek); break;
        case 'b': vysledekPrikazu = pripojString(prikaz + 1, aktualniRadek, true); break;
        case 'i':                   tiskniPredRadek(prikaz + 1); break;
        case 'f': vysledekPrikazu = najdiVzor(aktualniRadek, prikaz + 1); break;
        case 'c': vysledekPrikazu = podmineneGoto(p_indexPrikazu, pocetPrikazu, aktualniRadek, prikaz, polePrikazu); (*p_indexPrikazu)--; break;
        case 'e': vysledekPrikazu = pripojString("\n", aktualniRadek, false); break;
        case 'q': return KONEC;
        default: fprintf(stderr, "Neznamy prikaz na radku %d!\n", (*p_indexPrikazu) + 1); return CHYBA;
    }

    (*p_indexPrikazu)++;
    return vysledekPrikazu;
}

int main(int argc, char *argv[]) 
{
    // Zobrazit napovedu a skoncit pokud nejsou prave dva argumenty
    if (argc != 2) 
    {
        tiskniNapovedu();
        return EXIT_SUCCESS;
    }

    // Vytvor pole prikazu a nacti do nej soubor s prikazy
    char polePrikazu[MAX_RADKU][MAX_DELKA_RADKU];
    int indexPrikazu = 0;
    int pocetPrikazu = nactiPrikazy(argv[1], polePrikazu);

    if (pocetPrikazu == CHYBA)
        return EXIT_FAILURE;

    // Nacti prvni radek vstupu pred spustenim hlavniho cyklu
    char aktualniRadek[MAX_DELKA_RADKU];
    if (nactiRadekVstupu(aktualniRadek) != USPECH)
        return EXIT_SUCCESS;

    int vysledekPrikazu;
    // Hlavni smycka programu, vykonava prikazy
    while ((vysledekPrikazu = provedPrikaz(polePrikazu, aktualniRadek, &indexPrikazu, pocetPrikazu)) == USPECH) 
        ;
    
    // Provadeni prikazu skonceno, podivame se zda uspesne nebo s errorem
    return (vysledekPrikazu == CHYBA) ? EXIT_FAILURE : EXIT_SUCCESS;
}