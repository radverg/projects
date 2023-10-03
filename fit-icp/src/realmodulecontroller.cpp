/**
 * @file realmodulecontroller.cpp
 * @author xsedmi04 (Adam Sedmík)
 * @author xsysmi00 (Michal Sýs)
 * @date May 2020
 *
 * ICP Project, VUT FIT
 *
 * Contains implementation of the RealModuleController class
 */

#include <QPixmap>
#include "realmodulecontroller.h"
#include "ui_mainwindow.h"
#include "utils.h"
#include "xmlstopsloader.h"
#include "xmlonlinetrainloader.h"


RealModuleController::RealModuleController(Ui::MainWindow* ui)
    : QObject(nullptr),
      ui{ui},
      MIAMI_OFFSET{64800}
{
    vehicle_renderer             = new VehicleRenderer(ui->graphicsViewReal->scene());

    // Timer for updating current time
    timer = new QTimer(this);
    timer->setInterval(1000);
    connect(timer, &QTimer::timeout, this, &RealModuleController::timer_ticked);
    timer->start();

    // Timer for sending network requests (each 10s)
    network_timer = new QTimer(this);
    network_timer->setInterval(10000);
    connect(network_timer, &QTimer::timeout, this, &RealModuleController::timer_request);
    network_timer->start();

    // Display current times
    ui->lcdReal->display(QTime::currentTime().toString(Qt::TextDate));
    ui->lcdRealMiami->display(QTime::currentTime().addSecs(MIAMI_OFFSET).toString(Qt::TextDate));

    // Setup info label
    ui->info_label->setText("Click on train to show more info. \nTrains update every 10 seconds\nwhen on this tab\nActive connection needed");
    ui->info_label->setStyleSheet("QLabel { font-weight: bold; background-color : white; color : black; }");
    ui->info_label->setFrameShape(QFrame::Panel);

    // Setup background image as pixmap
    QImage sourceImage = QImage("../examples/map_bck.jpg");
    pic = new QGraphicsPixmapItem(QPixmap::fromImage(sourceImage));
    pic->setZValue(-1);
    pic->setX(-10); // both starting points differ a bit from 0 due to a little inaccuracy when cutting the picture
    pic->setY(5);
    ui->graphicsViewReal->scene()->addItem(pic);

    // Try loading and render stops
    try
    {
        std::map<std::string, Stop> stops = XMLStopsLoader("../examples/real_stops.xml").load_stops();
        ui->graphicsViewReal->render_stops(stops, true);
    }
    catch (const std::runtime_error &)
    {
        // If couldn't be parsed, we just won't render stops, rest should be fine
    }

    // Setup selection
    connect(ui->graphicsViewReal->scene(), SIGNAL(selectionChanged()), this, SLOT(item_selected()));

    // Setup HTTPS connection to site we get information from
    manager = new QNetworkAccessManager(this);
    QSslConfiguration config = QSslConfiguration::defaultConfiguration();
    request.setUrl(QUrl("https://www.miamidade.gov/transit/WebServices/MoverTrains/"));
    request.setSslConfiguration(QSslConfiguration::defaultConfiguration());
    connect(manager, SIGNAL(finished(QNetworkReply*)), this, SLOT(replied(QNetworkReply*)));

    // Send one request initialy so we have data
    manager->get(request);
}

RealModuleController::~RealModuleController()
{
    // Free allocated items
    delete timer;
    delete network_timer;
    delete manager;
    delete vehicle_renderer;
    delete pic;
}


void RealModuleController::timer_ticked()
{
    // Update times each second
    ui->lcdReal->display(QTime::currentTime().toString(Qt::TextDate));
    ui->lcdRealMiami->display(QTime::currentTime().addSecs(MIAMI_OFFSET).toString(Qt::TextDate));
}

void RealModuleController::timer_request()
{
    // Only send requests when on Real tab
    if (ui->tabWidget->currentIndex() == 1)
    {
        manager->get(request);
    }
}

void RealModuleController::replied(QNetworkReply* reply)
{
    // Parse reply and render vehicles
    try
    {
        std::vector<Vehicle> vc = XMLOnlineTrainLoader(reply).load_vehicles();
        vehicle_renderer->render_vehicles(vc);
    }
    catch (const std::runtime_error &)
    {
        // If unparsable, just don't render vehicles
    }

    // Auto destroy reply
    reply->deleteLater();
}

void RealModuleController::item_selected()
{
    // Workaround bug https://bugreports.qt.io/browse/QTBUG-24667
    // When signal is emited while destructing app
    if (ui->graphicsViewSim == nullptr || ui->graphicsViewReal == nullptr)
        return;

    // Get selected items
    QList<QGraphicsItem *> select = ui->graphicsViewReal->scene()->selectedItems();
    if (!select.isEmpty())
    {
        ui->info_label->clear();
        if (dynamic_cast<QgraphicsCustomVehicleItem*>(select.first())->get_link() == nullptr)
        {
            // If we have string info, display it into info label
            std::vector<std::string> info = dynamic_cast<QgraphicsCustomVehicleItem*>(select.first())->get_info();
            for (const auto& q : info)
            {
                QString text = ui->info_label->text();
                text.append(QString::fromStdString(q) + "\n");
                ui->info_label->setText(text);
            }
        }
    }
}
