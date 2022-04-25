using System;
using System.Collections;
using System.Collections.Generic;

namespace ArithmeticParsing.Properties.Parsing
{
    /// <summary>
    /// A static class to manage the Evaluation of an arithmetic expression into a number
    /// </summary>
    public static class ParsingInteger
    {
        static List<char> _nextChar = new List<char>() { '+', '-', '*', '/', ')', ']', '}' };
        static List<char> _previousChar = new List<char>() { '+', '-', '*', '/', '(', '[', '{' };

        #region Main public function: Evaluate
        /// <summary>
        /// The Main public function to evaluate a string arithmetic formula
        /// </summary>
        public static NumberInt Evaluate(string inputExpression)
        {
            if (inputExpression is null)
            {
                return null;
            }
            string newInputExpression = inputExpression.Trim();

            if (newInputExpression == "")
            {
                return new NumberInt();
            }

            // First evalate the expressin with each grouping {}, [], or ()
            if (!EvaluateGrouping(newInputExpression, '{', '}', out newInputExpression, out string newComments1))
            {
                return new NumberInt(newComments1);
            }
            if (!EvaluateGrouping(newInputExpression, '[', ']', out newInputExpression, out string newComments2))
            {
                return new NumberInt(newComments2);
            }
            if (!EvaluateGrouping(newInputExpression, '(', ')', out newInputExpression, out string newComments3))
            {
                return new NumberInt(newComments3);
            }

            //Manage the operators from the lowest priority
            //Each operator +,-,*,/ has 2 arguments
            if (newInputExpression.IndexOf('+') > -1)
            {
                return ManageAdditions(newInputExpression);
            }
            else if (newInputExpression.IndexOf('-') > -1)
            {
                return ManageSubstractions(newInputExpression);
            }
            else if (newInputExpression.IndexOf('*') > -1)
            {
                return ManageMultiplications(newInputExpression);
            }
            else if (newInputExpression.IndexOf('/') > -1)
            {
                return ManageDivisions(newInputExpression);
            }

            //If the expression has none of the previous operator,
            //we can try to parse it into an intger
            if (int.TryParse(newInputExpression, out int result))
            {
                return new NumberInt(result);
            }
            return new NumberInt(string.Format("TryParse failed '{0}'", newInputExpression));
        }
        #endregion

        #region Operators
        /// <summary>
        /// To Manage Operator +
        /// </summary>
        private static NumberInt ManageAdditions(string input)
        {
            int pos = input.LastIndexOf("+");
            if (pos == -1)
            {
                return Evaluate(input);
            }
            //Special case if the operator is in first position
            if (pos == 0)
            {
                return Evaluate(input.Substring(1));
            }
            string leftArgument = input.Substring(0, pos);
            string rightArgument = input.Substring(pos + 1).Trim();
            if (rightArgument == "")
            {
                return new NumberInt(string.Format("Invalid Addition '{0}", input));
            }
            return Evaluate(leftArgument) + Evaluate(rightArgument);
        }

        /// <summary>
        /// To Manage Operator -
        /// if windows == -1, looks for '-' on the all expression
        /// otherwise looks for '-' on the first 'windows' characters
        /// </summary>
        private static NumberInt ManageSubstractions(string input, int windows = -1)
        {
            int pos = ((windows == -1) ? input : input.Substring(0, windows)).LastIndexOf("-");
            if (pos == -1)
            {
                return Evaluate(input);
            }
            //Special case if the operator is in first position
            if (pos == 0)
            {
                return Evaluate(input.Substring(1)).Opposite();
            }
  
            string leftArgument = input.Substring(0, pos).Trim();
            char lastSymbol = leftArgument[leftArgument.Length - 1];
            string rightArgument = input.Substring(pos + 1).Trim();

            if (rightArgument == "")
            {
                return new NumberInt(string.Format("Invalid Substraction '{0}", input));
            }
            if (lastSymbol == '+')
            {
                // sign '+-' equivalent to '-'
                leftArgument = leftArgument.Substring(0, leftArgument.Length - 1);
            }
            else if (lastSymbol == '-')
            {
                leftArgument = leftArgument.Substring(0, leftArgument.Length - 1);
                // 2 signs '-' equivalent to an addition
                return Evaluate(leftArgument) + Evaluate(rightArgument);
            }
            else if (lastSymbol == '*' || lastSymbol == '/')
            {
                // signs '*-' or '/-' which means a multiplication/division by a negative number
                if (leftArgument.LastIndexOf('-') > - 1)
                {
                    return ManageSubstractions(input, pos - 1);
                }
                //They are no grouping, addition or substraction in our expression input
                return Evaluate(leftArgument + rightArgument).Opposite();
            }
            return Evaluate(leftArgument) - Evaluate(rightArgument);
        }

        /// <summary>
        /// To Manage Operator *
        /// </summary>
        private static NumberInt ManageMultiplications(string input)
        {
            int pos = input.LastIndexOf("*");
            if (pos == -1)
            {
                return Evaluate(input);
            }
            //Special case if the operator is in first position
            if (pos == 0)
            {
                return new NumberInt(string.Format("Invalid multiplication '{0}'", input));
            }

            string leftArgument = input.Substring(0, pos);
            string rightArgument = input.Substring(pos + 1);
            if (rightArgument == "")
            {
                return new NumberInt(string.Format("Invalid Multiplication '{0}", input));
            }
            return Evaluate(leftArgument) * Evaluate(rightArgument);
        }

        /// <summary>
        /// To Manage Operator /
        /// </summary>
        private static NumberInt ManageDivisions(string input)
        {
            int pos = input.LastIndexOf("/");
            if (pos == -1)
            {
                return Evaluate(input);
            }
            //Special case if the operator is in first position
            if (pos == 0)
            {
                return new NumberInt(string.Format("Invalid division '{0}'", input));
            }

            string leftArgument = input.Substring(0, pos);
            string rightArgument = input.Substring(pos + 1).Trim();
            if (rightArgument == "")
            {
                return new NumberInt(string.Format("Invalid Division '{0}", input));
            }
            return Evaluate(leftArgument) / Evaluate(rightArgument);
        }
        #endregion

        #region Grouping
        /// <summary>
        /// To evaluate inside a specific grouping
        /// </summary>
        private static bool EvaluateGrouping(string input, char openGrouping, char closeGrouping,
            out string newExpression, out string newComments)
        {
            //Find the first occurence of the closing bracket
            int pos2 = input.IndexOf(closeGrouping);
            if (pos2 == -1)
            {
                int pos = input.LastIndexOf(openGrouping);
                if (pos == -1)
                {
                    //Unchanged expression
                    newComments = "";
                    newExpression = input;
                    return true;
                }
                newComments = "Missing " + closeGrouping;
                newExpression = "";
                return false;
            }

            //Find the last occurence of the opening bracket before our first closing bracket
            int pos1 = input.Substring(0, pos2).LastIndexOf(openGrouping);
            if (pos1 == -1)
            {
                newComments = "Missing " + openGrouping;
                newExpression = "";
                return false;
            }

            //Evaluate the expression within the 2 brackets
            string strExpressionToEvaluate = input.Substring(pos1 + 1, pos2 - pos1 - 1);
            NumberInt result = Evaluate(strExpressionToEvaluate);
            if (result.IsNaN)
            {
                newComments = string.Format("Invalid expression '{0}' | {1}", strExpressionToEvaluate, result.ErrorMessage);
                newExpression = "";
                return false;
            }

            //Return a new expression having the expression within the 2 brackets by its value
            string strPreGrouping = input.Substring(0, pos1).Trim();
            string strPostGRouping = input.Substring(pos2 + 1).Trim();

            if (strPreGrouping.Length > 0)
            {
                //Check the character just before the grouping
                char lastSymbol = strPreGrouping[strPreGrouping.Length - 1];
                if (!_previousChar.Contains(lastSymbol))
                {
                    newComments = string.Format("Unexpected symbol '{0}' before the grouping '{2} {1} {3}'",
                            lastSymbol, strExpressionToEvaluate, openGrouping, closeGrouping);
                    newExpression = "";
                    return false;
                }
                //Specific situation where the evaluation of the grouping is a negative number
                //The negative sign can be confused with the substraction
                if (result < (NumberInt)0)
                {
                    switch (lastSymbol)
                    {
                        case '+':
                            // If the symbol just before the grouping is '+', it can be removed
                            return EvaluateGrouping(strPreGrouping.Substring(0, strPreGrouping.Length - 1) + result.ToString()
                                + strPostGRouping, openGrouping, closeGrouping, out newExpression, out newComments);
                        case '-':
                            // If the symbol just before the grouping is '-',
                            return EvaluateGrouping(strPreGrouping.Substring(0, strPreGrouping.Length - 1) + "+" + result.Opposite().ToString()
                                + strPostGRouping, openGrouping, closeGrouping, out newExpression, out newComments);
                        case '*':
                        case '/':
                            // The situation where the symbol just before the grouping is '*' or '/' will be managedd
                            // within the private function ManageSubstractions
                            break;
                    }
                }
            }
            if (strPostGRouping.Length > 0)
            {
                //Check the character just before the grouping
                char nextSymbol = strPostGRouping[0];
                if (!_nextChar.Contains(nextSymbol))
                {
                    newComments = string.Format("Unexpected symbol '{0}' after the grouping '{2} {1} {3}'",
                            nextSymbol, strExpressionToEvaluate, openGrouping, closeGrouping);
                    newExpression = "";
                    return false;
                }
            }
            return EvaluateGrouping(strPreGrouping + result.ToString() + strPostGRouping,
                openGrouping, closeGrouping, out newExpression, out newComments);

        }
        #endregion

    }
}
