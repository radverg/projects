/**
 * @file qgraphicscustomvehicleitem.h
 * @author xsedmi04 (Adam Sedmík)
 * @date May 2020
 *
 * ICP Project, VUT FIT
 *
 * Declaration of the QGraphicsCustomVehicle class
 */

#ifndef QGRAPHICSCUSTOMVEHICLEITEM_H
#define QGRAPHICSCUSTOMVEHICLEITEM_H

#include <QGraphicsEllipseItem>
#include "link.h"

/**
 * @brief The QgraphicsCustomVehicleItem class. A Subclass of QGraphicsEllipseItem that can store additional information.
 *
 * This class is QGraphicsItem implementation of Vehicle that can be rendered on scene as an Ellipse.
 * Stores the same additional information as Vehicle.
 *
 * @see Vehicle
 * @see VehicleRenderer
 */
class QgraphicsCustomVehicleItem : public QGraphicsEllipseItem
{
private:
    /**
     * @brief Pointer to the link graphics vehicle belongs to.
     *
     * Used to display information when vehicle is clicked on.
     * Can be nullptr.
     */
    const Link* link;
    /**
     * @brief Additional information about graphics vehicle.
     *
     * Strings are ready to be displayed when clicked on.
     * Can be empty.
     */
    std::vector<std::string> info;

public:
    /**
     * @brief Constructs a graphics vehicle with Link information.
     *
     * Used for displaying info about a vehicle in simulation.
     * Info is empty.
     * @param link Pointer to the link vehicle belongs to.
     * @param x X coordinate for creating ellipse.
     * @param y Y coordinate for creating ellipse.
     */
    QgraphicsCustomVehicleItem(const Link* link, qreal x, qreal y);
    /**
     * @brief Costructs a graphics vehicle with string information.
     *
     * Used for displaying infomation in real controller.
     * Link is set to nullptr.
     * @param info Already complete vector of information
     * @param x X coordinate for creating ellipse.
     * @param y Y coordinate for creating ellipse.
     */
    QgraphicsCustomVehicleItem(std::vector<std::string> info, qreal x, qreal y);
    /**
     * @brief Getter for Link of graphics vehicle.
     * @return Link of the vehicle belongs to.
     */
    const Link* get_link() const { return link; };
    /**
     * @brief Getter for information about graphics vehicle.
     * @return Vector of string information about vehicle.
     */
    std::vector<std::string> get_info() const {return info;};
};

#endif // QGRAPHICSCUSTOMVEHICLEITEM_H
