namespace LibCore
{
    public interface IFsmState
    {
    }

    public abstract class FsmState<TOwner> : IReference, IFsmState
    {
        public struct Transition
        {
            public Type to;

            // trigger == 0 is not valid
            public string trigger;
        }

        protected Fsm<TOwner> _fsm;
        protected List<Transition> _transitions = new List<Transition>();
        private FsmState<TOwner> _fromState;

        public TOwner Owner => _fsm.Owner;
        public FsmState<TOwner> FromState => _fromState;

        public static TState Create<TState>() where TState : FsmState<TOwner>, new()
        {
            TState state = ReferencePool.Get<TState>();
            return state;
        }

        public void Init(Fsm<TOwner> fsm)
        {
            _fsm = fsm;
            OnInit();
        }

        public void Dispose()
        {
            ReferencePool.Release(this);
        }

        public Type Trigger(string trigger)
        {
            foreach (var transition in _transitions)
            {
                if (transition.trigger.Equals(trigger))
                {
                    return transition.to;
                }
            }

            return null;
        }

        public FsmState<TOwner> Permit<TState>(string trigger) where TState : FsmState<TOwner>, new()
        {
            // Check if the state is already permitted
            foreach (var transition in _transitions)
            {
                if (transition.trigger.Equals(trigger))
                    return this;
            }

            // new permit
            _transitions.Add(new Transition() { to = typeof(TState), trigger = trigger });
            return this;
        }

        public void SetFromState(FsmState<TOwner> fromState)
        {
            _fromState = fromState;
        }

        protected virtual void OnInit()
        {
        }

        public virtual void OnEnter()
        {
        }

        public virtual void OnUpdate(float deltaTime)
        {
        }

        public virtual void OnFixedUpdate()
        {
        }

        public virtual void OnLateUpdate()
        {
        }

        public virtual void OnAnimatorUpdate()
        {
        }

        /// NOTE: Don't fire trigger in OnExit
        public virtual void OnExit()
        {
        }

        public virtual void Clear()
        {
            _transitions.Clear();
        }
    }
}