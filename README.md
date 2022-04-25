# Arithmetic-Expression
This repository contains a working code to evaluate arithmetic expressions.
The code supports the operators +, -, *, / and the grouping (), [] and {}.
For example the expression "1 + 2*3 " will return the answer 7.

#Getting Started
#Prerequisites
The project has been developed in c# with Visual Studio 2019 for Mac.
It requires the package Nunit (3.13.3).

#Installation
- Clone this repository
- Open the project master 'ProjectCS.sln'
- Build the 2 projects 'ArithmeticParsing' and 'ArithmeticParsingTest'.
- Run the 'ArithmeticParsingTest' project to check that all unit tests are successful.

#Usage
The main code is the project 'ArithmeticParsing'.

The program required the name of a file with its path as an input.It could be provided as an argument. Alternatively, if not provided, the program will ask for it within the console window.

The file should be a csv or a text file with one arithmetic expression per line to be evaluated.
The output is displayed directly in the console window as "expression = answer".

#License
Distributed under the MIT License. See LICENSE.txt for more information.

#Contact
Christophe Le Lannou- lelannou@talktalk.net

Project Link: https://github.com/Christophe897/Arithmetic-Expression.git
