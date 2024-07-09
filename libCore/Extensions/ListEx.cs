using System.Runtime.CompilerServices;

namespace LibCore
{

    public delegate int Compare<T>(T x, T y);
    public delegate K MatchPattern<T, K>(T x);

    public static class ListEx
    {
        public static Span<T> AsSpan<T>(List<T> list)
        {
            if (list == null)
                return default(Span<T>);
            StrongBox<T[]> box = Unsafe.As<StrongBox<T[]>>(list);
            return new Span<T>(box.Value, 0, list.Count);
        }

        public static void Resize<T>(this IList<T> list, int newSize, Func<T> factory)
        {
            if (list.Count == newSize)
                return;

            if (list.Count < newSize)
            {
                int countDiff = newSize - list.Count;
                for (int i = 0; i < countDiff; i++)
                {
                    if (factory != null)
                        list.Add(factory());
                    else
                        list.Add(default);
                }
                return;
            }

            if (list.Count > newSize)
            {
                int countDiff = newSize - list.Count;
                for (int i = 0; i < countDiff; i++)
                {
                    list.RemoveAt(list.Count - 1);
                }
            }
        }

        public static bool BinarySearch<T>(IList<T> list, T target, Compare<T> comp, out int index)
        {
            index = LowerBound(list, target, comp);
            if (index == list.Count || comp(list[index], target) != 0)
                return false;
            return true;
        }

        public static bool BinarySearch<T, K>(IList<T> list,
                                              K target,
                                              MatchPattern<T, K> matchPattern,
                                              Compare<K> comp,
                                              out int index)
        {
            index = LowerBound(list, target, matchPattern, comp);
            if (index == list.Count || comp(matchPattern(list[index]), target) != 0)
                return false;
            return true;
        }

        public static int LowerBound<T>(IList<T> list, T target, Compare<T> comp)
        {
            return LowerBound(list, 0, list.Count, target, comp);
        }

        public static int UpperBound<T>(IList<T> list, T target, Compare<T> comp)
        {
            return UpperBound(list, 0, list.Count, target, comp);
        }

        public static int LowerBound<T, K>(IList<T> list, K target, MatchPattern<T, K> matchPattern, Compare<K> comp)
        {
            return LowerBound(list, 0, list.Count, target, matchPattern, comp);
        }

        public static int UpperBound<T, K>(IList<T> list, K target, MatchPattern<T, K> matchPattern, Compare<K> comp)
        {
            return UpperBound(list, 0, list.Count, target, matchPattern, comp);
        }

        public static int LowerBound<T>(IList<T> list, int index, int count, T target, Compare<T> comp)
        {
            int lo = index;
            int hi = index + count;
            while (lo < hi)
            {
                int mid = (lo + hi) / 2;
                T midVal = list[mid];
                int compRes = comp(midVal, target);
                // match
                if (compRes == 0)
                {
                    hi = mid;
                }
                // mid is small
                else if (compRes < 0)
                {
                    lo = mid + 1;
                }
                // mid is big
                else if (compRes > 0)
                {
                    hi = mid;
                }
            }
            return hi;
        }

        public static int UpperBound<T>(IList<T> list, int index, int count, T target, Compare<T> comp)
        {
            int lo = index;
            int hi = index + count;
            while (lo < hi)
            {
                int mid = (lo + hi) / 2;
                T midVal = list[mid];
                int compRes = comp(midVal, target);
                // match
                if (compRes == 0)
                {
                    lo = mid + 1;
                }
                // mid is small
                else if (compRes < 0)
                {
                    lo = mid + 1;
                }
                // mid is big
                else if (compRes > 0)
                {
                    hi = mid;
                }
            }
            return lo;
        }

        public static int LowerBound<T, K>(IList<T> list,
                                           int index,
                                           int count,
                                           K target,
                                           MatchPattern<T, K> matchPattern,
                                           Compare<K> comp)
        {
            int lo = index;
            int hi = index + count;
            while (lo < hi)
            {
                int mid = (lo + hi) / 2;
                K midVal = matchPattern(list[mid]);
                int compRes = comp(midVal, target);
                // match
                if (compRes == 0)
                {
                    hi = mid;
                }
                // mid is small
                else if (compRes < 0)
                {
                    lo = mid + 1;
                }
                // mid is big
                else if (compRes > 0)
                {
                    hi = mid;
                }
            }
            return hi;
        }

        public static int UpperBound<T, K>(IList<T> list,
                                           int index,
                                           int count,
                                           K target,
                                           MatchPattern<T, K> matchPattern,
                                           Compare<K> comp)
        {
            int lo = index;
            int hi = index + count;
            while (lo < hi)
            {
                int mid = (lo + hi) / 2;
                K midVal = matchPattern(list[mid]);
                int compRes = comp(midVal, target);
                // match
                if (compRes == 0)
                {
                    lo = mid + 1;
                }
                // mid is small
                else if (compRes < 0)
                {
                    lo = mid + 1;
                }
                // mid is big
                else if (compRes > 0)
                {
                    hi = mid;
                }
            }
            return hi;
        }
    }

}