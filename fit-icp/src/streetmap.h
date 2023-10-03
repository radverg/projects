/**
 * @file streetmap.h
 * @author xvever13 (Radek Veverka)
 * @author xsysmi00 (Michal Sýs)
 * @author xsedmi04 (Adam Sedmík)
 * @date May 2020
 *
 * ICP Project, VUT FIT
 *
 * Includes declaration of the StreetMap class
 */

#ifndef MAP_H
#define MAP_H

#include <map>
#include <vector>
#include "street.h"
#include "stop.h"

/**
 * @brief The StreetMap class. Holds information about all streets and stops.
 *
 * This class is very simple, contains only two maps, one for streets and one for stops.
 * The rest are just helper methods for searching the map, getters or add methods.
 * Keys to the maps are strings which contain name (unique identifier) of the Streets/Stops.
 * @see Street
 * @see Stop
 */
class StreetMap
{
private:
    /**
     * @brief Map of all streets on the map. Key is the name of a street.
     */
    std::map<std::string, Street> streets;

    /**
     * @brief Map of all stops on the map. Key is the name of a stop.
     */
    std::map<std::string, Stop> stops;

public:
    /**
     * @brief Copies the given street and adds it to the map of streets under it's name.
     * @param street Street to be added.
     */
    void add_street(const Street& street);

    /**
     * @brief Copies the given stop and adds it to the map of stops under it's name.
     * @param stop Stop to be added.
     */
    void add_stop(const Stop& stop);

    /**
     * @brief Getter for the map of streets.
     * @return Reference to the map of streets in this StreetMap.
     *
     * We have to provide reference to non-const, because unlike on stops, some adjustments needs to be done on streets from outside (eg. set density).
     */
    std::map<std::string, Street>& get_streets() { return streets; };

    /**
     * @brief Getter for the map of stops.
     * @return Reference to constant map of stops in this StreetMap.
     */
    const std::map<std::string, Stop>& get_stops() const { return stops; };

    /**
     * @brief Used to get an ordered container of streets that follow up from the start point of given street.
     * @param str Street to calculate the follow-up streets from.
     * @return Ordered container of streets that follow up from the start point of given street.
     */
    std::vector<Street> get_follow_up_start(const Street &str) const;
    /**
     * @brief Used to get an ordered container of streets that follow up from the end point of given street.
     * @param str Street to calculate the follow-up streets from.
     * @return Ordered container of streets that follow up from the end point of given street.
     */
    std::vector<Street> get_follow_up_end(const Street &str) const;
    /**
     * @brief Used to get an ordered container of streets that follow up from start or end point of given street.
     * @param str Street to calculate the follow-up streets from.
     * @return Ordered container of streets that follow up from the start or end point of given street.
     */
    std::vector<Street> get_follow_up(const Street &str) const;
    /**
     * @brief Used to get an ordered container of streets that can no longer be selected as streets to close.
     *
     * When making detour, user cannot close a street that has already been closed
     * and he cannot choose a street that is already a part of another detour.
     *
     * @return Ordered container of streets that user cannot select when choosing a street to close.
     */
    std::vector<Street> get_unselectable() const;

    /**
     * @brief Handles reseting streets detours
     */
    void reset_streets_detour();
};

#endif // MAP_H
