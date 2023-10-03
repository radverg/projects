/**
 * @file xmlroutesloader.cpp
 * @author xvever13 (Radek Veverka)
 * @date May 2020
 *
 * ICP Project, VUT FIT
 *
 * Contains implementation of the XMLRoutesLoader class
 */

#include "xmlroutesloader.h"
#include "routebuilder.h"

RoutesLinks XMLRoutesLoader::load_routes_links(StreetMap& street_map)
{
    QDomNodeList xml_routes     = parsed_xml.elementsByTagName("route");
    QDomNodeList xml_path_nodes = parsed_xml.elementsByTagName("path").at(0).childNodes();

    RoutesLinks output;

    // Create and build route

    for (int i = 0; i < xml_routes.length(); i++)
    {
        QDomNode xml_route = xml_routes.item(i);
        Route* rt = new Route(xml_route.attributes().namedItem("id").nodeValue().toStdString());
        RouteBuilder rt_builder(&street_map, rt);

        QDomNodeList xml_path_nodes = xml_route.firstChildElement("path").childNodes();
        for (int j = 0; j < xml_path_nodes.length(); j++)
        {
            QDomNode xml_path_node = xml_path_nodes.item(j);
            std::string node_value = xml_path_node.firstChild().nodeValue().toStdString();
            if (xml_path_node.nodeName() == "street")
            {
                rt_builder.append_street(street_map.get_streets().at(node_value));
            }
            else
            {
                // Its a stop node
                Stop stop = street_map.get_stops().at(node_value);
                rt_builder.append_stop(street_map.get_stops().at(node_value));
            }
        }

        rt->update_distance();
        rt->update_reversed();

        output.routes.push_back(rt);

        // Route is ready, create links
        QDomNodeList xml_link_nodes = xml_route.firstChildElement("links").childNodes();
        for (int k = 0; k < xml_link_nodes.length(); k++)
        {
            auto link_node_attrs = xml_link_nodes.item(k).attributes();
            bool is_reversed = (link_node_attrs.namedItem("reverse").nodeValue() == "true") ? true : false;
            Link* lnk = new Link(
                (is_reversed) ? rt->get_reversed() : rt,
                QTime::fromString(link_node_attrs.namedItem("start").nodeValue(), "hh:mm"),
                QTime::fromString(link_node_attrs.namedItem("end").nodeValue(), "hh:mm")
            );

            output.links.push_back(lnk);
        }
    }

    return output;
}
