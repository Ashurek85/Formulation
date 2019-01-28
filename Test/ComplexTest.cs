using Core;
using Core.Blocks.Collections;
using Core.Blocks.Comparison;
using Core.Blocks.Conditional;
using Core.Blocks.Constants;
using Core.Blocks.DataContext;
using Core.Blocks.Elementals;
using Core.TypeDefinitions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Test.DataContext;

namespace Test
{
    [TestClass]
    public class ComplexTest
    {

        [TestMethod]
        public void ComplexIfElse()
        {
            // If Department is IT -> Number of subordinates
            // Else -> 0

            Formula<double, NumericType, TestDataContext> formula = new Formula<double, NumericType, TestDataContext>()
            {
                Operations = new IfElse<StringType, NumericType>(new Equal<StringType>(new ReadData<StringType>(nameof(TestDataContext.Department)), new StringConstant("IT")),
                                                              new DataNavigation<NumericType>(nameof(TestDataContext.Subordinates), new Count()),
                                                              new NumericConstant(0D)),
            };

            Assert.IsTrue(formula.Calculate(TestDataContext.Instance) == 3D);
        }

        [TestMethod]
        public void ComplexFirst()
        {
            // Name of Subordinate with more remaining vacations
            Formula<string, StringType, TestDataContext> formula = new Formula<string, StringType, TestDataContext>()
            {
                Operations = new DataNavigation<StringType>(nameof(TestDataContext.Subordinates),
                                                            new Where<NumericType, StringType>(
                                                                    new Equal<NumericType>(new ReadData<NumericType>(nameof(Person.RemainingVacation)),
                                                                                           new GlobalDataNavigation<NumericType>(
                                                                                                   new DataNavigation<NumericType>(nameof(TestDataContext.Subordinates), new Max<NumericType>(nameof(Person.RemainingVacation))))),
                                                                    new First<StringType>(nameof(Person.Name)))),
            };
            Assert.IsTrue(formula.Calculate(TestDataContext.Instance) == "Abraham");
        }


        [TestMethod]
        public void ComplexSum()
        {
            // Sum of Active RemainingVacation (Subordinates)
            Formula<double, NumericType, TestDataContext> formula = new Formula<double, NumericType, TestDataContext>()
            {
                Operations = new DataNavigation<NumericType>(nameof(TestDataContext.Subordinates),
                                                            new Where<BoolType, NumericType>(
                                                                    new Equal<BoolType>(new ReadData<BoolType>(nameof(Person.InActive)),
                                                                                        new BoolConstant(true)),
                                                                    new Sum<NumericType>(nameof(Person.RemainingVacation)))),
            };
            Assert.IsTrue(formula.Calculate(TestDataContext.Instance) == 25D);
        }


        [TestMethod]
        public void ComplexCount()
        {
            // Count of Married Subordinates
            Formula<double, NumericType, TestDataContext> formula = new Formula<double, NumericType, TestDataContext>()
            {
                Operations = new DataNavigation<NumericType>(nameof(TestDataContext.Subordinates),
                                                            new Where<BoolType, NumericType>(
                                                                    new Equal<BoolType>(new ReadData<BoolType>(nameof(Person.IsMarried)),
                                                                                        new BoolConstant(true)),
                                                                    new Count())),
            };
            Assert.IsTrue(formula.Calculate(TestDataContext.Instance) == 2D);
        }
    }
}

