Algebra
 Algebra(Arithmetic Expression) Parse Library for .NET
Version 1.1
---

What's this?
 This is the library for algebra (arithmetic expression) parsing library for .NET.
 This allows you to parse simply expression.

Requirements
 This requires .NET Framework 4 Client Profile.

Usage
Add Reference
 1. Add this project file to your solution.
 2. Add reference to the project "Algebra" to your project that you want to use this.
 3. Additionally, add "using Algebra;" to your code.

Use Library
 This library provides the class to calculate.
 Use Calculator class
 1. Give the expression string to parse to the constructor of Calculator class.
    You can also give the parameters at this time.
 2. Call Calculator.Calculate method, or Calculator.CalculateRange method.

License
 This library is available under MIT License.
---
Copyright (c) 2011 mayth

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"),
to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software,
and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
---

Reference for Calculator class
 --- Constructor ---
	Calculator(string expression)
 Summary: Initialize Calculator class with the expression string.
 Parameters:
  expression: The expression string.
 Exceptions:
  ArgumentNullException: <expression> is null, empty string, or contains only whitespaces.


	Calculator(string expression, IDictionary<char, double> parameter)
 Summary: Initialize Calculator class with the expression string and the parameter dictionary.
 Parameters:
  expression: The expression string.
  parameter: The dictionary of parameter.
 Exceptions:
  ArgumentNullException: <expression> is null, empty string, or contains only whitespaces.

 --- Public Properties ---
	IDictonary<char, double> Parameter
 Summary: Gets/Sets the dictionary of paramters.

 --- Public Methods ---
	void SetExpression(string expression)
 Summary: Change expression that was set to this instance.
 Parameters:
  expression: The expression string.
 Exceptions:
  ArgumentNullException: <expression> is null, empty string, or contains only whitespaces.

	bool CheckParameter(ICollection<char> parameter)
 Summary: Check whether <parameter> contains all of characters to calculate the expression.
 Parameters:
  parameter: The dictionary of parameter.
 Returns:
  If <parameter> contains all of characters to calculate the expression, this returns true; otherwise, false.

	ICollection<char> GetCharacters()
 Summary: Get all characters to calculate the expression.
 Returns:
  The collection of characters that is needed to calculate the expression.
  If no characters are needed, this returns an empty collection.

	double Calculate(double x)
 Summary: Calculate the expression.
 Parameters:
  x: The value that substitutes for "x" in the expression.
 Returns:
  A calculated value.

	IDictionary<double, double> CalculateRange(Range domain)
 Summary: Calculate the expression.
 Parameters:
  domain: The domain of value.
 Returns:
  A dictionary of calculation result.
  Key is a variable("x"), Value is a calculation result when substitute Key for "x" in the expression.


Reference for Range class

 Summary: The class that represents a domain.
 Range class implements IEnumerable<double>

 --- Constructor ---
	Range()
 Summary: Initialize Range class that represents empty range.

	Range(double from, double to)
 Summary: Initialize Range class with the value of beginning of range, the value of ending of range, and default step value.
 Parameters:
  from: The beginning value of the range.
  to: The ending value of the range.

	Range(double from, double to, double step)
 Summary: Initialize Range class with the value of beginning of range, the value of ending of range, and the step value.
 Parameters:
  from: The beginning value of the range.
  to: The ending value of the range.
  step: The step value.

 --- Public Methods ---
	IEnumerator<double> GetEnumerator()
 Summary: Get enumerator.

	IEnumerator IEnumerable.GetEnumerator()
 Summary: Get enumerator.

 --- Public Static Methods ---
	bool IsEmpty(Range range)
 Summary: Check whether <range> represents empty range or not.
 Parameters:
  range: The Range object to check.
 Returns:
  If <range> represents empty range, this returns true; otherwise, false.
