using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;

namespace MapperTests
{
    //The [TestClass] attribute must be present in this class
    //It gives rise to a warning that no test is available in the class
    //But, without the attribute, tests in the parent class will not be run.
    [TestClass]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Unit Test Helper methods")]
    public class MapperUnitTests : MapperTestsGeneric<ClassA, ClassB>
    {
        private readonly Person employeeA = new Person { Name = Constants.PersonAName };
        private readonly Person employeeB = new Person { Name = Constants.PersonBName };
        private readonly DateTime classADate = DateTime.Parse(Constants.ClassADate, CultureInfo.CurrentCulture);
        private readonly DateTime classBDate = DateTime.Parse(Constants.ClassBDate, CultureInfo.CurrentCulture);

        protected override ClassA CreateSampleClassA()
        {
            ClassA classA = new ClassA
            {
                Name = Constants.NameA,
                Age = Constants.AgeA,
                Cash = Constants.CashA,
                Code = Constants.CodeA,
                Id = Constants.IdAValue,
                Date = classADate,
                Employee = employeeA
            };
            return classA;
        }

        protected override ClassB CreateSampleClassB()
        {
            ClassB classB = new ClassB
            {
                Name = Constants.NameB,
                Age = Constants.AgeB,
                Cash = Constants.CashB,
                CodeName = Constants.CodeNameB,
                Date = classBDate,
                Employee = employeeB,
                RecordNumber = Constants.RecordNumberBValue
            };
            return classB;
        }

        protected override bool AreAllPropsUnchanged(ClassA a) => a.Name == Constants.NameA &&
                  a.Age == Constants.AgeA &&
                  a.Cash == Constants.CashA &&
                  a.Code == Constants.CodeA &&
                  a.Id == Constants.IdAValue &&
                  a.Date == classADate &&
                  a.Employee == employeeA;

        protected override bool AreAllPropsUnchanged(ClassB b) => b.Name == Constants.NameB &&
                  b.Age == Constants.AgeB &&
                  b.Cash == Constants.CashB &&
                  b.CodeName == Constants.CodeNameB &&
                  b.RecordNumber == Constants.RecordNumberBValue &&
                  b.Date == classBDate &&
                  b.Employee == employeeB;
        protected override bool AreUnpairedPropsUnchanged(ClassB b) => b.CodeName == Constants.CodeNameB &&
            b.RecordNumber == Constants.RecordNumberBValue;

        protected override bool AreUnpairedPropsUnchanged(ClassA a) => a.Code == Constants.CodeA &&
            a.Id == Constants.IdAValue;

        protected override bool AreSameNamePropsMappedFromClassBToClassA(ClassA a) => a.Name == Constants.NameB &&
                            a.Age == Constants.AgeB &&
                            a.Cash == Constants.CashB &&
                            a.Date == classBDate &&
                            a.Employee == employeeB;

        protected override bool AreSameNamePropsMappedFromClassAToClassB(ClassB b) => b.Name == Constants.NameA &&
                            b.Age == Constants.AgeA &&
                            b.Cash == Constants.CashA &&
                            b.Date == classADate &&
                            b.Employee == employeeA;
        protected override bool ArePairedPropsMappedFromClassAToClassB(ClassB b) => b.CodeName == Constants.CodeA &&
                    b.RecordNumber == Constants.IdAValue;

        protected override bool ArePairedPropsMappedFromClassBToClassA(ClassA a) => a.Code == Constants.CodeNameB &&
                   a.Id == Constants.RecordNumberBValue;
        protected override (string NameA, string NameB) Get2NamesToPair()
        {
            return (Constants.CodeA, Constants.CodeNameB);
        }

        protected override (string NameA, string NameB) Get2NamesToPairFromPropsWithDifferentTypes()
        {
            return (Constants.CodeA, Constants.RecordNumberB);
        }

        protected override (string NameA, string NameB)[] GetArrayOfNamesToPair()
        {
            (string NameA, string NameB)[] names = new (string NameA, string NameB)[]
            {
                 (Constants.CodeA, Constants.CodeNameB), (Constants.IdA,Constants.RecordNumberB)
            };
            return names;
        }
    }
}
