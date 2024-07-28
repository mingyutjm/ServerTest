namespace Server3
{

    public struct TypePair
    {
        public Type type1;
        public Type? type2;

        public static TypePair Create<TObj>()
        {
            return new TypePair() { type1 = typeof(TObj), type2 = null };
        }

        public static TypePair Create<TObj, TArg>()
        {
            return new TypePair() { type1 = typeof(TObj), type2 = typeof(TArg) };
        }

        public static TypePair Create(Type type1)
        {
            return new TypePair() { type1 = type1, type2 = null };
        }

        public static TypePair Create(Type type1, Type type2)
        {
            return new TypePair() { type1 = type1, type2 = type2 };
        }

        public override string ToString()
        {
            return $"<{type1.Name}, {type2?.Name}>";
        }
    }

    public class TypePairCompare : IEqualityComparer<TypePair>
    {
        public static TypePairCompare Instance = new TypePairCompare();

        public bool Equals(TypePair x, TypePair y)
        {
            return x.type1 == y.type1 && x.type2 == y.type2;
        }

        public int GetHashCode(TypePair obj)
        {
            int hash1 = obj.type1.GetHashCode();
            int hash2 = obj.type2.GetHashCode();
            return hash1 << 16 | hash2;
        }
    }

}