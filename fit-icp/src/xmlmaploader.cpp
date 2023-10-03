/**
 * @file xmlmaploader.cpp
 * @author xvever13 (Radek Veverka)
 * @date May 2020
 *
 * ICP Project, VUT FIT
 *
 * Contains implementation of the XMLMapLoader class
 */

#include <QString>
#include <QPoint>
#include "xmlmaploader.h"
#include "stop.h"

StreetMap* XMLMapLoader::load_map()
{
    // Allocate new map object
    StreetMap* result_map = new StreetMap();

    // Iterate through streets, initialize them and append to the map
    QDomElement root = parsed_xml.documentElement();
    QDomNodeList xml_streets = root.childNodes();
    for (int i = 0; i < xml_streets.length(); i++)
    {
        QDomNode xml_street     = xml_streets.item(i);
        // Append the street to resulting map.
        Street strt = load_street(xml_street);
        result_map->add_street(strt);

        QDomNodeList xml_stops = xml_street.childNodes();
        for (int j = 0; j < xml_stops.length(); j++)
        {
            QDomNode xml_stop = xml_stops.item(j);
            // Append the stop to resulting street.
            result_map->add_stop(load_stop(xml_stop, strt));
        }
    }

    return result_map;
}

Street XMLMapLoader::load_street(const QDomNode& street_node)
{
    QDomNamedNodeMap attrs = street_node.attributes();

    // Create street with QPoints and names parsed from xml document
    Street strt(
       QPoint(attrs.namedItem("x1").nodeValue().toInt(), attrs.namedItem("y1").nodeValue().toInt()),
       QPoint(attrs.namedItem("x2").nodeValue().toInt(), attrs.namedItem("y2").nodeValue().toInt()),
       attrs.namedItem("name").nodeValue().toStdString()
    );

    return strt;
}

Stop XMLMapLoader::load_stop(const QDomNode& stop_node, const Street& street)
{
    QDomNamedNodeMap attrs = stop_node.attributes();
    Stop stop(
        attrs.namedItem("name").nodeValue().toStdString(),
        attrs.namedItem("position").nodeValue().toFloat(),
        street
    );

    return stop;
}
