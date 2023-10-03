/**
 * IMS - projekt
 * prosinec 2020
 * Lukáš Wagner (xwagne10), Radek Veverka (xvever13)
 */

#pragma once

#define ERRARG 1
#define HELPARG 2

struct settings{
    const char* filename = nullptr;
    unsigned long long stats_every = 0;
    unsigned long long maps_every = 0;
};

int arg_parse(int argc, char* argv[], settings& sett);
void printHelp();