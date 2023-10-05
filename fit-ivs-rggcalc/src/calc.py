#!/usr/bin/env python3
#------------------------------------------------------------------------------
# Project:              IVS - calculator
# Team:					rgg
# Authors:				Radek Veverka (xvever13)
# Filename:				calc.py
# Language:				Python, English
# Created:              11.4.2019
# Last modification:    16.4.2019
# Licence:				GPLv3
#------------------------------------------------------------------------------

##
# @mainpage IVS calculator, program documentation
# Welcome to the program documentation for our project to IVS. The calculator is written in Python. The main idea of it's architecture
# is to keep gui, math library and expression evaluator as separated classes and make them work together by program's entry class.
#
# The application looks like this:
# @image html screenshot.png
# The application consists of 4 main files:
# - @ref rgg_mathlib.py 	    	Contains our math library.
# - @ref calculator_gui.py 			Contains setup of our PyQt gui for the calculator.
# - @ref expression_evaluator.py 	Contains processing of the input from the gui and using math library to compute the results.
# - @ref calc.py 					The main entry file of the program. Instantiates classes from files above and runs the application.
#
# @date April 2019
# @author rgg
#

##
# @file calc.py
#
# @brief The entry file of the calculator.
# Connets gui, mathlib and parser together, making the calculator work.
# @author Radek Veverka (xvever13)

from calculator_gui import Main_Window
from rgg_mathlib import RggMathLib
from rgg_mathlib import InvalidOperandException
from PyQt5 import QtWidgets, QtGui, QtCore
from PyQt5.QtGui import QKeySequence
from PyQt5.QtWidgets import QMessageBox, QAction, QShortcut, QApplication
import sys
from expression_evaluator import ExpressionEvaluator 

##
# @class RggCalc
# @brief The main class of the calculator, responsible for running the whole thing.
# The application is run by instantiating this class, connects gui, library and evaluator together.
class RggCalc():
	def __init__(self):
		## Application window setup
		self.app = QtWidgets.QApplication(sys.argv)

		# Initializing submodules of the application
		## Instance of the class representing the gui.
		self.gui = Main_Window() 
		## Instance of class representing math library
		self.math_lib = RggMathLib() 
		## Instance of class representing expression evaluator
		self.expr_evaluator = ExpressionEvaluator() 

		self.gui.eq_but.clicked.connect(self.recalculate)

		self.eqa_but_shortcut = QShortcut(QKeySequence("Return"), self.gui)
		self.eqa_but_shortcut.activated.connect(self.recalculate)
		self.eqb_but_shortcut = QShortcut(QKeySequence("Enter"), self.gui)
		self.eqb_but_shortcut.activated.connect(self.recalculate)

		# Run
		sys.exit(self.app.exec_())
	
	##
	# Callback for equal button - clears result display in gui and inserts new computed value
	# Also, wraps the computation into tryexcept block and in case of error, shows the message
	#
	def recalculate(self):
		expr = self.gui.get_expression()
		self.gui.clear_result_display()
		try:
			result = self.expr_evaluator.evaluate_expression(expr)
			self.gui.append_to_result_display(str(result))
		except InvalidOperandException as ex:
			self.gui.append_to_result_display(ex.message)
		except OverflowError as ex:
			self.gui.append_to_result_display("OverflowErr")
		except Exception as ex:
			self.gui.append_to_result_display("SyntaxErr")

# Start the calculator by instantiating the main class
RggCalc()