QT       += core gui xml
QT       += widgets network

greaterThan(QT_MAJOR_VERSION, 4): QT += widgets

CONFIG += c++1z

# The following define makes your compiler emit warnings if you use
# any Qt feature that has been marked deprecated (the exact warnings
# depend on your compiler). Please consult the documentation of the
# deprecated API in order to know how to port your code away from it.
DEFINES += QT_DEPRECATED_WARNINGS

# You can also make your code fail to compile if it uses deprecated APIs.
# In order to do so, uncomment the following line.
# You can also select to disable deprecated APIs only up to a certain version of Qt.
#DEFINES += QT_DISABLE_DEPRECATED_BEFORE=0x060000    # disables all the APIs deprecated before Qt 6.0.0

SOURCES += \
    densitydialog.cpp \
    detourdialog.cpp \
    link.cpp \
    main.cpp \
    mainwindow.cpp \
    maprenderer.cpp \
    rendererlinkoverview.cpp \
    route.cpp \
    routebuilder.cpp \
    simmodulecontroller.cpp \
    stop.cpp \
    street.cpp \
    streetmap.cpp \
    utils.cpp \
    xmlloader.cpp \
    xmlmaploader.cpp \
    xmlroutesloader.cpp \
    simulator.cpp \
    vehicle.cpp \
    vehiclerenderer.cpp \
    qgraphicscustomvehicleitem.cpp \
    realmodulecontroller.cpp \
    xmlstopsloader.cpp \
    xmlonlinetrainloader.cpp

HEADERS += \
    densitydialog.h \
    detourdialog.h \
    link.h \
    mainwindow.h \
    maprenderer.h \
    rendererlinkoverview.h \
    route.h \
    routebuilder.h \
    simmodulecontroller.h \
    stop.h \
    street.h \
    streetmap.h \
    utils.h \
    xmlloader.h \
    xmlmaploader.h \
    xmlroutesloader.h \
    simulator.h \
    vehicle.h \
    vehiclerenderer.h \
    qgraphicscustomvehicleitem.h \
    realmodulecontroller.h \
    xmlstopsloader.h \
    xmlonlinetrainloader.h

FORMS += \
    mainwindow.ui

# Default rules for deployment.
qnx: target.path = /tmp/$${TARGET}/bin
else: unix:!android: target.path = /opt/$${TARGET}/bin
!isEmpty(target.path): INSTALLS += target

DISTFILES += \
    ../examples/routes_links.xml \
    ../examples/streetmap.xml \
    ../examples/real_stops.xml
