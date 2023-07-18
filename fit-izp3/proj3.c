/**
 * Kostra programu pro 3. projekt IZP 2017/18
 * 
 * Jméno: Radek Veverka
 * Login: xvever13
 * Datum: listopad 2018
 *
 * Jednoducha shlukova analyza
 * Unweighted pair-group average
 * https://is.muni.cz/th/172767/fi_b/5739129/web/web/usrov.html
 */
#include <stdio.h>
#include <stdlib.h>
#include <assert.h>
#include <math.h> // sqrtf
#include <limits.h> // INT_MAX

#define CHYBA -1

/*****************************************************************
 * Ladici makra. Vypnout jejich efekt lze definici makra
 * NDEBUG, napr.:
 *   a) pri prekladu argumentem prekladaci -DNDEBUG
 *   b) v souboru (na radek pred #include <assert.h>
 *      #define NDEBUG
 */
#ifdef NDEBUG
#define debug(s)
#define dfmt(s, ...)
#define dint(i)
#define dfloat(f)
#else

// vypise ladici retezec
#define debug(s) printf("- %s\n", s)

// vypise formatovany ladici vystup - pouziti podobne jako printf
#define dfmt(s, ...) printf(" - "__FILE__":%u: "s"\n",__LINE__,__VA_ARGS__)

// vypise ladici informaci o promenne - pouziti dint(identifikator_promenne)
#define dint(i) printf(" - " __FILE__ ":%u: " #i " = %d\n", __LINE__, i)

// vypise ladici informaci o promenne typu float - pouziti
// dfloat(identifikator_promenne)
#define dfloat(f) printf(" - " __FILE__ ":%u: " #f " = %g\n", __LINE__, f)

#endif

/*****************************************************************
 * Deklarace potrebnych datovych typu:
 *
 * TYTO DEKLARACE NEMENTE
 *
 *   struct obj_t - struktura objektu: identifikator a souradnice
 *   struct cluster_t - shluk objektu:
 *      pocet objektu ve shluku,
 *      kapacita shluku (pocet objektu, pro ktere je rezervovano
 *          misto v poli),
 *      ukazatel na pole shluku.
 */

struct obj_t {
    int id;
    float x;
    float y;
};

struct cluster_t {
    int size;
    int capacity;
    struct obj_t *obj;
};

/*****************************************************************
 * Deklarace potrebnych funkci.
 *
 * PROTOTYPY FUNKCI NEMENTE
 *
 * IMPLEMENTUJTE POUZE FUNKCE NA MISTECH OZNACENYCH 'TODO'
 *
 */

/*
 Inicializace shluku 'c'. Alokuje pamet pro cap objektu (kapacitu).
 Ukazatel NULL u pole objektu znamena kapacitu 0.
*/
void init_cluster(struct cluster_t *c, int cap)
{
    assert(c != NULL);
    assert(cap >= 0);

    c->size = 0;
    c->capacity = cap;

    if (cap == 0)
        c->obj = NULL; // Nebudeme mallocovat kdyz je kapacita 0
    else
    {
        c->obj = malloc(cap * sizeof(struct obj_t));
        if (c->obj == NULL)
        {
            fprintf(stderr, "Nepodarilo se alokovat pamet!\n");
            exit(EXIT_FAILURE);
        }
    }
}

/*
 Odstraneni vsech objektu shluku a inicializace na prazdny shluk.
 */
void clear_cluster(struct cluster_t *c)
{
    free(c->obj);
    init_cluster(c, 0);
}

/// Chunk of cluster objects. Value recommended for reallocation.
const int CLUSTER_CHUNK = 10;

/*
 Zmena kapacity shluku 'c' na kapacitu 'new_cap'.
 */
struct cluster_t *resize_cluster(struct cluster_t *c, int new_cap)
{
    // TUTO FUNKCI NEMENTE
    assert(c);
    assert(c->capacity >= 0);
    assert(new_cap >= 0);

    if (c->capacity >= new_cap)
        return c;

    size_t size = sizeof(struct obj_t) * new_cap;

    void *arr = realloc(c->obj, size);
    if (arr == NULL)
        return NULL;

    c->obj = (struct obj_t*)arr;
    c->capacity = new_cap;
    return c;
}

/*
 Prida objekt 'obj' na konec shluku 'c'. Rozsiri shluk, pokud se do nej objekt
 nevejde.
 */
void append_cluster(struct cluster_t *c, struct obj_t obj)
{
    if (c->size == c->capacity) // Musime zvetsit kapacitu?
    {
        struct cluster_t *result = resize_cluster(c, c->capacity + CLUSTER_CHUNK);
        if (result == NULL)
        {
            // Resize se nepovedl
            fprintf(stderr, "Zmena velikosti pole objektu se nepovedla!");
            exit(EXIT_FAILURE);
        }
    }

    c->obj[c->size] = obj;
    c->size++;
}

/*
 Seradi objekty ve shluku 'c' vzestupne podle jejich identifikacniho cisla.
 */
void sort_cluster(struct cluster_t *c);

/*
 Do shluku 'c1' prida objekty 'c2'. Shluk 'c1' bude v pripade nutnosti rozsiren.
 Objekty ve shluku 'c1' budou serazeny vzestupne podle identifikacniho cisla.
 Shluk 'c2' bude nezmenen.
 */
void merge_clusters(struct cluster_t *c1, struct cluster_t *c2)
{
    assert(c1 != NULL);
    assert(c2 != NULL);

    int c2size = c2->size;
    for (int i = 0; i < c2size; i++)
        append_cluster(c1, c2->obj[i]); // Append resi i pripadne rozsirovani
    
    sort_cluster(c1); // Ma to byt serazene
}

/**********************************************************************/
/* Prace s polem shluku */

/*
 Odstrani shluk z pole shluku 'carr'. Pole shluku obsahuje 'narr' polozek
 (shluku). Shluk pro odstraneni se nachazi na indexu 'idx'. Funkce vraci novy
 pocet shluku v poli.
*/
int remove_cluster(struct cluster_t *carr, int narr, int idx)
{
    assert(idx < narr);
    assert(narr > 0);

    clear_cluster(carr + idx);
    
    for (int i = idx; i < narr - 1; i++) // Cyklus posune clustery za idx o jedno vpred
        carr[i] = carr[i + 1];
    
    return narr - 1;
}

/*
 Pocita Euklidovskou vzdalenost mezi dvema objekty.
 */
float obj_distance(struct obj_t *o1, struct obj_t *o2)
{
    assert(o1 != NULL);
    assert(o2 != NULL);

    float dx = fabs(o1->x - o2->x);
    float dy = fabs(o1->y - o2->y);
    return sqrtf(dx * dx + dy * dy); // Pythagorova veta
}

/*
 Pocita vzdalenost dvou shluku.
*/
float cluster_distance(struct cluster_t *c1, struct cluster_t *c2)
{
    assert(c1 != NULL);
    assert(c1->size > 0);
    assert(c2 != NULL);
    assert(c2->size > 0);

    float vzdalenost = INT_MAX;
    int size1 = c1->size, size2 = c2->size;

    for (int s1 = 0; s1 < size1; s1++)
    {
        for (int s2 = 0; s2 < size2; s2++)
        {
            float novaVzdalenost = obj_distance(c1->obj + s1, c2->obj + s2);
            if (novaVzdalenost < vzdalenost)
                vzdalenost = novaVzdalenost;
        }       
    }

    return vzdalenost;
}

/*
 Funkce najde dva nejblizsi shluky. V poli shluku 'carr' o velikosti 'narr'
 hleda dva nejblizsi shluky. Nalezene shluky identifikuje jejich indexy v poli
 'carr'. Funkce nalezene shluky (indexy do pole 'carr') uklada do pameti na
 adresu 'c1' resp. 'c2'.
*/
void find_neighbours(struct cluster_t *carr, int narr, int *c1, int *c2)
{
    assert(narr > 0);

    if (narr == 1)
    {
        *c1 = 0;
        *c2 = 0;
        return;
    }

    int c1nej = 0, c2nej = 1;
    float vzdalenost = cluster_distance(carr, carr + 1);
    
    for (int sc1 = 0; sc1 < narr; sc1++)
    {
        for (int sc2 = sc1; sc2 < narr; sc2++)
        {
            if (sc1 == sc2)
                continue;
            
            float novaVzdalenost = cluster_distance(carr + sc1, carr + sc2);
            if (novaVzdalenost < vzdalenost)
            {
                vzdalenost = novaVzdalenost;
                c1nej = sc1;
                c2nej = sc2;
            }
        }
    }

    *c1 = c1nej;
    *c2 = c2nej;
}

// pomocna funkce pro razeni shluku
static int obj_sort_compar(const void *a, const void *b)
{
    // TUTO FUNKCI NEMENTE
    const struct obj_t *o1 = (const struct obj_t *)a;
    const struct obj_t *o2 = (const struct obj_t *)b;
    if (o1->id < o2->id) return -1;
    if (o1->id > o2->id) return 1;
    return 0;
}

/*
 Razeni objektu ve shluku vzestupne podle jejich identifikatoru.
*/
void sort_cluster(struct cluster_t *c)
{
    // TUTO FUNKCI NEMENTE
    qsort(c->obj, c->size, sizeof(struct obj_t), &obj_sort_compar);
}

/*
 Tisk shluku 'c' na stdout.
*/
void print_cluster(struct cluster_t *c)
{
    // TUTO FUNKCI NEMENTE
    for (int i = 0; i < c->size; i++)
    {
        if (i) putchar(' ');
        printf("%d[%g,%g]", c->obj[i].id, c->obj[i].x, c->obj[i].y);
    }
    putchar('\n');
}

/*
 Ze souboru 'filename' nacte objekty. Pro kazdy objekt vytvori shluk a ulozi
 jej do pole shluku. Alokuje prostor pro pole vsech shluku a ukazatel na prvni
 polozku pole (ukalazatel na prvni shluk v alokovanem poli) ulozi do pameti,
 kam se odkazuje parametr 'arr'. Funkce vraci pocet nactenych objektu (shluku).
 V pripade nejake chyby uklada do pameti, kam se odkazuje 'arr', hodnotu NULL.
*/
int load_clusters(char *filename, struct cluster_t **arr)
{
    assert(arr != NULL);

    FILE *soubor = fopen(filename, "r");

    if (soubor == NULL)
    {
        fprintf(stderr, "Nepodarilo se nacist soubor!\n");
        return CHYBA;
    }

    // Na prvnim radku ma byt pocet objektu
    int pocetObjektu = CHYBA;
    if (fscanf(soubor, "count=%d", &pocetObjektu) != 1)
    {
        fprintf(stderr, "Nespravny format souboru!");
        return CHYBA;
    }
    
    // Alokovat pole clusteru
    *arr = malloc(sizeof(struct cluster_t) * pocetObjektu);
    if (*arr == NULL)
    {
        fprintf(stderr, "Nepodarilo se alokovat pamet pro pole clusteru!");
        return CHYBA;
    }

    // Dale nacist objekty
    int id, x, y, i;
    for (i = 0; i < pocetObjektu; i++)
    {
        if (fscanf(soubor, " %d %d %d", &id, &x, &y) != 3 || x < 0 || x > 1000 || y > 1000 || y < 0)
        { // Toto byla chyba, pred ukoncenim uklidit
            // Uklizeci proces
            for(int j = 0; j < i; j++)
                clear_cluster((*arr) + j);
            free(*arr);
            fclose(soubor);
            
            fprintf(stderr, "Obsah souboru neodpovida zadani!\n");
            return CHYBA;
        }
        
        init_cluster((*arr) + i, CLUSTER_CHUNK);
        struct obj_t objekt = { id = id, x = x, y = y }; 
        append_cluster((*arr) + i, objekt);
    }

    fclose(soubor);
    return pocetObjektu;
}

/*
 Tisk pole shluku. Parametr 'carr' je ukazatel na prvni polozku (shluk).
 Tiskne se prvnich 'narr' shluku.
*/
void print_clusters(struct cluster_t *carr, int narr)
{
    printf("Clusters:\n");
    for (int i = 0; i < narr; i++)
    {
        printf("cluster %d: ", i);
        print_cluster(&carr[i]);
    }
}

int main(int argc, char *argv[])
{
    struct cluster_t *clusters;

    int pozadovanyPocetShluku = (argc > 2) ? strtol(argv[2], NULL, 10) : 1;
    if (pozadovanyPocetShluku <= 0)
    {
        fprintf(stderr, "Nevalidni argument!\n");
        return EXIT_FAILURE;
    }

    int pocetObjektu = load_clusters(argv[1], &clusters);
    if (pocetObjektu == CHYBA)
        return EXIT_FAILURE;

    if (pocetObjektu < pozadovanyPocetShluku)
    {
        // Uklidit
        for(int i = 0; i < pocetObjektu; i++)
            clear_cluster(clusters + i);
        fprintf(stderr, "Prilis mnoho pozadovanych shluku!\n");
        free(clusters); // Vycistit dynamicky alokovane pole
        return EXIT_FAILURE;
    }
    
    // Postupne snizovani poctu clusteru
    int aktualneShluku = pocetObjektu;
    while (aktualneShluku > pozadovanyPocetShluku)
    {
        int nej1, nej2;
        find_neighbours(clusters, aktualneShluku, &nej1, &nej2); // Nalezneme nejblizsi shluky
        merge_clusters(clusters + nej1, clusters + nej2); // spojime je
        aktualneShluku = remove_cluster(clusters, aktualneShluku, nej2); // vymazeme prebyvajici
    }
    
    print_clusters(clusters, aktualneShluku);

    // Uklidit
    for(int i = 0; i < aktualneShluku; i++)
       clear_cluster(clusters + i);
    
    free(clusters); // Vycistit dynamicky alokovane pole
    return EXIT_SUCCESS;
}