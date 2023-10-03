/**
 * @file realmodulecontroller.h
 * @author xsedmi04 (Adam Sedmík)
 * @author xsysmi00 (Michal Sýs)
 * @date May 2020
 *
 * ICP Project, VUT FIT
 *
 * Contains declaration of the RealModuleController class
 */

#ifndef REALMODULECONTROLLER_H
#define REALMODULECONTROLLER_H

#include <QMainWindow>
#include <QObject>
#include <QTimer>
#include <QtNetwork>
#include "vehiclerenderer.h"


namespace Ui { class MainWindow; };

/**
 * @brief The RealModuleController class. Manages and shows real data.
 *
 * This is independent module that is connected to the rest of the application in separate tab.
 * It uses mainly classes and functions implemented elsewhere like from MapRenderer and VehicleRenderer.
 * Although it uses some classes from other parts of the application, it doesn't use them for any underlying logic.
 * Needs internet connection to get information from web source.
 * Extends QObject to include signal-slot functionality.
 * @see MapRenderer
 * @see VehicleRenderer
 */
class RealModuleController : public QObject
{
    Q_OBJECT
public:
    /**
     * @brief Constructor for RealModuleController.
     *
     * Setups and connects parts of the real tab.
     * Initializes timers, network and labels.
     * Also draws stops and image background.
     * @param ui The interface of application
     */
    explicit RealModuleController(Ui::MainWindow* ui);
    /**
     * @brief Destructor for RealModuleController. Frees all items created during construction;
     */
     ~RealModuleController();

private:
    /**
     * @brief Instance of a mainwindow user interface.
     */
    Ui::MainWindow *ui;
    /**
     * @brief Timer for updating clock widget.
     */
    QTimer* timer;
    /**
     * @brief Timer for sending network requests.
     *
     * Request is send every 10 seconds for new data.
     */
    QTimer* network_timer;
    /**
     * @brief Vehicle renderer instance for drawing vehicles.
     */
    VehicleRenderer* vehicle_renderer;
    /**
     * @brief Network manager to handle requests.
     */
    QNetworkAccessManager *manager;
    /**
     * @brief Network request to be send.
     *
     * Has setup HTTPS connection.
     * Gets data from: https://www.miamidade.gov/transit/WebServices/MoverTrains/
     */
    QNetworkRequest request;
    /**
     * @brief Picture item used as background.
     */
    QGraphicsPixmapItem *pic;
    /**
     * @brief Constant number of seconds Miami is offset from Prague timezone.
     *
     * Used to calculate time in Miami where data are from.
     * Timer won't work correctly outside of Prague timezone.
     */
    const int MIAMI_OFFSET;

public slots:
    /**
     * @brief Updates clock widgets.
     */
    void timer_ticked();
    /**
     * @brief Sends request for new data.
     *
     * Only sends request when user is on current tab.
     */
    void timer_request();
    /**
     * @brief Handles network request reply.
     * @param reply Reply from request
     *
     * Parses given reply and calls to render parsed data.
     * If data couldn't be parsed, does nothing.
     */
    void replied(QNetworkReply* reply);
    /**
     * @brief Updates info label when item is selected.
     */
    void item_selected();
};

#endif // REALMODULECONTROLLER_H
