using Core;
using Core.Blocks.Collections;
using Core.Blocks.Comparison;
using Core.Blocks.Conditional;
using Core.Blocks.Constants;
using Core.Blocks.DataContext;
using Core.Blocks.Logical;
using Core.TypeDefinitions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Test.DataContext;

namespace Test
{
    [TestClass]
    public class SerializationTest
    {

        [TestMethod]
        public void Serialization1()
        {
            Formula<bool, BoolType, TestDataContext> formula = new Formula<bool, BoolType, TestDataContext>()
            {
                Operations = new BoolConstant(true),
            };

            string xml = @"<?xml version=""1.0""?>
<FormulaOfBooleanBoolTypeTestDataContext xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <Operations xsi:type=""BoolConstant"">
    <Value>true</Value>
  </Operations>
</FormulaOfBooleanBoolTypeTestDataContext>";

            CheckTest(formula, xml, true);
        }

        [TestMethod]
        public void Serialization2()
        {
            Formula<bool, BoolType, TestDataContext> formula = new Formula<bool, BoolType, TestDataContext>()
            {
                Operations = new And(new BoolConstant(true), new BoolConstant(true)),
            };

            string xml = @"<?xml version=""1.0""?>
<FormulaOfBooleanBoolTypeTestDataContext xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <Operations xsi:type=""And"">
    <LeftOperand xsi:type=""BoolConstant"">
      <Value>true</Value>
    </LeftOperand>
    <RightOperand xsi:type=""BoolConstant"">
      <Value>true</Value>
    </RightOperand>
  </Operations>
</FormulaOfBooleanBoolTypeTestDataContext>";

            CheckTest(formula, xml, true);
        }

        [TestMethod]
        public void Serialization3()
        {
            Formula<bool, BoolType, TestDataContext> formula = new Formula<bool, BoolType, TestDataContext>()
            {
                Operations = new Or(new Equal<NumericType>(new NumericConstant(3D), new NumericConstant(5D)),
                                    new GreaterThan<NumericType>(new NumericConstant(5D), new NumericConstant(10D))),
            };

            string xml = @"<?xml version=""1.0""?>
<FormulaOfBooleanBoolTypeTestDataContext xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <Operations xsi:type=""Or"">
    <LeftOperand xsi:type=""EqualOfNumericType"">
      <LeftOperand xsi:type=""NumericConstant"">
        <Value>3</Value>
      </LeftOperand>
      <RightOperand xsi:type=""NumericConstant"">
        <Value>5</Value>
      </RightOperand>
    </LeftOperand>
    <RightOperand xsi:type=""GreaterThanOfNumericType"">
      <LeftOperand xsi:type=""NumericConstant"">
        <Value>5</Value>
      </LeftOperand>
      <RightOperand xsi:type=""NumericConstant"">
        <Value>10</Value>
      </RightOperand>
    </RightOperand>
  </Operations>
</FormulaOfBooleanBoolTypeTestDataContext>";

            CheckTest(formula, xml, false);
        }


        [TestMethod]
        public void Serialization4()
        {
            Formula<double, NumericType, TestDataContext> formula = new Formula<double, NumericType, TestDataContext>()
            {
                Operations = new IfElse<StringType, NumericType>(new Equal<StringType>(new StringConstant("Value1"), new StringConstant("Value2")),
                                                                 new NumericConstant(10D),
                                                                 new NumericConstant(5D)),
            };

            string xml = @"<?xml version=""1.0""?>
<FormulaOfDoubleNumericTypeTestDataContext xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <Operations xsi:type=""IfElseOfStringTypeNumericType"">
    <Comparison xsi:type=""EqualOfStringType"">
      <LeftOperand xsi:type=""StringConstant"">
        <Value>Value1</Value>
      </LeftOperand>
      <RightOperand xsi:type=""StringConstant"">
        <Value>Value2</Value>
      </RightOperand>
    </Comparison>
    <True xsi:type=""NumericConstant"">
      <Value>10</Value>
    </True>
    <False xsi:type=""NumericConstant"">
      <Value>5</Value>
    </False>
  </Operations>
</FormulaOfDoubleNumericTypeTestDataContext>";

            CheckTest(formula, xml, 5D);
        }

        [TestMethod]
        public void Serialization5()
        {
            Formula<double, NumericType, TestDataContext> formula = new Formula<double, NumericType, TestDataContext>()
            {
                Operations = new IfElse<StringType, NumericType>(new Equal<StringType>(new ReadData<StringType>(nameof(TestDataContext.Department)), new StringConstant("IT")),
                                                              new DataNavigation<NumericType>(nameof(TestDataContext.Subordinates), new Count()),
                                                              new NumericConstant(0D)),
            };

            string xml = @"<?xml version=""1.0""?>
<FormulaOfDoubleNumericTypeTestDataContext xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <Operations xsi:type=""IfElseOfStringTypeNumericType"">
    <Comparison xsi:type=""EqualOfStringType"">
      <LeftOperand xsi:type=""ReadDataOfStringType"">
        <PropertyName>Department</PropertyName>
      </LeftOperand>
      <RightOperand xsi:type=""StringConstant"">
        <Value>IT</Value>
      </RightOperand>
    </Comparison>
    <True xsi:type=""DataNavigationOfNumericType"">
      <PropertyName>Subordinates</PropertyName>
      <InternalBlock xsi:type=""Count"" />
    </True>
    <False xsi:type=""NumericConstant"">
      <Value>0</Value>
    </False>
  </Operations>
</FormulaOfDoubleNumericTypeTestDataContext>";

            CheckTest(formula, xml, 3D);
        }

        [TestMethod]
        public void Serialization6()
        {
            Formula<string, StringType, TestDataContext> formula = new Formula<string, StringType, TestDataContext>()
            {
                Operations = new DataNavigation<StringType>(nameof(TestDataContext.Subordinates),
                                                            new Where<NumericType, StringType>(
                                                                    new Equal<NumericType>(new ReadData<NumericType>(nameof(Person.RemainingVacation)),
                                                                                           new GlobalDataNavigation<NumericType>(
                                                                                                   new DataNavigation<NumericType>(nameof(TestDataContext.Subordinates), new Max<NumericType>(nameof(Person.RemainingVacation))))),
                                                                    new First<StringType>(nameof(Person.Name)))),
            };

            string xml = @"<?xml version=""1.0""?>
<FormulaOfStringStringTypeTestDataContext xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <Operations xsi:type=""DataNavigationOfStringType"">
    <PropertyName>Subordinates</PropertyName>
    <InternalBlock xsi:type=""WhereOfNumericTypeStringType"">
      <Comparison xsi:type=""EqualOfNumericType"">
        <LeftOperand xsi:type=""ReadDataOfNumericType"">
          <PropertyName>RemainingVacation</PropertyName>
        </LeftOperand>
        <RightOperand xsi:type=""GlobalDataNavigationOfNumericType"">
          <InternalBlock xsi:type=""DataNavigationOfNumericType"">
            <PropertyName>Subordinates</PropertyName>
            <InternalBlock xsi:type=""MaxOfNumericType"">
              <SelectedProperty>RemainingVacation</SelectedProperty>
            </InternalBlock>
          </InternalBlock>
        </RightOperand>
      </Comparison>
      <InternalBlock xsi:type=""FirstOfStringType"">
        <SelectedProperty>Name</SelectedProperty>
      </InternalBlock>
    </InternalBlock>
  </Operations>
</FormulaOfStringStringTypeTestDataContext>";

            CheckTest(formula, xml, "Abraham");
        }

        [TestMethod]
        public void Serializacion7()
        {
            Formula<double, NumericType, TestDataContext> formula = new Formula<double, NumericType, TestDataContext>()
            {
                Operations = new DataNavigation<NumericType>(nameof(TestDataContext.Subordinates),
                                                            new Where<BoolType, NumericType>(
                                                                    new Equal<BoolType>(new ReadData<BoolType>(nameof(Person.InActive)),
                                                                                        new BoolConstant(true)),
                                                                    new Sum<NumericType>(nameof(Person.RemainingVacation)))),
            };

            string xml = @"<?xml version=""1.0""?>
<FormulaOfDoubleNumericTypeTestDataContext xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <Operations xsi:type=""DataNavigationOfNumericType"">
    <PropertyName>Subordinates</PropertyName>
    <InternalBlock xsi:type=""WhereOfBoolTypeNumericType"">
      <Comparison xsi:type=""EqualOfBoolType"">
        <LeftOperand xsi:type=""ReadDataOfBoolType"">
          <PropertyName>InActive</PropertyName>
        </LeftOperand>
        <RightOperand xsi:type=""BoolConstant"">
          <Value>true</Value>
        </RightOperand>
      </Comparison>
      <InternalBlock xsi:type=""SumOfNumericType"">
        <SelectedProperty>RemainingVacation</SelectedProperty>
      </InternalBlock>
    </InternalBlock>
  </Operations>
</FormulaOfDoubleNumericTypeTestDataContext>";

            CheckTest(formula, xml, 25D);
        }


        [TestMethod]
        public void Serializacion8()
        {
            Formula<double, NumericType, TestDataContext> formula = new Formula<double, NumericType, TestDataContext>()
            {
                Operations = new DataNavigation<NumericType>(nameof(TestDataContext.Subordinates),
                                                            new Where<BoolType, NumericType>(
                                                                    new Equal<BoolType>(new ReadData<BoolType>(nameof(Person.IsMarried)),
                                                                                        new BoolConstant(true)),
                                                                    new Count())),
            };
            string xml = @"<?xml version=""1.0""?>
<FormulaOfDoubleNumericTypeTestDataContext xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <Operations xsi:type=""DataNavigationOfNumericType"">
    <PropertyName>Subordinates</PropertyName>
    <InternalBlock xsi:type=""WhereOfBoolTypeNumericType"">
      <Comparison xsi:type=""EqualOfBoolType"">
        <LeftOperand xsi:type=""ReadDataOfBoolType"">
          <PropertyName>IsMarried</PropertyName>
        </LeftOperand>
        <RightOperand xsi:type=""BoolConstant"">
          <Value>true</Value>
        </RightOperand>
      </Comparison>
      <InternalBlock xsi:type=""Count"" />
    </InternalBlock>
  </Operations>
</FormulaOfDoubleNumericTypeTestDataContext>";
            CheckTest(formula, xml, 2D);
        }


        private void CheckTest<TResult, TPrimitiveType>(Formula<TResult, TPrimitiveType, TestDataContext> formula, string xml, TResult value)
            where TPrimitiveType : BaseType<TResult>            
        {
            string formulaXml = Formula<TResult, TPrimitiveType, TestDataContext>.SerializeFormula(formula);

            Assert.IsTrue(string.Compare(formulaXml, xml) == 0);

            Formula<TResult, TPrimitiveType, TestDataContext> deserializedFormula = Formula<TResult, TPrimitiveType, TestDataContext>.DeserializeFormula(formulaXml);
            Assert.IsTrue(value.Equals(deserializedFormula.Calculate(TestDataContext.Instance)));
        }
    }
}
