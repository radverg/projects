/**
 * @file maprenderer.cpp
 * @author xvever13 (Radek Veverka)
 * @author xsedmi04 (Adam Sedmík)
 * @date May 2020
 *
 * ICP Project, VUT FIT
 *
 * Implementation of the MapRenderer class
 */

#include "maprenderer.h"
#include <QGraphicsLineItem>
#include <QWheelEvent>

MapRenderer::MapRenderer(QWidget *parent)
    : QGraphicsView(parent),
      MAX_SCALE{5},
      MIN_SCALE{0.1},
      SCALE_FACTOR{0.2},
      highlighted_path{nullptr},
      current_scale{1}
{
    setScene(new QGraphicsScene(this));
    sc = scene();
}

void MapRenderer::render_street_map(StreetMap* smptr)
{
    for(const auto& street : smptr->get_streets())
    {
        QGraphicsLineItem* line = sc->addLine(QLineF(street.second.get_start(), street.second.get_end()));
        streets_lines.insert({street.first, line});

        QGraphicsTextItem* text = sc->addText(QString::fromStdString(street.second.get_name()));
        text->setRotation(street.second.get_slope());
        text->setPos(street.second.get_middle());
    }

    render_stops(smptr->get_stops(), false);
}

void MapRenderer::render_stops(std::map<std::string, Stop> stops, bool draw_names)
{
    for (const auto& stop : stops)
    {
        // Stops are 6x6 rectancles, offset coords by half to align
        sc->addRect(stop.second.get_coords().x()-3, stop.second.get_coords().y()-3, 6, 6, QPen(), QBrush(Qt::blue));
        if (draw_names)
        {
            QGraphicsTextItem* text = sc->addText(QString::fromStdString(stop.second.get_name()));
            text->setPos(QPoint(stop.second.get_coords()));
            text->setDefaultTextColor(Qt::darkBlue);
        }
    }
}

void MapRenderer::render_detour(StreetMap* smptr)
{
    // first detour streets
    for(const auto& street : smptr->get_streets())
    {
        if (!street.second.get_detour().empty())
        {
            std::vector<Street> detour = street.second.get_detour();
            for(const auto& detour_street : detour)
            {
                QPen pen(Qt::black);
                pen.setWidth(2);
                streets_lines.at(detour_street.get_name())->setPen(pen);
            }
        }
    }

    // then blocked streets (blocked can be part of detour)
    for(const auto& street : smptr->get_streets())
    {
        if (!street.second.get_detour().empty())
        {
            streets_lines.at(street.first)->setPen(Qt::DotLine);
        }
    }
}

void MapRenderer::remove_detour()
{
    for(const auto& item : streets_lines)
    {
        // Reset hightlights
        QPen pen(Qt::black);
        pen.setWidth(1);
        pen.setStyle(Qt::SolidLine);
        item.second->setPen(pen);
    }
}

void MapRenderer::highlight_route(const Route* route)
{
    // Clean current highliht, if any
    if (highlighted_path != nullptr)
    {
        sc->removeItem(highlighted_path);
        highlighted_path = nullptr;
    }

    if (route == nullptr)
       return;

    // Build new path
    std::vector<QPoint> route_points = route->get_path();
    QPainterPath path(route_points.front());
    for (auto p = std::next(route_points.begin()); p != route_points.end(); p++)
        path.lineTo(*p);

    QPen pen(Qt::darkGreen);
    pen.setWidth(2);
    highlighted_path = sc->addPath(path, pen);
}

void MapRenderer::wheelEvent(QWheelEvent *event)
{
    // We react to wheel event by zooming in/out the renderer
    float dir = (event->delta() < 0) ? -1 : 1;

    resetMatrix();
    current_scale += dir * SCALE_FACTOR;
    // Restrict the scale value to be within the allowed scale range
    if (current_scale < MIN_SCALE) current_scale = MIN_SCALE;
    if (current_scale > MAX_SCALE) current_scale = MAX_SCALE;
    scale(current_scale, current_scale);
}
