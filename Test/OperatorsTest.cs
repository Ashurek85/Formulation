using Core;
using Core.Blocks.Constants;
using Core.Blocks.Operators;
using Core.TypeDefinitions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.DataContext;

namespace Test
{
    [TestClass]
    public class OperatorsTest
    {

        [TestMethod]
        public void Divide()
        {
            Formula<double, NumericType, TestDataContext> formula = new Formula<double, NumericType, TestDataContext>()
            {
                Operations = new Divide(new NumericConstant(10D), new NumericConstant(2D)),
            };

            Assert.IsTrue(formula.Calculate(TestDataContext.Instance) == 5D);
        }

        [TestMethod]
        public void DivideFalse()
        {
            Formula<double, NumericType, TestDataContext> formula = new Formula<double, NumericType, TestDataContext>()
            {
                Operations = new Divide(new NumericConstant(20D), new NumericConstant(2D)),
            };

            Assert.IsFalse(formula.Calculate(TestDataContext.Instance) == 8D);
        }

        [TestMethod]
        public void Multiply()
        {
            Formula<double, NumericType, TestDataContext> formula = new Formula<double, NumericType, TestDataContext>()
            {
                Operations = new Multiply(new NumericConstant(10D), new NumericConstant(2D)),
            };

            Assert.IsTrue(formula.Calculate(TestDataContext.Instance) == 20D);
        }

        [TestMethod]
        public void MultiplyFalse()
        {
            Formula<double, NumericType, TestDataContext> formula = new Formula<double, NumericType, TestDataContext>()
            {
                Operations = new Multiply(new NumericConstant(20D), new NumericConstant(2D)),
            };

            Assert.IsFalse(formula.Calculate(TestDataContext.Instance) == 10D);
        }

        [TestMethod]
        public void Pow()
        {
            Formula<double, NumericType, TestDataContext> formula = new Formula<double, NumericType, TestDataContext>()
            {
                Operations = new Pow(new NumericConstant(2D), new NumericConstant(3D)),
            };

            Assert.IsTrue(formula.Calculate(TestDataContext.Instance) == 8D);
        }

        [TestMethod]
        public void PowFalse()
        {
            Formula<double, NumericType, TestDataContext> formula = new Formula<double, NumericType, TestDataContext>()
            {
                Operations = new Pow(new NumericConstant(2D), new NumericConstant(4D)),
            };

            Assert.IsFalse(formula.Calculate(TestDataContext.Instance) == 4D);
        }

        [TestMethod]
        public void Sqrt()
        {
            Formula<double, NumericType, TestDataContext> formula = new Formula<double, NumericType, TestDataContext>()
            {
                Operations = new Sqrt(new NumericConstant(9D)),
            };

            Assert.IsTrue(formula.Calculate(TestDataContext.Instance) == 3D);
        }

        [TestMethod]
        public void SqrtFalse()
        {
            Formula<double, NumericType, TestDataContext> formula = new Formula<double, NumericType, TestDataContext>()
            {
                Operations = new Sqrt(new NumericConstant(15D)),
            };

            Assert.IsFalse(formula.Calculate(TestDataContext.Instance) == 4D);
        }

        [TestMethod]
        public void Round()
        {
            Formula<double, NumericType, TestDataContext> formula = new Formula<double, NumericType, TestDataContext>()
            {
                Operations = new Round(new NumericConstant(9.5698D), 2),
            };

            Assert.IsTrue(formula.Calculate(TestDataContext.Instance) == 9.57D);
        }

        [TestMethod]
        public void RoundFalse()
        {
            Formula<double, NumericType, TestDataContext> formula = new Formula<double, NumericType, TestDataContext>()
            {
                Operations = new Round(new NumericConstant(15.9998D), 3),
            };

            Assert.IsFalse(formula.Calculate(TestDataContext.Instance) == 15.999D);
        }

        [TestMethod]
        public void Subtract()
        {
            Formula<double, NumericType, TestDataContext> formula = new Formula<double, NumericType, TestDataContext>()
            {
                Operations = new Subtract<NumericType>(new NumericConstant(15.3D), new NumericConstant(2.3D)),
            };

            Assert.IsTrue(formula.Calculate(TestDataContext.Instance) == 13D);
        }

        [TestMethod]
        public void SubtractFalse()
        {
            Formula<double, NumericType, TestDataContext> formula = new Formula<double, NumericType, TestDataContext>()
            {
                Operations = new Subtract<NumericType>(new NumericConstant(9D), new NumericConstant(3D)),
            };

            Assert.IsFalse(formula.Calculate(TestDataContext.Instance) == 12D);
        }

        [TestMethod]
        public void Add()
        {
            Formula<double, NumericType, TestDataContext> formula = new Formula<double, NumericType, TestDataContext>()
            {
                Operations = new Add<NumericType>(new NumericConstant(15.3D), new NumericConstant(2.5D)),
            };

            Assert.IsTrue(formula.Calculate(TestDataContext.Instance) == 17.8D);
        }

        [TestMethod]
        public void AddFalse()
        {
            Formula<double, NumericType, TestDataContext> formula = new Formula<double, NumericType, TestDataContext>()
            {
                Operations = new Add<NumericType>(new NumericConstant(9D), new NumericConstant(3D)),
            };

            Assert.IsFalse(formula.Calculate(TestDataContext.Instance) == 13D);
        }

        [TestMethod]
        public void Interpolation()
        {
            Formula<double, NumericType, TestDataContext> formula = new Formula<double, NumericType, TestDataContext>()
            {
                Operations = new Interpolation(new NumericConstant(1D), 
                                               new NumericConstant(2D),
                                               new NumericConstant(3D),
                                               new NumericConstant(4D),
                                               new NumericConstant(5D)),
            };

            Assert.IsTrue(formula.Calculate(TestDataContext.Instance) == 6D);
        }

        [TestMethod]
        public void Abs()
        {
            Formula<double, NumericType, TestDataContext> formula = new Formula<double, NumericType, TestDataContext>()
            {
                Operations = new Abs(new NumericConstant(-10D)),
            };
            Assert.IsTrue(formula.Calculate(TestDataContext.Instance) == 10D);
        }
    }
}
