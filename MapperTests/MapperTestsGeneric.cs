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
        protected abstract (string NameA, string NameB) Get2PropNamesToForceMatch();
        protected abstract (string NameA, string NameB)[] GetArrayOfPropNamesToForceMatch();
        protected abstract (string NameA, string NameB) Get2PropNamesToForceMatchFromPropsWithDifferentTypes();
        protected abstract bool AreAllPropsUnchanged(TClassA a,TClassA unmappedA);
        protected abstract bool AreAllPropsUnchanged(TClassB b, TClassB unmappedB);
        protected abstract bool AreUnmatchedPropsUnchanged(TClassA a, TClassA unmappedA);
        protected abstract bool AreUnmatchedPropsUnchanged(TClassB b,TClassB unmappedB);
        protected abstract bool AreForcedMatchedPropsMappedFromClassAToClassB(TClassB b, TClassA unmappedA);
        protected abstract bool AreForcedMatchedPropsMappedFromClassBToClassA(TClassA a,TClassB unmappedB);
        protected abstract bool AreSameNamePropsMappedFromClassBToClassA(TClassA a, TClassB b);
        protected abstract bool AreSameNamePropsMappedFromClassAToClassB(TClassB b, TClassA unmatchedA);

        [TestMethod]
        public void MapClassAClassBDoesNotChangeClassA()
        {
            Mapper<TClassA, TClassB> mapper = new Mapper<TClassA, TClassB>();
            TClassA a = CreateSampleClassA();
            TClassA unmappedA = CreateSampleClassA();
            TClassB b = CreateSampleClassB();
            mapper.Map(a, b);
            Assert.IsTrue(AreAllPropsUnchanged(a,unmappedA));
        }

        [TestMethod]
        public void MapClassBClassADoesNotChangeClassB()
        {
            Mapper<TClassA, TClassB> mapper = new Mapper<TClassA, TClassB>();
            TClassA a = CreateSampleClassA();
            TClassB b = CreateSampleClassB();
            TClassB unmappedB=CreateSampleClassB();
            mapper.Map(b, a);
            Assert.IsTrue(AreAllPropsUnchanged(b,unmappedB));
        }

        [TestMethod]
        public void MapClassAClassBDoesNotChangeClassBsUnmatchedProperties()
        {
            Mapper<TClassA, TClassB> mapper = new Mapper<TClassA, TClassB>();
            TClassA a = CreateSampleClassA();
            TClassB b = CreateSampleClassB();
            TClassB unmappedB = CreateSampleClassB();
            mapper.Map(a, b);
            Assert.IsTrue(AreUnmatchedPropsUnchanged(b,unmappedB));
        }

        [TestMethod]
        public void MapClassBClassADoesNotChangeClassAsUnmatchedProperties()
        {
            Mapper<TClassA, TClassB> mapper = new Mapper<TClassA, TClassB>();
            TClassA a = CreateSampleClassA();
            TClassA unmappedA = CreateSampleClassA();
            TClassB b = CreateSampleClassB();
            mapper.Map(b, a);
            Assert.IsTrue(AreUnmatchedPropsUnchanged(a,unmappedA));
        }

        [TestMethod]
        public void MapClassAClassBMapsSameNamePropertyValuesFromClassAToClassB()
        {
            Mapper<TClassA, TClassB> mapper = new Mapper<TClassA, TClassB>();
            TClassA a = CreateSampleClassA();
            TClassA unmappedA = CreateSampleClassA();
            TClassB b = CreateSampleClassB();
            mapper.Map(a, b);
            Assert.IsTrue(AreSameNamePropsMappedFromClassAToClassB(b,unmappedA));
        }

 

        [TestMethod]
        public void MapClassBClassAMapsSameNamePropertyValuesFromClassBToClassA()
        {
            Mapper<TClassA, TClassB> mapper = new Mapper<TClassA, TClassB>();
            TClassA a = CreateSampleClassA();
            TClassB b = CreateSampleClassB();
            TClassB unmappedB = CreateSampleClassB();
            mapper.Map(b, a);
            Assert.IsTrue(AreSameNamePropsMappedFromClassBToClassA(a,unmappedB));
        }

        [TestMethod]
        public void ForceMatchAddsATupleToMatchingProperties()
        {
            Mapper<TClassA, TClassB> mapper = new Mapper<TClassA, TClassB>();
            (string NameA, string NameB) = Get2PropNamesToForceMatch();
            int mappings = mapper.GetMappingsTotal;
            mapper.ForceMatch(NameA, NameB);
            Assert.IsTrue(mappings + 1 == mapper.GetMappingsTotal);
        }

        [TestMethod]
        public void ForceMatchAddsArrayCountTuplesToMatchingProperties()
        {
            Mapper<TClassA, TClassB> mapper = new Mapper<TClassA, TClassB>();
            (string NameA, string NameB)[] namesArray = GetArrayOfPropNamesToForceMatch();
            int mappings = mapper.GetMappingsTotal;
            mapper.ForceMatch(namesArray);
            Assert.IsTrue(mappings + namesArray.Length == mapper.GetMappingsTotal);
        }

        [TestMethod]
        public void MapClassAClassBWithForcingMapsForcedMachedPropsFromAToB()
        {
            Mapper<TClassA, TClassB> mapper = new Mapper<TClassA, TClassB>();
            TClassA a = CreateSampleClassA();
            TClassA unmappedA = CreateSampleClassA();
            TClassB b = CreateSampleClassB();
           
            (string NameA, string NameB)[] namesArray = GetArrayOfPropNamesToForceMatch();
            mapper.ForceMatch(namesArray);
            mapper.Map(a, b);
            Assert.IsTrue(AreForcedMatchedPropsMappedFromClassAToClassB(b,unmappedA));
        }

        [TestMethod]
        public void MapClassBClassAWithForcingMapsForcedMappedPropsFromBToA()
        {
            Mapper<TClassA, TClassB> mapper = new Mapper<TClassA, TClassB>();
            TClassA a = CreateSampleClassA();
            TClassB b = CreateSampleClassB();
            TClassB unmappedB = CreateSampleClassB();
            (string NameA, string NameB)[] namesArray = GetArrayOfPropNamesToForceMatch();
            mapper.ForceMatch(namesArray);
            mapper.Map(b, a);
            Assert.IsTrue(AreForcedMatchedPropsMappedFromClassBToClassA(a,unmappedB));
        }

        [TestMethod]                                 //test fail message
        [ExpectedException(typeof(ArgumentException), "Nonexistent property name was allowed")]
        public void ForceMatchThrowsArgumentExceptionWhenPropNameNotFound()
        {
            Mapper<TClassA, TClassB> mapper = new Mapper<TClassA, TClassB>();
            (string NameA, string NameB) = Get2PropNamesToForceMatch();
            string illegalName = "*" + NameA;
            mapper.ForceMatch(illegalName, NameB);

        }

        [TestMethod]                                 //test fail message
        [ExpectedException(typeof(ArgumentException), "Different property Types were allowed")]
        public void ForceMatchThrowsArgumentExceptionWhenPairedTypesDoNotMatch()
        {
            Mapper<TClassA, TClassB> mapper = new Mapper<TClassA, TClassB>();
            (string NameA, string NameB) = Get2PropNamesToForceMatchFromPropsWithDifferentTypes();
            mapper.ForceMatch(NameA, NameB);
           
        }
    }
}