using System;
using NUnit.Framework;
<<<<<<< Updated upstream
using Xamarin.UITest;
using Xamarin.UITest.iOS;
using Xamarin.UITest.Queries;
=======
>>>>>>> Stashed changes
using ArithmeticParsing.Properties.Parsing;

namespace ArithmeticParsingTest
{
    /// <summary>
    /// Tests for class ParsingInteger
    /// </summary>
    [TestFixture]
    public class ParsingIntegerTest
    {
        /// <summary>
        /// Tests for class ParsingInteger
        /// </summary>
        [Test]
        public void CheckSimpleResult()
        {
            string expression = "1";
            NumberInt i1 = ParsingInteger.Evaluate(expression);
            Assert.IsTrue(i1.ToString() == "1");

            expression = "-1";
            i1 = ParsingInteger.Evaluate(expression);
            Assert.IsTrue(i1.ToString() == "-1");

            expression = "2 + 3 *5";
            i1 = ParsingInteger.Evaluate(expression);
            Assert.IsTrue(i1.ToString() == "17");
        }

        /// <summary>
        /// Tests brakets for class ParsingInteger
        /// </summary>
        [Test]
        public void CheckResultBrackets()
        {
            string expression = "()";
            NumberInt i1 = ParsingInteger.Evaluate(expression);
            Assert.IsTrue(i1.IsNaN);

            expression = "(-1)";
            i1 = ParsingInteger.Evaluate(expression);
            Assert.IsTrue(i1.ToString() == "-1");

            expression = "(-1)-(-1)";
            i1 = ParsingInteger.Evaluate(expression);
            Assert.IsTrue(i1.ToString() == "0");

            expression = "(-1)*(-1)";
            i1 = ParsingInteger.Evaluate(expression);
            Assert.IsTrue(i1.ToString() == "1");

            expression = "(-1)/(-1)";
            i1 = ParsingInteger.Evaluate(expression);
            Assert.IsTrue(i1.ToString() == "1");

            expression = "(-1)*(5-7)";
            i1 = ParsingInteger.Evaluate(expression);
            Assert.IsTrue(i1.ToString() == "2");
        }

        /// <summary>
        /// Tests incorrect formula
        /// </summary>
        [Test]
        public void CheckIncorrectFormula()
        {
            string expression = "3**2";
            NumberInt i1 = ParsingInteger.Evaluate(expression);
            Assert.IsTrue(i1.IsNaN);

            expression = "1 / 0";
            i1 = ParsingInteger.Evaluate(expression);
            Assert.IsTrue(i1.IsNaN);

            expression = "+ (-1 * (5 - 7)";
            i1 = ParsingInteger.Evaluate(expression);
            Assert.IsTrue(i1.IsNaN);

            expression = "3(2 + 5)";
            i1 = ParsingInteger.Evaluate(expression);
            Assert.IsTrue(i1.IsNaN);

            expression = "(2 - 5) 3";
            i1 = ParsingInteger.Evaluate(expression);
            Assert.IsTrue(i1.IsNaN);

            expression = "(2 + 5) 3";
            i1 = ParsingInteger.Evaluate(expression);
            Assert.IsTrue(i1.IsNaN);

            expression = "3(2 - 5)";
            i1 = ParsingInteger.Evaluate(expression);
            Assert.IsTrue(i1.IsNaN);

            expression = "a + b";
            i1 = ParsingInteger.Evaluate(expression);
            Assert.IsTrue(i1.IsNaN);

            expression = "sqrt(4)";
            i1 = ParsingInteger.Evaluate(expression);
            Assert.IsTrue(i1.IsNaN);

            expression = "1.2 + 2";
            i1 = ParsingInteger.Evaluate(expression);
            Assert.IsTrue(i1.IsNaN);

            expression = "1,2 + 3";
            i1 = ParsingInteger.Evaluate(expression);
            Assert.IsTrue(i1.IsNaN);

            expression = "4 % 2";
            i1 = ParsingInteger.Evaluate(expression);
            Assert.IsTrue(i1.IsNaN);
        }

        /// <summary>
        /// Tests double operators
        /// </summary>
        [Test]
        public void CheckDoubleOperators()
        {
            Assert.IsTrue(ParsingInteger.Evaluate("2+ +2").IsNaN);
            Assert.IsTrue(ParsingInteger.Evaluate("2 + -2").ToString() == "0");
            Assert.IsTrue(ParsingInteger.Evaluate("2 + *2").IsNaN);
            Assert.IsTrue(ParsingInteger.Evaluate("2 + / 2").IsNaN);
            Assert.IsTrue(ParsingInteger.Evaluate("2 - +2").IsNaN);
            Assert.IsTrue(ParsingInteger.Evaluate("2- -2").ToString() == "4");
            Assert.IsTrue(ParsingInteger.Evaluate("2 - *2").IsNaN);
            Assert.IsTrue(ParsingInteger.Evaluate("2 - / 2").IsNaN);
            Assert.IsTrue(ParsingInteger.Evaluate("2 * +2").IsNaN);
            Assert.IsTrue(ParsingInteger.Evaluate("2 * -2").ToString() == "-4");
            Assert.IsTrue(ParsingInteger.Evaluate("2 * *2").IsNaN);
            Assert.IsTrue(ParsingInteger.Evaluate("2 * / 2").IsNaN);
            Assert.IsTrue(ParsingInteger.Evaluate("2 / +2").IsNaN);
            Assert.IsTrue(ParsingInteger.Evaluate("2 / -2").ToString() == "-1");
            Assert.IsTrue(ParsingInteger.Evaluate("2 / * 2").IsNaN);
            Assert.IsTrue(ParsingInteger.Evaluate("2 / / 2").IsNaN);
        }
    }
}
