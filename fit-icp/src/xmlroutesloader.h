/**
 * @file xmlroutesloader.h
 * @author xvever13 (Radek Veverka)
 * @date May 2020
 *
 * ICP Project, VUT FIT
 *
 * Contains declaration of the XMLRoutesLoader class
 */

#ifndef XMLROUTESLOADER_H
#define XMLROUTESLOADER_H

#include <vector>
#include "xmlloader.h"
#include "link.h"
#include "streetmap.h"

/**
 * @brief The RoutesLinks struct. For returning two values from the loader.
 */
struct RoutesLinks
{
    std::vector<Link*> links;   /**< Vector of loaded and dynamically allocated links */
    std::vector<Route*> routes; /**< Vector of loaded and dynamically allocated routes */
};

/**
 * @brief The XMLRoutesLoader class. Used to load xml representation of Routes and their schedule (Links).
 *
 * Dynamically allocates Routes and Links parsed from XML.
 * Also uses Routebuilder and correctly initializes all created objects.
 */
class XMLRoutesLoader : public XMLLoader
{
    using XMLLoader::XMLLoader; // Use base constructor.

public:
    /**
     * @brief The main parsing method that takes care of the whole parsing process.
     * @param street_map A map of Streets and Stops
     * @return Newly allocated Route/Link objects, vectors in resulting struct contain their pointers.
     */
    RoutesLinks load_routes_links(StreetMap& street_map);

};

#endif // XMLROUTESLOADER_H
