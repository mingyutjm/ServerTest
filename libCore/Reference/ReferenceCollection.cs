﻿namespace LibCore
{

    public class ReferenceCollection
    {
        private readonly Queue<IReference> _references;
        private readonly Type _referenceType;
        private int _usingReferenceCount;
        private int _acquireReferenceCount;
        private int _releaseReferenceCount;
        private int _addReferenceCount;
        private int _removeReferenceCount;

        public Type ReferenceType => _referenceType;
        public int UnusedReferenceCount => _references.Count;
        public int UsingReferenceCount => _usingReferenceCount;
        public int GetReferenceCount => _acquireReferenceCount;
        public int ReleaseReferenceCount => _releaseReferenceCount;
        public int AddReferenceCount => _addReferenceCount;
        public int RemoveReferenceCount => _removeReferenceCount;

        public ReferenceCollection(Type referenceType)
        {
            _references = new Queue<IReference>();
            _referenceType = referenceType;
            _usingReferenceCount = 0;
            _acquireReferenceCount = 0;
            _releaseReferenceCount = 0;
            _addReferenceCount = 0;
            _removeReferenceCount = 0;
        }

        public T Get<T>() where T : class, IReference, new()
        {
            if (typeof(T) != _referenceType)
            {
                throw new Exception("Type is invalid.");
            }

            _usingReferenceCount++;
            _acquireReferenceCount++;
            // lock (_references)
            {
                if (_references.Count > 0)
                {
                    return (T)_references.Dequeue();
                }
            }

            _addReferenceCount++;
            return new T();
        }

        public IReference Get()
        {
            _usingReferenceCount++;
            _acquireReferenceCount++;
            // lock (_references)
            {
                if (_references.Count > 0)
                {
                    return _references.Dequeue();
                }
            }

            _addReferenceCount++;
            return (IReference)Activator.CreateInstance(_referenceType);
        }

        public void Release(IReference reference)
        {
            reference.Clear();
            // lock (_references)
            {
                // Profiler.BeginSample("Release(IReference reference) - if (_references.Contains(reference));");
                // if (_references.Contains(reference))
                // {
                //     throw new Exception("The reference has been released.");
                // }
                // Profiler.EndSample();

                _references.Enqueue(reference);
            }

            _releaseReferenceCount++;
            _usingReferenceCount--;
        }

        public void Add<T>(int count) where T : class, IReference, new()
        {
            if (typeof(T) != _referenceType)
            {
                throw new Exception("Type is invalid.");
            }

            // lock (_references)
            {
                _addReferenceCount += count;
                while (count-- > 0)
                {
                    _references.Enqueue(new T());
                }
            }
        }

        public void Add(int count)
        {
            // lock (_references)
            {
                _addReferenceCount += count;
                while (count-- > 0)
                {
                    _references.Enqueue((IReference)Activator.CreateInstance(_referenceType));
                }
            }
        }

        public void Remove(int count)
        {
            // lock (_references)
            {
                if (count > _references.Count)
                {
                    count = _references.Count;
                }

                _removeReferenceCount += count;
                while (count-- > 0)
                {
                    _references.Dequeue();
                }
            }
        }

        public void RemoveAll()
        {
            // lock (_references)
            {
                _removeReferenceCount += _references.Count;
                _references.Clear();
            }
        }
    }

}