/**
 * @file qgraphicscustomvehicleitem.cpp
 * @author xsedmi04 (Adam Sedmík)
 * @date May 2020
 *
 * ICP Project, VUT FIT
 *
 * Implementation of the QGraphicsCustomVehicle class
 */

#include "qgraphicscustomvehicleitem.h"

QgraphicsCustomVehicleItem::QgraphicsCustomVehicleItem(const Link* link, qreal x, qreal y)
    : QGraphicsEllipseItem(x-5, y-5, 10, 10),
      link{link}
{
    // Selectable and on top
    setFlag(ItemIsSelectable);
    setZValue(1);
}

QgraphicsCustomVehicleItem::QgraphicsCustomVehicleItem(std::vector<std::string> info, qreal x, qreal y)
    : QGraphicsEllipseItem(x-5, y-5, 10, 10),
      link{nullptr},
      info{info}
{
    // Selectable and on top
    setFlag(ItemIsSelectable);
    setZValue(1);
}
