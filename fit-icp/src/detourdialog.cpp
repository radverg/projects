/**
 * @file detourdialog.cpp
 * @author xsysmi00 (Michal Sýs)
 * @date May 2020
 *
 * ICP project, VUT FIT
 *
 * Contains implementation of DetourDialog class.
*/
#include "detourdialog.h"
#include <QDialog>
#include <QHBoxLayout>
#include <QPushButton>
#include <QMessageBox>
#include <algorithm>

DetourDialog::DetourDialog(StreetMap* strMap)
{
    this->strMap = strMap;
    setWindowTitle("Detour Options");
    // hide question mark button
    Qt::WindowFlags flags(Qt::WindowTitleHint);
    this->setWindowFlags(flags);

    // label with instructions
    instructionLabel = new QLabel("Select street to close:");
    instructionLabel->setStyleSheet("font-weight: bold; color:blue");

    buttonBox = new QDialogButtonBox(QDialogButtonBox::Ok | QDialogButtonBox::Close);
    // might not be the best idea to change meaning of ok button
    // change button names so it matches the function
    buttonBox->button(QDialogButtonBox::Ok)->setText("Reset selection");
    buttonBox->button(QDialogButtonBox::Close)->setText("Close window");
    connect(buttonBox, SIGNAL(accepted()), this, SLOT(selection_reset()));
    connect(buttonBox, SIGNAL(rejected()), this, SLOT(reject()));

    // add all streets to List Widget
    streetList = new QListWidget;
    for (const auto &str : strMap->get_streets())
    {
        QString strName = QString::fromStdString(str.second.get_name());
        streetList->addItem(strName);
    }

    // when item in ListWidget is selected, continue to next selection
    connect(streetList, &QListWidget::itemClicked, this, &DetourDialog::street_selected);

    // set layout
    QVBoxLayout* mainLayout = new QVBoxLayout;
    mainLayout->addWidget(instructionLabel);
    mainLayout->addWidget(streetList);
    mainLayout->addWidget(buttonBox);

    setLayout(mainLayout);  

}

void DetourDialog::show_no_street_available()
{
    QMessageBox msgBox;
    msgBox.setWindowTitle("Detour making error");
    msgBox.setText("Cannot connect new detour through here!");
    msgBox.setInformativeText("Try to choose streets that will connect you to the street you want to close.\n\nSelection will be reset.");
    msgBox.setIcon(QMessageBox::Critical);
    msgBox.exec();
    DetourDialog::selection_reset();
}

void DetourDialog::show_wrong_detour()
{
    QMessageBox msgBox;
    msgBox.setWindowTitle("Detour making error");
    msgBox.setText("Detour must end on the other side of closed street!");
    msgBox.setInformativeText("Selection will be reset.");
    msgBox.setIcon(QMessageBox::Critical);
    msgBox.exec();
    DetourDialog::selection_reset();
}

// user presses item, save data, move on to selection of the street to avoid
void DetourDialog::street_selected(QListWidgetItem* item)
{
    // this is the first iteration of detour selection
    firstIter = true;
    closedStreet = item->text();
    // get the street name from selected item
    QString qstreetName = item->text();
    // convert street name to std::string
    std::string streetName = qstreetName.toUtf8().constData();
    // clear previous options
    streetList->clear();


    // show follow up streets in list view, only the ones from start point of street
    for (const auto &street : strMap->get_follow_up_start(strMap->get_streets().at(streetName)))
    {
        QString qstrName = QString::fromStdString(street.get_name());
        // add only streets that are not closed
        if (!strMap->get_streets().at(street.get_name()).is_closed()) streetList->addItem(qstrName);
    }
    // if there is no street to choose, open error
    if (streetList->count() == 0)
    {
        DetourDialog::show_no_street_available();
        return;
    }

    instructionLabel->setText("Select new route:");

    // we need a new slot to call
    disconnect(streetList, &QListWidget::itemClicked, this, &DetourDialog::street_selected);
    connect(streetList, &QListWidget::itemClicked, this, &DetourDialog::next_street);

}

void DetourDialog::next_street(QListWidgetItem *nextstreet)
{
    // get the street name from selected item
    QString qstreetName = nextstreet->text();
    // convert street name to std::string
    std::string streetName = qstreetName.toUtf8().constData();
    std::string closedstreetName = closedStreet.toUtf8().constData();
    // add street to detour vector
    detourStreets.push_back(strMap->get_streets().at(streetName));

    // check if selection ended - after the first iteration, if chosen street has common point with closed street
    if (firstIter == false && strMap->get_streets().at(streetName).has_common_point(strMap->get_streets().at(closedstreetName)))
    {
        // if street selection ends at the start of closed street, detour does not make sense
        if (strMap->get_streets().at(closedstreetName).get_common_point(strMap->get_streets().at(streetName)) == strMap->get_streets().at(closedstreetName).get_start())
        {
            DetourDialog::show_wrong_detour();
            return;
        }
        // set the detour of closed street as its detour vector
        strMap->get_streets().at(closedstreetName).set_detour(detourStreets);
        streetList->clear();
        // make all streets clickable again, except for closed streets and streets in detour vector
        std::vector<Street> cannotSelect = strMap->get_unselectable();
        for (const auto &str : strMap->get_streets())
        {
            QString actualStreetName = QString::fromStdString(str.second.get_name());
            if (!(std::find(cannotSelect.begin(), cannotSelect.end(), str.second) != cannotSelect.end())) streetList->addItem(actualStreetName);
        }
        // clear detour streets vector
        detourStreets.clear();
        // make new connections
        disconnect(streetList, &QListWidget::itemClicked, this, &DetourDialog::next_street);
        connect(streetList, &QListWidget::itemClicked, this, &DetourDialog::street_selected);
        // emit signal to SimModuleController
        emit on_detour_finished();
        // close window, do not execute any other code
        instructionLabel->setText("Select street to close:");
        DetourDialog::close();
        return;
    }

    // if this is not the starting ( = closed) street:
    // clear previous options
    streetList->clear();
    // show follow up streets in list view

    // if there already is a street "behind" this one in detourVector (meaning atleast 1 iteration has been made)
    if (firstIter == false)
    {
        // get name of previous street in detour street vector
        std::string laststreetName = detourStreets[detourStreets.size()-2].get_name();
        // when the common point of both streets is the actual street's start
        if(strMap->get_streets().at(streetName).get_common_point(strMap->get_streets().at(laststreetName)) ==
           strMap->get_streets().at(streetName).get_start())
        {
            // show streets that continue from this streets end
            for (const auto &street : strMap->get_follow_up_end(strMap->get_streets().at(streetName)))
            {
                QString qstrName = QString::fromStdString(street.get_name());
                // add only streets that are not closed and not in detour vector
                if (!strMap->get_streets().at(street.get_name()).is_closed() &&
                    !(std::find(detourStreets.begin(), detourStreets.end(), street) != detourStreets.end()))
                { streetList->addItem(qstrName); }
            }
        }
        else if (strMap->get_streets().at(streetName).get_common_point(strMap->get_streets().at(laststreetName)) ==
                 strMap->get_streets().at(streetName).get_end())
        {
            // show streets that continue from this streets start
            for (const auto &street : strMap->get_follow_up_start(strMap->get_streets().at(streetName)))
            {
                QString qstrName = QString::fromStdString(street.get_name());
                // add only streets that are not closed and not in detour vector
                if (!strMap->get_streets().at(street.get_name()).is_closed() &&
                    !(std::find(detourStreets.begin(), detourStreets.end(), street) != detourStreets.end()))
                { streetList->addItem(qstrName); }
            }

        }
    }

    // if this is the first iteration, we have to check on the closed street
    else if (firstIter == true)
    {
        // when the common point of both streets is the actual streets start
        if(strMap->get_streets().at(streetName).get_common_point(strMap->get_streets().at(closedstreetName)) ==
           strMap->get_streets().at(streetName).get_start())
        {
            // show streets that continue from this streets end
            for (const auto &street : strMap->get_follow_up_end(strMap->get_streets().at(streetName)))
            {
                QString qstrName = QString::fromStdString(street.get_name());
                // this is the first iteration, hide the closed street name
                if (qstrName == closedStreet) continue;
                // add only streets that are not closed and not in detour vector
                if (!strMap->get_streets().at(street.get_name()).is_closed() &&
                    !(std::find(detourStreets.begin(), detourStreets.end(), street) != detourStreets.end()))
                { streetList->addItem(qstrName); }
            }
        }
        else if (strMap->get_streets().at(streetName).get_common_point(strMap->get_streets().at(closedstreetName)) ==
                 strMap->get_streets().at(streetName).get_end())
        {
            // show streets that continue from this streets start
            for (const auto &street : strMap->get_follow_up_start(strMap->get_streets().at(streetName)))
            {
                QString qstrName = QString::fromStdString(street.get_name());
                // this is the first iteration, hide the closed street name
                if (qstrName == closedStreet) continue;
                // add only streets that are not closed and not in detour vector
                if (!strMap->get_streets().at(street.get_name()).is_closed() &&
                    !(std::find(detourStreets.begin(), detourStreets.end(), street) != detourStreets.end()))
                { streetList->addItem(qstrName); }
            }
        }
        firstIter = false;
    }
    // if there is no street to choose, open error
    if (streetList->count() == 0)
    {
        DetourDialog::show_no_street_available();
        return;
    }
    instructionLabel->setText("Select new route:");
}

// when user presses reset selection
void DetourDialog::selection_reset()
{
    streetList->clear();
    // make all streets clickable again, except for closed streets and streets already in detour
    std::vector<Street> cannotSelect = strMap->get_unselectable();
    for (const auto &str : strMap->get_streets())
    {
        QString actualStreetName = QString::fromStdString(str.second.get_name());
        if (!(std::find(cannotSelect.begin(), cannotSelect.end(), str.second) != cannotSelect.end())) streetList->addItem(actualStreetName);
    }
    // clear detour streets vector
    detourStreets.clear();
    // make new connections
    disconnect(streetList, &QListWidget::itemClicked, this, &DetourDialog::next_street);
    connect(streetList, &QListWidget::itemClicked, this, &DetourDialog::street_selected);
    // change label text so it matches the actual selection
    instructionLabel->setText("Select street to close:");
}




