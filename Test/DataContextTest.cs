using Core;
using Core.Blocks.DataContext;
using Core.TypeDefinitions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.DataContext;

namespace Test
{
    [TestClass]
    public class DataContextTest
    {

        [TestMethod]
        public void BasicDataReading()
        {
            // Name of department
            Formula<string, StringType, TestDataContext> formula = new Formula<string, StringType, TestDataContext>()
            {
                Operations = new ReadData<StringType>(nameof(TestDataContext.Department))
            };

            Assert.IsTrue(formula.Calculate(TestDataContext.Instance) == "IT");
        }

        [TestMethod]
        public void NavigateAndDataReading()
        {
            // Boss email
            Formula<string, StringType, TestDataContext> formula = new Formula<string, StringType, TestDataContext>()
            {
                Operations = new DataNavigation<StringType>(nameof(TestDataContext.Boss), new ReadData<StringType>(nameof(Person.Email))),
            };

            Assert.IsTrue(formula.Calculate(TestDataContext.Instance) == "wyatt.horton@itcompany.com");
        }

    }
}
