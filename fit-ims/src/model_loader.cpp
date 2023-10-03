/**
 * IMS - projekt
 * prosinec 2020
 * Lukáš Wagner (xwagne10), Radek Veverka (xvever13)
 */

#include "model_loader.hpp"

void finishLine(FILE* f)
{
    int tmpchar = fgetc(f);
    while (tmpchar != '\n' && tmpchar != EOF)
    {
        tmpchar = fgetc(f);
    }
}

int loadModel(const char* file_name, model_t& frame)
{
    // Seed random number generator
    srand(time(NULL));
    FILE* file = fopen(file_name, "r");
    if (file == nullptr)
    {
        perror("Error while trying to read file with simulation.");
        return EXIT_FAILURE;
    }

    // Sim length
    unsigned hours;
    fscanf(file, "%ud", &hours); finishLine(file);
    frame.iterations = (hours * 3600) * TICKS_PER_SEC;
    frame.stats.target_itercount = frame.iterations;
    // Spawning per hour
    double ppls_per_minute, default_wait1, default_wait2,
        default_stop_inspace, default_stop, default_stop_important;
    for (size_t i = 0; i < hours; i++)
    {
        fscanf(file, "%lf", &ppls_per_minute);
        frame.spawner.probability_fragments.push_back(ppls_per_minute);
    }

    finishLine(file);
    fscanf(file, "%lf %lf", &default_wait1, &default_wait2);
    finishLine(file);
    fscanf(file, "%lf %lf %lf", &default_stop_inspace, &default_stop, &default_stop_important);
    finishLine(file);
    for (size_t i = 0; i < 3; i++)
    {
        double tmp;
        fscanf(file, "%lf", &tmp);
        frame.spawner.ppl_status_probs.push_back(tmp);
    }
    finishLine(file);

    frame.frame_num = 0;
    unsigned distancing, len_x, len_y;
    fscanf(file, "%ud", &distancing); finishLine(file);
    frame.distancing = distancing;
    fscanf(file, "%ud", &len_x); finishLine(file);
    fscanf(file, "%ud", &len_y); finishLine(file);

    std::vector<std::vector<cell_t>> tmp = std::vector<std::vector<cell_t>>(len_x);
    frame.frames.push_back(tmp);
    auto& board = frame.frames[0];
    
    for (size_t row = 0; row < len_y; row++)
    {
        for (size_t col = 0; col < len_x; col++)
        {
            board[col].push_back({ });
            auto& celldata = board[col][row];
            celldata.distrib = std::lognormal_distribution<double>(default_wait1, default_wait2);
            celldata.inhabitant = nullptr;
            celldata.popularity = 0; // Loaded later
            int celltype = fgetc(file);
            if (celltype == ' ' || celltype == '*' || celltype == '.')
            {
                celldata.entity_behaviors.push_back(spreadable);
                celldata.entity_behaviors.push_back(pausable);
                celldata.entity_behaviors.push_back(walkable);

            }
            else if (celltype == 'I')
            {
                celldata.entity_behaviors.push_back(spawnable);
                celldata.entity_behaviors.push_back(walkable);
            }
            else if (celltype == 'O')
            {
                celldata.entity_behaviors.push_back(despawnable);
                celldata.entity_behaviors.push_back(walkable);
            }
            else if (celltype == '|')
            {
                celldata.entity_behaviors.push_back(spreadable);
            }

            if (celltype == '.') celldata.popularity = default_stop;
            if (celltype == '*') celldata.popularity = default_stop_important;
            if (celltype == ' ') celldata.popularity = default_stop_inspace;
        }

        finishLine(file);
    }

    // Create more frames
    for (size_t i = 1; i < FRAME_BUFFER_LENGTH; i++)
    {
        frame.frames.push_back(frame.frames[0]);
    }
    

    return EXIT_SUCCESS;
}
