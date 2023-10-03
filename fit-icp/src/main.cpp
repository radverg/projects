/**
* @mainpage
* Authors:
* Radek Veverka (xvever13),
* Adam Sedmík (xsedmi04),
* Michal Sýs (xsysmi00)
*
* Below we explain general architecture of our traffic tracker as well as implementation details of particular modules.
* Our team consist of 3 members, therefore apart from simulator we implemented also a real view on data periodically downloaded from a server.
*
* @section arch Application architecture
*
* We aimed to develop the application in a way that everything related to the program logic is independent on the graphical user interface.
* In other words, logic knows nothing about GUI and GUI modules can be detached and replaced without causing any harm to the logical modules.
* This idea is commonly known as MVC (model view controller) architecture pattern. We also implemented controller modules that initialize
* the application and connect GUI and logical modules together.
*
* However, we do not consider our project architecture to be clean MVC.
* For example, we break the common principle that GUI modules shoud not know where the data come from.
* That means, instead of passing raw data to GUI by the controller, we usually pass whole object instances from model domain.
* This makes rendering much easier, since we can directly utilize all public members of the model objects for rendering logic.
* To compensate this, we try to pass all objects from model domain to GUI as constant arguments, so that no GUI module can touch the core
* of the application and make it inconsistent.
*
* To sum up, logic does not know anything about attached GUI or the controllers. GUI knows about logic, but can use it only for getting necessary data for rendering.
*
* @section strt Program startup and control
*
* When program starts, execution is autmatically passed to the main window. From here we immediatelly initialize two controllers - one for simulated part of the project and one for
* the real part. Execution is then passed to the controllers. They load and initialize necessary stuff, run timers and keep GUI updated. They are the core control units of the application.
*
* @section dt Data loading
*
* As a fromat for the map and links represenation, we chose XML, mainly because the server used in the extension provides data about the traffic in XML format as well.
* XML is loaded and parsed by our XML loader classes with the help of Qt xml library. We defined loaders for each type of data we use and extended them from one base XML loader class.
* The base class checks XML validity, which is common to all loaders. Individual loaders implement own logic for reading the XML Dom tree, allocating, initializing and returning necessary objects.
*
* @section sim Simulation
* The simulator is capable of performing two main tasks - reset and update.
* Reset uses given time and sets position and state of all vehicles to match link data loaded from XML. Is used when user sets time.
* Update adds a second to the clock and moves vehicles forward.
* Current position of the vehicles (=links) is represented by the distance from the beginning of the Route the link is related to.
* This makes moving the links easy by just adding a distance computed from time and speed.
* The real coordinates of the vehicle are computed on demand using the Route information (streets, stops) and current distance.
* Invocations of Update and Reset methods are handled by the controller.
*
* @section traf Traffic density
* A street can be affected by a coefficient that slows down the vehicles.
* Density of 2 means that vehicles are 2 times slower on the street.
* On every update, simulator finds the street the link is currently on, gets the coefficient and reduces the amount of distance added to the link.
*
* @section det Detours
* Each street contain vector of detour streets. If this vector is empty, the street is opened.
* When detour adding is completed and dialog closed, all Routes get rebuilt using updated detour information on the streets.
* Once Routes are rebuilt, simulation continues normally without any difference.
*
* @section Delay
* Traffic density and detours cause delays on vehicles going through affected area. Delays are computed dynamically and displayed in link itenerary.
* Delay computation uses current time and distance of the vehicles and compares it to the original time loaded from a file.
*
* @section ext Extension
* For the extension we chose suggested server that tracks underground trains in an area of Miami.
* For network communication we used appropriate library from Qt.
* To simplify translating real world coordinates to rendering surface, we scale them linearly in the extracted area of Miami.
* This means that this module would not automatically work on a different area, since several hardcoded constants would have to be adjusted accordingly.
* We render picture from OpenStreetMap as real map background.
*
* @section ren Rendering
* To display map, stops, vehicles and itinerary, we use QGraphicsView widget.
* This works as stateful canvas and items are drawn by just adding them to the scene.
* We implement one Renderer class that extends QGraphicsView and contains necessary logic for drawing the map.
* This Renderer is common to both simulated and real module.
* Logic for drawing vehicles is separated in other class.
* Itinerary renderer is again separate class that extends QGraphicsView widget.
*/


// Automatically generated by QtCreator
#include "mainwindow.h"
#include <QApplication>

int main(int argc, char *argv[])
{
    QApplication a(argc, argv);
    MainWindow w;
    w.show();

    return a.exec();
}
