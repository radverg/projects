/**
 * @file
 */

 /**
 * @author Radek Veverka
 * @date 5.12.2018
 * @mainpage IZP - project 3 - simple cluster analysis
 * This program takes points as input and merges them into clusters.
 */

/**
 * @defgroup structures Structures 
 * Structures for storing objects and clusters.
 * 
 * @defgroup clusters Cluster functions
 * Functins that manipulate with the clusters  
 *
 * @defgroup printing Printing functions
 * Functions that are supposed to print clusters to the output 
 * 
 * @defgroup constants Constants
 * Constant values for this program.
 * 
 * @defgroup distance Distance functions
 * Functions for counting distances and finding closest objects/clusters
 * 
 * @defgroup sorting Sorting functions
 * Functions for sorting some stuff (clusters).
 */

/**
 * @brief Object structure with unique ID.
 * Represents a point with two coordinates.
 * @ingroup structures
 */
struct obj_t {
    int id;     /**< An unique id of an object. Integer. */
    float x;    /**< X coordinate of this object (point). */
    float y;    /**< Y coordinate of this object (point). */
};

/**
 * @brief Cluster - dynamic array for objects.
 * Contains information about current object count and object capacity.
 * @ingroup structures
 */
struct cluster_t {
    int size;       /**< Current amount of objects stored in the array. */
    int capacity;   /**< Maximum amount of objects that can be stored in the array */
    struct obj_t *obj; /**< Object array - pointer to the first object */
};


/**
 * Initializes a new cluster by allocating heap memory for specified amount of objects.
 * Sets the size of the cluster to 0.
 * @ingroup clusters
 * @param c A pointer to the empty cluster.
 * @param cap For how many objects the memory should be allocated. 
 * @pre c is not NULL pointer
 * @pre cap is greater or equal to zero
 * @post Memory for desired capacity is allocated.
 */
void init_cluster(struct cluster_t *c, int cap);

/**
 * Deallocates memory used for the objects in this cluster.
 * Reinitializes this cluster with 0 capacity.
 * @ingroup clusters
 * @param c A pointer to the cluster that is to be cleared.
 */
void clear_cluster(struct cluster_t *c);

/**
 * Preferred amount of objects for increasing cluster capacity.
 * @ingroup constants
 */
extern const int CLUSTER_CHUNK;

/**
 * Changes cluster's capacity by reallocating memory for it's objects.
 * @ingroup clusters
 * @param c Pointer to a cluster which is to be resized.
 * @param new_cap The new amount of objects in this cluster.
 * @return struct cluster_t* Pointer to the changed cluster on success. NULL on failure.
 */
struct cluster_t *resize_cluster(struct cluster_t *c, int new_cap);

/**
 * Adds a new object to the end of the cluster and increments size of the cluster
 * If needed, capacity of the cluster is increased.
 * @ingroup clusters
 * @param c Pointer to a cluster that should append a new object.
 * @param obj Object that should be appended.
 */
void append_cluster(struct cluster_t *c, struct obj_t obj);

/**
 * Takes objects from one cluster (c2) and appends them to another cluster (c1).
 * If needed, capacity of the cluster is increased.
 * @ingroup clusters
 * @param c1 Cluster that will be extended.
 * @param c2 Cluster whose objects will be copied.
 * @pre Both arguments are not NULL pointers.
 * @post Objects from c2 are attached to c1.
 * @post c2 remains unchanged
 * @post c1's objects are sorted by id 
 */
void merge_clusters(struct cluster_t *c1, struct cluster_t *c2);

/**
 * Removes a cluster from the array of clusters on specified index.
 * @ingroup clusters
 * @param carr Array of clusters.
 * @param narr Amount of clusters in the array.
 * @param idx Index of a cluster to be removed.
 * @return int New amount of clusters in tha array.
 * @pre idx is lower than narr
 * @pre narr is greater than 0
 * @post Objects of removed cluster are cleared.
 * @post Cluster array does not contain removed cluster.
 */
int remove_cluster(struct cluster_t *carr, int narr, int idx);

/**
 * Counts the disatance between two objects (points) using Pythagoras theorem.
 * @ingroup distance
 * @param o1 Pointer to the first object
 * @param o2 Pointer to the second object
 * @return float The distatnce between two points.
 * @pre c1 is not a NULL pointer
 * @pre c2 is not a NULL pointer
 */
float obj_distance(struct obj_t *o1, struct obj_t *o2);

/**
 * Counts distance between two clusters by finding the closest points.
 * @ingroup distance
 * @param c1 Pointer to the first cluster
 * @param c2 Pointer to the second cluster
 * @return float Distance between clusters
 * @pre c1 is not a NULL pointer
 * @pre c2 is not a NULL pointer
 * @pre both clusters have size at least 1
 */
float cluster_distance(struct cluster_t *c1, struct cluster_t *c2);

/**
 * Finds two closest clusters in the array of clusters
 * @ingroup distance
 * @param carr The cluster array
 * @param narr Amount of elements in the array
 * @param c1 Pointer for storing index of one cluster
 * @param c2 Pointer for storing index of second cluster
 * @pre Cluster array is not empty
 * @post Two closest clusters are saved to c1 and c2 passed by reference
 */
void find_neighbours(struct cluster_t *carr, int narr, int *c1, int *c2);

/**
 * Sorts objects in the cluster according to their ID's.
 * Uses quicksort algorithm.
 * @ingroup sorting
 * @param c Pointer to a cluster whose objects are to be sorted.
 */
void sort_cluster(struct cluster_t *c);

/**
 * Prints text representation of a cluster to the standard output.
 * Uses following format: ID[X,Y] ID[X,Y] ...
 * @ingroup printing
 * @param c Pointer to a cluster that is to be printed.
 */
void print_cluster(struct cluster_t *c);

/**
 * Loads objects from a file and puts them to the clusters one by one.
 * Allocates memory for the cluster array.
 * 
 * @ingroup clusters
 * @param filename Name of file to be opened.
 * @param arr Pointer to an array that will be allocated.
 * @return int Amount of objects (clusters) loaded. -1 on error.
 */
int load_clusters(char *filename, struct cluster_t **arr);

/**
 * Calls print_cluster on each member of cluster array
 * @ingroup printing
 * @param carr Cluster array.
 * @param narr Cluster array length.
 */
void print_clusters(struct cluster_t *carr, int narr);
