namespace LibCore
{

    public interface IComp : IReference
    {
        ICompOwner Owner { get; }
        int UpdateOrder { get; }
        bool IsEnabled { get; }
        void SetOwner(ICompOwner owner);
        void Dispose();
        void Enable();
        void Disable();
        void SetEnabled(bool enabled);
        void OnRemove();
        void Update(float dt);
        void FixedUpdate();
        void LateUpdate();
        void LateFixedUpdate();
    }

}