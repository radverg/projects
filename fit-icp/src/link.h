/**
 * @file link.h
 * @author xvever13 (Radek Veverka)
 * @author xsedmi04 (Adam Sedmík)
 * @date May 2020
 *
 * ICP Project, VUT FIT
 *
 * Declaration of the Link class
 */

#ifndef LINK_H
#define LINK_H

#include <QTime>
#include <map>
#include "route.h"

/**
 * @brief The Link class. Represents one travel over a route.
 *
 * From the beginning, object of this class is tightly bound to a route.
 * The main differences of Route and Link:
 * - Route defines path on a map.
 * - Link defines one travel over specific Route from given start time to given end time.
 * In other words, link can represent one vehicle on a route at a specific time.
 * More Links can be bound to a single route or it's reversed route.
 * The main difference is also that link works with time and current distance, that changes over time and simulates movement,
 * whereas Route just defines a path for a link.
 */
class Link
{
private:
    /**
     * @brief Route pointer this link is bound to.
     *
     * Given in constructor and not changed anymore.
     */
    Route* const route;

    /**
     * @brief Start time of this Link.
     *
     * At this time, link is on first stop position on related Route.
     */
    const QTime start;

    /**
     * @brief End time of this Link.
     *
     * At this time, link is on the last stop position on related Route.
     */
    const QTime end;

    /**
     * @brief Stop times between start time and end time.
     *
     * For each stop from related Route, target time is computed and stored in this map.
     * Key is the name of a stop.
     * This is used for dynamically computing current delay of the Link.
     */
    std::map<std::string,QTime> original_stop_times;

    /**
     * @brief Is this link currently active on the map?
     * This property indicates whether Link is currently on the way.
     * Property is used and managed by the Simulator.
     */
    bool active;

    /**
     * @brief Current distance of the Link.
     * This value says how far is the Link from the start of related Route at the moment.
     * Used and managed by the Simulator.
     */
    float distance;

    /**
     * @brief Speed at which this Link travels.
     *
     * It is computed during class initialization from related Route distance
     * and start time and end time of the Link.
     */
    float default_speed;

public:
    /**
     * @brief Constructs new Link.
     *
     * - Binds given route.
     * - Computes and stores default_speed.
     * - Computes and stores original_stop_times.
     * - Initializes rest of members normally.
     * @param route Route this Link will be bound to.
     * @param start Start time of the Link.
     * @param end End time of the Link.
     */
    Link(Route* const route, const QTime start , const QTime end);

    /**
     * @brief Getter for start member.
     * @return Start time of the link.
     */
    QTime get_start() const { return start; };

    /**
     * @brief Getter for end member.
     * @return End time of the link.
     */
    QTime get_end() const { return end; };

    /**
     * @brief Getter for Route member.
     * @return Pointer to related route.
     */
    Route* get_route() const { return route; };

    /**
     * @brief Setter for activity member, used by the Simulator.
     * @param activity New activity value to be set.
     */
    void set_activity(bool activity) { active = activity; };

    /**
     * @brief Getter for activity member.
     * @return Current activity value.
     */
    bool get_activity() const { return active; };

    /**
     * @brief Setter for distance member, used by the Simulator.
     * @param dist New distance to be set.
     */
    void set_distance(float dist);

    /**
     * @brief Getter for distance member.
     * @return Current distance.
     */
    float get_distance() const { return distance; };

    /**
     * @brief Getter for default_speed member.
     * @return Default speed of the Link
     */
    float get_default_speed() const { return default_speed; };

    /**
     * @brief Getter for original_stop_times member.
     * @return Reference to map of times for each stop.
     */
    const std::map<std::string,QTime>& get_original_stop_times() const { return original_stop_times; };

    /**
     * @brief Resets distance so it is in an accordance to the given time.
     *
     * Uses default speed and start time to compute the distance.
     *
     * @param new_time Time to compute the distance from.
     * @throws If computed distance is greater than total distance of the Route.
     */
    void reset_distance(QTime new_time);

    /**
     * @brief Determines whether the Link should be on it's way at the given time.
     *
     * Works with start time of the link, but not with end time, since that can be different when detour is added.
     * End time always remains the same in order to correctly compute current delay of the Link.
     *
     * @param time Time to check.
     * @return Whether link is on it's way (is/should be active).
     */
    bool is_on_map_by_time(const QTime& time) const;

    /**
     * @brief Finds out name of the street this Link is currently located at.
     *
     * Finds it by traversing current distance from the first stop using Route->traverse_distance.
     *
     * @return Street name where Link is located.
     */
    std::string get_current_street_name() const;

    /**
     * @brief Computes target times within time range <start,end> for each stop on the route, using default_speed.
     * @return Map of stop names to their times.
     */
    std::map<std::string,QTime> compute_stop_times() const;
};

#endif // LINK_H
