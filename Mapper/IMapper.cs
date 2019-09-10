namespace Mapper
{
	public interface IMapper<TClassA, TClassB>
	{
		void Map(TClassA source, TClassB sink);
		void Map(TClassB source, TClassA sink);
        TClassA Map(TClassB source);
        TClassB Map(TClassA source);
        void Pair(string propNameA, string propNameB);
        void Pair((string propNameA, string propNameB)[] pairs);

    }
}