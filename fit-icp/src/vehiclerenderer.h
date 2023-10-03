/**
 * @file vehiclerenderer.h
 * @author xsedmi04 (Adam Sedmík)
 * @date May 2020
 *
 * ICP Project, VUT FIT
 *
 * Contains declaration of the VehicleRenderer class
 */

#ifndef VEHICLERENDERER_H
#define VEHICLERENDERER_H

#include <QGraphicsScene>
#include "vehicle.h"
#include "qgraphicscustomvehicleitem.h"

/**
 * @brief The VehicleRenderer class. Renders vehicles on scene.
 *
 * @see Vehicle
 * @see QGraphicsCustomVehicleItem
 */
class VehicleRenderer
{
private:
    /**
     * @brief Scene where vehicles will be rendered.
     */
    QGraphicsScene* sc;
    /**
     * @brief Stores vehicles currently rendered.
     *
     * So we can remove them from scene later,
     * and also free them.
     */
    std::vector<QgraphicsCustomVehicleItem*> vehicles;

public:
    /**
     * @brief Constructs VehicleRenderer.
     * @param sc Scene where vehicles will be rendered.
     */
    VehicleRenderer(QGraphicsScene* sc);
    /**
     * @brief Destructor for VehicleRenderer.
     *
     * Frees all items in vehicles vector.
     */
    ~VehicleRenderer();
    /**
     * @brief Renders given vehicles on a scene.
     *
     * Given vehicles to render, removes currently rendered vehicles and renders vehicles given.
     * During rendering, simple Vehicle class is converted to QGraphicsCustomVehicleItem.
     * @param ongoing Vector of vehicles to be rendered.
     */
    void render_vehicles(std::vector<Vehicle> ongoing);
};


#endif // VEHICLERENDERER_H
