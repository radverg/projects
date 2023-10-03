/**
 * @file mainwindow.cpp
 * @author xsysmi00 (Michal Sýs)
 * @author xvever13 (Radek Veverka)
 * @author QtCreator
 * @date May 2020
 *
 * ICP project, VUT FIT
 *
 * Contains implementatiom of MainWindow class.
*/
#include "mainwindow.h"
#include "ui_mainwindow.h"
#include <QMessageBox>

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    ui->setupUi(this);

    // Initialize and run main module and extension module
    simmod = new SimModuleController(ui);
    realmod = new RealModuleController(ui);

}

MainWindow::~MainWindow()
{
    delete ui;
}

void MainWindow::on_exit_button_clicked()
{
    QMessageBox::StandardButton reply = QMessageBox::question(this,
                          "Exit application","Are you sure you want to exit?",
                          QMessageBox::Yes | QMessageBox::No );
    if(reply == QMessageBox::Yes) {
        QApplication::quit();
    }else { }
}

