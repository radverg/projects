/**
 * @file xmlmaploader.h
 * @author xvever13 (Radek Veverka)
 * @date May 2020
 *
 * ICP Project, VUT FIT
 *
 * Contains declaration of the XMLMapLoader class
 */

#ifndef XMLMAPLOADER_H
#define XMLMAPLOADER_H

#include "xmlloader.h"
#include "streetmap.h"
#include "stop.h"

/**
 * @brief The XMLMapLoader class.
 * Is used for loading a map and creating StreetMap object.
 */
class XMLMapLoader : public XMLLoader
{
    using XMLLoader::XMLLoader; // Include base constructor, we do not need to redefine it

private:
    /**
     * @brief Helper function that processes given xml street node and parses it to Street object.
     * @param street_node XML node to process.
     * @return New Street object.
     */
    Street load_street(const QDomNode& street_node);

    /**
     * @brief Helper function that processes given xml street node and parses it to Street object.
     * @param stop_node XML node to oprocess.
     * @param street Street that this stop is located at.
     * @return New Stop object.
     */
    Stop load_stop(const QDomNode& stop_node, const Street& street);

public:
    /**
     * @brief Main xml map parsing method.
     *
     * Parses XML document and returns new allocated StreetMap object, with everything initialized and ready.
     * @return New StreetMap object, dynamically allocated.
     */
    StreetMap* load_map();

};

#endif // XMLMAPLOADER_H
