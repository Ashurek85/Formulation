using Core;
using Core.Blocks.Comparison;
using Core.Blocks.Conditional;
using Core.Blocks.Constants;
using Core.TypeDefinitions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.DataContext;

namespace Test
{
    [TestClass]
    public class ConditionalTest
    {

        [TestMethod]
        public void SimpleIfElse()
        {
            Formula<bool, BoolType, TestDataContext> formula = new Formula<bool, BoolType, TestDataContext>()
            {
                Operations = new IfElse<NumericType, BoolType>(new Equal<NumericType>(new NumericConstant(3D), new NumericConstant(5D)),
                                                               new BoolConstant(true),
                                                               new BoolConstant(false)),
            };

            Assert.IsFalse(formula.Calculate(TestDataContext.Instance));
        }

        [TestMethod]
        public void StringNumericIfElse()
        {
            Formula<double, NumericType, TestDataContext> formula = new Formula<double, NumericType, TestDataContext>()
            {
                Operations = new IfElse<StringType, NumericType>(new Equal<StringType>(new StringConstant("Value1"), new StringConstant("Value2")),
                                                                 new NumericConstant(10D),
                                                                 new NumericConstant(5D)),
            };

            Assert.IsTrue(formula.Calculate(TestDataContext.Instance) == 5D);
        }

    }
}
