/**
 * @file mainwindow.h
 * @author Qt Creator
 * @author xsysmi00 (Michal Sýs)
 * @author xvever13 (Radek Veverka)
 * @date May 2020
 *
 * ICP project, VUT FIT
 *
 * Contains declaration of MainWindow class.
*/
#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include <QMainWindow>
#include "simmodulecontroller.h"
#include "realmodulecontroller.h"

QT_BEGIN_NAMESPACE
namespace Ui { class MainWindow; }
QT_END_NAMESPACE

/**
 * @brief The MainWindow class - generated automatically
 */
class MainWindow : public QMainWindow
{
    Q_OBJECT
private:

public:
    MainWindow(QWidget *parent = nullptr);
    ~MainWindow();

private slots:
    /**
     * @brief Opens up an exit dialog.
     */
    void on_exit_button_clicked();

private:
    Ui::MainWindow *ui;
    /**
     * @brief Main application module (traffic simulator)
     */
    SimModuleController* simmod;
    /**
     * @brief Extension module (Miami metro tracker and visualizer)
     */
    RealModuleController* realmod;


};
#endif // MAINWINDOW_H
