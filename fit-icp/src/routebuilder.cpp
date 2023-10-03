/**
 * @file routebuilder.cpp
 * @author xvever13 (Radek Veverka)
 * @author xsedmi04 (Adam Sedmík)
 * @date May 2020
 *
 * ICP Project, VUT FIT
 *
 * Implementation of the RouteBuilder class.
 */

#include "routebuilder.h"

RouteBuilder::RouteBuilder(StreetMap *streetmap, Route *route)
    : strmap{streetmap},
      rt{route}
{
    // Nothing else necessary here.
}

void RouteBuilder::append_street(const Street& street)
{
    auto& streets = rt->streets;

    if (streets.size() == 0)
    {
        // We know nothing - just append the street, this is first one.
        streets.push_back(street);
    }
    else
    {
        // This is not first street, we need to reverse accordingly
        Street& last_str = streets.back();
        streets.push_back(street);
        Street& new_str = streets.back();

        if (new_str == last_str)
        {
            // If the streets are same, we have to ensure correct direction
            if (last_str.get_end() != new_str.get_start())
                new_str.reverse();
        }
        else
        {
            QPoint joinpoint  = last_str.get_common_point(new_str);

            if (streets.size() == 2 && last_str.get_start() == joinpoint) // Just pushed second street, adjust first one
                last_str.reverse();
            if (new_str.get_end() == joinpoint)
                new_str.reverse();
        }
    }
}


void RouteBuilder::append_stop(const Stop& stop)
{
    rt->stops.push_back(stop);
    // Also append street this stop belongs to
    const Street& strt = strmap->get_streets().at(stop.get_street_name());
    if (rt->streets.size() > 0 && rt->streets.back() == strt) // Do not append if last street is equal, that would duplicate street
        return;

    append_street( strmap->get_streets().at(stop.get_street_name()) );
}

void RouteBuilder::detour_rebuild()
{
    // If route is being rebuild, try to save original values
    rt->save_route();

    std::string start_street_name = rt->stops.front().get_street_name();
    std::string end_street_name   = rt->stops.back().get_street_name();
    auto old_streets = rt->streets;
    rt->streets.clear();

    // Iterate streets
    for (auto& strt : old_streets)
    {
        // Is this street detoured?
        const Street& strmapstr = strmap->get_streets().at( strt.get_name() );
        if (strmapstr.is_closed() && start_street_name != strmapstr.get_name() && end_street_name != strmapstr.get_name())
        {
            // Street is detoured, do not append it but append all from detour list
            const std::vector<Street>& detour_streets = (strmapstr.get_detour().front().has_endpont( strt.get_start() ) ) ?
                        strmapstr.get_detour() : std::vector<Street>(strmapstr.get_detour().rbegin(), strmapstr.get_detour().rend());
            for (const auto& det_str : detour_streets)
            {
               append_street(det_str);
            }
        }
        else
        {
            // Street is not detoured, append it normally
            append_street(strt);
        }
    }

    // Remove all stops that are located on closed street, but do not remove first and last
    if (rt->stops.size() > 2)
        rt->stops.erase(std::remove_if(std::next(rt->stops.begin()), std::prev(rt->stops.end()), [this](const Stop& s) { return strmap->get_streets().at(s.get_street_name()).is_closed(); }), std::prev(rt->stops.end()));

    rt->update_distance();
    rt->update_reversed();
}
