/**
 * @file vehicle.h
 * @author xsedmi04 (Adam Sedmík)
 * @date May 2020
 *
 * ICP Project, VUT FIT
 *
 * Contains declaration of the Vehicle class
 */

#ifndef VEHICLE_H
#define VEHICLE_H

#include <QPoint>
#include "link.h"

/**
 * @brief The Vehicle class. Holds information about ongoing vehicles.
 *
 * Simple class only to be constructed and send elsewhere.
 * This class is used to deliver information about vehicle from simulator to renderer.
 * Vehicle is represented as a simple QPoint for it's location.
 * Vehicle can also hold information about itself in Link or string form.
 *
 * @see Link
 * */
class Vehicle
{
private:
    /**
     * @brief Coordinates of vehicle.
     */
    QPoint location;
    /**
     * @brief Pointer to the link the vehicle belongs to.
     *
     * Can be nullptr.
     */
    const Link* link;
    /**
     * @brief Additional information about vehicle.
     *
     * Strings are ready to be printed.
     * Can be empty.
     */
    std::vector<std::string> info;

public:
    /**
     * @brief Constructs a vehicle with Link information.
     *
     * Used for displaying info about vehicle in simulation.
     * Info is empty.
     * @param location Coordinates of vehicle, should be calculated before initialization.
     * @param link Pointer to the link the vehicle belongs to.
     */
    Vehicle(QPoint location, const Link* link);
    /**
     * @brief Costructs a vehicle with string information.
     *
     * Used for displaying information in real controller.
     * Link is set to nullptr.
     * @param location Coordinates of vehicle, should be calculated before initialization.
     * @param info Already complete vector of information
     */
    Vehicle(QPoint location, std::vector<std::string> info);
    /**
     * @brief Getter for location of vehicle.
     * @return Coordinates in QPoint location of vehicle.
     */
    QPoint get_location() const {return location; };
    /**
     * @brief Getter for Link of vehicle.
     * @return Link of the vehicle belongs to.
     */
    const Link* get_link() const {return link; };
    /**
     * @brief Getter for information about vehicle.
     * @return Vector of string information about vehicle.
     */
    std::vector<std::string> get_info() const {return info;};
};

#endif // VEHICLE_H
