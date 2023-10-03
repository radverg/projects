/**
 * @file xmlstopsloader.cpp
 * @author xsedmi04 (Adam Sedmík)
 * @date May 2020
 *
 * ICP Project, VUT FIT
 *
 * Contains implementation of the XMLStopsLoader class
 */

#include <QString>
#include <QPoint>
#include "xmlstopsloader.h"
#include "utils.h"

std::map<std::string, Stop> XMLStopsLoader::load_stops()
{
    std::map<std::string, Stop> result_stops;
    QDomElement root = parsed_xml.documentElement();
    QDomNodeList xml_stops = root.childNodes();
    for (int i = 0; i < xml_stops.length(); i++)
    {
        QDomNode xml_stop = xml_stops.item(i);
        QDomNamedNodeMap attrs = xml_stop.attributes();
        QPoint converted = Utils::Math::real_to_coord(attrs.namedItem("latitude").nodeValue().toFloat(), attrs.namedItem("longitude").nodeValue().toFloat());
        Stop tmp = Stop(attrs.namedItem("name").nodeValue().toStdString(), converted);
        result_stops.insert({attrs.namedItem("name").nodeValue().toStdString(), tmp});
    }

    return result_stops;
}
