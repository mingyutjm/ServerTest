namespace Server3
{

    public interface IObjPool : IReference
    {
        public TypePair Id { get; }
        public int CountAll { get; }
        public int CountInActive { get; }
        public int CountActive { get; }
        public void Release(object obj);
    }

}