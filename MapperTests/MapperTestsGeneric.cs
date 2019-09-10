using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestMapper;
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
        protected abstract bool AreAllPropsUnchanged(TClassA a);
		protected abstract bool AreUnsharedPropsUnchanged(TClassA a);
		protected abstract bool AreSharedPropsInClassAMatchedToThoseInClassB(TClassA a);
		protected abstract bool AreAllPropsUnchanged(TClassB b);
		protected abstract bool AreUnsharedPropsUnchanged(TClassB b);
		protected abstract bool AreSharedPropsInClassBMatchedToThoseInClassA(TClassB b);
        protected abstract bool ArePairedNamesMappedFromClassAToClassB(TClassB b);
        protected abstract bool ArePairedNamesMappedFromClassBToClassA(TClassA a);

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
			Assert.IsTrue(AreUnsharedPropsUnchanged(b));
		}



		[TestMethod]
		public void MapClassAClassBSetsClassBsSharedPropertiesToClassAsSharedPropertiesValues()
		{
			Mapper<TClassA, TClassB> mapper = new Mapper<TClassA, TClassB>();
			TClassA a = CreateSampleClassA();
			TClassB b = CreateSampleClassB();
			mapper.Map(a, b);
			Assert.IsTrue(AreSharedPropsInClassBMatchedToThoseInClassA(b));
		}



		[TestMethod]
		public void MapClassBClassADoesNotChangeClassAsUnmatchedProperties()
		{
			Mapper<TClassA, TClassB> mapper = new Mapper<TClassA, TClassB>();
			TClassA a = CreateSampleClassA();
			TClassB b = CreateSampleClassB();
			mapper.Map(b, a);
			Assert.IsTrue(AreUnsharedPropsUnchanged(a));
		}

		[TestMethod]
		public void MapClassBClassASetsClassAsSharedPropertiesToClassBsSharedPropertiesValues()
		{
			Mapper<TClassA, TClassB> mapper = new Mapper<TClassA, TClassB>();
			TClassA a = CreateSampleClassA();
			TClassB b = CreateSampleClassB();
			mapper.Map(b, a);
			Assert.IsTrue(AreSharedPropsInClassAMatchedToThoseInClassB(a));
		}

        [TestMethod]
        public void PairAddsATupleToMatchingProperties()
        {
            Mapper<TClassA, TClassB> mapper = new Mapper<TClassA, TClassB>();
            (string NameA, string NameB)= Get2NamesToPair();
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
            Assert.IsTrue(ArePairedNamesMappedFromClassAToClassB(b));
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
            Assert.IsTrue(ArePairedNamesMappedFromClassBToClassA(a));
        }
    }
}