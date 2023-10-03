/**
 * @file street.h
 * @author xvever13 (Radek Veverka)
 * @author xsysmi00 (Michal Sýs)
 * @author xsedmi04 (Adam Sedmík)
 * @date May 2020
 *
 * ICP Project, VUT FIT
 *
 * Includes declaration of the Street class
 */

#ifndef STREET_H
#define STREET_H

#include <iostream>
#include <vector>
#include <QPoint>

/**
 * @brief The Street class. Holds necessary information about a street - start point, end point, name, traffic density and detour info.
 *
 * As well as Stop, this class is more like a "primitive" data type.
 * It is not meant to be allocated dynamically.
 * It basically represents a line with extra information and this class is not linked with anything else.
 * This approach offers more flexibility when working with Streets and Stops.
 */
class Street
{
private:
    /**
     * @brief Unique string identifier of the street.
     */
    std::string name;

    /**
     * @brief Pixel coordinates of the start point of the street.
     */
    QPoint start;

    /**
     * @brief Pixel coordinates of the end point of the street.
     */
    QPoint end;

    /**
     * @brief Traffic density coefficient.
     *
     * Represents the level of traffic density over the whole street.
     * This number is used to scale down speed of simulated vehicles currently going on this street.
     */
    double traff_density;

    /**
     * @brief Vector of streets that are set as detour to this street.
     *
     * When a street gets closed, alternative path is stored in this attribute.
     * Empty vector means that the street is not closed.
     */
    std::vector<Street> detour;

public:
    /**
     * @brief Constructs a street using start point, end point and identifier (name).
     *
     * Only copies attributes to the members. No other action is necessary.
     * @param start Start point of the street.
     * @param end End point of the street.
     * @param name Identifier of the street.
     */
    Street(QPoint start, QPoint end, std::string name);

    /**
     * @brief Swaps start point and end point of this street.
     *
     * This is useful for building the route from the streets, where builder has to ensure correct street orientation.
     */
    void reverse();

    /**
     * @brief Finds common point of the streets - only start points and end points are taken into consideration.
     *
     * @throws If no start/end points are same (common) for two streets.
     * @param other The second street to compare.
     * @return End point or start point of the street - the one that is common with the other street.
     */
    QPoint get_common_point(const Street& other);

    /**
     * @brief Checks if this street and street passed as argument have one of it's endpoints on the same location.
     * @param other Other street
     * @return True if two streets have common endpoint
     */
    bool has_common_point(const Street& other) const;

    /**
     * @brief Getter for the start coordinates of the street.
     * @return Start coordinates of the street.
     */
    QPoint get_start() const { return start; };

    /**
     * @brief Computes the point that is half-way on the street. Uses start point and end point of the street.
     * @return Point in the middle of the street.
     */
    QPoint get_middle() const;

    /**
     * @brief Getter for the end coordinates of the street.
     * @return End coordinates of the street.
     */
    QPoint get_end() const { return end; };

    /**
     * @brief Computes the angle between street line and x axis.
     * @return Angle between street line and x axis, in degrees.
     */
    qreal get_slope() const;

    /**
     * @brief Computes the length of the street using start point and end point.
     * @return The length of the street.
     */
    float length() const;

    /**
     * @brief Computes distance between given point and start point of this street.
     * @param from The other point
     * @return Distance between "from" point given and start point of the street.
     */
    float dist_to_start(const QPoint& from) const;

    /**
     * @brief Computes distance between given point and end point of this street.
     * @param from The other point
     * @return Distance between "from" point given and end point of the street.
     */
    float dist_to_end(const QPoint& from) const;

    /**
     * @brief Custom comparison operator that performs comparison on street names (identifiers).
     * @param rhs Right hand side of the operation.
     * @return Whether identifiers of the streets are equal.
     */
    bool operator ==(const Street& rhs);

    /**
     * @brief Checks whether the given point is one of endpoints of the street (start or end).
     * @param pt Point to check.
     * @return True if coordinates of given point matches start or end point of the street.
     */
    bool has_endpont(QPoint pt) const;

    /**
     * @brief Getter for the unique name of the street.
     * @return Name of the street.
     */
    std::string get_name() const { return name; };

    /**
     * @brief Setter for the detour vector.
     * @param streetDetour Vector of streets to be copied to detour vector of the street.
     */
    void set_detour(std::vector<Street> streetDetour) { detour = streetDetour; }

    /**
     * @brief Getter for the detour vector.
     * @return Current vector of streets participating in detour for this street.
     */
    const std::vector<Street>& get_detour() const { return detour; }

    /**
     * @brief Getter for the traffic density coefficient.
     * @return Trafic density coefficient.
     */
    double get_density() const { return traff_density; }

    /**
     * @brief Setter for the traffic density coefficient.
     * @param density The new traffic density to be set.
     */
    void set_density(double density) { traff_density = density; }

    /**
     * @brief Determines whether this street is closed by couting the elements in detour vector member of the street.
     * @return Whether the street is closed or not.
     */
    bool is_closed() const;

    /**
     * @brief Clears detour vector
     */
    void clear_detour();
};

#endif // STREET_H

