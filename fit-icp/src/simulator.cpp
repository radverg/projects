/**
 * @file simulator.cpp
 * @author xsedmi04 (Adam Sedmík)
 * @author xvever13 (Radek Veverka)
 * @date May 2020
 *
 * ICP Project, VUT FIT
 *
 * Contains implementation of the Simulator class
 */

#include "simulator.h"
#include "utils.h"

Simulator::Simulator(StreetMap* street_map, std::vector<Link*> links)
    : currrent_time(QTime()),
      speed_coefficient{1},
      street_map{street_map},
      links{links}

{

}

void Simulator::update(int delta_milisec)
{
    // Calculate current time
    currrent_time = currrent_time.addSecs((delta_milisec / 1000) * speed_coefficient);

    for(const auto link : links)
    {
        if (link->get_activity())
        {
            // This link is active, ok, just move it forward
            double density = street_map->get_streets().at(link->get_current_street_name()).get_density();
            // Distance is calulated - current distance + speed * speed coefficent (timer skip) and is slowed by density
            float dist = link->get_distance() + ((link->get_default_speed() * speed_coefficient) / density);
            link->set_distance(dist);

            // Did we just reach end of route?
            if (link->get_route()->get_total_distance() < dist)
            {
                // We did, so set this link to be inactive
                link->set_activity(false);
            }
        }
        else
        {
            // This link is not active, but maybe it is just about to start
            if (link->is_on_map_by_time(currrent_time))
            {
                link->set_activity(true);
                link->reset_distance(currrent_time);
            }
        }
    }
}

void Simulator::reset(QTime to_time)
{
    currrent_time = to_time;
    for(const auto link : this->links)
    {
        // Default is inactive
        link->set_activity(false);

        // Is it time for link to be on map
        if (link->is_on_map_by_time(to_time))
        {
            link->set_activity(true);
            link->reset_distance(to_time);
        }
    }
}

std::vector<Vehicle> Simulator::get_ongoing()
{
    // Return vector
    std::vector<Vehicle> ongoing;
    for(const auto link : links)
    {
        if (link->get_activity())
        {
            // If link is currently active calculate its coordinates and append to return vector
            float dist = link->get_distance();
            Vehicle vc = Vehicle(link->get_route()->distance_to_coordinates(dist), link);
            ongoing.push_back(vc);
        }
    }

    return ongoing;
}

void Simulator::reset_streets()
{
    // Try to reset routes for all links
    for(const auto& link : links)
    {
        link->get_route()->reset_route();
    }

    // Call street map to reset it's streets detours
    street_map->reset_streets_detour();
}
