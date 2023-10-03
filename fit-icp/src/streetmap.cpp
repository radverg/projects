/**
 * @file streetmap.cpp
 * @author xvever13 (Radek Veverka)
 * @author xsysmi00 (Michal Sýs)
 * @author xsedmi04 (Adam Sedmík)
 * @date May 2020
 *
 * ICP Project, VUT FIT
 *
 * Includes implementation of the StreetMap class
 */

#include "streetmap.h"

void StreetMap::add_street(const Street& street)
{
    streets.insert({ street.get_name(), street });
}

void StreetMap::add_stop(const Stop& stop)
{
    stops.insert({ stop.get_name(), stop });
}

std::vector<Street> StreetMap::get_follow_up_start(const Street &street) const
{
    std::vector<Street> streetVector;
    // if start coords of given street match with start or end of any other street, add that street to vector
    // must not be the same street, if it is the same street, do not add it
    for ( const auto &str : streets)
    {
        if ( (street.get_start() == str.second.get_end() || street.get_start() == str.second.get_start() ) && str.second.get_name() != street.get_name())
           { streetVector.push_back(str.second); }
    }

    return streetVector;
}

std::vector<Street> StreetMap::get_follow_up_end(const Street &street) const
{
    std::vector<Street> streetVector;
    // if end coords of given street match with start or end of any other street, add that street to vector
    // must not be the same street, if it is the same street, do not add it
    for ( const auto &str : streets)
    {
        if ( (street.get_end() == str.second.get_end() || street.get_end() == str.second.get_start() ) && str.second.get_name() != street.get_name())
           { streetVector.push_back(str.second); }
    }

    return streetVector;
}

std::vector<Street> StreetMap::get_follow_up(const Street &street) const
{
    std::vector<Street> streetVector;
    // if end or start coords of given street match with start or end of any other street, add that street to vector
    // must not be the same street, if it is the same street, do not add it
    for ( const auto &str : streets)
    {
        if ( (street.get_end() == str.second.get_end() || street.get_end() == str.second.get_start() ||
             street.get_start() == str.second.get_end() || street.get_start() == str.second.get_start() ) && str.second.get_name() != street.get_name() )
           { streetVector.push_back(str.second); }
    }

    return streetVector;
}

std::vector<Street> StreetMap::get_unselectable() const
{
    std::vector<Street> unselectableStreets;
    for (const auto &str : streets)
    {
        if (str.second.is_closed())
        {
            // add any closed street and its detour to unselectable streets
            unselectableStreets.push_back(str.second);
            unselectableStreets.insert(unselectableStreets.end(), str.second.get_detour().begin(),str.second.get_detour().end());
        }
    }
    return unselectableStreets;
}

void StreetMap::reset_streets_detour()
{
    // Clear detour vector for all its streets
    for (auto& street : streets)
    {
        street.second.clear_detour();
    }
}
