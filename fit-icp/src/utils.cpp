/**
 * @file utils.cpp
 * @author xvever13 (Radek Veverka)
 * @author xsysmi00 (Michal Sýs)
 * @date May 2020
 *
 * ICP Project, VUT FIT
 *
 * Contains implementation of utility functions.
 */

#include "utils.h"
#include "math.h"

QPoint Utils::Math::position_on_line(QPoint linestart, QPoint lineend, float factor)
{
    return linestart + ((lineend - linestart) * factor);
}

float Utils::Math::qpoint_distance(const QPoint& first, const QPoint& second)
{
    QPoint sub = first - second;
    return sqrtf(sub.x() * sub.x() + sub.y() * sub.y());
}


QPoint Utils::Math::real_to_coord(float latitude, float longitude)
{
    float real_x;
    float xConstant = 0.024526;  // this is the greatest difference in longitude two points on map can make
    float xZero = -80.202628;    // this is the starting longitude for our part of map
    float real_x_max = 576.0;    // width of picture in pixels
    real_x = ((longitude - xZero)/xConstant)*real_x_max; // solved using the Rule of Three

    float real_y;
    float yConstant = 0.032096;  // this is the greatest difference in latitude two points on map can make
    float yZero = 25.791307;     // this is the starting latitude for our part of map
    float real_y_max = 830.0;    // height of picture in pixels
    real_y = ((yZero - latitude)/yConstant)*real_y_max; // solved using the Rule of Three

    QPoint real_coord; // points will be converted to int, might not be the best accuracy possible, but it works well
    real_coord.setX(real_x);
    real_coord.setY(real_y);

    return real_coord;
}

int Utils::Time::seconds_between_times(const QTime& starttime, const QTime& endtime)
{
    int secs = starttime.secsTo(endtime);
    return (secs < 0) ? 86400 + secs : secs;
}

bool Utils::Time::is_time_between_times(const QTime& starttime, const QTime& endtime, const QTime& midtime)
{
    if (starttime <= endtime)
    {
        return midtime >= starttime && midtime <= endtime;
    }
    else
    {
        return (midtime >= starttime && midtime <= QTime(23, 59, 59) ) || (midtime <= endtime && midtime >= QTime(0,0,0) );
    }

    return false;
}
