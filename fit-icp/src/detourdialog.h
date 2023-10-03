/**
 * @file detourdialog.h
 * @author xsysmi00 (Michal Sýs)
 * @date May 2020
 *
 * ICP project, VUT FIT
 *
 * Contains declaration of DetourDialog class.
*/
#ifndef DETOURWIDGET_H
#define DETOURWIDGET_H

#include <QDialog>
#include <QDialogButtonBox>
#include <QListWidget>
#include <QLabel>
#include "streetmap.h"

/**
 * @brief The DetourDialog class
 *
 * This class guides user through making of detours on rendered map.
 * Detour dialog is opened with push button in mainwindow and linearly progresses through:
 * - Selection of which street to close
 * - Selection of streets that mark the new route to the closed street.
 *
 * After the selection is made, the detour is set as closed street's attribute.
 * User also has an option to reset the detour selection or close the window.
 */
class DetourDialog : public QDialog
{
    Q_OBJECT

private:
    /**
     * @brief Button box with two options: Reset the detour selection or Close the window.
     */
    QDialogButtonBox* buttonBox;
    /**
     * @brief List with all the streets to choose.
     */
    QListWidget* streetList;
    /**
     * @brief A map with all the streets (and stops, which this class doesn't use).
     */
    StreetMap* strMap;
    /**
     * @brief Label with instructions, guides user through the process.
     */
    QLabel* instructionLabel;
    /**
     * @brief Name of the closed street.
     *
     * In the implementation of the class, this is mainly needed to check if the selection loop ended.
     */
    QString closedStreet;
    /**
     * @brief Marks the first iteration of street selection
     */
    bool firstIter = true;
    /**
     * @brief Ordered container of streets that user selected for detour.
     *
     * When the selection ends, this will be set as "detour" atribute to closed street.
     */
    std::vector<Street> detourStreets;
    /**
     * @brief Opens up an error message when there is no available street to choose from.
     *
     * Also resets the selection.
     */
    void show_no_street_available();
    /**
     * @brief Opens up an error message when user connected the detour to the point where it started from.
     *
     * When street selection ends at the start of closed street, detour does not make sense.
     * Also resets the selection.
     */
    void show_wrong_detour();

private slots:
    /**
     * @brief Handles the selection after a street has been closed.
     * @param canceledstreet Selected item from QListWidget (QListWidgetItem)
     */
    void street_selected(QListWidgetItem* canceledstreet);
    /**
     * @brief Handles the selection of streets that will make the detour, works in a loop.
     * @param nextstreet Selected item from QListWidget (QListWidgetItem)
     */
    void next_street(QListWidgetItem* nextstreet);

public slots:
    /**
     * @brief Called whenever there has to be a selection reset made.
     *
     * It has to be made public because it is also used in SimModuleController.
     *
     * @see SimModuleController
     */
    void selection_reset();

public:
    /**
     * @brief Creates a detour dialog with the selection of streets in StreetMap.
     *
     * @param strMap A map with all the streets and stops (StreetMap)
     */
    DetourDialog(StreetMap* strMap);

signals:
    /**
     * @brief Sends a signal to SimModuleController after finishing the detour selection.
     *
     * @see SimModuleController
     */
    void on_detour_finished();

};

#endif // DETOURWIDGET_H
