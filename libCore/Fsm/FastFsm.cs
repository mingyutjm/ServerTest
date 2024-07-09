﻿namespace LibCore
{

    /// <summary>
    /// A simple state machine implementation.
    /// </summary>
    /// <typeparam name="TState"> State Type </typeparam>
    /// <typeparam name="TTrigger"> Trigger Type </typeparam>
    public class FastFsm<TState, TTrigger>
    {
        #region Internal Class

        private class Transition
        {
            public State to;
            public TTrigger trigger;
        }

        public class State
        {
            public readonly TState owner;
            private MAction<TState?> _enterAction;
            private MAction _exitAction;
            private MAction<float> _updateAction;
            private MAction _fixedUpdateAction;
            private List<Transition> _transitions;

            public State(TState owner)
            {
                this.owner = owner;
                _transitions = new List<Transition>();
            }

            public State Fire(TTrigger trigger)
            {
                foreach (var transition in _transitions)
                {
                    if (EqualityComparer<TTrigger>.Default.Equals(transition.trigger, trigger))
                    {
                        return transition.to;
                    }
                }
                return null;
            }

            public void OnEnter(TState? fromState)
            {
                _enterAction?.Invoke(fromState);
            }

            public void OnUpdate(float deltaTime)
            {
                _updateAction?.Invoke(deltaTime);
            }

            public void OnFixedUpdate()
            {
                _fixedUpdateAction?.Invoke();
            }

            public void OnExit()
            {
                _exitAction?.Invoke();
            }

            public bool HasTrigger(TTrigger trigger)
            {
                foreach (var transition in _transitions)
                {
                    if (EqualityComparer<TTrigger>.Default.Equals(transition.trigger, trigger))
                    {
                        return true;
                    }
                }
                return false;
            }

            public bool CanTrigger(TTrigger trigger)
            {
                foreach (var transition in _transitions)
                {
                    if (EqualityComparer<TTrigger>.Default.Equals(transition.trigger, trigger))
                    {
                        return true;
                    }
                }
                return false;
            }

            public void AddTransition(State to, TTrigger trigger)
            {
                _transitions.Add(new Transition() { to = to, trigger = trigger, });
            }

            public void SetEnterAction(MAction<TState> action)
            {
                _enterAction = action;
            }

            public void SetExitAction(MAction action)
            {
                _exitAction = action;
            }

            public void SetUpdateAction(MAction<float> action)
            {
                _updateAction = action;
            }

            public void SetFixedUpdateAction(MAction action)
            {
                _fixedUpdateAction = action;
            }

            public void Clear()
            {
                _enterAction = null;
                _exitAction = null;
                _updateAction = null;
                _transitions.Clear();
            }
        }

        // Transition Config
        public class TransitionConfig
        {
            private readonly State _from;
            private readonly FastFsm<TState, TTrigger> _machine;

            public TransitionConfig(State from, FastFsm<TState, TTrigger> machine)
            {
                _from = from;
                _machine = machine;
            }

            public TransitionConfig Permit(TState nextState, TTrigger trigger)
            {
                if (_from.HasTrigger(trigger))
                    return this;

                var toState = _machine.GetState(nextState);
                if (toState != null)
                    _from.AddTransition(toState, trigger);
                return this;
            }

            public TransitionConfig OnEnter(MAction<TState> action)
            {
                _from.SetEnterAction(action);
                return this;
            }

            public TransitionConfig OnUpdate(MAction<float> action)
            {
                _from.SetUpdateAction(action);
                return this;
            }

            public TransitionConfig OnFixedUpdate(MAction action)
            {
                _from.SetFixedUpdateAction(action);
                return this;
            }

            public TransitionConfig OnExit(MAction action)
            {
                _from.SetExitAction(action);
                return this;
            }
        }

        #endregion Internal Class

        private List<State> _states;
        private State? _currentState;
        private bool _isStarted = false;

        public bool IsStarted => _isStarted;
        public TState? CurrentState => _currentState.owner;

        public FastFsm()
        {
            if (!typeof(TState).IsEnum)
            {
                throw new Exception("TState must be enum type");
            }

            var states = Enum.GetValues(typeof(TState)).Cast<TState>().ToList();
            _states = new List<State>();
            for (int i = 0; i < states.Count; i++)
            {
                State state = new State(states[i]);
                _states.Add(state);
            }
        }

        public FastFsm(List<TState> states)
        {
            _states = new List<State>();
            for (int i = 0; i < states.Count; i++)
            {
                State state = new State(states[i]);
                _states.Add(state);
            }
        }

        public void Start(TState initialState)
        {
            State state = GetState(initialState);
            if (state == null)
            {
                // ERROR!!
                return;
            }
            _currentState = state;
            _currentState.OnEnter(default);
            _isStarted = true;
        }

        public void Update(float deltaTime)
        {
            if (_isStarted)
                _currentState.OnUpdate(deltaTime);
        }

        public void FixedUpdate()
        {
            if (_isStarted)
                _currentState.OnFixedUpdate();
        }

        public TransitionConfig Configure(TState state)
        {
            State findState = GetState(state);
            if (findState != null)
            {
                return new TransitionConfig(findState, this);
            }
            // ERROR! ThrowException
            State newState = new State(state);
            _states.Add(newState);
            return new TransitionConfig(newState, this);
        }

        public State? GetState(TState state)
        {
            for (var i = 0; i < _states.Count; i++)
            {
                if (EqualityComparer<TState>.Default.Equals(_states[i].owner, state))
                {
                    return _states[i];
                }
            }
            return null;
        }

        public bool HasState(TState state)
        {
            for (var i = 0; i < _states.Count; i++)
            {
                if (EqualityComparer<TState>.Default.Equals(_states[i].owner, state))
                {
                    return true;
                }
            }
            return false;
        }

        public void ChangeState(TState state)
        {
            if (EqualityComparer<TState>.Default.Equals(_currentState.owner, state))
                return;

            for (var i = 0; i < _states.Count; i++)
            {
                if (EqualityComparer<TState>.Default.Equals(_states[i].owner, state))
                {
                    _currentState.OnExit();
                    TState oldState = _currentState.owner;
                    _currentState = _states[i];
                    _currentState.OnEnter(oldState);
                }
            }
        }

        // Send Trigger
        public void Fire(TTrigger trigger)
        {
            var nextState = _currentState.Fire(trigger);
            if (nextState != null)
            {
                _currentState.OnExit();
                TState oldState = _currentState.owner;
                _currentState = nextState;
                _currentState.OnEnter(oldState);
            }
        }

        public bool CanTrigger(TTrigger trigger)
        {
            return _currentState.CanTrigger(trigger);
        }

        public void Destroy()
        {
            if (_currentState != null)
                _currentState.OnExit();
            _isStarted = false;
            for (int i = 0; i < _states.Count; i++)
            {
                _states[i].Clear();
            }
            _states.Clear();
            _currentState = null;
        }
    }

}