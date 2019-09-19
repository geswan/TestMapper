namespace Mapper
{
    public interface IMapper<TClassA, TClassB>
    {
        void Map(TClassA source, TClassB sink);
        void Map(TClassB source, TClassA sink);
        void ForceMatch(string propNameA, string propNameB);
        void ForceMatch((string propNameA, string propNameB)[] pairs);
        bool Exclude(string propName);

    }
}