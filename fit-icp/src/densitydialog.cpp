/**
 * @file densitydialog.cpp
 * @author xsysmi00 (Michal Sýs)
 * @date May 2020
 *
 * ICP project, VUT FIT
 *
 * Contains implementation of DensityDialog class.
*/
#include "densitydialog.h"
#include <QDialog>
#include <QHBoxLayout>
#include <QPushButton>
#include <QTextEdit>

DensityDialog::DensityDialog(StreetMap* strMap)
{
    this->strMap=strMap;

    setWindowTitle("Traffic Density Options");
    // hide question mark button
    Qt::WindowFlags flags(Qt::WindowTitleHint);
    this->setWindowFlags(flags);

    // label with instructions
    instructionLabel = new QLabel("Select street and update density:");
    instructionLabel->setStyleSheet("font-weight: bold; color:blue");

    // create ComboBox with names of loaded streets
    comboBox = new QComboBox;
    for (const auto& str : strMap->get_streets())
    {
        comboBox->addItem(QString::fromStdString(str.second.get_name()));
    }

    // create LineEdit for setting traffic density, connect with each street's density
    line = new QLineEdit;

    // set default line text
    QString comboText = comboBox->currentText();
    // convert QString to std::string
    std::string comboStreetName = comboText.toUtf8().constData();
    // find street by given name, get density, set it as default for lineEdit
    Street &comboStreet = strMap->get_streets().at(comboStreetName);
    double comboStreetDensity = comboStreet.get_density();
    QString defaultLineText = QString::number(comboStreetDensity);
    line->setText(defaultLineText);

    // connect slots
    connect(comboBox, &QComboBox::currentTextChanged, this, &DensityDialog::show_street_density);

    // create buttons
    buttonBox = new QDialogButtonBox(QDialogButtonBox::Ok | QDialogButtonBox::Cancel);
    buttonBox->button(QDialogButtonBox::Ok)->setText("Confirm");
    connect(buttonBox, &QDialogButtonBox::accepted, this, &QDialog::accept);
    connect(buttonBox, &QDialogButtonBox::rejected, this, &QDialog::reject);

    // set layouts
    QHBoxLayout* subLayout = new QHBoxLayout;
    subLayout->addWidget(comboBox);
    subLayout->addWidget(line);

    QVBoxLayout* mainLayout = new QVBoxLayout;
    mainLayout->addWidget(instructionLabel);
    mainLayout->addLayout(subLayout);
    mainLayout->addWidget(buttonBox);

    setLayout(mainLayout);
}

void DensityDialog::accept()
{
    QString text = line->text();
    double newDensity = text.toDouble();
    // when user chooses negative or way too big numbers (max density cap = 5)
    if ( newDensity <= 1 ) newDensity = 1;
    else if ( newDensity > 5 ) newDensity = 5;
    QString qcurrentStreetName = comboBox->currentText();
    std::string currentStreetName = qcurrentStreetName.toUtf8().constData();
    // find street by given name, set new density
    Street &currentStreet = strMap->get_streets().at(currentStreetName);
    currentStreet.set_density(newDensity);

    this->close();
}

void DensityDialog::show_street_density(QString strname)
{
    // convert QString to std::string, find a street with this name
    std::string currentStreetName = strname.toUtf8().constData();
    Street currentStreet = strMap->get_streets().at(currentStreetName);
    // get density of this street, convert to QString, display
    double streetDensity = currentStreet.get_density();
    line->setText(QString::number(streetDensity));
}
