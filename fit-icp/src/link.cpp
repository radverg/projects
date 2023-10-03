/**
 * @file link.cpp
 * @author xvever13 (Radek Veverka)
 * @author xsedmi04 (Adam Sedmík)
 * @date May 2020
 *
 * ICP Project, VUT FIT
 *
 * Implementation of the Link class
 */

#include "link.h"
#include "utils.h"
#include "math.h"

Link::Link(Route* const route, const QTime start , const QTime end)
    : route{route},
      start{start},
      end{end},
      active{false},
      distance{0}
{
    int total_time      = Utils::Time::seconds_between_times(start, end);
    default_speed       = route->get_total_distance() / total_time;
    original_stop_times = compute_stop_times();
}

void Link::reset_distance(QTime new_time)
{
    int total_time = Utils::Time::seconds_between_times(start, new_time);
    distance = total_time * get_default_speed();

    if (distance > route->get_total_distance())
        throw std::runtime_error(std::string("Distance is greater than total distance: "));
}

void Link::set_distance(float dist)
{
    if (dist > route->get_total_distance())
        distance = route->get_total_distance();
    else
        distance = dist;
}

bool Link::is_on_map_by_time(const QTime& time) const
{
    QTime realend = start.addSecs( floor(route->get_total_distance() / default_speed) );
    return Utils::Time::is_time_between_times(start, realend, time);
}

std::map<std::string,QTime> Link::compute_stop_times() const
{
    std::map<std::string,QTime> out;
    float speed = default_speed;

    route->traversee([this, &out, &speed] (stop_citerator stop, street_citerator, float dist) {
        out[stop->get_name()] = get_start().addSecs( dist / speed );
    });

    return out;
}

std::string Link::get_current_street_name() const
{
    return route->traverse_distance(route->get_stops().begin(), distance)->get_name();
}
