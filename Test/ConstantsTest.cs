using Core;
using Core.Blocks.Constants;
using Core.TypeDefinitions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.DataContext;

namespace Test
{
    [TestClass]
    public class ConstantsTest
    {
        [TestMethod]
        public void TrueConstant()
        {
            Formula<bool, BoolType, TestDataContext> formula = new Formula<bool, BoolType, TestDataContext>()
            {
                Operations = new BoolConstant(true),
            };

            Assert.IsTrue(formula.Calculate(TestDataContext.Instance));
        }

        [TestMethod]
        public void FalseConstant()
        {
            Formula<bool, BoolType, TestDataContext> formula = new Formula<bool, BoolType, TestDataContext>()
            {
                Operations = new BoolConstant(false),
            };

            Assert.IsFalse(formula.Calculate(TestDataContext.Instance));
        }
    }
}
