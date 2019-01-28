using Core;
using Core.Blocks.Comparison;
using Core.Blocks.Constants;
using Core.TypeDefinitions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.DataContext;

namespace Test
{
    [TestClass]
    public class ComparisonTest
    {
        [TestMethod]
        public void EqualBoolTrue()
        {
            Formula<bool, BoolType, TestDataContext> formula = new Formula<bool, BoolType, TestDataContext>()
            {
                Operations = new Equal<BoolType>(new BoolConstant(true), new BoolConstant(true)),
            };

            Assert.IsTrue(formula.Calculate(TestDataContext.Instance));
        }

        [TestMethod]
        public void EqualBoolFalse()
        {
            Formula<bool, BoolType, TestDataContext> formula = new Formula<bool, BoolType, TestDataContext>()
            {
                Operations = new Equal<BoolType>(new BoolConstant(true), new BoolConstant(false)),
            };

            Assert.IsFalse(formula.Calculate(TestDataContext.Instance));
        }

        [TestMethod]
        public void CompositeEqualBool()
        {
            Formula<bool, BoolType, TestDataContext> formula = new Formula<bool, BoolType, TestDataContext>()
            {
                Operations = new Equal<BoolType>(new BoolConstant(true), new Equal<BoolType>(new BoolConstant(false), new BoolConstant(false))),
            };

            Assert.IsTrue(formula.Calculate(TestDataContext.Instance));
        }

        [TestMethod]
        public void NotEqualBoolTrue()
        {
            Formula<bool, BoolType, TestDataContext> formula = new Formula<bool, BoolType, TestDataContext>()
            {
                Operations = new NotEqual<BoolType>(new BoolConstant(false), new BoolConstant(true)),
            };

            Assert.IsTrue(formula.Calculate(TestDataContext.Instance));
        }

        [TestMethod]
        public void NotEqualBoolFalse()
        {
            Formula<bool, BoolType, TestDataContext> formula = new Formula<bool, BoolType, TestDataContext>()
            {
                Operations = new NotEqual<BoolType>(new BoolConstant(false), new BoolConstant(false)),
            };

            Assert.IsFalse(formula.Calculate(TestDataContext.Instance));
        }

        [TestMethod]
        public void CompositeNotEqualBool()
        {
            Formula<bool, BoolType, TestDataContext> formula = new Formula<bool, BoolType, TestDataContext>()
            {
                Operations = new NotEqual<BoolType>(new BoolConstant(false), new NotEqual<BoolType>(new BoolConstant(true), new BoolConstant(false))),
            };

            Assert.IsTrue(formula.Calculate(TestDataContext.Instance));
        }


        [TestMethod]
        public void GreaterThanTrue()
        {
            Formula<bool, BoolType, TestDataContext> formula = new Formula<bool, BoolType, TestDataContext>()
            {
                Operations = new GreaterThan<NumericType>(new NumericConstant(50D), new NumericConstant(10D)),
            };

            Assert.IsTrue(formula.Calculate(TestDataContext.Instance));
        }

        [TestMethod]
        public void GreaterThanFalse()
        {
            Formula<bool, BoolType, TestDataContext> formula = new Formula<bool, BoolType, TestDataContext>()
            {
                Operations = new GreaterThan<NumericType>(new NumericConstant(100D), new NumericConstant(600D)),
            };

            Assert.IsFalse(formula.Calculate(TestDataContext.Instance));
        }

        [TestMethod]
        public void GreaterThanOrEqualTrue()
        {
            Formula<bool, BoolType, TestDataContext> formula = new Formula<bool, BoolType, TestDataContext>()
            {
                Operations = new GreaterThanOrEqual<NumericType>(new NumericConstant(50D), new NumericConstant(50D)),
            };

            Assert.IsTrue(formula.Calculate(TestDataContext.Instance));
        }

        [TestMethod]
        public void GreaterThanOrEqualFalse()
        {
            Formula<bool, BoolType, TestDataContext> formula = new Formula<bool, BoolType, TestDataContext>()
            {
                Operations = new GreaterThanOrEqual<NumericType>(new NumericConstant(100D), new NumericConstant(600D)),
            };

            Assert.IsFalse(formula.Calculate(TestDataContext.Instance));
        }

        [TestMethod]
        public void LessThanTrue()
        {
            Formula<bool, BoolType, TestDataContext> formula = new Formula<bool, BoolType, TestDataContext>()
            {
                Operations = new LessThan<NumericType>(new NumericConstant(3D), new NumericConstant(10D)),
            };

            Assert.IsTrue(formula.Calculate(TestDataContext.Instance));
        }

        [TestMethod]
        public void LessThanFalse()
        {
            Formula<bool, BoolType, TestDataContext> formula = new Formula<bool, BoolType, TestDataContext>()
            {
                Operations = new LessThan<NumericType>(new NumericConstant(5D), new NumericConstant(4D)),
            };

            Assert.IsFalse(formula.Calculate(TestDataContext.Instance));
        }

        [TestMethod]
        public void LessThanOrEqualTrue()
        {
            Formula<bool, BoolType, TestDataContext> formula = new Formula<bool, BoolType, TestDataContext>()
            {
                Operations = new LessThanOrEqual<NumericType>(new NumericConstant(50D), new NumericConstant(50D)),
            };

            Assert.IsTrue(formula.Calculate(TestDataContext.Instance));
        }

        [TestMethod]
        public void LessThanOrEqualFalse()
        {
            Formula<bool, BoolType, TestDataContext> formula = new Formula<bool, BoolType, TestDataContext>()
            {
                Operations = new LessThanOrEqual<NumericType>(new NumericConstant(100D), new NumericConstant(50D)),
            };

            Assert.IsFalse(formula.Calculate(TestDataContext.Instance));
        }
    }
}
