#------------------------------------------------------------------------------
# Project:              IVS - calculator
# Team:					rgg
# Authors:				Radek Veverka (xvever13)
# Filename:				expression_evaluator.py
# Language:				Python, English
# Created:              13.4.2019
# Last modification:    14.4.2019
# Licence:				GPLv3
#------------------------------------------------------------------------------

##
# @file expression_evaluator.py
#
# @brief Contains class for computing the math expressions represented as string
# @author Radek Veverka (xvever13)

import re
import rgg_mathlib

##
# @class ExpressionEvaluator
# @brief The ExpressionEvaluator is able to compute given string containing math expression
# Use it's only public method evaluate_expression(expr)
# Other methods are for internal purposes
# @note For parsing and evaluating string, this class uses regexes a lot, hence importing re module
class ExpressionEvaluator:
	def __init__(self):
		## The instance of our math library, used for computations
		self.math_lib = rgg_mathlib.RggMathLib() 

	##
	# Computes the math expression in a string.
	# Uses RggMathLib to compute
	# Format example: '6+3^2-fac(3)+(3+2)*7'
	# Supported operators: +, -, *, ÷, ^, √, fac, ln
	# This method potentially throws exceptions (format error, math error), always call it in trycatch block
	# @public
	# @param expr Expression to evaluate.
	# @return The resulting number as string
	def evaluate_expression(self, expr):
		# Start evaluating by this preprocessing:
		expr = re.sub(" ", "", expr) # Removing spaces from expression
		if (len(expr) == 0):
			return ""
		expr = re.sub("-([0-9]*)(\^|√)", r'-1*\1\2', expr) # Example: -2^3 -> -1*2^3
		expr = re.sub("([^0-9\)])√", r'\g<1>2√', expr) # Set implicit root as square root: 2*√9 -> 2*2√9
		expr = re.sub("([0-9])(ln|fac)", r"\1*\2", expr) # Implicit multiplication: 5ln(4) -> 5*ln(4)
		expr = re.sub("^√", r'2√', expr) # Implicit square root for these at the beginning of expression
		expr = re.sub("\)\(", ")*(", expr) # Insert * between brackets if not present
		expr = re.sub("([0-9])\(", r"\1*(", expr) # Insert * between bracket and number eg. 4(5+2) -> 4*(5+2)
		expr = re.sub("\)([0-9])", r")*\1", expr) # Insert * between bracket and number eg. 4(5+2) -> 4*(5+2)

		# We need to work with pseudo-signs as normal would collide with numbers in exponent format (1549e-15)
		expr = expr.replace("+", "p")
		expr = expr.replace("-", "m")

		exprResult = self._evaluate_brackets(expr)
		exprResult = self.to_number(exprResult)
		return exprResult

	##
	# @brief Validates usage of brackets
	# Validates if every bracket is closed and if every bracket has start somewhere
	# @private
	# @param expr Expresion to evaluate
	# @throw InvalidOperandException Wrong usage of brackets
	# @return The resulting number as string
	def _evaluate_brackets(self, expr):
		indented = 0
		ended = 0
		for c in expr:
			if (c == '('):
				indented += 1
			if (c == ')'):
				ended += 1
			if (indented < ended):
				raise rgg_mathlib.InvalidOperandException("BrcketErr", "Wrong usage of brackets")

		if (indented > ended):
			raise rgg_mathlib.InvalidOperandException("BrcketErr", "Wrong parenthesis")
		else:
			return self._eval_bracketed_expression(expr, indented)

	##
	# @brief Removes brackets
	# Calls _eval_non_bracket_expression on every substring starting from the deepest indented parenthesis
	# @private
	# @param expr Expresion to evaluate
	# @param indented Number of brackets
	# @return Final result
	def _eval_bracketed_expression(self, expr, indented):
		for i in range(indented):						# for every bracketed expresion
			start_at = 0
			end_at = 0
			brackets = 0
			
			for c in expr:									# search for start bracket and count position
				if (c == '('):								#	 and count to last bracket
					brackets += 1
					
				if ( brackets == indented-i ):				# if we are at start of last bracket
					for c in expr[start_at+1:]:				#	 count to end of bracket
						if (c == ')'):
							break
						end_at += 1
					expr = re.sub("\([^\(\)]*\)", self._eval_non_bracket_expression(expr[start_at+1:start_at+end_at+1]), expr)
					break

				start_at += 1

		return self._eval_non_bracket_expression(expr)
		

	##
	# Computes the math expression in a string
	# Cannot parse expression with brackets
	# @private
	# @param expr Expression to evaluate.
	# @return The resulting number as string
	def _eval_non_bracket_expression(self, expr):
		expr = self._normalize_pseudo_signs(expr)
		expr = self._eval_priority1(expr)
		expr = self._eval_priority2(expr)
		expr = self._eval_priority3(expr)
		return expr

	##
	# Helper method that takes string, removes part between given indices and replaces it 
	# by new given string. Quite usable for evaluating expressions.
	# @private
	# @param text The string to perform cutting and replacing on.
	# @param replacing_by The string that is to be inserted
	# @param start Index indicating where to start cutting
	# @param end Index indicating where to stop cutting
	# @return The resulting string
	def _insert_str_between_indices(self, text, replacing_by, start, end):
		return text[:start] + str(replacing_by) + text[end:]

	##
	# Simplifies the expression by removing reduntant + and - signs (they are represeted as p and m instead)
	# Applies folowing changes:
	# All "--" to "+"
	# Then all multiple pluses ("+++") to a single plus
	# Then all "+-" and "-+" to "-"
	# @private
	# @param expr String with an expression to be processed
	# @return Expression (string) with normalized signs
	def _normalize_pseudo_signs(self, expr):
		expr = re.sub("mm", "p", expr)
		expr = re.sub("p{2,}", "p", expr)
		expr = re.sub("(pm|mp)", "m", expr)
		if (len(expr) > 0 and expr[0] == "p"):
			expr = expr[1:]
		return expr
				
	##
	# The first part of the expression evaluating process.
	# This computes operations with the highest priority:
	# pow, root, ln, fac
	# Inserts the results to the expression for further processing, replacing the operator and operands.
	# Evaluates from right to left
	# @private
	# @param expr An expression to perform evaluating on
	# @returns {string} Expression with inserted results of computed subexpressions
	#
	def _eval_priority1(self, expr):
		while (True):  # Do this until sub_expr_list contains something (the condition is inside the loop)
			# Let's find all pow, root, ln, fac functions and their operands and select the LAST (we go from right to left) for further processing 
			sub_expr_list = re.findall("(?=(m{0,1}[0-9.e+-]+(?:\^|√)m{0,1}[0-9.e+-]+|(?:ln|fac)m{0,1}[0-9.e+-]*))", expr)
			sub_expr_list.reverse()
			if (len(sub_expr_list) == 0):
				break
			sub_expr = sub_expr_list[0]
			start = expr.rfind(sub_expr)
			end = start + len(sub_expr)

			# Extract operands and compute the result via our math library
			if ("ln" in sub_expr):
				result = self.math_lib.ln(self.to_number(sub_expr[2:]))
			elif ("fac" in sub_expr):
				result = self.math_lib.factorial(self.to_number(sub_expr[3:]))
			elif ("^" in sub_expr):
				operands = sub_expr.split("^")
				result = self.math_lib.pow(self.to_number(operands[0]), self.to_number(operands[1]))
			elif ("√" in sub_expr):
				operands = sub_expr.split("√")
				result = self.math_lib.root(self.to_number(operands[0]), self.to_number(operands[1]))

			# Replace this subexpression by the result
			expr = self._insert_str_between_indices(expr, self.to_pseudo_str(result), start, end)	
		return expr

	##
	# The second part of the expression evaluating process.
	# This computes operations with the middle priority:
	# *, ÷
	# Inserts the results to the expression for further processing, replacing the operator and operands.
	# Evaluates from left to right
	# @private
	# @param expr An expression to perform evaluating on
	# @returns {string} Expression with inserted results of computed subexpressions
	def _eval_priority2(self, expr):
		while (True):  # Do this until sub_expr_list contains something (the condition is inside the loop)
			# Let's find all * and / operators and their operands and select the first for further processing
			sub_expr_list = re.findall("(m{0,1}[0-9.e+-]*(?:\*|/)m{0,1}[0-9.e+-]*)", expr)
			if (len(sub_expr_list) == 0):
				break
			sub_expr = sub_expr_list[0]
			start = expr.find(sub_expr)
			end = start + len(sub_expr)

			# Extract operands and compute the result via our math library
			if ("*" in sub_expr):
				operands = sub_expr.split("*")
				result = self.math_lib.mul(self.to_number(operands[0]), self.to_number(operands[1]))
			elif ("/" in sub_expr):
				operands = sub_expr.split("/")				
				result = self.math_lib.div(self.to_number(operands[0]), self.to_number(operands[1]))
			# These operations can consume the sign - add it in case result is positive
			if (result >= 0):
				result = "p" + str(result)
			
			# Replace this subexpression by the result
			expr = self._insert_str_between_indices(expr, self.to_pseudo_str(result), start, end)		
		return expr

	##
	# The last part of the expression evaluating process.
	# This computes operations with the lowest priority:
	# +, -
	# Inserts the results to the expression for further processing, replacing the operator and operands.
	# Evaluates from left to right
	# @private
	# @param expr An expression to perform evaluating on
	# @returns {string} Expression with inserted results of computed subexpressions
	def _eval_priority3(self, expr):
		expr = self._normalize_pseudo_signs(expr)
		# Minus signs are painful, do a little trick here
		# Change expressions like -8-7-9 to -8+-7+-9 so add function can be used
		expr = re.sub(r'([0-9])m([0-9])', r'\1pm\2', expr)
		while (True): # Do this until sub_expr_list contains something (the condition is inside the loop)
			# Let's find all + operators and their operands and select the first for further processing
			sub_expr_list = re.findall(r"(m{0,1}[0-9.e+-]*pm{0,1}[0-9.e+-]*)", expr)
			if (len(sub_expr_list) == 0):
				break
			sub_expr = sub_expr_list[0]
			start = expr.find(sub_expr)
			end = start + len(sub_expr)

			# Extract operands and compute the result via our math library
			if ("p" in sub_expr):
				operands = sub_expr.split("p")
				result = self.math_lib.add(self.to_number(operands[0]), self.to_number(operands[1]))

			# Replace this subexpression by the result
			expr = self._insert_str_between_indices(expr, self.to_pseudo_str(result), start, end)		
		return expr

	##
	# Converts the string to number, if possible, returns int, otherwise float
	# Handles numbers represented with pseudo-sign as well
	# @throw ValueError On invalid string format
	# @public
	# @param num_txt String to be parsed
	# @return Float or integer instance
	def to_number(self, num_txt):
		num_txt = self._to_normal_sign(num_txt)
		if ("." in num_txt):
			return float(num_txt)
		else:
			return int(num_txt)

	##
	# @brief Converts the number to string with pseudo-sign.
	# If you need standard conversion, use to_normal_str() instead
	# @public
	# @param num Number to be converted
	# @return String instance of the number
	def to_pseudo_str(self, num):
		str_num = str(num)
		return self._to_pseudo_sign(str_num)

	##
	# @brief Converts the number to string with normal sign.
	# @public
	# @param num Number to be converted
	# @return String instance of the number
	def to_normal_str(self, num):
		str_num = str(num)
		return self._to_normal_sign(str_num)

	## 
	# @brief Replaces the sign of given number (in string form) to pseudo-sign (p, m) if necessary
	# Example: -4186 -> m4186, +420 -> p420
	# @private
	# @param num_str The number to process
	# @return Number with replaced sign as string
	def _to_pseudo_sign(self, num_str):
		if len(num_str) < 1:
			return num_str
		if (num_str[0] == "+"):
			return "p" + num_str[1:]
		if (num_str[0] == "-"):
			return "m" + num_str[1:]
		return num_str
	
	## 
	# @brief Replaces the pseudo sign (p, m) of the given number (in string form) to normal-sign (+, -) if necessary
	# Example: m4186 -> -4186, p420 -> +420
	# @private
	# @param num_str The number to process
	# @return Number with replaced sign as string
	def _to_normal_sign(self, num_str):
		if len(num_str) < 1:
			return num_str
		if (num_str[0] == "p"):
			return num_str[1:]
		if (num_str[0] == "m"):
			return "-" + num_str[1:]
		return num_str

ExpressionEvaluator()