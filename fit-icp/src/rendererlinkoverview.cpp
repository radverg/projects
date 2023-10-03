/**
 * @file rendererlinkoverview.cpp
 * @author xvever13 (Radek Veverka)
 * @date May 2020
 *
 * ICP Project, VUT FIT
 *
 * Implementation of the RendererLinkOverview class
 */

#include <QGraphicsRectItem>
#include <QGraphicsSimpleTextItem>
#include "rendererlinkoverview.h"
#include "utils.h"


RendererLinkOverview::RendererLinkOverview(QWidget *parent)
    : QGraphicsView(parent),
      link{nullptr},
      STOP_SQR_HALFWIDTH{5}
{
    setScene(new QGraphicsScene(this));
    auto sc = scene();
    baseline = QLine(10, 60, 900, 60);

    // Create main line
    mainline = sc->addLine(baseline);
    // Create header text
    headertext = sc->addSimpleText("", QFont("Arial", 18, QFont::Bold));
    // Create vehicle marker
    vehicle_marker = sc->addEllipse(QRectF(0, 0, STOP_SQR_HALFWIDTH * 2, STOP_SQR_HALFWIDTH * 2), QPen(), QBrush(Qt::green));
    vehicle_marker->setZValue(1);
}

void RendererLinkOverview::refresh(QTime current_clock)
{
    if (link == nullptr || link->get_activity() == false)
    {
        // Hide this and get out of here
        setVisible(false);
        return;
    }

    setVisible(true);

    auto sc = scene();

    // Remove previous stops
    for (auto item : stopitems)
    {
        sc->removeItem(item);
        delete item;
    }

    stopitems.clear();

    // Now, lets iterate over stops and create them
    const Route* rt = link->get_route();
    float total_distance = rt->get_total_distance();
    auto orig_stop_times = link->get_original_stop_times();
    float before_dist   = 0;
    float after_dist    = 0;
    QTime before_time;
    QTime after_time;

    // Traverse the route and render itinerary
    rt->traversee([&](stop_citerator stop, street_citerator, float dist) {
        QPoint stop_position = Utils::Math::position_on_line(baseline.p1(), baseline.p2(), dist / total_distance);
        QGraphicsRectItem* rectitem = sc->addRect(stop_position.x() - STOP_SQR_HALFWIDTH, stop_position.y() - STOP_SQR_HALFWIDTH, STOP_SQR_HALFWIDTH * 2, STOP_SQR_HALFWIDTH * 2, QPen(), QBrush(Qt::black));
        stopitems.push_back(rectitem);

        // Draw stop name as well
        QGraphicsTextItem* text = sc->addText(QString::fromStdString(stop->get_name()));
        text->setPos(stop_position.x() + 5, stop_position.y() + 5);
        text->setRotation(45);
        stopitems.push_back(text);

        // Draw stop original time
        QTime t(orig_stop_times[stop->get_name()]);
        QGraphicsSimpleTextItem* text_time = sc->addSimpleText(t.toString("hh:mm"));
        text_time->setPos(stop_position - QPoint(text_time->boundingRect().width() / 2, 20));
        stopitems.push_back(text_time);

        if (dist <= link->get_distance())
        {
            before_dist = dist;
            before_time = orig_stop_times[stop->get_name()];
        }

        if (after_dist == 0 && dist > link->get_distance())
        {
            after_dist = dist;
            after_time = orig_stop_times[stop->get_name()];
        }
    });

    // Move vehicle marker to correct position
    vehicle_marker->setPos(Utils::Math::position_on_line(baseline.p1(), baseline.p2(), link->get_distance() / total_distance) - QPoint(STOP_SQR_HALFWIDTH, STOP_SQR_HALFWIDTH));

    // Compute delay and refresh header
    int delay_sec = 0;

    if (after_dist != 0)
    {
        float inter_stop_distance = after_dist - before_dist;
        float factor = (link->get_distance() - before_dist) / inter_stop_distance;
        int inter_stop_secs = Utils::Time::seconds_between_times(before_time, after_time);
        QTime target_time = before_time.addSecs(inter_stop_secs * factor); // At this time, link is supposed to be at current position if no delays are present
        delay_sec = Utils::Time::seconds_between_times(target_time, current_clock);
        // Tolerate two seconds
        if (delay_sec >= -2 && delay_sec <= 2) delay_sec = 0;
    }

    refresh_header(QString::fromStdString(rt->get_id()), delay_sec);
}


void RendererLinkOverview::refresh_header(QString link_name, int delay_sec)
{
    QString result_text = link_name;
    headertext->setBrush(QBrush(Qt::black));

    if (delay_sec > 0)
    {
        result_text += ", Delay: " + QTime(0, 0).addSecs(delay_sec).toString("hh:mm:ss");
        headertext->setBrush(QBrush(Qt::red));
    }

    QPoint center = Utils::Math::position_on_line(baseline.p1(), baseline.p2(), 0.5);
    headertext->setText(result_text);
    QRectF br = headertext->boundingRect();
    headertext->setPos(QPoint(center.x() - br.width() / 2, center.y() - 50));
}

