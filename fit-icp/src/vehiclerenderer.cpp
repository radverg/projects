/**
 * @file vehiclerenderer.cpp
 * @author xsedmi04 (Adam Sedmík)
 * @date May 2020
 *
 * ICP Project, VUT FIT
 *
 * Contains implementation of the VehicleRenderer class
 */

#include "vehiclerenderer.h"

VehicleRenderer::VehicleRenderer(QGraphicsScene* sc)
    : sc{sc}
{

}

VehicleRenderer::~VehicleRenderer()
{
    for (const auto& vc : vehicles)
    {
        sc->removeItem(vc);
        delete vc;
    }

    vehicles.clear();
}

void VehicleRenderer::render_vehicles(std::vector<Vehicle> ongoing)
{
    // Remove current vehicles from scene
    for (const auto& vc : vehicles)
    {
        sc->removeItem(vc);
        // Don't forget to free them
        delete vc;
    }

    vehicles.clear();

    // Render vehicles sent
    for (const auto& vehicle : ongoing)
    {
        // Make Vehicle int QGraphics Item
        QgraphicsCustomVehicleItem *vc;

        // Construct based if we have Link available
        if (vehicle.get_link() == nullptr)
            vc = new QgraphicsCustomVehicleItem(vehicle.get_info(), vehicle.get_location().x(), vehicle.get_location().y());
        else
            vc = new QgraphicsCustomVehicleItem(vehicle.get_link(), vehicle.get_location().x(), vehicle.get_location().y());


        // Set vehicle color and add it to scene
        vc->setBrush(QBrush(Qt::green));
        sc->addItem(vc);

        // Store rendered vehicles
        vehicles.push_back(vc);
    }
}

