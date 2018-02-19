
using System;
using NUnit.Framework;
using TDD.Calculator;

namespace TDD.Tests
{
    [TestFixture]
    public class SimpleCalculatorTests
    {
        [Test]
        public void Add_EmptyString()
        {
            var numbers = "";
            var calculator = new SimpleCalculator();
            Assert.AreEqual(0, calculator.Add(numbers));
        }

        [Test]
        public void Add_SingleNumber()
        {
            var numbers = "1";
            var calculator = new SimpleCalculator();
            Assert.AreEqual(1, calculator.Add(numbers));
        }

        [Test]
        public void Add_1And2()
        {
            var numbers = "1,2";
            var calculator = new SimpleCalculator();
            Assert.AreEqual(1+2, calculator.Add(numbers));
        }

        [Test]
        public void Add_Many()
        {
            var numbers = "1,2,7,3,10";
            var calculator = new SimpleCalculator();
            Assert.AreEqual(1+2+7+3+10, calculator.Add(numbers));
        }

        [Test]
        public void Add_WithNewLineDelimeter()
        {
            var numbers = "1\n2\n3\n777";
            var calculator = new SimpleCalculator();
            Assert.AreEqual(1 + 2 + 3 + 777, calculator.Add(numbers));
        }

        [Test]
        public void Add_WithTwoDelimeters()
        {
            var numbers = "1\n2,3";
            var calculator = new SimpleCalculator();
            Assert.AreEqual(1+2+3, calculator.Add(numbers));
        }

        [Test]
        public void Add_WithDifferentDelimiter_Percent()
        {
            var numbers = "//%\n2%555%7";
            var calculator = new SimpleCalculator();
            Assert.AreEqual(2+555+7, calculator.Add(numbers));
        }

        [Test]
        public void Add_WithDifferentDelimiter_Semicolon()
        {
            var numbers = "//;\n2;22;44";
            var calculator = new SimpleCalculator();
            Assert.AreEqual(2 + 22 + 44, calculator.Add(numbers));
        }

        [Test]
        public void Add_ExceptionIsThrownIfNegativeNumber()
        {
            var numbers = "//;\n1;-2;66;4";
            var calculator = new SimpleCalculator();
            Assert.Throws<Exception>(() => calculator.Add(numbers));
        }

        [Test]
        public void Add_ExceptionWithListOfNegativeNumbersIsThrownIfNegativeNumber()
        {
            var numbers = "//;\n1;-2;55;-33;-4";
            var calculator = new SimpleCalculator();
            var expectedException = new Exception();

            try
            {
                calculator.Add(numbers);
            }
            catch (Exception e)
            {
                expectedException = e;
            }

            Assert.AreEqual("Negatives are not allowed: -2, -33, -4", expectedException.Message);
        }

        [Test]
        public void Add_NumbersBiggerThan1000AreIgnored_Zero()
        {
            var numbers = "//;\n0;1001";
            var calculator = new SimpleCalculator();
            Assert.AreEqual(0, calculator.Add(numbers));
        }

        [Test]
        public void Add_NumbersAbove1000AreIgnored_OneNumber()
        {
            var numbers = "//;\n2;1001";
            var calculator = new SimpleCalculator();
            Assert.AreEqual(2, calculator.Add(numbers));
        }

        [Test]
        public void Add_NumbersAbove1000AreIgnored_ManyNumbers()
        {
            var numbers = "//;\n1;1000;444;1001;9999";
            var calculator = new SimpleCalculator();
            Assert.AreEqual(1+1000+444, calculator.Add(numbers));
        }

        [Test]
        public void Add_AnyLengthDelimiter_SingleDelimiter()
        {
            var numbers = "//[—]\n22—1—1000—444—1001—9999";
            var calculator = new SimpleCalculator();
            Assert.AreEqual(22 + 1 + 1000 + 444, calculator.Add(numbers));
        }

        [Test]
        public void Add_AnyLengthDelimiter_MultiCharsDelimiter()
        {
            var numbers = "//[==]\n22==1==1000==444==1001==9999";
            var calculator = new SimpleCalculator();
            Assert.AreEqual(22+1+1000+444, calculator.Add(numbers));
        }

        [Test]
        public void Add_MultipleDelimeters_SingleCharsDelimiters()
        {
            var numbers = "//[;][.][=][+][#][@][*][&]\n1.2*99*43*888@444";
            var calculator = new SimpleCalculator();
            Assert.AreEqual(1+2+99+43+888+444, calculator.Add(numbers));
        }

        [Test]
        public void Add_MultipleDelimeters_MultiCharsDelimiters()
        {
            var numbers = "//[==][***][/]\n1/1000***400==1001/9999";
            var calculator = new SimpleCalculator();
            Assert.AreEqual(1+1000+400, calculator.Add(numbers));
        }
    }
}