using Core;
using Core.Blocks.Collections;
using Core.Blocks.Comparison;
using Core.Blocks.Constants;
using Core.Blocks.DataContext;
using Core.TypeDefinitions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.DataContext;

namespace Test
{
    [TestClass]
    public class CollectionsTest
    {

        [TestMethod]
        public void Max()
        {
            Formula<double, NumericType, TestDataContext> formula = new Formula<double, NumericType, TestDataContext>()
            {
                Operations = new DataNavigation<NumericType>(nameof(TestDataContext.Subordinates), new Max<NumericType>(nameof(Person.RemainingVacation))),
            };

            Assert.IsTrue(formula.Calculate(TestDataContext.Instance) == 21D);
        }

        [TestMethod]
        public void Avg()
        {
            Formula<double, NumericType, TestDataContext> formula = new Formula<double, NumericType, TestDataContext>()
            {
                Operations = new DataNavigation<NumericType>(nameof(TestDataContext.Subordinates), new Average<NumericType>(nameof(Person.RemainingVacation))),
            };

            Assert.IsTrue(formula.Calculate(TestDataContext.Instance) == 15.333333333333334D);
        }

        [TestMethod]
        public void GeoMean()
        {
            Formula<double, NumericType, TestDataContext> formula = new Formula<double, NumericType, TestDataContext>()
            {
                Operations = new DataNavigation<NumericType>(nameof(TestDataContext.Subordinates), new GeometricMean<NumericType>(nameof(Person.RemainingVacation))),
            };

            Assert.IsTrue(formula.Calculate(TestDataContext.Instance) == 14.658972088782376D);
        }

        [TestMethod]
        public void Min()
        {
            Formula<double, NumericType, TestDataContext> formula = new Formula<double, NumericType, TestDataContext>()
            {
                Operations = new DataNavigation<NumericType>(nameof(TestDataContext.Subordinates), new Min<NumericType>(nameof(Person.RemainingVacation))),
            };

            Assert.IsTrue(formula.Calculate(TestDataContext.Instance) == 10D);
        }

        [TestMethod]
        public void Sum()
        {
            Formula<double, NumericType, TestDataContext> formula = new Formula<double, NumericType, TestDataContext>()
            {
                Operations = new DataNavigation<NumericType>(nameof(TestDataContext.Subordinates), new Sum<NumericType>(nameof(Person.Salary))),
            };

            Assert.IsTrue(formula.Calculate(TestDataContext.Instance) == 122600D);
        }

        [TestMethod]
        public void Count()
        {
            Formula<double, NumericType, TestDataContext> formula = new Formula<double, NumericType, TestDataContext>()
            {
                Operations = new DataNavigation<NumericType>(nameof(TestDataContext.Subordinates), new Count()),
            };

            Assert.IsTrue(formula.Calculate(TestDataContext.Instance) == 3D);
        }

        [TestMethod]
        public void Concat()
        {
            Formula<string, StringType, TestDataContext> formula = new Formula<string, StringType, TestDataContext>()
            {
                Operations = new DataNavigation<StringType>(nameof(TestDataContext.Subordinates), new Concat(nameof(Person.Name), ";")),
            };

            Assert.IsTrue(formula.Calculate(TestDataContext.Instance) == "Craig;Bennett;Abraham");
        }

        [TestMethod]
        public void Where()
        {
            Formula<double, NumericType, TestDataContext> formula = new Formula<double, NumericType, TestDataContext>()
            {
                Operations = new DataNavigation<NumericType>(nameof(TestDataContext.Subordinates), new Where<NumericType, NumericType>(
                                                                                                        new GreaterThan<NumericType>(new ReadData<NumericType>(nameof(Person.Salary)),
                                                                                                                                     new NumericConstant(40000D)),
                                                                                                        new Sum<NumericType>(nameof(Person.Salary)))),
            };

            Assert.IsTrue(formula.Calculate(TestDataContext.Instance) == 92600D);
        }

        [TestMethod]
        public void First()
        {
            Formula<double, NumericType, TestDataContext> formula = new Formula<double, NumericType, TestDataContext>()
            {
                Operations = new DataNavigation<NumericType>(nameof(TestDataContext.Subordinates), new Where<NumericType, NumericType>(
                                                                                                        new Equal<NumericType>(new ReadData<NumericType>(nameof(Person.Salary)),
                                                                                                                               new NumericConstant(30000D)),
                                                                                                        new First<NumericType>(nameof(Person.Id)))),
            };

            Assert.IsTrue(formula.Calculate(TestDataContext.Instance) == 4D);
        }

        [TestMethod]
        public void FirstString()
        {
            Formula<string, StringType, TestDataContext> formula = new Formula<string, StringType, TestDataContext>()
            {
                Operations = new DataNavigation<StringType>(nameof(TestDataContext.Subordinates), new Where<NumericType, StringType>(
                                                                                                        new Equal<NumericType>(new ReadData<NumericType>(nameof(Person.Id)),
                                                                                                                               new NumericConstant(4D)),
                                                                                                        new First<StringType>(nameof(Person.Email)))),
            };

            Assert.IsTrue(formula.Calculate(TestDataContext.Instance) == "abraham.bragg@itcompany.com");
        }
    }
}
