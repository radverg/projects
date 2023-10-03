/**
 * @file stop.h
 * @author xvever13 (Radek Veverka)
 * @author xsedmi04 (Adam Sedmík)
 * @date May 2020
 *
 * ICP Project, VUT FIT
 *
 * Contains declaration of the Stop class
 */
#ifndef STOP_H
#define STOP_H
#include <iostream>
#include <QPoint>

class Street; // Need to declare this to avoid cycled includes

/**
 * @brief The Stop class. Holds basic information about a stop - name and position.
 *
 * As well as Street, this class is more like a "primitive" data type.
 * It is not meant to be allocated dynamically.
 * Basically it represents a simple point with extra information like name, street name and relative position.
 * Since this is simple type, it does not contain any direct link to the related street, but stores ID of the related street instead.
 * This approach offers more flexibility when working with Streets and Stops.
 */
class Stop
{
private:
    /**
     * @brief Name of the stop.
     *
     * This also serves as a unique identifier for the stop.
     */
    std::string name;
    /**
     * @brief Position of the stop on given street.
     *
     * Range from 0 to 1, determines how far this stop is from the beginning of the street.
     */
    float position;
    /**
     * @brief Real coordinates of the stop.
     *
     * They are computed at construction time from position of the street and relative position of the stop to the street.
     */
    QPoint real_coordinates;
    /**
     * @brief Identifier of the street that this stop belongs to.
     *
     * To keep stop as a simple type, we do not store a link to the related street, but just the unique name of the street.
     */
    std::string street_name;

public:
    /**
     * @brief Constructs a stop that belongs to the given street.
     *
     * Real coordinates are computed automatically from given relative position and street.
     * Name of the street is stored as well.
     * @param name Name (identifier) of this stop.
     * @param position Relative position of this stop on the street.
     * @param street Street this stop is located at.
     */
    Stop(std::string name, float position, const Street& street);

    /**
     * @brief Constructs a stop that does not belong to any street.
     *
     * This is useful in real module, where we do not know and load any information about the streets.
     * @param name Name (identifier) of this stop
     * @param real_coordinates Real coordinates of the stop. They are not computed from a street but stored directly.
     */
    Stop(std::string name, QPoint real_coordinates);

    /**
     * @brief Getter for real coordinates of this stop.
     * @return Coordinates of this stop (in pixels).
     */
    QPoint get_coords() const { return real_coordinates; };

    /**
     * @brief Getter for the unique name of this stop.
     * @return Name of this stop.
     */
    std::string get_name() const { return name; };

    /**
     * @brief Getter for a name of related street.
     * @return Name of the street this stop is located at.
     */
    std::string get_street_name() const { return street_name; };
};

#endif // STOP_H
