namespace LibCore
{

    public struct ReferencePoolInfo
    {
        private readonly Type _type;
        private readonly int _unusedReferenceCount;
        private readonly int _usingReferenceCount;
        private readonly int _acquireReferenceCount;
        private readonly int _releaseReferenceCount;
        private readonly int _addReferenceCount;
        private readonly int _removeReferenceCount;

        public Type Type => _type;
        public int UnusedReferenceCount => _unusedReferenceCount;
        public int UsingReferenceCount => _usingReferenceCount;
        public int AcquireReferenceCount => _acquireReferenceCount;
        public int ReleaseReferenceCount => _releaseReferenceCount;
        public int AddReferenceCount => _addReferenceCount;
        public int RemoveReferenceCount => _removeReferenceCount;

        public ReferencePoolInfo(Type type,
                                 int unusedReferenceCount,
                                 int usingReferenceCount,
                                 int acquireReferenceCount,
                                 int releaseReferenceCount,
                                 int addReferenceCount,
                                 int removeReferenceCount)
        {
            _type = type;
            _unusedReferenceCount = unusedReferenceCount;
            _usingReferenceCount = usingReferenceCount;
            _acquireReferenceCount = acquireReferenceCount;
            _releaseReferenceCount = releaseReferenceCount;
            _addReferenceCount = addReferenceCount;
            _removeReferenceCount = removeReferenceCount;
        }
    }

}