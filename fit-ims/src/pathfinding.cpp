/**
 * IMS - projekt
 * prosinec 2020
 * Lukáš Wagner (xwagne10), Radek Veverka (xvever13)
 */

#include "cellular_automata.hpp"
#include "pathfinding.hpp"

#include <iostream>
#include <algorithm>
#include <queue>


bool blocked_path(vector<vector<cell_t>>& frame_old, int x, int y, person_t* person, int dist)
{
    if (frame_old[x][y].inhabitant != nullptr){
        return true;
    }
    
    int x_min, y_min, x_max, y_max;
    x_min = max(x-dist, 0);
    y_min = max(y-dist, 0);
    x_max = min(x+dist, (int)frame_old.size()-1);      // WATCHOUT FOR OVERFLOW (we are working with small area tho...)
    y_max = min(y+dist, (int)frame_old[x].size()-1);   // WATCHOUT FOR OVERFLOW (we are working with small area tho...)

    // create sub-area for path find
    vector<vector<tile_t>>area(x_max-x_min+1, vector<tile_t>(y_max-y_min+1));
    for(int i = 0; i <= x_max-x_min; i++){
        for(int j = 0; j <= y_max-y_min; j++){
            cell_t& cell = frame_old[i+x_min][j+y_min];
            if (cell.inhabitant != nullptr){
                if(cell.inhabitant != person)
                    // somebody else
                    area[i][j].type = dist+1;
                else
                    // central
                    area[i][j].type = 0;
            } else if (find(cell.entity_behaviors.begin(), cell.entity_behaviors.end(), &spreadable) == cell.entity_behaviors.end()){
                // not spreadable
                area[i][j].type = -2;
            } else {
                // free space
                area[i][j].type = 0;
            }
        }
    }
    if (area[x-x_min][y-y_min].type > 0){
        // walk right into him
        return true;
    }
    area[x-x_min][y-y_min].type = -1;

    // flood fill
    return flood_fill(area); // returns true if touched
    
}

int pathfind(int x, int y, vector<vector<cell_t>>& frame_old)
{
    vector<vector<tile_t>>area(frame_old.size(), vector<tile_t>(frame_old[0].size()));

    for(int i = 0; i < (int)frame_old.size(); i++){
        for(int j = 0; j < (int)frame_old[i].size(); j++){
            cell_t& cell = frame_old[i][j];
            if (find(cell.entity_behaviors.begin(), cell.entity_behaviors.end(), &walkable) == cell.entity_behaviors.end()){
                // not walkable
                area[i][j].type = -2;
            } else {
                // free space
                area[i][j].type = 0;
            }
            area[i][j].group = frame_old[i][j].group;
            area[i][j].x = i;
            area[i][j].y = j;
        }
    }

    flood_fill_path(area, frame_old[x][y].inhabitant, x, y);
    return 0;
}

int pathfind_people(int x, int y, vector<vector<cell_t>>& frame_old, int dist)
{
    vector<vector<tile_t>>area(frame_old.size(), vector<tile_t>(frame_old[0].size()));

    for(int i = 0; i < (int)frame_old.size(); i++){
        for(int j = 0; j < (int)frame_old[i].size(); j++){
            cell_t& cell = frame_old[i][j];
            if (find(cell.entity_behaviors.begin(), cell.entity_behaviors.end(), &walkable) == cell.entity_behaviors.end()){
                // not walkable
                area[i][j].type = -2;
            } else if (cell.inhabitant != nullptr){
                if(cell.inhabitant != frame_old[x][y].inhabitant)
                    // somebody else
                    area[i][j].type = dist+1;
                else
                    // central
                    area[i][j].type = -1;
            } else {
                // free space
                area[i][j].type = 0;
            }
            area[i][j].group = frame_old[i][j].group;
            area[i][j].x = i;
            area[i][j].y = j;
        }
    }

    flood_fill(area);
    for(int i = 0; i < (int)area.size(); i++){
        for(int j = 0; j < (int)area[i].size(); j++){
            if (area[i][j].type > 0){
                area[i][j].type = -2;
            }
        }
    }

    flood_fill_path(area, frame_old[x][y].inhabitant, x, y);

    return 0;
}

int flood_fill_path(vector<vector<tile_t>>& area, person_t* person, int x, int y)
{
    // static const auto less = [](tile_t* x, tile_t* y){return x->dist > y->dist;};
    // priority_queue<tile_t*, vector<tile_t*>, decltype(less)> queue(less);
    person->path.clear();
    queue<tile_t*> queue;
    vector<tile_t*> closed;

        // cerr<<"   ";
        // for(int lol = 0; lol < area[0].size(); lol++){
        //     cerr<<lol<< (lol<10?" ":"");
        // }
        // cerr<<endl;
        // int lol = 0;
        // for(auto& col : area){
        //     cerr<<lol<< (lol<10?"  ":" ");
        //     lol++;
        //     for(tile_t& cell : col){
        //         cerr<<cell.type<< " ";
        //     }
        //     cerr<<endl;
        // }

    area[x][y].dist = 1;
    queue.push(&area[x][y]);

    bool x_over;
    bool x_under;
    bool y_over;
    bool y_under;

    while (!queue.empty())
    {
        tile_t* now = queue.front();
        if (find(closed.begin(),closed.end(),now) != closed.end()){
            // is in closed
            queue.pop();
            continue;
        }
        closed.push_back(now);
        
        pair<int,int> coords = {now->x,now->y};
        if (person->plan.empty()){
            if (person->despawn == coords){
                break;
            }
        }
        if (find(person->plan.begin(),person->plan.end(),coords) != person->plan.end()){
            break;
        }
        queue.pop();

        x_over = now->x + 1 >= (int)area.size();
        x_under = now->x - 1 < 0;
        y_over = now->y + 1 >= (int)area[0].size();
        y_under = now->y - 1 < 0;

        if(!x_over){
            if(!y_over){
                if(area[now->x+1][now->y+1].dist == 0 || area[now->x+1][now->y+1].dist > area[now->x][now->y].dist){
                    if (area[now->x+1][now->y+1].group == now->group || area[now->x+1][now->y+1].group == now->group+1){
                        if (area[now->x+1][now->y+1].type != -2){
                            area[now->x+1][now->y+1].dist = now->dist+1;
                            queue.push(&area[now->x+1][now->y+1]);
                        }
                    }
                }
            }
            if(!y_under){
                if(area[now->x+1][now->y-1].dist == 0 || area[now->x+1][now->y-1].dist > area[now->x][now->y].dist){
                    if (area[now->x+1][now->y-1].group == now->group || area[now->x+1][now->y-1].group == now->group+1){
                        if (area[now->x+1][now->y-1].type != -2){
                            area[now->x+1][now->y-1].dist = now->dist+1;
                            queue.push(&area[now->x+1][now->y-1]);
                        }
                    }
                }
            }
            if(area[now->x+1][now->y].dist == 0 || area[now->x+1][now->y].dist > area[now->x][now->y].dist){
                if (area[now->x+1][now->y].group == now->group || area[now->x+1][now->y].group == now->group+1){
                    if (area[now->x+1][now->y].type != -2){
                        area[now->x+1][now->y].dist = now->dist+1;
                        queue.push(&area[now->x+1][now->y]);
                    }
                }
            }
        }
        if(!x_under){
            if(!y_over){
                if(area[now->x-1][now->y+1].dist == 0 || area[now->x-1][now->y+1].dist > area[now->x][now->y].dist){
                    if (area[now->x-1][now->y+1].group == now->group || area[now->x-1][now->y+1].group == now->group+1){
                        if (area[now->x-1][now->y+1].type != -2){
                            area[now->x-1][now->y+1].dist = now->dist+1;
                            queue.push(&area[now->x-1][now->y+1]);
                        }
                    }
                }
            }
            if(!y_under){
                if(area[now->x-1][now->y-1].dist == 0 || area[now->x-1][now->y-1].dist > area[now->x][now->y].dist){
                    if (area[now->x-1][now->y-1].group == now->group || area[now->x-1][now->y-1].group == now->group+1){
                        if (area[now->x-1][now->y-1].type != -2){
                            area[now->x-1][now->y-1].dist = now->dist+1;
                            queue.push(&area[now->x-1][now->y-1]);
                        }
                    }
                }
            }
            if(area[now->x-1][now->y].dist == 0 || area[now->x-1][now->y].dist > area[now->x][now->y].dist){
                if (area[now->x-1][now->y].group == now->group || area[now->x-1][now->y].group == now->group+1){
                    if (area[now->x-1][now->y].type != -2){
                        area[now->x-1][now->y].dist = now->dist+1;
                        queue.push(&area[now->x-1][now->y]);
                    }
                }
            }
        }
        if(!y_over){
            if(area[now->x][now->y+1].dist == 0 || area[now->x][now->y+1].dist > area[now->x][now->y].dist){
                if (area[now->x][now->y+1].group == now->group || area[now->x][now->y+1].group == now->group+1){
                    if (area[now->x][now->y+1].type != -2){
                        area[now->x][now->y+1].dist = now->dist+1;
                        queue.push(&area[now->x][now->y+1]);
                    }
                }
            }
        }
        if(!y_under){
            if(area[now->x][now->y-1].dist == 0 || area[now->x][now->y-1].dist > area[now->x][now->y].dist){
                if (area[now->x][now->y-1].group == now->group || area[now->x][now->y-1].group == now->group+1){
                    if (area[now->x][now->y-1].type != -2){
                        area[now->x][now->y-1].dist = now->dist+1;
                        queue.push(&area[now->x][now->y-1]);
                    }
                }
            }
        }
    }

    if (!queue.empty()){ // found a destination

        // cerr<<"   ";
        // for(int lol = 0; lol < area[0].size(); lol++){
        //     cerr<<lol<< (lol<10?" ":"");
        // }
        // cerr<<endl;
        // int lol = 0;
        // for(auto& col : area){
        //     cerr<<lol<< (lol<10?"  ":" ");
        //     lol++;
        //     for(tile_t& cell : col){
        //         cerr<<cell.dist<< " ";
        //     }
        //     cerr<<endl;
        // }



        tile_t* now = queue.front();
        while(now != &area[x][y])
        {
            pair<int,int> coords = {now->x,now->y};
            person->path.push_back(coords);

            bool x_over(now->x + 1 >= (int)area.size());
            bool x_under(now->x - 1 < 0);
            bool y_over(now->y + 1 >= (int)area[0].size());
            bool y_under(now->y - 1 < 0);

            tile_t* tobe = now;
            if(!x_over){
                if(!y_over){
                    if(area[now->x+1][now->y+1].dist > 0 && area[now->x+1][now->y+1].dist < tobe->dist){
                        tobe = &area[now->x+1][now->y+1];
                    }
                }
                if(!y_under){
                    if(area[now->x+1][now->y-1].dist > 0 && area[now->x+1][now->y-1].dist < tobe->dist){
                        tobe = &area[now->x+1][now->y-1];
                    }
                }
                if(area[now->x+1][now->y].dist > 0 && area[now->x+1][now->y].dist < tobe->dist){
                    tobe = &area[now->x+1][now->y];
                }
            }
            if(!x_under){
                if(!y_over){
                    if(area[now->x-1][now->y+1].dist > 0 && area[now->x-1][now->y+1].dist < tobe->dist){
                        tobe = &area[now->x-1][now->y+1];
                    }
                }
                if(!y_under){
                    if(area[now->x-1][now->y-1].dist > 0 && area[now->x-1][now->y-1].dist < tobe->dist){
                        tobe = &area[now->x-1][now->y-1];
                    }
                }
                if(area[now->x-1][now->y].dist > 0 && area[now->x-1][now->y].dist < tobe->dist){
                    tobe = &area[now->x-1][now->y];
                }
            }
            if(!y_over){
                if(area[now->x][now->y+1].dist > 0 && area[now->x][now->y+1].dist < tobe->dist){
                    tobe = &area[now->x][now->y+1];
                }
            }
            if(!y_under){
                if(area[now->x][now->y-1].dist > 0 && area[now->x][now->y-1].dist < tobe->dist){
                    tobe = &area[now->x][now->y-1];
                }
            }
            now = tobe;
        }
    }

    return 0;
}

bool flood_fill(vector<vector<tile_t>>& area)
{
    int x_min, y_min, x_max, y_max;
    bool changed = true;
    while(changed)
    {
        changed = false;
        // for every cell
        for(int i = 0; i < (int)area.size(); i++){
            for(int j = 0; j < (int)area[i].size(); j++){
                // if cell is potential distance
                if (area[i][j].type > 1){
                    // create it's vicinity and spread it
                    x_min = max(i-1, 0);
                    y_min = max(j-1, 0);
                    x_max = min(i+1, (int)area.size()-1);      // WATCHOUT FOR OVERFLOW (we are working with small area tho...)
                    y_max = min(j+1, (int)area[i].size()-1);   // WATCHOUT FOR OVERFLOW (we are working with small area tho...)
                    for(int k = x_min; k <= x_max; k++){
                        for(int l = y_min; l <= y_max; l++){
                            if (area[k][l].type == -1){
                                return true;
                            }
                            if (area[k][l].type == -2){
                                // cannot spread through unspreadable
                                continue;
                            }
                            if (area[k][l].type < area[i][j].type -1){
                                changed = true;
                                area[k][l].type = area[i][j].type - 1;
                            }
                        }
                    }
                }
            }
        }
    }

    return false;
}