using Mapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MapperTests
{

    public abstract class MapperTestsGeneric<TClassA, TClassB>
         where TClassA : class
         where TClassB : class
    {
        protected abstract TClassA CreateSampleClassA();
        protected abstract TClassB CreateSampleClassB();
        protected abstract (string NameA, string NameB) Get2NamesToPair();
        protected abstract (string NameA, string NameB)[] GetArrayOfNamesToPair();
        protected abstract (string NameA, string NameB) Get2NamesToPairFromPropsWithDifferentTypes();
        protected abstract bool AreAllPropsUnchanged(TClassA a);
        protected abstract bool AreAllPropsUnchanged(TClassB b);
        protected abstract bool AreUnpairedPropsUnchanged(TClassA a);
         protected abstract bool AreUnpairedPropsUnchanged(TClassB b);
        protected abstract bool ArePairedPropsMappedFromClassAToClassB(TClassB b);
        protected abstract bool ArePairedPropsMappedFromClassBToClassA(TClassA a);
        protected abstract bool AreSameNamePropsMappedFromClassBToClassA(TClassA a);
        protected abstract bool AreSameNamePropsMappedFromClassAToClassB(TClassB b);


        [TestMethod]
        public void MapClassAClassBDoesNotChangeClassA()
        {
            Mapper<TClassA, TClassB> mapper = new Mapper<TClassA, TClassB>();
            TClassA a = CreateSampleClassA();
            TClassB b = CreateSampleClassB();
            mapper.Map(a, b);
            Assert.IsTrue(AreAllPropsUnchanged(a));
        }

        [TestMethod]
        public void MapClassBClassADoesNotChangeClassB()
        {
            Mapper<TClassA, TClassB> mapper = new Mapper<TClassA, TClassB>();
            TClassA a = CreateSampleClassA();
            TClassB b = CreateSampleClassB();
            mapper.Map(b, a);
            Assert.IsTrue(AreAllPropsUnchanged(b));
        }

        [TestMethod]
        public void MapClassAClassBDoesNotChangeClassBsUnmatchedProperties()
        {
            Mapper<TClassA, TClassB> mapper = new Mapper<TClassA, TClassB>();
            TClassA a = CreateSampleClassA();
            TClassB b = CreateSampleClassB();
            mapper.Map(a, b);
            Assert.IsTrue(AreUnpairedPropsUnchanged(b));
        }

        [TestMethod]
        public void MapClassAClassBMapsSameNamePropertyValuesFromClassAToClassB()
        {
            Mapper<TClassA, TClassB> mapper = new Mapper<TClassA, TClassB>();
            TClassA a = CreateSampleClassA();
            TClassB b = CreateSampleClassB();
            mapper.Map(a, b);
            Assert.IsTrue(AreSameNamePropsMappedFromClassAToClassB(b));
        }

        [TestMethod]
        public void MapClassBClassADoesNotChangeClassAsUnmatchedProperties()
        {
            Mapper<TClassA, TClassB> mapper = new Mapper<TClassA, TClassB>();
            TClassA a = CreateSampleClassA();
            TClassB b = CreateSampleClassB();
            mapper.Map(b, a);
            Assert.IsTrue(AreUnpairedPropsUnchanged(a));
        }

        [TestMethod]
        public void MapClassBClassASetsClassAsSharedPropertiesToClassBsSharedPropertiesValues()
        {
            Mapper<TClassA, TClassB> mapper = new Mapper<TClassA, TClassB>();
            TClassA a = CreateSampleClassA();
            TClassB b = CreateSampleClassB();
            mapper.Map(b, a);
            Assert.IsTrue(ArePairedPropsMappedFromClassBToClassA(a));
        }

        [TestMethod]
        public void PairAddsATupleToMatchingProperties()
        {
            Mapper<TClassA, TClassB> mapper = new Mapper<TClassA, TClassB>();
            (string NameA, string NameB) = Get2NamesToPair();
            int mappings = mapper.GetMappingsTotal();
            mapper.Pair(NameA, NameB);
            Assert.IsTrue(mappings + 1 == mapper.GetMappingsTotal());
        }

        [TestMethod]
        public void PairAddsArrayCountTuplesToMatchingProperties()
        {
            Mapper<TClassA, TClassB> mapper = new Mapper<TClassA, TClassB>();
            (string NameA, string NameB)[] namesArray = GetArrayOfNamesToPair();
            int mappings = mapper.GetMappingsTotal();
            mapper.Pair(namesArray);
            Assert.IsTrue(mappings + namesArray.Length == mapper.GetMappingsTotal());
        }

        [TestMethod]
        public void MapClassAClassBMapsPairedPropsFromAToB()
        {
            Mapper<TClassA, TClassB> mapper = new Mapper<TClassA, TClassB>();
            TClassA a = CreateSampleClassA();
            TClassB b = CreateSampleClassB();
            (string NameA, string NameB)[] namesArray = GetArrayOfNamesToPair();
            mapper.Pair(namesArray);
            mapper.Map(a, b);
            Assert.IsTrue(ArePairedPropsMappedFromClassAToClassB(b));
        }

        [TestMethod]
        public void MapClassBClassAMapsPairedPropsFromBToA()
        {
            Mapper<TClassA, TClassB> mapper = new Mapper<TClassA, TClassB>();
            TClassA a = CreateSampleClassA();
            TClassB b = CreateSampleClassB();
            (string NameA, string NameB)[] namesArray = GetArrayOfNamesToPair();
            mapper.Pair(namesArray);
            mapper.Map(b, a);
            Assert.IsTrue(ArePairedPropsMappedFromClassBToClassA(a));
        }

        [TestMethod]                                 //test fail message
        [ExpectedException(typeof(ArgumentException), "An unmatched name was allowed")]
        public void PairThrowsArgumentExceptionWhenPropNameNotFound()
        {
            Mapper<TClassA, TClassB> mapper = new Mapper<TClassA, TClassB>();
            (string NameA, string NameB) = Get2NamesToPair();
            string unmatchedName = "*" + NameA;
            mapper.Pair(unmatchedName, NameB);

        }

        [TestMethod]                                 //test fail message
        [ExpectedException(typeof(ArgumentException), "Different property Types were allowed")]
        public void PairThrowsArgumentExceptionWhenPairedTypesDoNotMatch()
        {
            Mapper<TClassA, TClassB> mapper = new Mapper<TClassA, TClassB>();
            (string NameA, string NameB) = Get2NamesToPairFromPropsWithDifferentTypes();
            mapper.Pair(NameA, NameB);
           
        }
    }
}