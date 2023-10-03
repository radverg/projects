/**
 * @file maprenderer.h
 * @author xvever13 (Radek Veverka)
 * @author xsedmi04 (Adam Sedmík)
 * @date May 2020
 *
 * ICP Project, VUT FIT
 *
 * Declaration of the MapRenderer class
 */

#ifndef MapRenderer_H
#define MapRenderer_H

#include <QGraphicsView>
#include <map>
#include "streetmap.h"
#include "route.h"
#include "vehicle.h"

/**
 * @brief The MapRenderer class. This class is capable of rendering everything related to the map.
 *
 * QGraphicsView widget in UI was promoted to this subclass.
 * Rendering vehicles is done in separate renderer.
 * Renderer is using QGraphicsScene from QGraphicsView, which is stateful.
 */
class MapRenderer : public QGraphicsView
{
    Q_OBJECT

private:
    const float MAX_SCALE;      /**< Scale of the graphics view should never go above this value. */
    const float MIN_SCALE;      /**< Scale of the graphics view should never go below this value. */
    const float SCALE_FACTOR;   /**< How fast the graphics view will zoom in/out */

    /**
     * @brief Holds pointers to graphics objects representing streets.
     *
     * Used to change how streets are displayed without creating new ones.
     */
    std::map<std::string, QGraphicsLineItem*> streets_lines;

    /**
     * @brief Here is stored the path that is used to highlight a route.
     */
    QGraphicsPathItem* highlighted_path;

    /**
     * @brief Reference to the graphics scene that is rendered on.
     */
    QGraphicsScene* sc;

    /**
     * @brief Overriden wheel event. Used for zooming in/out the scene.
     * @param event Event details.
     */
    void wheelEvent(QWheelEvent *event) override;

    /**
     * @brief Current scale value of the scene transform.
     * This needs to be manually stored and managed since this value is not easy to get back
     * from QGraphicsView object.
     */
    float current_scale;

public:
    /**
     * @brief Automatically generated constructor by QtCreator
     * @param parent Parent widget of this widget.
     */
    explicit MapRenderer(QWidget *parent = nullptr);

    /**
     * @brief Renders the streetmap - streets and their names. Then invokes render_stops().
     *
     * Needs to be invoked only once, since everything rendered by this function is static
     * and will never have to be updated throughout the program.
     *
     * @param smptr Pointer to the streetmap object to render.
     */
    void render_street_map(StreetMap* smptr);

    /**
     * @brief Highlights route by creating a path and rendering it over rendered streetmap.
     * @param route Route to highlight, if no route is given (=nullptr), highlighted route will be cleared.
     */
    void highlight_route(const Route* route = nullptr);

    /**
     * @brief Highlights detours by changing Pen of the streets.
     *
     * Closed streets will become dotted and detour streets will become thicker.
     * @param smptr StreetMap object to search for detours.
     */
    void render_detour(StreetMap* smptr);

    /**
     * @brief Reset Pen of all streets, negating the effect of render_detour method.
     */
    void remove_detour();

    /**
     * @brief Renders given stops and optionally their names. (for extension)
     *
     * This is static content that does not need to be rerendered.
     *
     * @param stops Map of stops to be rendered.
     * @param draw_names Whether to draw texts with names near the stops.
     */
    void render_stops(std::map<std::string, Stop> stops, bool draw_names);

};

#endif // MapRenderer_H
