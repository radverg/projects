/**
 * @file xmlloader.cpp
 * @author xvever13 (Radek Veverka)
 * @author xsedmi04 (Adam Sedmík)
 * @date May 2020
 *
 * ICP Project, VUT FIT
 *
 * Contains implementation of the XMLLoader class
 */

#include "xmlloader.h"
#include <QFile>

XMLLoader::XMLLoader(std::string filename)
{
    // Open file with filename given as parameter
    QFile file(QString::fromStdString(filename));
    if (!file.open(QIODevice::ReadOnly))
    {
        throw std::runtime_error(std::string("Could not open XML file: ") + filename);
    }

    // Parse content of the loaded file to QDomDocument
    if (!parsed_xml.setContent(&file))
    {
        throw std::runtime_error("Invalid XML format.");
    }
}

XMLLoader::XMLLoader(QNetworkReply *data)
{
    if (!parsed_xml.setContent(data))
    {
        throw std::runtime_error("Invalid XML format.");
    }
}
