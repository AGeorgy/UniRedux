using System;

namespace UniRedux.Redux
{
    public class SelectObservable<TState, TPartialState> : IReduxSelectObservable<TPartialState>, IReduxObserver<TState>
    {
        private readonly IReduxObservable<TState> _source;
        private readonly Func<TState, TPartialState> _selector;
        private IReduxObserver<TPartialState> _observer;
        private TPartialState _lastState;

        public SelectObservable(IReduxObservable<TState> source, Func<TState, TPartialState> selector)
        {
            _lastState = default;
            _source = source;
            _selector = selector;
        }

        public IDisposable Subscribe(IReduxObserver<TPartialState> observer)
        {
            _observer = observer;
            return _source.Subscribe(this);
        }
        
        public IReduxSelectObservable<TNewPartialState> Select<TNewPartialState>(Func<TPartialState, TNewPartialState> selector)
        {
            return new SelectObservable<TPartialState, TNewPartialState>(this, selector);
        }

        public void Invoke(TState value)
        {
            if (IsValid)
            {
                var state = _selector.Invoke(value);
                if (!Equals(_lastState, state))
                {
                    _lastState = state;
                    _observer.Invoke(_lastState);
                }
            }
        }

        public void ForceInvoke(TState value)
        {
            if (IsValid)
            {
                var state = _selector.Invoke(value);
                _lastState = state;
                _observer.ForceInvoke(_lastState);
            }
        }

        private bool IsValid => _selector != null && _observer != null;
    }
}