namespace Mapper
{
	public interface IMapper<TClassA, TClassB>
	{
		void Map(TClassA source, TClassB sink);
		void Map(TClassB source, TClassA sink);
         void Pair(string propNameA, string propNameB);
        void Pair((string propNameA, string propNameB)[] pairs);

    }
}