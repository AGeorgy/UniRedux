using System;
using System.Collections.Generic;

namespace UniRedux.Redux
{
    public class StateProvider<TState> : IReduxObservable<TState>
    {
        private readonly List<IReduxObserver<TState>> _observers;
        private readonly object _lockObject = new();
        private readonly TState _state;

        public StateProvider(TState state)
        {
            _state = state;
            _observers = new List<IReduxObserver<TState>>();
        }

        public void Invoke(TState value)
        {
            foreach (var observer in _observers)
            {
                observer.Invoke(value);
            }
        }

        private void ForceInvoke(IReduxObserver<TState> observer)
        {
            observer.ForceInvoke(_state);
        }

        public IDisposable Subscribe(IReduxObserver<TState> observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }

            return new Subscription(this, observer);
        }

        private sealed class Subscription : IDisposable
        {
            private bool _isDisposed;
            private StateProvider<TState> _stateProvider;
            private IReduxObserver<TState> _observer;

            public Subscription(StateProvider<TState> stateProvider, IReduxObserver<TState> observer)
            {
                _stateProvider = stateProvider;
                _observer = observer;
                _stateProvider.ForceInvoke(_observer);
            }

            public void Dispose()
            {
                if (!_isDisposed)
                {
                    _isDisposed = true;
                    lock (_stateProvider._lockObject)
                    {
                        if (_observer != null && _stateProvider._observers.Contains(_observer))
                            _stateProvider._observers.Remove(_observer);
                    }

                    _stateProvider = null;
                    _observer = null;
                }
            }
        }
    }
}