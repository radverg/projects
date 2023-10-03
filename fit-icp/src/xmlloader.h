/**
 * @file xmlloader.h
 * @author xvever13 (Radek Veverka)
 * @author xsedmi04 (Adam Sedmík)
 * @date May 2020
 *
 * ICP Project, VUT FIT
 *
 * Contains declaration of the XMLLoader class
 */

#ifndef XMLLOADER_H
#define XMLLOADER_H

#include <iostream>
#include <QDomDocument>
#include <QNetworkReply>

/**
 * @brief The XMLLoader class. Base class for all xml loaders.
 *
 * Performs simple checks eg. whether file can be opened or xml can be parsed.
 * @throws runtime_error if something fails.
 */
class XMLLoader
{
protected:
    /**
     * @brief Parsed QDomDocument xml dom representation.
     */
    QDomDocument parsed_xml;

public:
    /**
     * @brief Attempts to load given file with xml and parse it into parsed_xml.
     * @param filename Path to file to be loaded.
     */
    XMLLoader(std::string filename);

    /**
     * @brief Attempts to load given network resource with xml and parse it into parsed_xml.
     * @param data Data from HTTP response.
     */
    XMLLoader(QNetworkReply *data);
};

#endif // XMLLOADER_H
