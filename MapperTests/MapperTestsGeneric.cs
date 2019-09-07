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
		protected abstract bool AreAllPropsUnchanged(TClassA a);
		protected abstract bool AreUnsharedPropsUnchanged(TClassA a);
		protected abstract bool AreSharedPropsInClassAMatchedToThoseInClassB(TClassA a);
		protected abstract bool AreAllPropsUnchanged(TClassB b);
		protected abstract bool AreUnsharedPropsUnchanged(TClassB b);
		protected abstract bool AreSharedPropsInClassBMatchedToThoseInClassA(TClassB b);

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
	}
}