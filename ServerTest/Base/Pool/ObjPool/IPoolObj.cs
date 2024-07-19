namespace Server3
{

    public interface IPoolObj<TArg> : IReference
    {
        /// <summary>
        /// On Get from pool
        /// </summary>
        /// <param name="arg"></param>
        public void OnGet(TArg arg);

        /// On back to pool
        public void OnRelease();
    }

}