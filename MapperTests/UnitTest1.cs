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
				Employee = employeeB
			};
			return classB;
		}


		protected override bool AreAllPropsUnchanged(ClassA a) => a.Name == Constants.NameA &&
				  a.Age == Constants.AgeA &&
				  a.Cash == Constants.CashA &&
				  a.Code == Constants.CodeA &&
				  a.Date == classADate &&
				  a.Employee == employeeA;

		protected override bool AreAllPropsUnchanged(ClassB b) => b.Name == Constants.NameB &&
				  b.Age == Constants.AgeB &&
				  b.Cash == Constants.CashB &&
				  b.CodeName == Constants.CodeNameB &&
				  b.Date == classBDate &&
				  b.Employee == employeeB;
		protected override bool AreUnsharedPropsUnchanged(ClassB b) => b.CodeName == Constants.CodeNameB;


		protected override bool AreUnsharedPropsUnchanged(ClassA a) => a.Code == Constants.CodeA;



		protected override bool AreSharedPropsInClassAMatchedToThoseInClassB(ClassA a) => a.Name == Constants.NameB &&
							a.Age == Constants.AgeB &&
							a.Cash == Constants.CashB &&
							a.Date == classBDate &&
							a.Employee == employeeB;




		protected override bool AreSharedPropsInClassBMatchedToThoseInClassA(ClassB b) => b.Name == Constants.NameA &&
							b.Age == Constants.AgeA &&
							b.Cash == Constants.CashA &&
							b.Date == classADate &&
							b.Employee == employeeA;


	}
}
