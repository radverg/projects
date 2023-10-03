/**
 * @file simmodulecontroller.h
 * @author xvever13 (Radek Veverka)
 * @author xsedmi04 (Adam Sedmík)
 * @author xsysmi00 (Michal Sýs)
 * @date May 2020
 *
 * ICP project, VUT FIT
 *
 * Contains declaration of SimModuleController class.
*/
#ifndef SIMMODULECONTROLLER_H
#define SIMMODULECONTROLLER_H

#include <QMainWindow>
#include <QObject>
#include "simulator.h"
#include "densitydialog.h"
#include "detourdialog.h"
#include <QTimer>
#include "vehiclerenderer.h"
#include "xmlroutesloader.h"

namespace Ui { class MainWindow; };

/**
 * @brief The SimModuleController class
 * Purpose of this class is to manage simulated part of the software.
 * It connects GUI to the logical classes of the application.
 * Extends QObject to include signal-slot functionality
 */
class SimModuleController : public QObject
{
    Q_OBJECT
public:
    /**
     * @brief Creates a controller to manage simulated part of the software.
     * @param ui Interface to simulate on.
     */
    explicit SimModuleController(Ui::MainWindow* ui);
    /**
     * @brief Destructor for SimModuleController. Clears all structures allocated during construction.
     */
    ~SimModuleController();

private:
    /**
     * @brief Instance of a mainwindow user interface used as a background for the application.
     */
    Ui::MainWindow *ui;
    /**
     * @brief Instance of a simulator needed to simulate traffic.
     */
    Simulator* simulator;
    /**
     * @brief Instance of a timer needed to update the application in constant intervals.
     */
    QTimer* timer;
    /**
     * @brief Dialog used to handle density setting for each street.
     */
    DensityDialog* densityDialog;
    /**
     * @brief Dialog used to handle creating detours.
     */
    DetourDialog * detourDialog;
    /**
     * @brief Instance of a VehicleRenderer class needed to render vehicles on screen.
     */
    VehicleRenderer* vehicleRenderer;
    /**
     * @brief Contains ordered containers of Routes and Links.
     * Loaded from a file with XMLRoutesLoader.
     *
     * @see XMLRoutesLoader
     */
    RoutesLinks routeslinks;
    /**
     * @brief Contains map of streets and stops.
     * Loaded from a file with XMLMapLoader.
     *
     * @see XMLMapLoader
     */
    StreetMap* strtm;
    /**
     * @brief Contains the currently selected link.
     */
    const Link* selected_link;

public slots:
    /**
     * @brief Updates simulator and it's visuals whenever timer sends a Timeout signal.
     */
    void timer_ticked();
    /**
     * @brief Sets speed in simulator.
     * @param speed Coefficient used to determine the new simulator speed. (double)
     */
    void speed_set(double speed);
    /**
     * @brief Sets time in simulator.
     * @param time Time which simulator resets to (QTime)
     */
    void time_set(QTime time);
    /**
     * @brief Highlights a route of a clicked vehicle and displays it's route details.
     */
    void item_selected();
    /**
     * @brief Renders streetmap with updated routes whenever a detour is created.
     *
     * @see DetourDialog
     */
    void detour_created();
    /**
     * @brief Resets simulator routes and streets to original values.
     */
    void detour_reset();
    /**
     * @brief Opens a density setting dialog when a corresponding pushbutton is clicked.
     *
     * @see DensityDialog
     */
    void on_density_click();
    /**
     * @brief Opens a detour creation dialog when a corresponding pushbutton is clicked.
     *
     * @see DetourDialog
     */
    void on_detour_click();

};

#endif // SIMMODULECONTROLLER_H
