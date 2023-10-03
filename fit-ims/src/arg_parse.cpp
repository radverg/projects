/**
 * IMS - projekt
 * prosinec 2020
 * Lukáš Wagner (xwagne10), Radek Veverka (xvever13)
 */

#include "arg_parse.hpp"
#include <iostream>
#include <getopt.h>
#include <sstream>

int arg_parse(int argc, char* argv[], settings& sett)
{
    using namespace std;

    const char* const short_opt = "s:m:f:h";
    const option long_opt[] = {
        {"file",required_argument,nullptr,'f'},
        {"stats",required_argument,nullptr,'s'},
        {"map",required_argument,nullptr,'m'},
        {"help",no_argument,nullptr,'h'},
    };

    istringstream ss; // for integer conversion with checking

    int c;
    while((c = getopt_long(argc, argv, short_opt,long_opt,nullptr)) != -1)
    {
        switch (c)
        {
            case 'f':
                sett.filename = optarg;
                break;
            case 's':
                ss = istringstream(optarg);
                if(!(ss >> sett.stats_every)){
                    cerr<<"Wrong number of iteration on stats";
                    return ERRARG;
                }
                else if(!ss.eof()){
                    cerr<<"Wrong number of iteration on stats";
                    return ERRARG;
                }
                break;
            case 'm':
                ss = istringstream(optarg);
                if(!(ss >> sett.maps_every)){
                    cerr<<"Wrong number of iteration on maps";
                    return ERRARG;
                }
                else if(!ss.eof()){
                    cerr<<"Wrong number of iteration on maps";
                    return ERRARG;
                }
                break;
            case 'h':
                printHelp();
                return HELPARG;
                break;
            default:
                break;
        }
    }

    if(sett.filename == nullptr){
        cerr<<"Missing required filename\n\n";
        printHelp();
        return ERRARG;
    }

    return 0;
}

void printHelp()
{
    using namespace std;
    cerr<< "Usage:"
        << "-f, --file=filename\n\t Filename of input file\n"
        << "-m, --maps=num\n\t Number of iterations between live map prints to stdcerr (console printing slows program)\n"
        << "-s, --stats=num\n\t Number of iterations between statistics prints to stdcerr (console printing slows program)\n";             
}