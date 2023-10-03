/**
 * @file xmlonlinetrainloader.cpp
 * @author xsedmi04 (Adam Sedmík)
 * @date May 2020
 *
 * ICP Project, VUT FIT
 *
 * Contains implementation of the XMLOnlineTrainLoader class
 */

#include <QTime>
#include <QString>
#include <QPoint>
#include "xmlonlinetrainloader.h"
#include "utils.h"
#include "vehicle.h"

std::vector<Vehicle> XMLOnlineTrainLoader::load_vehicles()
{
    std::vector<Vehicle> result_trains;
    QDomElement root = parsed_xml.documentElement();
    QDomNodeList xml_records= root.childNodes();
    for (int i = 0; i < xml_records.length(); i++)
    {
        QDomNodeList xml_record = xml_records.item(i).childNodes();
        float lati = 0;
        float longi = 0;
        std::vector<std::string> info;

        for (int j = 0; j < xml_record.length(); j++)
        {
            // Extract information
            QDomNode xml_record_node = xml_record.item(j);
            if (xml_record_node.nodeName() == "Latitude")
                lati = xml_record_node.firstChild().nodeValue().toFloat();
            if (xml_record_node.nodeName() == "Longitude")
                longi = xml_record_node.firstChild().nodeValue().toFloat();
            if (xml_record_node.nodeName() == "LocationUpdated")
            {
                if (QTime::fromString(QString(xml_record_node.firstChild().nodeValue()), "h:mm:ss AP").toString().toStdString() == "") //QT version ?
                    info.push_back("Location Updated: " + xml_record_node.firstChild().nodeValue().toStdString());
                else
                    info.push_back("Location Updated: " + QTime::fromString(QString(xml_record_node.firstChild().nodeValue()), "h:mm:ss AP").toString().toStdString());
            }
            if (xml_record_node.nodeName() == "LoopName")
                info.push_back(xml_record_node.firstChild().nodeValue().toStdString() + " Loop");
            if (xml_record_node.nodeName() == "TrainID")
                info.push_back("Train ID: " + xml_record_node.firstChild().nodeValue().toStdString());
        }

        // If didn't change don't load as vehicle
        if (lati == 0 || longi == 0)
            continue;

        // Calculate location to point
        QPoint loc = Utils::Math::real_to_coord(lati, longi);
        Vehicle vc = Vehicle(loc, info);

        result_trains.push_back(vc);
    }

    return result_trains;
}
