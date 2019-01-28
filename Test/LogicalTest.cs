using Core;
using Core.Blocks.Comparison;
using Core.Blocks.Constants;
using Core.Blocks.Logical;
using Core.TypeDefinitions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.DataContext;

namespace Test
{
    [TestClass]
    public class LogicalTest
    {

        [TestMethod]
        public void BasicAnd()
        {
            Formula<bool, BoolType, TestDataContext> formula = new Formula<bool, BoolType, TestDataContext>()
            {
                Operations = new And(new BoolConstant(true), new BoolConstant(true)),
            };

            Assert.IsTrue(formula.Calculate(TestDataContext.Instance));
        }

        [TestMethod]
        public void NegativeBasicAnd()
        {
            Formula<bool, BoolType, TestDataContext> formula = new Formula<bool, BoolType, TestDataContext>()
            {
                Operations = new And(new BoolConstant(false), new BoolConstant(true)),
            };

            Assert.IsFalse(formula.Calculate(TestDataContext.Instance));
        }

        [TestMethod]
        public void BasicOr()
        {
            Formula<bool, BoolType, TestDataContext> formula = new Formula<bool, BoolType, TestDataContext>()
            {
                Operations = new Or(new BoolConstant(true), new BoolConstant(false)),
            };

            Assert.IsTrue(formula.Calculate(TestDataContext.Instance));
        }

        [TestMethod]
        public void NegativeBasicOr()
        {
            Formula<bool, BoolType, TestDataContext> formula = new Formula<bool, BoolType, TestDataContext>()
            {
                Operations = new Or(new BoolConstant(false), new BoolConstant(false)),
            };

            Assert.IsFalse(formula.Calculate(TestDataContext.Instance));
        }


        [TestMethod]
        public void CompositeAndEqual()
        {
            Formula<bool, BoolType, TestDataContext> formula = new Formula<bool, BoolType, TestDataContext>()
            {
                Operations = new And(new Equal<NumericType>(new NumericConstant(3D), new NumericConstant(3D)), new BoolConstant(true)),
            };

            Assert.IsTrue(formula.Calculate(TestDataContext.Instance));
        }

        [TestMethod]
        public void CompositeOrEqual()
        {
            Formula<bool, BoolType, TestDataContext> formula = new Formula<bool, BoolType, TestDataContext>()
            {
                Operations = new Or(new Equal<NumericType>(new NumericConstant(3D), new NumericConstant(5D)), 
                                    new GreaterThan<NumericType>(new NumericConstant(5D), new NumericConstant(10D))),
            };

            Assert.IsFalse(formula.Calculate(TestDataContext.Instance));
        }
    }
}
