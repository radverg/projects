/**
 * IMS - projekt
 * prosinec 2020
 * Lukáš Wagner (xwagne10), Radek Veverka (xvever13)
 */

#pragma once

#include <vector>
#include <queue>
#include <random>

using namespace std;
#define TICKS_PER_SEC (3)
#define COVID_MIN_FUME_RATE (0.012)
#define COVID_MAX_FUME_RATE (0.139)
#define FRAME_BUFFER_LENGTH (2)

struct spawner_t
{
    std::vector<double> probability_fragments;
    int ticks_per_fragment = TICKS_PER_SEC * 3600;
    std::vector<double> ppl_status_probs;
};

/**
 * Structure representing person in model
 */
struct person_t {
    enum state_t {
        REGULAR,    ///< not infected yet
        INFECTING,  ///< infecting others
        IMMUNE,      ///< constant
        INFECTED   ///< output state, non infecting (incubation)
    };
    state_t state;

    double fume_rate;       ///< rate of infecting vicinity (cells) | accumulated if REGULAR
    long long wait_time;    ///< if not present walk, if in new_frame pause (walk before pause) and not present add
    int patience;
    
    pair<int,int> despawn;
    vector<pair<int,int>> plan; ///< places planned to be visited
    vector<pair<int,int>> path; ///< optimization to not calculate whole path every step
    unsigned long starttick;
};

typedef struct cell_t cell_t;
typedef struct model_t model_t;
typedef int (*entity_func_f)(vector<vector<cell_t>>& frame_old, vector<vector<cell_t>>& frame_new, int x, int y, model_t& model);

/**
 * Strucutre representing a single cell
 */
struct cell_t {
    // double stay_mid;        ///< for pause, mu of log-normal
    // double stay_dev;        ///< for pause, sig of log-normal
    // vvvv  takes the mu and sig above as params in constructor
    std::lognormal_distribution<double> distrib; ///< for pause, distribution of stay time
    double popularity;      ///< for pause, likelihood of visit
    char group;             ///< walking order group

    double fumes;           ///< infectivity of cell
    person_t* inhabitant;   ///< person_t | NULL
    vector<entity_func_f> entity_behaviors;

};

struct simstats_t {
    unsigned long long target_itercount = 0;
    unsigned long long itercount = 0;
    unsigned spawned_regular = 0;
    unsigned spawned_infecting = 0;
    unsigned spawned_immune = 0;
    unsigned left_infected = 0;
    unsigned left_uninfected = 0;
    unsigned left_total = 0;
    unsigned ticksspent_total = 0;
    double fumes_total = 0;
};

void drawStats(FILE* out, const model_t& model);
void drawPersonLeaveStats(FILE* out, const person_t* p);

typedef struct simstats_t simstats_t;

/**
 * Structure representing main model data
 */
struct model_t {
    std::default_random_engine generator;   ///< default rng, no need to initialize I think
    std::exponential_distribution<double> infect_dist = std::exponential_distribution<double>(1);
    spawner_t spawner;                      ///< Configuration of spawning ppl.
    vector<vector<vector<cell_t>>> frames;  ///< buffer for frames, switching frames each iteration
    char frame_num;                         ///< current frame
    simstats_t stats;

    unsigned long long iterations;  ///< loaded, decrementing by one, time to walk one cell
    unsigned long long current_iteration = 0;
    int distancing;                 ///< set distancing number of cells
    queue<person_t*> spawn_queue;         ///< number of people waiting in their cars on mall limit
};

int walkable(vector<vector<cell_t>>& frame_old, vector<vector<cell_t>>& frame_new, int x, int y, model_t& model);
int spreadable(vector<vector<cell_t>>& frame_old, vector<vector<cell_t>>& frame_new, int x, int y, model_t& model);
int spawnable(vector<vector<cell_t>>& frame_old, vector<vector<cell_t>>& frame_new, int x, int y, model_t& model);
int despawnable(vector<vector<cell_t>>& frame_old, vector<vector<cell_t>>& frame_new, int x, int y, model_t& model);
int pausable(vector<vector<cell_t>>& frame_old, vector<vector<cell_t>>& frame_new, int x, int y, model_t& model);

// enum type_t {
//     SPACE,  ///< passable
//     WALL,   ///< common impassable
//     QUEUE,  ///< cash register for example
//     PAUSE,  ///< shelfs
//     HALF,   ///< half wall, common impassable but with spread
//     SPAWN   ///< enter and leave point
// };
// type_t type;

#define RED "31"
#define GREEN "32"
#define YELLOW "33"
#define BLUE "34"
#define MAGENTA "35"
#define CYAN "36"
#define WHITE "37"

#define BG_RED "41"
#define BG_BLUE "44"
#define BG_MAGENTA "45"
#define BG_CYAN "46"
#define BG_WHITE "47"
void putc_col(const char c, const char* col, const char* bg);
void print_map(vector<vector<cell_t>>& frame);
