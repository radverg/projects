/**
 * @file route.cpp
 * @author xvever13 (Radek Veverka)
 * @author xsedmi04 (Adam Sedmík)
 * @date May 2020
 *
 * ICP Project, VUT FIT
 *
 * Implementation of the Route class.
 */
#include "route.h"
#include "utils.h"

Route::Route(std::string id)
    : id{id},
      route_reversed{nullptr}
{
    // Nothing else needed here
}

Route::~Route()
{
    // Delete reversed route if it exists.
    if (route_reversed != nullptr)
        delete route_reversed;
}

void Route::update_reversed()
{
    if (route_reversed == nullptr)
        route_reversed = new Route(id);

    route_reversed->stops   = std::vector<Stop>(stops.rbegin(), stops.rend());      // Copy stops, reversed order
    route_reversed->streets = std::list<Street>(streets.rbegin(), streets.rend());  // Copy streets, reversed order
    route_reversed->total_distance = total_distance; // Copy distance
    for (auto& street : route_reversed->streets) // Reverse orientation of all streets
        street.reverse();
}

void Route::update_distance()
{
    total_distance = traversee();
}

QPoint Route::distance_to_coordinates(float dist) const
{
    float out_street_dist; // Here we will have of the point from the start of last street
    auto streetit = traverse_distance(stops.cbegin(), dist, &out_street_dist);
    return Utils::Math::position_on_line(streetit->get_start(), streetit->get_end(), out_street_dist / streetit->length());
}

float Route::traversee()
{
    return traversee([](std::vector<Stop>::const_iterator, std::list<Street>::const_iterator, float){});
}

float Route::traversee(std::function<void(std::vector<Stop>::const_iterator current_stop, std::list<Street>::const_iterator current_street, float current_dist)> callback) const
{
    float dist = 0;

    std::vector<Stop>::const_iterator current_stop   = stops.begin();
    std::list<Street>::const_iterator current_street = streets.begin();

    while (current_stop != stops.end())
    {
        callback(current_stop, current_street, dist);

        std::vector<Stop>::const_iterator next_stop = std::next(current_stop);
        if (next_stop == stops.end()) // No next stop?
            break;

        // Are stops on the same street?
        if (std::next(current_street) == streets.end() || (current_stop->get_street_name() == next_stop->get_street_name() &&
                (current_street->get_name() != std::next(current_street)->get_name())))
        {
            dist += Utils::Math::qpoint_distance(current_stop->get_coords(), next_stop->get_coords());
            current_stop++;
            continue;
        }

        // Not on the same streets
        dist += current_street->dist_to_end(current_stop->get_coords());
        current_street++;

        while (current_street->get_name() != next_stop->get_street_name())
        {
            dist += current_street->length();
            current_street++;
        }

        dist += current_street->dist_to_start(next_stop->get_coords());

        current_stop++;
    }

    return dist;
}

std::list<Street>::const_iterator Route::traverse_distance(stop_citerator from_stop, float dist, float* out_last_street_dist) const
{
    float tmp_dist = 0;
    // Find street that we should begin from (one that contains given stop)
    street_citerator curr_street = std::find_if(streets.begin(), streets.end(), [&from_stop](const Street& s) { return s.get_name() == (*from_stop).get_street_name(); });
    float first_street_dist = curr_street->dist_to_start(from_stop->get_coords());
    tmp_dist -= first_street_dist;
    while (tmp_dist <= dist)
    {
        tmp_dist += curr_street->length();
        curr_street++;
    }

    curr_street--;
    if (out_last_street_dist != nullptr)
        (*out_last_street_dist) = curr_street->length() - (tmp_dist - dist);
    return curr_street;
}

std::vector<QPoint> Route::get_path() const
{
    std::vector<QPoint> result;
    for (const auto& street : streets)
       result.push_back(street.get_end()); // Add endpoints of all streets.

    result.insert(result.begin(), stops.front().get_coords());
    result.pop_back(); // Remove end of last street, we will replace it by last stop position
    result.push_back(stops.back().get_coords());

    return result;
}

void Route::save_route()
{
    // Save only when originals were not previosly saved
    if (original_streets.empty() && original_stops.empty())
    {
        original_streets = streets;
        original_stops = stops;
    }
}

void Route::reset_route()
{
    // If originals are empty no need to reset
    if (original_streets.empty() && original_stops.empty())
        return;

    streets = original_streets;
    stops = original_stops;

    // Dont forget to update
    update_distance();
    update_reversed();
}
