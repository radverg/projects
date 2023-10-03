/**
 * @file densitydialog.h
 * @author xsysmi00 (Michal Sýs)
 * @date May 2020
 *
 * ICP project, VUT FIT
 *
 * Contains declaration of DensityDialog class.
*/
#ifndef DENSITYDIALOG_H
#define DENSITYDIALOG_H

#include <QDialog>
#include <QLineEdit>
#include <QDialogButtonBox>
#include <QComboBox>
#include <QLabel>
#include "streetmap.h"

/**
 * @brief The DensityDialog class
 *
 * This class shows a dialog used to set traffic density of any street in rendered map.
 *
 * The dialog is opened with push button in mainwindow.
 * User then selects a street and sets the density by writing a number into line edit, then
 * confirms his decision by clicking the Confirm button.
 *
 * Density is then set as an attribute of the chosen street.
 */
class DensityDialog : public QDialog
{
    Q_OBJECT

private:
    /**
     * @brief A button box with two options: Confirm the selection or Close the window
     */
    QDialogButtonBox* buttonBox;
    /**
     * @brief Combo box with selection of all the streets in street map
     */
    QComboBox* comboBox;
    /**
     * @brief Line edit that shows selected street's density, also used for user input.
     */
    QLineEdit* line;
    /**
     * @brief A map with all the streets (and stops, which this class doesn't use).
     */
    StreetMap* strMap;
    /**
     * @brief Label with instructions.
     */
    QLabel* instructionLabel;

private slots:
    /**
     * @brief Overriden slot of QDialog class, saves data when user confirms the selection.
     *
     * The density taken from user input is set as a "traff_density" attribute of the currently selected street.
     */
    void accept() override;
    /**
     * @brief Displays current street's density onto the line edit.
     * @param strname Name of the currently selected street from combo box (QString)
     */
    void show_street_density(QString strname);

public:
    /**
     * @brief Creates a density dialog with the selection of streets in StreetMap.
     * @param strMap A map with all the streets and stops (StreetMap)
     */
    DensityDialog(StreetMap* strMap);

};

#endif // DENSITYDIALOG_H
