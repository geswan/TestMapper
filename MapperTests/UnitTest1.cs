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

        protected override bool AreAllPropsUnchanged(ClassA a, ClassA unmappedA)
        {
            return a.Name == unmappedA.Name &&
              a.Age == unmappedA.Age &&
              a.Cash == unmappedA.Cash &&
              a.Code == unmappedA.Code &&
              a.Id == unmappedA.Id &&
              a.Date == unmappedA.Date &&
              a.Employee == employeeA;
        }
        protected override bool AreAllPropsUnchanged(ClassB b, ClassB unmappedB)
        {
            return b.Name == unmappedB.Name &&
                   b.Age == unmappedB.Age &&
                   b.Cash == unmappedB.Cash &&
                   b.CodeName == unmappedB.CodeName &&
                   b.RecordNumber == unmappedB.RecordNumber &&
                   b.Date == unmappedB.Date &&
                   b.Employee == employeeB;
        }
        protected override bool AreUnmatchedPropsUnchanged(ClassB b, ClassB unmappedB)
        {
            return b.CodeName == unmappedB.CodeName &&
            b.RecordNumber == unmappedB.RecordNumber;
        }

        protected override bool AreUnmatchedPropsUnchanged(ClassA a, ClassA unmappedA)
        {
            return a.Code == unmappedA.Code &&
              a.Id == unmappedA.Id;
        }

        protected override bool AreSameNamePropsMappedFromBtoA(ClassA a, ClassB unmappedB) => a.Name == unmappedB.Name &&
                            a.Age == unmappedB.Age &&
                            a.Cash == unmappedB.Cash &&
                            a.Date == unmappedB.Date &&
                            a.Employee == unmappedB.Employee;

        protected override bool AreSameNamePropsMappedFromAtoB(ClassB b, ClassA unmappedA) => b.Name == unmappedA.Name &&
                            b.Age == unmappedA.Age &&
                            b.Cash == unmappedA.Cash &&
                            b.Date == unmappedA.Date &&
                            b.Employee == unmappedA.Employee;

        protected override (string NameA, string NameB) Get2PropNamesToForceMatch()
        {
            return (nameof(ClassA.Code), nameof(ClassB.CodeName));
        }


        protected override (string NameA, string NameB) Get2PropNamesToForceMatchFromPropsWithDifferentTypes()
        {

            return (nameof(ClassA.Code), nameof(ClassB.RecordNumber));
        }

        protected override (string PropBName, Func<ClassB, ClassB, bool> AreEqual) GetExcludedMatchTestMetadata()
        {
            bool areEqual(ClassB b, ClassB unMatchedB) => b.Age == unMatchedB.Age;
            return (nameof(ClassB.Age),  areEqual);
        }
    }
}
