namespace TestMapper
{
	public interface IMapper<TClassA, TClassB>
	{
		void Map(TClassA source, TClassB sink);
		void Map(TClassB source, TClassA sink);
	}
}