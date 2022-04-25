using System;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.iOS;
using Xamarin.UITest.Queries;
using ArithmeticParsing.Properties.Parsing;

namespace ArithmeticParsingTest
{

    /// <summary>
    /// Tests for class NumberInt
    /// </summary>
    [TestFixture]
    public class NumberIntTest
    {
        /// <summary>
        /// Test Functions ==
        /// </summary>
        [Test]
        public void CompareTest()
        {
            NumberInt i1 = new NumberInt(1);

            NumberInt i2 = null;
            Assert.IsFalse(i1 == i2);

            NumberInt i3 = new NumberInt(i1);
            Assert.IsTrue(i1 == i3);
            Assert.IsFalse(i1 != i3);

            i3 = new NumberInt(4);
            Assert.IsFalse(i1 == i3);
            Assert.IsTrue(i1 != i3);

            NumberInt i4 = new NumberInt("Division by 0");
            NumberInt i5 = new NumberInt(i4);
            Assert.IsTrue(i4 == i5);
            Assert.IsFalse(i4 != i5);

            i5 = new NumberInt();
            Assert.IsFalse(i4 == i5);
            Assert.IsFalse(i4 != i5);
        }

        /// <summary>
        /// Tests for operators +
        /// </summary>
        [Test]
        public void AdditionsTest()
        {
            NumberInt i1 = new NumberInt(1);
            NumberInt i2 = new NumberInt (10);
            NumberInt i3 = i1 + i2;

            Assert.AreEqual(i3.ToString(), "11");

            NumberInt i4 = new NumberInt();
            NumberInt i5 = i1 + i4;
            Assert.IsTrue(i5.IsNaN);
            Assert.IsTrue(i5.ErrorMessage == "No Input");
        }

        /// <summary>
        /// Tests for operators -
        /// </summary>
        [Test]
        public void SubstractionsTest()
        {
            NumberInt i1 = new NumberInt(1);
            NumberInt i2 = new NumberInt(10);
            NumberInt i3 = i1 - i2;

            Assert.AreEqual(i3.ToString(), "-9");

            NumberInt i4 = new NumberInt();
            NumberInt i5 = i1 - i4;
            Assert.IsTrue(i5.IsNaN);
            Assert.IsTrue(i5.ErrorMessage == "No Input");
        }

        /// <summary>
        /// Tests for operators +
        /// </summary>
        [Test]
        public void MultiplicationsTest()
        {
            NumberInt i1 = new NumberInt(2);
            NumberInt i2 = new NumberInt(10);
            NumberInt i3 = i1 * i2;

            Assert.AreEqual(i3.ToString(), "20");

            NumberInt i4 = new NumberInt();
            NumberInt i5 = i1 - i4;
            Assert.IsTrue(i5.IsNaN);
            Assert.IsTrue(i5.ErrorMessage == "No Input");
        }

        /// <summary>
        /// Tests for operators +
        /// </summary>
        [Test]
        public void DivisionsTest()
        {
            NumberInt i1 = new NumberInt(40);

            NumberInt i2 = new NumberInt(10);
            NumberInt i3 = i1 / i2;
            Assert.AreEqual(i3.ToString(), "4");

            NumberInt i4 = new NumberInt(0);
            NumberInt i5 = i1 / i4;
            Assert.IsTrue(i5.IsNaN);
            Assert.IsTrue(i5.ErrorMessage == "Division by 0");

            NumberInt i6 = new NumberInt();
            NumberInt i7 = i1 / i6;
            Assert.IsTrue(i7.IsNaN);
            Assert.IsTrue(i7.ErrorMessage == "No Input");
        }

        /// <summary>
        /// Tests function Opposite
        /// </summary>
        [Test]
        public void OppositeTest()
        {
            NumberInt i1 = new NumberInt(40);
            NumberInt i2 = i1.Opposite();
            Assert.AreEqual(i2.ToString(), "-40");


            NumberInt i3 = new NumberInt();
            NumberInt i4 = i3.Opposite();
            Assert.IsTrue(i4.IsNaN);
            Assert.IsTrue(i4.ErrorMessage == "No Input");
        }
    }
}
