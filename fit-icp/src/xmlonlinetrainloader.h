/**
 * @file xmlonlinetrainloader.h
 * @author xsedmi04 (Adam Sedmík)
 * @date May 2020
 *
 * ICP Project, VUT FIT
 *
 * Contains declaration of the XMLOnlineTrainLoader class
 */

#ifndef XMLONLINETRAINLOADER_H
#define XMLONLINETRAINLOADER_H

#include "xmlloader.h"
#include "vehicle.h"

/**
 * @brief The XMLOnlineTrainLoader class. This XML loader is used
 * to parse XML structure received from internet resource https://www.miamidade.gov/transit/WebServices/MoverTrains/
 * It produces a vector of vehicles with their coordinates and additional information.
 */
class XMLOnlineTrainLoader : public XMLLoader
{
    using XMLLoader::XMLLoader; // Use base constructor

public:
    /**
     * @brief Main parsing function. Parses loaded XML document into array of Vehicle objects.
     * @return Vector of parsed Vehicle objects.
     */
    std::vector<Vehicle> load_vehicles();

private:
};
#endif // XMLONLINETRAINLOADER_H
