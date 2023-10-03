/**
 * @file simulator.h
 * @author xsedmi04 (Adam Sedmík)
 * @author xvever13 (Radek Veverka)
 * @date May 2020
 *
 * ICP Project, VUT FIT
 *
 * Contains declaration of the Simulator class
 */

#ifndef SIMULATOR_H
#define SIMULATOR_H

#include <QTime>
#include <vector>
#include "streetmap.h"
#include "link.h"
#include "vehicle.h"

/**
 * @brief The Simulator class. Simulates links on a street map.
 *
 * It's functions are called by a controller timer and it changes based on current time.
 * When functions are called, it updates Link properties accordingly.
 * Handles reset of time/simulation.
 *
 * @see Link
 */
class Simulator
{
private:
    /**
     * @brief Current time of simulation.
     */
    QTime currrent_time;
    /**
     * @brief Speed of simulation.
     *
     * How many seconds are skipped on update
     */
    double speed_coefficient;
    /**
     * @brief Pointer to street map.
     */
    StreetMap* street_map;
    /**
     * @brief Vector of links that are simulated.
     */
    std::vector<Link*> links;

public:
    /**
     * @brief Constructor for simulator.
     * @param street_map Pointer to street map to simulate.
     * @param links Vector of links to simulate.
     */
    Simulator(StreetMap* street_map, std::vector<Link*> links);
    /**
     * @brief Updates simulator.
     *
     * Uses simulator attributes to update simulator accordingly.
     * Updates all links present.
     * @param delta_milisec Timer interval used when calling update
     */
    void update(int delta_milisec);
    /**
     * @brief Resets simulator to the state it would be in at the given time.
     * @param to_time Time to reset simulator to.
     */
    void reset(QTime to_time);
    /**
     * @brief Gets all vehicles that are currently active in simulator.
     *
     * Is determined by links active and calculates their coordinates.
     * @return Vector of active vehicles.
     */
    std::vector<Vehicle> get_ongoing();
    /**
     * @brief Getter for street map of simulator.
     * @return Street map of simulator
     */
    StreetMap* get_street_map() { return street_map; };
    /**
     * @brief Getter for the current time of simulator.
     * @return Current time of simulator
     */
    QTime get_current_time() const { return currrent_time; };
    /**
     * @brief Setter for the speed of the simulator.
     * @param speed Speed of the simulator to set.
     */
    void set_speed(double speed) { speed_coefficient = speed; };
    /**
     * @brief Resets streets on streets map (handling detours)
     */
    void reset_streets();
};

#endif // SIMULATOR_H
