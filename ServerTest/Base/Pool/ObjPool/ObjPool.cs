using System.Collections;

namespace Server3
{

    /// <summary>
    /// 类或结构体的对象池
    /// </summary>
    /// <typeparam name="TObj">对象类型</typeparam>
    /// <typeparam name="TArg">对象的构造参数</typeparam>
    public class ObjPool<TObj> : IObjPool where TObj : IPoolObj, new()
    {
        private TypePair _id = TypePair.Create<TObj>();
        protected int _countAll;
        protected int _maxCount = Int32.MaxValue;
        protected Stack<TObj> _objs = new Stack<TObj>(16);

        public TypePair Id => _id;
        public virtual int CountAll => _countAll;
        public virtual int CountInActive => _objs.Count;
        public virtual int CountActive => _countAll - _objs.Count;

        public TObj Get()
        {
            if (!_objs.TryPop(out var obj))
            {
                obj = new TObj();
                _countAll++;
            }
            obj.OnGet();
            return obj;
        }

        public void Release(TObj? obj)
        {
            if (obj == null)
                return;

            obj.OnRelease();
            if (_objs.Count < _maxCount)
            {
                _objs.Push(obj);
            }
            else
            {
                _countAll--;
                obj.Dispose();
            }
        }

        public void Release(object? obj)
        {
            if (obj is TObj pObj)
                Release(pObj);
        }

        public void SetMaxSize(int size)
        {
            _maxCount = size;
        }

        public void Dispose()
        {
            foreach (var obj in _objs)
            {
                obj.Dispose();
            }
            _objs.Clear();
        }
    }

    /// <summary>
    /// 类或结构体的对象池
    /// </summary>
    /// <typeparam name="TObj">对象类型</typeparam>
    /// <typeparam name="TArg">对象的构造参数</typeparam>
    public class ObjPool<TObj, TArg> : IObjPool where TObj : IPoolObj<TArg>, new()
    {
        private TypePair _id = TypePair.Create<TObj, TArg>();
        protected int _countAll;
        protected int _maxCount = Int32.MaxValue;
        protected Stack<TObj> _objs = new Stack<TObj>(16);

        public TypePair Id => _id;
        public virtual int CountAll => _countAll;
        public virtual int CountInActive => _objs.Count;
        public virtual int CountActive => _countAll - _objs.Count;

        public TObj Get(TArg arg)
        {
            if (!_objs.TryPop(out var obj))
            {
                obj = new TObj();
                _countAll++;
            }
            obj.OnGet(arg);
            return obj;
        }

        public void Release(TObj? obj)
        {
            if (obj == null)
                return;

            obj.OnRelease();
            if (_objs.Count < _maxCount)
            {
                _objs.Push(obj);
            }
            else
            {
                _countAll--;
                obj.Dispose();
            }
        }

        public void Release(object? obj)
        {
            if (obj is TObj pObj)
                Release(pObj);
        }

        public void SetMaxSize(int size)
        {
            _maxCount = size;
        }

        public void Dispose()
        {
            foreach (var obj in _objs)
            {
                obj.Dispose();
            }
            _objs.Clear();
        }
    }

}