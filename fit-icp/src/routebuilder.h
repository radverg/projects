/**
 * @file routebuilder.h
 * @author xvever13 (Radek Veverka)
 * @date May 2020
 *
 * ICP Project, VUT FIT
 *
 * Declaration of the RouteBuilder class.
 */

#ifndef ROUTEBUILDER_H
#define ROUTEBUILDER_H

#include "streetmap.h"
#include "route.h"

/**
 * @brief The RouteBuilder class. Is responsible for correctly appending streets and stops to routes + managing detours.
 *
 * This class is a friend to Route class. We decided to move the building process of the route to separate class in order
 * to keep Route class clean of this little dirty and not straightforward task.
 *
 * When route is created, it is than passed to this "builder" along with StreetMap. Methods in this class can than manage the Route path.
 */
class RouteBuilder
{
private:
    /**
     * @brief Pointer to the street map this RouteBuilder is working with.
     */
    StreetMap* strmap;
    /**
     * @brief Pointer to the route this RouteBuilder is working with.
     */
    Route *rt;

public:
    /**
     * @brief Constructs RouteBuilder.
     * @param streetmap StreetMap this builder will work with.
     * @param route Route this builder will work with.
     */
    RouteBuilder(StreetMap *streetmap, Route *route);

    /**
     * @brief Appends new Street to the Route.
     *
     * Also reverses the street if the orientation is incorrect.
     *
     * @param street Street to be copied and appended.
     */
    void append_street(const Street& street);

    /**
     * @brief Appends new Stop to the Route.
     *
     * Also automatically appends underlying street if it was not appended previously.
     *
     * @param street Street to be copied and appended.
     */
    void append_stop(const Stop& stop);

    /**
     * @brief Cleans vector of streets and builds it again, checking for detours and appending them correctly.
     *
     * Deletes Stops that are located on closed streets.
     * If closed street contains first or last stop of the route, detour is ignored.
     */
    void detour_rebuild();
};

#endif // ROUTEBUILDER_H
