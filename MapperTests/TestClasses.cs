using System;

namespace MapperTests
{
	public class ClassA
	{
		public string Name { get; set; }
		public int Age { get; set; }
		public double Cash { get; set; }
		public string Code { get; set; }
		public DateTime Date { get; set; }
		public Person Employee { get; set; }
	}

	public class ClassB
	{
		public string Name { get; set; }
		public int Age { get; set; }
		public double Cash { get; set; }
		public string CodeName { get; set; }
		public DateTime Date { get; set; }
		public Person Employee { get; set; }
	}

	public class Person
	{
		public string Name { get; set; }
	}


}
