/**
 * @file xmlstopsloader.h
 * @author xsedmi04 (Adam Sedmík)
 * @date May 2020
 *
 * ICP Project, VUT FIT
 *
 * Contains declaration of the XMLStopsLoader class
 */

#ifndef XMLSTOPSLOADER_H
#define XMLSTOPSLOADER_H

#include "xmlloader.h"
#include "stop.h"

/**
 * @brief The XMLStopsLoader class. Is used by extension to load simple list of stops and their real world coordinates.
 *
 * Parser produces map of stops, where stop id is the string key in the map.
 */
class XMLStopsLoader : public XMLLoader
{
    using XMLLoader::XMLLoader; // Get base constructors

public:
    /**
     * @brief Main parsing method. Processes XML document and extracts information about stops.
     * @return Map of parsed stops, with string key (stop ID).
     */
    std::map<std::string, Stop> load_stops();

private:
};

#endif // XMLSTOPSLOADER_H
