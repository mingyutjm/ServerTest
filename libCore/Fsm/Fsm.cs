namespace LibCore
{

    // NOTE!!!
    // Trigger is just a 'string'
    // Trigger is not important, but can help us to see the structure of state machine
    // We can simply change state by calling 'ChangeState<TState>()' or 'Fire(string trigger)' if we configured the state's trigger
    // Fsm itself can be a state, so we can nest Fsm in Fsm
    // A sample can be found in Test/TestFsm
    public partial class Fsm<TOwner> : FsmState<TOwner>
    {
        public static Fsm<T> Create<T>(T owner, IEnumerable<FsmState<T>> states = null) where T : class
        {
            if (owner == null)
            {
                throw new Exception($"Fsm: {typeof(T).FullName} owner is invalid");
                // Log.Error($"Fsm {name} owner is null");
                // return null;
            }

            Fsm<T> fsm = ReferencePool.Get<Fsm<T>>();
            fsm._owner = owner;
            if (states == null)
                return fsm;

            foreach (var state in states)
            {
                if (state == null)
                    throw new Exception("Fsm states is null");

                Type stateType = state.GetType();
                if (fsm._states.ContainsKey(stateType))
                {
                    throw new Exception($"Fsm already exist state {stateType.FullName}");
                    // Log.Error($"Fsm {name} already exist state {stateType.FullName}");
                    // return null;
                }
                fsm._states.Add(stateType, state);
                state.Init(fsm);
            }
            return fsm;
        }

        // 派生自Fsm的类，用这个函数Create
        public static TFsm Create<TOwn, TFsm>(TOwn owner, IEnumerable<FsmState<TOwn>> states = null) where TFsm : Fsm<TOwn>, new()
        {
            if (owner == null)
            {
                throw new Exception($"Fsm: {typeof(TOwn).FullName} owner is invalid");
                // Log.Error($"Fsm {name} owner is null");
                // return null;
            }

            TFsm fsm = ReferencePool.Get<TFsm>();
            fsm._owner = owner;
            if (states == null)
                return fsm;

            foreach (var state in states)
            {
                if (state == null)
                    throw new Exception("Fsm states is null");

                Type stateType = state.GetType();
                if (fsm._states.ContainsKey(stateType))
                {
                    throw new Exception($"Fsm already exist state {stateType.FullName}");
                    // Log.Error($"Fsm {name} already exist state {stateType.FullName}");
                    // return null;
                }
                fsm._states.Add(stateType, state);
                state.Init(fsm);
            }
            return fsm;
        }
    }

    public partial class Fsm<TOwner> : FsmState<TOwner>
    {
        private TOwner _owner;
        private FsmState<TOwner> _currentState;
        private Dictionary<Type, FsmState<TOwner>> _states;

        public TOwner Owner => _owner;
        public bool IsRunning => _currentState != null;
        public FsmState<TOwner> CurrentState => _currentState;

        public Fsm()
        {
            _states = new Dictionary<Type, FsmState<TOwner>>();
        }

        public void SetOwner(TOwner owner)
        {
            _owner = owner;
        }

        public void Start<TState>() where TState : FsmState<TOwner>
        {
            var state = GetState<TState>();
            if (state == null)
            {
                throw new Exception($"Fsm {typeof(TState).FullName} is not exist");
                // Log.Error($"Fsm {typeof(TState).FullName} is not exist");
                // return;
            }
            _currentState = state;
            _currentState.OnEnter();
        }

        public void Start(Type stateType)
        {
            var state = GetState(stateType);
            if (state == null)
            {
                throw new Exception($"Fsm {stateType.FullName} is not exist");
            }
            _currentState = state;
            _currentState.OnEnter();
        }

        public TState Configure<TState>() where TState : FsmState<TOwner>, new()
        {
            if (!HasState<TState>())
            {
                var state = ReferencePool.Get<TState>();
                _states.Add(typeof(TState), state);
                state.Init(this);
            }
            return GetState<TState>();
        }

        public TState AddState<TState>() where TState : FsmState<TOwner>, new()
        {
            return Configure<TState>();
        }

        public void AddStates(IEnumerable<FsmState<TOwner>> states)
        {
            foreach (var state in states)
            {
                if (state == null)
                    throw new Exception("Fsm states is null");

                Type stateType = state.GetType();
                if (_states.ContainsKey(stateType))
                {
                    throw new Exception($"Fsm already exist state {stateType.FullName}");
                    // Log.Error($"Fsm {name} already exist state {stateType.FullName}");
                    // return null;
                }
                _states.Add(stateType, state);
                state.Init(this);
            }
        }

        // All trigger is string
        public void Fire(string trigger)
        {
            var nextStateType = _currentState.Trigger(trigger);
            if (nextStateType == null)
            {
                Log.Error($"State {_currentState.GetType().Name} cannot permit trigger {trigger}");
                return;
            }

            var nextState = GetState(nextStateType);
            if (nextState == null)
            {
                Log.Error($"Fsm does not contain state {nextStateType.Name}");
                return;
            }

            var oldState = _currentState;
            oldState.OnExit();
            _currentState = nextState;
            _currentState.SetFromState(oldState);
            _currentState.OnEnter();
        }

        public void ChangeState<TState>() where TState : FsmState<TOwner>
        {
            ChangeState(typeof(TState));
        }

        public void ChangeState(Type stateType)
        {
            if (_currentState == null)
            {
                Start(stateType);
                return;
            }
            if (_currentState.GetType().FullName == stateType.FullName)
                return;

            var newState = GetState(stateType);
            if (newState != null)
            {
                var oldState = _currentState;
                oldState.OnExit();
                _currentState = newState;
                _currentState.SetFromState(oldState);
                _currentState.OnEnter();
            }
        }

        public bool HasState<TState>() where TState : FsmState<TOwner>
        {
            return HasState(typeof(TState));
        }

        public bool HasState(Type stateType)
        {
            return _states.ContainsKey(stateType);
        }

        public TState GetState<TState>() where TState : FsmState<TOwner>
        {
            return GetState(typeof(TState)) as TState;
        }

        public FsmState<TOwner> GetState(Type stateType)
        {
            if (_states.TryGetValue(stateType, out var state))
            {
                return state;
            }
            return null;
        }

        public void GetAllStates(List<FsmState<TOwner>> states)
        {
            if (states == null)
            {
                throw new Exception("States is invalid");
                // Log.Error("States is invalid");
                // return;
            }

            states.Clear();
            foreach (var state in _states.Values)
            {
                states.Add(state);
            }
        }

        public void Update(float deltaTime)
        {
            if (IsRunning)
                _currentState.OnUpdate(deltaTime);
        }

        public void FixedUpdate()
        {
            if (IsRunning)
                _currentState.OnFixedUpdate();
        }

        public void LateUpdate()
        {
            if (IsRunning)
                _currentState.OnLateUpdate();
        }

        public void AnimatorUpdate()
        {
            if (IsRunning)
                _currentState.OnAnimatorUpdate();
        }

        public override void OnUpdate(float deltaTime)
        {
            Update(deltaTime);
        }

        public override void OnFixedUpdate()
        {
            FixedUpdate();
        }

        public override void OnLateUpdate()
        {
            LateUpdate();
        }

        public override void OnAnimatorUpdate()
        {
            AnimatorUpdate();
        }

        public new void Dispose()
        {
            if (_currentState != null)
                _currentState.OnExit();
            foreach (var state in _states.Values)
            {
                state.Dispose();
            }
            _states.Clear();
            _currentState = null;
        }
    }

}