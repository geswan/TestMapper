using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace TestMapper
{
	public class Mapper<TClassA, TClassB> : IMapper<TClassA, TClassB>
		where TClassA : class
		where TClassB : class


	{
		private readonly IEnumerable<(PropertyInfo classA, PropertyInfo classB)> matchingProperties;
		public Mapper()
		{
			Type classAType = typeof(TClassA);
			PropertyInfo[] classAProps = classAType.GetProperties();
			Type classBType = typeof(TClassB);
			PropertyInfo[] classBProps = classBType.GetProperties();
			matchingProperties =
			classAProps.Join(           // outer collection
			  classBProps,             // inner collection
			  a => a.Name, // outer key  
			  b => b.Name,     // inner key 
			  (a, b) => (a, b)//project into ValueTuple
			  );
		}
		public void Map(TClassA producer, TClassB consumer)
		{
			foreach (var (classAInfo, classBInfo) in matchingProperties)
			{

				classBInfo.SetValue(consumer, classAInfo.GetValue(producer));

			}
		}

		public void Map(TClassB producer, TClassA consumer)
		{
			foreach (var (classAInfo, classBInfo) in matchingProperties)
			{
				classAInfo.SetValue(consumer, classBInfo.GetValue(producer));
			}
		}
	}
}
