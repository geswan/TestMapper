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
        protected abstract (string NameA, string NameB) Get2PropNamesToForceMatchFromPropsWithDifferentTypes();
        protected abstract bool AreAllPropsUnchanged(TClassA a, TClassA unmappedA);
        protected abstract bool AreAllPropsUnchanged(TClassB b, TClassB unmappedB);
        protected abstract bool AreUnmatchedPropsUnchanged(TClassA a, TClassA unmappedA);
        protected abstract bool AreUnmatchedPropsUnchanged(TClassB b, TClassB unmappedB);
        protected abstract bool AreSameNamePropsMappedFromBtoA(TClassA a, TClassB unmappedB);
        protected abstract bool AreSameNamePropsMappedFromAtoB(TClassB b, TClassA unmappedA);
        protected abstract ((string NameA, string NameB)[] Names, Func<TClassA, TClassB, bool> AreEqual) GetForcedMatchTestMetadata();
        protected abstract (string NameB, Func<TClassB, TClassB, bool> AreEqual) GetExcludedMatchTestMetadata();

        [TestMethod]
        public void MapAtoBDoesNotChangeA()
        {
            Mapper<TClassA, TClassB> mapper = new Mapper<TClassA, TClassB>();
            TClassA a = CreateSampleClassA();
            TClassA unmappedA = CreateSampleClassA();
            TClassB b = CreateSampleClassB();
            mapper.Map(a, b);
            Assert.IsTrue(AreAllPropsUnchanged(a, unmappedA));
        }

        [TestMethod]
        public void MapBtoADoesNotChangeB()
        {
            Mapper<TClassA, TClassB> mapper = new Mapper<TClassA, TClassB>();
            TClassA a = CreateSampleClassA();
            TClassB b = CreateSampleClassB();
            TClassB unmappedB = CreateSampleClassB();
            mapper.Map(b, a);
            Assert.IsTrue(AreAllPropsUnchanged(b, unmappedB));
        }

        [TestMethod]
        public void MapAtoBDoesNotChangeUnmatchedPropertiesOfA()
        {
            Mapper<TClassA, TClassB> mapper = new Mapper<TClassA, TClassB>();
            TClassA a = CreateSampleClassA();
            TClassB b = CreateSampleClassB();
            TClassB unmappedB = CreateSampleClassB();
            mapper.Map(a, b);
            Assert.IsTrue(AreUnmatchedPropsUnchanged(b, unmappedB));
        }

        [TestMethod]
        public void MapBtoADoesNotChangeUnmatchedPropertiesOfA()
        {
            Mapper<TClassA, TClassB> mapper = new Mapper<TClassA, TClassB>();
            TClassA a = CreateSampleClassA();
            TClassA unmappedA = CreateSampleClassA();
            TClassB b = CreateSampleClassB();
            mapper.Map(b, a);
            Assert.IsTrue(AreUnmatchedPropsUnchanged(a, unmappedA));
        }

        [TestMethod]
        public void MapAtoBMapsSameNamePropertyValuesFromAtoB()
        {
            Mapper<TClassA, TClassB> mapper = new Mapper<TClassA, TClassB>();
            TClassA a = CreateSampleClassA();
            TClassA unmappedA = CreateSampleClassA();
            TClassB b = CreateSampleClassB();
            mapper.Map(a, b);
            Assert.IsTrue(AreSameNamePropsMappedFromAtoB(b, unmappedA));
        }



        [TestMethod]
        public void MapBtoAMapsSameNamePropertyValuesFromBtoA()
        {
            Mapper<TClassA, TClassB> mapper = new Mapper<TClassA, TClassB>();
            TClassA a = CreateSampleClassA();
            TClassB b = CreateSampleClassB();
            TClassB unmappedB = CreateSampleClassB();
            mapper.Map(b, a);
            Assert.IsTrue(AreSameNamePropsMappedFromBtoA(a, unmappedB));
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
            var (Names, _) = GetForcedMatchTestMetadata();
            int mappings = mapper.GetMappingsTotal;
            mapper.ForceMatch(Names);
            Assert.IsTrue(mappings + Names.Length == mapper.GetMappingsTotal);
        }

        [TestMethod]
        public void MapAtoBWithForcingMapsForcedMachedPropsFromAToB()
        {
            Mapper<TClassA, TClassB> mapper = new Mapper<TClassA, TClassB>();
            TClassA a = CreateSampleClassA();
            TClassA unmappedA = CreateSampleClassA();
            TClassB b = CreateSampleClassB();
            var (Names, AreEqual) = GetForcedMatchTestMetadata();
            mapper.ForceMatch(Names);
            mapper.Map(a, b);
            Assert.IsTrue(AreEqual(unmappedA, b));
        }

        [TestMethod]
        public void MapBtoAWithForcingMapsForcedMappedPropsFromBToA()
        {
            Mapper<TClassA, TClassB> mapper = new Mapper<TClassA, TClassB>();
            TClassA a = CreateSampleClassA();
            TClassB b = CreateSampleClassB();
            TClassB unmappedB = CreateSampleClassB();
            var (Names, AreEqual) = GetForcedMatchTestMetadata();
            mapper.ForceMatch(Names);
            mapper.Map(b, a);
            Assert.IsTrue(AreEqual(a, unmappedB));
        }

        [TestMethod]                                 //test fail message
        [ExpectedException(typeof(ArgumentException), "Non-existent property name was allowed")]
        public void ForceMatchThrowsArgumentExceptionWhenPropNameNotFound()
        {
            Mapper<TClassA, TClassB> mapper = new Mapper<TClassA, TClassB>();
            (string NameA, string NameB) = Get2PropNamesToForceMatch();
            string illegalName = "*" + NameA;
            mapper.ForceMatch(illegalName, NameB);

        }

        [TestMethod]                                 //test fail message
        [ExpectedException(typeof(ArgumentException), "Different property Types were allowed")]
        public void ForceMatchThrowsArgumentExceptionWhenMatchTypesDoNotMatch()
        {
            Mapper<TClassA, TClassB> mapper = new Mapper<TClassA, TClassB>();
            (string NameA, string NameB) = Get2PropNamesToForceMatchFromPropsWithDifferentTypes();
            mapper.ForceMatch(NameA, NameB);

        }

        [TestMethod]
        public void MapAtoBWithExclusionDoesNotMapExcludedProp()
        {
            Mapper<TClassA, TClassB> mapper = new Mapper<TClassA, TClassB>();
            TClassA a = CreateSampleClassA();
            TClassB b = CreateSampleClassB();
            TClassB unmappedB = CreateSampleClassB();
            var (Name, AreEqual) = GetExcludedMatchTestMetadata();
            mapper.Exclude(Name);
            mapper.Map(a, b);
            Assert.IsTrue(AreEqual(b, unmappedB));
        }


        [TestMethod]
        public void MapAtoBWithExclusionReturnsFalseIfNotFound()
        {
            Mapper<TClassA, TClassB> mapper = new Mapper<TClassA, TClassB>();
            var (Name, _) = GetExcludedMatchTestMetadata();
            var illegalName = "*" + Name;
            bool isSuccessful=mapper.Exclude(illegalName);
            Assert.IsFalse(isSuccessful);
        }

        [TestMethod]
        public void MapAtoBWithExclusionReturnsTrueIfFound()
        {
            Mapper<TClassA, TClassB> mapper = new Mapper<TClassA, TClassB>();
            var (Name, _) = GetExcludedMatchTestMetadata();
            bool isSuccessful = mapper.Exclude(Name);
            Assert.IsTrue(isSuccessful);
        }
    }
}