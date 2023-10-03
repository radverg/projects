/**
 * IMS - projekt
 * prosinec 2020
 * Lukáš Wagner (xwagne10), Radek Veverka (xvever13)
 */

#include <algorithm>
#include "spawner.hpp"
#include <iostream>
// Check if given cell contains given behavior function
bool containsFunction(const cell_t& cell, entity_func_f f)
{
    for (auto &&i : cell.entity_behaviors)
    {
        if (i == f) return true;
    }

    return false;
}

void generatePlan(model_t& model, person_t* for_pers)
{
    auto& board = model.frames[0];
    std::vector<std::pair<int,int>> despawnables;

    for (size_t x = 0; x < board.size(); x++)
    {
        for (size_t y = 0; y < board[0].size(); y++)
        {
            if (containsFunction(board[x][y], despawnable))
            {
                despawnables.push_back({x, y});
                continue;
            }

            if (board[x][y].popularity == 0) continue;
            else 
            {
                double rnd = (double)rand() / RAND_MAX;
                if (rnd < board[x][y].popularity)
                    for_pers->plan.push_back({ x, y });
            }
        }
    }

    // In the end, person must go to one of despawnables
    for_pers->despawn = (despawnables[rand() % despawnables.size()]);
}

person_t* gimmeNewRandomGuy(model_t& model)
{
    person_t* p = new person_t();
    p->fume_rate = 0;
    p->patience = 1000; // 5 minutes of patience for blocking

    // Randomly generate person's health status
    std::discrete_distribution<int> dist(model.spawner.ppl_status_probs.begin(), model.spawner.ppl_status_probs.end());
    p->state = (person_t::state_t)dist(model.generator);
    if (p->state == person_t::INFECTING)
        p->fume_rate = COVID_MIN_FUME_RATE + ( (double)rand() / RAND_MAX ) * (COVID_MAX_FUME_RATE - COVID_MIN_FUME_RATE);
    generatePlan(model, p);
    return p;
}

void handleSpawnTick(model_t& model)
{
    // Get current probability fragment
    unsigned frag = floor((double)model.current_iteration / model.spawner.ticks_per_fragment);
    double prob = 0;
    if (frag < model.spawner.probability_fragments.size())
    {
        prob = model.spawner.probability_fragments[frag];
        // Now prob is in ppl per minute
        prob = (double)prob / (TICKS_PER_SEC * 60);
    }

    double rnd = (double)rand() / RAND_MAX;
    if (rnd < prob)
    {
        person_t* ptt = gimmeNewRandomGuy(model);
        ptt->starttick = 0;
        model.spawn_queue.push( ptt );
    }
}