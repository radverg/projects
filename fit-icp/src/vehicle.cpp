/**
 * @file vehicle.cpp
 * @author xsedmi04 (Adam Sedmík)
 * @date May 2020
 *
 * ICP Project, VUT FIT
 *
 * Contains implementation of the Vehicle class
 */

#include "vehicle.h"

Vehicle::Vehicle(QPoint location, const Link* link)
    : location{location},
      link{link}
{

}

Vehicle::Vehicle(QPoint location, std::vector<std::string> info)
    : location{location},
      link{nullptr},
      info{info}
{

}

