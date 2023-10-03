/**
 * @file rendererlinkoverview.h
 * @author xvever13 (Radek Veverka)
 * @date May 2020
 *
 * ICP Project, VUT FIT
 *
 * Declaration of the RendererLinkOverview class
 */

#ifndef RENDERERLINKOVERVIEW_H
#define RENDERERLINKOVERVIEW_H

#include <QWidget>
#include <QGraphicsView>
#include <QLine>
#include "link.h"

/**
 * @brief The RendererLinkOverview class.
 * Extends QGraphicsView and is used to render itinerary of the Link under the map view.
 *
 * Needs to be periodically updated by the controller in order to keep the rendered information actual.
 */
class RendererLinkOverview : public QGraphicsView
{
    Q_OBJECT

private:
    /**
     * @brief Link of whose information is this Renderer currently rendering.
     */
    const Link* link;

    /**
     * @brief Main line of the itinerary. All other graphics objects in the itinerary are positioned relatively to this line.
     */
    QLine baseline;

    // --- Display objects, need to save them after allocation to be able to change them remove them from the scene ---
    QGraphicsLineItem*          mainline;
    std::vector<QGraphicsItem*> stopitems;
    QGraphicsSimpleTextItem*    headertext;
    QGraphicsEllipseItem*       vehicle_marker;

    /**
     * @brief Half width of the stop square, in pixels.
     */
    const int STOP_SQR_HALFWIDTH;

    /**
     * @brief Refreshes header of the overview - Link name and current delay. Handles the color change as well.
     * @param link_name Link name to render.
     * @param delay_sec Delay in seconds to render.
     */
    void refresh_header(QString link_name, int delay_sec);

public:
    /**
     * @brief Automatically generated construtor for the widgets by QtCreator.
     * @param parent Parent widget of this widget.
     */
    explicit RendererLinkOverview(QWidget *parent = nullptr);

    /**
     * @brief Setter for the Link member.
     * @param link The new link to be set and rendered after next update call.
     */
    void set_link(const Link* link) { this->link = link; };

    /**
     * @brief Rerenders the View according to actual information.
     * @param current_clock Current simulation clock. Used to compute and display current delay.
     */
    void refresh(QTime current_clock);

signals:

};

#endif // RENDERERLINKOVERVIEW_H
