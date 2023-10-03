/**
 * @file simmodulecontroller.cpp
 * @author xvever13 (Radek Veverka)
 * @author xsedmi04 (Adam Sedmík)
 * @author xsysmi00 (Michal Sýs)
 * @date May 2020
 *
 * ICP project, VUT FIT
 *
 * Contains implementation of SimModuleController class.
*/
#include "simmodulecontroller.h"
#include "ui_mainwindow.h"
#include "xmlmaploader.h"
#include "xmlroutesloader.h"
#include "vehiclerenderer.h"
#include "routebuilder.h"

SimModuleController::SimModuleController(Ui::MainWindow* ui) : QObject(nullptr), ui{ui}, selected_link{nullptr}
{
    // Lets initialize and connect the parts of the application
    // Loading...
    try
    {
        strtm                       = XMLMapLoader("../examples/streetmap.xml").load_map(); // Load street map
        routeslinks                 = XMLRoutesLoader("../examples/routes_links.xml").load_routes_links(*strtm); // Load links/routes
    }
    catch (const std::runtime_error &e)
    {
        // If couldn't be parsed, render error message, don't start or connnect anything
        // Real part still works
        ui->graphicsViewSim->scene()->addText(QString::fromStdString(std::string(e.what()) + "\n"
                                              "Default example files couldn't be parsed, please exit and fix this issue,\n"
                                              "or switch to Real tab to see real data."));
        return;
    }

    simulator                   = new Simulator(strtm, routeslinks.links); // Create simulator that will simulate on loaded stuff
    densityDialog               = new DensityDialog(strtm);
    detourDialog                = new DetourDialog(strtm);
    vehicleRenderer             = new VehicleRenderer(ui->graphicsViewSim->scene());

    // We gonna need a timer
    timer = new QTimer(this);
    timer->setInterval(1000);
    connect(timer, &QTimer::timeout, this, &SimModuleController::timer_ticked);
    timer->start();

    // We need to get notified when user sets time
    connect(ui->timeEdit, SIGNAL(userTimeChanged(QTime)), this, SLOT(time_set(QTime)));
    // We need to get notified when user sets speed
    connect(ui->doubleSpinBox, SIGNAL(valueChanged(double)), this, SLOT(speed_set(double)));

    // Set simulation time to current time, this will automatically reset simulation and set clock value
    time_set(QTime::currentTime());

    // Render streetmap
    ui->graphicsViewSim->render_street_map(strtm);

    // select items
    connect(ui->graphicsViewSim->scene(), SIGNAL(selectionChanged()), this, SLOT(item_selected()));

    // detour created
    connect(detourDialog, SIGNAL(on_detour_finished()), this, SLOT(detour_created()));

    // connect pushbuttons to dialogs and detour reset
    connect(ui->density_button, SIGNAL(clicked()), this, SLOT(on_density_click()));
    connect(ui->detour_button, SIGNAL(clicked()), this, SLOT(on_detour_click()));
    connect(ui->detour_reset_button, SIGNAL(clicked()), this, SLOT(detour_reset()));

}
SimModuleController::~SimModuleController()
{
    // Clear all routes and links
    for (auto route : routeslinks.routes)
        delete route;
    for (auto link : routeslinks.links)
        delete link;
    // Other allocated items
    delete strtm;
    delete simulator;
    delete vehicleRenderer;
    delete densityDialog;
    delete detourDialog;
    delete timer;
}

void SimModuleController::timer_ticked()
{
    // If timer ticks, we have to:
    // Update simulator
    simulator->update(timer->interval());
    std::vector<Vehicle> ongoing = simulator->get_ongoing();
    vehicleRenderer->render_vehicles(ongoing);

    // Clean selected link if inactive
    if (selected_link != nullptr && selected_link->get_activity() == false)
        selected_link = nullptr;

    // Update clock widget to display actual simulation time
    ui->lcdNumber->display(simulator->get_current_time().toString(Qt::TextDate));

    // Update overview panel
    ui->graphicsViewLink->refresh(simulator->get_current_time());

    // Update highlight
    ui->graphicsViewSim->highlight_route((selected_link == nullptr) ? nullptr : selected_link->get_route());
}

void SimModuleController::time_set(QTime time)
{
    // Time was set, we need to reset simulator to this time
    simulator->reset(time);
    std::vector<Vehicle> ongoing = simulator->get_ongoing();
    vehicleRenderer->render_vehicles(ongoing);

    // Also update clock to avoid 1 sec delay
    ui->lcdNumber->display(simulator->get_current_time().toString(Qt::TextDate));
    // Update overview panel
    ui->graphicsViewLink->refresh(simulator->get_current_time());

}

void SimModuleController::speed_set(double speed)
{
    // Speed was set, we need to reflect this in simulator
    simulator->set_speed(speed);
}


void SimModuleController::item_selected()
{
    // Workaround bug https://bugreports.qt.io/browse/QTBUG-24667
    // When signal is emited while destructing app
    if (ui->graphicsViewSim == nullptr || ui->graphicsViewReal == nullptr)
        return;

    QList<QGraphicsItem *> select =  ui->graphicsViewSim->scene()->selectedItems();
    if (!select.isEmpty())
    {
        selected_link = dynamic_cast<QgraphicsCustomVehicleItem*>(select.first())->get_link();

        // Highlight route
        ui->graphicsViewSim->highlight_route(selected_link->get_route());
        // Show route details
        ui->graphicsViewLink->set_link(selected_link);
        ui->graphicsViewLink->refresh(simulator->get_current_time());
    }
}

void SimModuleController::detour_created()
{
    // Need to render updated street map
    ui->graphicsViewSim->render_detour(simulator->get_street_map());

    // Refresh all routes
    for (auto rt : routeslinks.routes)
        RouteBuilder(strtm, rt).detour_rebuild();

}

void SimModuleController::detour_reset()
{
    // Reset in simulator and renderer
    simulator->reset_streets();
    simulator->reset(simulator->get_current_time());
    ui->graphicsViewSim->remove_detour();
    detourDialog->selection_reset();
}

void SimModuleController::on_density_click()
{
    densityDialog->exec();
}

void SimModuleController::on_detour_click()
{
    detourDialog->exec();
}

