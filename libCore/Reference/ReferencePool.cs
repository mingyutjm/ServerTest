namespace LibCore
{

    public class ReferencePool
    {
        private static readonly Dictionary<Type, ReferenceCollection> s_referenceCollections = new Dictionary<Type, ReferenceCollection>();

        public static int Count => s_referenceCollections.Count;

        public static ReferencePoolInfo[] GetAllReferencePoolInfos()
        {
            int index = 0;
            ReferencePoolInfo[] results = null;

            // lock (s_referenceCollections)
            {
                results = new ReferencePoolInfo[s_referenceCollections.Count];
                foreach (KeyValuePair<Type, ReferenceCollection> referenceCollection in s_referenceCollections)
                {
                    results[index++] = new ReferencePoolInfo(referenceCollection.Key,
                                                             referenceCollection.Value.UnusedReferenceCount,
                                                             referenceCollection.Value.UsingReferenceCount,
                                                             referenceCollection.Value.GetReferenceCount,
                                                             referenceCollection.Value.ReleaseReferenceCount,
                                                             referenceCollection.Value.AddReferenceCount,
                                                             referenceCollection.Value.RemoveReferenceCount);
                }
            }

            return results;
        }

        public static void ClearAll()
        {
            // lock (s_referenceCollections)
            {
                foreach (KeyValuePair<Type, ReferenceCollection> referenceCollection in s_referenceCollections)
                {
                    referenceCollection.Value.RemoveAll();
                }

                s_referenceCollections.Clear();
            }
        }

        public static T Get<T>() where T : class, IReference, new()
        {
            return GetReferenceCollection(typeof(T)).Get<T>();
        }

        public static IReference Get(Type referenceType)
        {
            InternalCheckReferenceType(referenceType);
            return GetReferenceCollection(referenceType).Get();
        }

        public static void Release(IReference reference)
        {
            if (reference == null)
            {
                throw new Exception("Reference is invalid.");
            }

            Type referenceType = reference.GetType();
            InternalCheckReferenceType(referenceType);
            
            GetReferenceCollection(referenceType).Release(reference);
        }

        public static void Add<T>(int count) where T : class, IReference, new()
        {
            GetReferenceCollection(typeof(T)).Add<T>(count);
        }

        public static void Add(Type referenceType, int count)
        {
            InternalCheckReferenceType(referenceType);
            GetReferenceCollection(referenceType).Add(count);
        }

        public static void Remove<T>(int count) where T : class, IReference
        {
            GetReferenceCollection(typeof(T)).Remove(count);
        }

        public static void Remove(Type referenceType, int count)
        {
            InternalCheckReferenceType(referenceType);
            GetReferenceCollection(referenceType).Remove(count);
        }

        public static void RemoveAll<T>() where T : class, IReference
        {
            GetReferenceCollection(typeof(T)).RemoveAll();
        }

        public static void RemoveAll(Type referenceType)
        {
            InternalCheckReferenceType(referenceType);
            GetReferenceCollection(referenceType).RemoveAll();
        }

        private static void InternalCheckReferenceType(Type referenceType)
        {
            if (referenceType == null)
            {
                throw new Exception("Reference type is invalid.");
            }

            // if (!referenceType.IsClass || referenceType.IsAbstract)
            // {
            //     throw new Exception("Reference type is not a non-abstract class type.");
            // }

            // if (!typeof(IReference).IsAssignableFrom(referenceType))
            // {
            //     throw new Exception($"Reference type '{referenceType.FullName}' is invalid.");
            // }
        }

        private static ReferenceCollection GetReferenceCollection(Type referenceType)
        {
            if (referenceType == null)
            {
                throw new Exception("ReferenceType is invalid.");
            }

            ReferenceCollection referenceCollection = null;
            // lock (s_referenceCollections)
            {
                if (!s_referenceCollections.TryGetValue(referenceType, out referenceCollection))
                {
                    referenceCollection = new ReferenceCollection(referenceType);
                    s_referenceCollections.Add(referenceType, referenceCollection);
                }
            }

            return referenceCollection;
        }
    }

}