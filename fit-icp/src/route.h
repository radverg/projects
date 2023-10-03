/**
 * @file route.h
 * @author xvever13 (Radek Veverka)
 * @author xsedmi04 (Adam Sedmík)
 * @date May 2020
 *
 * ICP Project, VUT FIT
 *
 * Declaration of the Route class
 */
#ifndef ROUTE_H
#define ROUTE_H

#include <list>
#include <vector>
#include <functional>
#include "street.h"
#include "stop.h"

// Alias iterators to something less complicated
using stop_citerator    = std::vector<Stop>::const_iterator;
using street_citerator  = std::list<Street>::const_iterator;

/**
 * @brief The Route class.
 *
 * This class represents a route on a map.
 * Route consists of:
 * - Unique identifier
 * - Ordered list of streets. Street is a "primitive type", that is not allocated dynamically and is copied instead. It basically represents a line.
 * - Ordered list of stops. Stop is a "primitive type" as well. Stop always belongs to one street.
 *
 * Initializing route with streets and stops and several other tasks are done by friend class RouteBuilder,
 * since these task are not trivial and would make this class complicated and confusing.
 *
 * @see Street
 * @see Stop
 * @see RouteBuilder
 */
class Route
{
private:
    friend class RouteBuilder; /**< Route builder can directly manipulate with the route. */

    /**
     * @brief Unique identifier of the route
     */
    std::string id;

    /**
     * @brief Ordered container for all streets this route currently contains.
     */
    std::list<Street> streets;

    /**
     * @brief Ordered container for all stops this route currently contains.
     */
    std::vector<Stop> stops;

    /**
     * @brief To avoid too many recalculations, here is stored the total distance of the route.
     *
     * Is computed from streets and stops in the stl containers, using traverse method.
     * Can be recalculated by calling update_distance()
     */
    float total_distance;

    /**
     * @brief Pointer to the reversed route of this route.
     *
     * Links that go in the opposite direction than defined for the route will use this route instead.
     * Therefore link uses it's route transparently and does not have to deal with any directions.
     */
    Route* route_reversed;

    /**
     * @brief List where to save streets before detour
     */
    std::list<Street> original_streets;

    /**
     * @brief Vector where to save stops before detour
     */
    std::vector<Stop> original_stops;

public:
    /**
     * @brief Creates route and initializes it by given ID.
     *
     * Route is than further built and initialized by friend class RouteBuilder.
     * @param id ID of the route.
     */
    Route(std::string id);
    ~Route();

    /**
     * @brief Makes reversed version of this route and stores it for later usage.
     *
     * If reverse route has not been created yet (reversed pointer is nullptr), allocates new route.
     * Otherwise works with current reversed pointer and updates reversed route in place.
     */
    void update_reversed();

    /**
     * @brief Recalculates distance and stores it for later usage.
     *
     * Needs to be called when route is somehow manipulated. (created, detoured).
     */
    void update_distance();

    /**
     * @brief Converts given distance on this route to real coordinate system.
     *
     * Traverses distance from the first stop. Then uses the last street to compute resulting coordinates.
     * @param dist Distance to convert.
     * @return QPoint representing real coordinates on the route.
     */
    QPoint distance_to_coordinates(float dist) const;

    /**
     * @brief Iterates through stops of this route and gets total distance.
     * @return Distance from first stop of this route to last stop of this route.
     */
    float traversee();

    /**
     * @brief Iterates through stops of this route and calls given callback function on each stop.
     *
     * Iteration starts at the first stop and continues until the last stop is reached.
     * Callback function receives iterator to current stop on the path, current street on the path and total distance from the beginning.
     *
     * @param callback Callback function to be invoked. If none is specified, paremeterless version of this method is called.
     * @return Total traversed distance. Is equal to total length of the route.
     */
    float traversee(std::function<void(stop_citerator current_stop, street_citerator current_street, float current_dist)> callback) const;

    /**
     * @brief Traverses specified distance from given stop and gets street on which it ends.
     * @param from_stop Iterator to start traversing from.
     * @param dist Total distance to be traversed.
     * @param out_last_street_dist Optional. The distance traversed on the last street.
     * @return Iterator to street that is at a given distance from from_stop.
     */
    street_citerator traverse_distance(stop_citerator from_stop, float dist, float *out_last_street_dist = nullptr) const;

    /**
     * @brief Compute path of this route.
     * Starts at first stop and continues through all streets to last stop.
     * @return Vector of QPoint coordinates of the path.
     */
    std::vector<QPoint> get_path() const;

    /**
     * @brief Getter for vector of stops.
     * @return Vector of stops.
     */
    const std::vector<Stop>& get_stops() const { return stops; };

    /**
     * @brief Getter for total distance of this route.
     * @return Total distance of this route.
     */
    float get_total_distance() const { return total_distance; };

    /**
     * @brief Getter for the reversed route pointer.
     * @return Pointer to the route that is reversed to this route.
     */
    Route* get_reversed() const { return route_reversed; };

    /**
     * @brief Getter for id of this route.
     * @return ID of this route, e.g. 103
     */
    std::string get_id() const { return id; }

    /**
     * @brief Resets streets and stops to their original values.
     */
    void reset_route();

    /**
     * @brief Saves currents streets and stops original values.
     */
    void save_route();
};

#endif // ROUTE_H
