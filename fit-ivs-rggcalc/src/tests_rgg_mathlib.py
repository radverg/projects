import unittest
import rgg_mathlib

class TestMathlib(unittest.TestCase):

	def setUp(self):
		self.mathlib = rgg_mathlib.RggMathLib()

	##
	# Test for add function. Performs test with set of numbers and also tries
	# to pass invalid arguments, expecting an exception.
	#
	def test_add(self):
		cases = [ # operand1, operand2, expected result
			[0, 0, 0],
			[1, 0, 1],
			[54, 13, 67],
			[1000000, -1000000, 0],
			[156848.98798722230, 879845.6642, 1036694.6521872223],
			[879451815198516849.856468188681, 26516848978465156189.894849848492, 2.7396300793663672e+19]
		]

		for case in cases:
			self.assertEqual(self.mathlib.add(case[0], case[1]), case[2])
		
		# Tests for invalid argumets given to the function
		self.subtest_two_invalid_operands(self.mathlib.add)

		
	##
	# Test for sub function. Performs test with set of numbers and also tries
	# to pass invalid arguments, expecting an exception.
	#
	def test_sub(self):
		cases = [ # operand1, operand2, expected result
			[0, 0, 0],
			[1, 0, 1],
			[5, 3, 2],
			[1000000, -1000000, 2000000],
			[5, -1, 6],
			[-10.5, -10.8, 0.3],
			[-1, -1, 0],
			[156848.98798722230, 879845.6642, -722996.6762127777],
			[-80000.89684, 30087.987, -110088.88384]
		]

		for case in cases:
			self.assertAlmostEqual(self.mathlib.sub(case[0], case[1]), case[2])
		
		# Tests for invalid argumets given to the function
		self.subtest_two_invalid_operands(self.mathlib.sub)

	##
	# Test for mul function. Performs test with set of numbers and also tries
	# to pass invalid arguments, expecting an exception.
	#
	def test_mul(self):
		cases = [ # operand1, operand2, expected result
			[0, 0, 0],
			[1, 0, 0],
			[5, 3, 15],
			[1000000, -1000000, -1000000000000],
			[5, -1, -5],
			[-10.5, -10.8, 113.4],
			[-1, -1, 1],
			[156848.98798722230, 879845.6642, 138002902014.71542],
		]

		for case in cases:
			self.assertEqual(self.mathlib.mul(case[0], case[1]), case[2])
		
		# Tests for invalid argumets given to the function
		self.subtest_two_invalid_operands(self.mathlib.mul)
		

	##
	# Test for div function. Performs test with set of numbers and also tries
	# to pass invalid arguments, expecting an exception.
	# Checks for exception when dividing by zero as well.
	def test_div(self):
		cases = [ # operand1, operand2, expected result
			[0, 1, 0],
			[1, 1, 1],
			[-1, -1, 1],
			[-6, 3, -2],
			[1000000, -1000000, -1],
			[67, -8, -8.375],
			[48, 150, 0.32],
			[156848.98798722230, 879845.6642, 0.17826875140634726],
		]

		for case in cases:
			self.assertEqual(self.mathlib.div(case[0], case[1]), case[2])
		
		# Tests for invalid argumets given to the function
		self.subtest_two_invalid_operands(self.mathlib.div)

		# Check division by zero
		self.assertRaises(rgg_mathlib.InvalidOperandException, self.mathlib.div, 1, 0) 

		# Inaccurate examples
		self.assertAlmostEqual(self.mathlib.div(1, 3), 0.33333333333)
		self.assertAlmostEqual(self.mathlib.div(-41, 150), -0.2733333333)


	##
	# Test for pow function. Performs test with set of numbers and also tries
	# to pass invalid arguments, expecting an exception.
	# Checks also exception raise on complex result
	def test_pow(self):
		cases = [ # operand1, operand2, expected result
			[0, 1, 0],
			[1, 1, 1],
			[0, 0, 1],
			[-6, 0, 1],

			[-1, -1, -1],
			[-6, 3, -216],
			[-6, 2, 36],

			[4, 0.5, 2],

			[54, 4.2, 18882252.081959557],
			[54, -4.2, 5.295978443987716e-08],

			[67, -8, 2.4626436805241987e-15],
			[48.23, 4.63, 62195494.15170386],
		]

		for case in cases:
			self.assertEqual(self.mathlib.pow(case[0], case[1]), case[2])
		
		# Tests for invalid argumets given to the function
		self.subtest_two_invalid_operands(self.mathlib.pow)

		# Negative number powered to float is complex - exception expected
		self.assertRaises(rgg_mathlib.InvalidOperandException, self.mathlib.pow, -54, 4.2)

	##
	# Test for factorial function. Performs test with set of numbers and also tries
	# to pass invalid arguments, expecting an exception.
	# Also checks if exception is raised on negative and float numbers
	def test_factorial(self):
		cases = [ # operand1, expected result
			[0, 1],
			[1, 1],
			[2, 2],
			[3, 6],
			[4, 24],
			[10, 3628800],
			[15, 1307674368000],
			[50, 30414093201713378043612608166064768844377641568960512000000000000],
			[100, 93326215443944152681699238856266700490715968264381621468592963895217599993229915608941463976156518286253697920827223758251185210916864000000000000000000000000]
		]

		for case in cases:
			self.assertEqual(self.mathlib.factorial(case[0]), case[1])
		
		# Invalid type of operand
		self.assertRaises(rgg_mathlib.InvalidOperandException, self.mathlib.factorial, "a")
		self.assertRaises(rgg_mathlib.InvalidOperandException, self.mathlib.factorial, list())
		self.assertRaises(rgg_mathlib.InvalidOperandException, self.mathlib.factorial, "")
		self.assertRaises(rgg_mathlib.InvalidOperandException, self.mathlib.factorial, "50")
		self.assertRaises(rgg_mathlib.InvalidOperandException, self.mathlib.factorial, None)

		# Invalid factorial numbers - negative and floats
		self.assertRaises(rgg_mathlib.InvalidOperandException, self.mathlib.factorial, -2)
		self.assertRaises(rgg_mathlib.InvalidOperandException, self.mathlib.factorial, -48.87)
		self.assertRaises(rgg_mathlib.InvalidOperandException, self.mathlib.factorial, 45.65)
		self.assertRaises(rgg_mathlib.InvalidOperandException, self.mathlib.factorial, 100.1)

	def test_root(self):
		cases = [ # operand_base, operand_x expected result
			[2, 4, 2],
			[3, 27, 3],
			[2.5, 5, 1.9036539387158786],
			[-2.5, 5, 0.5253055608807534],
			[1, 6.6, 6.6],
			[3, -27, -3],
			[-3, 27, 0.333333333],
			[-3, -27, -0.333333333],
			[10, 1000000, 3.981071706],
			[2, 100, 10],
			[4, 128, 3.363585661],
		]

		for case in cases:	
			self.assertAlmostEqual(self.mathlib.root(case[0], case[1]), case[2])

		# Complex results
		self.assertRaises(rgg_mathlib.InvalidOperandException, self.mathlib.root, 0, 2)
		self.assertRaises(rgg_mathlib.InvalidOperandException, self.mathlib.root, 2, -4)
		self.assertRaises(rgg_mathlib.InvalidOperandException, self.mathlib.root, 6, -4)

		# Invalid type of operands
		self.subtest_two_invalid_operands(self.mathlib.root)

	def test_ln(self):
		cases = [ # operand_base, operand_x expected result
			[1, 0],
			[2, 0.6931471806],
			[3, 1.098612289],
			[0.5, -0.6931471806],
			[2.36, 0.858661619],
			[100, 4.605170186],
			[6969, 8.849227021],
			[6464979798653, 29.497421],
		]

		for case in cases:	
			self.assertAlmostEqual(self.mathlib.ln(case[0]), case[1], places=4)

		# Invalid type of operand
		self.assertRaises(rgg_mathlib.InvalidOperandException, self.mathlib.factorial, "a")
		self.assertRaises(rgg_mathlib.InvalidOperandException, self.mathlib.factorial, list())
		self.assertRaises(rgg_mathlib.InvalidOperandException, self.mathlib.factorial, "")
		self.assertRaises(rgg_mathlib.InvalidOperandException, self.mathlib.factorial, "50")
		self.assertRaises(rgg_mathlib.InvalidOperandException, self.mathlib.factorial, None)

		# Invalid values for ln
		self.assertRaises(rgg_mathlib.InvalidOperandException, self.mathlib.ln, 0)
		self.assertRaises(rgg_mathlib.InvalidOperandException, self.mathlib.ln, -1)
		self.assertRaises(rgg_mathlib.InvalidOperandException, self.mathlib.ln, -549.8)

	def subtest_two_invalid_operands(self, callable):
		self.assertRaises(rgg_mathlib.InvalidOperandException, callable, "abc", 3)
		self.assertRaises(rgg_mathlib.InvalidOperandException, callable, list(), None)
		self.assertRaises(rgg_mathlib.InvalidOperandException, callable, "abc", "def")
		self.assertRaises(rgg_mathlib.InvalidOperandException, callable, "1", "1")
		self.assertRaises(rgg_mathlib.InvalidOperandException, callable, set(), tuple())
		self.assertRaises(rgg_mathlib.InvalidOperandException, callable, -3, "bcd")

if __name__ == "__main__":
	unittest.main()
