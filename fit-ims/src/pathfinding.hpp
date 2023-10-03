/**
 * IMS - projekt
 * prosinec 2020
 * Lukáš Wagner (xwagne10), Radek Veverka (xvever13)
 */
#pragma once

#include <vector>
#include "cellular_automata.hpp"

using namespace std;

struct tile_t {
    int x;
    int y;
    int dist;
    int group;
    int type;
};


bool blocked_path(vector<vector<cell_t>>& frame_old, int x, int y, person_t* person, int dist);
int pathfind(int x, int y, vector<vector<cell_t>>& frame_old);
int pathfind_people(int x, int y, vector<vector<cell_t>>& frame_old, int dist);
int flood_fill_path(vector<vector<tile_t>>&, person_t* person, int x, int y);
bool flood_fill(vector<vector<tile_t>>& area);