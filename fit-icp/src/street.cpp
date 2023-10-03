/**
 * @file street.cpp
 * @author xvever13 (Radek Veverka)
 * @author xsysmi00 (Michal Sýs)
 * @author xsedmi04 (Adam Sedmík)
 * @date May 2020
 *
 * ICP Project, VUT FIT
 *
 * Includes implementation of the Street class
 */

#include <QtMath>
#include "street.h"
#include "utils.h"

Street::Street(QPoint start, QPoint end, std::string name)
    : name{name},
      start{start},
      end{end},
      traff_density{1}

{
  // Nothing else to initialize here
}

qreal Street::get_slope() const
{
    float arctan= atan(float((this->get_end().ry()-this->get_start().ry()))/float((this->get_end().rx()-this->get_start().rx())));
    qreal slope = qRadiansToDegrees(arctan);
    return slope;
}

bool Street::operator==(const Street& rhs)
{
    return name == rhs.name;
}

void Street::reverse()
{
    std::swap(start, end);
}

QPoint Street::get_common_point(const Street& other)
{
    if (start == other.start || start == other.end)
        return start;
    if (end == other.start || end == other.end)
        return end;

    throw std::runtime_error("No common points for two streets.");
}


bool Street::has_common_point(const Street &other) const
{
    if (get_start() == other.get_start() || get_end() == other.get_end() ||
        get_end() == other.get_start()   || get_start() == other.get_end() ) return true;
    return false;
}

bool Street::has_endpont(QPoint pt) const
{
    return start == pt || end == pt;
}

float Street::length() const
{
   return Utils::Math::qpoint_distance(start, end);
}


float Street::dist_to_start(const QPoint& from) const
{
    return Utils::Math::qpoint_distance(start, from);
}

float Street::dist_to_end(const QPoint& from) const
{
    return Utils::Math::qpoint_distance(end, from);
}

QPoint Street::get_middle() const
{
    QPoint* middle = new QPoint;
    middle->setX((this->get_end().rx()+this->get_start().rx())/2);
    middle->setY((this->get_end().ry()+this->get_start().ry())/2);
    return *middle;
}

bool Street::is_closed() const
{
    if (this->detour.empty()) return false;
    return true;
}

void Street::clear_detour()
{
    detour.clear();
}
