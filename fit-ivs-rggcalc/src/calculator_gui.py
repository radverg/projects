#------------------------------------------------------------------------------
# Project:              IVS - calculator
# Team:					rgg
# Authors:				Dominik Plachy (xplach09)
# Filename:				calculator_gui.py
# Language:				Python, English
# Created:              1.4.2019
# Last modification:    16.4.2019
# Licence:				GPLv3
#------------------------------------------------------------------------------

##
# @file calculator_gui.py
#
# @brief File creating the window for communication between application and user
# @author Dominik Plachy (xplach09)

from PyQt5 import QtWidgets, QtGui, QtCore
from PyQt5.QtGui import QKeySequence
from PyQt5.QtWidgets import QMessageBox, QAction, QShortcut, QApplication
import sys

##
# @class Main_Window
# @brief The main class of gui, creates all widgets and sets functions of buttons.
#
class Main_Window(QtWidgets.QMainWindow):
    def __init__(self, **kwargs):
        super(Main_Window, self).__init__(**kwargs)

        self.setWindowTitle("Rgg Kalkulačka")
        self.setFixedSize(300,400)
        
        # path needs to change before packaging
        self.setWindowIcon(QtGui.QIcon("./src/assets/icon_rgg.png"))

        self.init_gui()
        self.show()

    ##
    # @brief Creates displays and buttons and places them into the window. 
    # Calls functions after clicking buttons.
    #
    def init_gui(self):
        window = QtWidgets.QWidget()
        window_layout = QtWidgets.QVBoxLayout()
        window.setLayout(window_layout)

        help_layout = QtWidgets.QHBoxLayout() # small help button
        dis1_layout = QtWidgets.QHBoxLayout() # shows expression
        dis2_layout = QtWidgets.QHBoxLayout() # shows result
        butrow1_layout = QtWidgets.QHBoxLayout() # Clr, Del, (, ), Log
        butrow2_layout = QtWidgets.QHBoxLayout() # 7, 8, 9, /, !
        butrow3_layout = QtWidgets.QHBoxLayout() # 4, 5, 6, *, ^
        butrow4_layout = QtWidgets.QHBoxLayout() # 1, 2, 3, -, √
        butrow5_layout = QtWidgets.QHBoxLayout() # 0, ., +, =

        window_layout.addLayout(help_layout)
        window_layout.addStretch()
        window_layout.addLayout(dis1_layout)
        window_layout.addStretch()
        window_layout.addLayout(dis2_layout)            # creates horizontal layouts
        window_layout.addStretch()
        window_layout.addLayout(butrow1_layout)
        window_layout.addLayout(butrow2_layout)
        window_layout.addLayout(butrow3_layout)
        window_layout.addLayout(butrow4_layout)
        window_layout.addLayout(butrow5_layout)

        # definitions of buttons
        self.help_but = QtWidgets.QPushButton("?", self)
        self.help_but.setMaximumHeight(20)
        self.help_but.setMaximumWidth(20)
        self.help_but.setStyleSheet("background-color: white") 

        self.dis1_label = QtWidgets.QLabel("")
        self.dis1_label.setFont(QtGui.QFont("Times", 13, QtGui.QFont.Black))
        self.dis1_label.setAlignment(QtCore.Qt.AlignRight)

        self.dis2_label = QtWidgets.QLabel("")
        self.dis2_label.setFont(QtGui.QFont("Arial", 16, QtGui.QFont.Black))
        self.dis2_label.setAlignment(QtCore.Qt.AlignRight)
        
        self.clr_but = QtWidgets.QPushButton("CLR", self)
        self.clr_but.setMinimumHeight(50)
        self.clr_but.setMinimumWidth(50)
        self.clr_but.setStyleSheet("background-color: #ff5555")
        self.clr_but.setFont(QtGui.QFont("Arial", 11, QtGui.QFont.Black))

        self.del_but = QtWidgets.QPushButton("DEL", self)
        self.del_but.setMinimumHeight(50)
        self.del_but.setMinimumWidth(50)
        self.del_but.setStyleSheet("background-color: #ff5555")
        self.del_but.setFont(QtGui.QFont("Arial", 11, QtGui.QFont.Black))

        self.lbrac_but = QtWidgets.QPushButton("(", self)
        self.lbrac_but.setMinimumHeight(50)
        self.lbrac_but.setMinimumWidth(50)
        self.lbrac_but.setStyleSheet("background-color: #8ea5c7")
        self.lbrac_but.setFont(QtGui.QFont("Arial", 11, QtGui.QFont.Black))

        self.rbrac_but = QtWidgets.QPushButton(")", self)
        self.rbrac_but.setMinimumHeight(50)
        self.rbrac_but.setMinimumWidth(50)
        self.rbrac_but.setStyleSheet("background-color: #8ea5c7")
        self.rbrac_but.setFont(QtGui.QFont("Arial", 11, QtGui.QFont.Black))

        self.log_but = QtWidgets.QPushButton("ln", self)
        self.log_but.setMinimumHeight(50)
        self.log_but.setMinimumWidth(50)
        self.log_but.setStyleSheet("background-color: #8ea5c7")
        self.log_but.setFont(QtGui.QFont("Arial", 11, QtGui.QFont.Black))
        
        self.zero_but = QtWidgets.QPushButton("0", self)
        self.zero_but.setMinimumHeight(50)
        self.zero_but.setMinimumWidth(109)
        self.zero_but.setStyleSheet("background-color: #bfbfbf") 
        self.zero_but.setFont(QtGui.QFont("Arial", 11, QtGui.QFont.Black))

        self.one_but = QtWidgets.QPushButton("1", self)
        self.one_but.setMinimumHeight(50)
        self.one_but.setMinimumWidth(50)
        self.one_but.setStyleSheet("background-color: #bfbfbf") 
        self.one_but.setFont(QtGui.QFont("Arial", 11, QtGui.QFont.Black))

        self.two_but = QtWidgets.QPushButton("2", self)
        self.two_but.setMinimumHeight(50)
        self.two_but.setMinimumWidth(50)
        self.two_but.setStyleSheet("background-color: #bfbfbf") 
        self.two_but.setFont(QtGui.QFont("Arial", 11, QtGui.QFont.Black))

        self.three_but = QtWidgets.QPushButton("3", self)
        self.three_but.setMinimumHeight(50)
        self.three_but.setMinimumWidth(50)
        self.three_but.setStyleSheet("background-color: #bfbfbf") 
        self.three_but.setFont(QtGui.QFont("Arial", 11, QtGui.QFont.Black))

        self.four_but = QtWidgets.QPushButton("4", self)
        self.four_but.setMinimumHeight(50)
        self.four_but.setMinimumWidth(50)
        self.four_but.setStyleSheet("background-color: #bfbfbf") 
        self.four_but.setFont(QtGui.QFont("Arial", 11, QtGui.QFont.Black))

        self.five_but = QtWidgets.QPushButton("5", self)
        self.five_but.setMinimumHeight(50)
        self.five_but.setMinimumWidth(50)
        self.five_but.setStyleSheet("background-color: #bfbfbf") 
        self.five_but.setFont(QtGui.QFont("Arial", 11, QtGui.QFont.Black))

        self.six_but = QtWidgets.QPushButton("6", self)
        self.six_but.setMinimumHeight(50)
        self.six_but.setMinimumWidth(50)
        self.six_but.setStyleSheet("background-color: #bfbfbf") 
        self.six_but.setFont(QtGui.QFont("Arial", 11, QtGui.QFont.Black))

        self.seven_but = QtWidgets.QPushButton("7", self)
        self.seven_but.setMinimumHeight(50)
        self.seven_but.setMinimumWidth(50)
        self.seven_but.setStyleSheet("background-color: #bfbfbf") 
        self.seven_but.setFont(QtGui.QFont("Arial", 11, QtGui.QFont.Black))

        self.eight_but = QtWidgets.QPushButton("8", self)
        self.eight_but.setMinimumHeight(50)
        self.eight_but.setMinimumWidth(50)
        self.eight_but.setStyleSheet("background-color: #bfbfbf") 
        self.eight_but.setFont(QtGui.QFont("Arial", 11, QtGui.QFont.Black))

        self.nine_but = QtWidgets.QPushButton("9", self)
        self.nine_but.setMinimumHeight(50)
        self.nine_but.setMinimumWidth(50)
        self.nine_but.setStyleSheet("background-color: #bfbfbf") 
        self.nine_but.setFont(QtGui.QFont("Arial", 11, QtGui.QFont.Black)) 


        self.add_but = QtWidgets.QPushButton("+", self)
        self.add_but.setMinimumHeight(50) 
        self.add_but.setMinimumWidth(50)
        self.add_but.setStyleSheet("background-color: #8ea5c7")
        self.add_but.setFont(QtGui.QFont("Arial", 11, QtGui.QFont.Black))
        self.shortcut = QShortcut(QKeySequence("+"), self)
        self.shortcut.activated.connect(lambda: self.append_to_expression_display('+'))

        self.sub_but = QtWidgets.QPushButton("-", self) 
        self.sub_but.setMinimumHeight(50)
        self.sub_but.setMinimumWidth(50)
        self.sub_but.setStyleSheet("background-color: #8ea5c7")
        self.sub_but.setFont(QtGui.QFont("Arial", 11, QtGui.QFont.Black))

        self.mul_but = QtWidgets.QPushButton("✕", self)
        self.mul_but.setMinimumHeight(50)
        self.mul_but.setMinimumWidth(50)
        self.mul_but.setStyleSheet("background-color: #8ea5c7")
        self.mul_but.setFont(QtGui.QFont("Arial", 11, QtGui.QFont.Black))

        self.div_but = QtWidgets.QPushButton("÷", self)
        self.div_but.setMinimumHeight(50)
        self.div_but.setMinimumWidth(50)
        self.div_but.setStyleSheet("background-color: #8ea5c7")
        self.div_but.setFont(QtGui.QFont("Arial", 11, QtGui.QFont.Black))
        
        self.eq_but = QtWidgets.QPushButton("=", self)
        self.eq_but.setMinimumHeight(50)
        self.eq_but.setMinimumWidth(50)
        self.eq_but.setStyleSheet("background-color: #8ea5c7")
        self.eq_but.setFont(QtGui.QFont("Arial", 11, QtGui.QFont.Black))
        
        self.point_but = QtWidgets.QPushButton(".", self)
        self.point_but.setMinimumHeight(50)
        self.point_but.setMinimumWidth(50)
        self.point_but.setStyleSheet("background-color: #bfbfbf")
        self.point_but.setFont(QtGui.QFont("Arial", 11, QtGui.QFont.Black))
        
        self.pwr_but = QtWidgets.QPushButton("^", self)
        self.pwr_but.setMinimumHeight(50)
        self.pwr_but.setMinimumWidth(50)
        self.pwr_but.setStyleSheet("background-color: #8ea5c7")
        self.pwr_but.setFont(QtGui.QFont("Arial", 11, QtGui.QFont.Black))

        self.sqrt_but = QtWidgets.QPushButton("ⁿ√", self)
        self.sqrt_but.setMinimumHeight(50)
        self.sqrt_but.setMinimumWidth(50)
        self.sqrt_but.setStyleSheet("background-color: #8ea5c7")
        self.sqrt_but.setFont(QtGui.QFont("Arial", 11, QtGui.QFont.Black))

        self.fact_but = QtWidgets.QPushButton("x!", self) 
        self.fact_but.setMinimumHeight(50)
        self.fact_but.setMinimumWidth(50)
        self.fact_but.setStyleSheet("background-color: #8ea5c7")
        self.fact_but.setFont(QtGui.QFont("Arial", 11, QtGui.QFont.Black))

        # adds widgets to layouts
        help_layout.addWidget(self.help_but)
        help_layout.addStretch()

        dis1_layout.addStretch()
        dis1_layout.addWidget(self.dis1_label)
        
        dis2_layout.addStretch()
        dis2_layout.addWidget(self.dis2_label)
        
        butrow1_layout.addWidget(self.clr_but)
        butrow1_layout.addWidget(self.del_but)
        butrow1_layout.addWidget(self.lbrac_but)
        butrow1_layout.addWidget(self.rbrac_but)
        butrow1_layout.addWidget(self.log_but)

        butrow2_layout.addWidget(self.seven_but)
        butrow2_layout.addWidget(self.eight_but)
        butrow2_layout.addWidget(self.nine_but)
        butrow2_layout.addWidget(self.div_but)
        butrow2_layout.addWidget(self.fact_but)

        butrow3_layout.addWidget(self.four_but)
        butrow3_layout.addWidget(self.five_but)
        butrow3_layout.addWidget(self.six_but)
        butrow3_layout.addWidget(self.mul_but)
        butrow3_layout.addWidget(self.pwr_but)

        butrow4_layout.addWidget(self.one_but)
        butrow4_layout.addWidget(self.two_but)
        butrow4_layout.addWidget(self.three_but)
        butrow4_layout.addWidget(self.sub_but)
        butrow4_layout.addWidget(self.sqrt_but)

        butrow5_layout.addWidget(self.zero_but)
        butrow5_layout.addWidget(self.point_but)
        butrow5_layout.addWidget(self.add_but)
        butrow5_layout.addWidget(self.eq_but)

        # controlling by buttons
        self.help_but.clicked.connect(self.show_help_msg)
        self.zero_but.clicked.connect(lambda: self.append_to_expression_display('0'))
        self.one_but.clicked.connect(lambda: self.append_to_expression_display('1'))
        self.two_but.clicked.connect(lambda: self.append_to_expression_display('2'))
        self.three_but.clicked.connect(lambda: self.append_to_expression_display('3'))
        self.four_but.clicked.connect(lambda: self.append_to_expression_display('4'))
        self.five_but.clicked.connect(lambda: self.append_to_expression_display('5'))
        self.six_but.clicked.connect(lambda: self.append_to_expression_display('6'))
        self.seven_but.clicked.connect(lambda: self.append_to_expression_display('7'))
        self.eight_but.clicked.connect(lambda: self.append_to_expression_display('8'))
        self.nine_but.clicked.connect(lambda: self.append_to_expression_display('9'))
        self.point_but.clicked.connect(lambda: self.append_to_expression_display('.'))
        self.add_but.clicked.connect(lambda: self.append_to_expression_display('+'))
        self.sub_but.clicked.connect(lambda: self.append_to_expression_display('-'))
        self.mul_but.clicked.connect(lambda: self.append_to_expression_display('*'))
        self.div_but.clicked.connect(lambda: self.append_to_expression_display('/'))
        self.pwr_but.clicked.connect(lambda: self.append_to_expression_display('^'))
        self.sqrt_but.clicked.connect(lambda: self.append_to_expression_display('√'))
        self.fact_but.clicked.connect(lambda: self.append_to_expression_display('fac'))
        self.lbrac_but.clicked.connect(lambda: self.append_to_expression_display('('))
        self.rbrac_but.clicked.connect(lambda: self.append_to_expression_display(')'))
        self.log_but.clicked.connect(lambda: self.append_to_expression_display('ln'))
        self.del_but.clicked.connect(self.delete_one_char)
        self.clr_but.clicked.connect(self.clear_expression_display)
        
        # controlling by keyboard
        self.zero_but_shortcut = QShortcut(QKeySequence("0"), self)
        self.zero_but_shortcut.activated.connect(lambda: self.append_to_expression_display('0'))
        self.one_but_shortcut = QShortcut(QKeySequence("1"), self)
        self.one_but_shortcut.activated.connect(lambda: self.append_to_expression_display('1'))
        self.two_but_shortcut = QShortcut(QKeySequence("2"), self)
        self.two_but_shortcut.activated.connect(lambda: self.append_to_expression_display('2'))
        self.three_but_shortcut = QShortcut(QKeySequence("3"), self)
        self.three_but_shortcut.activated.connect(lambda: self.append_to_expression_display('3'))
        self.four_but_shortcut = QShortcut(QKeySequence("4"), self)
        self.four_but_shortcut.activated.connect(lambda: self.append_to_expression_display('4'))
        self.five_but_shortcut = QShortcut(QKeySequence("5"), self)
        self.five_but_shortcut.activated.connect(lambda: self.append_to_expression_display('5'))
        self.six_but_shortcut = QShortcut(QKeySequence("6"), self)
        self.six_but_shortcut.activated.connect(lambda: self.append_to_expression_display('6'))
        self.seven_but_shortcut = QShortcut(QKeySequence("7"), self)
        self.seven_but_shortcut.activated.connect(lambda: self.append_to_expression_display('7'))
        self.eight_but_shortcut = QShortcut(QKeySequence("8"), self)
        self.eight_but_shortcut.activated.connect(lambda: self.append_to_expression_display('8'))
        self.nine_but_shortcut = QShortcut(QKeySequence("9"), self)
        self.nine_but_shortcut.activated.connect(lambda: self.append_to_expression_display('9'))
        self.add_but_shortcut = QShortcut(QKeySequence("Plus"), self)
        self.add_but_shortcut.activated.connect(lambda: self.append_to_expression_display('+'))
        self.sub_but_shortcut = QShortcut(QKeySequence("-"), self)
        self.sub_but_shortcut.activated.connect(lambda: self.append_to_expression_display('-'))
        self.mul_but_shortcut = QShortcut(QKeySequence("*"), self)
        self.mul_but_shortcut.activated.connect(lambda: self.append_to_expression_display('*'))
        self.div_but_shortcut = QShortcut(QKeySequence("/"), self)
        self.div_but_shortcut.activated.connect(lambda: self.append_to_expression_display('/'))
        self.pointa_but_shortcut = QShortcut(QKeySequence(","), self)
        self.pointa_but_shortcut.activated.connect(lambda: self.append_to_expression_display('.'))
        self.pointb_but_shortcut = QShortcut(QKeySequence("."), self)
        self.pointb_but_shortcut.activated.connect(lambda: self.append_to_expression_display('.'))
        self.lbrac_but_shortcut = QShortcut(QKeySequence("("), self)
        self.lbrac_but_shortcut.activated.connect(lambda: self.append_to_expression_display('('))
        self.rbrac_but_shortcut = QShortcut(QKeySequence(")"), self)
        self.rbrac_but_shortcut.activated.connect(lambda: self.append_to_expression_display(')'))
        self.del_but_shortcut = QShortcut(QKeySequence("Backspace"), self)
        self.del_but_shortcut.activated.connect(self.delete_one_char)
        self.clr_but_shortcut = QShortcut(QKeySequence("Delete"), self)
        self.clr_but_shortcut.activated.connect(self.clear_expression_display)

        self.setCentralWidget(window)

    ##
    # @brief Removes the content in display that shows the calculation progress
    #
    def clear_expression_display(self):
        self.dis1_label.setText("")

    ##
    # @brief Removes the content of display that shows the result of the expression
    #
    def clear_result_display(self):
        self.dis2_label.setText("")

    ##
    # @brief Adds text to the expression display without overwriting previous content
    # @param txt The text to be appended.
    #
    def append_to_expression_display(self, txt = ""):
        label_content = self.dis1_label.text()
        self.dis1_label.setText(label_content + txt)

    ##
    # @brief Adds text to the result display without overwriting previous content
    # @param txt The text to be appended.
    #
    def append_to_result_display(self, txt = ""):
        label_content = self.dis2_label.text()
        self.dis2_label.setText(label_content + txt)

    ##
    # @brief Deletes one character on right side of expression display
    #
    def delete_one_char(self):
        label_content = self.dis1_label.text()
        self.dis1_label.setText(label_content[:(len(label_content) - 1)])

    ##
    # @brief Reads text from expression display and returns it
    # @return Content of expression display
    #
    def get_expression(self):
        return self.dis1_label.text()

    ##
    # @brief Shows error messagebox
    # @param error_text The error text
    #
    def error_message(self, error_text = ""):
        QMessageBox.critical(self, "Error", error_text)

    ##
    # @brief Shows help messagebox
    #
    def show_help_msg(self):
        QMessageBox.about(self, "Nápověda", """<font color='black'><p><b>Nápověda k aplikaci Kalkulačka:</b></p>
            <p>Tato aplikace slouží k základním numerickým výpočtům.</p>
            <p>Syntaxe zadávání výrazu je prefixová pro funkci:  <i>ln</i>, <i>fac</i></p>
            <p>Umocňování se zapisuje: <i>základ ^ exponent</i>.</p>
            <p>Odmocňování se zapisuje: <i>exponent √ základ</i>. <br>
            Exponent je automaticky číslo těsně před odmocninou. Při nezadaném exponentu je exponent automaticky 2.</p>
            <p>Pro zbytek operací platí infixové zadávání.</p>
            <p><b>Verze:</b> 1.0</p>
            <p><b>Autor:</b> RGG</p>""")
