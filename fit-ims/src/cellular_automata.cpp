/**
 * IMS - projekt
 * prosinec 2020
 * Lukáš Wagner (xwagne10), Radek Veverka (xvever13)
 */

#include "cellular_automata.hpp"
#include "arg_parse.hpp"
#include "pathfinding.hpp"
#include "spawner.hpp"
#include "model_loader.hpp"

#include <iostream>
#include <random>
#include <algorithm>

int main(int argc, char* argv[])
{
    settings sett;
    model_t model;
    if (arg_parse(argc, argv, sett) != 0){
        return 1;
    }

    if (loadModel(sett.filename, model) != 0){
        cerr<<"file err\n";
        return 1;
    }

    for (;model.iterations > 0; model.iterations--)
    {
        model.current_iteration++;
        model.stats.itercount++;
        
        // handle people generation to queue, spawning handles cells
        handleSpawnTick(model);

        // switch frames (cycle through)
        auto& frame_old = model.frames[model.frame_num];
        model.frame_num = (model.frame_num + 1) % model.frames.size();
        auto& frame_new = model.frames[model.frame_num];

        ///////////// STATISTICS /////////////
        if (sett.stats_every != 0)
            if (!(model.current_iteration % sett.stats_every))
            {
                drawStats(stderr, model);
                
            }
        if (sett.maps_every != 0)
            if (!(model.current_iteration % sett.maps_every))
            {
                print_map(frame_old);
            }
        //////////////////////////////////////


        for(unsigned x = 0; x<frame_new.size(); x++){
            for(unsigned y = 0; y<frame_new[x].size(); y++)
            {
                // needed for additive spread withoud exponential growth
                frame_new[x][y].fumes = 0.0;
                frame_new[x][y].inhabitant = nullptr;
            }
        }

        for(unsigned x = 0; x<frame_old.size(); x++){
            for(unsigned y = 0; y<frame_old[x].size(); y++)
            {
                cell_t& cell = frame_old[x][y];
                // Entity component system
                for(entity_func_f entity_func : cell.entity_behaviors)
                {
                    entity_func(frame_old, frame_new, x, y, model);
                }
            }
        }
    }

    // print final statistics
    drawStats(stdout, model);
    return 0;
}




int walkable(vector<vector<cell_t>>& frame_old, vector<vector<cell_t>>& frame_new, int x, int y, model_t& model)
{
    cell_t& cell = frame_old[x][y];
    if (cell.inhabitant == nullptr){
        return 0;
    }

    if (cell.inhabitant->wait_time != 0){
        frame_new[x][y].inhabitant = cell.inhabitant;
        return 0;
    }

    if (cell.inhabitant->path.size() == 0)
    {
        // A* without people
        pathfind(x, y, frame_old);
    }

    pair<int,int> coord;
    cell_t* new_cell;

    coord = cell.inhabitant->path.back();
    new_cell = &frame_new[coord.first][coord.second];

    if (blocked_path(frame_old, coord.first, coord.second, cell.inhabitant, model.distancing))
    {
        // pathfind with people, if cannot walk around path=empty
        pathfind_people(x, y, frame_old, model.distancing);

        if (cell.inhabitant->path.size() == 0)
        {
            // on blocked path waits
            frame_new[x][y].inhabitant = cell.inhabitant;
            cell.inhabitant->patience--;
            if (cell.inhabitant->patience <= 0)
            {
                cell.inhabitant->plan.clear();
            }
            return 0;
        }
    }

    coord = cell.inhabitant->path.back();
    new_cell = &frame_new[coord.first][coord.second];

    if (blocked_path(frame_new, coord.first, coord.second, cell.inhabitant, model.distancing) || blocked_path(frame_old, coord.first, coord.second, cell.inhabitant, model.distancing))
    {
        frame_new[x][y].inhabitant = cell.inhabitant;
        cell.inhabitant->patience--;
        if (cell.inhabitant->patience <= 0)
        {
            cell.inhabitant->plan.clear();
        }
        return 0;
    }

    // on path walk
    if (cell.inhabitant->path.size() > 0)
    {
        new_cell->inhabitant = cell.inhabitant;
        cell.inhabitant->path.pop_back();
        return 0;
    }
    // on blocked path waits
    frame_new[x][y].inhabitant = cell.inhabitant;
    cell.inhabitant->patience--;
    if (cell.inhabitant->patience <= 0)
    {
        cell.inhabitant->plan.clear();
    }
    return 0;
}

int spreadable(vector<vector<cell_t>>& frame_old, vector<vector<cell_t>>& frame_new, int x, int y, model_t& model)
{
    (void)model;
    cell_t& cell = frame_old[x][y];

    if (cell.inhabitant != nullptr){
        if (cell.inhabitant->state == cell.inhabitant->REGULAR){
            cell.inhabitant->fume_rate += cell.fumes*0.0002; // Inhale rate with slight activity (1 m3/h) 1/10800 * 2 (one cell is only 0.5 m3)
            cell.fumes *= 0.9998;
        } else if (cell.inhabitant->state == cell.inhabitant->INFECTING){
            cell.fumes += cell.inhabitant->fume_rate;
        }
    }

    if (cell.fumes <= 0.0001){
        return 0;
    }
    // cerr<< frame_new[x][y].fumes << std::endl;

    // spreading constants
    // remember total survive = sum of survice, 4*lat_spread and 4*dia_spread
    // for SARS-CoV-2 should be total survive 0.99995 to match its half-life (0.64/h alive)
    // that is for uventilated and no gravity
    // (gravity dead 0.24/h, mechanical ventilation exchange rate  1.1/h -> actual rate (perfect mixing) 0.3 not changed)
    // 0.3*0.64*0.76 ~~> 0.146 
    // sum = 0.99982
    static const float survive = 0.96382;       // matched to resemble SARS-CoV-2 decay
    static const float lateral_spread = 0.005;  // spread depends on air flow, can differ
    static const float diagonal_spread = 0.004;

    int x_min, y_min, x_max, y_max;
    x_min = max(x-1, 0);
    y_min = max(y-1, 0);
    x_max = min(x+1, (int)frame_old.size()-1);      // WATCHOUT FOR OVERFLOW (we are working with small area tho...)
    y_max = min(y+1, (int)frame_old[x].size()-1);   // WATCHOUT FOR OVERFLOW (we are working with small area tho...)

    // cell.fumes *= survive;

    for(int i = x_min; i <= x_max; i++){
        for(int j = y_min; j <= y_max; j++){
            if (i == x && j == y){
                // same - no spread
                frame_new[i][j].fumes += (cell.fumes * survive);
            } else if (i == x || j == y){
                //lateral
                frame_new[i][j].fumes += (cell.fumes * lateral_spread);
            } else {
                // diagonal
                frame_new[i][j].fumes += (cell.fumes * diagonal_spread);
            }
        }
    }

    // frame_old[x][y].fumes = 0;

    return 0;
}

int spawnable(vector<vector<cell_t>>& frame_old, vector<vector<cell_t>>& frame_new, int x, int y, model_t& model)
{
    (void)frame_new;
    cell_t& cell = frame_old[x][y];
    if (!model.spawn_queue.empty()){
        if (!blocked_path(frame_old, x, y, nullptr, model.distancing)){
            cell.inhabitant = model.spawn_queue.front();
            person_t* ptt = cell.inhabitant;
            ptt->starttick = model.current_iteration;
            if (ptt->state == person_t::INFECTING) model.stats.spawned_infecting++;
            if (ptt->state == person_t::IMMUNE) model.stats.spawned_immune++;
            if (ptt->state == person_t::REGULAR) model.stats.spawned_regular++;
            model.spawn_queue.pop();
        }
    }

    return 0;
}

int despawnable(vector<vector<cell_t>>& frame_old, vector<vector<cell_t>>& frame_new, int x, int y, model_t& model)
{
    (void)frame_new;
    (void)model;
    cell_t& cell = frame_old[x][y];
    if (cell.inhabitant != nullptr){
        if (cell.inhabitant->plan.size() == 0)
        {
            if (cell.inhabitant->state == person_t::REGULAR)
            {
                if (model.infect_dist(model.generator) < cell.inhabitant->fume_rate)
                {
                    model.stats.left_infected++;
                    cell.inhabitant->state = person_t::INFECTED;
                }
                else
                {
                    model.stats.left_uninfected++;
                }

                model.stats.fumes_total += cell.inhabitant->fume_rate;
            }
           
            model.stats.ticksspent_total += (model.current_iteration - cell.inhabitant->starttick);
            model.stats.left_total++;
            // drawPersonLeaveStats(stdout, cell.inhabitant);
            delete cell.inhabitant;
            cell.inhabitant = nullptr;
        }
    }

    return 0;
}

int pausable(vector<vector<cell_t>>& frame_old, vector<vector<cell_t>>& frame_new, int x, int y, model_t& model)
{
    (void)frame_new;
    cell_t& cell = frame_old[x][y];
    if (cell.inhabitant != nullptr){
        if (cell.inhabitant->wait_time != 0)
        {
            cell.inhabitant->wait_time--;
        }
        else
        {
            auto& plan = cell.inhabitant->plan;
            auto tmp = find(plan.begin(),plan.end(),pair<int,int>(x,y));
            if (tmp != plan.end()){
                cell.inhabitant->wait_time = (long long)cell.distrib(model.generator);
                plan.erase(tmp);
            }
        }
    }

    return 0;
}

void putc_col(const char c, const char* col, const char* bg)
{
    std::cerr<< "\033[" << col << ";" << bg << "m" << c << "\033[0m";
}

void print_map(vector<vector<cell_t>>& frame)
{
    for (auto& row : frame)
    {
        for (cell_t& cell : row)
        {
            const char* fg = "37"; // white
            const char* bg = "40"; // black
            char c = ' ';
            if(find(cell.entity_behaviors.begin(),cell.entity_behaviors.end(), &walkable) == cell.entity_behaviors.end())
            {
                fg = "30"; // black
                c = '#';
            }
            if(find(cell.entity_behaviors.begin(),cell.entity_behaviors.end(), &spreadable) != cell.entity_behaviors.end())
            {
                if(cell.fumes >= 0.3)
                    bg = BG_RED;
                else if (cell.fumes >= 0.01)
                    bg = BG_MAGENTA;
                else
                    bg = BG_WHITE;
            }
            if(cell.inhabitant != nullptr)
            {
                c = 'o';
                if (cell.inhabitant->state == cell.inhabitant->INFECTING)
                    fg = GREEN;
                if (cell.inhabitant->state == cell.inhabitant->INFECTED)
                    fg = YELLOW;
                if (cell.inhabitant->state == cell.inhabitant->IMMUNE)
                    fg = "30";
                if (cell.inhabitant->state == cell.inhabitant->REGULAR)
                    fg = BLUE;
            }
            putc_col(c,fg,bg);
        }
        cerr<<endl;
    }
    cerr<<endl;
}

void drawPersonLeaveStats(FILE* out, const person_t* p)
{
    fprintf(out, "-- Person left: ");
    fprintf(out, "Fume rate = %lf; ", p->fume_rate);
    static const char* types[] = {
        "REGULAR", "INFECTING", "IMUNNE", "INFECTED"
    };
    fprintf(out, "Type: %s\n", types[p->state]);
}

void drawStats(FILE* out, const model_t& model)
{
    auto& stats = model.stats;
    int total_spawned = stats.spawned_immune + stats.spawned_infecting + stats.spawned_regular;
    fprintf(out, "---- Sim stats ----\n");
    fprintf(out, "Iteration: %llu of %llu\n", stats.itercount, stats.target_itercount);
    fprintf(out, "Total entered: %d\n", total_spawned);
    fprintf(out, "Entered regular: %d\n", stats.spawned_regular);
    fprintf(out, "Entered infecting: %d\n", stats.spawned_infecting);
    fprintf(out, "Entered immune: %d\n", stats.spawned_immune);

    fprintf(out, "Average shopping time: %fmin\n", (((float)stats.ticksspent_total / stats.left_total) / 3) / 60);
    fprintf(out, "Average fumes inhaled: %lf\n", stats.fumes_total / (stats.left_infected + stats.left_uninfected));

    fprintf(out, "\nLeft total: %d\n", stats.left_total);
    fprintf(out, "Left newly infected: %d\n", stats.left_infected);

    
    int in_shop_real = 0;
    for (size_t i = 0; i < model.frames[0].size(); i++)
    {
        for (size_t y = 0; y < model.frames[0][i].size(); y++)
        {
            /* code */
            if (model.frames[0][i][y].inhabitant != nullptr)
                in_shop_real++;
        }
        
        /* code */
    }
    
    fprintf(out, "\nIn shop: %d\n", in_shop_real); 
    fprintf(out, "In entrance queue: %lu\n", model.spawn_queue.size());

    fprintf(out, "\nNewly infected/total: %lf\n", (double)stats.left_infected / stats.left_total);
    fprintf(out, "Newly infected/total regular: %lf\n", (double)stats.left_infected / (stats.left_infected + stats.left_uninfected));

    fprintf(out, "-------------------\n");
}