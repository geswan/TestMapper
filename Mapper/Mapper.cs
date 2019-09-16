﻿using System;
using System.Collections.Generic;
using System.Linq;
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

        public void ForceMatch(string propNameA, string propNameB)
        {
            if (propNameA == null) throw new ArgumentNullException(nameof(propNameA));
            if (propNameB == null) throw new ArgumentNullException(nameof(propNameB));
            var propA = classAProps.FirstOrDefault(a => a.Name == propNameA);
            var propB = classBProps.FirstOrDefault(a => a.Name == propNameB);
            if (propA == null) throw new ArgumentException($"Property named {propNameA} not found");
            if (propB == null) throw new ArgumentException($"Property named {propNameB} not found");
            if(propA.PropertyType.FullName!= propB.PropertyType.FullName)
            throw new ArgumentException($"The property types do not match {propNameA}, {propNameB}");
            matchingProperties.Add((propA, propB));
        }

        public void ForceMatch((string propNameA, string propNameB)[] pairs)
        {
            if (pairs == null)
            throw new ArgumentNullException(nameof(pairs));
            foreach (var (propNameA, propNameB) in pairs)
            {
                ForceMatch(propNameA, propNameB);
            }
        }
        public int GetMappingsTotal => matchingProperties.Count;

 
    }
}
