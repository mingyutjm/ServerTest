namespace Server3.Pool.Example
{

    public class ExampleObj1 : IPoolObj
    {
        public void Dispose()
        {
        }

        public void OnGet()
        {
        }

        public void OnRelease()
        {
        }
    }

    public class ExampleObj2 : IPoolObj<int>
    {
        public void Dispose()
        {
        }

        public void OnGet(int arg)
        {
        }

        public void OnRelease()
        {
        }
    }

    public class ObjPoolExample
    {
        public void Example1()
        {
            ExampleObj1 obj1 = Obj.Create<ExampleObj1>();
            Obj.Destroy(obj1);

            ExampleObj2 obj2 = Obj.Create<ExampleObj2, int>(1);
            Obj.Destroy(obj2);
        }
    }

}