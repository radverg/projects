#------------------------------------------------------------------------------
# Project:              IVS - calculator
# Team:					rgg
# Authors:				Adam Sedmík (xsedmi04)
# Filename:				rgg_mathlib.py
# Language:				Python, English
# Created:              31.3.2019
# Last modification:    14.4.2019
# Licence:				GPLv3
#------------------------------------------------------------------------------

##
# @file rgg_mathlib.py
# Math library for rgg calculator
#
# This math library contains basic mathematical operations (+, -, *, /)
# Also some advanced operations like power, factorial, nth root and natural logaritm
# This library is made only python basic functions without importing other libraries

##
# @class RggMathLib
# @brief Class defining functions for math library
#
class RggMathLib():
	
	##
	# Function that validates if operand is a number
	# @private
	#
	# @note This function is used in every functions to check all their incoming parameters
	#
	# @param operand Variable to check 
	# @return Returns True if operand is a int or float
	#
	def __valid_operand(self, operand):
		return (isinstance(operand, int) or isinstance(operand, float))

	##
	# Function to add two numbers
	#
	# @param operand1 First number to be added
	# @param operand2 Second number to be added
	# @return Two added numbers together
	#
	def add(self, operand1, operand2):
		if (self.__valid_operand(operand1) and self.__valid_operand(operand2)):
			return operand1 + operand2	#CALCULATION
		else:
			raise InvalidOperandException("OperandErr","Invalid operand is not of valid instance float or int") 

	##
	# Function to subtract two numbers
	#
	# @param operand1 Number to be subtracked from
	# @param operand2 Number to subtrack
	# @return Subtraction of two numbers
	#
	def sub(self, operand1, operand2):
		if (self.__valid_operand(operand1) and self.__valid_operand(operand2)):
			return operand1 - operand2	#CALCULATION
		else:
			raise InvalidOperandException("OperandErr","Invalid operand is not of valid instance float or int") 

	##
	# Function to multiply
	#
	# @param operand1 First number to be be multiplied
	# @param operand2 Second number to be multiplied
	# @return Multiplication of two numbers together
	#
	def mul(self, operand1, operand2):
		if (self.__valid_operand(operand1) and self.__valid_operand(operand2)):
			return operand1 * operand2	#CALCULATION
		else:
			raise InvalidOperandException("OperandErr","Invalid operand is not of valid instance float or int") 

	##
	# Function to divide two numbers
	#
	# @note Checks for division by zero
	#
	# @param operand1 Divisor
	# @param operand2 Dividend 
	# @return Division of two numbers
	#
	def div(self, operand1, operand2):
		if (self.__valid_operand(operand1) and self.__valid_operand(operand2)):
			if (operand2 != 0):
				return operand1 / float(operand2)	#CALCULATION
			else:
				raise InvalidOperandException("ZeroDivErr","Dividing by zero not permitted") 	
		else:
			raise InvalidOperandException("OperandErr","Invalid operand is not of valid instance float or int") 

	##
	# Takes number to a power
	#
	# @note Checks if power would be valid real number
	#
	# @param operand1 Number to take to power
	# @param operand2 Power
	# @return Number taken to a power
	#
	def pow(self, operand1, operand2):
		if ((self.__valid_operand(operand1) and self.__valid_operand(operand2))):
			if (not ((operand1 < 0) and not operand2 % 1 == 0)):
				return operand1 ** operand2	#CALCULATION
			else:
				raise InvalidOperandException("ComplexErr","Negative number to float power is complex number") 
		else:
			raise InvalidOperandException("OperandErr","Invalid operand is not of valid instance float or int") 

	##
	# Makes factorial of a number
	#
	# @note Checks if number is positive integer
	#
	# @param operand1 Number to make factorial of
	# @return Factorial of a number
	#
	def factorial(self, operand1):
		if (isinstance(operand1,int) and operand1 >= 0):
			if (operand1 == 0):
				return 1	#CALCULATION for zero
			else:
				c = 1;				#CALCULATION
				for x in range(1, operand1):	#
					c = c * (x + 1)		#
				return c			#
		else:
			raise InvalidOperandException("FactorialErr","Factorial number must be be positive integer") 

	##
	# Makes nth root of a number
	#
	# @note Checks if root would be valid real number 
	#
	# @param operand_base Base of root, number to take root of
	# @param operand_x Root to take of base
	# @return Given nth of base 
	#
	def root(self, operand_base, operand_x):
		if ((self.__valid_operand(operand_base) and self.__valid_operand(operand_x))):
			if (operand_x < 0 and operand_base % 2 == 1):
				return -(( -operand_x ) ** ( 1.0 / operand_base ))	#CALCULATION for odd base
			else:
				if (operand_base == 0 or (operand_x < 0 and operand_base%2 == 0)):
					raise InvalidOperandException("ComplexErr","Even integer to negative power is complex number") 
				else:
					return operand_x ** ( 1.0 / operand_base )	#CALCULATION for even base
		else:
			raise InvalidOperandException("OperandErr","Invalid operand is not of valid instance float or int") 
		
	##
	# Calulates natural logarithm of a number
	#
	# @note Calculation with equation lim(n->inf) = n*(x^(1/n)-1)
	#
	# @param operand_x Number to take natural logarithm of
	# @return Natural logaritm of number
	#
	def ln(self, operand_x):
		if (self.__valid_operand(operand_x)):
			if (operand_x > 0):
				##
				# Accucuracy for natural logaritm calculation
				#
				acc = 100000000
				return acc * (( operand_x ** ( 1.0 / acc )) - 1)	#CALCULATION
			else:
				raise InvalidOperandException("LnErr","Logarithm must be larger than zero") 
		else:
			raise InvalidOperandException("OperandErr","Invalid operand is not of valid instance float or int") 

##
# @class InvalidOperandException
# @brief Custom exception for invalid operations
#
class InvalidOperandException(Exception):
	
	##
	# Initilizaton of exception
	#
	# @param message Message of exception 
	# @param description The detailed description of the exception
	#
	def __init__(self, message, description):
		self.message = message
		self.description = description


# END OF FILE rgg_mathlib.py #
