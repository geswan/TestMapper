using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;


namespace Mapper
{
	public class Mapper<TClassA, TClassB> : IMapper<TClassA, TClassB>
		where TClassA : class
		where TClassB : class


	{
		private readonly List<(PropertyInfo classA, PropertyInfo classB)> matchingProperties;
        private readonly PropertyInfo[] classAProps;
        private readonly PropertyInfo[] classBProps;

        public Mapper()
		{
			Type classAType = typeof(TClassA);
			classAProps = classAType.GetProperties();
			Type classBType = typeof(TClassB);
			classBProps = classBType.GetProperties();
			matchingProperties =
			classAProps.Join(           // outer collection
			  classBProps,             // inner collection
			  a => a.Name, // outer key  
			  b => b.Name,     // inner key 
			  (a, b) => (a, b)//project into ValueTuple
			  ).ToList();
            
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

      /// <summary>
      /// Maps TClassB to a new instance of TClassA using its default constructor
      /// Throws an InvalidOperationException if there is no default constructor
      /// </summary>
        public TClassA Map(TClassB source)
        {

            TClassA sink = CreateNewInstance<TClassA>();
            Map(source, sink);
            return sink;
        }
        /// <summary>
        /// Maps TClassA to a new instance of TClassB using its default constructor
        /// Throws an InvalidOperationException if there is no default constructor
        /// </summary>
        public TClassB Map(TClassA producer)
        {
            TClassB consumer = CreateNewInstance<TClassB>();
            Map(producer, consumer);
            return consumer;

        }

        public void Pair(string propNameA, string propNameB)
        {
            var propA = classAProps.FirstOrDefault(a => a.Name == propNameA);
            var propB = classBProps.FirstOrDefault(a => a.Name == propNameB);
            if (propA == null || propB == null)
            throw new ArgumentException($"No match found for {propNameA}, {propNameB}");
            if(propA.PropertyType.FullName!= propB.PropertyType.FullName)
                throw new ArgumentException($"The property types do not match {propNameA}, {propNameB}");
            matchingProperties.Add((propA, propB));
        }

        public void Pair((string propNameA, string propNameB)[] pairs)
        {
            if (pairs == null)
            throw new ArgumentNullException(nameof(pairs));
            foreach (var (propNameA, propNameB) in pairs)
            {
                Pair(propNameA, propNameB);
            }
        }
        public int GetMappingsTotal() => matchingProperties.Count;

        private static ConstructorInfo GetConstructorInfo(Type type)
        {
            ConstructorInfo[] constructorInfos = type.GetConstructors();
            int infoLength = constructorInfos.Length;
            if (infoLength == 0)
            {
                throw new InvalidOperationException(
                    "The type " + type.FullName + " does not have a public constructor.");
            }
            if (infoLength == 1)
            {
                return constructorInfos[0];
            }
            //if there is more than one constructor
            //use the constructor with the fewest parameters
            int minParameters = constructorInfos.Select(c => c.GetParameters().Length).Min();

            return constructorInfos.Single(c => c.GetParameters().Length == minParameters);
        }
        private T CreateNewInstance<T>()
        {
            Type type = typeof(T);
            ConstructorInfo constructorInfo = GetConstructorInfo(type);
            ParameterInfo[] parameters = constructorInfo.GetParameters();
            if (parameters.Length != 0)
            {
                throw new InvalidOperationException(
                    "The type " + type.FullName + " does not have a default constructor.");
            }
            //build an expression that represents calling the constructor. The NodeType is 'New'
            NewExpression constructorCallingExpression = Expression.New(constructorInfo);
            // Can only compile expression trees that represent  lambda expressions.
            //So need to call  the following
            Expression<Func<T>> lambdaExpression = Expression.Lambda<Func<T>>(constructorCallingExpression);
            //Finally, compile the lambdaExpression into a Delegate and call it
            //To visualise the expression when debugging use: lambdaExpression.ToString();
            var getter = lambdaExpression.Compile();
            return getter.Invoke();
        }
    }
}
