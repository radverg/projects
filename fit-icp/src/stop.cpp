/**
 * @file stop.cpp
 * @author xvever13 (Radek Veverka)
 * @author xsedmi04 (Adam Sedmík)
 * @date May 2020
 *
 * ICP Project, VUT FIT
 *
 * Contains implementation of the Stop class
 */

#include "stop.h"
#include "utils.h"
#include "street.h"

Stop::Stop(std::string name, float position, const Street& street)
    : name{name},
      position{position},
      street_name{street.get_name()}
{
    // Compute real coordinates from relative position on the street
    real_coordinates = Utils::Math::position_on_line(street.get_start(), street.get_end(), position);
}

Stop::Stop(std::string name, QPoint real_coordinates)
    : name{name},
      position{0},
      real_coordinates{real_coordinates},
      street_name{name}
{
    // This is stop not related to any street. No additional initialization is necessary.
}
